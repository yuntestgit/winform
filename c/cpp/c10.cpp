#include <cstdlib>
#include <iostream>

using namespace std;

char *StrLeftN(char*, int);

int main(void)
{
	cout<<"10.呼叫副程式StrLeftN()將字元陣列a[]左側N個字元放入b[]，再印出a[]，b[]之內容。\n";
	char a[999], *b;
	int n;
	cout<<"輸入字元陣列a[]：";
	cin>>a;
	cout<<"輸入整數N：";
	cin>>n;

	b=StrLeftN(a, n);
	cout<<"輸出字元陣列a[]："<<a<<"\n輸出字元陣列b[]："<<b<<"\n";

	system("pause");
	return 0;
}

char *StrLeftN(char *a, int n)
{
	char *c = new char[n];
	for(int i=0; i<n; i++)
	{
		c[i]=a[i];
	}
	c[n]='\0';
	return c;
}