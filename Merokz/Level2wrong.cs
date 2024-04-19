using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CCC.Helpers;

namespace CCC
{
    public class Level2 : ILevel
    {
        public int Level { get; set; }
        public bool Debug { get; set; }
        public string[,] LevelFiles { get; set; }
        private int CurrentUnitTest { get; set; }
        Dictionary<int, UnitTest> UnitTests { get; set; } = new Dictionary<int, UnitTest>();

        public Level2()
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

            // Assuming the input format in Lines follows:
            // Lines[0] = number of cases
            // Lines[1] onwards = coin values for each case, space-separated
            List<int> results = new List<int>();

            Lines = Lines.Skip(1).ToArray();

            for (int i = 1; i <= cases; i++)
            {
                // Parse the coin values from each line
                int[] coins = Lines[i].Split(' ').Select(int.Parse).OrderBy(x => x).ToArray();
                int smallestUnpayable = 1;

                // Use a set to easily check presence of integers
                HashSet<int> coinSet = new HashSet<int>(coins);

                // Find the smallest unpayable amount by checking each integer from 1 upwards
                while (coinSet.Contains(smallestUnpayable))
                {
                    smallestUnpayable++;
                }

                results.Add(smallestUnpayable);
            }

            // Store results in unitTest.Output as strings
            unitTest.Output = results.Select(x => x.ToString()).ToArray();
            //if (!IsAsCheckFunction && Debug)
            //{
            //    DebugUnitTest(unitTest);
            //}
            LevelHelper.WriteUnitTestLines(LevelFiles[unitTest.ID, 1], unitTest.Output);

            return unitTest;
        }




        public void DebugUnitTest(UnitTest unitTest)
        {
            List<string> DebugFile = ["DEBUGGING UNIT TEST: " + unitTest.ID];

            LevelHelper.WriteUnitTestLines(LevelFiles[unitTest.ID, 2], DebugFile.ToArray());
        }
    }
}
