#region <�� �� ע ��>
/*
 * ========================================================================
 * Copyright(c) �����²���ʯ�ͿƼ����޹�˾, All Rights Reserved.
 * ========================================================================
 *    
 * ���ߣ�[���]   ʱ�䣺2015/11/4 9:19:10  ��������ƣ�DEV-LIHAIJUN
 *
 * �ļ�����Task
 *
 * ˵����
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

namespace HebianGu.ComLibModule.TaskEx
{
    class TaskEx
    {

        /// <summary> �������� </summary>
        public void CreateTask()
        {
            //ʹ��TaskFactory����һ������
            TaskFactory tf = new TaskFactory();
            Task t1 = tf.StartNew(NewTask);
            //ʹ��Task��de Factory����һ������
            Task t2 = Task.Factory.StartNew(NewTask);
            ///////////////////////////////////////
            Task t3 = new Task(NewTask);
            t3.Start();
            Task t4 = new Task(NewTask, TaskCreationOptions.PreferFairness);
            t4.Start();
            Thread.Sleep(1000);//��Ϊ�����Ǻ�̨�̣߳��������������������߳�һ�������ȴ�����ȫ��ִ�����
        }

        void NewTask()
        {
            Console.WriteLine("��ʼһ������");
            Console.WriteLine("Task id:{0}", Task.CurrentId);
            Console.WriteLine("����ִ�����");
        }


        /// <summary> �������������� </summary>
         void CreateContinueTask()
        {
            Task t1 = new Task(FirstTask);
            Task t2 = t1.ContinueWith(SecondTask);
            t1.Start();
            Thread.Sleep(7000);
        }
        void FirstTask()
        {
            Console.WriteLine("��һ������ʼ��TaskID:{0}", Task.CurrentId);
            Thread.Sleep(3000);
        }
         void SecondTask(Task task)
        {
            Console.WriteLine("����{0}���", task.Id);
            Console.WriteLine("�ڶ�������ʼ��TaskID:{0}", Task.CurrentId);
            Console.WriteLine("������......");
            Thread.Sleep(3000);
        }

    }


}