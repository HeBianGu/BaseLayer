using System;
using System.Collections.Generic;
using System.Reflection;
//using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Globalization;

//using DevExpress.XtraBars.Ribbon;
//using DevExpress.XtraBars;
//using DevExpress.Utils;

//using OPT.Product.Windows.Forms;
//using OPT.Product.Base;


namespace ModuleManager
{
    public class BxModulesManager
    {
        public IBxFunctionModule GetFunctionModule(string id)
        {
            OPT.Product.Base.BxModulePath modulePath = BxSystemInfo.Instance.Regisries.GetModulePath(id);

            OPT.Product.BaseInterface.IBxUIConfigFile file = BxSystemInfo.Instance.UIConfigProvider.GetUIConfigFile(id);
            if(modulePath == null)
                return null;

            try
            {
                Assembly ass = Assembly.LoadFile(modulePath.DllFilePath);
                object[] objs = ass.GetCustomAttributes(typeof(BxModulesAttribute), false);
                IBxModules modules = (objs[0] as BxModulesAttribute).GetModules();
                return modules.GetModule(id, BxModuleType.FuncionModule) as IBxFunctionModule;
            }
            catch (System.Exception e)
            {
                return null;
            }

        }

        public IBxAnalyzeModule GetAnalyzeModule(string id)
        {
            OPT.Product.Base.BxModulePath modulePath = BxSystemInfo.Instance.Regisries.GetModulePath(id);
            if (modulePath == null)
                return null;

            try
            {
                Assembly ass = Assembly.LoadFile(modulePath.DllFilePath);
                object[] objs = ass.GetCustomAttributes(typeof(BxModulesAttribute), false);
                IBxModules modules = (objs[0] as BxModulesAttribute).GetModules();
                return modules.GetModule(id, BxModuleType.FuncionModule) as IBxAnalyzeModule;
            }
            catch (System.Exception e)
            {
                return null;
            }

        }

        public IBxResultModule GetResultModule(string id)
        {
            string path = BxSystemInfo.Instance.Regisries.GetResultModuleFilePath(id);
            if (path == null)
                return null;

            try
            {
                Assembly ass = Assembly.LoadFile(path);
                object[] objs = ass.GetCustomAttributes(typeof(BxModulesAttribute), false);
                IBxModules modules = (objs[0] as BxModulesAttribute).GetModules();
                return modules.GetModule(id, BxModuleType.ResultModule) as IBxResultModule;
            }
            catch (System.Exception e)
            {
                return null;
            }
        }

        static BxModulesManager s_instance = new BxModulesManager();
        public static BxModulesManager Instance { get { return s_instance; } }
    }
}
