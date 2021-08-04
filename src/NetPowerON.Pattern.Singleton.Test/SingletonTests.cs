using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetPowerON.Pattern.Singleton.Test.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetPowerON.Pattern.Singleton.Test
{
    [TestClass]
    public class SingletonTests
    {
        [TestMethod]
        public void ShouldCreateSingleton( )
        {
            var test = TestBaseSingleton.Create( );
            var test2 = TestBaseSingleton.Create( );

            Assert.IsTrue( object.ReferenceEquals( test, test2 ) );
        }

        [TestMethod]
        public void ShouldCreateUniqueSingletons( )
        {
            var test = TestBaseSingleton.Create( );
            var test2 = TestBaseSingleton.Create( );
            var test3 = TestAltBaseSingleton.Create( );
            var test4 = TestAltBaseSingleton.Create( );

            test3.MyVal = 100;

            Assert.AreEqual( test3.MyVal, test4.MyVal );
            Assert.IsTrue( object.ReferenceEquals( test3, test4 ) );

            Assert.IsFalse( object.ReferenceEquals( test3, test2 ) );
        }
    }
}
