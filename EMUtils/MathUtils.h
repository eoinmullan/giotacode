#pragma once

#include <vector>
#include <algorithm>
#include <numeric>
#include <stdexcept>
#include <stdlib.h>
#include <sstream>
#include "emvartypes.h"

namespace MathUtils
{
	template <typename T> std::string ToAlternateBaseRepresentation(const T& value, const T& base);
	template <typename T> std::vector<T> GetDivisors(const T& number);
	template <typename T> emu64 GetSumOfDivisors(const T& number);
	template <typename T> emu64 Factorial(const T& x);
	template <typename T> T PowerOf(const T& base, const T& power);
	template <typename T> std::vector<bool> GetPrimesBoolArrayToN(const T& n);
	template <typename T> std::vector<T> GetPrimesToN(const T& n);
	template <typename T> bool AreClose(const T& first, const T& second, const T& tolerance);
	template <typename T> T GetNumberOfDigits(const T& value, const T base = 10);
	
	template <typename T> std::string uncheckedToAlternateBaseRepresentation(const T& value, const T& base);
};

template <typename T> 
std::string MathUtils::ToAlternateBaseRepresentation(const T& value, const T& base)
{
	if (base < 0) {
		throw std::out_of_range("Cannot convert number to a negative base representation");
	}
	else if (base > 16) {
		throw std::out_of_range("Cannot convert number to greater than base 16 representation");
	}

	return uncheckedToAlternateBaseRepresentation(value, base);
}

template <typename T> 
std::string MathUtils::uncheckedToAlternateBaseRepresentation(const T& value, const T& base)
{
	stringstream ss;

	if (value >= base) {
		ss << uncheckedToAlternateBaseRepresentation(value/base, base);
	}
	auto digit = value%base;
	if (value%base > 9) {
		ss << static_cast<char>(digit - 10 + 'A');
	}
	else {
		ss << digit;
	}

	return ss.str();
}

template <typename T>
std::vector<T> MathUtils::GetDivisors(const T& number)
{
	vector<T> divisors;
	
	auto rootOfValue = static_cast<emuint>(sqrt(number));

	if (number > 0) {
		divisors.push_back(1);
		for (emuint i=2; i<=rootOfValue; i++) {
			if (i*i == number) {
				divisors.push_back(i);
			}
			else if (number%i==0) {
				divisors.push_back(i);
				divisors.push_back(number/i);
			}
		}
	}
	sort(divisors.begin(), divisors.end());
	return std::move(divisors);
}

template <typename T>
emu64 MathUtils::GetSumOfDivisors(const T& number)
{
	vector<T> temp = GetDivisors(number);
	return std::accumulate(temp.begin(), temp.end(), 0);
}

template <typename T>
emu64 MathUtils::Factorial(const T& value)
{
	if (value < 0) {
		throw std::out_of_range("Cannot calculate factorial of negative number");
	}
	else if (value > 20) {
		throw std::out_of_range("Result of factorial cannot be represented with 64 bits");
	}
	
	emu64 returnValue = 1;
	for (emu64 i = 2; i <= value; i++) {
		returnValue *= i;
	}
	return returnValue;
}

template <typename T>
T MathUtils::PowerOf(const T& base, const T& exponent)
{
	T result;

	if (0 == exponent) {
		result = 1;
	}
	else if (exponent < 0) {
		throw std::out_of_range("This function cannot calculate with negative exponents");
	}
	else {
		result = base;
		for (T i=1; i<exponent; i++) {
			result *= base;
		}
	}

	return result;
}

template <typename T>
std::vector<bool> MathUtils::GetPrimesBoolArrayToN(const T& value)
{
	if (value < 0) {
		throw std::out_of_range("Cannot get primes between zero and a negative number");
	}

	// Determine primes using Sieve of Eratosthenes
	std::vector<bool> primeArray(value+1, true);
	primeArray[0] = false;
	if (value > 0) {
		primeArray[1] = false;
		T i=2;
		while (i<value) {
			if (primeArray[i]) {
				T j=i+i;
				while (j<=value) {
					primeArray[j] = false;
					j += i;
				}
			}
			i++;
		}
	}

	return std::move(primeArray);
}

template <typename T> 
std::vector<T> MathUtils::GetPrimesToN(const T& n)
{
	auto nums = GetPrimesBoolArrayToN(n);
	std::vector<T> primes;

	for (T i=0; i<=n; i++) {
		if (nums[i]) {
			primes.push_back(i);
		}
	}

	return std::move(primes);
}

template <typename T>
bool MathUtils::AreClose(const T& first, const T& second, const T& tolerance)
{
	return abs(static_cast< std::make_signed<T>::type >(first-second)) <= static_cast< std::make_signed<T>::type >(tolerance);
}

template <>
bool MathUtils::AreClose(const double& first, const double& second, const double& tolerance)
{
	return fabs(first-second) <= tolerance;
}

template <>
bool MathUtils::AreClose(const float& first, const float& second, const float& tolerance)
{
	return fabs(first-second) <= tolerance;
}

template <typename T> T MathUtils::GetNumberOfDigits(const T& value, const T base /*= 10*/)
{
    T digits = 0;
	auto absoluteValue = abs(value);
    while (absoluteValue) {
        absoluteValue /= base;
        digits++;
    }
    return digits;
}
