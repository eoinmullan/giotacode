using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMUtils
{
    static public class MathUtils
    {
        public static List<bool> GetPrimesBoolArrayToN(int n)
        {
	        if (n < 0) {
		        throw new ArgumentOutOfRangeException("Cannot get primes between zero and a negative number");
            }

            // Determine primes using Sieve of Eratosthenes
            var primeArray = new List<bool>(n + 1);
            for (var i = 0; i <= n; i++)
            {
                primeArray.Add(true);
            }
            primeArray[0] = false;
            if (n > 0)
            {
                primeArray[1] = false;
                int i = 2;
                while (i < n)
                {
                    if (primeArray[i])
                    {
                        int j = i + i;
                        while (j <= n)
                        {
                            primeArray[j] = false;
                            j += i;
                        }
                    }
                    i++;
                }
            }

            return primeArray;
        }

        public static List<int> GetPrimesToN(int n)
        {
            var nums = GetPrimesBoolArrayToN(n);
            var primes = new List<int>();

            for (int i=0; i<=n; i++) {
                if (nums[i]) {
                    primes.Add(i);
                }
            }

            return primes;
        }
    }
}
