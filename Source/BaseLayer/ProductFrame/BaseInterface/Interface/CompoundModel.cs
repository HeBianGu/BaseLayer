using System;
using System.Collections;
using System.Collections.Generic;

namespace OPT.Product.BaseInterface
{
    /// <summary>
    /// 基础底层事件参数的基本类型，
    /// 所有基础底层的事件必须使用此类，或者从此类派生
    /// </summary>
    public class BxEventArgs
    {
        #region members
        object target;
        object trigger;
        Int32 eventID;
        object _result;
        Int32 _exitCode = -1;
        object _param = null;
        #endregion

        #region properties
        /// <summary>
        /// 所发生事件的ID
        /// </summary>
        public Int32 EventID { get { return eventID; } set { eventID = value; } }
        /// <summary>
        /// 所发生事件的承载者，即谁身上发生了事件
        /// </summary>
        public object Target { get { return target; } set { target = value; } }
        /// <summary>
        /// 所发生事件的触发者，即谁改变了目标
        /// </summary>
        public object Trigger { get { return trigger; } set { trigger = value; } }
        public object Param { get { return _param; } set { _param = value; } }
        /// <summary>
        /// 回传的值
        /// 如果不需要回传值,忽略本参数
        /// </summary>
        public object Result { get { return _result; } set { _result = value; } }
        public Int32 ExitCode { get { return _exitCode; } set { _exitCode = value; } }
        #endregion

        public BxEventArgs()
        {
            eventID = -1;
            target = null;
            trigger = null;
            _result = null;
        }

        public BxEventArgs(BxEventArgs e)
        {
            eventID = e.eventID;
            target = e.target;
            trigger = e.trigger;
            _result = e._result;
            _exitCode = e._exitCode;
            _param = e._param;
        }

        public BxEventArgs(Int32 eventID)
        {
            this.eventID = eventID;
            target = null;
            trigger = null;
            _result = null;
        }

        public BxEventArgs(Int32 eventID, object param)
        {
            this.eventID = eventID;
            target = null;
            trigger = null;
            _result = null;
            _param = param;
        }

        public BxEventArgs(Int32 eventID, object target, object trigger)
        {
            this.eventID = eventID;
            this.target = target;
            this.trigger = trigger;
            _result = null;
        }
    }

    public delegate void BxEventHandler(BxEventArgs e);

    public interface IBxElementEvent
    {
        event BxEventHandler ElementEvent;
        void FireEvent(BxEventArgs e);
        void BindEvent(int id, BxEventHandler handler);
        void RemoveEventBinding(int id, BxEventHandler handler);
    }

    public interface IBxModifyInfo
    {
        bool Modified { get; }
        void ResetModifyFlag();
    }

    public interface IBxElementSite : IBxElementEvent
    {
        IBxCompound Container { get; }
        IBxElement Element { get; }

        IBxUIConfig UIConfig { get; }
    }

    public interface IBxElement : IBxElementEvent
    {
        IBxElementSite[] ParentSites { get; }
    }

    public interface IBxCompound : IBxElement
    {
        IEnumerable<IBxElementSite> ChildSites { get; }
        //IBxElementSite ChildByIndex(int index);
        //IBxElementSite ChildByID(int configID);
        //int IndexOf(IBxElementSite child);
    }

    public static class BxCompoundExtendMethod
    {
        public static IBxElementSite ChildByIndex(this IBxCompound cmpd, int index)
        {
            int i = 0;
            foreach (IBxElementSite one in cmpd.ChildSites)
            {
                if (index == i)
                    return one;
                index++;
            }
            return null;
        }
        public static IBxElementSite ChildByID(this IBxCompound cmpd, string fullID)
        {
            foreach (IBxElementSite one in cmpd.ChildSites)
            {
                if ((one.UIConfig != null) && (one.UIConfig.FullID == fullID))
                    return one;
            }
            return null;
        }
        public static int IndexOf(this IBxCompound cmpd, IBxElementSite child)
        {
            int index = 0;
            foreach (IBxElementSite one in cmpd.ChildSites)
            {
                if (one == child)
                    return index;
                index++;
            }
            return -1;
        }
    }

    public interface IBxContainer : IBxCompound
    {
        Int32 Count { get; }
        IBxElementSite GetAt(Int32 index);
        void Append();
        void AppendRange(Int32 size);
        void Insert(Int32 index);
        void InsertRange(Int32 index, Int32 size);
        void Remove(Int32 index);
        void RemoveRange(Int32 index, Int32 size);
        void RemoveAll();
    }

    public interface IBxElementTableSite : IBxElementSite
    {
        //Int32 ColumnCount { get; }
        //IBxUIConfig ColumnConfig(Int32 index);
        //IBxUIConfig[] ColumnConfigs { get; }
        //Int32 CenterColumn { get; }
        IBxSubColumns SubColumns { get; }
        IBxElementSite GetCell(Int32 row, Int32 col);
        IBxContainer ElementEx { get; }
    }


    public interface IBxUIValue
    {
        string GetUIValue();
        bool SetUIValue(string val);
    }

    public interface IBxUIValueEx : IBxUIValue
    {
        string GetUIValue(int? decimalDigits);
        bool IsLegalValue(string val);
    }

    public interface IBxUIUnitValue : IBxUIValue
    {
        IBxUnit UIUnit { get; set; }
        string GetUIValue(IBxUnit unit);
        string GetUIValue(IBxUnit trgUnit, IBxUnit decimalDigitsUnit, int decimalDigits);
        bool SetUIValue(string val, IBxUnit unit);
    }






    public enum BLElementSiteMode
    {
        ValueOwner = 1,
        ValueReferer = 2
    }
    public interface IBLElementSite
    {
        BLElementSiteMode SiteMode { get; }
        object ReferTo { get; set; }
    }

    public interface IBLElementValue
    {
        IBLElementSite ReferFrom { get; set; }
    }
}
