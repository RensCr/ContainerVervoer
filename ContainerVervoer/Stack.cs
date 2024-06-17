using System;
using System.Collections.Generic;
using System.Linq;

namespace ContainerVervoer
{
    public class Stack
    {
        public List<Container> Containers { get; private set; }
        public const int MaxWeightPerStack = 150000; // 150 ton

        public Stack()
        {
            Containers = new List<Container>();
        }

        public int GetCurrentWeight()
        {
            return Containers.Sum(c => c.Weight);
        }

        public bool CanAddContainer(Container container)
        {
            return GetCurrentWeight() + container.Weight <= MaxWeightPerStack;
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

        public bool ContainsValuableCooledContainer()
        {
            return Containers.Any(c => c.ContainerType == ContainerType.ValuableCooled);
        }

        public bool ContainsValuableContainer()
        {
            return Containers.Any(c => c.ContainerType == ContainerType.Valuable);
        }

        public int GetStackLength()
        {
            return Containers.Count; // Bijvoorbeeld, retourneer het aantal containers in de stack
        }
        public bool ContainsContainer(Container container)
        {
            return Containers.Contains(container);
        }
    }
}
