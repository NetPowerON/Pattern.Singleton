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
    public class SingletonFactoryTests
    {
        [TestMethod]
        public void ShouldCreateFactories( )
        {
            var fact = new SingletonFactory<TestStringSingleton, string>( f => new TestStringSingleton( ) );
            var factoryObject = new SingletonFactory<TestStringSingleton, object>( f => new TestStringSingleton( ) );
        }
    }
}
