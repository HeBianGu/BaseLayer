using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using OPT.Product.BaseInterface;

namespace OPT.Product.Base
{
    public class BxTableSite<T> : BxArraySite<BxArrayValue<BxCompoundSite<T>>>, IBxElementTableSite
       where T : BxCompoundValue, new()
    {
        #region  members
        protected IBxUIConfigEx[] _columnConfigs = null;
        #endregion

        #region  constructor
        public BxTableSite() : base() { }
        #endregion

        public override void InitFieldInfo(IBxCompound container, FieldInfo info)
        {
            base.InitFieldInfo(container, info);
            //获取Sub element 的配置
            BxXmlUIItem staticItem = GetXmlUIItem();
            BxXmlUITable t = staticItem.SubItemTable;
            if (t != null)
            {
                _columnConfigs = new IBxUIConfigEx[t.SubColumns.Length];
                BxXmlUIItem item = null;
                for (int i = 0; i < t.SubColumns.Length; i++)
                {
                    item = new BxXmlUIItem(t.SubColumns[i], _staticItem.UIConfigFile);
                    _columnConfigs[i] = new BxUIConfigItemEx(item);
                }
            }
        }
        public override void InitStaticUIConfig(BxXmlUIItem staticItem)
        {
            base.InitStaticUIConfig(staticItem);
            BxXmlUITable t = staticItem.SubItemTable;
            if (t != null)
            {
                _columnConfigs = new IBxUIConfigEx[t.SubColumns.Length];
                BxXmlUIItem item = null;
                for (int i = 0; i < t.SubColumns.Length; i++)
                {
                    item = new BxXmlUIItem(t.SubColumns[i], _staticItem.UIConfigFile);
                    _columnConfigs[i] = new BxUIConfigItemEx(item);
                }
            }
        }

        #region IBxElementTableSite 成员
        public IBxElementSite GetCell(int row, int col)
        {
            T ele = _value[row].Value;
            if (ele == null)
                return null;
            IBxUIConfig config = null;
            Int32 id = _columnConfigs[col].ID;
            foreach (IBxElementSite one in ele.ChildSites)
            {
                config = one.UIConfig;
                if (config != null)
                {
                    if (config.ID == id)
                    {
                        return one;
                    }
                }
            }
            return null;
        }
        public IBxUIConfigEx[] ColumnConfigs
        {
            get { return _columnConfigs; }
        }
        public Int32 CenterColumn { get { return GetUIConfigItemEx().CenterColumn; } }
        public IBxContainer ElementEx
        {
            get { return _value; }
        }
        #endregion
    }


    public class BxArrayTableSite<T> : BxArraySite<T>,IBxElementTableSite
       where T : BxArrayValueBase, new()
    {
        #region  members
        protected IBxUIConfigEx[] _columnConfigs = null;
        #endregion

        #region  constructor
        public BxArrayTableSite() : base() { }
        #endregion

        public override void InitFieldInfo(IBxCompound container, FieldInfo info)
        {
            base.InitFieldInfo(container, info);
            //获取Sub element 的配置
            BxXmlUIItem staticItem = GetXmlUIItem();
            BxXmlUITable t = staticItem.SubItemTable;
            if (t != null)
            {
                _columnConfigs = new IBxUIConfigEx[t.SubColumns.Length];
                BxXmlUIItem item = null;
                for (int i = 0; i < t.SubColumns.Length; i++)
                {
                    item = new BxXmlUIItem(t.SubColumns[i], _staticItem.UIConfigFile, t.Ratios[i]);
                    _columnConfigs[i] = new BxUIConfigItemEx(item);
                }
            }
        }
        public override void InitStaticUIConfig(BxXmlUIItem staticItem)
        {
            base.InitStaticUIConfig(staticItem);
            BxXmlUITable t = staticItem.SubItemTable;
            if (t != null)
            {
                _columnConfigs = new IBxUIConfigEx[t.SubColumns.Length];
                BxXmlUIItem item = null;
                for (int i = 0; i < t.SubColumns.Length; i++)
                {
                    item = new BxXmlUIItem(t.SubColumns[i], _staticItem.UIConfigFile);
                    _columnConfigs[i] = new BxUIConfigItemEx(item);
                }
            }
        }


        //public void Append() { _value.Append(); }
        //public void AppendRange(int size) { _value.AppendRange(size); }
        //public void Insert(int index) { _value.Insert(index); }
        //public void InsertRange(int index, int size) { _value.InsertRange(index, size); }
        //public void Remove(int index) { _value.Remove(index); }
        //public void RemoveRange(int index, int size) { _value.RemoveRange(index, size); }
        //public void RemoveAll() { _value.RemoveAll(); }

        public int Count
        {
            get { return _value.Count; }
        }
        //public int ColumnCount { get { return _columnConfigs.Length; } }
        //public IBxUIConfig ColumnConfig(int index) { return _columnConfigs[index]; }
        #region IBxElementTableSite 成员
        public IBxUIConfigEx[] ColumnConfigs { get { return _columnConfigs; } }
        public Int32 CenterColumn { get { return GetUIConfigItemEx().CenterColumn; } }
        public IBxElementSite GetCell(int row, int col)
        {
            IBxCompound ele = _value.GetAt(row).Element as IBxCompound;
            if (ele == null)
                return null;
            IBxUIConfig config = null;
            Int32 id = _columnConfigs[col].ID;
            foreach (IBxElementSite one in ele.ChildSites)
            {
                config = one.UIConfig;
                if (config != null)
                {
                    if (config.ID == id)
                    {
                        return one;
                    }
                }
            }
            return null;
        }
        public IBxContainer ElementEx
        {
            get { return _value; }
        }
        #endregion
    }





    public class BxCompoundDefine<T> where T : BxCompoundValue, new()
    {
        public class Site : BxCompoundSite<T>
        {
        }
        public class Array : BxArrayValue<Site>
        {
        }
        public class ArraySite : BxArraySite<Array>
        {
        }
        public class TableSite : BxArrayTableSite<Array>
        {
        }
        public class TableSiteEx : BxTableSite<T>
        {
        }
    }

}
