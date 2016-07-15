#include "StdAfx.h"
#include "Des.h"
#include "RSA.h"
#include "EncDec.h"
#include "Encryption.h"


Encryption::Encryption(void)
{
}


Encryption::~Encryption(void)
{
}

CString Encryption::EncryString(CString strOriginal)
{
//	return strOriginal;

	// RSAº”√‹
	CBigInt BI;
	CRsa rsa;
	char * ModStr = ("12EAC9A20E9887268287E60CAD804902919CA5E58140B1D332535008A6C495D8F4A66AA236A536AE815FF684C4AFE38E1F9D1D60731D10E96EDD78575992CDF7");
	char * KeyStr = ("B1A0CC14474EFBA65180B63E9426DF861D05BA9B4A44C0C0CF6FBF8C53F788C710F070616771F0DCE908D58D7F24E04E13E9871F255F225057E3709971458F73");
	CString strCiphertext = rsa.Encrypt(strOriginal, KeyStr, ModStr);

	// DESº”√‹
	CDES des;
	strCiphertext = des.EncryString(CEncDec::EncryString(strCiphertext), "OPT.PEOffice");

	return strCiphertext;
}