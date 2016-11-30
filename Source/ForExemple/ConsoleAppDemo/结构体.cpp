#include "stdafx.h"
#include "结构体.h"


结构体::结构体()
{

}

结构体::~结构体()
{

}

void 结构体::Test()
{

	//	结构定义，只是一种数据类型，不占内存
	struct student
	{
		int num;
		char name[20];
		char sex;
		int age;
		float score;
		char address[30];
	};

	//	定义变量
	struct student student1, student2;

	student1.address;//	使用结构体

#define STUDENT struct people

	STUDENT
	{
		int num;
		char name[20];
		char sex;
		int age;
		float score;
		char address[30];
	};

	STUDENT people1, people2;

	people1.address;//	使用泛型结构体

	//int s::id = 50;//使用静态成员

}






//	结构定义，只是一种数据类型，不占内存
struct animal
{
	int num;
	char name[20];
	char sex;
	int age;
	float score;
	char address[30];
} animal1, animal2;//	直接定义变量

struct  s
{

	static int id;//	静态成员 多个对象公用一块空间

	int eng;
};