#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2017/4/1 15:49:40
 * 文件名：StreamService
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using HebianGu.ComLibModule.Wcf.Service.Interface;
using HebianGu.ComLibModule.WinHelper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.Wcf.Service
{
    class StreamPrivider: IStreamPrivider
    {
        static byte[] buffer_currect = null;

        static int get_buffer_length = 1024 * 100;  //设置每次传10k

        // HTodo  ：总长度 
        static long remain_length;

        static Stream _stream;

        /// <summary> 当前读到的缓存 </summary>
        public byte[] GetCurrectBuffer()
        {
             return buffer_currect;
        }

        public void SetStream(Stream stream)
        {
            _stream = stream;

            _stream.Position = 0;

            remain_length = stream.Length;
        }

        /// <summary> 读取压缩后字节流一块，并提升字节流的位置 </summary>
        public bool ReadNextBuffer()
        {
            bool bo;

            if (remain_length > 0)
            {
                if (remain_length > get_buffer_length)
                {
                    buffer_currect = new byte[get_buffer_length];

                    _stream.Read(buffer_currect, 0, get_buffer_length);
                    remain_length -= get_buffer_length;
                }
                else
                {
                    buffer_currect = new byte[remain_length];
                    _stream.Read(buffer_currect, 0, (int)remain_length);
                    remain_length = 0;
                }

                bo = true;
            }
            else
            {
                bo = false;
                _stream.Dispose();
            }
              
            return bo;

        }


        public void PrintStreenToStream()
        {
            Bitmap bitmap = WinSysHelper.Instance.PrintScreem();

            //FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            //新建一个内存流 用于存放图片
            MemoryStream strPic = new MemoryStream();

            bitmap.Save(strPic, ImageFormat.Jpeg);

            strPic.Position = 0;

            buffer_currect = new byte[get_buffer_length];


            strPic.Read(buffer_currect, 0, get_buffer_length);

            this.SetStream(strPic);

        }

    }
}
