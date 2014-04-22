using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using EMUtils;

namespace MathUtilsUnitTests
{
    [TestClass]
    public class TestGetPrimesBoolArrayToN
    {
        [TestMethod]
        public void shouldReturnSingleFalseElementListForZeroInput()
        {
            Assert.AreEqual(1, MathUtils.GetPrimesBoolArrayToN(0).Count);
            Assert.AreEqual(false, MathUtils.GetPrimesBoolArrayToN(0)[0]);
        }

        [TestMethod]
        public void shouldReturnListOfLengthOneGreaterThanInputArgument()
        {
            Assert.AreEqual(1, MathUtils.GetPrimesBoolArrayToN(0).Count);
            Assert.AreEqual(2, MathUtils.GetPrimesBoolArrayToN(1).Count);
            Assert.AreEqual(11, MathUtils.GetPrimesBoolArrayToN(10).Count);
            Assert.AreEqual(1001, MathUtils.GetPrimesBoolArrayToN(1000).Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Cannot get primes between zero and a negative number")]
        public void shouldThrowOutOfRangeForNegativeNumber()
        {
            MathUtils.GetPrimesBoolArrayToN(-1);
        }

        [TestMethod]
        public void shouldFindAllPrimesBelowFifty()
        {
            var primesToFifty = MathUtils.GetPrimesBoolArrayToN(50);
            var knownPrimes = new List<int> { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47 };
            for (var i = 1; i <= 50; i++)
            {
                if (knownPrimes.Contains(i))
                {
                    Assert.IsTrue(primesToFifty[i]);
                }
                else
                {
                    Assert.IsFalse(primesToFifty[i]);
                }
            }
        }
    }
}
