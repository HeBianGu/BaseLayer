using OPT.Product.SimalorManager.Eclipse.RegisterKeys.Child;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProperyGridControlDemo
{
    /// <summary> 维数定义 </summary>
    class DefineModelWSConfiger 
    {

        #region - TABDIMS -

        private TABDIMS Tabdims = new TABDIMS();

        [CategoryAttribute("表维数定义"), DescriptionAttribute("表维数定义		默认值1"), DisplayName("表维数定义"), ReadOnly(false)]
        public TABDIMS Tabdims1
        {
            get { return Tabdims; }
            set { Tabdims = value; }
        }

        #endregion

        #region - AQUDIMS -

        private AQUDIMS Aqudims = new AQUDIMS();
                [CategoryAttribute("平衡表维数定义"), DescriptionAttribute("平衡分区数"), DisplayName("平衡分区个数"), ReadOnly(false)]
        public AQUDIMS Aqudims1
        {
            get { return Aqudims; }
            set { Aqudims = value; }
        }

        #endregion
    }
}
