#pragma once

// ģ�������Ϣ
class ModuleInfo
{
public:
	// �汾��
	CString Version;
	// ������
	CString AppName;
	// ģ����
	CString ModuleName;
	// ģ����������
	CString LicenseCount;
	// ��������
	CString ExpiryDate;
	// ��������
	CString ExpiryDays;
	// �������
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


	CString uuid;		// Ψһ���ʶ����
	CString Version;	// ��ɰ汾��
	CString CreateDate;	// ��������
	CString Customer;	// �ͻ�����
	CString HostKey;	// �ͻ��������ؼ���Ϣ	
	CString LicenseType;// �������
	CString LogoffLicenseIDs;// �Ѿ�ע�������ID�б�(;�ż��)

	CArray<ModuleInfo, ModuleInfo&> m_arrModuleInfos; // ģ����Ϣ
};

