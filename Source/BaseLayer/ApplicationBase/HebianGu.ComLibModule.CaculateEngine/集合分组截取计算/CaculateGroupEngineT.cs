using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.CaculateEngine
{
    /// <summary> 封装一个阀值集合，当超过阀值触发集合事件 </summary>
    public class CaculateGroupEngine<T>
    {
        /// <summary> 传递阀值 </summary>
        public CaculateGroupEngine(int thresholdCout)
        {
            _thresholdCount = thresholdCout;
        }

        List<T> _groupTime = new List<T>();

        //  阀值
        int _thresholdCount;

        Action<List<T>> _thresholdEvent;
        /// <summary> 携带集合的事件 </summary>
        public Action<List<T>> ThresholdEvent
        {
            get { return _thresholdEvent; }
            set { _thresholdEvent = value; }
        }


        /// <summary> 添加项 如果超过阀值触发事件 并清空 </summary>
        public void AddItem(T item)
        {
            _groupTime.Add(item);


            if (_groupTime.Count == _thresholdCount)
            {
                if (_thresholdEvent != null)
                {
                    //  触发事件
                    _thresholdEvent(_groupTime);
                }

                //  清理数据
                this._groupTime.Clear();
            }
        }
    }
}
