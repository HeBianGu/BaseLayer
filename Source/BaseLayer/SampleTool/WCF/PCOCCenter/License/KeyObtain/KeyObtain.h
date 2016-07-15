// KeyObtain.h : KeyObtain DLL 的主头文件
//

#pragma once

#ifdef DLL_EXPORT
#define DLL_EXPORT __declspec(dllexport)
#else
#define DLL_EXPORT __declspec(dllimport)
#endif

// KeyObtain
// 有关此类实现的信息，请参阅 KeyObtain.cpp
//

class DLL_EXPORT KeyObtain
{

public:
	char* GetCPUID();
	char* GetHDSN();
	char* GetMAC();
	char* GetAllMACs();
	char* GetBIOSSN();
	char* GetHostName();
	char* GetHostID();
	char* GetKeyString(char *szDesKey=NULL);
	char* CheckHostKey(char *szHostKey);
};
