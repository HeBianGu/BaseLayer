using HebianGu.ComLibModule.Wcf.Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.Wcf.Service
{
    /// <summary> 服务端运行案例的缓存，重启服务时清空 </summary>
    public class CaseManager
    {

        /// <summary> 服务器上的案例缓存 </summary>
        public  List<CaseConfiger> caseCache = new List<CaseConfiger>();

        /// <summary> 增加案例 </summary>
        public void AddCase(CaseConfiger pCase)
        {
            if (!caseCache.Exists(l => l.Name == pCase.Name))
                caseCache.Add(pCase);
        }
        /// <summary> 移除案例 </summary>
        public void RemoveCase(CaseConfiger pCase)
        {
            caseCache.Remove(pCase);
        }

        /// <summary> 存在案例么？ </summary>
        public bool ExistCase(CaseConfiger pCase)
        {
            return caseCache.Exists(l => l.Name == pCase.Name);
        }

        /// <summary> 从缓存中获取案例 </summary>
        public CaseConfiger GetCatchCase(CaseConfiger pCase, bool isCreate = false)
        {
           
            if (caseCache.Exists(l => l.Name == pCase.Name))
            {
                if (isCreate)
                {
                    caseCache.RemoveAll(l => l.Name == pCase.Name);
                    AddCase(pCase);
                }
            }
            else
            {
                AddCase(pCase);
            }

            return caseCache.Find(l => l.Name == pCase.Name);
        }

        /// <summary> 在缓存中查找案例 </summary>
        public List<CaseConfiger> FindAll(Predicate<CaseConfiger> match)
        {
            return caseCache.FindAll(match);
        }

        /// <summary> 在缓存中查找案例 </summary>
        public CaseConfiger Find(Predicate<CaseConfiger> match)
        {
            return caseCache.Find(match);
        }

    }
}
