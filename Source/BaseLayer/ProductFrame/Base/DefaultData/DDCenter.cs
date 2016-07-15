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

    public class BxDDCenter : IBxDDCenter
    {
        List<BxDDProvider> _buffer = new List<BxDDProvider>();
        BxRegistries _registries = null;

        public BxDDCenter() { _registries = BxSystemInfo.Instance.Regisries; }
        public BxDDCenter(BxRegistries registries)
        {
            _registries = registries;
        }
        public void Refresh()
        {
            _buffer.Clear();
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
            return Path.Combine(path, @"DefaultData.xml");
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
            return Path.Combine(path, @"DefaultData.xml");
        }



        #region IBxDDCenter 成员
        public IBxDDProvider GetProvider(string moduleID)
        {
            BxDDProvider temp = _buffer.Find(x => x.ID == moduleID);
            if (temp == null)
            {
                //string path = SystemXmlFilePath(moduleID);
                //if (string.IsNullOrEmpty(path))
                //    return null;

                temp = new BxDDProvider();
                temp.ID = moduleID;
                //temp.LoadSystemXml(path);

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

        public IBxDefaultData GetDefaultData(string fullID)
        {
            string moduleId, id;
            BxUIConfigID.Split(fullID, out moduleId, out id);
            IBxDDProvider p = GetProvider(moduleId);
            if (p == null)
                return null;
            return p.Get(id);
        }

        public IBxDefaultData GetDefaultData(string moduleID, string id)
        {
            IBxDDProvider p = GetProvider(moduleID);
            if (p == null)
                return null;
            return p.Get(id);
        }
        #endregion
    }


}
