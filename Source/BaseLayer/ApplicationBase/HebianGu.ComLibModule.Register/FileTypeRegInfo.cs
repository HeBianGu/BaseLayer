#region <�� �� ע ��>
/*
 * ========================================================================
 * Copyright(c) �����²���ʯ�ͿƼ����޹�˾, All Rights Reserved.
 * ========================================================================
 *    
 * ���ߣ�[���]   ʱ�䣺2015/11/6 9:47:14  ��������ƣ�DEV-LIHAIJUN
 *
 * �ļ�����FileTypeRegInfo
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
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.RegisterEx
{

    public class FileTypeRegInfo
    {
        /// <summary>
        /// Ŀ�������ļ�����չ��
        /// </summary>
        public string ExtendName;  //".xcf"

        /// <summary>
        /// Ŀ���ļ�����˵��
        /// </summary>
        public string Description; //"XCodeFactory��Ŀ�ļ�"

        /// <summary>
        /// Ŀ�������ļ�������ͼ��
        /// </summary>
        public string IcoPath;

        /// <summary>
        /// ��Ŀ�������ļ���Ӧ�ó���
        /// </summary>
        public string ExePath;

        public FileTypeRegInfo()
        {
        }

        public FileTypeRegInfo(string extendName)
        {
            this.ExtendName = extendName;
        }
    }
}