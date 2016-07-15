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
	char	bit[BI_MAXLEN];//低位对应低位，高位对应高位
	UINT	len; //大数长度
};
typedef struct bigint BigInt; 

//////////////////////////////////////////////////////////////////////
//大数算法库(class CBigInt)
class CBigInt
{
public:
	CBigInt();
	bool    GetPrime(BigInt &P,UINT len);//获取随机大素数
	bool    PowMod(BigInt &C,BigInt &A,BigInt &B,BigInt &N);//大数乘方取模
	bool	Inverse(BigInt &A,BigInt &X,BigInt &N,BigInt &Y);//求乘法逆元
	bool    Mul(BigInt &C,BigInt &A,BigInt &B);//大数乘法
	bool	Div(BigInt &M,BigInt &Spls,BigInt &A,BigInt &B);//大数除法
	bool    Add(BigInt &C,BigInt &A,BigInt &B);//大数加法
	bool    Sub(BigInt &C,BigInt &A,const BigInt &B);//大数减法
	bool    ShR(BigInt &Out,BigInt &In,UINT len);//大数逻辑右移
	int     Cmp(const BigInt &A,const BigInt &B);//大数比较
	BigInt  New(long n);//产生一个大数
	void	SetVal(BigInt &BI,long n);//设置大数的值
	bool    BuildBIFromByte(BigInt &Out,const char *In,UINT len);//由输入字节组构造一个大数
	bool    BuildBIFromHalfByte(BigInt &Out,const char *In,UINT len);//由输入半字节组构造一个大数
	bool    BuildBIFromStr(BigInt &Out,char *In,UINT len);//由输入字符串构造一个大数
	BigInt  GetBIFromStr(CString strIn);
	bool    RandVal(BigInt &Out,UINT len);//产生随机大数

private:
	bool	GetDivNext(BigInt &Spls,const BigInt &A,const BigInt &B,int &i);
	bool    InitMulCache(BigInt &In);//初始化乘法缓存
	void    SetLen(BigInt &BI,UINT start);//设置大数的长度

private:
	BigInt	MulCache[ BI_BASE ];//乘法缓冲区
	BigInt   Zero,One,Two;
};

//////////////////////////////////////////////////////////////////////
//通用库(class CGfL)
class CGfL
{
public:
	static  bool ByteToBit(bool *Out,const char *In,UINT len,UINT num=8);//字节组转换成位组
	static  bool BitToByte(char *Out,const bool *In,UINT len,UINT num=8);//位组转换成字节组
	static  bool HalfByteToByte(char *Out,const char *In,UINT len);//半字节组转换成字节组
	static  bool ByteToHalfByte(char *Out,const char *In,UINT len);//字节组转换成半字节组
	static  int  StrToHalfByte(char *Out,char *In,UINT len);//字符串转换成半字节组
	static  int  HalfByteToStr(char *Out,char *In,UINT len);//半字节组转换成字符串

	//功能:Bytes到Bits的转换,
	//参数:待变换字符串,处理后结果存放缓冲区指针,Bits缓冲区大小
	static void Bytes2Bits(char *srcBytes, char* dstBits, unsigned int sizeBits);

	//功能:Bits到Bytes的转换,
	//参数:待变换字符串,处理后结果存放缓冲区指针,Bits缓冲区大小
	static void Bits2Bytes(char *dstBytes, char* srcBits, unsigned int sizeBits);

	//功能:Int到Bits的转换,
	//参数:待变换字符串,处理后结果存放缓冲区指针
	static void Int2Bits(unsigned int srcByte, char* dstBits);

	//功能:Bits到Hex的转换
	//参数:待变换字符串,处理后结果存放缓冲区指针,Bits缓冲区大小
	static void Bits2Hex(char *dstHex, char* srcBits, unsigned int sizeBits);

	//功能:Bits到Hex的转换
	//参数:待变换字符串,处理后结果存放缓冲区指针,Bits缓冲区大小
	static void Hex2Bits(char *srcHex, char* dstBits, unsigned int sizeBits);

	static char ConvertHexChar(char ch);

	 /**
     * @notes 十六进制字符串转化为字节数组
     * @param str
     * @return byte[]
     */
	static bool HexStrToBytes(CString strHexSrc, char *bytes, UINT lenbytes);

	/**
     * @notes 字节数组转化为十六进制字符串
     * @param bytes
     * @return String
     */
	static CString BytesToHexStr(char *bytes, int lenbytes);
};

class CRsa
{
public:
	CRsa();
	CString Encrypt(CString In, CString strKey, CString strMod);//加密全部字符
	CString Decrypt(CString In, CString strKey, CString strMod);//解密全部字符

	int 	EncDecBlock(char *Out,char *In,UINT lenIn);//加解密块

	bool    GetKey(BigInt &p,BigInt &q,BigInt &e,BigInt &d,BigInt &n,
		UINT plen,UINT qlen,UINT elen);//获取RSA密钥对
	bool    SetKey(char *KeyStr,char *ModStr);//由输入密钥串设置RSA密钥和模n

private:
	BigInt  key,n,Zero;//密钥，模n，常量0
	CBigInt BI;
};

#endif // !defined(AFX_RSA_H__A0CC2413_F410_45CE_911B_7A21D7A5155B__INCLUDED_)
