#pragma once

#include "MathUtils.h"
#include <iomanip>
#include <string>
#include <sstream>

class embiguint
{
public:
	embiguint();
	template <typename T>embiguint(T num);
	~embiguint(void) {}
	
	bool areEqual (const embiguint& rhs) const;
	bool isLessThan (const embiguint& rhs) const;
	embiguint& operator+= (const embiguint& rhs);
	template <typename T> embiguint& operator+= (const T& num);

	std::string Get() const;
	emuint GetNoDigits() const;

private:
	void fixDigits();

	std::vector<emuint> number;
	static const emuint noDigits = 9;
	emuint digitMask;
};

bool operator== (const embiguint& lhs, const embiguint& rhs) { return lhs.areEqual(rhs); }
bool operator!= (const embiguint& lhs, const embiguint& rhs) { return !lhs.areEqual(rhs); }
bool operator< (const embiguint& lhs, const embiguint& rhs) { return lhs.isLessThan(rhs); }
bool operator> (const embiguint& lhs, const embiguint& rhs) { return rhs.isLessThan(lhs); }
bool operator<= (const embiguint& lhs, const embiguint& rhs) { return !rhs.isLessThan(lhs); }
bool operator>= (const embiguint& lhs, const embiguint& rhs) { return !lhs.isLessThan(rhs); }
const embiguint operator+ (const embiguint& lhs, const embiguint& rhs);

template <typename T>embiguint::embiguint(T num)
	: digitMask(MathUtils::PowerOf(emuint(10), noDigits))
{
	while (num > 0) {
		number.push_back(num % digitMask);
		num /= digitMask;
	}
}

embiguint::embiguint()
	: digitMask(MathUtils::PowerOf(emuint(10), noDigits))
{
}

bool embiguint::areEqual (const embiguint& rhs) const
{
	bool areEqual = true;

	if (number.size() != rhs.number.size()) {
		areEqual = false;
	}
	else {
		for (emuint i=0; i<number.size() && areEqual; i++) {
			if (number[i] != rhs.number[i]) {
				areEqual = false;
			}
		}
	}

	return areEqual;
}

bool embiguint::isLessThan (const embiguint& rhs) const
{
	bool lhsLessThanRhs = false;

	if (number.size() < rhs.number.size()) {
		lhsLessThanRhs = true;
	}
	else if (number.size() == rhs.number.size()) {
		int i = number.size()-1;
		while (i >= 0 && !lhsLessThanRhs) {
			if (number[i] == rhs.number[i]) {
				i--;
			}
			else {
				if (number[i] < rhs.number[i]) {
					lhsLessThanRhs = true;
				}
				break;
			}
		}
	}
	
	return lhsLessThanRhs;
}

const embiguint operator+ (const embiguint& lhs, const embiguint& rhs)
{
	embiguint temp(lhs);
	temp += rhs;
	return temp;
}

embiguint& embiguint::operator+= (const embiguint& rhs)
{
	if (number.size() < rhs.number.size()) {
		for (emuint i=number.size(); i<rhs.number.size(); i++) {
			number.push_back(0);
		}
	}

	for (emuint i=0; i<rhs.number.size(); i++) {
		number[i] += rhs.number[i];
	}

	fixDigits();

	return *this;
}

template <typename T>embiguint& embiguint::operator+= (const T& num)
{
	T numCopy = num;
	emuint counter = 0;
	while (numCopy > 0 && counter < number.size()) {
		number[counter++] += numCopy % digitMask;
		numCopy /= digitMask;
	}
	while (numCopy > 0) {
		number.push_back(numCopy % digitMask);
		numCopy /= digitMask;
	}
	fixDigits();

	return *this;
}

std::string embiguint::Get() const
{
	std::stringstream retString;
	if (number.size() != 0) {
		retString << number[number.size()-1];
		for (int i=number.size()-2; i>=0; i--) {
			retString << std::setfill('0') << std::setw(noDigits) << number[i];
		}
	}
	return retString.str();
}

emuint embiguint::GetNoDigits() const
{
	return (number.size()-1)*noDigits + std::to_string(number[number.size()-1]).size();
}

void embiguint::fixDigits()
{
	if (number.size() > 0 ) {
		if (number.size() == 1) {
			if (number[0] >= digitMask) {
				number.push_back(number[0] / digitMask);
				number[0] %= digitMask;
			}
		}
		else {
			for (emuint i=0; i<number.size()-1; i++) {
				if (number[i] >= digitMask) {
					number[i+1] += number[i] / digitMask;
					number[i] %= digitMask;
				}
			}
			if (number[number.size()-1] >= digitMask) {
				number.push_back(number[number.size()-1]/digitMask);
				number[number.size()-2] %= digitMask;
			}
		}
	}
}