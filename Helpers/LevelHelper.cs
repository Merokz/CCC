using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCC.Helpers
{
    public class LevelHelper
    {
        public static string[] ReadUnitTestLines(string path)
        {
            return File.ReadAllLines(path);
        }
        public static void WriteUnitTestLines(string path, string[] lines)
        {
            File.WriteAllLines(path, lines);
        }

    }
}
