#pragma once
class 指向函数的指针
{

	void Test();

	int Test1(int i,int j,int(*p)(int, int));

	int Test2(int i, int j);

public:
	指向函数的指针();
	~指向函数的指针();
};

