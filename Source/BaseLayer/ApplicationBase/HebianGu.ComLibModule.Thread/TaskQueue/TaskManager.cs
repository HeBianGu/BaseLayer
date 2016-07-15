#region <�� �� ע ��>
/*
 * ========================================================================
 * Copyright(c) �����²���ʯ�ͿƼ����޹�˾, All Rights Reserved.
 * ========================================================================
 *    
 * ���ߣ�[���]   ʱ�䣺2015/11/4 10:49:13  ��������ƣ�DEV-LIHAIJUN
 *
 * �ļ�����TaskManager
 *
 * ˵��������һ���̼߳���������� ����IWorkInterface�ӿ�RunWork()����
 * 
 * 
 * �޸��ߣ�           ʱ�䣺               
 * �޸�˵����
 * ========================================================================
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.ThreadEx.TaskQueue
{
    /// <summary> ����һ���̼߳���������� ����IWorkInterface�ӿ�RunWork()���� </summary>
    public class TaskManager<T> where T : IWorkInterface
    {

        /// <summary> ������� </summary>
        private static Queue<T> m_List;

        /// <summary> �̻߳��� </summary>
        private static object m_obj = new object();

        /// <summary> ��ʼ������ </summary>
        public TaskManager()
        {
            if (m_List == null)
                m_List = new Queue<T>();
        }

        /// <summary> ����ʵʱ��������ִ�� </summary>
        public void ThreadWork()
        {
            while (true)
            {
                //  ��ȡ����
                T work = Pop();

                //  ִ������
               if(!work.RunWork())
               {
                   //  д������־
               }

                Thread.Sleep(1);
            }
        }

        /// <summary> �����������ȡ������ </summary>
        public T Pop()
        {

            Monitor.Enter(m_obj);

            T ac = default(T);
            try
            {
                //�����������ݣ�����.����ȴ�
                if (m_List.Count > 0)
                {
                    ac = m_List.Dequeue();
                }
                else
                {
                    Monitor.Wait(m_obj);
                    ac = m_List.Dequeue();
                }
            }
            finally
            {
                Monitor.Exit(m_obj);
            }
            return ac;
        }

        /// <summary> ���������������� </summary>
        public void Push(T work)
        {
            //  ����
            Monitor.Enter(m_obj);

            //  ��������������
            m_List.Enqueue(work);

            //  ֪ͨ�ȴ������е��߳���������״̬�ĸ��ġ�
            Monitor.Pulse(m_obj);

            //  �ͷ���
            Monitor.Exit(m_obj);
        }

        /// <summary> �����߳��첽ִ�м�� </summary>
        public void Start()
        {
            Thread th = new Thread(new ThreadStart(this.ThreadWork));
            th.Start();
        }
    }
}