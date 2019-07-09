using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.TestLib;

namespace console
{
    public class AnotherClass
    {
        public static List<string> AllClasses = new List<string>{ "Test.TestLib." + nameof(Class1) };
        public object Create()
        {
            return new Class1();
        }
    }
}
