using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace WeakDelegateProject
{
    class WeakDelegate
    {

        public WeakReference weakReference;
        private MethodInfo targetMethodInfo;
        private Type eventHandlerDelegateType;
        private Delegate del;

        public WeakDelegate(Delegate method)
        {
            eventHandlerDelegateType = method.GetType();
            targetMethodInfo = method.Method;
            weakReference = new WeakReference(method.Target);
            gen();
        }

        private void gen()
        {

        }

        public Delegate Weak
        {
            get
            {
                return del;
            }
        }

        //public void Weakg(int a)
        //{
        //    Method(a);
        //}
    }
}
