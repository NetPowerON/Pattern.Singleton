﻿using NetPowerON.Pattern.Singleton.Base;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace NetPowerON.Pattern.Singleton
{
    public abstract class SingletonEquatableBase<TSelf, TObject> : AbstractSingletonBase
        where TSelf : SingletonEquatableBase<TSelf, TObject>
        where TObject : struct, IEquatable<TObject>
    {
        private static readonly ConcurrentDictionary<TObject, SingletonFactory<TSelf, object>>
            _concurrentMap = new( );
        private static SingletonFactory<TSelf, object>? _defaultFactory;

        private static  bool        _noKey      = false;
        private static  TObject     _key;

        public static TSelf Create( TObject key )
        {
            _key = key;
            return GetFactory( key ).Create( );
        }

        public static TSelf Create( )
        {
            if( _defaultFactory is null )
            {
                _noKey = true;
                _defaultFactory = new( tclass => ( TSelf ) Activator.CreateInstance( typeof( TSelf ), true ) );
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

        private static SingletonFactoryBase<TSelf> GetFactory( TObject key )
        {
            return _concurrentMap.GetOrAdd( key, key => new SingletonFactory<TSelf, object>(
                tclass => ( TSelf )Activator.CreateInstance( typeof( TSelf ), true )
                )
            );
        }
    }
}
