using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farba.Helper
{
    internal static class MathHelper
    {
        public static int Factorial(int n)
        {
            if (n < 0)
            {
                return 0;
            }

            if (n == 0)
            {
                return 1;
            }

            return n * Factorial(n - 1);

        }

        public static int CombinationCount(int n, int k)
        {
            return k > 0 && k <= n ? Factorial(n) / (Factorial(n - k) * Factorial(k)) : -1;
        }
    }
}
