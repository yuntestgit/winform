#include <cstdlib>
#include <iostream>

using namespace std;

int StrLen(char*);
char *StrRev(char*);

int main(void)
{
	cout<<"13.�I�s�Ƶ{��StrRev()�N�r���}�Ca[]�r�������A�˩�Jb[]�A�A�L�Xa[]�Ab[]�����e�C\n";
	char a[999], *b;
	cout<<"��J�r���}�Ca[]�G";
	cin>>a;

	b=StrRev(a);
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