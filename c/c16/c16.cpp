#include <cstdlib>
#include <iostream>
#include <time.h>

using namespace std;

int random(int, int);
void prt_mat(int[], int, int);

int main(void)
{
	cout<<"16.亂數做出正整數陣列a[30]，呼叫prt_mat()將a[30]印成每列column行之矩陣。(column為輸入值)\n";
	
	srand((unsigned)time(NULL));

	int a[30], i, column;
	for(i=0; i<30; i++)
	{
		a[i]=random(0, 9);
		cout<<"a["<<i<<"]="<<a[i]<<"\n";
	}

	cout<<"輸入column：";
	cin>>column;
	cout<<"prt_mat()輸出：\n";
	prt_mat(a, 30, column);

	system("pause");
	return 0;
}

int random(int min, int max)
{
	int r = (rand()%(max-min+1))+min;
	return r;
}

void prt_mat(int a[], int length, int column)
{
	for(int i=0; i<length; i++)
	{
		if((i+1)%column==0)
		{
			cout<<a[i]<<"\n";
		}
		else
		{
			cout<<a[i]<<" ";
		}
	}
}