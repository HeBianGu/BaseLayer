// LicenseGenerator.cpp : �������̨Ӧ�ó������ڵ㡣
//

#include "stdafx.h"
#include "LicenseGenerator.h"

#include "../LicenseFile/LicenseFile.h"
#pragma comment(lib, "LicenseFile.lib")

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// Ψһ��Ӧ�ó������

CWinApp theApp;

using namespace std;

int _tmain(int argc, TCHAR* argv[], TCHAR* envp[])
{
	int nRetCode = 0;

	HMODULE hModule = ::GetModuleHandle(NULL);
	
	if (hModule != NULL)
	{
		// ��ʼ�� MFC ����ʧ��ʱ��ʾ����
		if (!AfxWinInit(hModule, NULL, ::GetCommandLine(), 0))
		{
			// TODO: ���Ĵ�������Է���������Ҫ
			_tprintf(_T("����: MFC ��ʼ��ʧ��\n"));
			nRetCode = 1;
		}
		else
		{
			// ��ȡ�����в��� -k "keyfile.xml" -m "moduleInfofile.xml" -l "license.lic"
			// keyfile.key�ļ��ṹ ��xml�ļ���
			/*
			<?xml version="1.0" encoding="UTF-8"?>
			<!--OPT License Request Key-->
			<HostInfo>
			<HostID>BF00</HostID>
			<HostKey>B9302A229BBB9A36</HostKey>
			<HostMAC>00-25-A8-20-22-00</HostMAC>
			<HostHDSN>V58P3V3NTS13005082SA</HostHDSN>
			<HostCPUID>BFEBFBFF000206A70000000000000000</HostCPUID>
			</HostInfo>
			*/

			// moduleInfofile.xml�ļ��ṹ
			/*
			<?xml version="1.0" encoding="UTF-8"?>
			<!-- OPT License AppModuleInfo -->
			<LicenseInfo>
				<Customer>��ʯ��ʤ���ֹ�˾</Customer>
				<CreateDate>2013-4-12</CreateDate>
				<Version>PEOffice V6.0.0.1</Version>
				<Applications>
					<Application Name="ProdAna" Version="V6.0.0.1">
					<Modules>
						<Module Name="ProdAna.TestModule1" Version="V6.0.0.1" LicenseCount="30" ExpiryDate="2014-4-12" LicenseType="Trial"/>
						<Module Name="ProdAna.CurveModule" Version="V6.0.0.1" LicenseCount="30" ExpiryDate="2014-4-12" LicenseType="Trial"/>
					</Modules>
					</Application>
					<Application Name="ProdDesign" Version="V6.0.0.1">
					<Modules>
						<Module Name="ProdDesign.TestModule1" Version="V6.0.0.1" LicenseCount="30" ExpiryDate="2014-4-12" LicenseType="Trial"/>
						<Module Name="ProdDesign.PVTModule" Version="V6.0.0.1" LicenseCount="30" ExpiryDate="2014-4-12" LicenseType="Trial"/>
					</Modules>
					</Application>
				</Applications>
			</LicenseInfo>
			*/
			if(argc!=4)
			{
				_tprintf(_T("LicenseGenerator.exeʹ�÷������£�\n"));
				_tprintf(_T("LicenseGenerator.exe -k\"keyfile.xml\" -m\"moduleInfofile.xml\" -l\"license.lic\"\n"));
				_tprintf(_T(" -k\"keyfile.xml\"         �����������Ĺؼ���Ϣ�ļ�\n"));
				_tprintf(_T(" -m\"moduleInfofile.xml\"  ģ��������Ϣ�����������Ϣ\n"));
				_tprintf(_T(" -l\"license.lic\"         ��������Ϣ��Ȩ�ļ�\n"));

				return nRetCode;
			}
			CMap<CString, LPCTSTR, CString, LPCTSTR> mapArg;

			for(int i=1; i<argc; i++)
			{
				CString sArg = argv[i];

				CString argType = sArg.Left(2);
				CString argStr  = sArg.Right(sArg.GetLength()-2);
				mapArg.SetAt(argType, argStr);
			}

			CString strKeyFile = mapArg["-k"];
			CString strModuleInfoFile = mapArg["-m"];
			CString strLicenseFile = mapArg["-l"];

			// TODO: �ڴ˴�ΪӦ�ó������Ϊ��д���롣
			LicenseFile oLicenseFile;

			oLicenseFile.SetLicenseInfo(strKeyFile.GetBuffer(strKeyFile.GetLength()), strModuleInfoFile.GetBuffer(strModuleInfoFile.GetLength()));
			oLicenseFile.WriteLicenseFile(_T(strLicenseFile.GetBuffer(strLicenseFile.GetLength())));
		}
	}
	else
	{
		// TODO: ���Ĵ�������Է���������Ҫ
		_tprintf(_T("����: GetModuleHandle ʧ��\n"));
		nRetCode = 1;
	}

	return nRetCode;
}
