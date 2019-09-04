#include <iostream>
#include <cstdlib>

using namespace std;

int main(void)
{
	while(true)
	{
		int i, i2, length=0;
		char a[999];

		cin>>a;

		while(a[length]!='\0')
		{
			length++;
		}

		i2=length-(length/3)*3;

		for(i=0; i<i2; i++)
		{
			cout<<a[i];
		}

		if(length>3)
		{
			if(i2!=0)
			{
				cout<<",";
			}
		}

		for(i=i2; i<length; i++)
		{
			cout<<a[i];
			if((i-i2)%3==2)
			{
				if(i!=length-1)
				{
					cout<<",";
				}
			}
		}

		cout<<"\n";
	}
	system("pause");
	return 0;
}