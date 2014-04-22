using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using EMUtils;

namespace MathUtilsUnitTests
{
    [TestClass]
    public class TestGetPrimesToN
    {
        [TestMethod]
        public void shouldReturnEmptyVectorForZeroInput()
		{
			Assert.AreEqual(0, MathUtils.GetPrimesToN(0).Count);
		}

        [TestMethod]
		public void shouldReturnEmptyVectorForInputOfOne()
        {
            Assert.AreEqual(0, MathUtils.GetPrimesToN(1).Count);
        }
		
        [TestMethod]
        public void shouldFindAllPrimesBelowFifty()
        {
            var knownPrimes = new List<int>{ 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47 };
            CollectionAssert.AreEqual(knownPrimes, MathUtils.GetPrimesToN(50));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Cannot get primes between zero and a negative number")]
        public void shouldThrowOutOfRangeForNegativeNumber()
        {
            MathUtils.GetPrimesToN(-1);
        }
    }
}
