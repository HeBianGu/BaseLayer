using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OPT.Product.Base
{

    class FESelf : IFEUnary
    {
        #region IFEUnary 成员
        public EFormulaElementType ElementType { get { return EFormulaElementType.unary; } }
        public bool CanFollowedBy(EFormulaElementType follow) {throw new Exception("Not Implemented!");}
       
        public double Calculate(double param) {return param;}
        public double Calculate(params double[] input){return input[0];}
        #endregion
    }

    /// <summary>
    /// 负号"-" ,一元算符
    /// </summary>
    class FEMinus : IFEUnary
    {
        #region IFEUnary 成员
        public EFormulaElementType ElementType { get { return EFormulaElementType.unary; } }
        public bool CanFollowedBy(EFormulaElementType follow)
        {
            switch (follow)
            {
                case EFormulaElementType.number: return true;
                case EFormulaElementType.variant: return true;
                case EFormulaElementType.unary: return true;
                case EFormulaElementType.leftBrace: return true;
            }
            return false;
        }
        public double Calculate(double param) { return -1*param; }
        public double Calculate(params double[] input){return -1*input[0];}
        #endregion
        
        public override string ToString()
        {
            return "-";
        }
    }

}
