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
                unitTest.CasesAmount = int.TryParse(Lines[0], out int cases) ? cases : throw new Exception("Amount didn't work");
                //remove the whitespace until the first number
                Lines = Lines.Skip(2).ToArray();
          

                //for (int i = 0; i < unitTest.CasesAmount; i++)
                //{
                //    //the input looks like 1231, 3131  so we need to split it by the comma and then by the space
                //    string[] Case = Lines[i].Split(',');
                //    int[] Numbers = Array.ConvertAll(Case, int.Parse);
                //    unitTest.Cases.TryAdd(i, Numbers);
                //}




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
