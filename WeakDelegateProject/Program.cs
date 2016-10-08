using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeakDelegateProject
{
    class Program
    {
        static void Main(string[] args)
        {
            SourceClass source = new SourceClass();
            ListenerClass listener = new ListenerClass();
            
            source.Completed += (Action<int>)new WeakDelegate((Action<int>)listener.Handler).Weak;
            source.Completed1 += (Action<int, double>)new WeakDelegate((Action<int, double>)listener.Handler).Weak;
            source.Completed2 += (Action<int, double, int>)new WeakDelegate((Action<int, double, int>)listener.Handler).Weak;

            source.CallEvent();
            Console.ReadLine();
        }
    }
}
