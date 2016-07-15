using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using OPT.Product.BaseInterface;

namespace OPT.Product.Base
{

    //public class BxTableSite1<T> : BxElementSiteT<BxArray<BxElementSiteT<T>>>, IBxElementTableSite
    //where T : BxCompound, new()
    //{
    //    #region  members
    //    protected IBxUIConfig[] _columnConfigs = null;
    //    #endregion

    //    #region  constructor
    //    #endregion

    //    public override void InitFieldInfo(IBxCompound container, FieldInfo info)
    //    {
    //        base.InitFieldInfo(container, info);
    //        //获取Sub element 的配置

    //    }

    //    #region IBxElementTableSite 成员
    //    public IBxElementSite GetCell(int row, int col)
    //    {
    //        T ele = Value[row].Value;
    //        if (ele == null)
    //            return null;
    //        IBxUIConfig config = null;
    //        Int32 id = _columnConfigs[col].ID;
    //        foreach (IBxElementSite one in ele.ChildSites)
    //        {
    //            config = one.UIConfig;
    //            if (config != null)
    //            {
    //                if (config.ID == id)
    //                {
    //                    return one;
    //                }
    //            }
    //        }
    //        return null;
    //    }
    //    public IBxUIConfig[] ColumnConfigs
    //    {
    //        get 
    //        {
    //            if (_columnConfigs == null)
    //            {
    //                if( (UIConfig != null) && (UIConfig.SubColumns != null))
    //                {
    //                    _columnConfigs = new IBxUIConfig[UIConfig.SubColumns.Count];
    //                    int index = 0;
    //                    foreach (IBxSubColumn one in UIConfig.SubColumns.SubColumns)
    //                    {
    //                        ColumnConfigs[index] = one.UIConfig as IBxUIConfig;
    //                        index++;
    //                    }
    //                }
    //                //BxXmlUIItem staticItem = GetXmlUIItem();
    //                //BxXmlUITable t = staticItem.SubItemTable;
    //                //if (t != null)
    //                //{
    //                //    _columnConfigs = new IBxUIConfig[t.SubColumns.Length];
    //                //    BxXmlUIItem item = null;
    //                //    for (int i = 0; i < t.SubColumns.Length; i++)
    //                //    {
    //                //        item = new BxXmlUIItem(t.SubColumns[i], _staticItem.UIConfigFile);
    //                //        _columnConfigs[i] = new BxUIConfigFromSuic(item);
    //                //    }
    //                //}
    //            }
    //            return _columnConfigs; 
    //        }
    //    }
    //    public Int32 CenterColumn { get { return GetUIConfigItemEx().CenterColumn; } }
    //    public IBxContainer ElementEx
    //    {
    //        get { return Value; }
    //    }
    //    #endregion
    //}




    public class BxTableSiteEx<T> : BxElementSiteT<T>, IBxElementTableSite
      where T : IBxContainer, IBxElementOwner, new()
    {
        #region  members
        //protected IBxUIConfig[] _columnConfigs = null;
        #endregion

        #region  constructor
        public BxTableSiteEx() : base() { }
        #endregion

        public int Count
        {
            get { return Value.Count; }
        }
        #region IBxElementTableSite 成员
        //public IBxUIConfig[] ColumnConfigs
        //{
        //    get
        //    {
        //        if (_columnConfigs == null)
        //        {
        //            if ((UIConfig != null) && (UIConfig.SubColumns != null))
        //            {
        //                _columnConfigs = new IBxUIConfig[UIConfig.SubColumns.Count];
        //                int index = 0;
        //                foreach (IBxSubColumn one in UIConfig.SubColumns.SubColumns)
        //                {
        //                    ColumnConfigs[index] = one.UIConfig as IBxUIConfig;
        //                    index++;
        //                }
        //            }
        //        }
        //        return _columnConfigs;
        //    }
        //}
        //public Int32 CenterColumn
        //{
        //    get
        //    {
        //        if ((UIConfig != null) && (UIConfig.SubColumns != null))
        //            return UIConfig.SubColumns.CenterColumn;
        //        return -1;
        //    }
        //}
        public IBxElementSite GetCell(int row, int col)
        {
            IBxCompound ele = Value.GetAt(row).Element as IBxCompound;
            if (ele == null)
                return null;
            IBxUIConfig config = null;
            int id = SubColumns[col].UIConfig.ID;
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
            get { return Value; }
        }
        public IBxSubColumns SubColumns
        {
            get
            {
                if (UIConfig != null)
                    return UIConfig.SubColumns;
                return null;
            }
        }
        #endregion
    }

}
