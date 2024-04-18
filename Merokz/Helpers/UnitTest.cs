using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCC.Helpers
{
    public class UnitTest
    {
        public int ID { get; set; }
        public string[] Input { get; set; }
        //public (int, int) CaseDimensions { get; set; }

        //publci string[] Cases { get; set; }
        public int CasesAmount { get; set; }
        public string[] Output { get; set; }

        public UnitTest(int unitTestID, string[] unitTestinput)
        {
            ID = unitTestID;
            Input = unitTestinput;
        }
    }
}
