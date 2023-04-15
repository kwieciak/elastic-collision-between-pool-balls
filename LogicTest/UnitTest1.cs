using Logic;
namespace LogicTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            LogicAbstractAPI board = LogicAbstractAPI.CreateAPIInstance();
            board.AddBalls(3, 3);
            board.StartMovement();
            Thread.Sleep(2000);
            List<List<int>> result1 = board.GetAllBallsPosition();
            Thread.Sleep(2000);
            List<List<int>> result2 = board.GetAllBallsPosition();
            Console.WriteLine(result1.Count);
            Console.WriteLine(result2.Count);
            Assert.AreNotEqual(result1[1], result2[1]);
        }
    }
}