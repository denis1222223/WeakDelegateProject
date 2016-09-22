using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeakDelegateProject
{
    class Program
    {
        delegate void MyDelegate(ref int a);
        static event MyDelegate myEvent;

        static void Incr(ref int a)
        {
            a++;
        }
        static void Incr2(ref int a)
        {
            a++;
        }
        static void Incr3(ref int a)
        {
            a++;
        }

        static void Main(string[] args)
        {
            MyDelegate del = new MyDelegate(Incr);

            Console.WriteLine(myEvent.Target.ToString());

            int f = 4;
            myEvent(ref f);

            Console.WriteLine(f);

            Console.ReadLine();
        }

    }
}
