using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OPT.Product.Base
{
    sealed class FENumber : IFormulaElement
    {
        private double _value;
        public double Value { get { return _value; } }

        public FENumber(double dVal) { _value = dVal; }

        #region IFormulaElement 成员
        public EFormulaElementType ElementType{ get { return EFormulaElementType.number; } }
        public bool CanFollowedBy(EFormulaElementType follow)
        {
            switch (follow)
            {
                case EFormulaElementType.binary: return true;
                case EFormulaElementType.rightBrace: return true;
            }
            return false;
        }
        #endregion
    }

    sealed class FEVariant : IFormulaElement
    {
        private Int16 _index;
        public Int16 Index { get { return _index; } }
        public FEVariant(Int16 nIndex) : base() { _index = nIndex; }

        #region IFormulaElement 成员
        public EFormulaElementType ElementType { get { return EFormulaElementType.variant; } }
        public bool CanFollowedBy(EFormulaElementType follow)
        {
            switch (follow)
            {
                case EFormulaElementType.binary: return true;
                case EFormulaElementType.rightBrace: return true;
            }
            return false;
        }
        #endregion

        public override string ToString()
        {
            return string.Format("Variant{0}", _index);
        }
    }

    sealed class FELeftBrace : IFormulaElement
    {
        public EFormulaElementType ElementType { get { return EFormulaElementType.leftBrace; } }
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

        public override string ToString()
        {
            return "(";
        }
    }

    sealed class FERightBrace : IFormulaElement
    {
        #region IFormulaElement 成员
        public EFormulaElementType ElementType { get { return EFormulaElementType.rightBrace; } }
        public bool CanFollowedBy(EFormulaElementType follow)
        {
            switch (follow)
            {
                case EFormulaElementType.binary: return true;
                case EFormulaElementType.rightBrace: return true;
            }
            return false;
        }
        #endregion

        public override string ToString()
        {
            return ")";
        }
    }

}
