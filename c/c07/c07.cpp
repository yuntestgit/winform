#include <cstdlib>
#include <iostream>

using namespace std;

int StrLen(char*);

int main(void)
{
	cout<<"7.�I�s�Ƶ{��StrLen()�A��X�r���}�Ca[]���r���ƥءA(���]�A\\0)\n";
	cout<<"��J�r���}�Ca[]�G";

	char a[999];
	cin>>a;
	cout<<"Length = "<<StrLen(a)<<"\n";

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