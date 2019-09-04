#include <cstdlib>
#include <iostream>
#include <string>

using namespace std;

long _pow(long, long);

int main(void)
{
	cout<<"3.輸入一個都是數字的字串資料s[]，將它加上886後儲存在長整數變數n中再印出。\n";
	cout<<"輸入字串s[]：";

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