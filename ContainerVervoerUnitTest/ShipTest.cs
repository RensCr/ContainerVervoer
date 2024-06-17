using ContainerVervoer;
namespace ContainerVervoerUnitTest
{
    [TestClass]
    public class ShipTest
    {
        [TestMethod]
        public void Ship_Constructor_InitializesCorrectly()
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
            Assert.AreEqual(width, ship.MaxStacks);
            Assert.AreEqual(length, ship.Rows.Count);
            Assert.IsTrue(ship.Rows.All(row => row.Stacks.Count == width));
        }

        [TestMethod]
        public void CreateRowsAndStacks_CreatesCorrectNumberOfRowsAndStacks()
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
        public void CalculateMaxWeight_ReturnsCorrectValue()
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
        public void Ship_MaxWeightIsCalculatedCorrectly()
        {
            // Arrange
            int length = 12;
            int width = 7;

            // Act
            var ship = new Ship(length, width);

            // Assert
            Assert.AreEqual(12600000, ship.MaxWeight); // (12 * 7) * 150000
        }

        [TestMethod]
        public void Ship_RowsAreCreatedWithStacks()
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
        public void Ship_StacksAreInitializedWithEmptyContainers()
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
    }
}