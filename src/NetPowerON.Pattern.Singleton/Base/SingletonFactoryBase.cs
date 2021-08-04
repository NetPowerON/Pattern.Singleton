using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace NetPowerON.Pattern.Singleton.Base
{
    internal abstract class SingletonFactoryBase<TClass> : IDisposable
        where TClass : class, IDisposable
    {
        private                 int                     _countRequested;

        public int CountRequested
        {
            get
            {
                return _countRequested;
            }
        }

        public abstract TClass Create( );

        public SingletonFactoryBase<TClass> SubtractOneFromCountRequested( )
        {
            Interlocked.Decrement( ref _countRequested );
            return this;
        }

        protected SingletonFactoryBase<TClass> IncrementRefCount( )
        {
            Interlocked.Increment( ref _countRequested );
            return this;
        }

        public virtual void Dispose( )
        {
        }
    }
}
