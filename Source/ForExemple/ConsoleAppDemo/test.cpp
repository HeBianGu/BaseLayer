#include "stdafx.h"
#include "test.h"
#include "stdarg.h"//	���������ɱ�������ռ�

extern int ss;//	�ⲿȫ�ֱ���˵��

//extern int kk;��ֻ�ܱ��ļ�ʹ�ã�

#define G 9.8 //	����� (ֻ�ǻ�е�滻���ַ����滻���ڱ���֮ǰ�滻���)
#define PI 3.1415926
#define L 2*PI*PI;//	��Ĳ���û�

#define S(a,b) a*b;//	�������ĺ궨�� �м䲻�ܼӿո�

#define S(r) PI*r*r;


test::test()
{

}

test::test(int a)
{

}

test::test(test &a)
{

}

void test::TestMethod()
{
	int tt = ss;//	�����ļ��ⶨ���ȫ�ֱ���

	//int mm = kk;//	ֻ���ڱ��ļ���ʹ�ã���extern��Ӧ

	//int oo = ww;

	static int pp;//	��̬�ֲ��������ڱ�ĺ����в�������
}

void test::ȱʡֵ(int a, int b = 1)
{
	a = G*b;//	ʹ�ú�

	a = S(a, b);//	ʹ�ô������ĺ궨�� = a=a*b;

	a = S(2, 3);//	ʹ�ô������ĺ궨�� = a=2*3;

	S(a);//	= PI*a*a

	S(a + b)//	=	PI*a+b*a+b ������PI*(a+b)*(a+b)
}

#undef G //	��ֹ���������

void test::���Կɱ亯��(int a, int b = 1)
{

	va_start(_ap, b);//��ʼ�� b��Ϊ�ɱ����Ǯ�����һ��ȷ���Ĳ���

	va_arg(_ap, int);//����ȡ���� int��Ϊ�ɱ����������������

	va_end(_ap);//��ȷ����
}

void test::�ɱ����(va_list ap)
{

}

test::~test()
{
}
