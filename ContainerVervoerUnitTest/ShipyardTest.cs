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
        public void LoadShip_ContainersWithinWeightLimits_PlaceContainersCorrectly()
        {
            // Arrange
            var ship = new Ship(3, 3); // Creating a ship with 3 rows and 3 stacks per row
            var containers = new List<Container>
            {
                new Container(10000, ContainerType.ValuableCooled), // Valuable Cooled
                new Container(12000, ContainerType.Coolable),       // Coolable
                new Container(8000, ContainerType.Valuable),         // Valuable
                new Container(15000, ContainerType.Normal),          // Normal
                new Container(14000, ContainerType.Normal),
                new Container(13000, ContainerType.Normal)
            };

            var shipyard = new Shipyard();

            // Act
            List<Row> loadedRows = shipyard.LoadShip(ship, containers);

            // Assert
            Assert.AreEqual(3, loadedRows.Count); // Should have 3 rows
            Assert.AreEqual(3, loadedRows[0].Stacks.Count); // Each row should have 3 stacks

            // Verify placement of specific containers
            Assert.IsTrue(loadedRows[0].Stacks[0].ContainsValuableCooledContainer()); // Valuable Cooled container should be in stack 0 of row 0
            Assert.IsTrue(loadedRows[0].Stacks[1].ContainsContainer(containers[1])); // Coolable container should be in stack 1 of row 0
            Assert.IsTrue(loadedRows[1].Stacks[0].ContainsContainer(containers[2])); // Valuable container should be in stack 0 of row 1
            Assert.IsTrue(loadedRows[2].Stacks[0].ContainsContainer(containers[3])); // Normal container should be in stack 0 of row 2
            Assert.IsTrue(loadedRows[2].Stacks[1].ContainsContainer(containers[4])); // Normal container should be in stack 1 of row 2
            Assert.IsTrue(loadedRows[2].Stacks[2].ContainsContainer(containers[5])); // Normal container should be in stack 2 of row 2
        }

        [TestMethod]
        public void LoadShip_ContainersExceedMaxWeight_ShowWeightRequirementsWarning()
        {
            // Arrange
            var ship = new Ship(2, 2); // Creating a ship with 2 rows and 2 stacks per row
            var containers = new List<Container>
            {
                new Container(80000, ContainerType.Normal),  // Exceeds max weight
                new Container(40000, ContainerType.Normal),
                new Container(30000, ContainerType.Normal),
                new Container(30000, ContainerType.Normal)
            };

            var shipyard = new Shipyard();

            // Act
            List<Row> loadedRows = shipyard.LoadShip(ship, containers);

            // Assert
            Assert.AreEqual(2, loadedRows.Count); // Should have 2 rows
            Assert.AreEqual(2, loadedRows[0].Stacks.Count); // Each row should have 2 stacks

            // Verify that no containers are placed because they exceed the weight limit
            foreach (var row in loadedRows)
            {
                foreach (var stack in row.Stacks)
                {
                    Assert.AreEqual(0, stack.Containers.Count); // No containers should be placed in any stack
                }
            }
        }

        [TestMethod]
        public void LoadShip_ContainersBelowHalfMaxWeight_ShowWeightRequirementsWarning()
        {
            // Arrange
            var ship = new Ship(2, 2); // Creating a ship with 2 rows and 2 stacks per row
            var containers = new List<Container>
            {
                new Container(50000, ContainerType.Normal),
                new Container(10000, ContainerType.Normal),
                new Container(10000, ContainerType.Normal),
                new Container(10000, ContainerType.Normal)
            };

            var shipyard = new Shipyard();

            // Act
            List<Row> loadedRows = shipyard.LoadShip(ship, containers);

            // Assert
            Assert.AreEqual(2, loadedRows.Count); // Should have 2 rows
            Assert.AreEqual(2, loadedRows[0].Stacks.Count); // Each row should have 2 stacks

            // Verify that no containers are placed because they do not meet the weight requirements
            foreach (var row in loadedRows)
            {
                foreach (var stack in row.Stacks)
                {
                    Assert.AreEqual(0, stack.Containers.Count); // No containers should be placed in any stack
                }
            }
        }

        [TestMethod]
        public void LoadShip_ContainersWithPlacementWarnings_DisplayWarnings()
        {
            // Arrange
            var ship = new Ship(3, 3); // Creating a ship with 3 rows and 3 stacks per row
            var containers = new List<Container>
            {
                new Container(10000, ContainerType.ValuableCooled), // Valuable Cooled
                new Container(12000, ContainerType.ValuableCooled),
                new Container(14000, ContainerType.ValuableCooled),
                new Container(16000, ContainerType.ValuableCooled),
                new Container(8000, ContainerType.Valuable),         // Valuable
                new Container(15000, ContainerType.Normal),          // Normal
                new Container(14000, ContainerType.Normal)
            };

            var shipyard = new Shipyard();

            // Act
            List<Row> loadedRows = shipyard.LoadShip(ship, containers);

            // Assert
            Assert.AreEqual(3, loadedRows.Count); // Should have 3 rows
            Assert.AreEqual(3, loadedRows[0].Stacks.Count); // Each row should have 3 stacks
        }

        [TestMethod]
        public void LoadShip_ExactMaxWeight_PlaceContainersCorrectly()
        {
            // Arrange
            var ship = new Ship(3, 3); // Creating a ship with 3 rows and 3 stacks per row
            var containers = new List<Container>
            {
                new Container(15000, ContainerType.Normal),  // Normal
                new Container(15000, ContainerType.Normal),
                new Container(15000, ContainerType.Normal),
                new Container(15000, ContainerType.Normal),
                new Container(15000, ContainerType.Normal),
                new Container(15000, ContainerType.Normal),
                new Container(15000, ContainerType.Normal),
                new Container(15000, ContainerType.Normal),
                new Container(15000, ContainerType.Normal),
                new Container(15000, ContainerType.Normal),
                new Container(15000, ContainerType.Normal),
                new Container(15000, ContainerType.Normal)
            };

            var shipyard = new Shipyard();

            // Act
            List<Row> loadedRows = shipyard.LoadShip(ship, containers);

            // Assert
            Assert.AreEqual(3, loadedRows.Count); // Should have 3 rows
            Assert.AreEqual(3, loadedRows[0].Stacks.Count); // Each row should have 3 stacks

            // Verify placement of specific containers
            Assert.AreEqual(3, loadedRows[0].Stacks[0].GetStackLength()); // Stack 0 of row 0 should have 3 containers
            Assert.AreEqual(3, loadedRows[0].Stacks[1].GetStackLength()); // Stack 1 of row 0 should have 3 containers
            Assert.AreEqual(3, loadedRows[0].Stacks[2].GetStackLength()); // Stack 2 of row 0 should have 3 containers
            Assert.AreEqual(3, loadedRows[1].Stacks[0].GetStackLength()); // Stack 0 of row 1 should have 3 containers
            Assert.AreEqual(0, loadedRows[1].Stacks[1].GetStackLength()); // Stack 1 of row 1 should have 0 containers
            Assert.AreEqual(0, loadedRows[1].Stacks[2].GetStackLength()); // Stack 2 of row 1 should have 0 containers
            Assert.AreEqual(0, loadedRows[2].Stacks[0].GetStackLength()); // Stack 0 of row 2 should have 0 containers
            Assert.AreEqual(0, loadedRows[2].Stacks[1].GetStackLength()); // Stack 1 of row 2 should have 0 containers
            Assert.AreEqual(0, loadedRows[2].Stacks[2].GetStackLength()); // Stack 2 of row 2 should have 0 containers
        }

        [TestMethod]
        public void LoadShip_SingleContainerExceedsMaxWeight_ShowWeightRequirementsWarning()
        {
            // Arrange
            var ship = new Ship(2, 2); // Creating a ship with 2 rows and 2 stacks per row
            var containers = new List<Container>
            {
                new Container(160000, ContainerType.Normal),  // Exceeds max weight
                new Container(30000, ContainerType.Normal),
                new Container(30000, ContainerType.Normal),
                new Container(30000, ContainerType.Normal)
            };

            var shipyard = new Shipyard();

            // Act
            List<Row> loadedRows = shipyard.LoadShip(ship, containers);

            // Assert
            Assert.AreEqual(2, loadedRows.Count); // Should have 2 rows
            Assert.AreEqual(2, loadedRows[0].Stacks.Count); // Each row should have 2 stacks

            // Verify that no containers are placed because they exceed the weight limit
            foreach (var row in loadedRows)
            {
                foreach (var stack in row.Stacks)
                {
                    Assert.AreEqual(0, stack.GetStackLength()); // No containers should be placed in any stack
                }
            }
        }

        [TestMethod]
        public void LoadShip_SingleContainerBelowMinWeight_ShowWeightRequirementsWarning()
        {
            // Arrange
            var ship = new Ship(2, 2); // Creating a ship with 2 rows and 2 stacks per row
            var containers = new List<Container>
            {
                new Container(10000, ContainerType.Normal),
                new Container(10000, ContainerType.Normal),
                new Container(10000, ContainerType.Normal),
                new Container(10000, ContainerType.Normal)
            };

            var shipyard = new Shipyard();

            // Act
            List<Row> loadedRows = shipyard.LoadShip(ship, containers);

            // Assert
            Assert.AreEqual(2, loadedRows.Count); // Should have 2 rows
            Assert.AreEqual(2, loadedRows[0].Stacks.Count); // Each row should have 2 stacks

            // Verify that no containers are placed because they do not meet the weight requirements
            foreach (var row in loadedRows)
            {
                foreach (var stack in row.Stacks)
                {
                    Assert.AreEqual(0, stack.GetStackLength()); // No containers should be placed in any stack
                }
            }
        }

        [TestMethod]
        public void LoadShip_ContainersWithMaxValuableCooledContainers_PlaceContainersCorrectly()
        {
            // Arrange
            var ship = new Ship(3, 3); // Creating a ship with 3 rows and 3 stacks per row
            var containers = new List<Container>
            {
                new Container(10000, ContainerType.ValuableCooled), // Valuable Cooled
                new Container(12000, ContainerType.ValuableCooled),
                new Container(14000, ContainerType.ValuableCooled),
                new Container(16000, ContainerType.ValuableCooled),
                new Container(8000, ContainerType.Valuable),         // Valuable
                new Container(15000, ContainerType.Normal),          // Normal
                new Container(14000, ContainerType.Normal)
            };

            var shipyard = new Shipyard();

            // Act
            List<Row> loadedRows = shipyard.LoadShip(ship, containers);

            // Assert
            Assert.AreEqual(3, loadedRows.Count); // Should have 3 rows
            Assert.AreEqual(3, loadedRows[0].Stacks.Count); // Each row should have 3 stacks

            // Verify placement of specific containers
            Assert.IsTrue(loadedRows[0].Stacks[0].ContainsValuableCooledContainer()); // Valuable Cooled container should be in stack 0 of row 0
            Assert.IsTrue(loadedRows[0].Stacks[1].ContainsContainer(containers[1])); // Valuable Cooled container should be in stack 1 of row 0
            Assert.IsTrue(loadedRows[0].Stacks[2].ContainsContainer(containers[2])); // Valuable Cooled container should be in stack 2 of row 0
            Assert.IsTrue(loadedRows[1].Stacks[0].ContainsContainer(containers[3])); // Valuable Cooled container should be in stack 0 of row 1
            Assert.IsTrue(loadedRows[1].Stacks[1].ContainsContainer(containers[4])); // Valuable container should be in stack 1 of row 1
            Assert.IsTrue(loadedRows[2].Stacks[0].ContainsContainer(containers[5])); // Normal container should be in stack 0 of row 2
            Assert.IsTrue(loadedRows[2].Stacks[1].ContainsContainer(containers[6])); // Normal container should be in stack 1 of row 2
        }

        [TestMethod]
        public void LoadShip_ManyContainersWithMultipleRowsAndStacks_PlaceContainersCorrectly()
        {
            // Arrange
            var ship = new Ship(4, 4); // Creating a ship with 4 rows and 4 stacks per row
            var containers = new List<Container>
            {
                new Container(100000, ContainerType.ValuableCooled), // Valuable Cooled
                new Container(120000, ContainerType.Coolable),       // Coolable
                new Container(140000, ContainerType.Valuable),         // Valuable
                new Container(16000, ContainerType.Normal),          // Normal
                new Container(100000, ContainerType.ValuableCooled), // Valuable Cooled
                new Container(120000, ContainerType.Coolable),       // Coolable
                new Container(140000, ContainerType.Valuable),         // Valuable
                new Container(16000, ContainerType.Normal),          // Normal
                new Container(100000, ContainerType.ValuableCooled), // Valuable Cooled
                new Container(120000, ContainerType.Coolable),       // Coolable
                new Container(140000, ContainerType.Valuable),         // Valuable
                new Container(16000, ContainerType.Normal),          // Normal
                new Container(100000, ContainerType.ValuableCooled), // Valuable Cooled
                new Container(120000, ContainerType.Coolable),       // Coolable
                new Container(140000, ContainerType.Valuable),         // Valuable
                new Container(16000, ContainerType.Normal),          // Normal
                new Container(10000, ContainerType.ValuableCooled), // Valuable Cooled
                new Container(12000, ContainerType.Coolable),       // Coolable
                new Container(14000, ContainerType.Valuable),         // Valuable
                new Container(16000, ContainerType.Normal),          // Normal
                new Container(10000, ContainerType.ValuableCooled), // Valuable Cooled
                new Container(12000, ContainerType.Coolable),       // Coolable
                new Container(14000, ContainerType.Valuable),         // Valuable
                new Container(16000, ContainerType.Normal),          // Normal
                new Container(10000, ContainerType.ValuableCooled), // Valuable Cooled
                new Container(12000, ContainerType.Coolable),       // Coolable
                new Container(14000, ContainerType.Valuable),         // Valuable
                new Container(16000, ContainerType.Normal)           // Normal

                
                    

            };

            var shipyard = new Shipyard();

            // Act
            List<Row> loadedRows = shipyard.LoadShip(ship, containers);

            // Assert
            Assert.AreEqual(4, loadedRows.Count); // Should have 4 rows
            Assert.AreEqual(4, loadedRows[0].Stacks.Count); // Each row should have 4 stacks

            // Verify placement of specific containers
            Assert.IsTrue(loadedRows[0].Stacks[0].ContainsValuableCooledContainer()); // Valuable Cooled container should be in stack 0 of row 0
            Assert.IsTrue(loadedRows[0].Stacks[1].ContainsContainer(containers[1])); // Coolable container should be in stack 1 of row 0
            Assert.IsTrue(loadedRows[0].Stacks[2].ContainsContainer(containers[2])); // Valuable container should be in stack 2 of row 0
            Assert.IsTrue(loadedRows[0].Stacks[3].ContainsContainer(containers[3])); // Normal container should be in stack 3 of row 0

            Assert.IsTrue(loadedRows[1].Stacks[0].ContainsContainer(containers[4])); // Valuable Cooled container should be in stack 0 of row 1
            Assert.IsTrue(loadedRows[1].Stacks[1].ContainsContainer(containers[5])); // Coolable container should be in stack 1 of row 1
            Assert.IsTrue(loadedRows[1].Stacks[2].ContainsContainer(containers[6])); // Valuable container should be in stack 2 of row 1
            Assert.IsTrue(loadedRows[1].Stacks[3].ContainsContainer(containers[7])); // Normal container should be in stack 3 of row 1

            Assert.IsTrue(loadedRows[2].Stacks[0].ContainsContainer(containers[8])); // Valuable Cooled container should be in stack 0 of row 2
            Assert.IsTrue(loadedRows[2].Stacks[1].ContainsContainer(containers[9])); // Coolable container should be in stack 1 of row 2
            Assert.IsTrue(loadedRows[2].Stacks[2].ContainsContainer(containers[10])); // Valuable container should be in stack 2 of row 2
            Assert.IsTrue(loadedRows[2].Stacks[3].ContainsContainer(containers[11])); // Normal container should be in stack 3 of row 2

            Assert.IsTrue(loadedRows[3].Stacks[0].ContainsContainer(containers[12])); // Valuable Cooled container should be in stack 0 of row 3
            Assert.IsTrue(loadedRows[3].Stacks[1].ContainsContainer(containers[13])); // Coolable container should be in stack 1 of row 3
            Assert.IsTrue(loadedRows[3].Stacks[2].ContainsContainer(containers[14])); // Valuable container should be in stack 2 of row 3
            Assert.IsTrue(loadedRows[3].Stacks[3].ContainsContainer(containers[15])); // Normal container should be in stack 3 of row 3
        }

        [TestMethod]
        public void LoadShip_EmptyContainerList_NoContainersPlaced()
        {
            // Arrange
            var ship = new Ship(2, 2); // Creating a ship with 2 rows and 2 stacks per row
            var containers = new List<Container>(); // Empty container list

            var shipyard = new Shipyard();

            // Act
            List<Row> loadedRows = shipyard.LoadShip(ship, containers);

            // Assert
            Assert.AreEqual(2, loadedRows.Count); // Should have 2 rows
            Assert.AreEqual(2, loadedRows[0].Stacks.Count); // Each row should have 2 stacks

            // Verify that no containers are placed because the list is empty
            foreach (var row in loadedRows)
            {
                foreach (var stack in row.Stacks)
                {
                    Assert.AreEqual(0, stack.GetStackLength()); // No containers should be placed in any stack
                }
            }
        }

        [TestMethod]
        public void LoadShip_AllContainersUnplaceable_DisplayWarnings()
        {
            // Arrange
            var ship = new Ship(2, 2); // Creating a ship with 2 rows and 2 stacks per row
            var containers = new List<Container>
            {
                new Container(10000, ContainerType.ValuableCooled), // Valuable Cooled
                new Container(10000, ContainerType.ValuableCooled),
                new Container(10000, ContainerType.ValuableCooled),
                new Container(10000, ContainerType.ValuableCooled),
                new Container(10000, ContainerType.ValuableCooled),
                new Container(10000, ContainerType.ValuableCooled),
                new Container(10000, ContainerType.ValuableCooled),
                new Container(10000, ContainerType.ValuableCooled)
            };

            var shipyard = new Shipyard();

            // Act
            List<Row> loadedRows = shipyard.LoadShip(ship, containers);

            // Assert
            Assert.AreEqual(2, loadedRows.Count); // Should have 2 rows
            Assert.AreEqual(2, loadedRows[0].Stacks.Count); // Each row should have 2 stacks
        }

        [TestMethod]
        public void LoadShip_ContainersWithEqualWeightsAndLimitedSpace_PlaceContainersEqually()
        {
            // Arrange
            var ship = new Ship(3, 3); // Creating a ship with 3 rows and 3 stacks per row
            var containers = new List<Container>
            {
                new Container(10000, ContainerType.ValuableCooled), // Valuable Cooled
                new Container(10000, ContainerType.Coolable),       // Coolable
                new Container(10000, ContainerType.Valuable),         // Valuable
                new Container(10000, ContainerType.Normal),          // Normal
                new Container(10000, ContainerType.Normal),
                new Container(10000, ContainerType.Normal)
            };

            var shipyard = new Shipyard();

            // Act
            List<Row> loadedRows = shipyard.LoadShip(ship, containers);

            // Assert
            Assert.AreEqual(3, loadedRows.Count); // Should have 3 rows
            Assert.AreEqual(3, loadedRows[0].Stacks.Count); // Each row should have 3 stacks

            // Verify placement of specific containers
            Assert.AreEqual(1, loadedRows[0].Stacks[0].GetStackLength()); // Stack 0 of row 0 should have 1 container
            Assert.AreEqual(1, loadedRows[0].Stacks[1].GetStackLength()); // Stack 1 of row 0 should have 1 container
            Assert.AreEqual(1, loadedRows[0].Stacks[2].GetStackLength()); // Stack 2 of row 0 should have 1 container
            Assert.AreEqual(1, loadedRows[1].Stacks[0].GetStackLength()); // Stack 0 of row 1 should have 1 container
            Assert.AreEqual(1, loadedRows[1].Stacks[1].GetStackLength()); // Stack 1 of row 1 should have 1 container
            Assert.AreEqual(1, loadedRows[1].Stacks[2].GetStackLength()); // Stack 2 of row 1 should have 1 container
            Assert.AreEqual(1, loadedRows[2].Stacks[0].GetStackLength()); // Stack 0 of row 2 should have 1 container
            Assert.AreEqual(1, loadedRows[2].Stacks[1].GetStackLength()); // Stack 1 of row 2 should have 1 container
            Assert.AreEqual(0, loadedRows[2].Stacks[2].GetStackLength()); // Stack 2 of row 2 should have 0 containers
        }

    }
}
