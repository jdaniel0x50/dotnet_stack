using System;

namespace _01_fundamentals
{
    class Program
    {
        static void Main()
        {
            // create a loop that prints all values from 1-255
            Console.WriteLine("*** Part One: All Values 1-255 ***");
            int countMin = 1;
            int countMax = 255;
            for (int i = countMin; i <= countMax; i++) 
            {
                Console.WriteLine(i);
            }

            // create a loop that prints values divisible by 3 or 5, from 1 to 100
            Console.WriteLine("");
            Console.WriteLine("*** Part Two: All Values Divisible by 3; 1-100 ***");
            countMax = 100;
            int divisor1;
            int divisor2;
            int divisor;
            for (int i = countMin; i <= countMax; i++)
            {
                divisor1 = 3;
                divisor2 = 5;
                if (i % divisor1 == 0 || i % divisor2 == 0)
                {
                    if (i % divisor1 == 0)
                    {
                        divisor = divisor1;
                    }
                    else
                    {
                        divisor = divisor2;
                    }
                    Console.WriteLine("{0} is divisible by {1}", i, divisor);
                }
            }

            // create a loop that prints values divisible by 3 or 5, from 1 to 100
            Console.WriteLine("");
            Console.WriteLine("*** Part Three: All 'Fizz' for values divisible by 3 and 'Buzz' for values divisible by 5; 1-100 ***");
            countMax = 100;
            for (int i = countMin; i <= countMax; i++)
            {
                divisor = 3;
                if (i % divisor == 0)
                {
                    Console.WriteLine("Fizz, {0}", i);
                }
                divisor = 5;
                if (i % divisor == 0)
                {
                    Console.WriteLine("Buzz, {0}", i);
                }
            }

            // create a loop that prints values divisible by 3 or 5, from 1 to 100
            Console.WriteLine("");
            Console.WriteLine("*** Part Four: Print numbers divisible by 3 or 5 without modulus; 1-100 ***");
            countMax = 100;
            for (int i = countMin; i <= countMax; i++)
            {
                divisor = 3;
                if (divisor * (i / divisor) == i)
                {
                    Console.WriteLine("Fizz, {0}", i);
                }
                divisor = 5;
                if (divisor * (i / divisor) == i)
                {
                    Console.WriteLine("Buzz, {0}", i);
                }
            }

            // create a loop that prints values divisible by 3 or 5, from 1 to 100
            Console.WriteLine("");
            Console.WriteLine("*** Part Five: Generate 10 random values ***");
            Random rand = new Random();
            countMin = 1;
            countMax = 10;
            divisor1 = 3;
            divisor2 = 5;
            for (int i = countMin; i <= countMax; i++)
            {
                int randomNum = rand.Next(1,101);
                if (randomNum % divisor1 == 0)
                {
                    Console.WriteLine("Fizz, random number is {0}", randomNum);
                }
                else if (randomNum % divisor2 == 0)
                {
                    Console.WriteLine("Buzz, random number is {0}", randomNum);
                }
                else
                {
                    Console.WriteLine("Random = " + randomNum);
                }
            }

        }
    }
}
