#include <cstdlib>
#include <iostream>
#include <string>

using namespace std;

long _pow(long, long);

int main(void)
{
	cout<<"3.��J�@�ӳ��O�Ʀr���r����s[]�A�N���[�W886���x�s�b������ܼ�n���A�L�X�C\n";
	cout<<"��J�r��s[]�G";

	string s;
	int i, len;
	long n=0;

	cin>>s;
	len=s.length();
	for(i=len-1; i>=0; i--)
	{
		n+=(s[i]-'0')*_pow(10, len-i-1);
	}
	n+=886;

	cout<<"n = "<<n<<"\n";

	system("pause");
	return 0;
}

long _pow(long a, long b)
{
	if(b==0)
	{
		return 1;
	}
	else
	{
		long c=1;
		for(int i=1; i<=b; i++)
		{
			c*=a;
		}
		return c;
	}
}