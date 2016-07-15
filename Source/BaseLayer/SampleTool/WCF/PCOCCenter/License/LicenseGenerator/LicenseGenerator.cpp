// LicenseGenerator.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include "LicenseGenerator.h"

#include "../LicenseFile/LicenseFile.h"
#pragma comment(lib, "LicenseFile.lib")

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// 唯一的应用程序对象

CWinApp theApp;

using namespace std;

int _tmain(int argc, TCHAR* argv[], TCHAR* envp[])
{
	int nRetCode = 0;

	HMODULE hModule = ::GetModuleHandle(NULL);
	
	if (hModule != NULL)
	{
		// 初始化 MFC 并在失败时显示错误
		if (!AfxWinInit(hModule, NULL, ::GetCommandLine(), 0))
		{
			// TODO: 更改错误代码以符合您的需要
			_tprintf(_T("错误: MFC 初始化失败\n"));
			nRetCode = 1;
		}
		else
		{
			// 获取命令行参数 -k "keyfile.xml" -m "moduleInfofile.xml" -l "license.lic"
			// keyfile.key文件结构 （xml文件）
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

			// moduleInfofile.xml文件结构
			/*
			<?xml version="1.0" encoding="UTF-8"?>
			<!-- OPT License AppModuleInfo -->
			<LicenseInfo>
				<Customer>中石油胜利分公司</Customer>
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
				_tprintf(_T("LicenseGenerator.exe使用方法如下：\n"));
				_tprintf(_T("LicenseGenerator.exe -k\"keyfile.xml\" -m\"moduleInfofile.xml\" -l\"license.lic\"\n"));
				_tprintf(_T(" -k\"keyfile.xml\"         服务器主机的关键信息文件\n"));
				_tprintf(_T(" -m\"moduleInfofile.xml\"  模块配置信息，包括许可信息\n"));
				_tprintf(_T(" -l\"license.lic\"         输出许可信息授权文件\n"));

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

			// TODO: 在此处为应用程序的行为编写代码。
			LicenseFile oLicenseFile;

			oLicenseFile.SetLicenseInfo(strKeyFile.GetBuffer(strKeyFile.GetLength()), strModuleInfoFile.GetBuffer(strModuleInfoFile.GetLength()));
			oLicenseFile.WriteLicenseFile(_T(strLicenseFile.GetBuffer(strLicenseFile.GetLength())));
		}
	}
	else
	{
		// TODO: 更改错误代码以符合您的需要
		_tprintf(_T("错误: GetModuleHandle 失败\n"));
		nRetCode = 1;
	}

	return nRetCode;
}
