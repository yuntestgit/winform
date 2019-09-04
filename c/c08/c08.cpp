#include <cstdlib>
#include <iostream>

using namespace std;

int StrLen(char*);
char * StrCat(char*, char*);

int main(void)
{
	cout<<"8.�I�s�Ƶ{��StrCat()�N�r���}�Ca[]�Pb[]�s����c[]�C\n";
	char a[999], b[999];

	cout<<"��J�r���}�Ca[]�G";
	cin>>a;
	cout<<"��J�r���}�Cb[]�G";
	cin>>b;

	char *c = StrCat(a, b);

	cout<<"��X�r���}�Cc[]�G"<<c<<"\n";

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

char *StrCat(char *a, char *b)
{
	int len1, len2, i;
	len1=StrLen(a);
	len2=StrLen(b);
	char *c = new char[len1+len2];

	for(i=0; i<len1; i++)
	{
		c[i]=a[i];
	}
	for(i=0; i<len2; i++)
	{
		c[i+len1]=b[i];
	}
	c[len1+len2]='\0';

	return c;
}