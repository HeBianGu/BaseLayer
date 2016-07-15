#include "StdAfx.h"
#include "Des.h"
#include "RSA.h"
#include "EncDec.h"
#include "Decryption.h"


Decryption::Decryption(void)
{
}


Decryption::~Decryption(void)
{
}

CString Decryption::DecryString(CString strCiphertext)
{
//	return strCiphertext;
	CDES oDES;
	CString strOriginal = oDES.DecryString(CEncDec::DecryString(strCiphertext), "OPT.PEOffice");

	// RSAΩ‚√‹
	CBigInt BI;
	CRsa rsa;
	char * ModStr = ("12EAC9A20E9887268287E60CAD804902919CA5E58140B1D332535008A6C495D8F4A66AA236A536AE815FF684C4AFE38E1F9D1D60731D10E96EDD78575992CDF7");
	char * KeyStr = ("3195F90EDA618BC4248FE1F0A812189287DC0306E0A764CB158C3D66840F88960E089DC3C92147C06710DBF4040BC21DBF358892AB0002562921697DB6CE3117");
	strOriginal = rsa.Decrypt(strOriginal, KeyStr, ModStr);

	return strOriginal;
}