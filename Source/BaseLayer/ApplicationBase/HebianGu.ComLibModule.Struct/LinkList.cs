using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.Struct
{
    /// <summary> 链表节点类 </summary>
    public class ListNode
    {
        public ListNode(int NewValue)
        {
            Value = NewValue;
        }

        private object obj;
        /// <summary>
        /// 携带
        /// </summary>
        public object Obj
        {
            get { return obj; }
            set { obj = value; }
        }

        /// <summary>
        /// 前一个节点
        /// </summary>

        public ListNode Previous;

        /// <summary>
        /// 后一个节点
        /// </summary>

        public ListNode Next;

        /// <summary>
        /// 值
        /// </summary>

        public int Value;
    }

    /// <summary> 有序链表 </summary>
    public class Creastlist
    {
        public Creastlist()
        {
            //构造函数
            //初始化
            ListCountValue = 0;
            Head = null;
            Tail = null;
        }

        /// <summary>
        /// 构造一个初始化好的链表
        /// </summary>
        /// <param name="str">初始化的字符串，并以逗号或者空格隔开</param>
        public Creastlist(string str)
        {
            //构造函数
            //初始化
            char[] separator = { ',', ' ' };
            string[] s = str.Split(separator);
            Creastlist list = new Creastlist();
            foreach (string i in s)
            {
                int j = Convert.ToInt32(i);
                list.Append(j);
            }
            ListCountValue = s.Length;
            Tail = list.Current;
            Head = list.Head;
        }

        /// <summary>
        /// 头指针
        /// </summary>
        private ListNode Head;

        /// <summary>
        ///  尾指针
        /// </summary>
        private ListNode Tail;

        /// <summary>
        /// 当前指针
        /// </summary>
        private ListNode Current;

        /// <summary>
        /// 链表数据的个数
        /// </summary>
        private int ListCountValue;

        /// <summary>
        /// 尾部添加数据 
        /// </summary>
        /// <param name="DataValue">值</param>
        public void Append(int DataValue)
        {
            ListNode NewNode = new ListNode(DataValue);

            if (IsNull())

            //如果头指针为空
            {
                Head = NewNode;
                Tail = NewNode;
            }
            else
            {
                Tail.Next = NewNode;
                NewNode.Previous = Tail;
                Tail = NewNode;
            }
            Current = NewNode;
            //链表数据个数加一
            ListCountValue += 1;
        }
        /// <summary>
        /// 尾部添加数据 
        /// </summary>
        /// <param name="DataValue">值</param>
        public void Append(ListNode NewNode)
        {
            if (IsNull())

            //如果头指针为空
            {
                Head = NewNode;
                Tail = NewNode;
            }
            else
            {
                Tail.Next = NewNode;
                NewNode.Previous = Tail;
                Tail = NewNode;
            }
            Current = NewNode;
            //链表数据个数加一
            ListCountValue += 1;
        }
        /// <summary>
        /// 删除当前的数据
        /// </summary>
        public void Delete()
        {
            //若为空链表
            if (!IsNull())
            {
                //若删除头
                if (IsBof())
                {
                    Head = Current.Next;
                    Current = Head;
                    ListCountValue -= 1;
                    return;
                }

                //若删除尾
                if (IsEof())
                {
                    Tail = Current.Previous;
                    Current = Tail;
                    ListCountValue -= 1;
                    return;
                }

                //若删除中间数据
                Current.Previous.Next = Current.Next;
                Current = Current.Previous;
                ListCountValue -= 1;
                return;
            }
        }

        /// <summary>
        /// 向后移动一个数据
        /// </summary>
        public void MoveNext()
        {
            if (!IsEof())
            {
                Current = Current.Next;
            }
            else
            {
                Current = Head;
            }
        }

        /// <summary>
        /// 向前移动一个数据 
        /// </summary>
        public void MovePrevious()
        {
            if (!IsBof()) Current = Current.Previous;
        }

        /// <summary>
        /// 移动到第一个数据
        /// </summary>
        public void MoveFrist()
        {
            Current = Head;
        }


        /// <summary>
        ///  移动到最后一个数据
        /// </summary>
        public void MoveLast()
        {
            Current = Tail;
        }

        /// <summary>
        /// 判断是否为空链表
        /// </summary>
        /// <returns>返回bool值</returns>
        public bool IsNull()
        {
            if (ListCountValue == 0)
                return true;

            return false;
        }

        /// <summary>
        /// 判断是否为到达尾 
        /// </summary>
        /// <returns></returns>
        public bool IsEof()
        {
            if (Current == Tail)
                return true;
            return false;
        }

        /// <summary>
        /// 判断是否为到达头部
        /// </summary>
        /// <returns></returns>
        public bool IsBof()
        {
            if (Current == Head)
                return true;

            return false;
        }

        /// <summary>
        /// 获取当前数据
        /// </summary>
        /// <returns>返回当前节点的值</returns>
        public int GetCurrentValue()
        {
            return Current.Value;
        }


        /// <summary>
        /// 获取当前数据
        /// </summary>
        /// <returns>返回当前节点的值</returns>
        public object GetCurrentObj()
        {
            return Current.Obj;
        }

        /// <summary>
        ///  取得链表的数据个数
        /// </summary>
        public int ListCount
        {
            get
            {
                return ListCountValue;
            }
        }


        /// <summary>
        /// 清空链表
        /// </summary>
        public void Clear()
        {
            MoveFrist();
            while (!IsNull())
            {
                //若不为空链表,从尾部删除  
                Delete();
            }
        }


        /// <summary>
        ///  在当前位置前插入数据
        /// </summary>
        /// <param name="DataValue">要插入的数据</param>
        public void Insert(int DataValue)
        {
            ListNode NewNode = new ListNode(DataValue);
            if (IsNull())
            {
                //为空表，则添加
                Append(DataValue);
                return;
            }

            if (IsBof())
            {
                //为头部插入
                NewNode.Next = Head;
                Head.Previous = NewNode;
                Head = NewNode;
                Current = Head;
                ListCountValue += 1;
                return;
            }
            //中间插入
            NewNode.Next = Current;
            NewNode.Previous = Current.Previous;
            Current.Previous.Next = NewNode;
            Current.Previous = NewNode;
            Current = NewNode;
            ListCountValue += 1;
        }

        /// <summary>
        /// 进行升序插入
        /// </summary>
        /// <param name="InsertValue"></param>

        public void InsertAscending(int InsertValue)
        {
            //参数：InsertValue 插入的数据
            //为空链表
            if (IsNull())
            {
                //添加
                Append(InsertValue);
                return;
            }

            //移动到头
            MoveFrist();
            if ((InsertValue < GetCurrentValue()))
            {
                //满足条件，则插入，退出
                Insert(InsertValue);
                return;
            }
            while (true)
            {
                if (InsertValue < GetCurrentValue())
                {
                    //满族条件，则插入，退出
                    Insert(InsertValue);
                    break;
                }
                if (IsEof())
                {
                    //尾部添加
                    Append(InsertValue);
                    break;
                }
                //移动到下一个指针
                MoveNext();
            }
        }

        //进行降序插入
        public void InsertUnAscending(int InsertValue)
        {
            //参数：InsertValue 插入的数据                      
            //为空链表
            if (IsNull())
            {
                //添加
                Append(InsertValue);
                return;
            }
            //移动到头
            MoveFrist();
            if (InsertValue > GetCurrentValue())
            {
                //满足条件，则插入，退出
                Insert(InsertValue);
                return;
            }
            while (true)
            {
                if (InsertValue > GetCurrentValue())
                {
                    //满族条件，则插入，退出
                    Insert(InsertValue);
                    break;
                }
                if (IsEof())
                {
                    //尾部添加
                    Append(InsertValue);
                    break;
                }
                //移动到下一个指针
                MoveNext();
            }
        }

        /// <summary>
        /// 打印链表中全部的数
        /// </summary>
        /// <param name="list">链表参数</param>
        public void ShowList(Creastlist list)
        {
            Console.WriteLine("当前链表中数据为：");
            list.MoveFrist();
            for (int i = 0; i < list.ListCount; i++)
            {
                Console.Write(list.GetCurrentValue() + "  ");
                list.MoveNext();
            }
        }

    }
}
