using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetPowerON.Pattern.Singleton.Test.Model
{
    public class TestAltStringSingleton : SingletonObjectBase<TestAltStringSingleton, string>
    {
        public bool IsOnDisposedCalled
        {
            get;
            private set;
        } = false;

        protected override void OnDisposed( )
        {
            IsOnDisposedCalled = true;
            base.OnDisposed( );
        }
    }
}
