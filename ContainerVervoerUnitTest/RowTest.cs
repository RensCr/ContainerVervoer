using ContainerVervoer;
namespace ContainerVervoerUnitTest
{
    [TestClass]
    public class RowTest
    {

        [TestMethod]
        public void Row_GetCurrentTotalWeight_ShouldReturnsCorrectWeight()
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
            int totalWeight = row.GetCurrentTotalWeight();

            // Assert
            Assert.AreEqual(27000, totalWeight);
        }
    }
}

