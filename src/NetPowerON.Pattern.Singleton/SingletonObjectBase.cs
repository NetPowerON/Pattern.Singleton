using NetPowerON.Pattern.Singleton.Base;
using System;
using System.Collections.Concurrent;

namespace NetPowerON.Pattern.Singleton
{
    public abstract class SingletonObjectBase<TClass, TObject> : AbstractSingletonBase
        where TClass : SingletonObjectBase<TClass, TObject>
        where TObject : class
    {
        private static readonly ConcurrentDictionary<TObject, SingletonFactory<TClass, TObject>>
            _concurrentMap = new( );

        private static SingletonFactory<TClass, TObject>?
            _nullFactory;

        public virtual TObject? Item
        {
            get;
            private set;
        }

        public static TClass Create( TObject? item = null )
        {
            return GetFactory( item ).Create( item );
        }

        public static TClass Create(TClass replacing, TObject item)
        {
            if( replacing is null )
            {
                return Create( item );
            }
            replacing.Dispose( );
            return Create( item );
        }

        protected static void Remove( SingletonObjectBase<TClass, TObject> singleton )
        {
            if( singleton is null )
            {
                return;
            }
            GC.SuppressFinalize( singleton );
            singleton.Dispose( );
            if( singleton.Item is not null )
            {
                _concurrentMap.TryRemove( singleton.Item, out _ );
            }
        }

        private static SingletonFactory<TClass, TObject> GetFactory( TObject? item )
        {
            if( item is null )
            {
                if( _nullFactory is null )
                {
                    _nullFactory = new SingletonFactory<TClass, TObject>( obj => ( TClass )Activator.CreateInstance( typeof( TClass ), true ) );
                }
                return _nullFactory;
            }
            SingletonFactory<TClass, TObject> factory;
            var map = _concurrentMap;
            if( !map.TryGetValue( item, out factory ) )
            {
                factory = new SingletonFactory<TClass, TObject>( obj => {
                    var tc = ( TClass )Activator.CreateInstance( typeof( TClass ), true );
                    if( obj is not null )
                    {
                        tc.Item = obj;
                    }
                    return tc;
                } );
                map.TryAdd( item, factory );
            }
            return factory;
        }

        public override void Dispose( )
        {
            var factory = GetFactory( Item );
            if( factory.CountRequested == 0 )
            {
                return;
            }
            if( factory.SubtractOneFromCountRequested( ).CountRequested == 0 )
            {
                Remove( this );
                IsDisposed = true;
                OnDisposed( );
            }
        }
    }
}
