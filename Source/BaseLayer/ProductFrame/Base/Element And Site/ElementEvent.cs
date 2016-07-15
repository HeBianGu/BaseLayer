using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using OPT.Product.BaseInterface;

namespace OPT.Product.Base
{

    public class BxEventItem
    {
        public int id = -1;
        public BxEventHandler _eventHandler = null;
    }

    public class BxElementEvent : IBxElementEvent
    {

        #region IBxElementEvent 成员
        public event BxEventHandler ElementEvent;
        List<BxEventItem> _eventItems = null;
        #endregion

        public void BindEvent(int id, BxEventHandler handler)
        {
            if (id == 0)
            {
                ElementEvent += handler;
                return;
            }

            BxEventItem item = null;
            if (_eventItems != null)
            {
                item = _eventItems.Find(x => x.id == id);
            }
            else
            {
                _eventItems = new List<BxEventItem>();
            }

            if (item == null)
            {
                item = new BxEventItem();
                item.id = id;
                _eventItems.Add(item);
            }
            item._eventHandler += handler;
        }
        public void RemoveEventBinding(int id, BxEventHandler handler)
        {
            if (id == 0)
            {
                ElementEvent -= handler;
                return;
            }
            if (_eventItems != null)
            {
                int index = _eventItems.FindIndex(x => x.id == id);
                if (index > -1)
                {
                    _eventItems[index]._eventHandler -= handler;
                    if (_eventItems[index]._eventHandler == null)
                        _eventItems.RemoveAt(index);
                }
            }
        }

        public void FireEvent(int id, BxEventArgs e)
        {
            if (_eventItems != null)
            {
                BxEventItem item = _eventItems.Find(x => x.id == id);
                if ((item != null) && (item._eventHandler != null))
                    item._eventHandler(e);
            }
            if (ElementEvent != null)
                ElementEvent(e);
        }

        public void FireEvent(BxEventArgs e)
        {
            FireEvent(e.EventID, e);
        }

        public BxEventArgs FireEvent(Int32 eventID, object trigger)
        {
            if (ElementEvent == null)
                return null;

            BxEventArgs e = new BxEventArgs(eventID, this, trigger);
            ElementEvent(e);
            return e;
        }

        public BxEventArgs FireEvent(Int32 eventID, object target, object trigger)
        {
            if (ElementEvent == null)
                return null;

            BxEventArgs e = new BxEventArgs(eventID, target, trigger);
            ElementEvent.Invoke(e);
            return e;
        }
    }

    public class BxEventID
    {
        public const Int32 valueChanged = 1;

        public const Int32 StructChanged = 100;
        public const Int32 ArraySizeChanged = 101;
        public const Int32 ArrayAdded = 102;
        public const Int32 ArrayRemoved = 103;
        public const Int32 ArrayInserted = 104;

        public const Int32 siteConfigChanged = 200;
        public const Int32 siteShowChanged = 201;
        public const Int32 siteShowTitleChanged = 202;
        public const Int32 siteUserHideChanged = 203;

        public const Int32 customEventBegin = 1000;

    }

}


