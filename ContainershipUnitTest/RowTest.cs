using ContainerVervoer;


namespace ContainershipUnitTest
{
    [TestClass]
    public class RowTest
    {
        [TestMethod]
        public void Constructor_ShouldInitializeStacksAsEmptyList()
        {
            //arrange
            Row row = new Row();

            //assert
            Assert.IsNotNull(row);
            Assert.AreEqual(0,row.Stacks.Count);
        }
    }
}
