// MD5.h: interface for the CMD5 class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_MD5_H__EA6A200B_1336_43F3_B866_2A2E28D54560__INCLUDED_)
#define AFX_MD5_H__EA6A200B_1336_43F3_B866_2A2E28D54560__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

//测试判别:摘自http://www.ietf.org/rfc/rfc1321.txt
/*
MD5 ("") = d41d8cd98f00b204e9800998ecf8427e
MD5 ("a") = 0cc175b9c0f1b6a831c399e269772661
MD5 ("abc") = 900150983cd24fb0d6963f7d28e17f72
MD5 ("message digest") = f96b697d7cb7938d525a2f31aaf161d0
MD5 ("abcdefghijklmnopqrstuvwxyz") = c3fcd3d76192e4007dfb496cca67e13b
MD5 ("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789") =
d174ab98d277d9f5a5611c2c9f419d9f
MD5 ("123456789012345678901234567890123456789012345678901234567890123456
78901234567890") = 57edf4a22be3c955ac49da2e2107b67a
*/
//补充MD5算法是不可逆的算法,也就是说不存在解密,惟有暴力解密才可以解密。 该算法主要是对摘要进行加密
//主要是用于用户口令的加密,例如:UNIX里面用户口令保存都是也MD5加密后进行存储,当用户登录时,首先要对输
//入的口令进行MD5进行加密,然后再和存储的MD5加密用户口令进行比较
#include <string>
using namespace std;

class CMD5
{
public:
	//CONSTRUCTOR
	CMD5();
	//对基类的虚函数进行实现
	void AddData(char const* pcData, int iDataLength);
	void FinalDigest(char* pcDigest);
	void Reset();
	virtual ~CMD5();
protected:
	//设置附上64位的消息长度
	enum { BLOCKSIZE=64 };
	BOOL m_bAddData;
private: 
	//总共要经过4轮变换
	enum { MD128LENGTH=4 };
	//设定每次读取文件长度长度为1024位,数据长度为384(可以i自己设定)
	enum { DATA_LEN=384, BUFF_LEN=1024 };
	//四个32-位的变量,数值必须为正整数,就是A,B,C,D
	unsigned int m_auiBuf[4];
	unsigned int m_auiBits[2];
	//获取M0-〉M64消息的64个子分组 
	unsigned char m_aucIn[64];

	//主循环要经过四轮变换，第一轮要经过16次操作，都是由下面的基本运算组成
	static unsigned int F(unsigned int x, unsigned int y, unsigned int z);
	static unsigned int G(unsigned int x, unsigned int y, unsigned int z);
	static unsigned int H(unsigned int x, unsigned int y, unsigned int z);
	static unsigned int I(unsigned int x, unsigned int y, unsigned int z);

	//对FF,GG,HH,II函数进行定义
	//具体的参数说明:第一个参数为:F1、F2、F3、F4方法的选择，第二个参数为:运算的结果,即初始化的参数
	//第三、四、五个参数为初始化的参数,具体为a,b,c,d中的不同的三个,第六个参数为:消息的第J个子分组,第七个参数为循环左移S位
	static void MD5STEP(unsigned int (*f)(unsigned int x, unsigned int y, unsigned int z),
		unsigned int& w, unsigned int x, unsigned int y, unsigned int z, unsigned int data, unsigned int s);
	//MD5四轮变换算法，具体是对四轮中的每一轮进行16次运算
	void MD5Transform();
};
//具体的函数可以参考http://www.ietf.org/rfc/rfc1321.txt,The MD5 Message-Digest Algorithm(MD5摘要加密算法)
//第一个非线性函数，即所谓的F函数:F(X,Y,Z) = (X&Y)|((~X)&Z)
inline unsigned int CMD5::F(unsigned int x, unsigned int y, unsigned int z)
{
	return (x & y | ~x & z);
}
//第二个非线性函数，即所谓的G函数:G(X,Y,Z) = (X&Z)|(Y&(~Z))
inline unsigned int CMD5::G(unsigned int x, unsigned int y, unsigned int z)
{
	return F(z, x, y);
}
//第三个非线性函数，即所谓的H函数:H(X,Y,Z) = X XOR Y XOR Z
inline unsigned int CMD5::H(unsigned int x, unsigned int y, unsigned int z)
{
	return x ^ y ^ z;
} 
//第四个非线性函数，即所谓的I函数:I(X,Y,Z) = Y XOR (X | (~Z))
inline unsigned int CMD5::I(unsigned int x, unsigned int y, unsigned int z)
{
	return (y ^ (x | ~z));
}
//
inline void CMD5::MD5STEP(unsigned int (*f)(unsigned int x, unsigned int y, unsigned int z),
	unsigned int& w, unsigned int x, unsigned int y, unsigned int z, unsigned int data, unsigned int s)
{
	w += f(x, y, z) + data;
	w = w << s | w >> (32-s);
	w += x;
}
#endif // !defined(AFX_MD5_H__EA6A200B_1336_43F3_B866_2A2E28D54560__INCLUDED_)

