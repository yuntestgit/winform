#include <cstdlib>
#include <iostream>

using namespace std;

int StrLen(char*);
void StrCopy(char *a, char *&b);

int main(void)
{
	cout<<"9.呼叫副程式StrCopy()將字元陣列a[]複製到b[]，再印出a[]、b[]之內容。\n";
	char a[999], *b;

	cout<<"輸入字元陣列a[]：";
	cin>>a;
	StrCopy(a, b);
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

void StrCopy(char *a, char *&b)
{
	int len, i;
	len=StrLen(a);
	char *c = new char[len];
	for(i=0; i<len; i++)
	{
		c[i]=a[i];
	}
	c[len]='\0';
	b=c;
}