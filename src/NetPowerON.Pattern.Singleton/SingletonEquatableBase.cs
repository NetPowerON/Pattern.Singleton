using NetPowerON.Pattern.Singleton.Base;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace NetPowerON.Pattern.Singleton
{
    public abstract class SingletonEquatableBase<TClass, TObject> : AbstractSingletonBase
        where TClass : SingletonEquatableBase<TClass, TObject>
        where TObject : struct, IEquatable<TObject>
    {
        private static readonly ConcurrentDictionary<TObject, SingletonFactory<TClass, object>>
            _concurrentMap = new( );
        private static SingletonFactory<TClass, object>? _defaultFactory;

        private static  bool        _noKey      = false;
        private static  TObject     _key;

        public static TClass Create( TObject key )
        {
            _key = key;
            return GetFactory( key ).Create( );
        }

        public static TClass Create( )
        {
            if( _defaultFactory is null )
            {
                _noKey = true;
                _defaultFactory = new( tclass => ( TClass ) Activator.CreateInstance( typeof( TClass ), true ) );
            }
            return _defaultFactory.Create( );
        }

        public override void Dispose( )
        {
            if( _noKey && _defaultFactory is not null )
            {
                if( _defaultFactory.SubtractOneFromCountRequested( ).CountRequested == 0 )
                {
                    IsDisposed = true;
                    OnDisposed( );
                }
                return;
            }
            
        }

        private static SingletonFactoryBase<TClass> GetFactory( TObject key )
        {
            return _concurrentMap.GetOrAdd( key, key => new SingletonFactory<TClass, object>(
                tclass => ( TClass )Activator.CreateInstance( typeof( TClass ), true )
                )
            );
        }
    }
}
