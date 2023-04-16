using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicTest
{
    [TestClass]
    public class BallTests
    {
        IBall ball = IBall.CreateBall(1,2,3);

        [TestMethod]
        public void ConstructorTest()
        {
            Assert.IsNotNull(ball);
        }

        [TestMethod]
        public void CreateBallTest()
        {
            Assert.AreEqual(1, ball.PosX);
            Assert.AreEqual(2, ball.PosY);
            Assert.AreEqual(3, ball.Radius);
        }

        [TestMethod]
        public void SetBallCoordinatesTest()
        {
            ball.PosX = 5;
            ball.PosY = 7;
            Assert.AreEqual(5, ball.PosX);
            Assert.AreEqual(7, ball.PosY);
        }

        [TestMethod]
        public void SetBallRadiusTest()
        {
            ball.Radius = 6;
            Assert.AreEqual(6, ball.Radius);
        }

    }
}
