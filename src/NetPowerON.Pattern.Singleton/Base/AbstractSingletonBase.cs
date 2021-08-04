using System;
using System.Collections.Generic;
using System.Text;

namespace NetPowerON.Pattern.Singleton.Base
{
    public abstract class AbstractSingletonBase : IDisposable
    {
        public bool IsDisposed
        {
            get;
            protected set;
        }

        public virtual void Dispose( )
        {
            
        }

        protected virtual void OnDisposed( )
        {

        }
    }
}
