#include <cstdlib>
#include <iostream>

using namespace std;

char *StrLeftN(char*, int);

int main(void)
{
	cout<<"10.�I�s�Ƶ{��StrLeftN()�N�r���}�Ca[]����N�Ӧr����Jb[]�A�A�L�Xa[]�Ab[]�����e�C\n";
	char a[999], *b;
	int n;
	cout<<"��J�r���}�Ca[]�G";
	cin>>a;
	cout<<"��J���N�G";
	cin>>n;

	b=StrLeftN(a, n);
	cout<<"��X�r���}�Ca[]�G"<<a<<"\n��X�r���}�Cb[]�G"<<b<<"\n";

	system("pause");
	return 0;
}

char *StrLeftN(char *a, int n)
{
	char *c = new char[n];
	for(int i=0; i<n; i++)
	{
		c[i]=a[i];
	}
	c[n]='\0';
	return c;
}