#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/18 10:31:48
 * 文件名：Dimensional
 * 说明：多维空间结构
 *       Create by lihaijun 多维空间一次类推
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using HebianGu.ComLibModule.Struct.Dimensional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.Struct.Dimensional
{
    /// <summary> 一维空间 </summary>
    public class _1D<T> : DimBase
    {
        List<T> data = new List<T>();
    }
    /// <summary> 二维空间 </summary>
    public class _2D<T> : List<_1D<T>>
    {
    }

    /// <summary> 三维空间 </summary>
    public class _3D<T> : List<_2D<T>>
    {
    }

    /// <summary> 四维空间 </summary>
    public class _4D<T> : List<_3D<T>>
    {
    }

    /// <summary> 五维空间 </summary>
    public class _5D<T> : List<_4D<T>>
    {

    }
}
