#pragma once

// 模块许可信息
class ModuleInfo
{
public:
	// 版本号
	CString Version;
	// 程序名
	CString AppName;
	// 模块名
	CString ModuleName;
	// 模块可用许可数
	CString LicenseCount;
	// 过期日期
	CString ExpiryDate;
	// 过期天数
	CString ExpiryDays;
	// 许可类型
	CString LicenseType;

	CString AppModuleStr()
	{
		CString strSeperator = _T("\\&msp");
		CString strAppModule = AppName; strAppModule += strSeperator;
		strAppModule += ModuleName; strAppModule += strSeperator;

		return strAppModule;
	}

	CString ModuleInfoStr()
	{
		CString strSeperator = _T("\\&msp");
		CString strModuleInfo = Version; strModuleInfo += strSeperator;
		strModuleInfo += AppName; strModuleInfo += strSeperator;
		strModuleInfo += ModuleName; strModuleInfo += strSeperator;
		strModuleInfo += LicenseCount; strModuleInfo += strSeperator;
		strModuleInfo += ExpiryDate; strModuleInfo += strSeperator;
		strModuleInfo += ExpiryDays; strModuleInfo += strSeperator;
		strModuleInfo += LicenseType; strModuleInfo += strSeperator;

		return strModuleInfo;
	}

	bool SegModuleString(CString strModuleInfo, CStringArray &strInfos)
	{
		CString strSeperator = _T("\\&msp");

		int nPos = strModuleInfo.Find(strSeperator);
		while(nPos>=0)
		{
			strInfos.Add(strModuleInfo.Left(nPos));
			strModuleInfo = strModuleInfo.Right(strModuleInfo.GetLength()-nPos-strSeperator.GetLength());

			nPos = strModuleInfo.Find(strSeperator);
		}

		return true;
	}

	bool ModuleFromString(CString strModule)
	{
		CStringArray strInfos;

		SegModuleString(strModule, strInfos);

		if( strInfos.GetCount()>=7 )
		{
			Version = strInfos[0];
			AppName = strInfos[1];
			ModuleName = strInfos[2];
			LicenseCount = strInfos[3];
			ExpiryDate = strInfos[4];
			ExpiryDays = strInfos[5];
			LicenseType = strInfos[6];
		}

		return true;
	}
};

class LicenseInfo
{
public:
	LicenseInfo(void);
	~LicenseInfo(void);

	int GetLicenseAppCount();
	CString GenLicenseProperty();

	void SetCustomer(CString strCustomer);
	void SetHostKey(CString strHostKey);
	void SetVersion(CString strVersion);
	void SetCreateDate(CString strCreateDate);
	void SetLicenseType(CString strLicenseType);
	void SetLogoffLicenseID(CString strLogoffLicenseIDs);
	void Add(ModuleInfo oModuleInfo);
	void Clear();


	CString uuid;		// 唯一许可识别码
	CString Version;	// 许可版本号
	CString CreateDate;	// 创建日期
	CString Customer;	// 客户名称
	CString HostKey;	// 客户服务器关键信息	
	CString LicenseType;// 许可类型
	CString LogoffLicenseIDs;// 已经注销的许可ID列表(;号间隔)

	CArray<ModuleInfo, ModuleInfo&> m_arrModuleInfos; // 模块信息
};

