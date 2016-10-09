using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestWeakDelegate
{
    public class TestMethodsKit
    {

        public int resultIntValue { get; set; }

        public void Sum(int x, int y)
        {
            resultIntValue = x + y;
        }

        public void Mul(int x, int y, byte z)
        {
            resultIntValue = x * y * z;
        }

        public string makeString(string firstStr, int number, string secondString)
        {
            return firstStr + number.ToString() + secondString;
        }

        public void NullFunc()
        {
            
        }
    }
}
