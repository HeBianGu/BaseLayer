using System;
using System.Collections.Generic;
using System.Reflection;
using OPT.Product.BaseInterface;


namespace OPT.Product.Base
{
    public class BxUnitValue : BxSingleElement, IBxUIUnitValue/*, IBxElementInit*/
    {
        double _value;
        IBxUnit _unit;
        bool _InitedUnit;
        BxUIConfigItemEx _config;

        public BxUnitValue() { _value = default(double); _unit = null; _InitedUnit = false; }
        public BxUnitValue(double val, IBxUnit unit)
        {
            _value = val;
            Unit = unit;
        }

        public void InitConfig(BxUIConfigItemEx config)
        {
            _config = config;
        }

        void InitUnit()
        {
            if (_InitedUnit)
                return;
            if (_config != null)
                _unit = _config.Unit;
            _InitedUnit = true;
        }

        public double Value
        {
            get
            {
                return _value;
            }
            set { _value = value; }
        }
        public IBxUnit Unit
        {
            get
            {
                InitUnit();
                return _unit;
            }
            set
            {
                _InitedUnit = true;
                Valid = (value != null);
                _unit = value;
            }
        }

        public void SetUV(double val, IBxUnit unit)
        {
            _value = val;
            Unit = unit;
        }
        public void ChangeUnit(IBxUnit unit)
        {
            if (Unit != null)
            {
                if (Unit.Category != unit.Category)
                    throw new Exception("can not switch unit between diffrent unit category!");
                _value = Unit.EMConverTo(_value, unit);
                Unit = unit;
            }
        }


        #region IBxUIUnitValue 成员
        public IBxUnit UIUnit
        {
            get { return _config.Unit; }
            set { _config.Unit = value; }
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
                SetUV(d, unit);
                return true;
            }
            return false;
        }
        #endregion

        #region IBxUIValue 成员
        public override string GetUIValue()
        {
            return GetUIValue(UIUnit);
        }
        public override bool SetUIValue(string val)
        {
            return SetUIValue(val, UIUnit);
        }
        #endregion

        #region IBxPersistStorageNode 成员
        public override void SaveStorageNode(IBxStorageNode node)
        {
            node.SetElement(BxStorageLable.elementValue, _value.ToString());
            node.SetElement(BxStorageLable.elementUnitCate, Unit.Category.ID);
            node.SetElement(BxStorageLable.elementUnit, Unit.ID);
        }
        public override void LoadStorageNode(IBxStorageNode node)
        {
            string val = node.GetElementValue(BxStorageLable.elementValue);
            string uc = node.GetElementValue(BxStorageLable.elementUnitCate);
            string u = node.GetElementValue(BxStorageLable.elementUnit);
            if (!string.IsNullOrEmpty(uc))
            {
                IBxUnit unit = BxSystemInfo.Instance.UnitsCenter.Parse(uc).Parse(u);
                SetUIValue(val, unit);
            }
        }
        #endregion

        #region IBxPersistString 成员
        public override string SaveToString()
        {
            if (!Valid)
                return null;
            return _value.ToString() + "," + _unit.Category.Name + "," + _unit.Name;
        }
        public override bool LoadFromString(string s)
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
    }

    public class BxUnitValueS : BxGenerousSiteT<BxUnitValue>
    {
        #region  constructor
        public BxUnitValueS()
            : base()
        {
            _value.InitConfig(Config);
        }
        #endregion

        #region IBxElementSiteInit 成员
        public override void InitFieldInfo(IBxCompound container, FieldInfo info)
        {
            base.InitFieldInfo(container, info);
            _value.InitConfig(Config);
        }
        public override void InitStaticUIConfig(BxXmlUIItem staticItem)
        {
            base.InitStaticUIConfig(staticItem);
            _value.InitConfig(Config);
        }
        public override void InitCarrier(IBxElementCarrier carrier)
        {
            base.InitCarrier(carrier);
            _value.InitConfig(Config);
        }
        #endregion


        public double ValueEx
        {
            get { return _value.Value; }
            set { _value.Value = value; }
        }
        public IBxUnit Unit
        {
            get { return _value.Unit; }
            set { _value.Unit = value; }
        }
        public void SetUV(double val, IBxUnit unit)
        {
            _value.SetUV(val, unit);
        }
        public void ChangeUnit(IBxUnit unit)
        {
            _value.ChangeUnit(unit);
        }

        //#region IBxPersistStorageNode 成员
        //public string GetValue(IBxUnit unit)
        //{
        //    return _value.GetValue(unit);
        //}
        //public bool SetValue(string val, IBxUnit unit)
        //{
        //    return _value.SetValue(val, unit);
        //}
        //#endregion


    }

}