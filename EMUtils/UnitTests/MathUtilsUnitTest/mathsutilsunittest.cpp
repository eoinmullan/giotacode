#include "CppUnitTest.h"

#include <vector>
#include <stdexcept>
#include "MathUtils.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;
using namespace std;

namespace MathUtilsUnitTest
{
	TEST_CLASS(TestGetDivisors)
	{
	public:
		TEST_METHOD(shouldGetDivisorsOfTypeInt)
		{
			const int arr1[] = {1, 2, 3};
			vector<int> expectedResult(arr1, arr1 + sizeof(arr1) / sizeof(arr1[0]) );
			Assert::IsTrue(expectedResult == MathUtils::GetDivisors(static_cast<int>(6)));

			const int arr2[] = {1};
			expectedResult.assign(arr2, arr2 + sizeof(arr2) / sizeof(arr2[0]) );
			Assert::IsTrue(expectedResult == MathUtils::GetDivisors(static_cast<int>(7)));

			const int arr3[] = {1, 2, 3, 4, 6, 9, 12, 18};
			expectedResult.assign(arr3, arr3 + sizeof(arr3) / sizeof(arr3[0]) );
			Assert::IsTrue(expectedResult == MathUtils::GetDivisors(static_cast<int>(36)));

			const int arr4[] = {1, 2, 3, 4, 5, 6, 10, 12, 15, 20, 30};
			expectedResult.assign(arr4, arr4 + sizeof(arr4) / sizeof(arr4[0]) );
			Assert::IsTrue(expectedResult == MathUtils::GetDivisors(static_cast<int>(60)));
		}
		
		TEST_METHOD(shouldGetDivisorsOfTypeT)
		{
			const char arr1[] = {1, 2, 3, 4, 5, 6, 10, 12, 15, 20, 30};
			vector<char> expectedResultChars(arr1, arr1 + sizeof(arr1) / sizeof(arr1[0]) );
			Assert::IsTrue(expectedResultChars == MathUtils::GetDivisors(static_cast<char>(60)));
			
			const unsigned int arr2[] = {1, 2, 3, 4, 5, 6, 10, 12, 15, 20, 30};
			vector<unsigned int> expectedResultUInts(arr2, arr2 + sizeof(arr2) / sizeof(arr2[0]) );
			Assert::IsTrue(expectedResultUInts == MathUtils::GetDivisors(static_cast<unsigned int>(60)));
			
			const long arr3[] = {1, 2, 3, 4, 5, 6, 10, 12, 15, 20, 30};
			vector<long> expectedResultLongs(arr3, arr3 + sizeof(arr3) / sizeof(arr3[0]) );
			Assert::IsTrue(expectedResultLongs == MathUtils::GetDivisors(static_cast<long>(60)));
			
			const unsigned long arr4[] = {1, 2, 3, 4, 5, 6, 10, 12, 15, 20, 30};
			vector<unsigned long> expectedResultULongs(arr4, arr4 + sizeof(arr4) / sizeof(arr4[0]) );
			Assert::IsTrue(expectedResultULongs == MathUtils::GetDivisors(static_cast<unsigned long>(60)));
		}
		
		TEST_METHOD(shouldReturnNoDivisorsOfZero)
		{
			vector<int> expectedResult;
			Assert::IsTrue(expectedResult == MathUtils::GetDivisors(static_cast<int>(0)));
		}
		
		TEST_METHOD(shouldReturnNoDivisorsOfNegativeNumber)
		{
			vector<int> expectedResult;
			Assert::IsTrue(expectedResult == MathUtils::GetDivisors(static_cast<int>(-60)));
		}
	};

	TEST_CLASS(TestGetSumOfDivisors)
	{
	public:		
		TEST_METHOD(shouldGetSumOfDivisorsOfTypeInt)
		{
			emu64 expectedResult = 6;
			Assert::AreEqual(expectedResult, MathUtils::GetSumOfDivisors(static_cast<int>(6)));

			expectedResult = 1;
			Assert::AreEqual(expectedResult, MathUtils::GetSumOfDivisors(static_cast<int>(7)));

			expectedResult = 55;
			Assert::AreEqual(expectedResult, MathUtils::GetSumOfDivisors(static_cast<int>(36)));

			expectedResult = 108;
			Assert::AreEqual(expectedResult, MathUtils::GetSumOfDivisors(static_cast<int>(60)));
		}
		
		TEST_METHOD(shouldGetSumOfDivisorsOfTypeT)
		{
			emu64 expectedResult = 108;
			Assert::AreEqual(expectedResult, MathUtils::GetSumOfDivisors(static_cast<char>(60)));

			Assert::AreEqual(expectedResult, MathUtils::GetSumOfDivisors(static_cast<unsigned int>(60)));

			Assert::AreEqual(expectedResult, MathUtils::GetSumOfDivisors(static_cast<long>(60)));

			Assert::AreEqual(expectedResult, MathUtils::GetSumOfDivisors(static_cast<unsigned long>(60)));
		}
		
		TEST_METHOD(shouldGetZeroSumOfDivisorsOfZero)
		{
			Assert::AreEqual(static_cast<emu64>(0), MathUtils::GetSumOfDivisors(static_cast<char>(0)));
		}
		
		TEST_METHOD(shouldGetZeroSumOfDivisorsOfNegativeNumber)
		{
			Assert::AreEqual(static_cast<emu64>(0), MathUtils::GetSumOfDivisors(static_cast<char>(-60)));
		}
	};

	TEST_CLASS(TestFactorial)
	{
	public:
		TEST_METHOD(shouldGetFactorialOfTypeInt)
		{
			emu64 expectedResult = 1;
			Assert::AreEqual(expectedResult, MathUtils::Factorial(1));

			expectedResult = 2;
			Assert::AreEqual(expectedResult, MathUtils::Factorial(2));

			expectedResult = 6;
			Assert::AreEqual(expectedResult, MathUtils::Factorial(3));

			expectedResult = 120;
			Assert::AreEqual(expectedResult, MathUtils::Factorial(5));

			expectedResult = 3628800;
			Assert::AreEqual(expectedResult, MathUtils::Factorial(10));

			expectedResult = 2432902008176640000;
			Assert::AreEqual(expectedResult, MathUtils::Factorial(20));
		}

		TEST_METHOD(shouldGetFactorialOfTypeT)
		{
			emu64 expectedResult = 3628800;
			Assert::AreEqual(expectedResult, MathUtils::Factorial(static_cast<char>(10)));

			Assert::AreEqual(expectedResult, MathUtils::Factorial(static_cast<unsigned int>(10)));

			Assert::AreEqual(expectedResult, MathUtils::Factorial(static_cast<long>(10)));

			Assert::AreEqual(expectedResult, MathUtils::Factorial(static_cast<unsigned long>(10)));
		}
		
		TEST_METHOD(shouldGiveOneForFactorialOfZero)
		{
			Assert::AreEqual(static_cast<emu64>(1), MathUtils::Factorial(0));
		}

		TEST_METHOD(shouldThrowExceptionForFactorialOfNegative)
		{
			Assert::ExpectException<std::out_of_range>( [ ] { MathUtils::Factorial(-1); });
		}

		TEST_METHOD(shouldThrowExceptionWhenResultIsLargerThan64Bits)
		{
			Assert::ExpectException<std::out_of_range>( [ ] { MathUtils::Factorial(21); });
		}
	};

	TEST_CLASS(TestPowerOf)
	{
		TEST_METHOD(shouldReturnOneForAnythingToThePowerOfZero)
		{
			Assert::AreEqual(1, MathUtils::PowerOf(2, 0));
			Assert::AreEqual(1, MathUtils::PowerOf(0, 0));
			Assert::AreEqual(1, MathUtils::PowerOf(-2, 0));			
		}

		TEST_METHOD(shouldReturnBaseWhenExponentIsOne)
		{
			Assert::AreEqual(5, MathUtils::PowerOf(5, 1));
			Assert::AreEqual(2, MathUtils::PowerOf(2, 1));
			Assert::AreEqual(0, MathUtils::PowerOf(0, 1));
			Assert::AreEqual(-2, MathUtils::PowerOf(-2, 1));
			Assert::AreEqual(-5, MathUtils::PowerOf(-5, 1));
		}

		TEST_METHOD(shouldReturnBaseToThePowerOfExponentForTypeInt)
		{
			Assert::AreEqual(2, MathUtils::PowerOf(2, 1));
			Assert::AreEqual(4, MathUtils::PowerOf(2, 2));
			Assert::AreEqual(8, MathUtils::PowerOf(2, 3));
			Assert::AreEqual(1024, MathUtils::PowerOf(2, 10));
			
			Assert::AreEqual(243, MathUtils::PowerOf(3, 5));
			Assert::AreEqual(3125, MathUtils::PowerOf(5, 5));
			Assert::AreEqual(4782969, MathUtils::PowerOf(9, 7));
		}

		TEST_METHOD(shouldReturnBaseToThePowerOfExponentForTypeT)
		{
			Assert::AreEqual(static_cast<char>(81), MathUtils::PowerOf(static_cast<char>(3), static_cast<char>(4)));
			Assert::AreEqual(static_cast<long>(81), MathUtils::PowerOf(static_cast<long>(3), static_cast<long>(4)));
			Assert::AreEqual(static_cast<unsigned long>(81), MathUtils::PowerOf(static_cast<unsigned long>(3), static_cast<unsigned long>(4)));
			Assert::AreEqual(static_cast<unsigned int>(81), MathUtils::PowerOf(static_cast<unsigned int>(3), static_cast<unsigned int>(4)));
		}

		TEST_METHOD(shouldThrowOutOfRangeForNegativeExponents)
		{
			Assert::ExpectException<std::out_of_range>( [] { MathUtils::PowerOf(2, -1); } );
		}
	};
	
	TEST_CLASS(TestGetPrimesBoolArrayToN)
	{
		TEST_METHOD(shouldReturnSingleFalseElementVectorForZeroInput)
		{
			vector<bool> expectedResultVector;
			expectedResultVector.push_back(false);
			Assert::IsTrue(expectedResultVector == MathUtils::GetPrimesBoolArrayToN(0));
		}

		TEST_METHOD(shouldReturnVectorOfLengthOneGreaterThanInputArgument)
		{
			Assert::AreEqual(static_cast<size_t>(1), MathUtils::GetPrimesBoolArrayToN(0).size());
			Assert::AreEqual(static_cast<size_t>(2), MathUtils::GetPrimesBoolArrayToN(1).size());
			Assert::AreEqual(static_cast<size_t>(11), MathUtils::GetPrimesBoolArrayToN(10).size());
			Assert::AreEqual(static_cast<size_t>(1001), MathUtils::GetPrimesBoolArrayToN(1000).size());
		}

		TEST_METHOD(shouldThrowOutOfRangeForNegativeNumber)
		{
			Assert::ExpectException<std::out_of_range>([] { MathUtils::GetPrimesBoolArrayToN(-1); });
		}

		TEST_METHOD(shouldFindAllPrimesBelowFiftyForTypeT)
		{
			vector<bool> expectedResultVector(51, false);
			expectedResultVector[2] = true;
			expectedResultVector[3] = true;
			expectedResultVector[5] = true;
			expectedResultVector[7] = true;
			expectedResultVector[11] = true;
			expectedResultVector[13] = true;
			expectedResultVector[17] = true;
			expectedResultVector[19] = true;
			expectedResultVector[23] = true;
			expectedResultVector[29] = true;
			expectedResultVector[31] = true;
			expectedResultVector[37] = true;
			expectedResultVector[41] = true;
			expectedResultVector[43] = true;
			expectedResultVector[47] = true;
			Assert::IsTrue(expectedResultVector == MathUtils::GetPrimesBoolArrayToN(50));
			Assert::IsTrue(expectedResultVector == MathUtils::GetPrimesBoolArrayToN(static_cast<char>(50)));
			Assert::IsTrue(expectedResultVector == MathUtils::GetPrimesBoolArrayToN(static_cast<unsigned int>(50)));
			Assert::IsTrue(expectedResultVector == MathUtils::GetPrimesBoolArrayToN(static_cast<long>(50)));
			Assert::IsTrue(expectedResultVector == MathUtils::GetPrimesBoolArrayToN(static_cast<unsigned long>(50)));
		}
	};

	TEST_CLASS(TestGetPrimesToN)
	{
		TEST_METHOD(shouldReturnEmptyVectorForZeroInput)
		{
			vector<int> emptyVector;
			Assert::IsTrue(emptyVector == MathUtils::GetPrimesToN(0));
		}
		
		TEST_METHOD(shouldReturnEmptyVectorForInputOfOne)
		{
			vector<int> emptyVector;
			Assert::IsTrue(emptyVector == MathUtils::GetPrimesToN(1));
		}
		
		TEST_METHOD(shouldFindAllPrimesBelowFiftyForTypeT)
		{
			const int arr[] = {2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47};
			vector<int> expectedInts(arr, arr + sizeof(arr) / sizeof(arr[0]) );
			Assert::IsTrue(expectedInts == MathUtils::GetPrimesToN(50));

			vector<char> expectedChars(expectedInts.begin(), expectedInts.end());
			Assert::IsTrue(expectedChars == MathUtils::GetPrimesToN(static_cast<char>(50)));

			vector<unsigned int> expectedUInts(expectedInts.begin(), expectedInts.end());
			Assert::IsTrue(expectedUInts == MathUtils::GetPrimesToN(static_cast<unsigned int>(50)));

			vector<long> expectedLongs(expectedInts.begin(), expectedInts.end());
			Assert::IsTrue(expectedLongs == MathUtils::GetPrimesToN(static_cast<long>(50)));

			vector<unsigned long> expectedULongs(expectedInts.begin(), expectedInts.end());
			Assert::IsTrue(expectedULongs == MathUtils::GetPrimesToN(static_cast<unsigned long>(50)));
		}

		TEST_METHOD(shouldThrowOutOfRangeForNegativeNumber)
		{
			Assert::ExpectException<std::out_of_range>([] { MathUtils::GetPrimesToN(-1); });
		}
	};

	TEST_CLASS(TestToAlternateBaseRepresentation)
	{
		TEST_METHOD(shouldConvertZeroToZeroInAnyBase)
		{
			Assert::AreEqual(string("0"), MathUtils::ToAlternateBaseRepresentation(0, 2));
			Assert::AreEqual(string("0"), MathUtils::ToAlternateBaseRepresentation(0, 10));
			Assert::AreEqual(string("0"), MathUtils::ToAlternateBaseRepresentation(0, 16));
		}

		TEST_METHOD(shouldConvertOneToOneInAnyBase)
		{
			Assert::AreEqual(string("1"), MathUtils::ToAlternateBaseRepresentation(1, 2));
			Assert::AreEqual(string("1"), MathUtils::ToAlternateBaseRepresentation(1, 10));
			Assert::AreEqual(string("1"), MathUtils::ToAlternateBaseRepresentation(1, 16));
		}

		TEST_METHOD(shouldReturnStringRepresentationOfAnyNumberInBaseTen)
		{
			Assert::AreEqual(string("1"), MathUtils::ToAlternateBaseRepresentation(1, 10));
			Assert::AreEqual(string("10"), MathUtils::ToAlternateBaseRepresentation(10, 10));
			Assert::AreEqual(string("10000"), MathUtils::ToAlternateBaseRepresentation(10000, 10));
			Assert::AreEqual(string("10001"), MathUtils::ToAlternateBaseRepresentation(10001, 10));
			Assert::AreEqual(string("51"), MathUtils::ToAlternateBaseRepresentation(51, 10));
			Assert::AreEqual(string("9999"), MathUtils::ToAlternateBaseRepresentation(9999, 10));
		}

		TEST_METHOD(shouldConvertNumbersToBinaryRepresentation)
		{
			Assert::AreEqual(string("1"), MathUtils::ToAlternateBaseRepresentation(1, 2));
			Assert::AreEqual(string("1010"), MathUtils::ToAlternateBaseRepresentation(10, 2));
			Assert::AreEqual(string("1111111"), MathUtils::ToAlternateBaseRepresentation(127, 2));
			Assert::AreEqual(string("10000000"), MathUtils::ToAlternateBaseRepresentation(128, 2));
		}

		TEST_METHOD(shouldConvertNumbersToTenaryRepresentation)
		{
			Assert::AreEqual(string("1"), MathUtils::ToAlternateBaseRepresentation(1, 3));
			Assert::AreEqual(string("101"), MathUtils::ToAlternateBaseRepresentation(10, 3));
			Assert::AreEqual(string("11201"), MathUtils::ToAlternateBaseRepresentation(127, 3));
			Assert::AreEqual(string("1101000"), MathUtils::ToAlternateBaseRepresentation(999, 3));
		}

		TEST_METHOD(shouldConvertAnyNumberToAnyBaseRepresentation)
		{
			Assert::AreEqual(string("33213"), MathUtils::ToAlternateBaseRepresentation(999, 4));
			Assert::AreEqual(string("12444"), MathUtils::ToAlternateBaseRepresentation(999, 5));
			Assert::AreEqual(string("4343"), MathUtils::ToAlternateBaseRepresentation(999, 6));
			Assert::AreEqual(string("2625"), MathUtils::ToAlternateBaseRepresentation(999, 7));
			Assert::AreEqual(string("1747"), MathUtils::ToAlternateBaseRepresentation(999, 8));
			Assert::AreEqual(string("1330"), MathUtils::ToAlternateBaseRepresentation(999, 9));
			Assert::AreEqual(string("829"), MathUtils::ToAlternateBaseRepresentation(999, 11));
			Assert::AreEqual(string("6B3"), MathUtils::ToAlternateBaseRepresentation(999, 12));
			Assert::AreEqual(string("5BB"), MathUtils::ToAlternateBaseRepresentation(999, 13));
			Assert::AreEqual(string("515"), MathUtils::ToAlternateBaseRepresentation(999, 14));
			Assert::AreEqual(string("469"), MathUtils::ToAlternateBaseRepresentation(999, 15));
			Assert::AreEqual(string("3E7"), MathUtils::ToAlternateBaseRepresentation(999, 16));
		}
		
		TEST_METHOD(shouldThrowOutOfRangeForNegativeBases)
		{
			Assert::ExpectException<std::out_of_range>([] { MathUtils::GetPrimesBoolArrayToN(-1); });
			Assert::ExpectException<std::out_of_range>([] { MathUtils::ToAlternateBaseRepresentation(999, -1); });
			Assert::ExpectException<std::out_of_range>([] { MathUtils::ToAlternateBaseRepresentation(999, -99); });
		}

		TEST_METHOD(shouldThrowOutOfRangeForBasesGreaterThan16)
		{
			Assert::ExpectException<std::out_of_range>([] { MathUtils::ToAlternateBaseRepresentation(999, 17);} );
			Assert::ExpectException<std::out_of_range>([] { MathUtils::ToAlternateBaseRepresentation(999, 99);} );
		}
	};
}