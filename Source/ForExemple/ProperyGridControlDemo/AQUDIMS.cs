#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/1 13:39:53
 * 文件名：COORD
 * 说明：
界面参数	描述	关键字	备注
1	数值水体数据行最大数	AQUDIMS	默认值1
2	数值水体连接数据行最大数		默认值1
3	CT水体影响函数表最大数		默认值1
4	CT水体每个影响函数表的最大行数		默认值36
5	解析水体最大数		默认值1
6	E100：连接单个解析水体的网格最大数
E300: 解析水体连接数据行最大数		默认值1
7	水体列表最大数		默认值0
8	单个水体列表中解析水体最大个数		默认值0

 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProperyGridControlDemo
{
    /// <summary> 水体维数定义 </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class AQUDIMS 
    {
        /// <summary> 数值水体数据行最大数	AQUDIMS	默认值1 </summary>
        public bool szstzdhs0 = true;
        [CategoryAttribute("基本信息"), DescriptionAttribute("数值水体数据行最大数  默认值1"), DisplayName("1.数值水体数据行最大数"), ReadOnly(false)]
        public bool Szstzdhs0
        {
            get { return szstzdhs0; }
            set { szstzdhs0 = value; }
        }
        /// <summary> 数值水体连接数据行最大数		默认值1 </summary>
        private string szstljs1 = "1";
        [CategoryAttribute("基本信息"), DescriptionAttribute("数值水体连接数据行最大数		默认值1"), DisplayName("2.数值水体连接数据行最大数"), ReadOnly(false)]
        public string Szstljs1
        {
            get { return szstljs1; }
            set { szstljs1 = value; }
        }
        /// <summary> CT水体影响函数表最大数		默认值1 </summary>
        private string ctstyxhs2 = "1";
        [CategoryAttribute("基本信息"), DescriptionAttribute("CT水体影响函数表最大数		默认值1"), DisplayName("3.水体影响函数表最大数"), ReadOnly(false)]
        public string Ctstyxhs2
        {
            get { return ctstyxhs2; }
            set { ctstyxhs2 = value; }
        }
        /// <summary> CT水体每个影响函数表的最大行数		默认值36 </summary>
        private string ctstyxzdhs3 = "36";
        [CategoryAttribute("基本信息"), DescriptionAttribute("CT水体每个影响函数表的最大行数		默认值36"), DisplayName("4.CT水体每个影响函数表的最大行数"), ReadOnly(false)]
        public string Ctstyxzdhs3
        {
            get { return ctstyxzdhs3; }
            set { ctstyxzdhs3 = value; }
        }
        /// <summary> 解析水体最大数		默认值1 </summary>
        private string jxstzds4 = "1";
        [CategoryAttribute("基本信息"), DescriptionAttribute("解析水体最大数		默认值1 "), DisplayName("5.解析水体最大数"), ReadOnly(false)]
        public string Jxstzds4
        {
            get { return jxstzds4; }
            set { jxstzds4 = value; }
        }
        /// <summary> E100：连接单个解析水体的网格最大数 </summary>
        private Color e100wgzds5;
        [CategoryAttribute("基本信息"), DescriptionAttribute("E100：连接单个解析水体的网格最大数"), DisplayName("6.E100：连接单个解析水体的网格最大数"), ReadOnly(false)]
        public Color E100wgzds5
        {
            get { return e100wgzds5; }
            set { e100wgzds5 = value; }
        }
        /// <summary> E300: 解析水体连接数据行最大数		默认值1 </summary>
        private string e300jxstzds6 = "1";
        [CategoryAttribute("基本信息"), DescriptionAttribute(" E300: 解析水体连接数据行最大数		默认值1"), DisplayName("7.E300: 解析水体连接数据行最大数"), ReadOnly(false)]
        public string E300jxstzds6
        {
            get { return e300jxstzds6; }
            set { e300jxstzds6 = value; }
        }
        /// <summary> 水体列表最大数		默认值0 </summary>
        private string stlbzds7 = "0";
        [CategoryAttribute("基本信息"), DescriptionAttribute("水体列表最大数		默认值0"), DisplayName("8.水体列表最大数"), ReadOnly(false)]
        public string Stlbzds7
        {
            get { return stlbzds7; }
            set { stlbzds7 = value; }
        }
        /// <summary> 单个水体列表中解析水体最大个数		默认值0 </summary>
        private string jxstzds8 = "0";
        [CategoryAttribute("基本信息"), DescriptionAttribute("单个水体列表中解析水体最大个数		默认值0"), DisplayName("9.单个水体列表中解析水体最大个数"), ReadOnly(true)]
        public string Jxstzds8
        {
            get { return jxstzds8; }
            set { jxstzds8 = value; }
        }
        /// <summary> 单个水体列表中解析水体最大个数		默认值0 </summary>
        private string jxstzds9 = "0";
        [CategoryAttribute("基本信息"), DescriptionAttribute("单个水体列表中解析水体最大个数		默认值0"), DisplayName("9.单个水体列表中解析水体最大个数"), ReadOnly(true)]
        public string Jxstzds9
        {
            get { return jxstzds9; }
            set { jxstzds9 = value; }
        }
        /// <summary> 单个水体列表中解析水体最大个数		默认值0 </summary>
        private string jxstzds10 = "0";
        [CategoryAttribute("基本信息"), DescriptionAttribute("单个水体列表中解析水体最大个数		默认值0"), DisplayName("9.单个水体列表中解析水体最大个数"), ReadOnly(true)]
        public string Jxstzds10
        {
            get { return jxstzds10; }
            set { jxstzds10 = value; }
        }
        /// <summary> 单个水体列表中解析水体最大个数		默认值0 </summary>
        private string jxstzds11= "0";
        [CategoryAttribute("基本信息"), DescriptionAttribute("单个水体列表中解析水体最大个数		默认值0"), DisplayName("9.单个水体列表中解析水体最大个数"), ReadOnly(true)]
        public string Jxstzds11
        {
            get { return jxstzds11; }
            set { jxstzds11 = value; }
        }
        /// <summary> 单个水体列表中解析水体最大个数		默认值0 </summary>
        private string jxstzds12 = "0";
        [CategoryAttribute("基本信息"), DescriptionAttribute("单个水体列表中解析水体最大个数		默认值0"), DisplayName("9.单个水体列表中解析水体最大个数"), ReadOnly(true)]
        public string Jxstzds12
        {
            get { return jxstzds12; }
            set { jxstzds12 = value; }
        }
        /// <summary> 单个水体列表中解析水体最大个数		默认值0 </summary>
        private string jxstzds13 = "0";
        [CategoryAttribute("基本信息"), DescriptionAttribute("单个水体列表中解析水体最大个数		默认值0"), DisplayName("9.单个水体列表中解析水体最大个数"), ReadOnly(true)]
        public string Jxstzds13
        {
            get { return jxstzds13; }
            set { jxstzds13 = value; }
        }


        string formatStr = "{0}{1}{2}{3}{4}{5}{6}{7} /";
        /// <summary> 转换成字符串 </summary>
        public override string ToString()
        {
            return string.Format(formatStr, szstzdhs0.ToString(), szstljs1.ToString(), ctstyxhs2.ToString(), ctstyxzdhs3.ToString(), jxstzds4.ToString(),
                e100wgzds5.ToString(), e300jxstzds6.ToString(), stlbzds7.ToString(), jxstzds8.ToString());
        }


    }
}
