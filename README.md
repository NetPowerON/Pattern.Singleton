Singleton
=========

<p>When you don't have a Inversion of Control container in your application but still would like to have singleton objects to hold state. Often developers would turn to a static class. While this will work it's not an extendable solution.</p>

<p>This small library was developed to fill a need where updating a code base that either can't use dependency injection or didn't implement it at the start and the code base has grown too large to refactor in a timely fashion.</p>

There are three base classes that you can inherit your Singleton Object class from.

Classes
-------

SingletonBase
SingletonEquatableBase
SingletonObjectBase

Usage
-----

    public MySingletonClass : SingletonBase<MySingletonClass>
    {

    }

    public MySingletonIntKeyClass : SingletonEquatableBase<MySingletonIntKeyClass, int>
    {

    }

    public MySingletonObjClass : SingletonObjectBase<MySingletonObjClass, MyObj>
    {

    }

Then the usuage would be like this for example.

    var singelton = MySingletonClass.Create( );

    var mysingletonint = MySingletonIntKeyClass.Create( );
    var other = MySingletonIntKeyClass.Create( 1 );

    var myObj = new MyObj( );
    var myObj2 = new MyObj( );

    var mysingletonObject = MySingletonObjClass.Create( myObj );
    var mysignletonObject = MySingletonObjClass.Create( myObj2 );
