using Data;
using System.Numerics;

namespace DataTests
{
    [TestClass]
    public class DataTests
    {
        [TestMethod]
        public void CreateBallTest()
        {
            IDataBall dataBall = IDataBall.CreateDataBall(1, 1, 1, 1, 1, 1);
            Assert.IsNotNull(dataBall);
        }

        [TestMethod] 
        public void BallSpeedTest() 
        {
            IDataBall dataBall = IDataBall.CreateDataBall(1, 2, 3, 4, 5, 6);
            Vector2 vector = new Vector2(5, 6);
            Assert.AreEqual(dataBall.Speed, vector);

        }

        [TestMethod]
        public void MoveTest()
        {
            IDataBall dataBall = IDataBall.CreateDataBall(1, 2, 3, 4, 5, 6);
            Vector2 vector = dataBall.Position;
            dataBall.Move();
            Vector2 vector2 = dataBall.Position;
            Assert.AreNotEqual(vector, vector2);
        }



        [TestMethod]
        public void CreateBoardTest()
        {
            IDataBoard DataAPI = IDataBoard.CreateApi(400, 580);
            Assert.IsNotNull(DataAPI);
        }

        [TestMethod]
        public void BoardHeightTest()
        {
            IDataBoard DataAPI = IDataBoard.CreateApi(400, 580);
            Assert.AreEqual(DataAPI.Height, 580);
        }

        [TestMethod]
        public void BoardWidthTest()
        {
            IDataBoard DataAPI = IDataBoard.CreateApi(400, 580);
            Assert.AreEqual(DataAPI.Width, 400);
        }

        [TestMethod]
        public void RemoveBallsTest()
        {
            IDataBoard DataAPI = IDataBoard.CreateApi(400, 580);
            IDataBall dataBall = DataAPI.AddDataBall(1, 1, 1, 1, 1, 1);
            IDataBall dataBall2 = DataAPI.AddDataBall(2, 2, 2, 2, 2, 2);
            Assert.AreEqual(DataAPI.GetAllBalls().Count, 2);
            DataAPI.RemoveAllBalls();
            Assert.AreEqual(DataAPI.GetAllBalls().Count, 0);
        }
    }
}