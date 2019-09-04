#include <cstdlib>
#include <iostream>

using namespace std;

int StrLen(char*);

int main(void)
{
	cout<<"4.輸入兩個字元陣列，將它們連接成一新字元陣列再印出。\n";
	char c1[999], c2[999];
	int len1, len2, i;

	cout<<"輸入字元陣列1：";
	cin>>c1;
	cout<<"輸入字元陣列2：";
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