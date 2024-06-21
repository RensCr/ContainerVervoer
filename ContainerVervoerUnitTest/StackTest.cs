using ContainerVervoer;
namespace ContainerVervoerUnitTest
{
    [TestClass]
    public class StackTest
    {
        [TestMethod]
        public void AddContainer_ContainerCanBeAdded_ShouldReturnsTrue()
        {
            // Arrange
            var stack = new Stack();
            var container = new Container(5, ContainerType.Normal);

            // Act
            bool added = stack.CanAddContainer(container);

            // Assert
            Assert.IsTrue(added);
        }

        [TestMethod]
        public void AddContainer_AddMultipleContainers_ShouldReturnTrue()
        {
            // Arrange
            var stack = new Stack();
            var container1 = new Container(2, ContainerType.Normal);
            var container2 = new Container(3, ContainerType.Normal); 

            // Act
            stack.AddContainer(container1);
            bool added = stack.CanAddContainer(container2);

            // Assert
            Assert.IsTrue(added);
        }

        [TestMethod]
        public void AddContainer_AddsContainerToStack_ShouldReturnCorrectStackInfo()
        {
            // Arrange
            var stack = new Stack();
            var container = new Container(5, ContainerType.Normal);

            // Act
            stack.AddContainer(container);

            // Assert
            Assert.AreEqual(1, stack.Containers.Count);
            Assert.IsTrue(stack.Containers.Contains(container));
        }

        [TestMethod]
        public void AddValueableContainer_AddsValuableContainerToTop_ShouldGiveCorrectStackInfo()
        {
            // Arrange
            var stack = new Stack();
            var valuableContainer1 = new Container(5, ContainerType.Valuable);
            var valuableContainer2 = new Container(6, ContainerType.Valuable);

            // Act
            stack.AddValueableContainer(valuableContainer1);
            stack.AddValueableContainer(valuableContainer2);

            // Assert
            Assert.AreEqual(2, stack.Containers.Count);
            Assert.AreEqual(valuableContainer2, stack.Containers[0]);
        }

        [TestMethod]
        public void ContainsContainerType_StackContainsValuableCooledContainer_ShouldReturnsTrue()
        {
            // Arrange
            var stack = new Stack();
            var valuableCooledContainer = new Container(5, ContainerType.ValuableCooled);
            stack.AddContainer(new Container(10, ContainerType.Normal));
            stack.AddContainer(valuableCooledContainer);

            // Act
            bool containsValuableCooled = stack.ContainsContainerType(ContainerType.ValuableCooled);

            // Assert
            Assert.IsTrue(containsValuableCooled);
        }

        [TestMethod]
        public void ContainsContainerType_StackDoesNotContainValuableCooledContainer_ShouldReturnsFalse()
        {
            // Arrange
            var stack = new Stack();
            stack.AddContainer(new Container(10, ContainerType.Normal));

            // Act
            bool containsValuableCooled = stack.ContainsContainerType(ContainerType.ValuableCooled);

            // Assert
            Assert.IsFalse(containsValuableCooled);
        }

        [TestMethod]
        public void ContainsContainerType_StackContainsValuableContainer_ShouldReturnsTrue()
        {
            // Arrange
            var stack = new Stack();
            var valuableContainer = new Container(5, ContainerType.Valuable);
            stack.AddContainer(new Container(10, ContainerType.Normal)); 
            stack.AddContainer(valuableContainer);

            // Act
            bool containsValuable = stack.ContainsContainerType(ContainerType.Valuable);

            // Assert
            Assert.IsTrue(containsValuable);
        }

        [TestMethod]
        public void ContainsContainerType_StackDoesNotContainValuableContainer_ShouldReturnsFalse()
        {
            // Arrange
            var stack = new Stack();
            stack.AddContainer(new Container(10, ContainerType.Normal));

            // Act
            bool containsValuable = stack.ContainsContainerType(ContainerType.Valuable);

            // Assert
            Assert.IsFalse(containsValuable);
        }

        [TestMethod]
        public void GetTotalWeight_AddMultipleContainers_ShouldReturnsCorrectWeight()
        {
            // Arrange
            var stack = new Stack();
            stack.AddContainer(new Container(5, ContainerType.Normal));
            stack.AddContainer(new Container(7, ContainerType.Normal));

            // Act
            int currentWeight = stack.GetTotalWeight();

            // Assert
            Assert.AreEqual(20000, currentWeight);
        }

        [TestMethod]
        public void StackHeight_AddMultipleContainers_ReturnsCorrectNumberOfContainers()
        {
            // Arrange
            var stack = new Stack();
            stack.AddContainer(new Container(5, ContainerType.Normal));
            stack.AddContainer(new Container(7, ContainerType.Normal));

            // Act
            int stackLength = stack.Height;

            // Assert
            Assert.AreEqual(2, stackLength);
        }

        [TestMethod]
        public void ContainsContainer_GiveAllSortContainer_ShouldReturn3ContainersInstack()
        {
            //arrange
            var stack = new Stack();
            var NormalContainer = new Container(5, ContainerType.Normal);
            var ValuableContainer = new Container(5, ContainerType.Valuable);
            var ValuableCooledContainer = new Container(5, ContainerType.ValuableCooled);
            var CooledContainer = new Container(5, ContainerType.Coolable);
            stack.AddContainer(NormalContainer);
            stack.AddContainer(ValuableContainer);
            stack.AddContainer(ValuableCooledContainer);

            
            //act
            bool StackContainsNormalContainer = stack.ContainsContainer(NormalContainer);
            bool StackContainsValuableContainer = stack.ContainsContainer(ValuableContainer);
            bool StackContainsValuableCooledContainer = stack.ContainsContainer(ValuableCooledContainer);
            bool StackContainscooledContainer = stack.ContainsContainer(CooledContainer);
            
            //assert
            Assert.IsTrue(StackContainsNormalContainer);
            Assert.IsTrue(StackContainsValuableContainer);
            Assert.IsTrue(StackContainsValuableCooledContainer);
            Assert.IsFalse(StackContainscooledContainer);
        }

        [TestMethod]
        public void AddContainer_AddInvalidContainer_ShouldReturnFalse()
        {
            //arrange
            Stack stack = new Stack();
            stack.AddContainer(new Container(25, ContainerType.Normal));
            stack.AddContainer(new Container(25, ContainerType.Normal));
            stack.AddContainer(new Container(25, ContainerType.Normal));
            stack.AddContainer(new Container(25, ContainerType.Normal));
            stack.AddContainer(new Container(25, ContainerType.Normal));
            
            //act
            bool IsContainerAdded = stack.AddContainer(new Container(26, ContainerType.Normal));
            
            //assert
            Assert.IsFalse(IsContainerAdded);
        }
        
    }

}

