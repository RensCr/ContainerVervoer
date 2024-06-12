
using ContainerVervoer;

namespace ContainershipUnitTest
{
    [TestClass]
    public class ContainerTest
    {
        [TestMethod]
        public void Constructor_WithWeightAndContainerType_ShouldGiveTotalWeightAndType()
        {
            //arrange
            int weightInContainer = 3000;
            var ContainerType = ContainerVervoer.ContainerType.Normal;
            
            //act
            Container container = new Container(weightInContainer, ContainerType);

            //assert
            Assert.AreEqual(7000, container.Weight);
            Assert.AreEqual(ContainerVervoer.ContainerType.Normal, container.ContainerType);
        }

        [TestMethod]
        public void Constructor_WithContainerType_ShouldGiveDefaultWeight()
        {
            //arrange
            int WeightInContainer = 0;
            var containerType = ContainerVervoer.ContainerType.Valuable;

            //act
            Container container = new Container(WeightInContainer, containerType);

            //assert
            Assert.AreEqual(4000, container.Weight);
            Assert.AreEqual(ContainerVervoer.ContainerType.Valuable, container.ContainerType);
        }
    }
}