#pragma once
class CEncDec
{
public:
	CEncDec(void);
	~CEncDec(void);

	static CString EncryString(CString strOriginal);
	static CString DecryString(CString strCiphertext);
};

