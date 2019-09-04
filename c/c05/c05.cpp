#include <cstdlib>
#include <iostream>

using namespace std;

int StrLen(char*);

int main(void)
{
	cout<<"5.輸入三個字元陣列，將它們連接成一新字元陣列再印出。\n";
	char c1[999], c2[999], c3[999];
	int len1, len2, len3, i;

	cout<<"輸入字元陣列1：";
	cin>>c1;
	cout<<"輸入字元陣列2：";
	cin>>c2;
	cout<<"輸入字元陣列3：";
	cin>>c3;

	len1=StrLen(c1);
	len2=StrLen(c2);
	len3=StrLen(c3);
	char *c = new char[len1+len2+len3];

	for(i=0; i<len1; i++)
	{
		c[i]=c1[i];
	}
	for(i=0; i<len2; i++)
	{
		c[i+len1]=c2[i];
	}
	for(i=0; i<len3; i++)
	{
		c[i+len1+len2]=c3[i];
	}
	c[len1+len2+len3]='\0';

	cout<<"輸出字元陣列："<<c<<"\n";

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