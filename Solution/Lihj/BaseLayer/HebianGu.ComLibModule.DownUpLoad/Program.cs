using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;

namespace HebianGu.ComLibModule.DownUpLoad.Web
{
    static class Program
    {
        static string _SavePath = @"D:\WorkArea\DevTest\BaseLayer\Solution\Lihj\BaseLayer\HebianGu.ComLibModule.DownUpLoad\bin\Debug\DownLoad";
        static void Main()
        {
            string fileName = @"D:\WorkArea\DevTest\BaseLayer\Solution\Lihj\BaseLayer\HebianGu.ComLibModule.DownUpLoad\bin\Debug\【BT吧】[BD中英双字1-3集]-西部世界 第一季-西方极乐园-Westworld Season 1-1.76GB.torrent";

            //string fileName = @"D:\WorkArea\DevTest\BaseLayer\Solution\Lihj\BaseLayer\HebianGu.ComLibModule.DownUpLoad\bin\Debug\杜拉拉升职记@圣城流氓兔兔.torrent";


            Torrent torrent = new Torrent(fileName);

            var v = torrent.GetType().GetProperties();

            foreach (var item in v)
            {
                object obj = item.GetValue(torrent);

                var objs = item.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (objs == null || objs.Length == 0) continue;

                DescriptionAttribute desc = objs[0] as DescriptionAttribute;

                Console.WriteLine(desc.Description + ":" + obj.ToString());
            }

            foreach (var item in torrent.FileList)
            {
                Console.WriteLine(item.PathUTF8.ToString());
            }

            string downloadFile = torrent.PublisherUTF8 + "\\" + torrent.FileList[0].PathUTF8;

            string url = Path.Combine(torrent.AnnounceList[1], downloadFile);

            url = @"http://btfans.3322.org:8000/announce?info_hash=%bd%06%ba%08%06%ba%94c%99%7c%04!b%03%91h%3f6%a7%c9&peer_id=-MO0200-625734543181&port=52138&supportcrypto=1&uploaded=0&downloaded=0&left=481433088&compact=1&numwant=100&key=%83%ea%fa%05%0f%3a%a7%3b&event=started";

            //url += "\\" + torrent.FileList[0].PathUTF8;

            HttpWebRequest hwr = WebRequest.Create(url) as HttpWebRequest;
            hwr.AllowWriteStreamBuffering = false;
            IAsyncResult res = hwr.BeginGetResponse(new AsyncCallback(DecodeResponse), hwr);
            //IAsyncResult res = hwr.BeginGetResponse(new AsyncCallback(AsyncDownLoadImg), hwr);
            Console.WriteLine("要下载的文件：" + downloadFile);

            Console.Read();
        }

        private static void DecodeResponse(IAsyncResult asyncResult)
        {
            WebRequest request = (WebRequest)asyncResult.AsyncState;
            string url = request.RequestUri.ToString();
            int count = 0;
            int num2 = 0;
            byte[] buffer = new byte[0x800];
            try
            {
                string saveFileName = _SavePath + "/" + url.Substring(url.LastIndexOf("/") + 1, url.Length - url.LastIndexOf("/") - 1);

                HttpWebResponse response = (HttpWebResponse)((HttpWebRequest)request).EndGetResponse(asyncResult);

                using (System.IO.FileStream stream = new System.IO.FileStream(saveFileName, System.IO.FileMode.Create))
                {
                    using (BinaryReader reader = new BinaryReader(response.GetResponseStream()))
                    {
                        if (response.ContentLength <= 0L)
                        {
                            goto Label_00B9;
                        }
                        while (num2 < response.ContentLength)
                        {
                            count = reader.Read(buffer, 0, buffer.Length);
                            stream.Write(buffer, 0, count);
                            num2 += count;
                        }
                        goto Label_00E8;
                    Label_00AE:
                        stream.Write(buffer, 0, count);
                    Label_00B9:
                        if ((count = reader.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            goto Label_00AE;
                        }
                    }
                Label_00E8:
                    response.Close();
                    stream.Seek(0L, SeekOrigin.Begin);
                }

                Console.WriteLine("下载完成");
            }
            catch (Exception ex)
            {
                Console.WriteLine("下载失败" + ex.Message);
            }

            Console.Read();
        }

        private static void AsyncDownLoadImg(IAsyncResult asyncResult)
        {
            WebRequest request = (WebRequest)asyncResult.AsyncState;
            string url = request.RequestUri.ToString();
            try
            {
                WebResponse response = request.EndGetResponse(asyncResult);
                long cLength = response.ContentLength;

                using (Stream s = response.GetResponseStream())
                {
                    string saveFileName = _SavePath + "/" + url.Substring(url.LastIndexOf("/") + 1, url.Length - url.LastIndexOf("/") - 1);

                    //FileStream的Write方法适合全部图片
                    //Image的Save不适合gif图片(Save(saveFileName, ImageFormat.Gif)同样不适合)
                    //WebClient的DownloadFileAsync可以，但是扩展不好
                    System.IO.FileStream so = new System.IO.FileStream(saveFileName, System.IO.FileMode.Create);
                    byte[] by = new byte[1024];
                    int size = 0;
                    while ((size = s.Read(by, 0, by.Length)) > 0)
                    {
                        so.Write(by, 0, size);
                    }
                    so.Close();


                    //Image img = Image.FromStream(s);
                    //img.Save(saveFileName);
                    //img.Dispose();

                    //WebClient wc = new WebClient();
                    //wc.DownloadFileAsync(new Uri(url), saveFileName);

                    s.Close();
                }

                Console.WriteLine("下载完成");
            }
            catch (Exception ex)
            {
                Console.WriteLine("下载失败" + ex.Message);
            }

            Console.Read();
        }
    }
}



