using NetPowerON.Pattern.Singleton.Base;
using System;
using System.Collections.Concurrent;

namespace NetPowerON.Pattern.Singleton
{
    public abstract class SingletonObjectBase<TSelf, TObject> : AbstractSingletonBase
        where TSelf : SingletonObjectBase<TSelf, TObject>
        where TObject : class
    {
        private static readonly ConcurrentDictionary<TObject, SingletonFactory<TSelf, TObject>>
            _concurrentMap = new( );

        private static SingletonFactory<TSelf, TObject>?
            _nullFactory;

        public virtual TObject? Item
        {
            get;
            private set;
        }

        public static TSelf Create( TObject? item = null )
        {
            return GetFactory( item ).Create( item );
        }

        public static TSelf Create(TSelf replacing, TObject item)
        {
            if( replacing is null )
            {
                return Create( item );
            }
            replacing.Dispose( );
            return Create( item );
        }

        protected static void Remove( SingletonObjectBase<TSelf, TObject> singleton )
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

        private static SingletonFactory<TSelf, TObject> GetFactory( TObject? item )
        {
            if( item is null )
            {
                if( _nullFactory is null )
                {
                    _nullFactory = new SingletonFactory<TSelf, TObject>( obj => ( TSelf )Activator.CreateInstance( typeof( TSelf ), true ) );
                }
                return _nullFactory;
            }
            SingletonFactory<TSelf, TObject> factory;
            var map = _concurrentMap;
            if( !map.TryGetValue( item, out factory ) )
            {
                factory = new SingletonFactory<TSelf, TObject>( obj => {
                    var tc = ( TSelf )Activator.CreateInstance( typeof( TSelf ), true );
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
