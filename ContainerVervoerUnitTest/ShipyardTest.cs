using ContainerVervoer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerVervoerUnitTest
{
    [TestClass]
    public class ShipyardTest
    {
        public void LoadShip_AddMultipleContainers_ShouldPlaceContainersCorrect()
        {
            // Arrange
            var ship = new Ship(3, 3); 
            var containers = new List<Container>
            {
                new Container(10, ContainerType.ValuableCooled), 
                new Container(12, ContainerType.Coolable),       
                new Container(8, ContainerType.Valuable),         
                new Container(15, ContainerType.Normal),          
                new Container(14, ContainerType.Normal),
                new Container(13, ContainerType.Normal)
            };

            var shipyard = new Shipyard();

            // Act
            List<Row> loadedRows = shipyard.LoadShip(ship, containers);

            // Assert
            Assert.AreEqual(3, loadedRows.Count); 
            Assert.AreEqual(3, loadedRows[0].Stacks.Count); 
            Assert.IsTrue(loadedRows[0].Stacks[0].ContainsContainerType(ContainerType.ValuableCooled)); 
            Assert.IsTrue(loadedRows[0].Stacks[1].ContainsContainer(containers[1]));
            Assert.IsTrue(loadedRows[1].Stacks[0].ContainsContainer(containers[2]));
            Assert.IsTrue(loadedRows[2].Stacks[0].ContainsContainer(containers[3]));
            Assert.IsTrue(loadedRows[2].Stacks[1].ContainsContainer(containers[4]));
            Assert.IsTrue(loadedRows[2].Stacks[2].ContainsContainer(containers[5]));
        }



        [TestMethod]
        public void LoadShip_DontAddContainers_ShouldReturnEmptyStacks()
        {
            // Arrange
            var ship = new Ship(2, 2); 
            var containers = new List<Container>();

            var shipyard = new Shipyard();

            // Act
            List<Row> loadedRows = shipyard.LoadShip(ship, containers);

            // Assert
            Assert.AreEqual(2, loadedRows.Count);
            Assert.AreEqual(2, loadedRows[0].Stacks.Count); 

            foreach (var row in loadedRows)
            {
                foreach (var stack in row.Stacks)
                {
                    Assert.AreEqual(0, stack.Height);
                }
            }
        }

        [TestMethod]
        public void LoadShip_AddValuableContainers_ShouldNotPlaceAllContainers()
        {
            // Arrange
            var ship = new Ship(2, 2); 
            var containers = new List<Container>
            {
                new Container(10, ContainerType.ValuableCooled), 
                new Container(10, ContainerType.ValuableCooled),
                new Container(10, ContainerType.ValuableCooled),
                new Container(10, ContainerType.ValuableCooled),
                new Container(10, ContainerType.ValuableCooled),
                new Container(10, ContainerType.ValuableCooled),
                new Container(10, ContainerType.ValuableCooled),
                new Container(10, ContainerType.ValuableCooled)
            };

            var shipyard = new Shipyard();

            // Act
            List<Row> loadedRows = shipyard.LoadShip(ship, containers);

            // Assert
            Assert.AreEqual(2, loadedRows.Count); 
            Assert.AreEqual(2, loadedRows[0].Stacks.Count); 
        }
        [TestMethod]
        public void LoadShip_AddOneValuabelAndMultipleNormalContainers_ShouldPlaceAllContainers()
        {
            // Arrange
            var ship = new Ship(3, 3);
            var containers = new List<Container>
            {
                new Container(10, ContainerType.Valuable),
                new Container(12, ContainerType.Normal),
                new Container(8, ContainerType.Normal),
                new Container(15, ContainerType.Normal),
                new Container(14, ContainerType.Normal),
                new Container(13, ContainerType.Normal)
            };

            var shipyard = new Shipyard();

            // Act
            List<Row> loadedRows = shipyard.LoadShip(ship, containers);

            // Assert
            bool valuableIsAccessible = loadedRows.SelectMany(row => row.Stacks)
                                                   .Any(stack => stack.Containers.Any(container => container.ContainerType == ContainerType.Valuable) &&
                                                                 !stack.Containers.Any(container => container.ContainerType == ContainerType.Normal && container.Weight > 10000));
            Assert.IsTrue(valuableIsAccessible, "Valuable container is blocked by normal containers.");
        }

        [TestMethod]
        public void LoadShip_PlaceContainers_ShouldGivePlacedContainers()
        {
            // Arrange
            var ship = new Ship(3, 3); 
            var containers = new List<Container>
            {
                new Container(4, ContainerType.Normal),
                new Container(5, ContainerType.Normal),
                new Container(6, ContainerType.Normal),
                new Container(7, ContainerType.Normal),
                new Container(8, ContainerType.Normal),
                new Container(9, ContainerType.Normal)
            };

            var shipyard = new Shipyard();

            // Act
            List<Row> loadedRows = shipyard.LoadShip(ship, containers);

            // Assert
            int placedContainersCount = loadedRows.SelectMany(row => row.Stacks)
                                                  .SelectMany(stack => stack.Containers)
                                                  .Count();
            Assert.AreEqual(containers.Count, placedContainersCount, "Not all containers were placed as expected.");
        }

        [TestMethod]
        public void LoadShip_AddMultipleContainers_ShouldBalaceShip()
        {
            // Arrange
            var ship = new Ship(2, 2);
            var containers = new List<Container>
            {
                new Container(20, ContainerType.Normal),
                new Container(18, ContainerType.Normal),
                new Container(16, ContainerType.Normal),
                new Container(14, ContainerType.Normal),
                new Container(12, ContainerType.Normal)
            };

            var shipyard = new Shipyard();

            // Act
            List<Row> loadedRows = shipyard.LoadShip(ship, containers);

            // Assert
            int row1Weight = loadedRows[0].Stacks.Sum(s => s.GetTotalWeight());
            int row2Weight = loadedRows[1].Stacks.Sum(s => s.GetTotalWeight());
            int totalWeight = row1Weight + row2Weight;

            double weightDifference = Math.Abs(row1Weight - row2Weight);
            double allowableDifference = totalWeight * 0.2;

            Assert.IsTrue(weightDifference <= allowableDifference, "The weight distribution between rows is not balanced. Difference: " + weightDifference + ", Allowable: " + allowableDifference);
        }


        [TestMethod]
        public void LoadShip_AddMultipleContainersAndOnevaluable_ShouldPlaceValuableNotBlocked()
        {
            // Arrange
            var ship = new Ship(2, 2);
            var containers = new List<Container>
            {
                new Container(10, ContainerType.Valuable),
                new Container(12, ContainerType.Normal),
                new Container(14, ContainerType.Normal),
                new Container(16, ContainerType.Normal)
            };

            var shipyard = new Shipyard();

            // Act
            List<Row> loadedRows = shipyard.LoadShip(ship, containers);

            // Assert
            var valuableStack = loadedRows.SelectMany(row => row.Stacks)
                                          .FirstOrDefault(stack => stack.Containers.Any(c => c.ContainerType == ContainerType.Valuable));

            Assert.IsNotNull(valuableStack, "Valuable container should be placed.");
            Assert.IsTrue(valuableStack.Containers.Last().ContainerType == ContainerType.Valuable, "Valuable container should not be blocked by other containers.");
        }

        [TestMethod]
        public void LoadShip_AddMultipleContainersWithDifferentTypes_ShouldPlaceCorrect()
        {
            // Arrange
            var ship = new Ship(3, 3);
            var containers = new List<Container>
            {
                new Container(10, ContainerType.Valuable),
                new Container(12, ContainerType.Coolable),
                new Container(14, ContainerType.Normal),
                new Container(16, ContainerType.ValuableCooled),
                new Container(18, ContainerType.Normal),
                new Container(20, ContainerType.Normal)
            };

            var shipyard = new Shipyard();

            // Act
            List<Row> loadedRows = shipyard.LoadShip(ship, containers);

            // Assert
            Assert.AreEqual(3, loadedRows.Count);
            Assert.AreEqual(3, loadedRows[0].Stacks.Count);

            // Verify placement logic
            Assert.IsTrue(loadedRows[0].Stacks.Any(stack => stack.ContainsContainerType(ContainerType.ValuableCooled)), "Valuable cooled container should be in row 1.");
            Assert.IsTrue(loadedRows.SelectMany(row => row.Stacks).Any(stack => stack.Containers.Any(container => container.ContainerType == ContainerType.Coolable)), "Coolable container should be placed.");
            Assert.IsTrue(loadedRows.SelectMany(row => row.Stacks).Any(stack => stack.Containers.Any(container => container.ContainerType == ContainerType.Valuable)), "Valuable container should be placed.");
        }

    }
}
