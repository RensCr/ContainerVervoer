namespace ContainerVervoer
{
    public class Container
    {
        public int Weight { get; set; } // Gewicht in kg
        public ContainerType ContainerType { get; set; }
        private const int WeightEmptyContainer = 4000;

        public Container(int weight, ContainerType containerType)
        {
            this.Weight = weight + WeightEmptyContainer;
            this.ContainerType = containerType;
        }
    }
}
