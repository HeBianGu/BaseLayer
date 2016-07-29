using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.CaculateEngine
{
    /// <summary> 集合中包含的实体单元 只比较到天 </summary>
    public class DayTimeModelEntity<T>:IComparable
    {
        DateTime time;
        /// <summary> 时间 </summary>
        public DateTime Time
        {
            get { return time; }
            set { time = value; }
        }

        T value;
        /// <summary> 值 </summary>
        public T Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public int CompareTo(object obj)
        {
            DayTimeModelEntity<T> temp = obj as DayTimeModelEntity<T>;

            if(this.time.Date>temp.time)
            {
                return 1;
            }
            else if(this.time.Date<temp.time)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }

    /// <summary> 集合中包含的实体单元 </summary>
    public class IntModelEntity<T> : IComparable
    {
        int _index;
        /// <summary> 序号 </summary>
        public int Index
        {
            get { return _index; }
            set { _index = value; }
        }

        T value;
        /// <summary> 值 </summary>
        public T Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public int CompareTo(object obj)
        {
            IntModelEntity<T> temp = obj as IntModelEntity<T>;

            return this._index.CompareTo(temp._index);
        }
    }
}
