// MD5.h: interface for the CMD5 class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_MD5_H__EA6A200B_1336_43F3_B866_2A2E28D54560__INCLUDED_)
#define AFX_MD5_H__EA6A200B_1336_43F3_B866_2A2E28D54560__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

//�����б�:ժ��http://www.ietf.org/rfc/rfc1321.txt
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
//����MD5�㷨�ǲ�������㷨,Ҳ����˵�����ڽ���,Ω�б������ܲſ��Խ��ܡ� ���㷨��Ҫ�Ƕ�ժҪ���м���
//��Ҫ�������û�����ļ���,����:UNIX�����û�����涼��ҲMD5���ܺ���д洢,���û���¼ʱ,����Ҫ����
//��Ŀ������MD5���м���,Ȼ���ٺʹ洢��MD5�����û�������бȽ�
#include <string>
using namespace std;

class CMD5
{
public:
	//CONSTRUCTOR
	CMD5();
	//�Ի�����麯������ʵ��
	void AddData(char const* pcData, int iDataLength);
	void FinalDigest(char* pcDigest);
	void Reset();
	virtual ~CMD5();
protected:
	//���ø���64λ����Ϣ����
	enum { BLOCKSIZE=64 };
	BOOL m_bAddData;
private: 
	//�ܹ�Ҫ����4�ֱ任
	enum { MD128LENGTH=4 };
	//�趨ÿ�ζ�ȡ�ļ����ȳ���Ϊ1024λ,���ݳ���Ϊ384(����i�Լ��趨)
	enum { DATA_LEN=384, BUFF_LEN=1024 };
	//�ĸ�32-λ�ı���,��ֵ����Ϊ������,����A,B,C,D
	unsigned int m_auiBuf[4];
	unsigned int m_auiBits[2];
	//��ȡM0-��M64��Ϣ��64���ӷ��� 
	unsigned char m_aucIn[64];

	//��ѭ��Ҫ�������ֱ任����һ��Ҫ����16�β���������������Ļ����������
	static unsigned int F(unsigned int x, unsigned int y, unsigned int z);
	static unsigned int G(unsigned int x, unsigned int y, unsigned int z);
	static unsigned int H(unsigned int x, unsigned int y, unsigned int z);
	static unsigned int I(unsigned int x, unsigned int y, unsigned int z);

	//��FF,GG,HH,II�������ж���
	//����Ĳ���˵��:��һ������Ϊ:F1��F2��F3��F4������ѡ�񣬵ڶ�������Ϊ:����Ľ��,����ʼ���Ĳ���
	//�������ġ��������Ϊ��ʼ���Ĳ���,����Ϊa,b,c,d�еĲ�ͬ������,����������Ϊ:��Ϣ�ĵ�J���ӷ���,���߸�����Ϊѭ������Sλ
	static void MD5STEP(unsigned int (*f)(unsigned int x, unsigned int y, unsigned int z),
		unsigned int& w, unsigned int x, unsigned int y, unsigned int z, unsigned int data, unsigned int s);
	//MD5���ֱ任�㷨�������Ƕ������е�ÿһ�ֽ���16������
	void MD5Transform();
};
//����ĺ������Բο�http://www.ietf.org/rfc/rfc1321.txt,The MD5 Message-Digest Algorithm(MD5ժҪ�����㷨)
//��һ�������Ժ���������ν��F����:F(X,Y,Z) = (X&Y)|((~X)&Z)
inline unsigned int CMD5::F(unsigned int x, unsigned int y, unsigned int z)
{
	return (x & y | ~x & z);
}
//�ڶ��������Ժ���������ν��G����:G(X,Y,Z) = (X&Z)|(Y&(~Z))
inline unsigned int CMD5::G(unsigned int x, unsigned int y, unsigned int z)
{
	return F(z, x, y);
}
//�����������Ժ���������ν��H����:H(X,Y,Z) = X XOR Y XOR Z
inline unsigned int CMD5::H(unsigned int x, unsigned int y, unsigned int z)
{
	return x ^ y ^ z;
} 
//���ĸ������Ժ���������ν��I����:I(X,Y,Z) = Y XOR (X | (~Z))
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

