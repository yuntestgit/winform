#include <cstdlib>
#include <iostream>

using namespace std;

int StrLen(char*);
char *StrRightN(char*, int);

int main(void)
{
	cout<<"10.呼叫副程式StrRightN()將字元陣列a[]右側N個字元放入b[]，再印出a[]，b[]之內容。\n";
	char a[999], *b;
	int n;
	cout<<"輸入字元陣列a[]：";
	cin>>a;
	cout<<"輸入整數N：";
	cin>>n;

	b=StrRightN(a, n);
	cout<<"輸出字元陣列a[]："<<a<<"\n輸出字元陣列b[]："<<b<<"\n";

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

char *StrRightN(char *a, int n)
{
	char *c = new char[n];
	int len = StrLen(a);
	for(int i=len-n; i<len; i++)
	{
		c[i-(len-n)]=a[i];
	}
	c[n]='\0';
	return c;
}