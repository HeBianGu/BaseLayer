// RSA.cpp: implementation of the CRSA class.
//
//////////////////////////////////////////////////////////////////////

#include "stdafx.h"
#include "RSA.h"
#include <math.h>
#include <string.h>

#ifdef _DEBUG
#undef THIS_FILE
static char THIS_FILE[]=__FILE__;
#define new DEBUG_NEW
#endif
///////////////////////////////////////////////////////////////////////////////
// Construction
///////////////////////////////////////////////////////////////////////////////
CRsa::CRsa()
{
	Zero = BI.New(0);
	key = n = Zero;
}

///////////////////////////////////////////////////////////////////////////////
// CRsa Functions
///////////////////////////////////////////////////////////////////////////////
#define CHECK(x)			{if( !(x) ) return false;}
#define CHECK_MSG(x,msg)	{if( !(x) ){/*CWindow::ShowMessage(msg);*/return false;}}

//加密全部字符
CString CRsa::Encrypt(CString In, CString strKey, CString strMod)
{
	if(  SetKey( strKey.GetBuffer(), strMod.GetBuffer() ) == false )
		return "";

	CString strOut;
	char * buff = new char[n.len/2];

	UINT outLen = 0, inLen = In.GetLength();
	UINT pos=0, len = inLen>n.len/2?n.len/2:inLen;
	
	CString strBlock;
	// 循环加密
	while(inLen>=0)
	{
		strBlock = In.Left(len);

		if(len>=1 && strBlock[len-1] >= 0 && strBlock[len-1]  <= 127 ) //半角字符
		{
		}
		else
		{
			if(len>=2 && strBlock[len-2] >= 0 && strBlock[len-2]  <= 127 ) //半角字符
			{
				// 出现汉字截断 需要把len-1
				len = len-1;
				strBlock = In.Left(len);
			}
			else
			{
			}
		}

		In = In.Right(In.GetLength()-len);

		memset(buff, 0, sizeof(char)*n.len/2);
		outLen = EncDecBlock(buff, strBlock.GetBuffer(len), len);		
		strOut += CGfL::BytesToHexStr(buff, outLen);
		strOut += "@"; // 分段解密分割符
		inLen = In.GetLength();
		len = inLen>n.len/2?n.len/2:inLen;

		if(In == "") break;
	}

	delete[] buff;

	return strOut;
}

//解密全部字符
CString CRsa::Decrypt(CString In, CString strKey, CString strMod)
{
	if(  SetKey( strKey.GetBuffer(), strMod.GetBuffer() ) == false )
		return "";

	int bufflen = n.len/2+1;
	char * buffBytes = new char[bufflen];
	char * buffOuts = new char[bufflen];

	UINT outLen = 0, inLen = In.GetLength();
	
	CString strBlock, strOut;

	// 循环解密
	int nPos = In.Find("@");
	while(nPos>0)
	{
		strBlock = In.Left(nPos);
		In = In.Right(In.GetLength()-nPos-1);
		nPos = In.Find("@");

		memset(buffBytes, 0, sizeof(char)*bufflen);
		CGfL::HexStrToBytes(strBlock, buffBytes, bufflen);
		
		memset(buffOuts, 0, sizeof(char)*bufflen);
		outLen = EncDecBlock(buffOuts, buffBytes, bufflen);		
		strOut += buffOuts;		
	}

	delete[] buffBytes;
	delete[] buffOuts;

	return strOut;
}

/******************************************************************************/
//	名称：EncDecBlock
//	功能：加密解密块
//  参数：len为加密内容的长度；KeyStr,ModStr为0结尾的密钥串，用于设置RSA密钥和模n
//	返回：加密(解密)后的密文(明文)长度
/******************************************************************************/
int  CRsa::EncDecBlock(char *Out,char *In,UINT len)
{
	static BigInt  a,c;

	CHECK( Out && In && len )
	
	// 由输入构造一个大数a
	CHECK_MSG( BI.BuildBIFromByte(a,In,len) && a.len <= n.len, "错误：RSA加密(解密)的内容过长!" )
	// 加密(解密)
	CHECK( BI.PowMod(c,a,key,n) )
	CGfL::HalfByteToByte(Out,c.bit,c.len);

	return	(c.len+1)>>1;
}
/******************************************************************************/
//	名称：SetKey
//	功能：由输入密钥串设置RSA密钥和模n
//  参数：KeyStr,ModStr为0结尾的密钥串，对应密钥和模n
//	返回：设置成功返回true，否则返回false
/******************************************************************************/
bool CRsa::SetKey(char *KeyStr,char *ModStr)
{
	int		klen,nlen;

	key = n = Zero;
	CHECK_MSG( KeyStr && ModStr && KeyStr && (klen=strlen(KeyStr)) && (nlen=strlen(ModStr)) &&
		klen<=nlen && BI.BuildBIFromStr(key,KeyStr,klen) && BI.BuildBIFromStr(n,ModStr,nlen), 
		"设置RSA密钥出错：空密钥，或密钥太长!" )

		return true;
}
/******************************************************************************/
//	名称：GetKey
//	功能：获取RSA密钥对
//  参数：
//	返回：获取成功返回true，否则返回false
//  备注：如果p,q非空，请保证其为素数。如果需要产生素数p,q，请在调用前将p,q置0
/******************************************************************************/
bool CRsa::GetKey(BigInt &p,BigInt &q,BigInt &e,BigInt &d,BigInt &n,
	UINT plen,UINT qlen,UINT elen)

{
	BigInt  p_1,q_1,n_1,tmp;

	// 如果p,q=0，则产生素数
	if( !p.len )
		CHECK( BI.GetPrime(p,plen) )
		if( !q.len )
			CHECK( BI.GetPrime(q,qlen) )

			CHECK_MSG( p.len>4 && p.len<=BI_MAXLEN/4 && q.len>4 && q.len<=BI_MAXLEN/4 &&
			elen>=max(p.len,q.len) && elen<=p.len+q.len, "长度不在合法范围之内! " )
			CHECK_MSG( BI.Cmp(p,q), "错误：素数p,q相同!  " )
			// 计算n
			CHECK( BI.Mul(n,p,q) )
			// 防止密钥长度超过N，以免造成死循环
			if( elen>n.len )
				elen = n.len;
	p_1 = p; p_1.bit[0] -= 1;
	q_1 = q; q_1.bit[0] -= 1;
	CHECK( BI.Mul(n_1,p_1,q_1) )

		while( true )
		{
			BI.RandVal(e,elen);
			if( BI.Cmp(e,n)<0 && BI.Inverse(e,d,n_1,tmp) )
				return true;
		}
}

///////////////////////////////////////////////////////////////////////////////
// End of Files
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Little Primes
///////////////////////////////////////////////////////////////////////////////

#define PTL 550
const static int PrimeTable[PTL] =
{   3,    5,    7,    11,   13,   17,   19,   23,   29,   31,
37,   41,   43,   47,   53,   59,   61,   67,   71,   73,
79,   83,   89,   97,   101,  103,  107,  109,  113,  127, 
131,  137,  139,  149,  151,  157,  163,  167,  173,  179, 
181,  191,  193,  197,  199,  211,  223,  227,  229,  233, 
239,  241,  251,  257,  263,  269,  271,  277,  281,  283, 
293,  307,  311,  313,  317,  331,  337,  347,  349,  353, 
359,  367,  373,  379,  383,  389,  397,  401,  409,  419, 
421,  431,  433,  439,  443,  449,  457,  461,  463,  467, 
479,  487,  491,  499,  503,  509,  521,  523,  541,  547, 
557,  563,  569,  571,  577,  587,  593,  599,  601,  607, 
613,  617,  619,  631,  641,  643,  647,  653,  659,  661, 
673,  677,  683,  691,  701,  709,  719,  727,  733,  739, 
743,  751,  757,  761,  769,  773,  787,  797,  809,  811, 
821,  823,  827,  829,  839,  853,  857,  859,  863,  877,
881,  883,  887,  907,  911,  919,  929,  937,  941,  947, 
953,  967,  971,  977,  983,  991,  997,  1009, 1013, 1019, 
1021, 1031, 1033, 1039, 1049, 1051, 1061, 1063, 1069, 1087,
1091, 1093, 1097, 1103, 1109, 1117, 1123, 1129, 1151, 1153, 
1163, 1171, 1181, 1187, 1193, 1201, 1213, 1217, 1223, 1229, 
1231, 1237, 1249, 1259, 1277, 1279, 1283, 1289, 1291, 1297, 
1301, 1303, 1307, 1319, 1321, 1327, 1361, 1367, 1373, 1381,
1399, 1409, 1423, 1427, 1429, 1433, 1439, 1447, 1451, 1453, 
1459, 1471, 1481, 1483, 1487, 1489, 1493, 1499, 1511, 1523,
1531, 1543, 1549, 1553, 1559, 1567, 1571, 1579, 1583, 1597, 
1601, 1607, 1609, 1613, 1619, 1621, 1627, 1637, 1657, 1663, 
1667, 1669, 1693, 1697, 1699, 1709, 1721, 1723, 1733, 1741, 
1747, 1753, 1759, 1777, 1783, 1787, 1789, 1801, 1811, 1823, 
1831, 1847, 1861, 1867, 1871, 1873, 1877, 1879, 1889, 1901, 
1907, 1913, 1931, 1933, 1949, 1951, 1973, 1979, 1987, 1993, 
1997, 1999, 2003, 2011, 2017, 2027, 2029, 2039, 2053, 2063,
2069, 2081, 2083, 2087, 2089, 2099, 2111, 2113, 2129, 2131, 
2137, 2141, 2143, 2153, 2161, 2179, 2203, 2207, 2213, 2221, 
2237, 2239, 2243, 2251, 2267, 2269, 2273, 2281, 2287, 2293,
2297, 2309, 2311, 2333, 2339, 2341, 2347, 2351, 2357, 2371,
2377, 2381, 2383, 2389, 2393, 2399, 2411, 2417, 2423, 2437, 
2441, 2447, 2459, 2467, 2473, 2477, 2503, 2521, 2531, 2539, 
2543, 2549, 2551, 2557, 2579, 2591, 2593, 2609, 2617, 2621, 
2633, 2647, 2657, 2659, 2663, 2671, 2677, 2683, 2687, 2689, 
2693, 2699, 2707, 2711, 2713, 2719, 2729, 2731, 2741, 2749, 
2753, 2767, 2777, 2789, 2791, 2797, 2801, 2803, 2819, 2833, 
2837, 2843, 2851, 2857, 2861, 2879, 2887, 2897, 2903, 2909,
2917, 2927, 2939, 2953, 2957, 2963, 2969, 2971, 2999, 3001,
3011, 3019, 3023, 3037, 3041, 3049, 3061, 3067, 3079, 3083,
3089, 3109, 3119, 3121, 3137, 3163, 3167, 3169, 3181, 3187, 
3191, 3203, 3209, 3217, 3221, 3229, 3251, 3253, 3257, 3259, 
3271, 3299, 3301, 3307, 3313, 3319, 3323, 3329, 3331, 3343,
3347, 3359, 3361, 3371, 3373, 3389, 3391, 3407, 3413, 3433, 
3449, 3457, 3461, 3463, 3467, 3469, 3491, 3499, 3511, 3517, 
3527, 3529, 3533, 3539, 3541, 3547, 3557, 3559, 3571, 3581,
3583, 3593, 3607, 3613, 3617, 3623, 3631, 3637, 3643, 3659, 
3671, 3673, 3677, 3691, 3697, 3701, 3709, 3719, 3727, 3733, 
3739, 3761, 3767, 3769, 3779, 3793, 3797, 3803, 3821, 3823, 
3833, 3847, 3851, 3853, 3863, 3877, 3881, 3889, 3907, 3911, 
3917, 3919, 3923, 3929, 3931, 3943, 3947, 3967, 3989, 4001
};

////////////////////////////////////////////////////////////////////////////////
// Construction
////////////////////////////////////////////////////////////////////////////////
CBigInt::CBigInt()
{
	Zero= New(0);
	One = New(1);
	Two = New(2);
	MulCache[0] = Zero;
}

////////////////////////////////////////////////////////////////////////////////
// CBigInt Functions
////////////////////////////////////////////////////////////////////////////////
#define EQUAL(BI,y)			( BI.len==1 && BI.bit[0]==y )

/******************************************************************************/
//	名称：GetPrime
//	功能：获取随机大素数
//  参数：len—大素数长度>4,<=BI_MAXLEN/4
//	返回：获取成功返回true，否则返回false
/******************************************************************************/
bool CBigInt::GetPrime(BigInt &P,UINT len)
{
	CHECK_MSG( len>4 && len<=BI_MAXLEN/4, "素数长度不在合法范围之内!" )

		BigInt P_1,P_1Div2,LtP,A,Tmp;

	int			k=0;
	
Next:	++k;
	// 产生一个随机大数(奇数) P
	RandVal(P,len);
	P.bit[len-1] |= BI_BASE>>1;
	int i;
	// 先用小素数测试
	for(i=0; i<PTL; ++i)
	{
		SetVal(LtP,PrimeTable[i]);
		CHECK( Div(Tmp,A,P,LtP) )
			if( !A.len )
				goto Next;
	}
	// 再用 Lehmann 方法测试
	P_1=P; P_1.bit[0] -= 1;
	CHECK( Div(P_1Div2,Tmp,P_1,Two) )
		for(i=0; i<5; ++i)
		{   // 产生一个小随机大数 A
			SetVal(A,rand()+2);
			CHECK( PowMod(Tmp,A,P_1Div2,P) )
				if( ! ( EQUAL(Tmp,1)||!Cmp(Tmp,P_1) ) )
				{   
					goto Next;
				}
		}
		return true;
}
/******************************************************************************/
//	名称：PowMod
//	功能：大数乘方取模 ( C = Power(A,B) (Mod N) )
//  参数：
//	返回：成功返回true，否则返回false
/******************************************************************************/
bool CBigInt::PowMod(BigInt &C,BigInt &A,BigInt &B,BigInt &N)
{
	// N != 0
	CHECK( N.len ) 

		static bool		bits[ BI_MAXLEN*4 ];
	static BigInt	Out,Tmp,M,ModCache[ BI_MAXLEN*4 ];
	int i,j;

	// ModCache[i] = Power(A, Power(2,i) ) (Mod N)
	CHECK( Div(M,ModCache[0],A,N) )
		for(i=1,j=B.len<<2; i<j ; ++i)
		{   
			CHECK( Mul(Tmp,ModCache[i-1],ModCache[i-1]) )
				CHECK( Div(M,ModCache[i],Tmp,N) )
		}

		Out = One;
		// 将B分解成位组
		CGfL::ByteToBit(bits,B.bit,B.len,4);
		for(i=0; i<j; ++i)
		{
			if(bits[i])
			{
				CHECK( Mul(Tmp,Out,ModCache[i]) )
					CHECK( Div(M,Out,Tmp,N) )
			}
		}
		C = Out;

		return true;
}
/******************************************************************************/
//	名称：Inverse
//	功能：求乘法逆元 ( 即求X使 AX = 1 (mod N) )
//  参数：X—乘法逆元
//	返回：成功(A,N互素)返回 true; 否则返回 false
/******************************************************************************/
#define IV_CHECK(x) {if( !(x) ){ if(Spls)delete(Spls); if(M)delete(M);return false; }}

bool CBigInt::Inverse(BigInt &A,BigInt &X,BigInt &N,BigInt &Y)
{ 
	BigInt *Spls=new(BigInt),*M=new(BigInt);

	// 检查申请内存是否成功
	IV_CHECK( Spls && M )

		IV_CHECK( Div(*M,*Spls,N,A) )
		// 检查余数是否为0
		IV_CHECK( Spls->len )

		if( EQUAL((*Spls),1) )
		{
			Sub(X,N,*M);
			Sub(Y,A,One);
		}
		else
		{
			// 递归调用
			CHECK( Inverse(*Spls,X,A,Y) )

				static BigInt Tmp;
			IV_CHECK( Mul(Tmp,X,*M) )
				IV_CHECK( Add(Tmp,Tmp,Y) )
				IV_CHECK( Sub(Y,A,X) )
				IV_CHECK( Sub(X,N,Tmp) )
		}

		delete(Spls); 
		delete(M);

		return true;
}
/******************************************************************************/
//	名称：Mul
//	功能：大数乘法 ( C = A * B )
//  参数：
//	返回：成功返回true，否则返回false
/******************************************************************************/
bool CBigInt::Mul(BigInt &C,BigInt &A,BigInt &B)
{
	static BigInt Out,Tmp,*pa,*pb;

	if( A.len > B.len )
		pa=&A,pb=&B;
	else
		pa=&B,pb=&A;

	Out = Zero;
	CHECK( InitMulCache(*pa) )

		for(int i=0,j=B.len; i<j; ++i)
		{
			if( pb->bit[i] )
			{
				CHECK( ShR(Tmp,MulCache[ pb->bit[i] ],i) )
					CHECK( Add(Out,Out,Tmp) )
			}
		}
		C = Out;

		return true;
}
/******************************************************************************/
//	名称：GetDivNext
//	用途：从A中(高位(由i确定)到低位)移入一些位到Spls中，使Spls>=B
//  参数：i—指向A的"当前最高位"
//	返回：成功返回true,否则返回false
/******************************************************************************/
bool CBigInt::GetDivNext(BigInt &Spls,const BigInt &A,const BigInt &B,int &i)
{
	do{
		int k = B.len - Spls.len;
		if( k>0 )
		{
			if( i >= k-1 )
			{   
				i -= k;
				ShR(Spls,Spls,k);
				memcpy(Spls.bit,A.bit+i+1,k);
				// 如果Spls为0,则必须重设长度，因为移入的高位可能是0
				if( !Spls.len )
					SetLen(Spls,B.len-1);
			}
			else
			{
				ShR(Spls,Spls,i+1);
				memcpy(Spls.bit,A.bit ,i+1);
				if( !Spls.len )
					SetLen(Spls,i);
				return false;
			}
		}

		if( Cmp(Spls,B)<0 )
		{
			if( i>=0 )
			{
				ShR(Spls,Spls,1);
				Spls.bit[0] = A.bit[i--];
				if( !Spls.len && Spls.bit[0] )
					Spls.len = 1;
			}
			else
			{
				return false;
			}
		}
	}while( Spls.len < B.len );

	return true;
}
/******************************************************************************/
//	名称：BI_Div
//	功能：大数除法 ( M = A/B; Spls = A (Mod B) )
//  参数：
//	返回：成功返回true，否则返回false 
/******************************************************************************/
bool CBigInt::Div(BigInt &M,BigInt &Spls,BigInt &A,BigInt &B)
{
	// B != 0
	CHECK( B.len )

		if( Cmp(A,B)<0 )
		{
			M = Zero;
			Spls = A;
			return true;
		}

		static BigInt Out,Mod;

		Out = Mod = Zero;
		InitMulCache(B);
		for(int i=A.len-1,f,h,t,m; i>=0; )
		{
			if( !GetDivNext(Mod,A,B,i) )
				break;
			//用二分法找商
			h=1; t=BI_BASE-1;
			while( h<=t )
			{
				m = (h+t) >> 1;
				f = Cmp(MulCache[m],Mod);
				if( !f )
					break;
				if( f>0 )
					t = m-1;
				else
					h = m+1;
			}
			Out.bit[i+1] = f ? --h : m;

			if(f)
				Sub(Mod,Mod,MulCache[h]);
			else 
				Mod = Zero;
		}

		SetLen( Out,A.len-B.len);
		Spls = Mod;
		M = Out;

		return true;
}
/******************************************************************************/
//  名称：Add
//  功能：大数加法 ( C = A + B )
//  参数：
//	返回：成功返回true，否则返回false
/***************************************************************************/
bool CBigInt::Add(BigInt &C,BigInt &A,BigInt &B)
{
	static BigInt Out,*pa,*pb;

	if( A.len > B.len )
		pa=&A,pb=&B;
	else
		pa=&B,pb=&A;

	Out = *pa;

	UINT i,j,carry,tmp;
	for(i=0,carry=0,j=pb->len,tmp; i<j; ++i)
	{
		tmp = pa->bit[i] + pb->bit[i] + carry;
		if( tmp > BI_BASE-1 )
		{
			tmp -= BI_BASE;
			carry = 1;
		}
		else
		{
			carry = 0;
		}
		Out.bit[i] = tmp;
	}

	if(carry)
	{
		while( Out.bit[i] == BI_BASE-1 )
		{
			Out.bit[i] = 0;
			CHECK( ++i < BI_MAXLEN );
		}
		CHECK( i < BI_MAXLEN );
		++Out.bit[i];
		if( i == pa->len )
			++Out.len;
	}

	C = Out;

	return true;
}
/******************************************************************************/
//	名称：Sub
//	功能：大数减法 ( C = A - B )
//  参数：
//	返回：成功返回true，否则返回false
//  备注：调用前保证A >= B，否则结果不对
/******************************************************************************/
bool CBigInt::Sub(BigInt &C,BigInt &A,const BigInt &B)
{
	if( &C != &A )
		C = A;

	int i=0,carry=0,j=B.len,tmp;
	for( i=0,carry=0,j=B.len,tmp; i<j; ++i)
	{
		tmp = A.bit[i] - B.bit[i] - carry;
		if( tmp<0 )
		{
			tmp += BI_BASE;
			carry = 1;
		}
		else
		{
			carry = 0;
		}
		C.bit[i] = tmp;
	}

	if(carry)
	{
		while( !C.bit[i] )
		{
			C.bit[i] = BI_BASE-1;
			CHECK( ++i < BI_MAXLEN );
		}
		CHECK( i < BI_MAXLEN );
		--C.bit[i];
	}

	SetLen(C,C.len-1);

	return true;
}
/******************************************************************************/
//	名称：InitMulCache
//	功能：初始化乘法缓存 ( MulCache[i] = In*i )
//  参数：
//	返回：成功返回true，否则返回false
/******************************************************************************/
bool CBigInt::InitMulCache(BigInt &In)
{
	MulCache[1] = In;

	for(int i=2; i<BI_BASE; ++i)
	{
		CHECK( Add(MulCache[i],MulCache[i-1],In) )
	}

	return true;
}
/******************************************************************************/
//  名称：SetLen
//  功能：设置大数的长度
//  参数：
//	返回：
/***************************************************************************/
void CBigInt::SetLen(BigInt &BI,UINT start)
{
	int i=start;
	for( i=start; i>=0 && !BI.bit[i]; --i );

	BI.len = i+1;
}
/******************************************************************************/
//	名称：ShR
//	功能：大数逻辑右移
//  参数：
//	返回：成功返回true，否则返回false
/******************************************************************************/
bool CBigInt::ShR(BigInt &Out,BigInt &In,UINT len)
{
	CHECK( (In.len+len) <= BI_MAXLEN )

		if( !len )
		{
			if( &Out != &In )
				Out = In;
		}
		else if( In.len )
		{
			static BigInt Tmp;
			Tmp = Zero;
			memcpy(Tmp.bit+len,In.bit,In.len);
			Tmp.len = In.len + len;
			Out = Tmp;
		}

		return true;
}
/******************************************************************************/
//	名称：Cmp
//	功能：大数比较
//  参数：
//	返回：A>B 返回 1; A=B 返回 0; A<B 返回 -1
/******************************************************************************/
int CBigInt::Cmp(const BigInt &A,const BigInt &B)
{
	int i=A.len;

	if( (UINT)i > B.len )
		return 1;
	if( (UINT)i < B.len )
		return -1;

	for(--i; i>=0; --i)
	{
		if( A.bit[i] > B.bit[i] )
			return 1;
		if( A.bit[i] < B.bit[i] )
			return -1;
	}

	return 0;
}
/******************************************************************************/
//	名称：New
//	功能：产生一个大数
//  参数：
//	返回：
/******************************************************************************/
BigInt CBigInt::New(long n)
{
	int i=0;
	BigInt Out;

	memset(Out.bit,0,BI_MAXLEN);
	while(n)
	{
		Out.bit[i++] = n%BI_BASE;
		n /= BI_BASE;
	}
	Out.len = i;

	return Out;
}

/******************************************************************************/
//	名称：SetVal
//	功能：设置大数的值
//  参数：
//	返回：
/******************************************************************************/
void CBigInt::SetVal(BigInt &BI,long n)
{
	int i=0;

	BI = Zero;
	while(n)
	{
		BI.bit[i++] = n%BI_BASE;
		n /= BI_BASE;
	}
	BI.len = i;
}
/******************************************************************************/
//	名称：BuildBIFromByte
//	功能：由输入字节组构造一个大数
//  参数：
//	返回：构造成功返回true，否则返回false
/******************************************************************************/
bool CBigInt::BuildBIFromByte(BigInt &Out,const char *In,UINT len)
{
	CHECK( In && len<=BI_MAXLEN/4 )

		Out = Zero;
	CGfL::ByteToHalfByte(Out.bit,In,len);
	SetLen(Out,len<<1);

	return true;
}
/******************************************************************************/
//	名称：BuildBIFromHalfByte
//	功能：由输入半字节组构造一个大数
//  参数：
//	返回：构造成功返回true，否则返回false
/******************************************************************************/
bool CBigInt::BuildBIFromHalfByte(BigInt &Out,const char *In,UINT len)
{
	CHECK( In && len<=BI_MAXLEN/2 )

		Out = Zero;
	memcpy(Out.bit,In,len);
	SetLen(Out,len);

	return true;
}
/******************************************************************************/
//	名称：BuildBIFromStr
//	功能：由输入字符串构造一个大数
//  参数：len—字符串长度
//	返回：构造成功返回true，否则返回false
/******************************************************************************/
bool CBigInt::BuildBIFromStr(BigInt &Out,char *In,UINT len)
{
	CHECK( In && len<=BI_MAXLEN/2 )

		Out = Zero;
	CGfL::StrToHalfByte( Out.bit,In,len);
	SetLen(Out,len);

	return true;
}
BigInt CBigInt::GetBIFromStr(CString strIn)
{
	BigInt bi;
	BuildBIFromStr(bi,strIn.GetBuffer(strIn.GetLength()),strIn.GetLength());

	return bi;
}
/******************************************************************************/
//	名称：Rand
//	功能：产生随机大数
//  参数：len—随机大数的长度
//	返回：产生成功返回true，否则返回false
/******************************************************************************/
bool CBigInt::RandVal(BigInt &Out,UINT len)
{
	CHECK( len>0 && len<=BI_MAXLEN )

		Out = Zero;
	srand(GetTickCount());
	for(UINT i=0; i<len; ++i)
	{
		Out.bit[i] = rand()%BI_BASE;
	}

	Out.bit[0] |= 1;
	Out.bit[len-1] |= 1;
	Out.len = len;

	return true;
}
///////////////////////////////////////////////////////////////////////////////
// End of Files
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// CGfL Functions
///////////////////////////////////////////////////////////////////////////////
#define CHECK(x)		{if( !(x) ) return false;}

/******************************************************************************/
//	名称：ByteToBit
//	功能：将字节组转换成位组
//  参数：len—字节组长度；num—一个字节转换成几个位字节
//	返回：转换成功返回true，否则返回false
/******************************************************************************/
bool CGfL::ByteToBit(bool *Out,const char *In,UINT len,UINT num)
{
	CHECK( Out && In && num<=8 )

		for(UINT i=0,j; i<len; ++i,Out+=num)
		{
			for(j=0; j<num; ++j)
			{
				Out[j] = (In[i]>>j) & 1;
			}
		}

		return true;
}
/******************************************************************************/
//	名称：BitToByte
//	功能：将位组转换成字节组
//  参数：len—位组长度；num—几个位字节转换成一个字节
//	返回：转换成功返回true，否则返回false
/******************************************************************************/
bool CGfL::BitToByte(char *Out,const bool *In,UINT len,UINT num)
{
	CHECK( Out && In )
		UINT i,j,L;

	memset(Out,0,(len+num-1)/num);

	for(i=0,j,L=len/num; i<L; ++i,In+=num)
	{
		for(j=0; j<num; ++j)
		{
			Out[i] |= In[j]<<j;
		}
	}
	for(j=0; j<len%num; ++j)
	{
		Out[i] |= In[j]<<j;
	}

	return true;
}
/******************************************************************************/
//	名称：HalfByteToByte
//	功能：将半字节组转换成字节组
//  参数：
//	返回：转换成功返回true，否则返回false
/******************************************************************************/
bool CGfL::HalfByteToByte(char *Out,const char *In,UINT len)
{
	CHECK( Out && In )

		for(UINT i=0,j=len>>1; i<j; ++i)
		{
			*Out = In[0];
			*Out |= In[1]<<4;
			++Out; In += 2;
		}

		if( len%2 )
			*Out = *In;

		return true;
}
/******************************************************************************/
//	名称：ByteToHalfByte
//	功能：将字节组转换成半字节组
//  参数：
//	返回：转换成功返回true，否则返回false
/******************************************************************************/
bool CGfL::ByteToHalfByte(char *Out,const char *In,UINT len)
{
	CHECK( Out && In )

		for(UINT i=0; i<len; ++i)
		{
			Out[0] = (*In)&0xf;
			Out[1] = ((*In)>>4)&0xf;
			Out += 2; ++In;
		}

		return true;
}
/******************************************************************************/
//	名称：StrToHalfByte
//	功能：将字符串转换成半字节组
//  参数：
//	返回：合法字符('0'-'9','A'-'F')的个数
/******************************************************************************/
int CGfL::StrToHalfByte(char *Out,char *In,UINT len)
{
	CHECK( Out && In )
		UINT i,j;

	for(i=0,j=0; i<len; ++i)
	{
		if( (In[i]>='0') && (In[i]<='9') )
			Out[j++] = In[i]-'0';
		else if( (In[i]>='A') && (In[i]<='F') )
			Out[j++] = In[i]-'A'+10;
		else if( (In[i]>='a') && (In[i]<='f') )
			Out[j++] = In[i]-'a'+10;
	}

	return j;
}
/******************************************************************************/
//	名称：HalfByteToStr
//	功能：将半字节组转换成字符串
//  参数：
//	返回：合法数(0-15)的个数
/******************************************************************************/
int  CGfL::HalfByteToStr(char *Out,char *In,UINT len)
{
	CHECK( Out && In )
		UINT i,j;

	for(i=0,j=0; i<len; ++i)
	{
		if( (In[i]>=0) && (In[i]<10) )
			Out[j++] = In[i]+'0';
		else if( (In[i]>9) && (In[i]<16) )
			Out[j++] = In[i]-10+'A';
	}
	Out[j] = '\0';

	return j-1;
}

void CGfL::Bytes2Bits(char *srcBytes, char* dstBits, unsigned int sizeBits)
{
	for(unsigned int i=0; i < sizeBits; i++)
		dstBits[i] = ((srcBytes[i>>3]<<(i&7)) & 128)>>7;
}

void CGfL::Bits2Bytes(char *dstBytes, char* srcBits, unsigned int sizeBits)
{
	memset(dstBytes,0,sizeBits>>3);
	for(unsigned int i=0; i < sizeBits; i++)
		dstBytes[i>>3] |= (srcBits[i] << (7 - (i & 7)));
}

void CGfL::Int2Bits(unsigned int _src, char* dstBits)
{
	for(unsigned int i=0; i < 4; i++)
		dstBits[i] = ((_src<<i) & 8)>>3;
}

void CGfL::Bits2Hex(char *dstHex, char* srcBits, unsigned int sizeBits)
{
	memset(dstHex,0,sizeBits>>2);
	for(unsigned int i=0; i < sizeBits; i++) //convert to int 0-15
		dstHex[i>>2] += (srcBits[i] << (3 - (i & 3)));
	for(unsigned int j=0;j < (sizeBits>>2);j++)
		dstHex[j] += dstHex[j] > 9 ? 55 : 48; //convert to char '0'-'F'
}

void CGfL::Hex2Bits(char *srcHex, char* dstBits, unsigned int sizeBits)
{
	memset(dstBits,0,sizeBits);
	for(unsigned int i=0;i < (sizeBits>>2);i++)
		srcHex[i] -= srcHex[i] > 64 ? 55 : 48; //convert to char int 0-15
	for(unsigned int j=0; j < sizeBits; j++) 
		dstBits[j] = ((srcHex[j>>2]<<(j&3)) & 15) >> 3;

}

char CGfL::ConvertHexChar(char ch)
{
	if((ch>='0')&&(ch<='9'))   
		return   ch-0x30;   
	else   if((ch>='A')&&(ch<='F'))   
		return   ch-'A'+10;   
	else   if((ch>='a')&&(ch<='f'))   
		return   ch-'a'+10;   
	else   return   (-1);  
}

/**
     * @notes 字符串转化为字节数组
     * @param str
     * @return byte[]
*/
bool CGfL::HexStrToBytes(CString strHexSrc, char *bytes, UINT lenbytes) { 
    int length = strHexSrc.GetLength() / 2;
	if(length> lenbytes) return false;

    char* source = strHexSrc.GetBuffer(length);

    for (int i = 0; i < length; ++i) {
        byte bh = ConvertHexChar(source[i * 2]);
        bh = (byte)(bh << 4);
        byte bl = ConvertHexChar(source[i * 2 + 1]);
        bytes[i] = (byte)(bh ^ bl);
    }
        
    return true;
}

/**
     * @notes 字节数组转化为十六进制字符串
     * @param bytes
     * @return String
*/
CString CGfL::BytesToHexStr(char *bytes, int lenbytes)
{
	static char hexDigits[] = {'0','1','2','3','4','5','6','7','8','9','A','B','C','D','E','F'};
	char* hexchars = new char[lenbytes * 2+1];
	memset( hexchars, 0, sizeof(char)*(lenbytes * 2+1) );

	for (int i = 0; i < lenbytes; i++)
	{
		int b = bytes[i];
		hexchars[i * 2] = hexDigits[(b & 0xF0) >> 4];
		hexchars[i * 2 + 1] = hexDigits[b & 0x0F];
	}
	
	CString hexstr = hexchars;
	delete[] hexchars;

	return hexstr;
}

///////////////////////////////////////////////////////////////////////////////
// End of Files
///////////////////////////////////////////////////////////////////////////////
