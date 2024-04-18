﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCC
{
    public class Level1 : ILevel
    {
        public int Level { get; set; }
        public bool Debug { get; set; }
        public string[] Lines { get; set; }
        public string[,] LevelFiles { get; set; }

        public Level1()
        {
        }

        public void Run()
        {
            for (int currentLevel = 0; currentLevel < LevelFiles.Length; currentLevel++)
            {
                RunUnitTest(currentLevel);
            }
        }

        public void RunUnitTest(int UnitTest)
        {
            Lines = LevelHelper.ReadUnitTestLines(LevelFiles[UnitTest, 0]);
        }
    }
}