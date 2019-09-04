#include <cstdlib>
#include <iostream>

using namespace std;

int main(void)
{
	int i, i2;
	for(i=0; i<10; i++)
	{
		//A
		cout<<"["<<i<<"]";
		//B
		for(i2=0; i2<10; i2++)
		{
			//C
			cout<<i2;
			//D
		}
		//E
		cout<<"\n";

	}
	
	system("pause");
	return 0;
}

/*
if(i==?)
{
	break;
}

if(i2==?)
{
	break;
}
*/