﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCC
{
    public class Level5 : ILevel
    {
        public int Level { get; set; }
        public bool Debug { get; set; }
        public string[] Lines { get; set; }
        public string[,] LevelFiles { get; set; }

        public Level5()
        {
        }
        public void Run()
        {
        }
    }
}
