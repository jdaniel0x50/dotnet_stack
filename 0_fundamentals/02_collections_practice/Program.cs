using System;
using System.Collections.Generic;

namespace _02_collections_practice
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Part One: Create Array to Hold 0-9");
            int[] arrIntegers = new int[10];
            int startVal = 0;
            int endVal = 9;
            for (int i=startVal; i<=endVal; i++)
            {
                arrIntegers[i] = i;
            }
            foreach (int item in arrIntegers)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("");
            Console.WriteLine("Part Two: Create Array to Hold Names");
            string[] arrNames = {"Tim", "Martin", "Nikki", "Sara"};
            foreach (string item in arrNames)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("");
            Console.WriteLine("Part Three: Create Array to Hold True False x 10");
            bool[] arrBoolean = new bool[10];
            for (int i=startVal; i < endVal; i++)
            {
                if (i % 2 == 0)
                {
                    arrBoolean[i] = true;
                }
                else
                {
                    arrBoolean[i] = false;
                }
            }
            foreach (bool item in arrBoolean)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("");
            Console.WriteLine("Part Four: Generate a Multiplaction Table for 1-10");
            startVal = 1;
            endVal = 10;
            int numValues = endVal - startVal + 1;
            // begin multiplying mult1 and mult2 together in nested for loops
            // write to the terminal as the array is constructed
            int[,] product = new int[numValues,numValues];
            for (int i = startVal; i <= endVal; i++)
            {
                Console.Write("[ ");
                for (int j = startVal; j <= endVal; j++)
                {
                    product[i-1,j-1] = i * j;
                    if (product[i-1,j-1] < 10)
                    {
                        Console.Write(" ");
                    }
                    Console.Write(product[i-1,j-1] + ", ");
                }
                if (product[i-1, endVal-1] < 100)
                {
                    Console.Write(" ");
                }
                Console.WriteLine("]");
            }

            Console.WriteLine("");
            Console.WriteLine("Part Five: Generate a List of Ice Cream Flavors");
            List<string> iceCream = new List<string>();
            iceCream.Add("Vanilla");
            iceCream.Add("Chocolate");
            iceCream.Add("Rocky Road");
            iceCream.Add("Turtle");
            iceCream.Add("Mackinaw Island Fudge");
            Console.WriteLine("We currently offer {0} ice cream flavors.", iceCream.Count);
            Console.WriteLine(iceCream[2]);
            string flavor = iceCream[2];
            iceCream.Remove(flavor);
            Console.WriteLine("After removing the {0} flavor, we currently offer {1} ice cream flavors.", flavor, iceCream.Count);

            Console.WriteLine("");
            Console.WriteLine("Part Six: Generate a User Dictionary");
            Dictionary<string, string> userIceCream = new Dictionary<string, string>();
            foreach (string name in arrNames)
            {
                userIceCream.Add(name, null);
            }
            Random rand = new Random();
            foreach (string name in arrNames)
            {
                int randomFlavor = rand.Next(0,4);
                userIceCream[name] = iceCream[randomFlavor];
            }
            foreach (KeyValuePair<string, string> entry in userIceCream)
            {
                Console.WriteLine(entry.Key + " likes " + entry.Value + " ice cream");
            }

        }
    }
}
