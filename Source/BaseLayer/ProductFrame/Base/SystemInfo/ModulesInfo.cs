using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using OPT.Product.BaseInterface;

namespace OPT.Product.Base
{
#if false
    public class BxModulesInfo
    {
        public BxModuleInfo GetModuleInfo(string id)
        {
            string modulesConfigPath = BxSystemInfo.Instance.SystemPath.ModulesConfigPath;
            string modulesInfoFilePath = Path.Combine(modulesConfigPath, @"ModulesInfo.xml");
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
            XmlElement moduleNode = null;
            string xPath = string.Format("//file[@id='{0}']", id);
            moduleNode = root.SelectSingleNode(xPath) as XmlElement;
            if (moduleNode == null)
            {
                //MessageBox.Show("Can not find module " + id + " in file ModulesInfo.xml !\n");
                return null;
            }


            string relativePath = moduleNode.GetAttribute("relativePath");
            string dllName = moduleNode.GetAttribute("dll");
            dllName = Path.Combine(BxSystemInfo.Instance.SystemPath.ModulesDllPath, relativePath, dllName);
            string configPath = Path.Combine(modulesConfigPath, relativePath);

            BxModuleInfo ci = new BxModuleInfo();
            ci.DllFilePath = dllName;
            ci.ConfigPath = configPath;
            return ci;
        }


    }

#endif
}


















