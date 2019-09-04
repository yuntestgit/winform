#include <cstdlib>
#include <iostream>
#include <time.h>

using namespace std;

int random(int, int);
int **matrix(int, int, int, int);
int **matrix_times(int**, int, int, int**, int, int);
void prtmat(int**, int, int);

int main(void)
{
	cout<<"6.亂數做出兩矩陣a[3][3]，b[3][4]，元素均為2~8。印出a與b相乘的矩陣c[3][4]。\n";
	
	cout<<"a[3][3]：\n";
	srand((unsigned)time(NULL));
	int **a = matrix(3, 3, 2, 8);
	prtmat(a, 3, 3);
	cout<<"\n";

	cout<<"b[3][4]：\n";
	int **b = matrix(3, 4, 2, 8);
	prtmat(b, 3, 4);
	cout<<"\n";

	cout<<"c[3][4]：\n";
	int **c = matrix_times(a, 3, 3, b, 4, 4);
	prtmat(c, 3, 4);
	
	system("pause");
	return 0;
}

int random(int min, int max)
{
	int r = (rand()%(max-min+1))+min;
	return r;
}

int **matrix(int row, int column, int min, int max)
{
	int i, i2;
	int **r = new int*[row];
	for(i=0; i<row; i++)
	{
		r[i] = new int[column];
	}
	
	for(i=0; i<row; i++)
	{
		for(i2=0; i2<column; i2++)
		{
			r[i][i2]=random(min, max);
		}
	}
	return r;
}

int **matrix_times(int **m1, int r1, int c1, int **m2, int r2, int c2)
{
	int i, i2, i3;
	int **r = new int*[r1];
	for(i=0; i<r1; i++)
	{
		r[i] = new int[c2];
	}
	
	for(i=0; i<r1; i++)
	{
		for(i2=0; i2<c2; i2++)
		{
			r[i][i2]=0;
			for(i3=0; i3<c1; i3++)
			{
				r[i][i2]+=m1[i][i3]*m2[i3][i2];
			}
		}
	}
	return r;
}

void prtmat(int **matrix, int row, int column)
{
	for(int i=0; i<row; i++)
	{
		for(int j=0; j<column; j++)
		{
			cout<<matrix[i][j]<<" ";
		}
		cout<<"\n";
	}
}