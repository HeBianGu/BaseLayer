using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OPT.Product.Base
{
    /// <summary>
    /// 2元算符
    /// </summary>
    abstract class FEBinary : IFEBinary
    {
        #region IFEBinary 成员
        public EFormulaElementType ElementType { get { return EFormulaElementType.binary; } }
        public bool CanFollowedBy(EFormulaElementType follow)
        {
            switch (follow)
            {
                case EFormulaElementType.number: return true;
                case EFormulaElementType.variant: return true;
                case EFormulaElementType.leftBrace: return true;
                case EFormulaElementType.unary: return true;
            }
            return false;
        }
        abstract public double Calculate(params double[] input);
        abstract public double Calculate(double param1, double param2);
        abstract public EFEOperatorGrade Grade { get; }
        #endregion
    }

    /// <summary>
    /// 加号"+" ,2元算符
    /// </summary>
    sealed class FEBinaryAdd : FEBinary
    {
        override public string ToString()
        {
            return "+";
        }
        override public EFEOperatorGrade Grade { get { return EFEOperatorGrade.grade0; } }
        override public double Calculate(params double[] input) { return input[0] + input[1]; }
        override public double Calculate(double param1, double param2) { return param1 + param2; }
    }

    /// <summary>
    /// 减号"-" ,2元算符
    /// </summary>
    sealed class FEBinarySubtract : FEBinary
    {
        override public string ToString()
        {
            return "-";
        }
        override public EFEOperatorGrade Grade { get { return EFEOperatorGrade.grade0; } }
        override public double Calculate(params double[] input) { return input[0] - input[1]; }
        override public double Calculate(double param1, double param2) { return param1 - param2; }
    }

    /// <summary>
    /// 乘号"*" ,2元算符
    /// </summary>
    sealed class FEBinaryMultiply : FEBinary
    {
        override public string ToString()
        {
            return "*";
        }
        override public EFEOperatorGrade Grade { get { return EFEOperatorGrade.grade1; } }
        override public double Calculate(params double[] input) { return input[0] * input[1]; }
        override public double Calculate(double param1, double param2) { return param1 * param2; }
    }

    /// <summary>
    /// 除号"/" ,2元算符
    /// </summary>
    sealed class FEBinaryDivide : FEBinary
    {
        override public string ToString()
        {
            return "/";
        }
        override public EFEOperatorGrade Grade { get { return EFEOperatorGrade.grade1; } }
        override public double Calculate(params double[] input) { return input[0] / input[1]; }
        override public double Calculate(double param1, double param2) { return param1 / param2; }
    }
}
