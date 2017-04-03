using HebianGu.ComLibModule.WinHelper;
using HebianGu.ComLibModule.WPF.Provider.Controls;
using HeBianGu.Product.RemoteMonitor.Client;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HeBianGu.Product.RemoteMonitor
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            exc = new ExcuteClientService("192.168.205.147", "22889");

            //exc = new ExcuteClientService("localhost", "29995");

            this.textBox.Text = "/c  netstat -an";

            //TreeViewHelper.Instance.LoadTree(this.treeView);

            this.LoadTree();

        }


        /// <summary> 此方法的说明 </summary>
        public void LoadTree()
        {
            var drivers = exc.GetDrivers();

            foreach (var item in drivers)
            {
                TreeViewItem t = new TreeViewItem();
                t.Header = item;
                t.Tag = item;
                this.treeView.Items.Add(t);

                LoadChildNode(t);

                t.Expanded += (object sender, RoutedEventArgs e) =>
                {
                    LoadChildChildNode(t);
                };


            }
        }


        /// <summary> 加载当前节点 </summary>
        public void LoadChildNode(TreeViewItem treeItem)
        {
            var folders = exc.GetFolder(treeItem.Tag.ToString());

            foreach (var item in folders)
            {
                TreeViewItem t = new TreeViewItem();
                t.Header = System.IO.Path.GetFileName(item);
                t.Tag = item;
                treeItem.Items.Add(t);

                LoadChildChildNode(t);
             
                t.Expanded += (object sender, RoutedEventArgs e) =>
                {
                    LoadChildChildNode(t);
                };
            }
        }

        /// <summary> 加载当前节点下级节点的下级节点(只加载一个,为了显示有子节点) </summary>
        public void LoadChildChildNode(TreeViewItem treeItem)
        {
            foreach (var item in treeItem.Items)
            {
                TreeViewItem tvi = item as TreeViewItem;

                var folders = exc.GetFolder(tvi.Tag.ToString());

                if (tvi.Items.Count > 0) continue;

                foreach (var it in folders)
                {
                    TreeViewItem t = new TreeViewItem();
                    t.Header = System.IO.Path.GetFileName(it);
                    t.Tag = it;
                    tvi.Items.Add(t);

                    t.Expanded += (object sender, RoutedEventArgs e) =>
                    {
                        LoadChildChildNode(t);
                    };
                }
            }
        }



        ExcuteClientService exc;

        private void button_Click(object sender, RoutedEventArgs e)
        {
            ;

            //string str= exc.ExecuteCmdOutPut(this.textBox.Text);

            // MessageBox.Show(str);

            //exc.ExecuteCmd(this.textBox.Text);

            //Bitmap bitmap = timer1_Tick();

            //Bitmap bitmap = WinSysHelper.Instance.PrintScreem();
            ////string ImageName = DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".jpg";
            ////string path = System.IO.Path.Combine(Environment.CurrentDirectory, ImageName);

            //MemoryStream s = new MemoryStream();
            //bitmap.Save(s, ImageFormat.Jpeg);


            //s.Position = 0;

            //byte[] buffer_currect = new byte[1000];

            //s.Read(buffer_currect, 0, 100);

            //Bitmap bit = new Bitmap(s);

            //ImageSource map = this.ChangeBitmapToImageSource(bit);

            ////BitmapImage map = this.ToBitMapImage(stream);

            //this.image.Source = map;

            ////释放资源 
            //bitmap.Dispose();





            ////Stream stream = exc.GetImage();

            ////Bitmap bit = new Bitmap(stream);
            Bitmap bit = exc.GetPrintScreen();

            ImageSource map = this.ChangeBitmapToImageSource(bit);


            //BitmapImage map = this.BitmapToBitmapImage(bit);

            ////BitmapImage map = this.ToBitMapImage(stream);

            this.image.Source = map;


        }


        private Bitmap timer1_Tick()
        {
            int width = Screen.PrimaryScreen.Bounds.Width;
            int height = Screen.PrimaryScreen.Bounds.Height;
            //获得当前屏幕的大小 
            System.Drawing.Size mySize = new System.Drawing.Size(width, height);
            Bitmap bitmap = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bitmap);
            g.CopyFromScreen(0, 0, 0, 0, mySize);
            g.Dispose();
            GC.Collect();

            return bitmap;
        }

        public ImageSource ChangeBitmapToImageSource(Bitmap bitmap)
        {
            //Bitmap bitmap = icon.ToBitmap();  
            IntPtr hBitmap = bitmap.GetHbitmap();


            ImageSource wpfBitmap = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                hBitmap,
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            return wpfBitmap;
        }

        private BitmapImage BitmapToBitmapImage(System.Drawing.Bitmap bitmap)
        {
            BitmapImage bitmapImage = new BitmapImage();
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                bitmap.Save(ms, bitmap.RawFormat);
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = ms;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
            }
            return bitmapImage;
        }


        public BitmapImage ToBitMapImage(Stream ms)
        {
            //System.IO.MemoryStream ms = new System.IO.MemoryStream(buffer);
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = ms;
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.EndInit();
            ms.Dispose();

            return bitmapImage;
        }



        // //新建一个文件流 读取图片信息
        //     FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
        //     //新建一个内存流 用于存放图片
        //     Stream strPic = new MemoryStream();
        //     //设置位置为0 从头开始拷贝
        //     fs.Position = 0;
        //     //拷贝
        //     fs.CopyTo(strPic);

        ////public void ReciveData()
        ////{


        //    IPAddress ip = IPAddress.Any;
        //    Socket server = new Socket(AddressFamily.InterNetwork,SocketType.Dgram, ProtocolType.Udp);
        //    EndPoint ipe = new IPEndPoint(ip, port);
        //    server.Bind(ipe);
        //    byte[] buffer = new byte[60000];
        //    while (true)
        //    {
        //        MemoryStream ms = new MemoryStream();
        //        int len = server.Receive(buffer);
        //        ms.Write(buffer, 0, len);
        //        while (true)
        //        {
        //            if (len == 1 && buffer[0] == 100)
        //            {
        //                break;
        //            }
        //            //len = server.Receive(buffer);
        //            ms.Write(buffer, 0, len);
        //        }
        //        //流里面存放了一个图片的所有字节
        //        Bitmap bmp = new Bitmap(ms);

        //        ImageSource imageSource =
        //       System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(
        //       bmp.,
        //       Int32Rect.Empty,
        //       BitmapSizeOptions.FromEmptyOptions());

        //        return imageSource;

        //        this.image.Source = bmp;
        //    }




        //}
    }
}
