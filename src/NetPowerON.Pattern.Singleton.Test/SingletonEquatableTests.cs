using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetPowerON.Pattern.Singleton.Test.Model;

namespace NetPowerON.Pattern.Singleton.Test
{
    [TestClass]
    public class SingletonEquatableTests
    {
        [TestMethod]
        public void ShouldCreateSingletonEquatable( )
        {
            var test = TestIntSingleton.Create( );
            var test1 = TestIntSingleton.Create( );

            Assert.IsTrue( object.ReferenceEquals( test, test1 ) );

            var test2 = TestIntSingleton.Create( 1 );
            var test3 = TestIntSingleton.Create( 2 );

            Assert.IsFalse( object.ReferenceEquals( test2, test3 ) );

            var test4 = TestIntSingleton.Create( 1 );

            Assert.IsTrue( object.ReferenceEquals( test2, test4 ) );

            test2.Dispose( );
            Assert.IsFalse( test2.IsDisposed );
            test4.Dispose( );
            Assert.IsTrue( test2.IsDisposed );

            Assert.IsTrue( test4.IsOnDisposedCalled, "IsOnDisposed not called" );
            Assert.IsTrue( test4.IsDisposed, "IsDisposed is false" );
        }
    }
}
