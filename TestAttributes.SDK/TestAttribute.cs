using System;
using System.Collections.Generic;
using System.Text;

namespace TestAttributes.SDK {

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class TestAttribute : Attribute{

        public static bool assetEqual(int expected, int actual) {
                return (expected == actual);

        }


        public static bool assetEqual(string a, string b) {
            return a.Equals(b);
             
        }

    }
}
