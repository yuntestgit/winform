#include <cstdlib>
#include <iostream>
#include <string>

using namespace std;

int _count(long);

int main(void)
{
	cout<<"2.��J�@����Ƹ�ơA�N���x�s�b�r���ܼƤ��A�L�X�C\n";
	cout<<"��J����Ƹ�ơG";

	long l;
	int len, i=0;
	string s="";
	
	cin>>l;
	len=_count(l);

	char *c = new char[len];
	for(i=0; i<len; i++)
	{
		c[i]=l%10+'0';
		l/=10;
		s=c[i]+s;
	}

	cout<<"�r��G"<<s<<"\n";

	system("pause");
	return 0;
}

int _count(long l)
{
	int len=0;
	while(l!=0)
	{
		l/=10;
		len++;
	}
	return len;
}