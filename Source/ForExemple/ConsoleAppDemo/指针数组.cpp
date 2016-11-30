#include "stdafx.h"
#include "指针数组.h"


指针数组::指针数组()
{
	int *p[4];//	内有四个元素，每个元素可以放一个int型数据的地址(地址的集合)

	int(*p1)[4];//	p为指向有四个int型元素的以为数组的行指针,是不是相当于 int s[4]的头指针？

	int  p2[3][2];

	//p2[3][2] = *(*(p2 + 3) + 2) = *(p2[3] + 2);

	char *a[] = { "CHINA", "JAPAN", "AMERICA" };

	a[1][4] = *(*(a + 1) + 4);

	*a[2] = *(*(a + 2) + 0);

	**a = *(*(a + 0) + 0);

	//char	**b = { "aaaa", "bbbb", "cccc" };

	char *s[3] = { "aaaa", "bbbb", "cccc" };

	//char k[][3]={ "aaaa", "bbbb", "cccc" };

	//char u[][4] = { "aaaa", "bbbb", "cccc" };
}


指针数组::~指针数组()
{
}
