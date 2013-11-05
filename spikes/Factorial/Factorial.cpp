// Factorial.cpp : Defines the entry point for the console application.
//

#include <tchar.h>
#include <iostream>
#include "EMUtils.h"

using namespace std;

typedef unsigned long long ull;

#pragma inline_recursion( on )
#pragma inline_depth( 5 )
__inline ull RecursiveFactorial(const ull& value)
{
	return (value==1) ? 1 : value * RecursiveFactorial(value-1);
}

template <typename T>
T ForLoopFactorial(const T& value)
{
	T returnValue = 1;
	for (T i = 2; i <= value; i++) {
		returnValue *= i;
	}
	return returnValue;
}

int _tmain(int argc, wchar_t* argv[])
{
	unsigned long long total = 0;
	EMUtils::StartTimer();
	for (ull i=1; i<10000000; i++) {
		for (ull j=1; j<20; j++) {
			//total += ForLoopFactorial(j);
			total += RecursiveFactorial(j);
		}
	}
	cout << "Total: " << total << endl;
	cout << "Total time: " << EMUtils::GetLapTimeMs() << endl;

	system("pause");
	return 0;
}