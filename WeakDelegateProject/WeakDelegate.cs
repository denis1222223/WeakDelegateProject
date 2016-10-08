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
            eventHandlerDelegateType = method.GetType();
            targetMethodInfo = method.Method; 
                     
            GenerateDelegate();
        }

        private void GenerateDelegate()
        {
            DelegateFactory delegateFactory = new DelegateFactory(weakReference, targetMethodInfo);
            del = delegateFactory.GetDelegate();
        }     
    }
}
