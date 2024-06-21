using ContainerVervoer;
namespace ContainerVervoerUnitTest
{
    [TestClass]
    public class RowTest
    {

        [TestMethod]
        public void GetCurrentTotalWeight_AddMultipleContainersToStack_ShouldReturnCorrectStackWeight()
        {
            // Arrange
            var row = new Row();
            var stack1 = new Stack();
            var stack2 = new Stack();
            stack1.AddContainer(new Container(5, ContainerType.Normal));
            stack1.AddContainer(new Container(7, ContainerType.Normal));
            stack2.AddContainer(new Container(3, ContainerType.Normal));
            row.Stacks.Add(stack1);
            row.Stacks.Add(stack2);

            // Act
            int totalWeight = row.GetTotalWeight();

            // Assert
            Assert.AreEqual(27000, totalWeight);
        }
        [TestMethod]
        public void Constructor_addStacksToRow_ShouldReturnCorrectStackCount()
        {
            // Arrange
            var row = new Row();
            var stack1 = new Stack();
            var stack2 = new Stack();
            row.Stacks.Add(stack1);
            row.Stacks.Add(stack2);

            // Act
            int stackCount = row.Stacks.Count;

            // Assert
            Assert.AreEqual(2, stackCount);
        }

        [TestMethod]
        public void Constructor_addSingleStackToRow_ShouldReturn1EmptyStack()
        {
            // Arrange
            var row = new Row();
            var stack = new Stack();
            row.Stacks.Add(stack);

            // Act
            int stackCount = row.Stacks.Count;

            // Assert
            Assert.AreEqual(1, stackCount);
            Assert.AreEqual(0, stack.Containers.Count);
        }
    }
}

