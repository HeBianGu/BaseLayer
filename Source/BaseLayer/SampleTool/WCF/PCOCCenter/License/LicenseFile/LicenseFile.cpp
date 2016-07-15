// LicenseFile.cpp : ���� DLL �ĳ�ʼ�����̡�
//

#include "stdafx.h"
#include "Des.h"
#include "XMLFile.h"
using namespace std;

#define DLL_EXPORT
#include "LicenseFile.h"

char buffLicense[10240];
static CString strLicenseHeader = "OPTLIC";
static LicenseInfo oLicenseInfo;

void LicenseFile::SetLicenseInfo(char *strKeyFile, char *strModuleInfoFile)
{
	CXMLFile *xml = new CXMLFile(strKeyFile);
	xml->Open();
	string HostKey = xml->GetString("HostInfo", "HostKey", "");
	string LogoffLicenseIDs = xml->GetString("HostInfo", "LogoffLicenseID", "");
	delete xml;

	xml = new CXMLFile(strModuleInfoFile);
	xml->Open();
	string Customer = xml->GetString("LicenseInfo", "Customer", "");
	string CreateDate = xml->GetString("LicenseInfo", "CreateDate", "");
	string Version = xml->GetString("LicenseInfo", "Version", "");
	string LicenseType = xml->GetString("LicenseInfo", "LicenseType", "");

	MSXML2::IXMLDOMNodeListPtr appNodeList=NULL;
	long ret = xml->GetNodeList("LicenseInfo/Applications/Application", appNodeList);

	oLicenseInfo.Clear();
	if(appNodeList!=NULL)
	{
		MSXML2::IXMLDOMNodePtr appNode= NULL;
		for (int i=0; i<appNodeList->length; i++)
		{
			appNode = appNodeList->item[i];
			CString appName = xml->GetNodeAttribute(appNode, "Name", "").c_str();
			CString appVersion = xml->GetNodeAttribute(appNode, "Version", "").c_str();

			MSXML2::IXMLDOMNodeListPtr moduleNodeList=NULL;
			moduleNodeList = appNode->selectNodes(_bstr_t("Modules/Module"));
			
			if(moduleNodeList!=NULL)
			{
				MSXML2::IXMLDOMNodePtr moduleNode= NULL;
				for (int i=0; i<moduleNodeList->length; i++)
				{
					moduleNode = moduleNodeList->item[i];

					ModuleInfo oModuleInfo;
					oModuleInfo.AppName = appName;
					oModuleInfo.ModuleName = xml->GetNodeAttribute(moduleNode, "Name", "").c_str();
					oModuleInfo.Version = xml->GetNodeAttribute(moduleNode, "Version", "").c_str();
					oModuleInfo.LicenseCount = xml->GetNodeAttribute(moduleNode, "LicenseCount", "").c_str();
					oModuleInfo.ExpiryDate = xml->GetNodeAttribute(moduleNode, "ExpiryDate", "").c_str();
					oModuleInfo.LicenseType = xml->GetNodeAttribute(moduleNode, "LicenseType", "").c_str();

					oLicenseInfo.Add(oModuleInfo);
				}
			}
		}
	}

	delete xml;

	oLicenseInfo.SetHostKey(HostKey.c_str());
	oLicenseInfo.SetCustomer(Customer.c_str());
	oLicenseInfo.SetVersion(Version.c_str());
	oLicenseInfo.SetCreateDate(CreateDate.c_str());
	oLicenseInfo.SetLicenseType(LicenseType.c_str());
	oLicenseInfo.SetLogoffLicenseID(LogoffLicenseIDs.c_str());
}

bool LicenseFile::WriteLicenseFile(char * strFile)
{
	Encryption oEncryption;

	CStdioFile myFile;

	CFileException fileException;

	if ( !myFile.Open( strFile, CFile::modeCreate | CFile::modeReadWrite, &fileException ) )
	{
		return false;
	}

	try
	{
		myFile.WriteString(strLicenseHeader+"\n");
		CString strLicenseProperty = oLicenseInfo.GenLicenseProperty();
		strLicenseProperty = oEncryption.EncryString(strLicenseProperty);
		myFile.WriteString(strLicenseProperty+"\n");

		CDES des;
		myFile.WriteString("[ModuleList]\n");
		for(int i=0; i<oLicenseInfo.m_arrModuleInfos.GetCount(); i++)
		{
			CString strAppModule = oLicenseInfo.m_arrModuleInfos[i].AppModuleStr();
			strAppModule = des.EncryString(strAppModule, "OPT.ModuleList");
			myFile.WriteString(strAppModule+"\n");
		}

		CString strLastAppName = "";
		myFile.WriteString("[LicenseInfo]\n");
		for(int i=0; i<oLicenseInfo.m_arrModuleInfos.GetCount(); i++)
		{
			if(strLastAppName!=oLicenseInfo.m_arrModuleInfos[i].AppName)
			{
				strLastAppName = oLicenseInfo.m_arrModuleInfos[i].AppName;
				CString strModuleInfo = oLicenseInfo.m_arrModuleInfos[i].ModuleInfoStr();
				strModuleInfo = oEncryption.EncryString(strModuleInfo);
				myFile.WriteString(strModuleInfo+"\n");
			}
		}

		myFile.Close();
	}
	catch (CException* e)
	{
		return false;
	}

	return true;
}

/**
*  @param cbCharSize: lpCharStr���ֽڸ�����������Ϊ-1�������null������
*  @param cchWCharCnt: lpWCharStr���ַ�����
*  
*  @return: ����ɹ�������д��lpWCharStr���ַ�����������ɹ�����cchWiCharCnt==0��
*  ����lpWideCharStr��������Ҫ���ַ�������
*  ʧ�ܷ���0��
*  
**/
DWORD CharToWChar_t( LPSTR lpCharStr, DWORD cbCharSize, LPWSTR lpWCharStr, DWORD cchWCharCnt)
{
	return MultiByteToWideChar( CP_ACP, 0, lpCharStr, cbCharSize, lpWCharStr, cchWCharCnt );
}

/**
*  @param cchWCharCnt: lpCharStr�����ַ�wchar_t����
*  @return: ������Ҫ���ַ�����������null��������
**/
DWORD CStringToWChar_t( CString cstrInput, LPWSTR lpWCharStr, DWORD cchWCharCnt)
{
#ifdef UNICODE
	if ( cchWCharCnt > cstrInput.GetLength() )
	{
		wcscpy_s( lpWCharStr, cchWCharCnt, (LPCWSTR)(LPCTSTR)cstrInput );
	}
	return cstrInput.GetLength()+1;
#else
	return CharToWChar_t( (LPSTR)(LPCTSTR)cstrInput, cstrInput.GetLength(), lpWCharStr, cchWCharCnt);
#endif
}

bool LicenseFile::GetLicenseAppModuleList(wchar_t * wchFile, wchar_t ** appModuleList, int iarray, int itemMaxLength)
{
	CString strFile = CString(wchFile);

	CStdioFile myFile;

	CFileException fileException;

	if ( !myFile.Open( CString(strFile), CFile::modeRead, &fileException ) )
	{
		return false;
	}

	try
	{
		CString strFileHeader;
		CString strNode;
		CDES des;
		try
		{
			myFile.ReadString(strFileHeader);
			if(strFileHeader == strLicenseHeader)
			{
				while(myFile.ReadString(strNode))
				{
					if(strNode=="[ModuleList]")
					{
						CString strAppModule;

						int nModuleCount = 0;
						while(nModuleCount<iarray && myFile.ReadString(strAppModule))
						{
							if(strAppModule == "[LicenseInfo]") break;

							strAppModule = des.DecryString(strAppModule, "OPT.ModuleList");


							CStringW strW(strAppModule);

							if (strW.GetLength() < itemMaxLength)
							{
								wcscpy(appModuleList[nModuleCount], strW.GetBuffer());
							}
							else
							{
								ASSERT(FALSE);
							}


							nModuleCount++;
						}

						break;
					}
				}
			}
		}
		catch (CException* e)
		{
		}
		myFile.Close();


	}
	catch (CException* e)
	{
		return false;
	}

	return true;
}

bool LicenseFile::GetLicenseModuleInfo(wchar_t * wchFile, wchar_t ** moduleInfos, int iarray, int itemMaxLength)
{
	CString strFile = CString(wchFile);

	CStdioFile myFile;

	CFileException fileException;

	if ( !myFile.Open( CString(strFile), CFile::modeRead, &fileException ) )
	{
		return false;
	}

	try
	{
		CString strFileHeader, strNode;
		CString strLicenseProperty;
		Decryption oDecryption;
		try
		{
			myFile.ReadString(strFileHeader);
			if(strFileHeader == strLicenseHeader)
			{
				while(myFile.ReadString(strNode))
				{
					if(strNode=="[LicenseInfo]")
					{
						CString strModuleInfo;

						int nModuleCount = 0;
						while(nModuleCount<iarray && myFile.ReadString(strModuleInfo))
						{
							strModuleInfo = oDecryption.DecryString(strModuleInfo);

							CStringW strW(strModuleInfo);

							if (strW.GetLength() < itemMaxLength)
							{
								wcscpy(moduleInfos[nModuleCount], strW.GetBuffer());
							}
							else
							{
								ASSERT(FALSE);
							}

							nModuleCount++;
						}
						break;
					}
				}
			}
		}
		catch (CException* e)
		{
		}
		myFile.Close();


	}
	catch (CException* e)
	{
		return false;
	}

	return true;
}

char * LicenseFile::GetLicenseProperty(char * strFile)
{
	CString strLicenseProperty;
	Decryption oDecryption;

	CStdioFile myFile;

	CFileException fileException;

	if ( !myFile.Open( CString(strFile), CFile::modeRead, &fileException ) )
	{
		return NULL;
	}

	try
	{
		CString strFileHeader;
		try
		{
			myFile.ReadString(strFileHeader);
			if(strFileHeader == strLicenseHeader)
			{
				myFile.ReadString(strLicenseProperty);
			}
		}
		catch (CException* e)
		{
		}
		myFile.Close();

		strLicenseProperty = oDecryption.DecryString(strLicenseProperty);
		
		strcpy(buffLicense, strLicenseProperty);
	}
	catch (CException* e)
	{
		return NULL;
	}

	return buffLicense;
}
