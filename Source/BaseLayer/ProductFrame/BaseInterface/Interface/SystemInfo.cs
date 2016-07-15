using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Globalization;

namespace OPT.Product.BaseInterface
{

    public interface IBxSystemInfo
    {
        IBxSystemPath SystemPath { get; }
        IBxUnitsCenter UnitsCenter { get; }
        IBxUnitsCenter BaseUnitsCenter { get; }
        IBxUIConfigProvider UIConfigProvider { get; }
        IBxDUCenter DefaultUnitCenter { get; }
        CultureInfo DefaultCultureInfo { get; }
    }
    public interface IBxSystemPath
    {
        string PEOffice6Documents { get; }
        string BinPath { get; }
        string ConfigPath { get; }
        string GlobalConfigPath { get; }
        string CultureConfigPath { get; }
        string ProjectsPath { get; }

        //string ProductsConfigPath { get; }
        //string GeneralConfigPath { get; }
        //string ModulesConfigPath { get; }

        //string UIConfigPath { get; }
       // string UnitConfigPath { get; }
    }









    public interface IBLSystemPath
    {
        string BinPath { get; }
        string ConfigPath { get; }
        string UIConfigPath { get; }
        string UnitConfigPath { get; }
    }

    public interface IBLSystemInfo
    {
        IBLSystemPath SystemPath { get; }
        IBxUnitsCenter Units { get; }
        IBLUIConfigs XmlUIConfigs { get; }
    }
}
