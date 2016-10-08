using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeakDelegateProject
{
    class ListenerClass
    {
        public void Handler(int a)
        {
            Console.WriteLine(a);
        }

        public void Handler(int a, double b)
        {
            Console.WriteLine(a + " " + b);
        }

        public void Handler(int a, double b, int c)
        {
            Console.WriteLine(a + " " + b + " " + c);
        }
    }
}
