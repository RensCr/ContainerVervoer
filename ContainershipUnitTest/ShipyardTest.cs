using ContainerVervoer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainershipUnitTest
{
    [TestClass]
    public class ShipyardTest()
    {
        [TestMethod]
        public void CalculateBestOrder_WithEvenWidth_ShouldReturnCorrectOrder()
        {
            //arrange
            Ship ship = new Ship(100, 4);
            int[] expectedOrder = new int[] { 0, 3, 1, 2 };

            //act
            Shipyard shipyard = new Shipyard();
            int[] bestOrder = shipyard.calculateBestOrder(ship);

            //assert
            CollectionAssert.AreEqual(expectedOrder, bestOrder);
        }

        [TestMethod]
        public void CalculateBestOrder_WithOddWidth_ShouldReturnCorrectOrder()
        {
            //arrange
            Ship ship = new Ship(100, 5);
            int[] expectedOrder = new int[] { 0, 4, 1, 3, 2 };

            //act
            Shipyard shipyard = new Shipyard();
            int[] bestOrder = shipyard.calculateBestOrder(ship);

            //assert
            CollectionAssert.AreEqual(expectedOrder, bestOrder);
        }

        [TestMethod]
        public void CreateRowsAndStacks_ShouldCreateCorrectNumberOfRowsAndStacks()
        {
            //arrange
            Ship ship = new Ship(5, 3);
            Shipyard shipyard = new Shipyard();

            //act
            List<Row> rows = shipyard.CreateRowsAndStacks(ship);

            //assert
            Assert.AreEqual(5, rows.Count);
            foreach (var row in rows)
            {
                Assert.AreEqual(3, row.Stacks.Count);
            }
        }

        [TestMethod]
        public void CreateRowsAndStacks_ShouldReturnEmptyRowAndStack()
        {
            //arrange
            Ship ship = new Ship(0, 0);
            Shipyard shipyard = new Shipyard();

            //act
            List<Row> rows = shipyard.CreateRowsAndStacks(ship);

            //assert
            Assert.AreEqual(0, rows.Count);
        }

        [TestMethod]
        public void LoadSchip()
    }
}
