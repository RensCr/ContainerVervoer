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
        private const int MaxWeightOnTop = 120000; // 120 ton bovenop

        public Stack()
        {
            Containers = new List<Container>();
        }


    }

}
