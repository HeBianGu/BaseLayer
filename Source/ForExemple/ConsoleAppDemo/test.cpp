#include "stdafx.h"
#include "test.h"
#include "stdarg.h"//	变量个数可变的命名空间

extern int ss;//	外部全局变量说明

//extern int kk;（只能本文件使用）

#define G 9.8 //	定义宏 (只是机械替换，字符串替换，在编译之前替换完成)
#define PI 3.1415926
#define L 2*PI*PI;//	宏的层层置换

#define S(a,b) a*b;//	带参数的宏定义 中间不能加空格

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
	int tt = ss;//	引用文件外定义的全局变量

	//int mm = kk;//	只限在本文件中使用，于extern对应

	//int oo = ww;

	static int pp;//	静态局部变量，在别的函数中不能引用
}

void test::缺省值(int a, int b = 1)
{
	a = G*b;//	使用宏

	a = S(a, b);//	使用带参数的宏定义 = a=a*b;

	a = S(2, 3);//	使用带参数的宏定义 = a=2*3;

	S(a);//	= PI*a*a

	S(a + b)//	=	PI*a+b*a+b 而不是PI*(a+b)*(a+b)
}

#undef G //	终止宏的作用域

void test::测试可变函数(int a, int b = 1)
{

	va_start(_ap, b);//初始化 b即为可变参数钱的最后一个确定的参数

	va_arg(_ap, int);//依次取参数 int即为可变参数的数据类型名

	va_end(_ap);//正确结束
}

void test::可变参数(va_list ap)
{

}

test::~test()
{
}
