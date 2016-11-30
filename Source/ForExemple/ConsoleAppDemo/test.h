#pragma once
class test
{
	void TestMethod();

	void 缺省值(int a, int b);// 缺省值在cpp文件中显示

	void 可变参数(va_list ap);// 可变参数

	void 测试可变函数(int a, int b);// 可变参数
public:
	test();

	test(int a=1);

	test(test &a);//	拷贝，形参必须是同类型对象的引用

	~test();

	static int ww;//	全局静态变量

	int ee;//	全局动态变量

	va_list _ap;
};

