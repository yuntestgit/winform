#include <cstdlib>
#include <iostream>

using namespace std;

char *StrCutChar(char*);

int main(void)
{
	cout<<"14.呼叫副程式StrCutChar()去除字元陣列a[]中的特殊字元，放入b[]，再印出a[]，b[]之內容。\n";
	char a[999], *b;
	cout<<"輸入字元陣列a[]：";
	cin>>a;

	b=StrCutChar(a);
	cout<<"輸出字元陣列a[]："<<a<<"\n輸出字元陣列b[]："<<b<<"\n";

	system("pause");
	return 0;
}

char *StrCutChar(char *a)
{
	int n=0, i=0, i2=0;
	while(a[i]!='\0')
	{
		if((a[i]>='0' && a[i]<='9') || (a[i]>='a' && a[i]<='z') || (a[i]>='A' && a[i]<='Z'))
		{
			n++;
		}
		i++;
	}
	i=0;
	char *c = new char[n];
	for(i2=0; i2<n; i2++)
	{
		while(true)
		{
			if((a[i]>='0' && a[i]<='9') || (a[i]>='a' && a[i]<='z') || (a[i]>='A' && a[i]<='Z'))
			{
				c[i2]=a[i];
				i++;
				break;
			}
			i++;
		}
	}
	c[n]='\0';
	return c;
}