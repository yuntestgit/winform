#include <cstdlib>
#include <iostream>

using namespace std;

char *StrCutMN(char*, int, int);

int main(void)
{
	cout<<"10.�I�s�Ƶ{��StrCutMN()�N�r���}�Ca[]��M���N�Ӧr����Jb[]�A�A�L�Xa[]�Ab[]�����e�C\n";
	char a[999], *b;
	int m, n;
	cout<<"��J�r���}�Ca[]�G";
	cin>>a;
	cout<<"��J���M�G";
	cin>>m;
	cout<<"��J���N�G";
	cin>>n;

	b=StrCutMN(a, m, n);
	cout<<"��X�r���}�Ca[]�G"<<a<<"\n��X�r���}�Cb[]�G"<<b<<"\n";

	system("pause");
	return 0;
}

char *StrCutMN(char *a,int m, int n)
{
	char *c = new char[n-m+1];
	for(int i=m; i<=n; i++)
	{
		c[i-m]=a[i];
		if(i==n)
		{
			c[i-m+1]='\0';
		}
	}
	return c;
}