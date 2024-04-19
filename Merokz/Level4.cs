using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CCC.Helpers;

namespace CCC
{
    public class Level4 : ILevel
    {
        public int Level { get; set; }
        public bool Debug { get; set; }
        public string[,] LevelFiles { get; set; }
        private int CurrentUnitTest { get; set; }
        Dictionary<int, UnitTest> UnitTests { get; set; } = new Dictionary<int, UnitTest>();

        public Level4()
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

        public UnitTest RunUnitTest(UnitTest unitTest, bool IsAsCheckFunction = false)
        {
            string[] Lines = unitTest.Input;
            if (!int.TryParse(Lines[0], out int cases))
                throw new Exception("Could not parse the number of cases");

            // We need two more lines to parse here compared to the previous levels.
            List<string[]> allResults = new List<string[]>();

            int lineIndex = 3;
            for (int i = 0; i < cases; i++)
            {
                // Parse the coin values
                int[] coins = Lines[lineIndex++].Split(' ').Select(int.Parse).OrderBy(x => x).ToArray();
                // Parse the amounts
                int[] amounts = Lines[lineIndex++].Split(' ').Select(int.Parse).ToArray();

                List<string> results = new List<string>();
                foreach (var amount in amounts)
                {
                    results.Add(GetMinimumCoins(amount, coins));
                }
                allResults.Add(results.ToArray());
            }

            unitTest.Output = allResults.SelectMany(x => x).ToArray();
            LevelHelper.WriteUnitTestLines(LevelFiles[unitTest.ID, 1], unitTest.Output);

            return unitTest;
        }

        private string GetMinimumCoins(int amount, int[] coins)
        {
            if (amount == 0) return "0";

            // Utilize a long array if expecting very large amounts
            long[] dp = new long[amount + 1];
            int[] lastCoin = new int[amount + 1];
            Array.Fill(dp, long.MaxValue - 1);
            dp[0] = 0;

            for (int i = 0; i < coins.Length; i++)
            {
                for (int j = coins[i]; j <= amount; j++)
                {
                    if (dp[j - coins[i]] + 1 < dp[j])
                    {
                        dp[j] = dp[j - coins[i]] + 1;
                        lastCoin[j] = coins[i];
                    }
                }
            }

            return BuildResultString(amount, dp, lastCoin);
        }

        private string BuildResultString(int amount, long[] dp, int[] lastCoin)
        {
            if (dp[amount] == long.MaxValue - 1) return "Not Possible";

            StringBuilder result = new StringBuilder();
            int currentAmount = amount;
            Dictionary<int, int> coinCount = new Dictionary<int, int>();

            while (currentAmount > 0)
            {
                int coin = lastCoin[currentAmount];
                if (coinCount.ContainsKey(coin))
                {
                    coinCount[coin]++;
                }
                else
                {
                    coinCount[coin] = 1;
                }
                currentAmount -= coin;
            }

            foreach (var coin in coinCount)
            {
                result.Append($"{coin.Value}x{coin.Key} ");
            }

            return result.ToString().Trim();
        }
    }
}
