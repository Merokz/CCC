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

        public UnitTest RunUnitTest(UnitTest unitTest, bool IsAsCheckFunction = false)
        {
            string[] Lines = unitTest.Input;
            unitTest.CasesAmount = Lines.Length;
            
            Dictionary<int, int[]> Cases = new Dictionary<int, int[]>();
            Dictionary<int, string[]> Results = new Dictionary<int, string[]>();

            for (int i = 0; i < unitTest.CasesAmount; i++)
            {
                //the input looks like 1231, 3131  so we need to split it by the comma and then by the space
                string[] Case = Lines[i].Split(',');
                int[] Numbers = Array.ConvertAll(Case, int.Parse);
                Cases.TryAdd(i, Numbers);
            }

            //generate the results
            Results = new Dictionary<int, string[]>();
            foreach (int i in Cases.Keys)
            {
                int[] Case = Cases[i];
                string[] lines = new string[Case[0]];
                Results.TryAdd(i, lines);
            }

            Random random = new Random();
            char[] xo = { 'X', 'O' };
            for(int i = 0; i < unitTest.CasesAmount; i++)
            {
                for(int y = 0; y < Cases[i][0]; y++)
                {
                    string line = "";
                    for(int x = 0; x < Cases[i][1]; x++)
                    {
                        line += xo[random.Next(0, 2)];
                    }
                    Results[i][y] = line;
                }
            }
            //write the results to the output but add them all together and leave always 1 empty line
            unitTest.Output = new string[unitTest.CasesAmount];
            for(int i = 0; i < unitTest.CasesAmount; i++)
            {
                unitTest.Output[i] = string.Join("\n", Results[i]) + "\n";
            }

            if (!IsAsCheckFunction)
            {
                LevelHelper.WriteUnitTestLines(LevelFiles[unitTest.ID, 1], unitTest.Output);

                if (Debug) DebugUnitTest(unitTest);
            }
            return unitTest;
        }



        public void DebugUnitTest(UnitTest unitTest)
        {
            List<string> DebugFile = ["DEBUGGING UNIT TEST: " + unitTest.ID];
            
            LevelHelper.WriteUnitTestLines(LevelFiles[unitTest.ID, 2], DebugFile.ToArray());
        }
    }
}
