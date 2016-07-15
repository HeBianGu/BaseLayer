using System;
using System.Collections.Generic;
using System.Reflection;
using OPT.Product.BaseInterface;
using System.Globalization;


namespace OPT.Product.Base
{
    public class BxUnitDouble : BxElementT<double>, IBxUIUnitValue/*, IBxElementInit*/
    {
        IBxUnit _unit;

        public BxUnitDouble() { _unit = null; }
        public BxUnitDouble(double val, IBxUnit unit)
            : base(val)
        {
            _unit = unit;
        }

        //public override bool Valid
        //{
        //    get { return base.Valid && (Unit != null); }
        //    set { base.Valid = value; }
        //}

        public void Init(IBxUnit unit)
        {
            _unit = unit;
            Valid = false;
        }

        public IBxUnit Unit
        {
            get { return _unit; }
            set { _unit = value; Valid = true; }
        }

        public void SetValue(double val, IBxUnit unit)
        {
            Value = val;
            Unit = unit;
        }
        public double GetValue(IBxUnit unit)
        {
            if (unit.BaseUnitCate != Unit.BaseUnitCate)
                throw new Exception("Unit Category is not Matching!");
            return Unit.EMConverTo(Value, unit);
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
            get { return Unit; }
            set { Unit = value; }
        }
        public string GetUIValue(IBxUnit unit)
        {
            if (!Valid)
                return null;
            if (unit.BaseUnitCate != Unit.BaseUnitCate)
                return null;

            double val = Unit.EMConverTo(Value, unit);
            return val.ToString(CultureInfo.InvariantCulture);
        }

        public string GetUIValue(IBxUnit trgUnit, IBxUnit decimalDigitsUnit, int decimalDigits)
        {
            if (!Valid)
                return null;
            if (trgUnit.BaseUnitCate != Unit.BaseUnitCate)
                return null;

            string val = BxUnitConvert.ConverToEx(Value, Unit, trgUnit, decimalDigitsUnit, decimalDigits);
            return val;
        }

        public bool SetUIValue(string val, IBxUnit unit)
        {
            double d;
            if (double.TryParse(val, NumberStyles.Any, CultureInfo.InvariantCulture, out d))
            {
                SetValue(d, unit);
                return true;
            }
            Valid = false;
            return false;
        }

        public bool SetUIValue(string val, string unitCate, string unit)
        {
            IBxUnit u = BxSystemInfo.Instance.UnitsCenter.EMParse(unitCate, unit);
            return SetUIValue(val, u);
        }
        #endregion

        #region IBxUIValue 成员
        public override string GetUIValue()
        {
            if (!Valid)
                return null;
            return Value.ToString(CultureInfo.InvariantCulture) + "," + _unit.Category.ID + "," + _unit.ID;
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

        public override bool IsLegalValue(string val)
        {
            double temp;
            return double.TryParse(val, NumberStyles.Any, CultureInfo.InvariantCulture, out temp);
        }
        #endregion

        #region IBxPersistString 成员
        public override string SaveToString()
        {
            if (!Valid)
                return null;
            return Value.ToString(CultureInfo.InvariantCulture) + "," + _unit.Category.Code + "," + _unit.Code;
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

        public void CopyFrom(BxUnitDouble val)
        {
            if (val.Valid)
                SetValue(val.Value, val.Unit);
            else
                Valid = false;
        }

        /// <summary>
        /// 比较两个数的值是否相等(在转换成同样单位的情况下)
        /// 如果误差(以第一个数的单位为基准)小于1E-06,则认为是相等的.
        /// 如果两个数的单位都是NULL,则返回TRUE;
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        /// <exception cref="System.Exception">
        /// 如果一个单位是NULL,另一个单位不是NULL,如抛出异常
        /// </exception>
        public bool Equals(BxUnitDouble val)
        {
            if (Unit == null)
            {
                return (val.Unit == null);
            }
            double d2 = val.GetValue(Unit);
            return (Value == d2) || Math.Abs(Value - d2) < DOUBLE_DELTA;
        }
        public override bool Equals(object obj)
        {
            if (!(obj is BxUnitDouble))
                return false;
            return base.Equals(obj as BxUnitDouble);
        }
        public override int GetHashCode()
        {
            return GetUIValue().GetHashCode();
        }

        public static BxUnitDouble operator +(BxUnitDouble d1, BxUnitDouble d2)
        {
            return new BxUnitDouble(d1.Value + d2.GetValue(d1.Unit), d1.Unit);
        }
        public static BxUnitDouble operator -(BxUnitDouble d1, BxUnitDouble d2)
        {
            return new BxUnitDouble(d1.Value - d2.GetValue(d1.Unit), d1.Unit);
        }
        public static BxUnitDouble operator *(BxUnitDouble d1, BxUnitDouble d2)
        {
            return new BxUnitDouble(d1.Value * d2.GetValue(d1.Unit), d1.Unit);
        }
        public static BxUnitDouble operator /(BxUnitDouble d1, BxUnitDouble d2)
        {
            return new BxUnitDouble(d1.Value / d2.GetValue(d1.Unit), d1.Unit);
        }

        public const double DOUBLE_DELTA = 1E-06;
    }


    public class BxUnitDoubleS : BxElementSiteT<BxUnitDouble>
    {
        protected override void OnElementCreated()
        {
            if (Config != null)
                Value.Init(Config.Unit);
        }

        public double ValueCorrespondToUIConfigUnit()
        {
            if ((UIConfig != null) && (UIConfig.Unit != null))
            {
                return Value.GetValue(UIConfig.Unit);
            }
            throw new Exception("UIConfig中没有单位,无法取值.");

        }


    }


    //public static class XXUnitDoubleExtendMethod
    //{
    //    public static void EmSetValue(this BxUnitDouble me, BxUnitDouble other)
    //    {
    //        me.SetValue(other.Value, other.Unit);
    //    }

    //    public static BxUnitDouble EmPlus(this BxUnitDouble me, BxUnitDouble other)
    //    {
    //        BxUnitDouble temp = new BxUnitDouble();
    //        double d1 = me.Value + other.GetValue(me.Unit);
    //        me.SetValue(d1, me.Unit);
    //        return temp;
    //    }

    //    public static BxUnitDouble EmSubmit(this BxUnitDouble me, BxUnitDouble other)
    //    {
    //        BxUnitDouble temp = new BxUnitDouble();
    //        double d1 = me.Value - other.GetValue(me.Unit);
    //        me.SetValue(d1, me.Unit);
    //        return temp;
    //    }

    //}


}