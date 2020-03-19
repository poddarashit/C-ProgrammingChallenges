using System;
using System.Collections.Generic;


namespace Jackpot
{
    class Program
    {
        static void Main(string[] args)
        {
            //defining dictionary variable

            Dictionary<int, int> groupA = new Dictionary<int, int>();
            Dictionary<int, int> groupB = new Dictionary<int, int>();

            // adding key value pairs to dictionary

            groupA.Add(1, 3000);
            groupA.Add(2, 5000);
            groupA.Add(3, 7000);
            groupA.Add(4, 10000);

            groupB.Add(1, 2000);
            groupB.Add(2, 3000);
            groupB.Add(3, 4000);
            groupB.Add(4, 5000);

            int jackpotValue = 0;
            bool isNumber = false;

            while (!isNumber)
            {
                try
                {
                    Console.WriteLine("Enter Jackpot Value:");
                    jackpotValue = Convert.ToInt32(Console.ReadLine());
                    isNumber = true;

                    //object creation
                    var prog = new Program();

                    //function calling
                    prog.findWinningPairs(groupA, groupB, jackpotValue);
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Please enter valid number.");
                }

            }
                  
        }

        public void findWinningPairs(Dictionary<int, int> groupA, Dictionary<int, int> groupB, int jackpotValue)
        {
            bool jackpotFlag = false;

            try
            {
                foreach (KeyValuePair<int, int> dicA in groupA)
                {
                    foreach (KeyValuePair<int, int> dicB in groupB)
                    {
                        if (dicA.Value + dicB.Value == jackpotValue)
                        {
                            jackpotFlag = true;
                            Console.WriteLine(dicA.Key.ToString() + "," + dicB.Key.ToString());
                        }
                    }
                }

                if (!jackpotFlag)
                {
                    Console.WriteLine("No Winning pair");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }
    }
}
