// RSA.h: interface for the CRSA class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_RSA_H__A0CC2413_F410_45CE_911B_7A21D7A5155B__INCLUDED_)
#define AFX_RSA_H__A0CC2413_F410_45CE_911B_7A21D7A5155B__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

//////////////////////////////////////////////////////////////////////
#define BI_MAXLEN   1200
#define BI_BASE     16

//////////////////////////////////////////////////////////////////////
struct bigint
{
	char	bit[BI_MAXLEN];//��λ��Ӧ��λ����λ��Ӧ��λ
	UINT	len; //��������
};
typedef struct bigint BigInt; 

//////////////////////////////////////////////////////////////////////
//�����㷨��(class CBigInt)
class CBigInt
{
public:
	CBigInt();
	bool    GetPrime(BigInt &P,UINT len);//��ȡ���������
	bool    PowMod(BigInt &C,BigInt &A,BigInt &B,BigInt &N);//�����˷�ȡģ
	bool	Inverse(BigInt &A,BigInt &X,BigInt &N,BigInt &Y);//��˷���Ԫ
	bool    Mul(BigInt &C,BigInt &A,BigInt &B);//�����˷�
	bool	Div(BigInt &M,BigInt &Spls,BigInt &A,BigInt &B);//��������
	bool    Add(BigInt &C,BigInt &A,BigInt &B);//�����ӷ�
	bool    Sub(BigInt &C,BigInt &A,const BigInt &B);//��������
	bool    ShR(BigInt &Out,BigInt &In,UINT len);//�����߼�����
	int     Cmp(const BigInt &A,const BigInt &B);//�����Ƚ�
	BigInt  New(long n);//����һ������
	void	SetVal(BigInt &BI,long n);//���ô�����ֵ
	bool    BuildBIFromByte(BigInt &Out,const char *In,UINT len);//�������ֽ��鹹��һ������
	bool    BuildBIFromHalfByte(BigInt &Out,const char *In,UINT len);//��������ֽ��鹹��һ������
	bool    BuildBIFromStr(BigInt &Out,char *In,UINT len);//�������ַ�������һ������
	BigInt  GetBIFromStr(CString strIn);
	bool    RandVal(BigInt &Out,UINT len);//�����������

private:
	bool	GetDivNext(BigInt &Spls,const BigInt &A,const BigInt &B,int &i);
	bool    InitMulCache(BigInt &In);//��ʼ���˷�����
	void    SetLen(BigInt &BI,UINT start);//���ô����ĳ���

private:
	BigInt	MulCache[ BI_BASE ];//�˷�������
	BigInt   Zero,One,Two;
};

//////////////////////////////////////////////////////////////////////
//ͨ�ÿ�(class CGfL)
class CGfL
{
public:
	static  bool ByteToBit(bool *Out,const char *In,UINT len,UINT num=8);//�ֽ���ת����λ��
	static  bool BitToByte(char *Out,const bool *In,UINT len,UINT num=8);//λ��ת�����ֽ���
	static  bool HalfByteToByte(char *Out,const char *In,UINT len);//���ֽ���ת�����ֽ���
	static  bool ByteToHalfByte(char *Out,const char *In,UINT len);//�ֽ���ת���ɰ��ֽ���
	static  int  StrToHalfByte(char *Out,char *In,UINT len);//�ַ���ת���ɰ��ֽ���
	static  int  HalfByteToStr(char *Out,char *In,UINT len);//���ֽ���ת�����ַ���

	//����:Bytes��Bits��ת��,
	//����:���任�ַ���,���������Ż�����ָ��,Bits��������С
	static void Bytes2Bits(char *srcBytes, char* dstBits, unsigned int sizeBits);

	//����:Bits��Bytes��ת��,
	//����:���任�ַ���,���������Ż�����ָ��,Bits��������С
	static void Bits2Bytes(char *dstBytes, char* srcBits, unsigned int sizeBits);

	//����:Int��Bits��ת��,
	//����:���任�ַ���,���������Ż�����ָ��
	static void Int2Bits(unsigned int srcByte, char* dstBits);

	//����:Bits��Hex��ת��
	//����:���任�ַ���,���������Ż�����ָ��,Bits��������С
	static void Bits2Hex(char *dstHex, char* srcBits, unsigned int sizeBits);

	//����:Bits��Hex��ת��
	//����:���任�ַ���,���������Ż�����ָ��,Bits��������С
	static void Hex2Bits(char *srcHex, char* dstBits, unsigned int sizeBits);

	static char ConvertHexChar(char ch);

	 /**
     * @notes ʮ�������ַ���ת��Ϊ�ֽ�����
     * @param str
     * @return byte[]
     */
	static bool HexStrToBytes(CString strHexSrc, char *bytes, UINT lenbytes);

	/**
     * @notes �ֽ�����ת��Ϊʮ�������ַ���
     * @param bytes
     * @return String
     */
	static CString BytesToHexStr(char *bytes, int lenbytes);
};

class CRsa
{
public:
	CRsa();
	CString Encrypt(CString In, CString strKey, CString strMod);//����ȫ���ַ�
	CString Decrypt(CString In, CString strKey, CString strMod);//����ȫ���ַ�

	int 	EncDecBlock(char *Out,char *In,UINT lenIn);//�ӽ��ܿ�

	bool    GetKey(BigInt &p,BigInt &q,BigInt &e,BigInt &d,BigInt &n,
		UINT plen,UINT qlen,UINT elen);//��ȡRSA��Կ��
	bool    SetKey(char *KeyStr,char *ModStr);//��������Կ������RSA��Կ��ģn

private:
	BigInt  key,n,Zero;//��Կ��ģn������0
	CBigInt BI;
};

#endif // !defined(AFX_RSA_H__A0CC2413_F410_45CE_911B_7A21D7A5155B__INCLUDED_)
