// CPOC1.cpp: ���������� ����� ����� ��� ����������� ����������.
//
//Осуществляется подгрузка блоков данных с элементами матрицы в кэш-память, причем при умножении вектора на матрицу, обход
//идет по столбцам, что замедляет выполнение программы
//В то время как при умножении матрицы на вектор, считывается элемент матрицы, и стоящие за ним подряд (то есть, программа идет по массиву подряд - одной строкой, читая данные в основном из кэша, 
//не совершая "скачков через строки", что забивает кэш).
#include "stdafx.h"
#include <iostream>
#include "time.h"
using namespace std;

void fill_arrays(int** a, int* b)
{
	for (int i = 0; i < 1000; i++)
	{
		b[i] = 0;
	}
	for (int i = 0; i < 1000; i++)
	{
		for (int j = 0; j < 1000; j++)
		{
			a[i][j] = 0;
		}
	}
	srand(time(NULL));
	for (int i = 0; i < 1000; i++)
	{
		for (int j = 0; j < 1000; j++)
		{
			a[i][j] = rand() % 3 - 1;
		}
	}
	srand(time(NULL));
	for (int i = 0; i < 1000; i++)
	{
		b[i] = rand() % 3 - 1;
	}
}

void multiply_array_on_vector(int **a, int* b, int* res_1)
{
	for (int i = 0; i < 1000; i++)
	{
		res_1[i] = 0;
	}
	for (int i = 0; i < 1000; i++)
	{
		for (int j = 0; j < 1000; j++)
		{
			res_1[i] += a[i][j] * b[j];
		}
	}
}

void multiply_vector_on_array(int **a, int* b, int* res_2)
{
	for (int i = 0; i < 1000; i++)
	{
		res_2[i] = 0;
	}
	for (int i = 0; i < 1000; i++)
	{
		for (int j = 0; j < 1000; j++)
		{
			res_2[i] += b[j] * a[j][i];
		}
	}
}


int main()
{	
	int i,j;
	int** a = new int*[1000];
	for (i = 0; i < 1000; i++)
	{
		a[i] = new int[1000];
	}
	int*b = new int[1000];

	int* r1 = new int[1000];
	int* r2 = new int[1000];

	for (i = 0; i < 20; i++)
	{
		fill_arrays(a, b);
		clock_t timer1 = clock();
		multiply_array_on_vector(a, b, r1);
		timer1 = clock() - timer1;
		clock_t timer2 = clock();
		multiply_vector_on_array(a, b, r2);
		timer2 = clock() - timer2;
		double time1 = (double)timer1/ CLOCKS_PER_SEC;
		double time2 = (double)timer2/ CLOCKS_PER_SEC;
		cout << i+1 << ": time for array on vector: " << time1 <<"s." << "; time for vector on array: " << time2  <<"s." << " ;" << endl;
	}
	system("pause");
    return 0;
}

