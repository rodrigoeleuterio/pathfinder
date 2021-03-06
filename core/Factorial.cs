﻿namespace Eleutherius.Genesis.Core
{
    public static class Factorial
    {
        private static readonly decimal[] factorials = new decimal[28];

        static Factorial()
        {
            CalculateFactorials(27);
        }
        
        public static decimal GetFactorial(int factor) => factorials[factor];

        private static void CalculateFactorials(long factor)
        {
            factorials[0] = 1;
            for (int i = 1; i <= factor; i++) factorials[i] = i * factorials[i - 1];            
        }

        public static bool Between(this int i, int min, int max)
        {
            return min <= i && i <= max;
        }
    }
}
