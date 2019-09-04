// TEST.cpp : 定義主控台應用程式的進入點。
//

#include "stdafx.h"
#include <cstdlib>
#include <cstdio>

int _tmain(int argc, _TCHAR* argv[])
{
	int i = 0x00;
	int i2= 0xff;
	int i3 = i&i2;
	printf("%d", i3);
	system("pause");
	return 0;
}

