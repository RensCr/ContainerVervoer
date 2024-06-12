using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerVervoer
{
    public class Stack
    {
        public List<Container> Containers { get; private set; }
        public const int MaxWeightPerStack = 150000;

        public Stack()
        {
            Containers = new List<Container>();
        }

        public int GetCurrentWeight()
        {
            return Containers.Sum(c => c.Weight);
        }
    }
}


