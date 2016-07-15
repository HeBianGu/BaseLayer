using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml;
using System.IO;
using System.Globalization;
using OPT.Product.BaseInterface;

namespace OPT.Product.Base
{

    public class BxDUCenter : IBxDUCenter
    {
        List<BxDUProvider> _buffer = new List<BxDUProvider>();
        BxRegistries _registries = null;

        public BxDUCenter() { _registries = BxSystemInfo.Instance.Regisries; }
        public BxDUCenter(BxRegistries registries)
        {
            _registries = registries;
        }

        public void Refresh()
        {
            _buffer.Clear();
        }

        public string UserXmlFilePath(string moduleID)
        {
            string cultureConfig = BxSystemInfo.Instance.SystemPath.PEOffice6Documents;
            string relativePath = null;
            BxModulePath temp = BxSystemInfo.Instance.Regisries.GetModulePath(moduleID);
            if (temp != null)
            {
                relativePath = Path.Combine(@"Modules", temp.RelativePath);
            }
            else
            {
                relativePath = BxSystemInfo.Instance.Regisries.GetGeneralUIConfigRelativeDirectory(moduleID);
                if (!string.IsNullOrEmpty(relativePath))
                    relativePath = Path.Combine("GeneralLayer", relativePath);
            }
            if (string.IsNullOrEmpty(relativePath))
                return null;

            string path = Path.Combine(cultureConfig, relativePath);
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            return Path.Combine(path, @"DefaultUnits.xml");
        }

        public string SystemXmlFilePath(string moduleID)
        {
            string cultureConfig = BxSystemInfo.Instance.SystemPath.CultureConfigPath;
            string relativePath = null;
            BxModulePath temp = BxSystemInfo.Instance.Regisries.GetModulePath(moduleID);
            if (temp != null)
            {
                relativePath = Path.Combine(@"Modules", temp.RelativePath);
            }
            else
            {
                relativePath = BxSystemInfo.Instance.Regisries.GetGeneralUIConfigRelativeDirectory(moduleID);
                if (!string.IsNullOrEmpty(relativePath))
                    relativePath = Path.Combine("GeneralLayer", relativePath);
            }
            if (string.IsNullOrEmpty(relativePath))
                return null;

            string path = Path.Combine(cultureConfig, relativePath);
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            return Path.Combine(path, @"DefaultUnits.xml");
        }

        #region IBxDUCenter 成员
        public IBxDUProvider GetProvider(string moduleID)
        {
            BxDUProvider temp = _buffer.Find(x => x.ID == moduleID);
            if (temp == null)
            {
                //string path = SystemXmlFilePath(moduleID);
                //if (string.IsNullOrEmpty(path))
                //    return null;

                temp = new BxDUProvider();
                //temp.LoadSystemXml(path);
                temp.ID = moduleID;

                string path = UserXmlFilePath(moduleID);
                if (string.IsNullOrEmpty(path) || !System.IO.File.Exists(path))
                    path = SystemXmlFilePath(moduleID);
                if (!string.IsNullOrEmpty(path) && System.IO.File.Exists(path))
                {
                    temp.LoadUserXml(path);
                }


                _buffer.Add(temp);
            }
            return temp;
        }

        public IBxDefaultUnit GetDefaultUnit(string fullID)
        {
            string moduleId, id;
            BxUIConfigID.Split(fullID, out moduleId, out id);
            IBxDUProvider p = GetProvider(moduleId);
            if (p == null)
                return null;
            return p.Get(id);
        }

        public IBxDefaultUnit GetDefaultUnit(string moduleID, string id)
        {
            IBxDUProvider p = GetProvider(moduleID);
            if (p == null)
                return null;
            return p.Get(id);
        }

        #endregion
    }


    public static class BxDefultUnitExtendMethod
    {
        public static void EMInitDefaultUnit(this IBxCompound me)
        {

        }

        public static void InitDefaultUnit(IBxCompound cmpd, IBxDUCenter duc)
        {
            //foreach(IBxElementSite one in cmpd)
            //{
            //    IBxUIConfig uc = one.UIConfig;
            //    if (uc != null)
            //        InitDefaultUnitForUIConfig(uc, duc);

            //    if(one.Element is IBxContainer


            //}

        }

        public static void InitDefaultUnitForUIConfig(IBxUIConfig uc, IBxDUCenter duc)
        {
            if ((uc != null) && (uc.Unit != null))
            {
                IBxDefaultUnit du = duc.GetDefaultUnit(uc.FullID);
                if (du != null)
                {
                    uc.Unit = du.DefaultUnit;
                }
                //针对201表的列作处理
                if (uc.SubColumns != null)
                {
                    IBxDefaultUnit temp;
                    foreach (IBxSubColumn one in uc.SubColumns.Columns)
                    {
                        if (one.UIConfig.Unit != null)
                        {
                            temp = duc.GetDefaultUnit(one.UIConfig.FullID);
                            if (temp != null)
                                one.UIConfig.Unit = temp.DefaultUnit;
                        }
                    }
                }
            }
        }

        public static void InitDefaultUnit(IBxElementSite site, IBxDUCenter duc)
        {

        }
    }
}
