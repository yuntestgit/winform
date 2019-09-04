#include <cstdlib>
#include <iostream>
#include <time.h>

using namespace std;

int random(int, int);
int **matrix(int, int, int, int);
int **transpose_mat(int**, int, int);
void prtmat(int**, int, int);

int main(void)
{
	cout<<"15.亂數做出矩陣a[3][4]，呼叫副程式transpose_mat()求出a的轉置矩陣aT[4][3]再印出。\n";
	
	cout<<"a[3][4]：\n";
	srand((unsigned)time(NULL));
	int **a = matrix(3, 4, 0, 9);
	prtmat(a, 3, 4);
	cout<<"\n";

	cout<<"aT[4][3]：\n";
	int **aT = transpose_mat(a, 3, 4);
	prtmat(aT, 4, 3);

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

int **transpose_mat(int **matrix, int row, int column)
{
	int i, i2;
	int **r = new int*[column];
	for(i=0; i<column; i++)
	{
		r[i] = new int[row];
	}
	
	for(i=0; i<column; i++)
	{
		for(i2=0; i2<row; i2++)
		{
			r[i][i2]=matrix[i2][i];
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