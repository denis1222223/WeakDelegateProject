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

            source.Completed += (Action<int, int>)new WeakDelegate((Action<int>)listener.Handler).Weak;

            source.TikTak(1000);
            Console.ReadLine();
        }
    }
}
