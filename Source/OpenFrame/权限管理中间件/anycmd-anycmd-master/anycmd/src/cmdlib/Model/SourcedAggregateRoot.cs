
namespace Anycmd.Model
{
    using Events;
    using Snapshots;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using Util;

    /// <summary>
    /// 表示支持事件溯源机制的聚合跟的基类。
    /// </summary>
    /// <typeparam name="TAggregateRootId">聚合跟标识类型参数</typeparam>
    public abstract class SourcedAggregateRoot<TAggregateRootId> : ISourcedAggregateRoot<TAggregateRootId>
    {
        #region Private Fields
        private TAggregateRootId _id;
        private long _version;
        private long _eventVersion;
        private long _branch;
        private readonly List<IDomainEvent<TAggregateRootId>> _uncommittedEvents = new List<IDomainEvent<TAggregateRootId>>();
        private readonly Dictionary<Type, List<object>> _domainEventHandlers = new Dictionary<Type, List<object>>();
        #endregion

        #region Internal Constants
        /// <summary>
        /// 更新聚合根的版本号和清空未提交事件的方法的名称。
        /// </summary>
        internal const string UpdateVersionAndClearUncommittedEventsMethodName = @"UpdateVersionAndClearUncommittedEvents";
        #endregion

        #region Ctor
        /// <summary>
        /// 初始化一个 <c>SourcedAggregateRoot</c> 类型的实例。
        /// </summary>
        /// <param name="id">聚合根对象的唯一标识。</param>
        protected SourcedAggregateRoot(TAggregateRootId id)
        {
            _id = id;
            _version = Constants.ApplicationRuntime.DefaultVersion;
            _eventVersion = _version;
            _branch = Constants.ApplicationRuntime.DefaultBranch;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// 获取给定的聚合根的合法的领域事件处理程序。
        /// </summary>
        /// <param name="domainEvent">对应于该类型的事件的事件处理器将被返回</param>
        /// <returns>事件处理器集合</returns>
        private IEnumerable<object> GetDomainEventHandlers(IDomainEvent<TAggregateRootId> domainEvent)
        {
            var eventType = domainEvent.GetType();
            if (_domainEventHandlers.ContainsKey(eventType))
                return _domainEventHandlers[eventType];
            else
            {
                // firstly create and add all the handler methods defined within the aggregation root.
                MethodInfo[] allMethods = this.GetType().GetMethods(BindingFlags.Public |
                    BindingFlags.NonPublic | BindingFlags.Instance);
                var handlerMethods = from method in allMethods
                                     let returnType = method.ReturnType
                                     let @params = method.GetParameters()
                                     let handlerAttributes = method.GetCustomAttributes(typeof(HandlesAttribute), false)
                                     where returnType == typeof(void) &&
                                     @params != null &&
                                     @params.Any() &&
                                     @params[0].ParameterType == eventType &&
                                     handlerAttributes != null &&
                                     ((HandlesAttribute)handlerAttributes[0]).DomainEventType == eventType
                                     select new { MethodInfo = method };

                var handlers = (from handlerMethod in handlerMethods 
                                let inlineDomainEventHandlerType = typeof (InlineDomainEventHandler<,>).MakeGenericType(eventType) 
                                select Activator.CreateInstance(inlineDomainEventHandlerType, this, handlerMethod.MethodInfo)).ToList();
                // then read all the registered handlers.

                _domainEventHandlers.Add(eventType, handlers);
                return handlers;
            }
        }

        /// <summary>
        /// 在聚合根上处理给定的领域事件。
        /// </summary>
        /// <typeparam name="TEvent">事件对象的.NET类型</typeparam>
        /// <param name="event">The domain event which needs to be handled by aggregate root.</param>
        private void HandleEvent<TEvent>(TEvent @event)
            where TEvent : IDomainEvent<TAggregateRootId>
        {
            var handlers = this.GetDomainEventHandlers(@event);
            foreach (var handler in handlers)
            {
                var handleMethod = handler.GetType().GetMethod("Handle", BindingFlags.Public | BindingFlags.Instance);
                if (handleMethod != null)
                {
                    handleMethod.Invoke(handler, new object[] { @event });
                }
            }
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// 升起一个领域事件。
        /// </summary>
        /// <typeparam name="TEvent">The type of the domain event.</typeparam>
        /// <param name="event">The domain event to be raised.</param>
        protected virtual void RaiseEvent<TEvent>(TEvent @event) 
            where TEvent : Event<TAggregateRootId>, IDomainEvent<TAggregateRootId>
        {
            @event.Id = Guid.NewGuid();
            @event.Version = ++_eventVersion;
            @event.Source = this;
            @event.AssemblyQualifiedEventType = typeof(TEvent).AssemblyQualifiedName;
            @event.Branch = Constants.ApplicationRuntime.DefaultBranch;
            @event.Timestamp = DateTime.UtcNow;
            HandleEvent(@event);
            _uncommittedEvents.Add(@event);
        }
        /// <summary>
        /// 当在子类中重写时，根据给定的快照建造当前聚合根对象。
        /// </summary>
        /// <param name="snapshot">The <see cref="Anycmd.Snapshots.ISnapshot{T}"/> instance
        /// with which the aggregate is built.</param>
        protected abstract void DoBuildFromSnapshot(ISnapshot<TAggregateRootId> snapshot);

        /// <summary>
        /// 当在子类中重写时，基于当前聚合根对象建造快照。
        /// </summary>
        /// <returns>The <see cref="Anycmd.Snapshots.ISnapshot{T}"/> instance that is created based on
        /// the current aggregate.</returns>
        protected abstract ISnapshot<TAggregateRootId> DoCreateSnapshot();

        /// <summary>
        /// 更新聚合根的版本号和清空未提交事件。
        /// </summary>
        /// <remarks>This method is handled by the anycmd framework internally and should not be referenced in any circumstances.</remarks>
        [Obsolete(@"This method is handled by the anycmd framework internally and should not be referenced in any circumstances.", true)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected void UpdateVersionAndClearUncommittedEvents()
        {
            this._version = Version;
            this._uncommittedEvents.Clear();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Returns the hash code for current aggregate root.
        /// </summary>
        /// <returns>The calculated hash code for the current aggregate root.</returns>
        public override int GetHashCode()
        {
            return ReflectionHelper.GetHashCode(this.Id.GetHashCode(),
                this.UncommittedEvents.GetHashCode(),
                this.Version.GetHashCode(),
                this.Branch.GetHashCode());
        }

        /// <summary>
        /// Returns a <see cref="System.Boolean"/> value indicating whether this instance is equal to a specified
        /// object.
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns>True if obj is an instance of the <see cref="ISourcedAggregateRoot{T}"/> type and equals the value of this
        /// instance; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;
            if (obj == null)
                return false;
            var other = obj as SourcedAggregateRoot<TAggregateRootId>;
            if ((object)other == (object)null)
                return false;
            return other.Id.ToString() == this.Id.ToString();
        }
        #endregion

        #region ISourcedAggregateRoot Members
        /// <summary>
        /// 从历史事件中建造当前聚合根对象。
        /// </summary>
        /// <param name="historicalEvents">The historical events from which the aggregate is built.</param>
        public virtual void BuildFromHistory(IEnumerable<IDomainEvent<TAggregateRootId>> historicalEvents)
        {
            if (_uncommittedEvents.Any())
                _uncommittedEvents.Clear();
            var domainEvents = historicalEvents as IDomainEvent<TAggregateRootId>[] ?? historicalEvents.ToArray();
            foreach (var de in domainEvents)
                HandleEvent(de);
            _version = domainEvents[domainEvents.Length - 1].Version;
            _eventVersion = _version;
        }

        /// <summary>
        /// 读取所有未提交的事件。
        /// </summary>
        public virtual IEnumerable<IDomainEvent<TAggregateRootId>> UncommittedEvents
        {
            get { return _uncommittedEvents; }
        }

        /// <summary>
        /// 读取聚合根的版本号。
        /// </summary>
        public virtual long Version
        {
            get { return _version + _uncommittedEvents.Count; }
        }

        /// <summary>
        /// 读取当前聚合根所在的分支。
        /// </summary>
        public virtual long Branch
        {
            get { return _branch; }
        }
        #endregion

        #region IEntity Members
        /// <summary>
        /// 读取或设置当前聚合根的标识。
        /// </summary>
        public virtual TAggregateRootId Id
        {
            get { return _id; }
            set { _id = value; }
        }

        #endregion

        #region IOrignator Members
        /// <summary>
        /// 从给定的快照中建造当前聚合根对象。
        /// </summary>
        /// <param name="snapshot">The <see cref="Anycmd.Snapshots.ISnapshot{T}"/> instance
        /// with which the aggregate is built.</param>
        public virtual void BuildFromSnapshot(ISnapshot<TAggregateRootId> snapshot)
        {
            _branch = snapshot.Branch;
            _version = snapshot.Version;
            _id = snapshot.AggregateRootId;
            DoBuildFromSnapshot(snapshot);
            _uncommittedEvents.Clear();
        }

        /// <summary>
        /// 基于当前聚合根对象创建快照。
        /// </summary>
        /// <returns>The <see cref="Anycmd.Snapshots.ISnapshot{T}"/> instance that is created based on
        /// the current aggregate.</returns>
        public virtual ISnapshot<TAggregateRootId> CreateSnapshot()
        {
            var snapshot = DoCreateSnapshot();
            snapshot.Branch = Branch;
            snapshot.Version = Version;
            snapshot.Timestamp = DateTime.UtcNow;
            snapshot.AggregateRootId = _id;

            return snapshot;
        }
        #endregion
    }
}
