#include <cstdlib>
#include <iostream>

using namespace std;

char *StrCutChar(char*);

int main(void)
{
	cout<<"14.�I�s�Ƶ{��StrCutChar()�h���r���}�Ca[]�����S��r���A��Jb[]�A�A�L�Xa[]�Ab[]�����e�C\n";
	char a[999], *b;
	cout<<"��J�r���}�Ca[]�G";
	cin>>a;

	b=StrCutChar(a);
	cout<<"��X�r���}�Ca[]�G"<<a<<"\n��X�r���}�Cb[]�G"<<b<<"\n";

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