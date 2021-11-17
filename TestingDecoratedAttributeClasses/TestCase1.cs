using System;
using System.Collections.Generic;
using System.Text;
using TestAttributes.SDK;

namespace TestingDecoratedAttributeClasses {

    [TestAttribute]
    public class TestCase1 {

        [TestAttribute]
        public void testMethod1() {
            //Console.WriteLine("Test 11");
            Console.WriteLine(TestAttribute.assetEqual(3, 3));

        }
        [TestAttribute]
        public void testMethod2() {
            Console.WriteLine(TestAttribute.assetEqual(3, 7));

        }
        [TestAttribute]
        public void testMethod3() {
            Console.WriteLine(TestAttribute.assetEqual("hello", "hello"));

        }


        [TestAttribute]
        public void testMethod4() {
            Console.WriteLine(TestAttribute.assetEqual(1, 10));

        }
    }
}
