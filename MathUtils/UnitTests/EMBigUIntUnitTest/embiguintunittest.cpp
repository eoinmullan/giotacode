#include "CppUnitTest.h"
#include "embiguint.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;
using namespace std;

namespace embiguintUnitTest
{
	TEST_CLASS(UnitTest1)
	{
	public:
		
		TEST_METHOD(shouldReturnValueSetInConstructor)
		{
			Assert::AreEqual(string(""), embiguint(0).Get());
			Assert::AreEqual(string(""), embiguint().Get());
			Assert::AreEqual(string("12"), embiguint(12).Get());
			Assert::AreEqual(string("12345678"), embiguint(12345678).Get());
			Assert::AreEqual(string("123456789"), embiguint(123456789).Get());
			Assert::AreEqual(string("1234567890"), embiguint(1234567890).Get());
			Assert::AreEqual(string("12345678910111213141"), embiguint(12345678910111213141).Get());
		}

		TEST_METHOD(shouldBeCopyConstructable)
		{
			embiguint test1(123);
			embiguint test2(test1);
			Assert::AreEqual(test1.Get(), test2.Get());
			test2 += 1;
			Assert::AreNotEqual(test1.Get(), test2.Get());
		}

		TEST_METHOD(shouldCompareEqualembiguintsAsEqual)
		{
			Assert::IsTrue(embiguint(0) == embiguint(0));
			Assert::IsTrue(embiguint(20) == embiguint(20));
			Assert::IsTrue(embiguint(12345678910111213141) == embiguint(12345678910111213141));
		}

		TEST_METHOD(shouldBeComparableWithIntergalTypes)
		{
			Assert::IsTrue(20 == embiguint(20));
			Assert::IsTrue(embiguint(20) == 20);
			Assert::IsFalse(19 == embiguint(20));
			Assert::IsFalse(embiguint(20) == 21);
			Assert::IsFalse(20 != embiguint(20));
			Assert::IsFalse(embiguint(20) != 20);
			Assert::IsTrue(19 != embiguint(20));
			Assert::IsTrue(embiguint(20) != 21);
		}

		TEST_METHOD(shouldNotCompareDifferentembiguintsAsEqual)
		{
			Assert::IsFalse(embiguint(1) == embiguint(0));
			Assert::IsFalse(embiguint(20) == embiguint(12345678910111213141));
			Assert::IsFalse(20 == embiguint(12345678910111213141));
			Assert::IsFalse(embiguint(20) == 12345678910111213141);
			Assert::IsFalse(embiguint(12345678910111213141) == embiguint(12345678910111213142));
			Assert::IsFalse(embiguint(12345678910111213141) == embiguint(13345678910111213141));
		}

		TEST_METHOD(shouldCompareDifferentembiguintsUnequal)
		{
			Assert::IsTrue(embiguint(1) != embiguint(0));
			Assert::IsTrue(embiguint(20) != embiguint(12345678910111213141));
			Assert::IsTrue(20 != embiguint(12345678910111213141));
			Assert::IsTrue(embiguint(12345678910111213141) != 12345678910111213142);
			Assert::IsTrue(embiguint(12345678910111213141) != embiguint(13345678910111213141));
		}

		TEST_METHOD(shouldNotCompareEqualembiguintsUnequal)
		{
			Assert::IsFalse(embiguint(0) != embiguint(0));
			Assert::IsFalse(embiguint(20) != embiguint(20));
			Assert::IsFalse(20 != embiguint(20));
			Assert::IsFalse(embiguint(20) != 20);
			Assert::IsFalse(embiguint(12345678910111213141) != embiguint(12345678910111213141));
		}

		TEST_METHOD(shouldProvideAdditionAssignmentForTypeT)
		{
			embiguint test(10);
			test += 10;
			Assert::AreEqual(string("20"), test.Get());
			test += 100;
			Assert::AreEqual(string("120"), test.Get());
			test += 30000;
			Assert::AreEqual(string("30120"), test.Get());
		}

		TEST_METHOD(shouldAddLargeTypeTToSmallEmbigint)
		{
			embiguint test(10);
			test += 5000000000000000000;
			Assert::AreEqual(string("5000000000000000010"), test.Get());
			test += 5000000000000000000;
			Assert::AreEqual(string("10000000000000000010"), test.Get());
		}

		TEST_METHOD(shouldAddSmallTypeTToLargeEmbigint)
		{
			embiguint test(9999999999999999999);
			test += 1;
			test += 9999999999999999999;
			test += 1;
			Assert::AreEqual(string("20000000000000000000"), test.Get());
		}

		TEST_METHOD(shouldProvideAdditionAssignmentForTypeEmbiguint)
		{
			embiguint testNumber(10);
			embiguint test10(10);
			testNumber += test10;
			Assert::AreEqual(string("20"), testNumber.Get());
			embiguint test100(100);
			testNumber += test100;
			Assert::AreEqual(string("120"), testNumber.Get());			
			embiguint test30000(30000);
			testNumber += test30000;
			Assert::AreEqual(string("30120"), testNumber.Get());			
			embiguint testBigNo(5000000000000000000);
			testNumber += testBigNo;
			Assert::AreEqual(string("5000000000000030120"), testNumber.Get());
			testNumber += testBigNo;
			Assert::AreEqual(string("10000000000000030120"), testNumber.Get());
		}

		TEST_METHOD(shouldProvideAdditionOperatorForTypeEmbiguint)
		{
			Assert::AreEqual(string("30100"), (embiguint(100) + embiguint(30000)).Get());
			Assert::AreEqual(string("10000000000000000000"), (embiguint(9999999999999999999) + embiguint(1)).Get());
			Assert::AreEqual(string("10000000000000000000"), (embiguint(1) + embiguint(9999999999999999999)).Get());
			Assert::AreEqual(string("19999999999999999998"), (embiguint(9999999999999999999) +
				                                              embiguint(9999999999999999999)).Get());
		}

		TEST_METHOD(shouldProvideAdditionOperatorForTypeEmbiguintAndTypeT)
		{
			Assert::AreEqual(string("30100"), (embiguint(100) + 30000).Get());
			Assert::AreEqual(string("100000000000"), (embiguint(99999999999) + 1).Get());
			Assert::AreEqual(string("100000000000"), (embiguint(1) + 99999999999).Get());
			Assert::AreEqual(string("19999999999999999998"), (embiguint(9999999999999999999) +
				                                              9999999999999999999).Get());
		}

		TEST_METHOD(shouldProvideAdditionOperatorForTypeTAndTypeEmbiguint)
		{
			Assert::AreEqual(string("30100"), (100 + embiguint(30000)).Get());
			Assert::AreEqual(string("10000000000000000000"), (9999999999999999999 + embiguint(1)).Get());
			Assert::AreEqual(string("10000000000000000000"), (1 + embiguint(9999999999999999999)).Get());
			Assert::AreEqual(string("19999999999999999998"), (9999999999999999999 +
				                                              embiguint(9999999999999999999)).Get());
		}

		TEST_METHOD(shouldCompareEmbiguintsLessThanCorrectly)
		{
			Assert::IsTrue(embiguint(123) < embiguint(321));
			Assert::IsFalse(embiguint(321) < embiguint(123));
			Assert::IsTrue(embiguint(321) < embiguint(12345678910));
			Assert::IsFalse(embiguint(12345678910) < embiguint(123));
			Assert::IsFalse(embiguint(123) < embiguint(123));
			Assert::IsFalse(embiguint(12345678910) < embiguint(12345678910));
		}

		TEST_METHOD(shouldCompareEmbiguintsMoreThanCorrectly)
		{
			Assert::IsFalse(embiguint(123) > embiguint(321));
			Assert::IsTrue(embiguint(321) > embiguint(123));
			Assert::IsFalse(embiguint(321) > embiguint(12345678910));
			Assert::IsTrue(embiguint(12345678910) > embiguint(123));
			Assert::IsFalse(embiguint(123) > embiguint(123));
			Assert::IsFalse(embiguint(12345678910) > embiguint(12345678910));
		}

		TEST_METHOD(shouldCompareEmbiguintsLessThanOrEqualToCorrectly)
		{
			Assert::IsTrue(embiguint(123) <= embiguint(321));
			Assert::IsFalse(embiguint(321) <= embiguint(123));
			Assert::IsTrue(embiguint(321) <= embiguint(12345678910));
			Assert::IsFalse(embiguint(12345678910) <= embiguint(123));
			Assert::IsTrue(embiguint(123) <= embiguint(123));
			Assert::IsTrue(embiguint(12345678910) <= embiguint(12345678910));
		}

		TEST_METHOD(shouldCompareEmbiguintsMoreThanOrEqualToCorrectly)
		{
			Assert::IsFalse(embiguint(123) >= embiguint(321));
			Assert::IsTrue(embiguint(321) >= embiguint(123));
			Assert::IsFalse(embiguint(321) >= embiguint(12345678910));
			Assert::IsTrue(embiguint(12345678910) >= embiguint(123));
			Assert::IsTrue(embiguint(123) >= embiguint(123));
			Assert::IsTrue(embiguint(12345678910) >= embiguint(12345678910));
		}

		TEST_METHOD(shouldGetNumberOfDigits)
		{
			Assert::AreEqual(1u, embiguint(4).GetNoDigits());
			Assert::AreEqual(2u, embiguint(56).GetNoDigits());
			Assert::AreEqual(5u, embiguint(12345).GetNoDigits());
			Assert::AreEqual(10u, embiguint(5748295648).GetNoDigits());
		}
	};
}