﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CCC.Helpers;

namespace CCC
{
    public class Level3 : ILevel
    {
        public int Level { get; set; }
        public bool Debug { get; set; }
        public string[,] LevelFiles { get; set; }
        private int CurrentUnitTest { get; set; }
        Dictionary<int, UnitTest> UnitTests { get; set; } = new Dictionary<int, UnitTest>();

        public Level3()
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

            Lines = Lines.Skip(2).ToArray();
            List<string[]> allResults = new List<string[]>();

            for (int i = 0; i < cases; i++)
            {
                // Parse the coin values from each line
                int[] coins = Lines[i].Split(' ').Select(int.Parse).OrderBy(x => x).ToArray();
                List<string> results = new List<string>();

                // Get minimum coins needed for each amount from 1 to 100
                for (int amount = 1; amount <= 100; amount++)
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
            int[] dp = new int[amount + 1];
            int[] count = new int[amount + 1];
            int[] lastCoin = new int[amount + 1];
            Array.Fill(dp, int.MaxValue - 1);
            Array.Fill(count, 0);
            Array.Fill(lastCoin, -1);
            dp[0] = 0;

            for (int coin = 0; coin < coins.Length; coin++)
            {
                for (int j = coins[coin]; j <= amount; j++)
                {
                    if (dp[j - coins[coin]] + 1 < dp[j])
                    {
                        dp[j] = dp[j - coins[coin]] + 1;
                        count[j] = count[j - coins[coin]] + 1;
                        lastCoin[j] = coins[coin];
                    }
                }
            }

            return BuildResultString(amount, count, lastCoin);
        }

        private string BuildResultString(int amount, int[] count, int[] lastCoin)
        {
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
