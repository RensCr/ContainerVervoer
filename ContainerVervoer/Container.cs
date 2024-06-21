using System;
using System.Collections.Generic;
using System.Linq;

namespace ContainerVervoer
{
    public class Container
    {
        public int Weight { get; set; } // Gewicht in kg
        public ContainerType ContainerType { get; set; }

        private const int WeightEmptyContainer = 4000;
        private const int MaxWeightContainer = 30000;

        public Container(int weight, ContainerType containerType)
        {
            if ((weight*1000) + WeightEmptyContainer <= MaxWeightContainer)
            {
                this.Weight = (weight*1000) + WeightEmptyContainer;
            }
            this.ContainerType = containerType;
        }

        public bool CanPlaceInStack(Stack stack, List<Container> placedContainers)
        {
            if (stack.CanAddContainer(this))
            {
                stack.AddContainer(this);
                placedContainers.Add(this);
                
                return true;
            }
            return false;
        }


        public void PlaceValuableInStack(Stack stack, List<Container> placedContainers)
        {
            if (stack.CanAddContainer(this))
            {
                stack.AddValueableContainer(this);
                placedContainers.Add(this);
            }
        }
    }
}
