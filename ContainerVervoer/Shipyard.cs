using System;
using System.Collections.Generic;
using System.Linq;

namespace ContainerVervoer
{
    public class Shipyard
    {
        public List<Row> LoadShip(Ship ship, List<Container> containers)
        {
            List<Row> rows = ship.Rows;
            List<Container> placedContainers = new List<Container>();
            int TotalContainerWeight = containers.Sum(container => container.Weight);

            if ((TotalContainerWeight < ship.MaxWeight) && (TotalContainerWeight > (ship.MaxWeight / 2)))
            {
                var valuableCooledContainers = containers.Where(c => c.ContainerType == ContainerType.ValuableCooled).OrderByDescending(c => c.Weight).ToList();
                var coolableContainers = containers.Where(c => c.ContainerType == ContainerType.Coolable).ToList();
                var valuableContainers = containers.Where(c => c.ContainerType == ContainerType.Valuable).OrderByDescending(c => c.Weight).ToList();
                var normalContainers = containers.Where(c => c.ContainerType == ContainerType.Normal).OrderByDescending(c => c.Weight).ToList();

                int[] order = CalculateBestOrder(ship.Width);
                PlaceValuableCooledContainersOnRow1(rows, valuableCooledContainers, order, ship, placedContainers);
                PlaceCoolableContainersOnRow1(rows, coolableContainers, placedContainers);
                PlaceValuableContainers(rows, valuableContainers, placedContainers);
                PlaceNormalContainers(rows, normalContainers, placedContainers);

                return rows;
            }
            else
            {
                Console.WriteLine($"Containers do not meet weight requirements. {TotalContainerWeight}.{ship.MaxWeight}");
                return rows;
            }
        }

        private int[] CalculateBestOrder(int width)
        {
            int[] order = new int[width];
            for (int i = 0; i < width; i++)
            {
                order[i] = (i % 2 == 0) ? i / 2 : width - (i / 2) - 1;
            }
            return order;
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
                        container.CanPlaceInStack(stack, placedContainers);
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
                    container.CanPlaceInStack(minWeightStack, placedContainers);
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
                        container.PlaceValuableInStack(stack, placedContainers);
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
                            bool previousStackHasValuable = false;
                            bool nextStackHasValuable = false;

                            if (currentRowIndex > 0)
                            {
                                var previousRow = rows[currentRowIndex - 1];
                                var previousStack = previousRow.Stacks[stackIndex];
                                previousStackHasValuable = previousStack.ContainsValuableContainer();
                            }

                            if (currentRowIndex < rows.Count - 1)
                            {
                                var nextRow = rows[currentRowIndex + 1];
                                var nextStack = nextRow.Stacks[stackIndex];
                                nextStackHasValuable = nextStack.ContainsValuableContainer();
                            }

                            if ((previousStackHasValuable || nextStackHasValuable))
                            {
                                if (previousRowStackHeight >= stack.Containers.Count + 2)
                                {
                                    if (nextRowStackHeight >= stack.Containers.Count + 2)
                                    {
                                        if (previousStackHasValuable && previousRowStackHeight >= stack.Containers.Count + 2 && currentRowIndex > 0 && nextRowStackHeight >= stack.Containers.Count + 2 && currentRowIndex < rows.Count - 1)
                                        {
                                            var currentRow = rows[currentRowIndex];
                                            var currentStack = currentRow.Stacks[stackIndex];
                                            if (currentStack.CanAddContainer(container))
                                            {
                                                container.CanPlaceInStack(currentStack, placedContainers);
                                                placed = true;
                                                break;
                                            }
                                        }
                                        else if (!previousStackHasValuable && previousRowStackHeight >= stack.Containers.Count + 2 && currentRowIndex > 0)
                                        {
                                            var previousRow = rows[currentRowIndex - 1];
                                            var previousStack = previousRow.Stacks[stackIndex];
                                            if (previousStack.CanAddContainer(container))
                                            {
                                                container.CanPlaceInStack(previousStack, placedContainers);
                                                placed = true;
                                                break;
                                            }
                                        }
                                        else if (!nextStackHasValuable && nextRowStackHeight >= stack.Containers.Count + 2 && currentRowIndex < rows.Count - 1)
                                        {
                                            var nextRow = rows[currentRowIndex + 1];
                                            var nextStack = nextRow.Stacks[stackIndex];
                                            if (nextStack.CanAddContainer(container))
                                            {
                                                container.CanPlaceInStack(nextStack, placedContainers);
                                                placed = true;
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (rows.Count > currentRowIndex+1) { 
                                        var nextRow = rows[currentRowIndex + 1];
                                        var nextStack = nextRow.Stacks[stackIndex];
                                        container.CanPlaceInStack(nextStack, placedContainers);
                                        placed = true;

                                        break;
                                        }
                                    }
                                }
                                else
                                {
                                    if (currentRowIndex > 0) { 
                                        var previousRow = rows[currentRowIndex - 1];
                                        var previousStack = previousRow.Stacks[stackIndex];
                                        if (container.CanPlaceInStack(previousStack, placedContainers))
                                        {
                                            placed = true;
                                        }
                                        else
                                        {
                                            var nextRow = rows[currentRowIndex + 1];
                                            var nextStack = nextRow.Stacks[stackIndex];
                                            if (container.CanPlaceInStack(nextStack, placedContainers))
                                            {
                                                placed = true;
                                            }
                                        }
                                    }

                                }

                            }
                        }
                    }

                }
                if (!placed)
                {
                    Console.WriteLine($"Container : {container.ContainerType} met gewicht {container.Weight}. Kon niet geplaatst worden");
                }
            }
        }

    }
}
