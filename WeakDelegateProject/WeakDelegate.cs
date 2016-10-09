using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace WeakDelegateProject
{
    public class WeakDelegate
    {
        private WeakReference weakReference;
        private MethodInfo targetMethodInfo;
        private Delegate del;

        public Delegate Weak
        {
            get
            {
                return del;
            }
        }

        public WeakDelegate(Delegate method)
        {
            weakReference = new WeakReference(method.Target);
            targetMethodInfo = method.Method;
            GenerateDelegate();
        }

        private void GenerateDelegate()
        {
            DelegateFactory delegateFactory = new DelegateFactory(weakReference, targetMethodInfo);
            del = delegateFactory.GetDelegate();
        }     

        public bool IsAlive()
        {
            return weakReference.IsAlive;
        }
    }
}