#include "stdafx.h"
#include "数组.h"
#include "指针.h"
#include "test.h"


数组::数组()
{
	指针 s = 指针();

	指针 *s1 = new 指针();//	用new动态开辟对象控件，初始化

	delete s1;//	调用析构函数

	//delete s; //	只能回收指针new对象
	s.~指针();

	指针 *s2 = &s;

	s2->~指针();//	指针调用方法的方法

	指针 *arr = new 指针[3];

	delete[]arr;//	释放数组，指针变量的前面必须加上[]

	test t(5), k = test(6);//	构造函数

	t = 10;//	当够着函数只有一个参数可以用=强制赋值

	test copy(k);//	调用拷贝的构造函数

	test copyN = t; //	调用隐藏的构造函数



}

#define SIZE 60;

static int yy[4] = { 1, 2 };//用局部static或全局定义的数组不能赋初始值

extern int oo[4] = { 1, 2 };//用局部static或全局定义的数组不能赋初始值

void 数组::Tester()
{
	int art[6];

	int n = 7;

	//int kk[n];//	数组必须用常量定义 和C#有区别

	//int kk[SIZE];

	int uu[10] = { 1, 23, 4, 5 };//	数组的初始化 其他取默认值

	static int ww[4] = { 1, 2 };//	用static定义的数组不赋初始值默认'\0'

	//dd = { { 11, 3, 4 },{ 11, 3, 4 } };//	用局部static或全局定义的数组不能赋初始值

	int ss[2][3] = { { 11, 3, 4 }, { 11, 3, 4 } };//	分行赋值

	//ss[2][3] = { 11, 3, 4, 11, 3, 4 };//	顺序赋值 c++不可以重新初始值是吧

	int vv[2][3] = { 11, 3, 4, 11, 3, 4 };//	顺序赋值 c++不可以重新初始值是吧

	int jj[3][4] = { { 1 }, { 3 }, { 4 } };//	部分赋值初始值


	char aaa[] = { 'g', 'h', 'j' };//	字符数组 zhan长度占三个字节

	char ccc[] = "ghj";//	字符串	长度占四个字节 其中包含一个'\0'结束符

	char uuu[10] = "ghj"; //	默认后面都是'\0'

	char a[2][5] = { "abcd", "ABCD" };

	char str[3] = "ab";

	//str = "bc";//	非法，智能在定义开辟控件是赋值

}

数组::~数组()
{
}
