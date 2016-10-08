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
        public event Action<int> Completed;
        public event Action<int, double> Completed1;
        public event Action<int, double, int> Completed2;

        public void CallEvent()
        {
            Completed?.Invoke(1);
            Completed1?.Invoke(1,2);
            Completed2?.Invoke(1,2,3);
        }
    }
}
