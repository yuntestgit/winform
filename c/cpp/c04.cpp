#include <cstdlib>
#include <iostream>

using namespace std;

int StrLen(char*);

int main(void)
{
	cout<<"4.��J��Ӧr���}�C�A�N���̳s�����@�s�r���}�C�A�L�X�C\n";
	char c1[999], c2[999];
	int len1, len2, i;

	cout<<"��J�r���}�C1�G";
	cin>>c1;
	cout<<"��J�r���}�C2�G";
	cin>>c2;
	
	len1=StrLen(c1);
	len2=StrLen(c2);
	char *c = new char[len1+len2];

	for(i=0; i<len1; i++)
	{
		c[i]=c1[i];
	}
	for(i=0; i<len2; i++)
	{
		c[i+len1]=c2[i];
	}
	c[len1+len2]='\0';

	cout<<"��X�r���}�C�G"<<c<<"\n";

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