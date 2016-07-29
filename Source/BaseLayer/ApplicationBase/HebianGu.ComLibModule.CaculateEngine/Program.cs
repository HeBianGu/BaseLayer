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

            TestProvider1();

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
