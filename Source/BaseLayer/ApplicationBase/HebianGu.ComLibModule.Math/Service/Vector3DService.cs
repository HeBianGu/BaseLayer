#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2017/2/7 11:33:48
 * 文件名：Vector3DService
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.MathHelper
{
    /// <summary> 三维运算外部服务 </summary>
    public partial class Vector3DService
    {

        /// <summary> 求一些直线中离线外最近的一个投影（注：投影点需要在线上） </summary>
        public Vector3D GetNearestPoint(List<Vector3D> vecs, Vector3D myVec, Action<Vector3D, Vector3D> act = null)
        {
            Vector3D pcross = vecs.First();

            double minDistance = double.MaxValue;

            for (int i = 0; i < vecs.Count; i++)
            {
                if (i == 0) continue;

                Vector3D f1 = vecs[i - 1];

                Vector3D f2 = vecs[i];

                Vector3D pcrossPer = this.GetNearestPoint(f1, f2, myVec);

                double perDistance = myVec.Distance(pcrossPer);

                if (perDistance < minDistance)
                {
                    minDistance = perDistance;
                    pcross = pcrossPer;
                }
                if (act != null)
                {
                    act(f1, f2);
                }
            }

            return pcross;
        }

        /// <summary> 求点到线段最近的点 </summary>
        public Vector3D GetNearestPoint(Vector3D startLine, Vector3D endLine, Vector3D point)
        {
            Vector3D first = point - startLine;

            Vector3D second = point - endLine;

            Vector3D line = endLine - startLine;

            double dot1 = first.Dot(line);

            double dot2 = second.Dot(line);

            // Todo ：在外边 
            if (dot1 * dot2 < 0)
            {
                return this.GetProjectivePoint(startLine, endLine, point);
            }

            if (first.Magnitude() <= second.Magnitude())
            {
                return startLine;
            }

            return endLine;

        }

        /// <summary> 求直线外一点到该直线的投影点 </summary>
        public Vector3D GetProjectivePoint(Vector3D pStart, Vector3D pEnd, Vector3D pMy)
        {
            Vector3D pProject = new Vector3D();

            double k = (pEnd.Y - pStart.Y) / (pEnd.X - pStart.X);

            if (k == 0) //垂线斜率不存在情况
            {
                pProject.X = pMy.X;
                pProject.Y = pStart.Y;
            }
            else
            {
                pProject.X = (float)((k * pStart.X + pMy.X / k + pMy.Y - pStart.Y) / (1 / k + k));
                pProject.Y = (float)(-1 / k * (pProject.X - pMy.X) + pMy.Y);
            }

            return pProject;
        }
    }

    public partial class Vector3DService
    {
        #region - Start 单例模式 -

        /// <summary> 单例模式 </summary>
        private static Vector3DService t = null;

        /// <summary> 多线程锁 </summary>
        private static object localLock = new object();

        /// <summary> 创建指定对象的单例实例 </summary>
        public static Vector3DService Instance
        {
            get
            {
                if (t == null)
                {
                    lock (localLock)
                    {
                        if (t == null)
                            return t = new Vector3DService();
                    }
                }
                return t;
            }
        }
        /// <summary> 禁止外部实例 </summary>
        private Vector3DService()
        {

        }
        #endregion - 单例模式 End -

    }
}
