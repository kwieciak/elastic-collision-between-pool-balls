using Data;
using Logic;
using System.Numerics;
using System.Reflection.Emit;
using System.Xml.Schema;

namespace LogicTest
{
    [TestClass]
    public class BoardTests
    {

        LogicAbstractAPI board = LogicAbstractAPI.CreateAPIInstance(new FakeDataAPI(500, 500));
        internal class FakeDataBall : IDataBall
        {
            private Vector2 _position;
            public override Vector2 Position { get => _position; }

            public override Vector2 Speed { get; set; }
            public override bool HasCollided { get; set; }
            public override bool ContinueMoving { get; set; }

            public override event EventHandler<DataEventArgs> ChangedPosition;

            public override void Move()
            {
                this._position = Vector2.Zero;
            }
        }

        internal class FakeDataAPI : IDataBoard
        {
            public FakeDataAPI(int width, int height)
            {
                Width = width;
                Height = height;
            }

            public override int Width { get; }

            public override int Height { get; }

            public override IDataBall AddDataBall(int xPosition, int yPosition, int radius, int weight, int xSpeed, int ySpeed)
            {
                return new FakeDataBall();
            }

            public override List<IDataBall> GetAllBalls()
            {
                return new List<IDataBall>();
            }

            public override void RemoveAllBalls()
            {
                return;
            }
        }


        [TestMethod]
        public void ConstructorTest()
        {
            Assert.IsNotNull(board);
        }

        [TestMethod]
        public void AddingBallsTest()
        {
            board.AddBalls(3, 5);
            Assert.AreEqual(board.GetAllBalls().Count, 3);
        }
        [TestMethod]
        public void ClearingBoardTest()
        {
            board.AddBalls(3, 5);
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