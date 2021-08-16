using NetPowerON.Pattern.Singleton.Base;
using System;

namespace NetPowerON.Pattern.Singleton
{
    internal class SingletonFactory<TSelf, TParam> : SingletonFactoryBase<TSelf>
        where TSelf : class, IDisposable
        where TParam : class
    {
        private                 TSelf?                 _internalInstance;
        private     readonly    Func<TParam?, TSelf>   _creator;

        public SingletonFactory( Func<TParam?, TSelf> creator ) => _creator = creator;

        public override TSelf Create( )
        {
            return Create( null );
        }

        public TSelf Create( TParam? param )
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
