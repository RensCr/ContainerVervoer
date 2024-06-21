using ContainerVervoer;
namespace ContainerVervoerUnitTest
{
    [TestClass]
    public class ShipTest
    {
        [TestMethod]
        public void Ship_Constructor_ShouldInitializesCorrectly()
        {
            // Arrange
            int length = 10;
            int width = 5;

            // Act
            var ship = new Ship(length, width);

            // Assert
            Assert.AreEqual(length, ship.Length);
            Assert.AreEqual(width, ship.Width);
            Assert.AreEqual(7500000, ship.MaxWeight); // (10 * 5) * 150000
            Assert.AreEqual(length, ship.Rows.Count);
            Assert.IsTrue(ship.Rows.All(row => row.Stacks.Count == width));
        }

        [TestMethod]
        public void CreateRowsAndStacks_ShouldCreatesCorrectNumberOfRowsAndStacks()
        {
            // Arrange
            int length = 8;
            int width = 4;

            // Act
            var ship = new Ship(length, width);
            List<Row> rows = ship.Rows;

            // Assert
            Assert.AreEqual(length, rows.Count);
            Assert.IsTrue(rows.All(row => row.Stacks.Count == width));
        }

        [TestMethod]
        public void CalculateMaxWeight_ShouldReturnCorrectValue()
        {
            // Arrange
            int length = 6;
            int width = 3;
            var ship = new Ship(length, width);

            // Act
            int maxWeight = ship.CalculateMaxWeight();

            // Assert
            Assert.AreEqual(2700000, maxWeight); // (6 * 3) * 150000
        }

        [TestMethod]
        public void ShipRows_ShouldBeCreatedWithStacks()
        {
            // Arrange
            int length = 5;
            int width = 2;

            // Act
            var ship = new Ship(length, width);
            List<Row> rows = ship.Rows;

            // Assert
            foreach (var row in rows)
            {
                Assert.AreEqual(width, row.Stacks.Count);
            }
        }

        [TestMethod]
        public void Ship__CreateRowsAndStacks_ShouldBeInitializedWithEmptyContainers()
        {
            // Arrange
            int length = 3;
            int width = 4;

            // Act
            var ship = new Ship(length, width);
            List<Row> rows = ship.Rows;

            // Assert
            foreach (var row in rows)
            {
                foreach (var stack in row.Stacks)
                {
                    Assert.AreEqual(0, stack.Containers.Count);
                }
            }
        }

        [TestMethod]
        public void Ship_CalculateTotalWeight_ShouldReturnsCorrectValue()
        {
            // Arrange
            var ship = new Ship(3, 3);
            var container1 = new Container(2,ContainerType.Normal);
            var container2 = new Container(1, ContainerType.Normal);
            var container3 = new Container(3, ContainerType.Normal);
            ship.Rows[0].Stacks[0].AddContainer(container1);
            ship.Rows[1].Stacks[1].AddContainer(container2);
            ship.Rows[2].Stacks[2].AddContainer(container3);

            // Act
            int totalWeight = ship.CalulateTotalWeight(ship.Rows);

            // Assert
            Assert.AreEqual(18000, totalWeight);
        }

        [TestMethod]
        public void Ship_CalculateTotalWeight_ShouldReturnsZero()
        {
            // Arrange
            var ship = new Ship(3, 3);

            // Act
            int totalWeight = ship.CalulateTotalWeight(ship.Rows);

            // Assert
            Assert.AreEqual(0, totalWeight);
        }

        [TestMethod]
        public void Ship_CalculateTotalWeight_ShouldReturnsCorrectWeight()
        {
            // Arrange
            var ship = new Ship(3, 3);
            var container1 = new Container(1, ContainerType.Normal);
            var container2 = new Container(2, ContainerType.Normal);
            var container3 = new Container(3, ContainerType.Normal);
            var container4 = new Container(4, ContainerType.Normal);
            ship.Rows[0].Stacks[0].AddContainer(container1);
            ship.Rows[0].Stacks[1].AddContainer(container2);
            ship.Rows[1].Stacks[0].AddContainer(container3);
            ship.Rows[2].Stacks[2].AddContainer(container4);

            // Act
            int totalWeight = ship.CalulateTotalWeight(ship.Rows);

            // Assert
            Assert.AreEqual(26000, totalWeight);
        }

        [TestMethod]
        public void Ship_CalculateTotalWeight_ReturnsCorrectWeight()
        {
            // Arrange
            var ship = new Ship(3, 3);
            var container1 = new Container(1, ContainerType.Normal);
            var container2 = new Container(2, ContainerType.Normal);
            var container3 = new Container(3, ContainerType.Normal);
            ship.Rows[0].Stacks[0].AddContainer(container1);
            ship.Rows[0].Stacks[0].AddContainer(container2);
            ship.Rows[0].Stacks[0].AddContainer(container3);

            // Act
            int totalWeight = ship.CalulateTotalWeight(ship.Rows);

            // Assert
            Assert.AreEqual(18000, totalWeight);
        }

        [TestMethod]
        public void Ship_AddContainer_ShouldPlaceContainerToSpecificStack()
        {
            // Arrange
            var ship = new Ship(3, 3);
            var container = new Container(1, ContainerType.Normal);

            // Act
            ship.Rows[1].Stacks[1].AddContainer(container);

            // Assert
            Assert.AreEqual(1, ship.Rows[1].Stacks[1].Containers.Count);
            Assert.AreEqual(container, ship.Rows[1].Stacks[1].Containers[0]);
        }

        [TestMethod]
        public void Ship_AddMultipleContainers_ShouldBeAddedInDifferentStacks()
        {
            // Arrange
            var ship = new Ship(3, 3);
            var container1 = new Container(1, ContainerType.Normal);
            var container2 = new Container(2, ContainerType.Normal);

            // Act
            ship.Rows[0].Stacks[0].AddContainer(container1);
            ship.Rows[1].Stacks[1].AddContainer(container2);

            // Assert
            Assert.AreEqual(1, ship.Rows[0].Stacks[0].Containers.Count);
            Assert.AreEqual(container1, ship.Rows[0].Stacks[0].Containers[0]);
            Assert.AreEqual(1, ship.Rows[1].Stacks[1].Containers.Count);
            Assert.AreEqual(container2, ship.Rows[1].Stacks[1].Containers[0]);
        }

        [TestMethod]
        public void IsValidContainerSpot_ValuableContainersInAdjacentRows_ShouldReturnsFalse()
        {
            // Arrange
            var ship = new Ship(3, 3);
            var container1 = new Container(10,ContainerType.Valuable);
            var container2 = new Container(10, ContainerType.Valuable);
            ship.Rows[0].Stacks[1].Containers.Add(container1);
            ship.Rows[2].Stacks[1].Containers.Add(container2);

            // Act
            var result = ship.IsValidContainerSpot(1, 1);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValidContainerSpot_Place2normalContainers_ShouldReturnTrue()
        {
            // Arrange
            var ship = new Ship(3, 3);
            var container1 = new Container(10, ContainerType.Normal);
            var container2 = new Container(10, ContainerType.Normal);
            ship.Rows[0].Stacks[1].Containers.Add(container1);
            ship.Rows[2].Stacks[1].Containers.Add(container2);

            // Act
            var result = ship.IsValidContainerSpot(1, 1);

            // Assert
            Assert.IsTrue(result);
        }

    }
}