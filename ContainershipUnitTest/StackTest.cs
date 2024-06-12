using ContainerVervoer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainershipUnitTest
{
    [TestClass]
    public class StackTest()
    {
        [TestMethod]
        public void Constructor_ShouldInitializeContainersAsEmptyList()
        {
            //act
            Stack stack = new Stack();
            
            //assert
            Assert.IsNotNull(stack.Containers);
            Assert.AreEqual(0, stack.Containers.Count);
        }

        [TestMethod]
        public void GetCurrentWeight_ShouldReturnSumOfContainerWeights()
        {
            //act
            Stack stack = new Stack();
            int ExpectedWeight = 15000;

            //arrange
            stack.Containers.Add(new Container(1000,ContainerType.Normal));
            stack.Containers.Add(new Container(6000,ContainerType.Normal));
            int currentWeight = stack.GetCurrentWeight();

            //assert
            Assert.AreEqual(currentWeight, ExpectedWeight);
            Assert.AreEqual(2,stack.Containers.Count);
        }
    }
    
}
