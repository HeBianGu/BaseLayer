// LicenseFile.h : LicenseFile DLL 的主头文件
//

#pragma once

#include "LicenseInfo.h"
#include "Encryption.h"
#include "Decryption.h"

#ifdef DLL_EXPORT
#define DLL_EXPORT __declspec(dllexport)
#else
#define DLL_EXPORT __declspec(dllimport)
#endif

// CLicenseFileApp
// 有关此类实现的信息，请参阅 LicenseFile.cpp
//

class DLL_EXPORT LicenseFile
{

public:
	void SetLicenseInfo(char *strKeyFile, char *strModuleInfoFile);
	bool WriteLicenseFile(char * strFile);
	char * GetLicenseProperty(char * strFile);
	bool GetLicenseModuleInfo(wchar_t * wchFile, wchar_t ** moduleInfos, int iarray, int itemMaxLength);
	bool GetLicenseAppModuleList(wchar_t * wchFile, wchar_t ** appModuleList, int iarray, int itemMaxLength);
};
