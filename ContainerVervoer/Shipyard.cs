using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;

namespace ContainerVervoer
{
    public class Shipyard
    {
        public List<Row> LoadShip(Ship ship, List<Container> containers)
        {
            List<Row> rows = CreateRowsAndStacks(ship);
            List<Container> placedContainers = new List<Container>();
            int TotalContainerWeight = 0;
            foreach(var container in containers) 
            { 
                TotalContainerWeight += container.Weight;
            }
            if ((TotalContainerWeight < ship.MaxWeight) && (TotalContainerWeight > (ship.MaxWeight / 2)))
            {

                var valuableCooledContainers = containers.Where(c => c.ContainerType == ContainerType.ValuableCooled).OrderByDescending(c => c.Weight).ToList();
                var coolableContainers = containers.Where(c => c.ContainerType == ContainerType.Coolable).ToList();
                var valuableContainers = containers.Where(c => c.ContainerType == ContainerType.Valuable).OrderByDescending(c => c.Weight).ToList();
                var normalContainers = containers.Where(c => c.ContainerType == ContainerType.Normal).OrderByDescending(c => c.Weight).ToList();

                int[] order = CalculateBestOrder(ship);
                PlaceValuableCooledContainersOnRow1(rows, valuableCooledContainers, order, ship, placedContainers);
                PlaceCoolableContainersOnRow1(rows, coolableContainers, placedContainers);
                PlaceValuableContainers(rows, valuableContainers, placedContainers);
                PlaceNormalContainers(rows, normalContainers, placedContainers);

                return rows;
            }
            else
            {
                Console.WriteLine("Containers do not meet weight requirements.");
                return rows;
            }

        }

        public int[] CalculateBestOrder(Ship ship)
        {
            int[] order = new int[ship.Width];
            for (int i = 0; i < ship.Width; i++)
            {
                order[i] = (i % 2 == 0) ? i / 2 : ship.Width - (i / 2) - 1;
            }
            return order;
        }

        public List<Row> CreateRowsAndStacks(Ship ship)
        {
            List<Row> rows = new List<Row>();
            for (int i = 0; i < ship.Length; i++)
            {
                rows.Add(new Row());
            }

            foreach (var row in rows)
            {
                for (int i = 0; i < ship.Width; i++)
                {
                    row.Stacks.Add(new Stack());
                }
            }
            return rows;
        }

        private void PlaceValuableCooledContainersOnRow1(List<Row> rows, List<Container> valuableCooledContainers, int[] order, Ship ship, List<Container> placedContainers)
        {
            foreach (var container in valuableCooledContainers)
            {
                bool placed = false;
                int valuableCooledCount = rows[0].Stacks.SelectMany(s => s.Containers).Count(c => c.ContainerType == ContainerType.ValuableCooled);
                if (valuableCooledCount < ship.Width)
                {
                    var stack = rows[0].Stacks[order[valuableCooledCount]];
                    if (stack.CanAddContainer(container) && !stack.ContainsValuableCooledContainer())
                    {
                        stack.AddContainer(container);
                        placedContainers.Add(container);
                        placed = true;
                    }
                }
                if (!placed)
                {
                    Console.WriteLine($"Waarschuwing: Container van type {container.ContainerType} ({container.Weight} kg) kon niet worden geplaatst op rij 1. Maximaal toegestane aantal containers van dit type bereikt of al een waardevolle gekoelde container in de stapel.");
                }
            }
        }

        private void PlaceCoolableContainersOnRow1(List<Row> rows, List<Container> coolableContainers, List<Container> placedContainers)
        {
            foreach (var container in coolableContainers)
            {
                bool placed = false;
                var minWeightStack = rows[0].Stacks.OrderBy(s => s.GetCurrentWeight()).First();
                if (minWeightStack.CanAddContainer(container))
                {
                    minWeightStack.AddContainer(container);
                    placedContainers.Add(container);
                    placed = true;
                }

                if (!placed)
                {
                    Console.WriteLine($"Waarschuwing: Container van type {container.ContainerType} ({container.Weight} kg) kon niet worden geplaatst op rij 1.");
                }
            }
        }

        private void PlaceValuableContainers(List<Row> rows, List<Container> valuableContainers, List<Container> placedContainers)
        {
            foreach (var container in valuableContainers)
            {
                bool placed = false;

                // Filter de rijen waarvan het indexnummer modulo 3 resulteert in 1, 2, 4, 5, 7, 8, 10, 11, enzovoort
                var filteredRows = rows.Where((row, index) => (index + 1) % 3 != 0);

                foreach (var stack in filteredRows.SelectMany(r => r.Stacks).OrderBy(s => s.GetCurrentWeight()))
                {
                    if (!stack.ContainsValuableCooledContainer() && !stack.ContainsValuableContainer() && stack.CanAddContainer(container))
                    {
                        stack.AddValueableContainer(container);
                        placedContainers.Add(container);
                        placed = true;
                        break;
                    }
                }

                if (!placed)
                {
                    Console.WriteLine($"Container {container.ContainerType} met gewicht {container.Weight} kon niet geplaatst worden.");
                }
            }
        }




        private void PlaceNormalContainers(List<Row> rows, List<Container> normalContainers, List<Container> placedContainers)
        {
            foreach (var container in normalContainers)
            {
                bool placed = false;
                foreach (var row in rows)
                {
                    foreach (var stack in row.Stacks.OrderBy(s => s.GetCurrentWeight()))
                    {
                        int currentRowIndex = rows.IndexOf(row);
                        int stackIndex = row.Stacks.IndexOf(stack);

                        // Check the height of the stack in the previous and next rows, if they exist
                        int previousRowStackHeight = -1;
                        int nextRowStackHeight = -1;

                        if (currentRowIndex > 0)
                        {
                            var previousRow = rows[currentRowIndex - 1];
                            previousRowStackHeight = stackIndex < previousRow.Stacks.Count ? previousRow.Stacks[stackIndex].Containers.Count : -1;
                        }

                        if (currentRowIndex < rows.Count - 1)
                        {
                            var nextRow = rows[currentRowIndex + 1];
                            nextRowStackHeight = stackIndex < nextRow.Stacks.Count ? nextRow.Stacks[stackIndex].Containers.Count : -1;
                        }

                        if (stack.CanAddContainer(container))
                        {
                            bool previousStackEqual = previousRowStackHeight == stack.Containers.Count;
                            bool nextStackEqual = nextRowStackHeight == stack.Containers.Count;

                            if (previousStackEqual)
                            {
                                var previousRow = rows[currentRowIndex - 1];
                                var previousStack = previousRow.Stacks[stackIndex];
                                if (previousStack.CanAddContainer(container))
                                {
                                    previousStack.AddContainer(container);
                                    placedContainers.Add(container);
                                    placed = true;
                                    Console.WriteLine($"Placed container in stack {stackIndex} of previous row {currentRowIndex - 1}");
                                    break;
                                }
                            }
                            else if (nextStackEqual)
                            {
                                var nextRow = rows[currentRowIndex + 1];
                                var nextStack = nextRow.Stacks[stackIndex];
                                if (nextStack.CanAddContainer(container))
                                {
                                    nextStack.AddContainer(container);
                                    placedContainers.Add(container);
                                    placed = true;
                                    Console.WriteLine($"Placed container in stack {stackIndex} of next row {currentRowIndex + 1}");
                                    break;
                                }
                            }
                            else
                            {
                                stack.AddContainer(container);
                                placedContainers.Add(container);
                                placed = true;
                                Console.WriteLine($"Placed container in stack {stackIndex} of row {currentRowIndex}");
                                break;
                            }
                        }
                    }

                    if (placed)
                    {
                        break;
                    }
                }

                if (!placed)
                {
                    Console.WriteLine($"Warning: Container of type {container.ContainerType} ({container.Weight} kg) could not be placed.");
                }
            }

            // After initial placement, try to balance the stacks by moving containers if possible
            BalanceStacks(rows);
        }

        private void BalanceStacks(List<Row> rows)
        {
            var allStacks = rows.SelectMany(r => r.Stacks).OrderBy(s => s.Containers.Count).ToList();

            while (true)
            {
                var minStack = allStacks.First();
                var maxStack = allStacks.Last();

                // If the difference in height is 1 or less, stop balancing
                if (maxStack.Containers.Count - minStack.Containers.Count <= 1)
                {
                    break;
                }

                // Move a container from the maxStack to the minStack
                var containerToMove = maxStack.Containers.Last();
                maxStack.Containers.Remove(containerToMove);
                minStack.AddContainer(containerToMove);

                // Reorder the stacks after moving the container
                allStacks = allStacks.OrderBy(s => s.Containers.Count).ToList();
            }
        }



    }
}