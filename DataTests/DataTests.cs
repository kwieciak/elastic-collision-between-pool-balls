using Data;
using System.Numerics;

namespace DataTests
{
    [TestClass]
    public class DataTests
    {
        IDataBoard board = IDataBoard.CreateApi();

        [TestMethod]
        public void CreateBallTest()
        {
            IDataBall dataBall = board.AddDataBall(0, 0, 2, 1, 1, 1, null, 0);
            Assert.IsNotNull(dataBall);
        }
        
        [TestMethod] 
        public void BallSpeedTest() 
        {
            IDataBall dataBall = board.AddDataBall(1, 2, 3, 4, 5, 6, null, 1);
            Vector2 vector = new Vector2(5, 6);
            Assert.AreEqual(dataBall.Speed, vector);

        }


        [TestMethod]
        public void RemoveBallsTest()
        {
            IDataBoard DataAPI = IDataBoard.CreateApi(400, 580);
            IDataBall dataBall = DataAPI.AddDataBall(1, 1, 1, 1, 1, 1, null, 1);
            IDataBall dataBall2 = DataAPI.AddDataBall(2, 2, 2, 2, 2, 2, null, 2);
            Assert.AreEqual(DataAPI.GetAllBalls().Count, 2);
            DataAPI.RemoveAllBalls();
            Assert.AreEqual(DataAPI.GetAllBalls().Count, 0);
        }
    }
}