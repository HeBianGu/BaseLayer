#include "stdafx.h"
#include "指向函数的指针.h"


指向函数的指针::指向函数的指针()
{
}


指向函数的指针::~指向函数的指针()
{
}

void 指向函数的指针::Test()
{
	int(*p)(int, int);//	使用函数的指针需要先定义声明

	int  Test3(int, int);//	使用函数赋值也需要先定义声明

	int a = 0, b = 0, c;

	//p = Test2;//	不知道为什么用不了

	//c = p(a, b);

	//Test1(a, b, p);
}

int 指向函数的指针::Test1(int i, int j, int(*p)(int, int))
{
	//	使用指向函数的指针 委托
	return p(i, j);
}

int 指向函数的指针::Test2(int i, int j)
{
	//	使用指向函数的指针 委托
	return i + j;
}


