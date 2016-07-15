#pragma once
class HardwareInfo
{
public:
	HardwareInfo(void);
	~HardwareInfo(void);

	CString getCPUID();
	CString getHDSN();
	CString getBIOSSN();
	CString getHostName();
	UINT getMAC(CStringArray& arrMACs);
};

