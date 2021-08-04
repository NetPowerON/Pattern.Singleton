using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetPowerON.Pattern.Singleton.Test.Model
{
    public class TestAltBaseSingleton : SingletonBase<TestAltBaseSingleton>
    {
        public int MyVal
        {
            get;
            set;
        } = 0;
    }
}
