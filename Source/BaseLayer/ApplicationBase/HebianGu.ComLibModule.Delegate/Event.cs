#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/10/30 9:25:11  计算机名称：DEV-LIHAIJUN
 *
 * 文件名：Event
 *
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HebianGu.ComLibModule.DelegateEx
{
    public static class EventEx
    {

        /// <summary> 清除所有注册事件 ( 验证可用 ) </summary>
        /// <param name="objectHasEvents"> 是谁的事件 </param>
        /// <param name="eventName"> 事件名 (反射) </param>
        public static void ClearAllEvents(this object objectHasEvents, string eventName)
        {
            if (objectHasEvents == null)
            {
                return;
            }

            try
            {
                //  获取成员所有事件
                EventInfo[] events = objectHasEvents.GetType().GetEvents(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                if (events == null || events.Length < 1)
                {
                    return;
                }

                for (int i = 0; i < events.Length; i++)
                {
                    EventInfo ei = events[i];

                    if (ei.Name == eventName)
                    {
                        //  将事件转换成字段
                        FieldInfo fi = ei.DeclaringType.GetField(eventName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                        if (fi != null)
                        {
                            //  清空事件字段
                            fi.SetValue(objectHasEvents, null);
                        }

                        break;
                    }
                }
            }
            catch
            {
            }
        }


        /// <summary> 清理所有注册信息 </summary>
        /// <param name="pEventInfo"> 要清理的事件 </param>
        /// <param name="objectHasEvents"> 是谁的事件 </param>
        [Obsolete("该方法未测试", true)]
        public static bool ClearAll(this EventInfo pEventInfo, object objectHasEvents)
        {
            if (pEventInfo == null)
            {
                return true;
            }

            try
            {
                FieldInfo fi = pEventInfo.DeclaringType.GetField(pEventInfo.Name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                if (fi != null)
                {
                    fi.SetValue(objectHasEvents, null);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }


        /// <summary> ClearEvent(button1,"Click") 就会清除button1对象的Click事件的所有挂接事件。 </summary>
        [Obsolete("该方法未测试", true)]
        public static void ClearEvent(this Control control, string eventname)
        {
            if (control == null) return;
            if (string.IsNullOrEmpty(eventname)) return;

            BindingFlags mPropertyFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic;
            BindingFlags mFieldFlags = BindingFlags.Static | BindingFlags.NonPublic;
            Type controlType = typeof(System.Windows.Forms.Control);
            PropertyInfo propertyInfo = controlType.GetProperty("Events", mPropertyFlags);
            EventHandlerList eventHandlerList = (EventHandlerList)propertyInfo.GetValue(control, null);
            FieldInfo fieldInfo = (typeof(Control)).GetField("Event" + eventname, mFieldFlags);
            Delegate d = eventHandlerList[fieldInfo.GetValue(control)];

            if (d == null) return;
            EventInfo eventInfo = controlType.GetEvent(eventname);

            foreach (Delegate dx in d.GetInvocationList())
                eventInfo.RemoveEventHandler(control, dx);

        }

        /// <summary> 根据线程选择是否异步执行 </summary>
        public static void DoThread<T>(this EventHandler handle, EventArgs args)
        {
            if (handle == null) return;

            if (handle.Target is System.ComponentModel.ISynchronizeInvoke)
            {
                System.ComponentModel.ISynchronizeInvoke aSynch = handle.Target as System.ComponentModel.ISynchronizeInvoke;

                if (aSynch.InvokeRequired)
                {
                    object[] a = new object[] { handle, args };
                    // Todo ：检查是否异步调用 
                    aSynch.BeginInvoke(handle, a);
                }
                else
                {
                    // Todo ：检查同步调用 
                    handle(handle, args);
                }
            }

        }


    }
}