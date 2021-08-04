using NetPowerON.Pattern.Singleton.Base;
using System;

namespace NetPowerON.Pattern.Singleton
{
    internal class SingletonFactory<TClass, TParam> : SingletonFactoryBase<TClass>
        where TClass : class, IDisposable
        where TParam : class
    {
        private                 TClass?                 _internalInstance;
        private     readonly    Func<TParam?, TClass>   _creator;

        public SingletonFactory( Func<TParam?, TClass> creator ) => _creator = creator;

        public override TClass Create( )
        {
            return Create( null );
        }

        public TClass Create( TParam? param )
        {
            if( _internalInstance is null )
            {
                _internalInstance = _creator( param );
            }
            IncrementRefCount( );
            return _internalInstance;
        }

        public override void Dispose( )
        {
            _internalInstance?.Dispose( );
            base.Dispose( );
        }
    }
}
