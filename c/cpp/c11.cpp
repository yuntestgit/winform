#include <cstdlib>
#include <iostream>

using namespace std;

int StrLen(char*);
char *StrRightN(char*, int);

int main(void)
{
	cout<<"10.�I�s�Ƶ{��StrRightN()�N�r���}�Ca[]�k��N�Ӧr����Jb[]�A�A�L�Xa[]�Ab[]�����e�C\n";
	char a[999], *b;
	int n;
	cout<<"��J�r���}�Ca[]�G";
	cin>>a;
	cout<<"��J���N�G";
	cin>>n;

	b=StrRightN(a, n);
	cout<<"��X�r���}�Ca[]�G"<<a<<"\n��X�r���}�Cb[]�G"<<b<<"\n";

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