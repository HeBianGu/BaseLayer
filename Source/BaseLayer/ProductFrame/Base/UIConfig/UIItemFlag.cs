using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml;
using OPT.Product.BaseInterface;


namespace OPT.Product.Base
{
    public class BxUIConfigItemsFlag : IBxPersistString
    {
        protected UInt32 _flag = 0;
        protected UInt32 _validFlag = 0;
        //protected UInt32 _modifiedFlag = 0;

        public bool NeedSave
        {
            get { return _validFlag != 0; }
        }

        public bool HasValue(UInt32 mask) { return ((_validFlag & mask) != 0); }
        public void SetValue(UInt32 mask, bool? val)
        {
            if (val.HasValue)
            {
                _validFlag |= mask;
                if (val.Value)
                    _flag |= mask;
                else
                    _flag &= ~mask;
            }
            else
            {
                _validFlag &= ~mask;
            }
        }
        public bool? GetValue(UInt32 mask)
        {
            if ((_validFlag & mask) == 0)
                return null;
            return ((_flag & mask) != 0);
        }

        //public bool Modified { get { return _modifiedFlag != 0; } }
        //public bool Modified(UInt32 mask)
        //{
        //    return ((_modifiedFlag & mask) != 0);
        //}
        //void SetModified(UInt32 mask)
        //{
        //    _modifiedFlag |= mask;
        //}
        //public void ResetModified() { _modifiedFlag = 0; }

        public bool? Show { set { SetValue(s_flagMask_Show, value); } get { return GetValue(s_flagMask_Show); } }
        public bool? ShowTitle { set { SetValue(s_flagMask_ShowTitle, value); } get { return GetValue(s_flagMask_ShowTitle); } }
        public bool? Expand { set { SetValue(s_flagMask_Expand, value); } get { return GetValue(s_flagMask_Expand); } }
        public bool? UserHide { set { SetValue(s_flagMask_UserHide, value); } get { return GetValue(s_flagMask_UserHide); } }
        public bool? ReadOnly { set { SetValue(s_flagMask_ReadOnly, value); } get { return GetValue(s_flagMask_ReadOnly); } }
        public bool? ValueReadOnly { set { SetValue(s_flagMask_ValueReadOnly, value); } get { return GetValue(s_flagMask_ValueReadOnly); } }
        public bool? Fold { set { SetValue(s_flagMask_Fold, value); } get { return GetValue(s_flagMask_Fold); } }

        #region static flagMask
        static public UInt32 s_flagMask_Valid = ((UInt32)1) << 31;
        static public UInt32 s_flagMask_Show = 0x0001;
        static public UInt32 s_flagMask_ShowTitle = 0x0002;
        static public UInt32 s_flagMask_Expand = 0x0004;
        static public UInt32 s_flagMask_UserHide = 0x0080;
        static public UInt32 s_flagMask_ReadOnly = 0x0010;
        static public UInt32 s_flagMask_ValueReadOnly = 0x0020;
        static public UInt32 s_flagMask_Fold = 0x0040;
        #endregion

        #region IBxPersistString 成员

        public string SaveToString()
        {
            string s = string.Format("{0:x},{1:x}", _flag, _validFlag);
            return s;
        }

        public bool LoadFromString(string s)
        {
            int pos = s.IndexOf(',');
            string s1 = s.Substring(0, pos);
            string s2 = s.Substring(pos + 1);
            _flag = Convert.ToUInt32(s1, 16);
            _validFlag = Convert.ToUInt32(s2, 16);
            return true;
        }

        #endregion
    }
}