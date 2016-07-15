using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using OPT.Product.BaseInterface;

namespace OPT.Product.Base
{
    public class BxModulePath
    {
        public string DllFilePath { get; set; }
        public string ConfigPath { get; set; }
        public string RelativePath { get; set; }
    }

    public class BxRegistries
    {
        string _modulesDllPath = null;

        string _baseLayerConfigPath = null;
        string _generalConfigPath = null;
        string _applicationConfigPath = null;
        string _modulesConfigPath = null;
        string _registriesPath = null;

        string GeneralLayerConfigPath { get { return _generalConfigPath; } }
        string BaseLayerConfigPath { get { return _baseLayerConfigPath; } }
        string ApplicationConfigPath { get { return _applicationConfigPath; } }
        string ModulesConfigPath { get { return _modulesConfigPath; } }

        public BxModulePath GetModulePath(string id)
        {
            string modulesInfoFilePath = Path.Combine(_registriesPath, @"ModulesInfo.xml");
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(modulesInfoFilePath);
            }
            catch (System.Exception)
            {
                //MessageBox.Show("Can not open file " + modulesInfoFilePath + "!\n");
                return null;
            }
            XmlElement root = doc.DocumentElement;
            XmlElement fmNode = root.SelectSingleNode("FunctionModules") as XmlElement;
            string xPath = string.Format("//Module[@id='{0}']", id);
            XmlElement moduleNode = fmNode.SelectSingleNode(xPath) as XmlElement;
            if (moduleNode == null)
            {
                //MessageBox.Show("Can not find module " + id + " in file ModulesInfo.xml !\n");
                return null;
            }

            string relativePath = moduleNode.GetAttribute("relativePath");
            string dllName = moduleNode.GetAttribute("dll");
            dllName = Path.Combine(_modulesDllPath, relativePath, dllName);
            string configPath = Path.Combine(_modulesConfigPath, relativePath);

            BxModulePath mpath = new BxModulePath();
            mpath.RelativePath = relativePath;
            mpath.DllFilePath = dllName;
            mpath.ConfigPath = configPath;
            return mpath;
        }

        public string GetApplicationConfigPath(string id)
        {
            return Path.Combine(_applicationConfigPath, id);
        }

        public string GetUIConfigFilePath(string uiConfigID)
        {
            string uiConfigFilePath = GetGeneralUIConfigFilePath(uiConfigID);
            if (string.IsNullOrEmpty(uiConfigFilePath))
                uiConfigFilePath = GetModulesUIConfigFilePath(uiConfigID);
            return uiConfigFilePath;
        }

        string GetGeneralUIConfigFilePath(string uiConfigID)
        {
            string uiConfigFilePath = null;
            string filePath = Path.Combine(_registriesPath, @"GeneralUIConfigsInfo.xml");
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);
                XmlElement node = doc.DocumentElement.SelectSingleNode(string.Format(".//UIConfig[@id='{0}']", uiConfigID)) as XmlElement;
                if (node == null)
                    return null;

                string relativePath = node.GetAttribute("relativePath");
                uiConfigFilePath = Path.Combine(_generalConfigPath, relativePath);
            }
            catch (System.Exception)
            {
            }
            return uiConfigFilePath;
        }
        string GetModulesUIConfigFilePath(string uiConfigID)
        {
            BxModulePath path = GetModulePath(uiConfigID);
            if (path != null)
                return Path.Combine(path.ConfigPath, "UIConfig.xml");
            return null;
        }

        public BxRegistries(IBxSystemPath path)
        {
            _modulesDllPath = Path.Combine(path.BinPath, @"Modules");

            _baseLayerConfigPath = Path.Combine(path.CultureConfigPath, @"BaseLayer");
            _generalConfigPath = Path.Combine(path.CultureConfigPath, @"GeneralLayer");
            _applicationConfigPath = Path.Combine(path.CultureConfigPath, @"Applications");
            _modulesConfigPath = Path.Combine(path.CultureConfigPath, @"Modules");
            _registriesPath = Path.Combine(path.CultureConfigPath, @"Registries");
        }

        public string GetResultModuleFilePath(string id)
        {
            string modulesInfoFilePath = Path.Combine(_registriesPath, @"ModulesInfo.xml");
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(modulesInfoFilePath);
            }
            catch (System.Exception)
            {
                //MessageBox.Show("Can not open file " + modulesInfoFilePath + "!\n");
                return null;
            }
            XmlElement root = doc.DocumentElement;
            XmlElement rmNode = root.SelectSingleNode("ResultModules") as XmlElement;
            string xPath = string.Format("//Module[@id='{0}']", id);
            XmlElement moduleNode = rmNode.SelectSingleNode(xPath) as XmlElement;
            if (moduleNode == null)
            {
                //MessageBox.Show("Can not find module " + id + " in file ModulesInfo.xml !\n");
                return null;
            }

            string relativePath = moduleNode.GetAttribute("relativePath");
            string dllName = moduleNode.GetAttribute("dll");
            dllName = Path.Combine(_modulesDllPath, relativePath, dllName);

            return dllName;
        }

        public string GetGeneralUIConfigRelativeDirectory(string uiConfigID)
        {
            string uiConfigFilePath = null;
            string filePath = Path.Combine(_registriesPath, @"GeneralUIConfigsInfo.xml");
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);
                XmlElement node = doc.DocumentElement.SelectSingleNode(string.Format(".//UIConfig[@id='{0}']", uiConfigID)) as XmlElement;
                if (node == null)
                    return null;

                string relativePath = node.GetAttribute("relativePath");
                uiConfigFilePath = Path.GetDirectoryName( relativePath);
            }
            catch (System.Exception)
            {
            }
            return uiConfigFilePath;
        }

    }
}


















