#include <cstdlib>
#include <iostream>

using namespace std;

char *StrCutMN(char*, int, int);

int main(void)
{
	cout<<"10.呼叫副程式StrCutMN()將字元陣列a[]第M到第N個字元放入b[]，再印出a[]，b[]之內容。\n";
	char a[999], *b;
	int m, n;
	cout<<"輸入字元陣列a[]：";
	cin>>a;
	cout<<"輸入整數M：";
	cin>>m;
	cout<<"輸入整數N：";
	cin>>n;

	b=StrCutMN(a, m, n);
	cout<<"輸出字元陣列a[]："<<a<<"\n輸出字元陣列b[]："<<b<<"\n";

	system("pause");
	return 0;
}

char *StrCutMN(char *a,int m, int n)
{
	char *c = new char[n-m+1];
	for(int i=m; i<=n; i++)
	{
		c[i-m]=a[i];
		if(i==n)
		{
			c[i-m+1]='\0';
		}
	}
	return c;
}