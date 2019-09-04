#include <cstdlib>
#include <iostream>
#include <string>

using namespace std;

int main(void)
{
	int n = 5;
	int *a = new int[n];
	a[0] = 2;
	a[1] = 1;
	a[2] = 1;
	a[3] = 3;
	a[4] = 3;

	int *b = new int[n];
	b[0] = 1;
	b[1] = 2;
	b[2] = 2;
	b[3] = 3;
	b[4] = 3;

//int *b = new int[n] {1,2,2,3,3};
	//a = b;
	/*for(int i=0;i<n;i++)
	{
		a[i]=b[i];
	}*/

	system("pause");
	return 0;
}