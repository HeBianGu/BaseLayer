#include "stdafx.h"
#include "�ṹ��.h"


�ṹ��::�ṹ��()
{

}

�ṹ��::~�ṹ��()
{

}

void �ṹ��::Test()
{

	//	�ṹ���壬ֻ��һ���������ͣ���ռ�ڴ�
	struct student
	{
		int num;
		char name[20];
		char sex;
		int age;
		float score;
		char address[30];
	};

	//	�������
	struct student student1, student2;

	student1.address;//	ʹ�ýṹ��

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

	people1.address;//	ʹ�÷��ͽṹ��

	//int s::id = 50;//ʹ�þ�̬��Ա

}






//	�ṹ���壬ֻ��һ���������ͣ���ռ�ڴ�
struct animal
{
	int num;
	char name[20];
	char sex;
	int age;
	float score;
	char address[30];
} animal1, animal2;//	ֱ�Ӷ������

struct  s
{

	static int id;//	��̬��Ա ���������һ��ռ�

	int eng;
};