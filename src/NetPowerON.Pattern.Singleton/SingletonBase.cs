using NetPowerON.Pattern.Singleton.Base;
using System;

namespace NetPowerON.Pattern.Singleton
{
    public abstract class SingletonBase<TClass> : AbstractSingletonBase
        where TClass : SingletonBase<TClass>
    {
        private static SingletonFactory<TClass, TClass> factory = new( obj => ( TClass )Activator.CreateInstance( typeof( TClass ), true ) );
        
        public static TClass Create( )
        {
            return GetFactory( ).Create( );
        }

        private static SingletonFactory<TClass, TClass> GetFactory( )
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
