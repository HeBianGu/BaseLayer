using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Globalization;
using System.Xml;
using OPT.Product.BaseInterface;

namespace OPT.Product.Base
{
    public class BxSystemPath : IBxSystemPath
    {
        public BxSystemPath()
        {
            //string s;
            //foreach (Environment.SpecialFolder one in Enum.GetValues(typeof(Environment.SpecialFolder)))
            //{
            //    s = Environment.GetFolderPath(one);
            //}
        }
        public BxSystemPath(string binDirectoryPath, CultureInfo culture)
        {
            //PEOffice6Documents = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"OPT\PEOffice6", culture.Name);

            BinPath = binDirectoryPath;
            ModulesDllPath = Path.Combine(BinPath, @"Modules");

            ConfigPath = Path.Combine(binDirectoryPath, @"..\Config");

            GlobalConfigPath = Path.Combine(ConfigPath, @"Global");

            CultureConfigPath = Path.Combine(ConfigPath, culture.Name);
            RegistriesConfigPath = Path.Combine(CultureConfigPath, "Registries");
            ApplicationsConfigPath = Path.Combine(CultureConfigPath, "Applications");

            ProjectsPath = Path.Combine(binDirectoryPath, @"..\Projects\");

            PEOffice6Documents = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), GetPEOffice6DocumentSegment(GlobalConfigPath), culture.Name);
        }

        public static String GetPEOffice6DocumentSegment(string GlobalConfigPath)
        {
            String path = @"OPT\PEOffice6";
            String file = Path.Combine(GlobalConfigPath, @"BaseLayer\SysInfo\Paths.xml");

            if (File.Exists(file))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(file);
                XmlElement node = doc.SelectSingleNode("//path[@id='Documents']") as XmlElement;
                if (node != null)
                {
                    path = node.GetAttribute("v");
                }
            }
            return path;
        }
        #region IBxSystemPath 成员

        public string PEOffice6Documents { get; set; }
        public string BinPath { get; set; }
        public string ModulesDllPath { get; set; }

        public string ConfigPath { get; set; }

        public string GlobalConfigPath { get; set; }

        public string CultureConfigPath { get; set; }
        public string RegistriesConfigPath { get; set; }
        public string ApplicationsConfigPath { get; set; }

        public string ProjectsPath { get; set; }


        //public string UnitConfigPath { get; set; }
        #endregion
    }
}
