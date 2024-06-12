using ContainerVervoer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainershipUnitTest
{
    [TestClass]
    public class ShipTest()
    {
        [TestMethod]
        public void Constructor_ShouldInitializeMaxWeight() {
            //arrange
            int length = 2;
            int width = 2;
            int expectedWeight = (length * width) * 150000;

            //act
            Ship ship = new Ship(length,width);

            //assert
            Assert.AreEqual(expectedWeight,ship.MaxWeight);
            Assert.AreEqual(length,ship.Length);
            Assert.AreEqual(width,ship.Width);

        }
    }
}
