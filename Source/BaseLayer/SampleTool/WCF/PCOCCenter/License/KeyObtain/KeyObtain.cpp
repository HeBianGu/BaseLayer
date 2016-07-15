// KeyObtain.cpp : 定义 DLL 的初始化例程。
//

#include "stdafx.h"

#include "KeyObtain.h"

#include "DES.h"
#include "HardwareInfo.h"

char buff[1024];

char* KeyObtain::GetMAC()
{
	memset(buff, 0, sizeof(BYTE)*1024);

	HardwareInfo hdi;

	CString strMAC = "00-00-00-00-00-00";
	CStringArray arrMACs;
	UINT ret = hdi.getMAC(arrMACs);
	if(arrMACs.GetCount()>0) strMAC = arrMACs[0];

	strcpy(buff, strMAC);
	return buff;
}

char* KeyObtain::GetAllMACs()
{
	memset(buff, 0, sizeof(BYTE)*1024);

	HardwareInfo hdi;

	CString strMAC = "00-00-00-00-00-00";
	CString strAllMACs = "";
	CStringArray arrMACs;
	UINT ret = hdi.getMAC(arrMACs);
	for(int i=0; i<arrMACs.GetCount(); i++)
	{
		strMAC = arrMACs[i];
		strAllMACs += strMAC;
		if(i<arrMACs.GetCount()-1) strAllMACs += "|";
	}

	if(strAllMACs == "") strAllMACs = strMAC;

	strcpy(buff, strAllMACs);
	return buff;
}

char* KeyObtain::GetCPUID()
{
	memset(buff, 0, sizeof(BYTE)*1024);

	HardwareInfo hdi;
	strcpy(buff, hdi.getCPUID());
	return buff;
}

char* KeyObtain::GetHDSN()
{
	memset(buff, 0, sizeof(BYTE)*1024);

	HardwareInfo hdi;
	CString hdsn = hdi.getHDSN();
	hdsn = hdsn.Trim();
	strcpy(buff, hdsn);
	return buff;
}

char* KeyObtain::GetBIOSSN()
{
	memset(buff, 0, sizeof(BYTE)*1024);

	HardwareInfo hdi;
	strcpy(buff, hdi.getBIOSSN());
	return buff;
}

char* KeyObtain::GetHostName()
{
	memset(buff, 0, sizeof(BYTE)*1024);

	HardwareInfo hdi;
	strcpy(buff, hdi.getHostName());
	return buff;
}

#include <set> 
long randX()
{
	std::set<int> s; 

	srand((unsigned)time(NULL));

	while(1) 
	{ 
		int r = rand() % 99; 
		s.insert(r); 
		if(s.size() == 6) 
		{ 
			break; 
		} 
	}

	long randx = 0;
	std::set<int>::const_iterator b=s.begin();
	int mul=1;
	for(; b!=s.end(); ++b)
	{
		randx += *b * mul;
		mul *=10;
	}

	return randx;
}

char* KeyObtain::GetHostID()
{
	memset(buff, 0, sizeof(BYTE)*1024);

	HardwareInfo hdi;
	CString strCPUID = hdi.getCPUID();
	strCPUID = strCPUID.Trim();
	CString strMAC = "00-00-00-00-00-00";
	CStringArray arrMACs;
	UINT ret = hdi.getMAC(arrMACs);
	if(arrMACs.GetCount()>0) strMAC = arrMACs[0];
	strMAC = strMAC.Trim();

	CString hostID = strCPUID.Left(2)+strMAC.Right(2);
	strcpy(buff, hostID);

	return buff;
}

char* KeyObtain::GetKeyString(char *szDesKey)
{
	memset(buff, 0, sizeof(BYTE)*1024);

	HardwareInfo hdi;
	CString strHostName = hdi.getHostName();
	CString strCPUID = hdi.getCPUID();
	CString strHDSN = hdi.getHDSN();
	strHDSN = strHDSN.Trim();
	CString strMAC = "00-00-00-00-00-00";
	CStringArray arrMACs;

	if(szDesKey==NULL)
	{
		UINT ret = hdi.getMAC(arrMACs);
		if(arrMACs.GetCount()>0) strMAC = arrMACs[0];
	}

	long rand = randX();

	CString randKey;

	if(szDesKey==NULL)
	{
		randKey.Format("%04X", rand);
	}
	else
	{
		randKey = szDesKey;
		if(randKey.GetLength()<4)
			randKey += "PEOffice";
	}
	CString desKey4 = randKey.Right(4);
	CString desKey  = desKey4+desKey4;

	CDES oDes;

	CString sKey1 = oDes.EncryString(strHDSN, desKey);
	CString sKey2 = oDes.EncryString(strCPUID, desKey);

	CString KeyString = desKey4 + sKey1.Left(4) + sKey2.Left(4);

	for(int i=0; i<arrMACs.GetCount(); i++)
	{
		strMAC = arrMACs[i];
		CString sMacKey = oDes.EncryString(strMAC, desKey);
		KeyString += sMacKey.Left(4);
	}

	strcpy(buff, KeyString);

	return buff;
}

char* KeyObtain::CheckHostKey(char *szHostKey)
{
	memset(buff, 0, sizeof(BYTE)*1024);

	CString sRet = "failed";
	CString strHostKey = szHostKey;

	HardwareInfo hdi;
	CString strHostName = hdi.getHostName();
	CString strCPUID = hdi.getCPUID();
	CString strHDSN = hdi.getHDSN();
	strHDSN = strHDSN.Trim();
	CString strMAC = "00-00-00-00-00-00";
	CStringArray arrMACs;
	UINT ret = hdi.getMAC(arrMACs);
	
	CString desKey4 = strHostKey.Left(4);
	CString desKey = desKey4+desKey4;

	CDES oDes;

	CString sKey1 = oDes.EncryString(strHDSN, desKey);
	CString sKey2 = oDes.EncryString(strCPUID, desKey);

	if(strHostKey.Find(sKey1.Left(4))>=0 && strHostKey.Find(sKey2.Left(4))>0)
	{
		for(int i=0; i<arrMACs.GetCount(); i++)
		{
			strMAC = arrMACs[i];
			CString sMacKey = oDes.EncryString(strMAC, desKey);

			if(strHostKey.Find(sMacKey.Left(4))>0)
			{
				sRet = "success";
				break;
			}
		}
	}
	strcpy(buff, sRet);
	return buff;
}
