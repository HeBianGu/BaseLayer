
namespace Anycmd.Repositories
{
    using Model;
    using System;
    using System.Linq;

    /// <summary>
    /// 表示该接口的实现类是仓储事务上下文。
    /// </summary>
    public interface IRepositoryContext : IUnitOfWork, IDisposable
    {
        /// <summary>
        /// 获取当前仓储上下文对象的唯一标识。
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// 往仓储上下文中注册一个新对象。
        /// </summary>
        /// <param name="obj">The object to be registered.</param>
        void RegisterNew(object obj);

        /// <summary>
        /// 向仓储上下文中注册一个被修改的对象。
        /// </summary>
        /// <param name="obj">The object to be registered.</param>
        void RegisterModified(object obj);

        /// <summary>
        /// 向仓储上下文中注册一个被删除的东西。
        /// </summary>
        /// <param name="obj">The object to be registered.</param>
        void RegisterDeleted(object obj);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        IQueryable<TEntity> Query<TEntity>() where TEntity : class;
    }
}
