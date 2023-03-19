using Projekt;
namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Calculator calc = new Calculator();
            Assert.AreEqual(calc.add(1, 2), 3);
            Assert.AreEqual(calc.add(5, -2), 3);
            Assert.AreEqual(calc.subtract(8, 4), 4);
        }
        [TestMethod]
        public void TestMethod2()
        {
            Calculator calc = new Calculator();
            Assert.AreEqual(calc.square(2), 4);
            Assert.AreEqual(calc.square(4), 16);
            
        }
    }
}