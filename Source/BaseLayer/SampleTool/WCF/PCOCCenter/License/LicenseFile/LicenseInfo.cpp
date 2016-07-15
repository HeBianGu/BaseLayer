#include "StdAfx.h"
#include "LicenseInfo.h"


LicenseInfo::LicenseInfo()
{
	UUID uuidLicense;
	RPC_STATUS st = UuidCreate(&uuidLicense);	
	unsigned char *pszGuid;     
	UuidToString(&uuidLicense, &pszGuid);
	uuid = (char*)pszGuid;     
	RpcStringFree(&pszGuid);

	CreateDate = "2012-01-01";
	Customer = "中石油胜利分公司";
	HostKey = "A5EB02F10D332BD2";
	Version = "Ver6.0.0.1";

	ModuleInfo oModuleInfo;
	oModuleInfo.Version = Version;

	CString AppName;
	for(int i=0; i<1; i++)
	{
		AppName.Format("ProdAna%d",i);
		oModuleInfo.AppName = AppName;
		oModuleInfo.ModuleName = _T("ProdAna.TestModule");
		oModuleInfo.LicenseCount = _T("30");
		oModuleInfo.ExpiryDate = _T("2012-12-31");
		oModuleInfo.ExpiryDays = _T("");
		oModuleInfo.LicenseType = "Trial";
		m_arrModuleInfos.Add(oModuleInfo);

		AppName.Format("ProdDesign%d",i);
		oModuleInfo.AppName = AppName;
		oModuleInfo.ModuleName = _T("ProdDesign.TestModule");
		oModuleInfo.LicenseCount = _T("2");
		oModuleInfo.ExpiryDate = _T("2013-12-31");
		oModuleInfo.ExpiryDays = _T("");
		oModuleInfo.LicenseType = "Trial";
		m_arrModuleInfos.Add(oModuleInfo);
	}
}


LicenseInfo::~LicenseInfo(void)
{
}

void LicenseInfo::SetCustomer(CString strCustomer)
{
	Customer = strCustomer;
}

void LicenseInfo::SetHostKey(CString strHostKey)
{
	HostKey = strHostKey;
}

void LicenseInfo::SetVersion(CString strVersion)
{
	Version = strVersion;
}

void LicenseInfo::SetCreateDate(CString strCreateDate)
{
	CreateDate = strCreateDate;
}
void LicenseInfo::SetLicenseType(CString strLicenseType)
{
	LicenseType = strLicenseType;
}
void LicenseInfo::SetLogoffLicenseID(CString strLogoffLicenseIDs)
{
	LogoffLicenseIDs = strLogoffLicenseIDs;
}

void LicenseInfo::Add(ModuleInfo oModuleInfo)
{
	m_arrModuleInfos.Add(oModuleInfo);
}

void LicenseInfo::Clear()
{
	m_arrModuleInfos.RemoveAll();
}

int LicenseInfo::GetLicenseAppCount()
{
	int nLicenseAppCount = 0;
	CString strLastAppName = "";
	for(int i=0; i<m_arrModuleInfos.GetCount(); i++)
	{
		if(strLastAppName!=m_arrModuleInfos[i].AppName)
		{
			strLastAppName = m_arrModuleInfos[i].AppName;
			nLicenseAppCount++;
		}
	}

	return nLicenseAppCount;
}

CString LicenseInfo::GenLicenseProperty()
{
	CString strSeperator = _T("\\&nsp");

	CString strLicenseProperty = uuid; strLicenseProperty += strSeperator;
	strLicenseProperty += Version; strLicenseProperty += strSeperator;
	strLicenseProperty += CreateDate; strLicenseProperty += strSeperator;
	strLicenseProperty += Customer; strLicenseProperty += strSeperator;
	strLicenseProperty += HostKey; strLicenseProperty += strSeperator;
	strLicenseProperty += LicenseType; strLicenseProperty += strSeperator;

	CString strModuleCount; strModuleCount.Format("%d", m_arrModuleInfos.GetCount());
	strLicenseProperty += strModuleCount; strLicenseProperty += strSeperator;

	CString strLicenseAppCount; strLicenseAppCount.Format("%d", GetLicenseAppCount());
	strLicenseProperty += strLicenseAppCount; strLicenseProperty += strSeperator;

	strLicenseProperty += LogoffLicenseIDs; strLicenseProperty += strSeperator;

	return strLicenseProperty;
}

