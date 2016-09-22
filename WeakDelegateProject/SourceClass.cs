using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WeakDelegateProject
{
    class SourceClass
    {
        public event Action<int, int> Completed;

        public void TikTak(int ms)
        {
            Thread.Sleep(ms);
            Completed(ms);
        }
    }
}
