using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockExam {
    class GCD {
        public static int GreatestCommonDivisor(int a, int b) {
            if (b == 0) {
                return a;
            }

            return GreatestCommonDivisor(b, a % b);
        }
    }
}
