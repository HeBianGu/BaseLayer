using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.FrameWorkMode.ProvideMode
{
    /// <summary> 提供模式对象工程接口 </summary>
    interface ITimeEntityFactory
    {
        /// <summary> 创建时间步 </summary>
        List<ITimeEntity> CreateTimeEntitys();

    }



    class EclipseTimeEntityFactory : ITimeEntityFactory
    {
        public EclipseTimeEntityFactory(string baseFilePath)
        {
            _baseFilePath = baseFilePath;
        }

        string _baseFilePath;
        public List<ITimeEntity> CreateTimeEntitys()
        {
            //  具体创建方法
            List<ITimeEntity> entitys = new List<ITimeEntity>();

            return entitys;
        }

    }

    class tNavigatorTimeEntityFactory : ITimeEntityFactory
    {
        public tNavigatorTimeEntityFactory(string baseFilePath)
        {
            _baseFilePath = baseFilePath;
        }

        string _baseFilePath;

        public List<ITimeEntity> CreateTimeEntitys()
        {
            //  具体创建方法
            List<ITimeEntity> entitys = new List<ITimeEntity>();

            return entitys;

        }

    }
}
