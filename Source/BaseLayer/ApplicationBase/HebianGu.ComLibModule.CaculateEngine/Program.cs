using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.CaculateEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            Action<int> act1 = l =>
            {
                Console.WriteLine("第1次");
            };

            Action<Action<int>> act2 = l =>
            {
                Console.WriteLine("第2次");
                l(1);
            };

            Action<Action<Action<int>>> act3 = l =>
            {
                Console.WriteLine("第3次");
                l(act1);
            };
            Action<Action<Action<Action<int>>>> act4 = l =>
            {
                Console.WriteLine("第4次");
                l(act2);
            };

           act4(act3);



            Console.Read();
            //TestSplitEngine();

            //TestProvider1();

        }
        static Queue<DayTimeModelEntity<double>> CreateTest()
        {

            Queue<DayTimeModelEntity<double>> que = new Queue<DayTimeModelEntity<double>>();
            for (int i = 0; i < 100; i++)
            {
                DayTimeModelEntity<double> entity = new DayTimeModelEntity<double>();
                entity.Time = DateTime.Now.AddDays(i - 1);
                entity.Value = i;

                que.Enqueue(entity);
            }

            return que;
        }

        static void TestSplitEngine()
        {
            string[] ss = { "1", "2", "3", "", "", "4","", "", "5", "6", "7", "", "", "", "" };


            SplitDataEngine engine = new SplitDataEngine();

            var result = engine.SpiltSpace<string>(ss.ToList(), l => l, l => string.IsNullOrEmpty(l));

            result.ForEach(l =>
                {

                    // Todo ：输出索引 
                    Console.WriteLine("表格：" + result.FindIndex(k => k == l));

                    // Todo ：输出数据信息 
                    l.ForEach(k => Console.WriteLine(k));
                });

            Console.Read();
        }


        static void TestProvider1()
        {
            Queue<DayTimeModelEntity<double>> steps = CreateTest();

            DateTime start = DateTime.Now;

            DateTime end = start.AddDays(100);

            int space = 5;


            QueueSpaceCaculateProvider<double> provider = new QueueSpaceCaculateProvider<double>();

            provider.DefautValue = 0;

            provider.ThresholdEvent = l =>
            {
                //  计算平均值
                DayTimeModelEntity<double> entity = new DayTimeModelEntity<double>();

                entity.Time = l.Max(k => k.Time);

                entity.Value = l.Sum(k => k.Value) / l.Count;

                //  添加到集合中
                string format = "时间：{0}  平均值：{1}";

                Console.WriteLine(string.Format(format, entity.Time.ToString("yyyy-MM-dd"), entity.Value.ToString()));

            };

            provider.CaculateTimeValue(steps, start, end, space);


            Console.Read();
        }
    }
}
