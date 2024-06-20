using ContainerVervoer;
namespace ContainerVervoerUnitTest
{
    [TestClass]
    public class ContainerTest
    {
        [TestMethod]
        public void Constructor_ValidWeight_SetsWeight()
        {
            // Arrange
            int weight = 10; // Net weight without the empty container weight
            ContainerType containerType = ContainerType.Normal;

            // Act
            var container = new Container(weight, containerType);         

            // Assert
            Assert.AreEqual(weight*1000 + 4000, container.Weight);
        }
        [TestMethod]
        public void PlaceInStack_ValidStack_AddsContainer()
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
        public void PlaceInStack_ExceedsStackWeight_DoesNotAddContainer()
        {
            // Arrange
            var stack = new Stack();
            for (int i = 0; i < 15; i++) // Add enough containers to exceed the weight limit
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
            Assert.AreEqual(container, stack.Containers[0]); // It should be the first container in the stack
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
        public void WithinWeightLimit_ShouldReturnsTrue()
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
        public void CanAddToStack_ExceedsWeightLimit_ShouldReturnsFalse()
        {
            // Arrange
            var stack = new Stack();
            for (int i = 0; i < 15; i++) // Add enough containers to reach the weight limit
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
        public void Container_AddToStack_ShouldAddsAtEnd()
        {
            // Arrange
            var stack = new Stack();
            var container1 = new Container(10, ContainerType.Normal);
            var container2 = new Container(5, ContainerType.Normal);

            // Act
            container1.CanPlaceInStack(stack, new List<Container>());
            container2.CanPlaceInStack(stack, new List<Container>());

            // Assert
            Assert.AreEqual(container1, stack.Containers[0]);
            Assert.AreEqual(container2, stack.Containers[1]);
        }

        [TestMethod]
        public void Container_CannotAddToFullStack_ReturnsFalse()
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
        public void Container_Constructor_ValidatesWeightCorrectly()
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
        public void PlaceInStack_ValidStackAndContainer_AddsToStack()
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
        public void AddContainer_CanAddMultipleNormalContainers()
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
        [ExpectedException(typeof(Exception), "De container weegt teveel. De container weegt 35000 en maximaal 30000 toegelaten")]
        public void Container_Constructor_InvalidWeight_ThrowsException()
        {
            // Arrange
            int weight = 31; // Exceeds max weight when added with empty container weight
            ContainerType containerType = ContainerType.Normal;

            // Act
            var container = new Container(weight, containerType);
        }


        [TestMethod]
        public void CanPlaceInStack_StackHasSpace_AddsContainer()
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
        public void PlaceInStack_ExceedsWeightLimit_DoesNotAddContainer()
        {
            // Arrange
            var stack = new Stack();
            for (int i = 0; i < 15; i++) // Add enough containers to exceed the weight limit
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
        public void CanAddContainer_ValidatesWeightCorrectly_ReturnsTrue()
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
        public void CanAddContainer_ExceedsWeightLimit_ReturnsFalse()
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
        public void Container_ValidatesTypeAndWeight()
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
        public void PlaceNormalContainer_StackWithValuable_AddsContainer()
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

        [TestMethod]
        public void AddContainer_ValidWeight_AddsToStack()
        {
            // Arrange
            var stack = new Stack();
            var container = new Container(10, ContainerType.Normal);

            // Act
            stack.AddContainer(container);

            // Assert
            Assert.IsTrue(stack.Containers.Contains(container));
        }
    }
}
