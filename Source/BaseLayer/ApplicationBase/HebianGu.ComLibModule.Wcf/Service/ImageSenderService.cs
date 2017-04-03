#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2017/4/1 16:08:28
 * 文件名：ImageSenderService
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using HebianGu.ComLibModule.Wcf.Service;
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

namespace HebianGu.ComLibModule.Wcf.ServiceInterface
{
    class ImageSenderService : IImageSenderService
    {
        static IStreamPrivider _streamprivider = new StreamPrivider();

        public void PrintStreenToStream()
        {
            Bitmap bitmap = WinSysHelper.Instance.PrintScreem();

            //FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            //新建一个内存流 用于存放图片
            MemoryStream strPic = new MemoryStream();

            bitmap.Save(strPic, ImageFormat.Jpeg);

            _streamprivider.SetStream(strPic);

        }

        public bool ReadNextBuffer()
        {
            return _streamprivider.ReadNextBuffer();
        }

        public byte[] GetCurrentBuffer()
        {
            return _streamprivider.GetCurrectBuffer();
        }
    }
}
