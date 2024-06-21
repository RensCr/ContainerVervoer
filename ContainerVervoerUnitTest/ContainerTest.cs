using ContainerVervoer;
namespace ContainerVervoerUnitTest
{
    [TestClass]
    public class ContainerTest
    {
        [TestMethod]
        public void Constructor_ValidWeight_ShouldReturnCorrectWeight()
        {
            // Arrange
            int weight = 10; 
            ContainerType containerType = ContainerType.Normal;

            // Act
            var container = new Container(weight, containerType);         

            // Assert
            Assert.AreEqual(weight*1000 + 4000, container.Weight);
        }
        [TestMethod]
        public void CanPlaceInStack_ValidStack_AddsContainer()
        {
            // Arrange
            var stack = new Stack();
            var container = new Container(10, ContainerType.Normal);
            var placedContainers = new List<Container>();

            // Act
            container.CanPlaceInStack(stack, placedContainers);

            // Assert
            CollectionAssert.Contains(stack.Containers, container);
            CollectionAssert.Contains(placedContainers, container);
        }

        [TestMethod]
        public void CanPlaceInStack_ExceedsStackWeight_DoesNotAddContainer()
        {
            // Arrange
            var stack = new Stack();
            for (int i = 0; i < 15; i++) 
            {
                stack.AddContainer(new Container(10, ContainerType.Normal));
            }
            var container = new Container(10, ContainerType.Normal);
            var placedContainers = new List<Container>();

            // Act
            container.CanPlaceInStack(stack, placedContainers);

            // Assert
            Assert.IsFalse(stack.Containers.Contains(container));
            Assert.IsFalse(placedContainers.Contains(container));
        }

        [TestMethod]
        public void PlaceValuableInStack_ValidStack_AddsValuableContainer()
        {
            // Arrange
            var stack = new Stack();
            var container = new Container(10, ContainerType.Valuable);
            var placedContainers = new List<Container>();

            // Act
            container.PlaceValuableInStack(stack, placedContainers);

            // Assert
            CollectionAssert.Contains(stack.Containers, container);
            Assert.AreEqual(container, stack.Containers[0]);
            CollectionAssert.Contains(placedContainers, container);
        }

        [TestMethod]
        public void PlaceValuableInStack_ExceedsStackWeight_DoesNotAddValuableContainer()
        {
            // Arrange
            var stack = new Stack();
            for (int i = 0; i < 15; i++)
            {
                stack.AddContainer(new Container(10, ContainerType.Normal));
            }
            var container = new Container(10, ContainerType.Valuable);
            var placedContainers = new List<Container>();

            // Act
            container.PlaceValuableInStack(stack, placedContainers);

            // Assert
            Assert.IsFalse(stack.Containers.Contains(container));
            Assert.IsFalse(placedContainers.Contains(container));
        }
        [TestMethod]
        public void CanAddContainer_WithinWeightLimit_ShouldReturnsTrue()
        {
            // Arrange
            var stack = new Stack();
            var container = new Container(10, ContainerType.Normal);

            // Act
            bool canAdd = stack.CanAddContainer(container);

            // Assert
            Assert.IsTrue(canAdd);
        }

        [TestMethod]
        public void CanAddContainer_ExceedsWeightLimit_ShouldReturnsFalse()
        {
            // Arrange
            var stack = new Stack();
            for (int i = 0; i < 15; i++) 
            {
                stack.AddContainer(new Container(10, ContainerType.Normal));
            }
            var container = new Container(10, ContainerType.Normal);

            // Act
            bool canAdd = stack.CanAddContainer(container);

            // Assert
            Assert.IsFalse(canAdd);
        }


        [TestMethod]
        public void CanAddContainer_CannotAddToFullStack_ReturnsFalse()
        {
            // Arrange
            var stack = new Stack();
            for (int i = 0; i < 15; i++) 
            {
                stack.AddContainer(new Container(10, ContainerType.Normal));
            }
            var container = new Container(10, ContainerType.Normal);

            // Act
            bool canAdd = stack.CanAddContainer(container);

            // Assert
            Assert.IsFalse(canAdd);
        }

        [TestMethod]
        public void Constructor_ValidatesWeightCorrectly_ShouldReturnValidWeight()
        {
            // Arrange
            int weight = 10;
            ContainerType containerType = ContainerType.Normal;

            // Act
            var container = new Container(weight, containerType);

            // Assert
            Assert.AreEqual(weight*1000 + 4000, container.Weight);
        }

        [TestMethod]
        public void CanPlaceInStack_ValidStackAndContainer_ShouldReturnTrue()
        {
            // Arrange
            var stack = new Stack();
            var container = new Container(10, ContainerType.Normal);
            var placedContainers = new List<Container>();

            // Act
            container.CanPlaceInStack(stack, placedContainers);

            // Assert
            Assert.IsTrue(stack.Containers.Contains(container));
            Assert.IsTrue(placedContainers.Contains(container));
        }

        [TestMethod]
        public void AddContainer_CanAddMultipleNormalContainers_shouldReturnAllContainers()
        {
            // Arrange
            var stack = new Stack();
            var container1 = new Container(10, ContainerType.Normal);
            var container2 = new Container(5, ContainerType.Normal);

            // Act
            stack.AddContainer(container1);
            stack.AddContainer(container2);

            // Assert
            Assert.AreEqual(2, stack.Containers.Count);
            Assert.AreEqual(container1, stack.Containers[0]);
            Assert.AreEqual(container2, stack.Containers[1]);
        }

        [TestMethod]
        public void CanPlaceInStack_AddContainersToStack_ShouldReturnTrue()
        {
            // Arrange
            var stack = new Stack();
            var container = new Container(10, ContainerType.Normal);
            var placedContainers = new List<Container>();

            // Act
            bool result = container.CanPlaceInStack(stack, placedContainers);

            // Assert
            Assert.IsTrue(result);
            Assert.IsTrue(stack.Containers.Contains(container));
            Assert.IsTrue(placedContainers.Contains(container));
        }

        [TestMethod]
        public void CanPlaceInStack_ExceedsWeightLimit_ShouldReturnFalse()
        {
            // Arrange
            var stack = new Stack();
            for (int i = 0; i < 15; i++)
            {
                stack.AddContainer(new Container(10, ContainerType.Normal));
            }
            var container = new Container(10, ContainerType.Normal);
            var placedContainers = new List<Container>();

            // Act
            container.CanPlaceInStack(stack, placedContainers);

            // Assert
            Assert.IsFalse(stack.Containers.Contains(container));
            Assert.IsFalse(placedContainers.Contains(container));
        }

        [TestMethod]
        public void CanAddContainer_ValidatesWeightCorrectly_ShouldReturnTrue()
        {
            // Arrange
            var stack = new Stack();
            var container = new Container(10, ContainerType.Normal);

            // Act
            bool result = stack.CanAddContainer(container);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanAddContainer_ExceedsWeightLimit_ShouldReturnFalse()
        {
            // Arrange
            var stack = new Stack();
            for (int i = 0; i < 15; i++)
            {
                stack.AddContainer(new Container(10, ContainerType.Normal));
            }
            var container = new Container(10, ContainerType.Normal);

            // Act
            bool result = stack.CanAddContainer(container);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Container_ValidatesTypeAndWeight_ShouldGiveCorrectContainerInformation()
        {
            // Arrange
            int weight = 5;
            ContainerType containerType = ContainerType.ValuableCooled;

            // Act
            var container = new Container(weight, containerType);

            // Assert
            Assert.AreEqual(weight*1000 + 4000, container.Weight);
            Assert.AreEqual(ContainerType.ValuableCooled, container.ContainerType);
        }

        [TestMethod]
        public void CanPlaceInstack_PlaceContainerInStackWithValuableContainer_ShoudlReturnTrue()
        {
            // Arrange
            var stack = new Stack();
            var valuableContainer = new Container(10, ContainerType.Valuable);
            stack.AddContainer(valuableContainer);

            var normalContainer = new Container(5, ContainerType.Normal);
            var placedContainers = new List<Container>();

            // Act
            normalContainer.CanPlaceInStack(stack, placedContainers);

            // Assert
            Assert.IsTrue(stack.Containers.Contains(normalContainer));
            Assert.IsTrue(placedContainers.Contains(normalContainer));
        }
    }
}
