#pragma once
class test
{
	void TestMethod();

	void ȱʡֵ(int a, int b);// ȱʡֵ��cpp�ļ�����ʾ

	void �ɱ����(va_list ap);// �ɱ����

	void ���Կɱ亯��(int a, int b);// �ɱ����
public:
	test();

	test(int a=1);

	test(test &a);//	�������βα�����ͬ���Ͷ��������

	~test();

	static int ww;//	ȫ�־�̬����

	int ee;//	ȫ�ֶ�̬����

	va_list _ap;
};

