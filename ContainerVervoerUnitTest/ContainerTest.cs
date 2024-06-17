using ContainerVervoer;
namespace ContainerVervoerUnitTest
{
    [TestClass]
    public class ContainerTest
    {
        [TestMethod]
        public void Container_Constructor_ValidWeight_SetsWeight()
        {
            // Arrange
            int weight = 10000; // Net weight without the empty container weight
            ContainerType containerType = ContainerType.Normal;

            // Act
            var container = new Container(weight, containerType);         

            // Assert
            Assert.AreEqual(weight + 4000, container.Weight);
        }
        [TestMethod]
        public void PlaceInStack_ValidStack_AddsContainer()
        {
            // Arrange
            var stack = new Stack();
            var container = new Container(10000, ContainerType.Normal);
            var placedContainers = new List<Container>();

            // Act
            container.PlaceInStack(stack, placedContainers);

            // Assert
            CollectionAssert.Contains(stack.Containers, container);
            CollectionAssert.Contains(placedContainers, container);
        }

        [TestMethod]
        public void PlaceInStack_ExceedsStackWeight_DoesNotAddContainer()
        {
            // Arrange
            var stack = new Stack();
            for (int i = 0; i < 15; i++) // Add enough containers to exceed the weight limit
            {
                stack.AddContainer(new Container(10000, ContainerType.Normal));
            }
            var container = new Container(10000, ContainerType.Normal);
            var placedContainers = new List<Container>();

            // Act
            container.PlaceInStack(stack, placedContainers);

            // Assert
            Assert.IsFalse(stack.Containers.Contains(container));
            Assert.IsFalse(placedContainers.Contains(container));
        }

        [TestMethod]
        public void PlaceValuableInStack_ValidStack_AddsValuableContainer()
        {
            // Arrange
            var stack = new Stack();
            var container = new Container(10000, ContainerType.Valuable);
            var placedContainers = new List<Container>();

            // Act
            container.PlaceValuableInStack(stack, placedContainers);

            // Assert
            CollectionAssert.Contains(stack.Containers, container);
            Assert.AreEqual(container, stack.Containers[0]); // It should be the first container in the stack
            CollectionAssert.Contains(placedContainers, container);
        }

        [TestMethod]
        public void PlaceValuableInStack_ExceedsStackWeight_DoesNotAddValuableContainer()
        {
            // Arrange
            var stack = new Stack();
            for (int i = 0; i < 15; i++) // Add enough containers to exceed the weight limit
            {
                stack.AddContainer(new Container(10000, ContainerType.Normal));
            }
            var container = new Container(10000, ContainerType.Valuable);
            var placedContainers = new List<Container>();

            // Act
            container.PlaceValuableInStack(stack, placedContainers);

            // Assert
            Assert.IsFalse(stack.Containers.Contains(container));
            Assert.IsFalse(placedContainers.Contains(container));
        }
        [TestMethod]
        public void CanAddToStack_WithinWeightLimit_ReturnsTrue()
        {
            // Arrange
            var stack = new Stack();
            var container = new Container(10000, ContainerType.Normal);

            // Act
            bool canAdd = stack.CanAddContainer(container);

            // Assert
            Assert.IsTrue(canAdd);
        }

        [TestMethod]
        public void Container_CanAddToStack_ExceedsWeightLimit_ReturnsFalse()
        {
            // Arrange
            var stack = new Stack();
            for (int i = 0; i < 15; i++) // Add enough containers to reach the weight limit
            {
                stack.AddContainer(new Container(10000, ContainerType.Normal));
            }
            var container = new Container(10000, ContainerType.Normal);

            // Act
            bool canAdd = stack.CanAddContainer(container);

            // Assert
            Assert.IsFalse(canAdd);
        }

        [TestMethod]
        public void Container_AddToStack_AddsAtEnd()
        {
            // Arrange
            var stack = new Stack();
            var container1 = new Container(10000, ContainerType.Normal);
            var container2 = new Container(5000, ContainerType.Normal);

            // Act
            container1.PlaceInStack(stack, new List<Container>());
            container2.PlaceInStack(stack, new List<Container>());

            // Assert
            Assert.AreEqual(container1, stack.Containers[0]);
            Assert.AreEqual(container2, stack.Containers[1]);
        }

        [TestMethod]
        public void Container_AddValueableContainer_AddsAtStart()
        {
            // Arrange
            var stack = new Stack();
            var container1 = new Container(10000, ContainerType.Normal);
            var valuableContainer = new Container(5000, ContainerType.Valuable);

            // Act
            container1.PlaceInStack(stack, new List<Container>());
            valuableContainer.PlaceValuableInStack(stack, new List<Container>());

            // Assert
            Assert.AreEqual(valuableContainer, stack.Containers[0]);
            Assert.AreEqual(container1, stack.Containers[1]);
        }

        // Ensure previously written tests are included...

        [TestMethod]
        public void Container_CannotAddToFullStack_ReturnsFalse()
        {
            // Arrange
            var stack = new Stack();
            for (int i = 0; i < 15; i++) 
            {
                stack.AddContainer(new Container(10000, ContainerType.Normal));
            }
            var container = new Container(10000, ContainerType.Normal);

            // Act
            bool canAdd = stack.CanAddContainer(container);

            // Assert
            Assert.IsFalse(canAdd);
        }

        [TestMethod]
        public void Container_Constructor_ValidatesWeightCorrectly()
        {
            // Arrange
            int weight = 100000;
            ContainerType containerType = ContainerType.Normal;

            // Act
            var container = new Container(weight, containerType);

            // Assert
            Assert.AreEqual(weight + 4000, container.Weight);
        }

        [TestMethod]
        public void PlaceInStack_ValidStackAndContainer_AddsToStack()
        {
            // Arrange
            var stack = new Stack();
            var container = new Container(10000, ContainerType.Normal);
            var placedContainers = new List<Container>();

            // Act
            container.PlaceInStack(stack, placedContainers);

            // Assert
            Assert.IsTrue(stack.Containers.Contains(container));
            Assert.IsTrue(placedContainers.Contains(container));
        }

        [TestMethod]
        public void AddContainer_CanAddMultipleNormalContainers()
        {
            // Arrange
            var stack = new Stack();
            var container1 = new Container(10000, ContainerType.Normal);
            var container2 = new Container(5000, ContainerType.Normal);

            // Act
            stack.AddContainer(container1);
            stack.AddContainer(container2);

            // Assert
            Assert.AreEqual(2, stack.Containers.Count);
            Assert.AreEqual(container1, stack.Containers[0]);
            Assert.AreEqual(container2, stack.Containers[1]);
        }
    }
}
