using CCC.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCC
{
    public class Level4
    {
        public string[,] LevelFiles { get; set; }

        public void Run()
        {
            int numberOfUnitTests = LevelFiles.GetLength(0);
            var tasks = new List<Task>();

            for (int i = 0; i < numberOfUnitTests; i++)
            {
                int index = i;
                tasks.Add(Task.Run(() =>
                {
                    var inputLines = LevelHelper.ReadUnitTestLines(LevelFiles[index, 0]);
                    var outputLines = ProcessInput(inputLines);
                    LevelHelper.WriteUnitTestLines(LevelFiles[index, 1], outputLines);
                }));
            }

            Task.WaitAll(tasks.ToArray());
        }

        private string[] ProcessInput(string[] lines)
        {
            int cases = int.Parse(lines[1]);
            List<string> results = new List<string>();

            int lineIndex = 3;
            for (int i = 0; i < cases; i++)
            {
                int[] coins = lines[lineIndex++].Split(' ').Select(int.Parse).OrderBy(x => x).ToArray();
                int[] amounts = lines[lineIndex++].Split(' ').Select(int.Parse).ToArray();

                foreach (var amount in amounts)
                {
                    string result = GetMinimumCoins(amount, coins);
                    results.Add(result);
                }
            }

            return results.ToArray();
        }

        private string GetMinimumCoins(int amount, int[] coins)
        {
            if (amount == 0) return "0x0";

            int[] dp = new int[amount + 1];
            Array.Fill(dp, int.MaxValue);
            dp[0] = 0;

            foreach (int coin in coins)
            {
                for (int j = coin; j <= amount; j++)
                {
                    if (dp[j - coin] != int.MaxValue && dp[j - coin] + 1 < dp[j])
                    {
                        dp[j] = dp[j - coin] + 1;
                    }
                }
            }

            if (dp[amount] == int.MaxValue) return "Not Possible";

            return BuildResultString(amount, coins, dp);
        }

        private string BuildResultString(int amount, int[] coins, int[] dp)
        {
            Dictionary<int, int> coinCount = new Dictionary<int, int>();
            for (int i = amount; i > 0;)
            {
                for (int j = coins.Length - 1; j >= 0; j--)
                {
                    int coin = coins[j];
                    if (i >= coin && dp[i] == dp[i - coin] + 1)
                    {
                        if (!coinCount.ContainsKey(coin))
                            coinCount[coin] = 0;
                        coinCount[coin]++;
                        i -= coin;
                        break;
                    }
                }
            }

            return string.Join(" ", coinCount.OrderByDescending(x => x.Key).Select(x => $"{x.Value}x{x.Key}"));
        }
    }

    // Mock classes for context
    //public static class LevelHelper
    //{
    //    public static string[] ReadUnitTestLines(string path) => System.IO.File.ReadAllLines(path);
    //    public static void WriteUnitTestLines(string path, string[] lines) => System.IO.File.WriteAllLines(path, lines);
    //}
}
