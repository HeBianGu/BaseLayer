using System;
using System.Collections.Generic;
using System.Reflection;
using OPT.Product.BaseInterface;


namespace OPT.Product.Base
{
    public class BxUnitDouble : BxElementT<double>, IBxUIUnitValue/*, IBxElementInit*/
    {
        IBxUnit _unit;

        public BxUnitDouble() { _value = default(double); _unit = null; }
        public BxUnitDouble(double val, IBxUnit unit)
        {
            _value = val;
            Unit = unit;
        }

        public override bool Valid { get { return _unit != null; } }
        public IBxUnit Unit
        {
            get { return _unit; }
            set { _unit = value; }
        }

        public void SetValue(double val, IBxUnit unit)
        {
            _value = val;
            Unit = unit;
        }
        //public void ChangeUnit(IBxUnit unit)
        //{
        //    if (Unit != null)
        //    {
        //        if (Unit.Category != unit.Category)
        //            throw new Exception("can not switch unit between diffrent unit category!");
        //        _value = Unit.EMConverTo(_value, unit);
        //        Unit = unit;
        //    }
        //}


        #region IBxUIUnitValue 成员
        public IBxUnit UIUnit
        {
            get { return _unit; }
            set { _unit = value; }
        }
        public string GetUIValue(IBxUnit unit)
        {
            double val = Unit.EMConverTo(_value, unit);
            return val.ToString();
        }
        public bool SetUIValue(string val, IBxUnit unit)
        {
            double d;
            if (double.TryParse(val, out d))
            {
                SetValue(d, unit);
                return true;
            }
            return false;
        }
        #endregion

        #region IBxUIValue 成员
        public override string GetUIValue()
        {
            if (!Valid)
                return null;
            return _value.ToString() + "," + _unit.Category.Name + "," + _unit.Name;
        }
        public override bool SetUIValue(string s)
        {
            string[] parts = s.Split(new char[] { ',' });
            if (parts.Length != 3)
            {
                Valid = false;
                return false;
            }
            IBxUnit unit = BxSystemInfo.Instance.UnitsCenter.Parse(parts[1]).Parse(parts[2]);
            return SetUIValue(parts[0], unit);
        }
        #endregion
        

        #region IBxPersistString 成员
        public override string SaveToString()
        {
            if (!Valid)
                return null;
            return _value.ToString() + "," + _unit.Category.Code + "," + _unit.Code;
        }
        public override bool LoadFromString(string s)
        {
            string[] parts = s.Split(new char[] { ',' });
            if (parts.Length != 3)
            {
                Valid = false;
                return false;
            }
            IBxUnit unit = BxSystemInfo.Instance.UnitsCenter.Find(parts[1]).Find(parts[2]);
            return SetUIValue(parts[0], unit);
        }
        #endregion
    }


    public class BxUnitDoubleS : BxElementSiteT<BxUnitDouble>
    {
       
    }

  


}