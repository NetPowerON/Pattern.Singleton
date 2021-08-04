using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetPowerON.Pattern.Singleton.Test.Model;

namespace NetPowerON.Pattern.Singleton.Test
{
    [TestClass]
    public class SingletonObjectTests
    {
        [TestMethod]
        public void ShouldCreateSingleton( )
        {
            var test = TestStringSingleton.Create( "Hello" );
            var test2 = TestStringSingleton.Create( "World" );

            Assert.IsFalse( object.ReferenceEquals( test, test2 ) );

            var test3 = TestStringSingleton.Create( "Test" );
            var test4 = TestStringSingleton.Create( "Test" );

            Assert.IsTrue( object.ReferenceEquals( test3, test4 ) );
        }

        [TestMethod]
        public void ShouldCreateUniqueTypes( )
        {
            var test = TestAltStringSingleton.Create( );
            var test2 = TestAltStringSingleton.Create( );
            var test3 = TestStringSingleton.Create( );
            var test4 = TestStringSingleton.Create( );

            Assert.IsTrue( object.ReferenceEquals( test, test2 ) );
            Assert.IsTrue( object.ReferenceEquals( test3, test4 ) );
            Assert.IsFalse( object.ReferenceEquals( test, test3 ) );
            Assert.IsFalse( object.ReferenceEquals( test2, test4 ) );

            Assert.IsFalse( test4.IsOnDisposedCalled );
            Assert.IsFalse( test4.IsDisposed );
        }

        [TestMethod]
        public void ShouldReDisposeWihtoutError( )
        {
            var test = TestAltStringSingleton.Create( "Hello" );
            var test2 = TestAltStringSingleton.Create( "Hello" );
            test.Dispose( );
            test2.Dispose( );
            test.Dispose( );
            test.Dispose( );
            test.Dispose( );
            test.Dispose( );

            Assert.IsTrue( test.IsOnDisposedCalled );
            Assert.IsTrue( test.IsDisposed );
        }

        [TestMethod]
        public void ShouldDisposeWhenAllCleared( )
        {
            var test = TestAltStringSingleton.Create( "Hello" );
            var test2 = TestAltStringSingleton.Create( "Hello" );

            Assert.IsTrue( object.ReferenceEquals( test, test2 ) );
            test2.Dispose( );
            test2 = null;

            Assert.IsFalse( test.IsDisposed );
            test.Dispose( );
            Assert.IsTrue( test.IsDisposed );
            Assert.IsTrue( test.IsOnDisposedCalled );
        }
    }
}
