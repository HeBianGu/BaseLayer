using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HebianGu.FrameWorkMode.ProvideMode
{


    /// <summary> 提供模式 对象实体接口 </summary>
    public interface ITimeEntity
    {
        /// <summary> 文件路径 </summary>
        string FilePath
        {
            get;
            set;
        }

    }

    /// <summary> 动态时间实体模型 </summary>
    public class TimeEntity : ITimeEntity
    {
        
        string filePath;

        /// <summary> 文件路径 </summary>
        public string FilePath
        {
            get
            {
                return filePath;
            }
            set
            {
                filePath = value;
            }
        }

        string caseName;

        /// <summary> 案例名称 </summary>
        public string CaseName
        {
            get { return caseName; }
            set { caseName = value; }
        }


        public  int RunCaseTaskWork(System.Threading.CancellationToken pToken)
        {
            //  具体执行方法

            return 0;
        }
       

     
    }






}
