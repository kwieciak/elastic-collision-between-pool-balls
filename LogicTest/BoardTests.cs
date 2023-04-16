using Logic;
using Model;
using System.Reflection.Emit;
using System.Xml.Schema;

namespace LogicTest
{
    [TestClass]
    public class BoardTests
    {
        LogicAbstractAPI board = LogicAbstractAPI.CreateAPIInstance();

        [TestMethod]
        public void ConstructorTest()
        {
            Assert.IsNotNull(board);
        }

        [TestMethod]
        public void AddingBallsTest()
        {
            board.AddBalls(3, 5);
            board.StartMovement();
            Assert.AreEqual(board.GetAllBalls().Count, 3);
        }

        [TestMethod]
        public void BallsMovingTest()
        {
            board.AddBalls(5, 3);
            board.StartMovement();
            Thread.Sleep(100);
            List<List<int>> result1 = board.GetAllBallsPosition();
            Thread.Sleep(2000);
            List<List<int>> result2 = board.GetAllBallsPosition();
            Assert.AreNotEqual(result1[0], result2[0]);
            Assert.AreNotEqual(result1[1], result2[1]);
            Assert.AreNotEqual(result1[2], result2[2]);
            Assert.AreNotEqual(result1[3], result2[3]);
            Assert.AreNotEqual(result1[4], result2[4]);
        }

        [TestMethod]
        public void ClearingBoardTest()
        {
            board.AddBalls(3, 5);
            board.StartMovement();
            Assert.AreEqual(board.GetAllBalls().Count, 3);
            board.ClearBoard();
            Assert.AreEqual(board.GetAllBalls().Count, 0);
        }

        [TestMethod]
        public void ClearingEmptyBoardTest()
        {
            board.ClearBoard();
            Assert.AreEqual(board.GetAllBalls().Count, 0);
        }


    }
}