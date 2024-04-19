using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CCC.Helpers;

namespace CCC
{
    public class Level1 : ILevel
    {
        public int Level { get; set; }
        public bool Debug { get; set; }
        public string[,] LevelFiles { get; set; }
        private int CurrentUnitTest { get; set; }
        Dictionary<int, UnitTest> UnitTests { get; set; } = new Dictionary<int, UnitTest>();

        public Level1()
        {
        }

        public void Run()
        {
            for (CurrentUnitTest = 0; CurrentUnitTest <= LevelFiles.Length / 3 - 1; CurrentUnitTest++)
            {
                UnitTest unitTest = new UnitTest(CurrentUnitTest, LevelHelper.ReadUnitTestLines(LevelFiles[CurrentUnitTest, 0]));
                UnitTests.TryAdd(CurrentUnitTest, unitTest);
                RunUnitTest(unitTest);
            }
        }

        public UnitTest RunUnitTest(UnitTest unitTest)
        {
            string[] Lines = unitTest.Input;
            //if (!int.TryParse(Lines[0], out int cases))
            //    throw new Exception("Could not parse the number of cases");

            Lines = Lines.Skip(3).ToArray();
            List<string> results = new List<string>();

            for (int i = 0; i < Lines.Length / 2; i++)
            {
                // Parse the coin values
                int[] coins = Lines[i].Split(' ').Select(int.Parse).OrderBy(x => x).ToArray();
                // Parse the amounts to be constructed
                int[] moneyToPay = Lines[i+1].Split(' ').Select(int.Parse).ToArray();

                foreach (int amount in moneyToPay)
                {
                    results.Add(CalculateMinimumCoins(coins, amount).ToString());
                }

            }

            // Store results in unitTest.Output as strings
            unitTest.Output = results.ToArray();

            LevelHelper.WriteUnitTestLines(LevelFiles[unitTest.ID, 1], unitTest.Output);

            return unitTest;
        }

        //CalculateMinimumCoins
        public string CalculateMinimumCoins(int[] coins, int moneyToPay)
        {
            Array.Sort(coins); // Ensure the coins are sorted for the two-pointer technique
            int left = 0; // Start pointer
            int right = coins.Length - 1; // End pointer

            while (left <= right)
            {
                int sum = coins[left] + coins[right];
                if (sum == moneyToPay)
                {
                    // Found the correct two coins
                    return $"{coins[left]} {coins[right]}";
                }
                else if (sum < moneyToPay)
                {
                    // If sum is less than required, move the left pointer to increase the sum
                    left++;
                }
                else
                {
                    // If sum is more than required, move the right pointer to decrease the sum
                    right--;
                }
            }

            return "No valid pair found";
        }

        public void DebugUnitTest(UnitTest unitTest)
        {
            List<string> DebugFile = ["DEBUGGING UNIT TEST: " + unitTest.ID];
            
            LevelHelper.WriteUnitTestLines(LevelFiles[unitTest.ID, 2], DebugFile.ToArray());
        }
    }
}
