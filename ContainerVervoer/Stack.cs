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

        public int GetCurrentStackWeight()
        {
            return Containers.Sum(c => c.Weight);
        }

        public bool CanAddContainer(Container container)
        {
            return GetCurrentStackWeight() + container.Weight <= MaxWeightPerStack && CanContainerHoldRestOfStack(container);
        }
        private bool CanContainerHoldRestOfStack(Container container)
        {
            return GetCurrentStackWeight() + container.Weight <= MaxWeightOnlowestContainer;
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

        public bool ContainsContainer(Container container)
        {
            return Containers.Contains(container);
        }
        public bool HasValuableContainer()
        {
            foreach (var container in Containers)
            {
                if (container.ContainerType == ContainerType.Valuable)
                {
                    return true;
                }
            }
            return false;

        }
    }
}
