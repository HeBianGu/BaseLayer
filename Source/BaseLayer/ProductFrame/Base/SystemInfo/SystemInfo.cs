using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Globalization;
using System.Xml;
using OPT.Product.BaseInterface;

namespace OPT.Product.Base
{
    public class BxSystemInfo : IBxSystemInfo
    {
        CultureInfo _culture = null;
        BxSystemPath _systemPath = null;
        BxUnitsProvider _baseUnitsCenter = null;
        BxSubSetUnitsCenter _unitsCenter = null;
        BxUIConfigProvider _provider = null;
        BxStaticUIConfigProvider _suicProvider = null;
        BxRegistries _regisries = null;
        BxDUCenter _defaultUnitCenter = null;
        BxDDCenter _defaultDataCenter = null;

        public void RefreshDefaultUnit()
        {
            if (_defaultUnitCenter != null)
                _defaultUnitCenter.Refresh();
        }
        public void RefreshDefaultData()
        {
            if (_defaultDataCenter != null)
                _defaultDataCenter.Refresh();
        }


        public void Init(string binDirectoryPath)
        {
            string rootPath = Path.Combine(binDirectoryPath, @"..\");
            //取PEOffice的整体系统配置
            InitProductsConfig(rootPath);

            //设置路径信息
            _systemPath = new BxSystemPath(binDirectoryPath, _culture);

            //加载注册信息
            _regisries = new BxRegistries(_systemPath);

            //加载单位制
            string sConfigFilePath = Path.Combine(_systemPath.GlobalConfigPath, @"BaseLayer\Unit\Unit.xml");
            string sParsedFilePath = Path.Combine(_systemPath.GlobalConfigPath, @"BaseLayer\Unit\ParsedUnit.xml");
            _baseUnitsCenter = new BxUnitsProvider(sConfigFilePath, sParsedFilePath);
            _baseUnitsCenter.Init();

            string unitSetFilePath = Path.Combine(_systemPath.CultureConfigPath, @"BaseLayer\UnitSet\UnitSet.xml");
            _unitsCenter = new BxSubSetUnitsCenter(_baseUnitsCenter);
            _unitsCenter.LoadUnitConfigFile(unitSetFilePath);
        }

        static BxSystemInfo s_instance = new BxSystemInfo();
        public static BxSystemInfo Instance { get { return s_instance; } }

        //取PEOffice的整体系统配置
        void InitProductsConfig(string rootPath)
        {
            String globalPath = Path.Combine(rootPath, @"Config\Global");
            string PEOffice6Documents = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), BxSystemPath.GetPEOffice6DocumentSegment(globalPath));
            string productsConfigFilePath = Path.Combine(PEOffice6Documents, @"Global\ProductsInfo\Config.xml");
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(productsConfigFilePath);
            }
            catch (Exception)
            {
                productsConfigFilePath = Path.Combine(rootPath, @"Config\Global\ProductsInfo\Config.xml");
                doc = new XmlDocument();
                doc.Load(productsConfigFilePath);
            }
            XmlElement cultureNode = doc.DocumentElement.SelectSingleNode("CultureInfo") as XmlElement;
            string language = cultureNode.GetAttribute("language");
            _culture = CultureInfo.GetCultureInfo(language);
        }

        #region IBxSystemInfo 成员
        public IBxSystemPath SystemPath { get { return _systemPath; } }
        public IBxUnitsCenter UnitsCenter { get { return _unitsCenter; } }
        public IBxUnitsCenter BaseUnitsCenter { get { return _baseUnitsCenter; } }
        public IBxUIConfigProvider UIConfigProvider
        {
            get
            {
                if (_provider == null)
                {
                    _provider = new BxUIConfigProvider();
                }
                return _provider;
            }
        }
        public IBxStaticUIConfigProvider SUICProvider
        {
            get
            {
                if (_suicProvider == null)
                {
                    _suicProvider = new BxStaticUIConfigProvider(UIConfigProvider);
                }
                return _suicProvider;
            }
        }
        public IBxDUCenter DefaultUnitCenter
        {
            get
            {
                if (_defaultUnitCenter == null)
                {
                    _defaultUnitCenter = new BxDUCenter();
                }
                return _defaultUnitCenter;
            }
        }
        public CultureInfo DefaultCultureInfo
        {
            get { return _culture; }
        }
        #endregion

        public BxRegistries Regisries { get { return _regisries; } }

        public IBxDDCenter DefaultDataCenter
        {
            get
            {
                if (_defaultDataCenter == null)
                {
                    _defaultDataCenter = new BxDDCenter();
                }
                return _defaultDataCenter;
            }
        }
    }
}
