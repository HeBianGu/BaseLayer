#include "stdafx.h"
#include "ָ������ָ��.h"


ָ������ָ��::ָ������ָ��()
{
}


ָ������ָ��::~ָ������ָ��()
{
}

void ָ������ָ��::Test()
{
	int(*p)(int, int);//	ʹ�ú�����ָ����Ҫ�ȶ�������

	int  Test3(int, int);//	ʹ�ú�����ֵҲ��Ҫ�ȶ�������

	int a = 0, b = 0, c;

	//p = Test2;//	��֪��Ϊʲô�ò���

	//c = p(a, b);

	//Test1(a, b, p);
}

int ָ������ָ��::Test1(int i, int j, int(*p)(int, int))
{
	//	ʹ��ָ������ָ�� ί��
	return p(i, j);
}

int ָ������ָ��::Test2(int i, int j)
{
	//	ʹ��ָ������ָ�� ί��
	return i + j;
}


