#include "stdafx.h"
#include "枚举.h"
#include "stdafx.h"
#include <iomanip>//不要忘记包含此头文件
#include <iostream>
using namespace std;


枚举::枚举()
{
	
}

void	枚举::Test()
{
	enum  weekday
	{
		sun, mon, tue//	直接当一个int值用了
	};

	/*sun=weekday.
	sun = mon;*/

	//weekday ss=sun;
	//ss = (enum  weekday)1;
	//mon = 1;
	cout << sun;
}

枚举::~枚举()
{
}



//enum  wss
//{
//	sun=0, mon=5, tue //	会显示重复定义
//};
