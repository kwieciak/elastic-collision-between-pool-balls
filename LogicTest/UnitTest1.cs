using Logic;
using Model;
using System.Reflection.Emit;
using System.Xml.Schema;

namespace LogicTest
{
    [TestClass]
    public class UnitTest1
    {
        /*
        [TestMethod]
        public void TestMethod1()
        {
            LogicAbstractAPI board = LogicAbstractAPI.CreateAPIInstance();
            board.AddBalls(5, 3);
            board.StartMovement();
            Thread.Sleep(100);
            List<List<int>> result1 = board.GetAllBallsPosition();
            Thread.Sleep(2000);
            List<List<int>> result2 = board.GetAllBallsPosition();
            Console.WriteLine(result1.Count);
            Console.WriteLine(result2.Count);
            Assert.AreNotEqual(result1[0], result2[0]);
            Assert.AreNotEqual(result1[1], result2[1]);
            Assert.AreNotEqual(result1[2], result2[2]);
            Assert.AreNotEqual(result1[3], result2[3]);
            Assert.AreNotEqual(result1[4], result2[4]);
            Assert.AreEqual(5, result1.Count);
            board.ClearBoard();
            
        }*/

        [TestMethod]
        public void TestMethod2()
        {
            ModelAbstractAPI api = ModelAbstractAPI.CreateAPIInstance();
            api.Start();
            //ICircle circle1 = api.GetCircles().ElementAt(0);
            //int x1 = circle1._x;
            //int y1 = circle1._y;
            button1_Click();
            ICircle circle2 = api.GetCircles().ElementAt(0);
            int x2 = circle2._x;
            int y2 = circle2._y;
            Assert.AreNotEqual(0, x2);
            Assert.AreNotEqual(0, y2);
            //api.ClearBalls();
        }
        private async Task button1_Click()
        {
            await Task.Delay(5000);
        }
    }
}