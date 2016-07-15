#include "stdafx.h"
#include "MD5.h"
#include <fstream>
#include <strstream>

#ifdef _DEBUG
#undef THIS_FILE
static char THIS_FILE[]=__FILE__;
#define new DEBUG_NEW
#endif

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////
//初始化变量值
CMD5::CMD5():m_bAddData(false)
{
	//对四个32位的变量进行初始化
	m_auiBuf[0] = 0x67452301;
	m_auiBuf[1] = 0xefcdab89;
	m_auiBuf[2] = 0x98badcfe;
	m_auiBuf[3] = 0x10325476;
	m_auiBits[0] = 0;
	m_auiBits[1] = 0;
}

CMD5::~CMD5()
{
}
void CMD5::AddData(char const* pcData, int iDataLength)
{
	if(iDataLength < 0) TRACE("MD5文件加密失败!,数据的长度必须大于0");

	unsigned int uiT;
	//更新位长度
	uiT = m_auiBits[0];
	//参考MD5Update函数
	if((m_auiBits[0] = uiT + ((unsigned int)iDataLength << 3)) < uiT)
		m_auiBits[1]++; 
	m_auiBits[1] += iDataLength >> 29;
	uiT = (uiT >> 3) & (BLOCKSIZE-1); //转化为字节
	//处理奇数块
	if(uiT != 0)
	{
		unsigned char *puc = (unsigned char *)m_aucIn + uiT;
		uiT = BLOCKSIZE - uiT;
		if(iDataLength < uiT)
		{
			memcpy(puc, pcData, iDataLength);
			return;
		}
		memcpy(puc, pcData, uiT);
		MD5Transform();
		pcData += uiT;
		iDataLength -= uiT;
	}
	//处理长度为64字节数据块
	while(iDataLength >= BLOCKSIZE)
	{
		memcpy(m_aucIn, pcData, BLOCKSIZE);
		MD5Transform();
		pcData += BLOCKSIZE;
		iDataLength -= BLOCKSIZE;
	}
	//处理余下的数据
	memcpy(m_aucIn, pcData, iDataLength);
	//设置标志位
	m_bAddData = true;
}
void CMD5::FinalDigest(char* pcDigest)
{
	if(m_bAddData == false)	TRACE("MD5文件加密失败!,没有数据被添加");

	unsigned int uiCount;
	unsigned char* puc;
	//参考MD5Update函数
	//计算字节对64取余运算
	uiCount = (m_auiBits[0] >> 3) & (BLOCKSIZE-1);
	//参考static unsigned char PADDING[64]~~即对对一个填充位设置为0x80
	puc = m_aucIn + uiCount;
	*puc++ = 0x80;
	//同时要通过填充来达到64个字节
	uiCount = BLOCKSIZE - uiCount - 1;
	//填充到64位长度
	//假如填充的长度未达到64位长度
	if(uiCount < 8)
	{
		//首先要填充前面64个字节
		memset(puc, 0, uiCount);
		MD5Transform();
		//然后要填充后面的56个字节
		memset(m_aucIn, 0, BLOCKSIZE-8);
	}
	//已经达到64位
	else
	{
		//直接填充56个字节
		memset(puc, 0, uiCount - 8);
	}
	//添加位长度同时进行MD5变换
	((unsigned int*)m_aucIn)[(BLOCKSIZE>>2)-2] = m_auiBits[0];
	((unsigned int*)m_aucIn)[(BLOCKSIZE>>2)-1] = m_auiBits[1];
	MD5Transform();
	memcpy(pcDigest, m_auiBuf, MD128LENGTH<<2);
	//重新设置大小
	Reset();
}
void CMD5::Reset()
{
	//对四个32位的变量重新设置
	m_auiBuf[0] = 0x67452301;
	m_auiBuf[1] = 0xefcdab89;
	m_auiBuf[2] = 0x98badcfe;
	m_auiBuf[3] = 0x10325476;
	m_auiBits[0] = 0;
	m_auiBits[1] = 0;
	//重设标志
	m_bAddData = false;
}

//MD5四轮变换算法,具体的64次运算过程如下
void CMD5::MD5Transform()
{
	unsigned int* puiIn = (unsigned int*)m_aucIn;
	register unsigned int a, b, c, d;
	a = m_auiBuf[0];
	b = m_auiBuf[1];
	c = m_auiBuf[2];
	d = m_auiBuf[3];
	//第一轮
	MD5STEP(F, a, b, c, d, puiIn[0] + 0xd76aa478, 7);
	MD5STEP(F, d, a, b, c, puiIn[1] + 0xe8c7b756, 12);
	MD5STEP(F, c, d, a, b, puiIn[2] + 0x242070db, 17);
	MD5STEP(F, b, c, d, a, puiIn[3] + 0xc1bdceee, 22);
	MD5STEP(F, a, b, c, d, puiIn[4] + 0xf57c0faf, 7);
	MD5STEP(F, d, a, b, c, puiIn[5] + 0x4787c62a, 12);
	MD5STEP(F, c, d, a, b, puiIn[6] + 0xa8304613, 17);
	MD5STEP(F, b, c, d, a, puiIn[7] + 0xfd469501, 22);
	MD5STEP(F, a, b, c, d, puiIn[8] + 0x698098d8, 7);
	MD5STEP(F, d, a, b, c, puiIn[9] + 0x8b44f7af, 12);
	MD5STEP(F, c, d, a, b, puiIn[10] + 0xffff5bb1, 17);
	MD5STEP(F, b, c, d, a, puiIn[11] + 0x895cd7be, 22);
	MD5STEP(F, a, b, c, d, puiIn[12] + 0x6b901122, 7);
	MD5STEP(F, d, a, b, c, puiIn[13] + 0xfd987193, 12);
	MD5STEP(F, c, d, a, b, puiIn[14] + 0xa679438e, 17);
	MD5STEP(F, b, c, d, a, puiIn[15] + 0x49b40821, 22);
	//第二轮
	MD5STEP(G, a, b, c, d, puiIn[1] + 0xf61e2562, 5);
	MD5STEP(G, d, a, b, c, puiIn[6] + 0xc040b340, 9);
	MD5STEP(G, c, d, a, b, puiIn[11] + 0x265e5a51, 14);
	MD5STEP(G, b, c, d, a, puiIn[0] + 0xe9b6c7aa, 20);
	MD5STEP(G, a, b, c, d, puiIn[5] + 0xd62f105d, 5);
	MD5STEP(G, d, a, b, c, puiIn[10] + 0x02441453, 9);
	MD5STEP(G, c, d, a, b, puiIn[15] + 0xd8a1e681, 14);
	MD5STEP(G, b, c, d, a, puiIn[4] + 0xe7d3fbc8, 20);
	MD5STEP(G, a, b, c, d, puiIn[9] + 0x21e1cde6, 5);
	MD5STEP(G, d, a, b, c, puiIn[14] + 0xc33707d6, 9);
	MD5STEP(G, c, d, a, b, puiIn[3] + 0xf4d50d87, 14);
	MD5STEP(G, b, c, d, a, puiIn[8] + 0x455a14ed, 20);
	MD5STEP(G, a, b, c, d, puiIn[13] + 0xa9e3e905, 5);
	MD5STEP(G, d, a, b, c, puiIn[2] + 0xfcefa3f8, 9);
	MD5STEP(G, c, d, a, b, puiIn[7] + 0x676f02d9, 14);
	MD5STEP(G, b, c, d, a, puiIn[12] + 0x8d2a4c8a, 20);
	//第三轮
	MD5STEP(H, a, b, c, d, puiIn[5] + 0xfffa3942, 4);
	MD5STEP(H, d, a, b, c, puiIn[8] + 0x8771f681, 11);
	MD5STEP(H, c, d, a, b, puiIn[11] + 0x6d9d6122, 16);
	MD5STEP(H, b, c, d, a, puiIn[14] + 0xfde5380c, 23);
	MD5STEP(H, a, b, c, d, puiIn[1] + 0xa4beea44, 4);
	MD5STEP(H, d, a, b, c, puiIn[4] + 0x4bdecfa9, 11);
	MD5STEP(H, c, d, a, b, puiIn[7] + 0xf6bb4b60, 16);
	MD5STEP(H, b, c, d, a, puiIn[10] + 0xbebfbc70, 23);
	MD5STEP(H, a, b, c, d, puiIn[13] + 0x289b7ec6, 4);
	MD5STEP(H, d, a, b, c, puiIn[0] + 0xeaa127fa, 11);
	MD5STEP(H, c, d, a, b, puiIn[3] + 0xd4ef3085, 16);
	MD5STEP(H, b, c, d, a, puiIn[6] + 0x04881d05, 23);
	MD5STEP(H, a, b, c, d, puiIn[9] + 0xd9d4d039, 4);
	MD5STEP(H, d, a, b, c, puiIn[12] + 0xe6db99e5, 11);
	MD5STEP(H, c, d, a, b, puiIn[15] + 0x1fa27cf8, 16);
	MD5STEP(H, b, c, d, a, puiIn[2] + 0xc4ac5665, 23);
	//第四轮
	MD5STEP(I, a, b, c, d, puiIn[0] + 0xf4292244, 6);
	MD5STEP(I, d, a, b, c, puiIn[7] + 0x432aff97, 10);
	MD5STEP(I, c, d, a, b, puiIn[14] + 0xab9423a7, 15);
	MD5STEP(I, b, c, d, a, puiIn[5] + 0xfc93a039, 21);
	MD5STEP(I, a, b, c, d, puiIn[12] + 0x655b59c3, 6);
	MD5STEP(I, d, a, b, c, puiIn[3] + 0x8f0ccc92, 10);
	MD5STEP(I, c, d, a, b, puiIn[10] + 0xffeff47d, 15);
	MD5STEP(I, b, c, d, a, puiIn[1] + 0x85845dd1, 21);
	MD5STEP(I, a, b, c, d, puiIn[8] + 0x6fa87e4f, 6);
	MD5STEP(I, d, a, b, c, puiIn[15] + 0xfe2ce6e0, 10);
	MD5STEP(I, c, d, a, b, puiIn[6] + 0xa3014314, 15);
	MD5STEP(I, b, c, d, a, puiIn[13] + 0x4e0811a1, 21);
	MD5STEP(I, a, b, c, d, puiIn[4] + 0xf7537e82, 6);
	MD5STEP(I, d, a, b, c, puiIn[11] + 0xbd3af235, 10);
	MD5STEP(I, c, d, a, b, puiIn[2] + 0x2ad7d2bb, 15);
	MD5STEP(I, b, c, d, a, puiIn[9] + 0xeb86d391, 21);
	//将ABCD分别加上abcd,
	m_auiBuf[0] += a;
	m_auiBuf[1] += b;
	m_auiBuf[2] += c;
	m_auiBuf[3] += d;
}
