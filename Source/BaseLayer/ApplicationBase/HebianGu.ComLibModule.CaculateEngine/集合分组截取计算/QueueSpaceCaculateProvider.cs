#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/11/2 18:47:29  计算机名称：DEV-LIHAIJUN
 *
 * 文件名：QueueTimeSpaceCaculatePrvider
 *
 * 说明：
 * 
 * 通过开始时间和结束时间以及时间间隔确定连续的时间段
 * 通过这个时间段处理指定传入的集合
 * 从集合开始查找如果时间小于时间段开始时间 = 屏蔽生成
 * 如果时间段中某个时间没有 = 生成一个默认数据
 * 当到达时间间隔的时间点时 = 触发注册事件 对集合处理

 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.CaculateEngine
{
    /// <summary> 提供DateTime比较 Queue队列的指定阀值触发的引擎 T表示载体 也就是要计算的类型 </summary>
    public class QueueSpaceCaculateProvider<T>
    {

        T defautValue = default(T);

        /// <summary> 如果指定时间没有设置默认值  </summary>
        public T DefautValue
        {
            get { return defautValue; }
            set { defautValue = value; }
        }

        Action<List<DayTimeModelEntity<T>>> _thresholdEvent;
        /// <summary> 携带集合的事件 </summary>
        public Action<List<DayTimeModelEntity<T>>> ThresholdEvent
        {
            get { return _thresholdEvent; }
            set { _thresholdEvent = value; }
        }

        /// <summary> 主要计算方法 </summary>
        /// <param name="times"> 要处理的集合 </param>
        /// <param name="start"> 起始时间 </param>
        /// <param name="end"> 结束时间 </param>
        /// <param name="space"> 时间间隔或阀值 </param>
        public void CaculateTimeValue(Queue<DayTimeModelEntity<T>> times, DateTime start, DateTime end, int space)
        {
            DateTime tempTime = start;

            CaculateGroupEngine<DayTimeModelEntity<T>> engine = new CaculateGroupEngine<DayTimeModelEntity<T>>(space);

            engine.ThresholdEvent = _thresholdEvent;

            while (tempTime < end)
            {

                DayTimeModelEntity<T> outTime = GetListEntity(times, tempTime);

                if (outTime == null) break;

                List<DayTimeModelEntity<T>> entityList = new List<DayTimeModelEntity<T>>();

                GetAppEntity(outTime, ref tempTime, ref entityList);

                foreach (var v in entityList)
                {
                    engine.AddItem(v);
                }
            }
        }

        /// <summary> 递归获取的时间比比较时间小则过滤掉 </summary>
       DayTimeModelEntity<T> GetListEntity(Queue<DayTimeModelEntity<T>> times, DateTime time)
        {
            if (times.Count == 0) return null;

            DayTimeModelEntity<T> outTime = times.Dequeue();

            if (outTime.CompareTo(time)<0)
            {
                return GetListEntity(times, time);
            }
            else
            {
                return outTime;
            }
        }

        /// <summary> 递归批量增加间断的时间 </summary>
       void GetAppEntity(DayTimeModelEntity<T> outTime, ref DateTime tempTime, ref List<DayTimeModelEntity<T>> entityList)
        {

            if (outTime.CompareTo(tempTime)==0)
            {
                entityList.Add(outTime);

                tempTime = tempTime.AddDays(1);
            }
            else
            {
                DayTimeModelEntity<T> entity = new DayTimeModelEntity<T>();
                entity.Time = tempTime;
                entity.Value = defautValue;
                entityList.Add(entity);

                tempTime = tempTime.AddDays(1);

                GetAppEntity(outTime, ref tempTime, ref entityList);
            }
        }
    }


}
