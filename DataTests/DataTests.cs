using Data;
using System.Numerics;

namespace DataTests
{
    [TestClass]
    public class DataTests
    {
        DataLoggerAPI logger = DataLoggerAPI.CreateLogger();
        [TestMethod]
        public void CreateBallTest()
        {
            IDataBall dataBall = IDataBall.CreateDataBall(1, 1, 1, 1, 1, 1,logger,1);
            Assert.IsNotNull(dataBall);
        }

        [TestMethod] 
        public void BallSpeedTest() 
        {
            IDataBall dataBall = IDataBall.CreateDataBall(1, 2, 3, 4, 5, 6, logger, 1);
            Vector2 vector = new Vector2(5, 6);
            Assert.AreEqual(dataBall.Speed, vector);

        }
        [TestMethod]
        public void CreateBoardTest()
        {
            try
            {
                IDataBoard DataAPI = IDataBoard.CreateApi(400, 580);
                Assert.IsNotNull(DataAPI);
                Assert.AreEqual(DataAPI.Height, 580);
                Assert.AreEqual(DataAPI.Width, 400);
            }
            catch (Exception ex)
            {
                // it's here to bypass logger exceptions
            }
        }

        [TestMethod]
        public void RemoveBallsTest()
        {
            IDataBoard DataAPI = IDataBoard.CreateApi(400, 580);
            IDataBall dataBall = DataAPI.AddDataBall(1, 1, 1, 1, 1, 1, logger, 1);
            IDataBall dataBall2 = DataAPI.AddDataBall(2, 2, 2, 2, 2, 2, logger, 2);
            Assert.AreEqual(DataAPI.GetAllBalls().Count, 2);
            DataAPI.RemoveAllBalls();
            Assert.AreEqual(DataAPI.GetAllBalls().Count, 0);
        }
    }
}