using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.MMF
{
    class Program
    {
        static void Main(string[] args)
        {
            long offset = 0x10000000; // 256 megabytes
            long length = 0x20000000; // 512 megabytes

            long test = 800 * 1024;// 800 * 1024 * 1024;// 800MB

            DateTime time = DateTime.Now;
            Console.WriteLine("开始创建文件！");

            using (MmfEntity<int> mmf = new MmfEntity<int>(@"E:\ExtremelyLargeImage.data", 100))
            {

                for (int i = 0; i < 100; i++)
                {
                    mmf.SetIndex(i, i);
                }

                for (int i = 0; i < 100; i++)
                {
                    Console.WriteLine("执行：" + i + "  值:" + mmf.GetIndex(i));
                }
            }
            /*
            // Create the memory-mapped file.
            using (var mmf = MemoryMappedFile.CreateFromFile(@"E:\ExtremelyLargeImage.data", FileMode.OpenOrCreate, "ImgA", test, MemoryMappedFileAccess.ReadWriteExecute))
            {
                Console.WriteLine("创建文件成功！");
                // Create a random access view, from the 256th megabyte (the offset)
                // to the 768th megabyte (the offset plus length).


                using (var accessor = mmf.CreateViewAccessor(0, test))
                {
                    Console.WriteLine("生成试图成功！");
                    int colorSize = Marshal.SizeOf(typeof(MyColor));
                    MyColor color;

                    // Make changes to the view.
                    for (long i = 0; i < test; i += colorSize)
                    {
                        //accessor.Read(i, out color);
                        

                        color = new MyColor();
                        color.Brighten((short)i);
                        accessor.Write(i, ref color);

                        //Console.WriteLine("执行：" + i);
                    }

                    //// Make changes to the view.
                    //for (long i = 0; i < test; i += colorSize)
                    //{
                    //    accessor.Read(i, out color);
                    //    //color.Brighten(10);
                    //    accessor.Write(i, ref color);

                    //    //Console.WriteLine("执行：" + i);
                    //}
                }
            }*/

            Console.WriteLine("完成！");

            Console.WriteLine("大小:" + test + "用时：" + (DateTime.Now - time).ToString());
            Console.Read();


        }
       

        static void Open()
        {
            // Assumes another process has created the memory-mapped file.
            using (var mmf = MemoryMappedFile.OpenExisting("ImgA"))
            {
                using (var accessor = mmf.CreateViewAccessor(4000000, 2000000))
                {
                    int colorSize = Marshal.SizeOf(typeof(MyColor));
                    MyColor color;

                    // Make changes to the view.
                    for (long i = 0; i < 1500000; i += colorSize)
                    {
                        accessor.Read(i, out color);
                        color.Brighten(20);
                        accessor.Write(i, ref color);
                    }
                }
            }
        }

        static void  CreateMemory()
        {
            using (MemoryMappedFile mmf = MemoryMappedFile.CreateNew("testmap", 10000))
            {
                bool mutexCreated;
                Mutex mutex = new Mutex(true, "testmapmutex", out mutexCreated);

                using (MemoryMappedViewStream stream = mmf.CreateViewStream())
                {
                    BinaryWriter writer = new BinaryWriter(stream);
                    writer.Write(1);
                }
                mutex.ReleaseMutex();

                Console.WriteLine("Start Process B and press ENTER to continue.");
                Console.ReadLine();

                Console.WriteLine("Start Process C and press ENTER to continue.");
                Console.ReadLine();

                mutex.WaitOne();
                using (MemoryMappedViewStream stream = mmf.CreateViewStream())
                {
                    BinaryReader reader = new BinaryReader(stream);
                    Console.WriteLine("Process A says: {0}", reader.ReadBoolean());
                    Console.WriteLine("Process B says: {0}", reader.ReadBoolean());
                    Console.WriteLine("Process C says: {0}", reader.ReadBoolean());
                }
                mutex.ReleaseMutex();
            }
        }

        static void OpenMemory()
        {
            try
            {
                using (MemoryMappedFile mmf = MemoryMappedFile.OpenExisting("testmap"))
                {

                    Mutex mutex = Mutex.OpenExisting("testmapmutex");
                    mutex.WaitOne();

                    using (MemoryMappedViewStream stream = mmf.CreateViewStream(1, 0))
                    {
                        BinaryWriter writer = new BinaryWriter(stream);
                        writer.Write(0);
                    }
                    mutex.ReleaseMutex();
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Memory-mapped file does not exist. Run Process A first.");
            }
        }

        static void OpenExistMemory()
        {
            try
            {
                using (MemoryMappedFile mmf = MemoryMappedFile.OpenExisting("testmap"))
                {

                    Mutex mutex = Mutex.OpenExisting("testmapmutex");
                    mutex.WaitOne();

                    using (MemoryMappedViewStream stream = mmf.CreateViewStream(2, 0))
                    {
                        BinaryWriter writer = new BinaryWriter(stream);
                        writer.Write(1);
                    }
                    mutex.ReleaseMutex();
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Memory-mapped file does not exist. Run Process A first, then B.");
            }
        }

        public struct MyColor
        {
            public short Red;
            public short Green;
            public short Blue;
            public short Alpha;

            // Make the view brighter.
            public void Brighten(short value)
            {
                Red = (short)Math.Min(short.MaxValue, (int)Red + value);
                Green = (short)Math.Min(short.MaxValue, (int)Green + value);
                Blue = (short)Math.Min(short.MaxValue, (int)Blue + value);
                Alpha = (short)Math.Min(short.MaxValue, (int)Alpha + value);
            }
        }
    }
}
