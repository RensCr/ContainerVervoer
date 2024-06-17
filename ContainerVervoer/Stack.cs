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

        public void AddContainer(Container container)
        {
            if (CanAddContainer(container))
            {
                Containers.Add(container);
            }
        }
        public void AddValueableContainer(Container container)
        {
            if (CanAddContainer(container))
            {
                Containers.Insert(0,container);
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
        // Voorbeeld van de Stack-klasse

       
            // Andere eigenschappen en methoden van de Stack-klasse

            public int GetStackLength()
            {
                return Containers.Count; // Bijvoorbeeld, retourneer het aantal containers in de stack
            }


        // In de Stack klasse of waar je stack implementatie zich bevindt


    }
}


