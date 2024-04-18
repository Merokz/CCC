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
        public string[] Output { get; set; } = new string[0];
        private string[] Debug { get; set; } = new string[0];
        
        public (int, int) CaseDimensions { get; set; }
        public Dictionary<int, int[]> Cases { get; set; } = new Dictionary<int, int[]>();
        public Dictionary<int, string[]> Results = new Dictionary<int, string[]>();

        public int CasesAmount { get; set; } 
        
        //public object[] ReturnObjects { get; set; }

        public UnitTest(int unitTestID, string[] unitTestinput)
        {
            ID = unitTestID;
            Input = unitTestinput;
        }
    }
}
