using System;
using System.Collections.Generic;
using System.Linq;

namespace ContainerVervoer
{
    public class Stack
    {
        public List<Container> Containers { get; private set; }
        public const int MaxWeightPerStack = 150000;
        public const int MaxWeightOnlowestContainer = 120000; 
        public int Height => Containers.Count; 

        public Stack()
        {
            Containers = new List<Container>();
        }

        public int GetTotalWeight()
        {
            return Containers.Sum(c => c.Weight);
        }

        public bool CanAddContainer(Container container)
        {
            return GetTotalWeight() + container.Weight <= MaxWeightPerStack && !IsWeightOnTopTooHeavy(container);
        }
        private bool IsWeightOnTopTooHeavy(Container container)
        {
            return !(GetTotalWeight() + container.Weight <= MaxWeightOnlowestContainer);
        }

        public bool AddContainer(Container container)
        {
            if (CanAddContainer(container))
            {
                Containers.Add(container);
                return true;
            }
            return false;
        }

        public void AddValueableContainer(Container container)
        {
            if (CanAddContainer(container))
            {
                Containers.Insert(0, container);
            }
        }
        public bool ContainsContainerType(ContainerType containerType)
        {
            return Containers.Any(c => c.ContainerType == containerType);
        }

        public bool ContainsContainer(Container container)
        {
            return Containers.Contains(container);
        }
    }
}
