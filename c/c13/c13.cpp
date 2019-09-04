#include <cstdlib>
#include <iostream>

using namespace std;

int StrLen(char*);
char *StrRev(char*);

int main(void)
{
	cout<<"13.呼叫副程式StrRev()將字元陣列a[]字元順序顛倒放入b[]，再印出a[]，b[]之內容。\n";
	char a[999], *b;
	cout<<"輸入字元陣列a[]：";
	cin>>a;

	b=StrRev(a);
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

char *StrRev(char *a)
{
	int len=StrLen(a);
	char *c = new char[len];
	
	for(int i=0; i<len; i++)
	{
		c[i]=a[len-i-1];
	}
	c[len]='\0';

	return c;
}