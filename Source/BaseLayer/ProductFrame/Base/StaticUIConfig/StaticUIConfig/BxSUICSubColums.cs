using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml;
using OPT.Product.BaseInterface;


namespace OPT.Product.Base
{
    public class BxSUICSubColum 
    {
        IBxStaticUIConfigPregnant _suicPregnant;
        UInt16 _widthRatio;
        string _code;

        public BxSUICSubColum(IBxStaticUIConfigPregnant suicPregnant, UInt16 widthRatio)
        {
            _suicPregnant = suicPregnant;
            _widthRatio = widthRatio;
            _code = null;
        }
        public BxSUICSubColum(IBxStaticUIConfigPregnant suicPregnant, UInt16 widthRatio, string code)
        {
            _suicPregnant = suicPregnant;
            _widthRatio = widthRatio;
            _code = code;
        }

        #region IBxSubColumn 成员

        public UInt16 WidthRation { get { return _widthRatio; } }
        public string Code { get { return _code; } }
        public IBxStaticUIConfigPregnant SUICPregnant { get { return _suicPregnant; } }
        #endregion
    }

    public class BxSUICSubColums
    {
        BxSUICSubColum[] _subColumns = null;
        Int32 _centerCol = -1;

        public BxSUICSubColum[] SubColumns { get { return _subColumns; } }
        public Int32 CenterCol { get { return _centerCol; } }

        public BxSUICSubColums(Int32 columnsNum, Int32 centerCol)
        {
            _subColumns = new BxSUICSubColum[columnsNum];
            _centerCol = centerCol;
        }

        public void InitColumn(int index, string fullID, IBxStaticUIConfigProvider suicProvider, UInt16 widthRatio)
        {
            _subColumns[index] = new BxSUICSubColum(new BxSUICPregnant(fullID, suicProvider), widthRatio);
        }
    }
}