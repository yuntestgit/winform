#include <cstdlib>
#include <iostream>

using namespace std;

int StrLen(char*);
void StrCopy(char *a, char *&b);

int main(void)
{
	cout<<"9.�I�s�Ƶ{��StrCopy()�N�r���}�Ca[]�ƻs��b[]�A�A�L�Xa[]�Bb[]�����e�C\n";
	char a[999], *b;

	cout<<"��J�r���}�Ca[]�G";
	cin>>a;
	StrCopy(a, b);
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