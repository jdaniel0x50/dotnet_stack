using System;
using System.Collections.Generic;

namespace _03_basic_13
{
    class Program
    {
        public static void PrintNums(int StartNum, int EndNum)
        {
            for (var i=StartNum; i<=EndNum; i++)
            {
                Console.Write(i + ", ");
            }
            Console.WriteLine("");
        }

        public static void PrintOddNums(int StartNum, int EndNum)
        {
            for (var i = StartNum; i <= EndNum; i++)
            {
                if (i % 2 != 0)
                {
                    Console.Write(i + ", ");
                }
            }
            Console.WriteLine("");
        }

        public static void PrintRunningSum (int StartNum, int EndNum)
        {
            int RunningSum = 0;
            for (var i = StartNum; i <= EndNum; i++)
            {
                RunningSum += i;
                Console.WriteLine("New Number: " + i + " ... Running Sum = " + RunningSum);
            }
        }

        public static void IteratePrintList (List<int> values)
        {
            foreach (int value in values)
            {
                Console.Write(value + ", ");
            }
            Console.WriteLine("");
        }

        public static int FindMaximum(List<int> values)
        {
            int currentMax = values[0];
            foreach (int value in values)
            {
                if (value > currentMax)
                {
                    currentMax = value;
                }
            }
            return currentMax;
        }

        public static int FindMinimum(List<int> values)
        {
            int currentMin = values[0];
            foreach (int value in values)
            {
                if (value < currentMin)
                {
                    currentMin = value;
                }
            }
            return currentMin;
        }


        public static int GetSum(List<int> values)
        {
            int runningSum = 0;
            foreach (int value in values)
            {
                runningSum += value;
            }
            return runningSum;
        }

        public static float GetAverage(List<int> values)
        {
            int listCount = values.Count;
            int listSum = GetSum(values);
            float listAverage = (float)listSum / listCount;
            return listAverage;
        }

        public static List<int> CreateListOdds(int StartNum, int EndNum)
        {
            List<int> ListOfInts = new List<int>();
            for (var i=StartNum; i<=EndNum; i++)
            {
                if (i % 2 != 0)
                {
                    ListOfInts.Add(i);
                }
            }
            return ListOfInts;
        }

        public static int CountLargerValues(List<int> values, int y)
        {
            int count = 0;
            foreach (int value in values)
            {
                if (value > y)
                {
                    count += 1;
                }
            }
            return count;
        }

        public static List<int> ReturnLargerValues(List<int> values, int y)
        {
            List<int> largerValues = new List<int>();
            foreach (int value in values)
            {
                if (value > y)
                {
                    largerValues.Add(value);
                }
            }
            return largerValues;
        }

        public static List<int> ReturnSquares(List<int> values)
        {
            List<int> squareValues = new List<int>();
            foreach (int value in values)
            {
                int valueSquared = (int)Math.Pow(value, 2);
                squareValues.Add(valueSquared);
            }
            return squareValues;
        }

        public static List<int> ReturnNoNegValues(List<int> values)
        {
            List<int> noNegValues = new List<int>();
            for (var i = 0; i<values.Count; i++)
            {
                if (values[i] < 0)
                {
                    noNegValues.Add(0);
                }
                else
                {
                    noNegValues.Add(values[i]);
                }
            }
            return noNegValues;
        }

        public static List<int> ReturnLeftShift(List<int> values)
        {
            List<int> leftShiftvalues = new List<int>();
            for (var i = 0; i < values.Count; i++)
            {
                if (i == (values.Count - 1))
                {
                    leftShiftvalues.Add(0);
                }
                else
                {
                    leftShiftvalues.Add(values[i+1]);
                }
            }
            return leftShiftvalues;
        }

        public static List<object> ReturnListReplace(List<int> values, string stringReplace)
        {
            List<object> listReplacedNegatives = new List<object>();
            for (var i = 0; i < values.Count; i++)
            {
                if (values[i] < 0)
                {
                    listReplacedNegatives.Add(stringReplace);
                }
                else
                {
                    listReplacedNegatives.Add(values[i]);
                }
            }
            return listReplacedNegatives;
        }

        public static void IteratePrintListObjects(List<object> values)
        {
            foreach (object value in values)
            {
                if (value is int)
                {
                    Console.Write(Convert.ToInt32(value) + ", ");
                }
                else if (value is string)
                {
                    Console.Write(value as string + ", ");
                }
            }
            Console.WriteLine("");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("");
            Console.WriteLine("Part One: Print all numbers from 1 to 255");
            int StartNum = 1;
            int EndNum = 255;
            PrintNums(StartNum, EndNum);

            Console.WriteLine("");
            Console.WriteLine("Part Two: Print all odd numbers from 1 to 255");
            PrintOddNums(StartNum, EndNum);

            Console.WriteLine("");
            Console.WriteLine("Part Three: Print all numbers and running sum from 0 to 255");
            StartNum = 0;
            PrintRunningSum(StartNum, EndNum);

            Console.WriteLine("");
            Console.WriteLine("Part Four: Iterate through an Array");
            List<int> ListOfInts = new List<int>();
            ListOfInts.Add(1);
            ListOfInts.Add(3);
            ListOfInts.Add(5);
            ListOfInts.Add(7);
            ListOfInts.Add(9);
            ListOfInts.Add(13);
            IteratePrintList(ListOfInts);

            Console.WriteLine("");
            Console.WriteLine("Part Five: Find and Print Max");
            ListOfInts.Add(-1);
            ListOfInts.Add(25);
            ListOfInts.Add(-13);
            int listMax = FindMaximum(ListOfInts);
            Console.WriteLine("The maximum in the list is " + listMax);


            Console.WriteLine("");
            Console.WriteLine("Part Six: Find Average");
            ListOfInts.Add(12);
            Console.WriteLine("The list is now:");
            IteratePrintList(ListOfInts);
            float listAverage = GetAverage(ListOfInts);
            Console.WriteLine("The average = " + listAverage);

            Console.WriteLine("");
            Console.WriteLine("Part Seven: Create and Print List of Odds 1-255");
            StartNum = 0;
            EndNum = 255;
            List<int> newList = new List<int>();
            newList = CreateListOdds(StartNum, EndNum);
            IteratePrintList(newList);

            Console.WriteLine("");
            Console.WriteLine("Part Eight: Find Values Larger than Y");
            int y = 3;
            Console.WriteLine("The list is now:");
            IteratePrintList(ListOfInts);
            int count = CountLargerValues(ListOfInts, y);
            List<int> largerValues = ReturnLargerValues(ListOfInts, y);
            Console.WriteLine("There are {0} values are larger than y = {1} in the list:", count, y);
            IteratePrintList(largerValues);

            Console.WriteLine("");
            Console.WriteLine("Part Nine: Find and Print Squares");
            Console.WriteLine("For the values larger than y = {0} (returned above), the squares are:", y);
            List<int> squaredValues = ReturnSquares(largerValues);
            IteratePrintList(squaredValues);

            Console.WriteLine("");
            Console.WriteLine("Part Ten: Eliminate Negatives; Replace with Zero");
            Console.WriteLine("The list is now:");
            IteratePrintList(ListOfInts);
            Console.WriteLine("With negatives eliminated:");
            List<int> noNegValues = new List<int>();
            noNegValues = ReturnNoNegValues(ListOfInts);
            IteratePrintList(noNegValues);

            Console.WriteLine("");
            Console.WriteLine("Part Eleven: Find Min, Max, Average");
            Console.WriteLine("For the list:");
            IteratePrintList(ListOfInts);
            Console.WriteLine("The minimum is " + FindMinimum(ListOfInts));
            Console.WriteLine("The maximum is " + FindMaximum(ListOfInts));
            Console.WriteLine("The average is " + GetAverage(ListOfInts));

            Console.WriteLine("");
            Console.WriteLine("Part Twelve: Shifting Values in Array");
            Console.WriteLine("For the list:");
            IteratePrintList(ListOfInts);
            Console.WriteLine("Shifted to the left:");
            List<int> leftShiftValues = ReturnLeftShift(ListOfInts);
            IteratePrintList(leftShiftValues);

            Console.WriteLine("");
            Console.WriteLine("Part Thirteen: Replace Negative Number with String");
            Console.WriteLine("For the list:");
            IteratePrintList(ListOfInts);
            string stringReplace = "Dojo";
            Console.WriteLine("Replaced String = " + stringReplace);
            List<object> replacedValues = ReturnListReplace(ListOfInts, stringReplace);
            IteratePrintListObjects(replacedValues);
        }
    }
}
