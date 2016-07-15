#pragma once
class Decryption
{
public:
	Decryption(void);
	~Decryption(void);

	CString DecryString(CString strCiphertext);
};

