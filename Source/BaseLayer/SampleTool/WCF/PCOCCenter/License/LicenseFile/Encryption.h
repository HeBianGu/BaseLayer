#pragma once

class Encryption
{
public:
	Encryption(void);
	~Encryption(void);

	CString EncryString(CString strOriginal);
};

