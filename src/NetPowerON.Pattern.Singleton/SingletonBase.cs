using NetPowerON.Pattern.Singleton.Base;
using System;

namespace NetPowerON.Pattern.Singleton
{
    public abstract class SingletonBase<TSelf> : AbstractSingletonBase
        where TSelf : SingletonBase<TSelf>
    {
        private static SingletonFactory<TSelf, TSelf> factory = new( obj => ( TSelf )Activator.CreateInstance( typeof( TSelf ), true ) );
        
        public static TSelf Create( )
        {
            return GetFactory( ).Create( );
        }

        private static SingletonFactory<TSelf, TSelf> GetFactory( )
        {
            return factory;
        }

        public override void Dispose( )
        {
            if( GetFactory( ).SubtractOneFromCountRequested( ).CountRequested == 0 )
            {
                IsDisposed = true;
                OnDisposed( );
            }
            base.Dispose( );
        }
    }
}
