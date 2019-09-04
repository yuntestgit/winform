#include <cstdlib>
#include <iostream>

using namespace std;

int StrLen(char*);

int main(void)
{
	cout<<"7.呼叫副程式StrLen()，算出字元陣列a[]之字元數目，(不包括\\0)\n";
	cout<<"輸入字元陣列a[]：";

	char a[999];
	cin>>a;
	cout<<"Length = "<<StrLen(a)<<"\n";

	system("pause");
	return 0;
}

int StrLen(char *a)
{
	int i=0;
	while(a[i]!='\0')
	{
		i++;
	}
	return i;
}