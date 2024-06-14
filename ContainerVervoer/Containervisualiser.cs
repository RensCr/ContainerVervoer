using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerVervoer
{
    public class Containervisualiser
    {
        public static string GenerateURL(Ship ship, List<Row> rows)
        {
            string baseURL = "https://i872272.luna.fhict.nl/ContainerVisualizer/index.html?";
            string stacks = "stacks=";
            string weights = "weights=";

            string length = "length=" + ship.Length;
            string width = "&width=" + ship.Width;

            int maxStacks = rows.Max(r => r.Stacks.Count);

            for (int stackIndex = 0; stackIndex < maxStacks; stackIndex++)
            {
                for (int rowIndex = 0; rowIndex < rows.Count; rowIndex++)
                {
                    var row = rows[rowIndex];

                    if (stackIndex < row.Stacks.Count)
                    {
                        var stack = row.Stacks[stackIndex];
                        for (int containerIndex = stack.Containers.Count - 1; containerIndex >= 0; containerIndex--)
                        {
                            var container = stack.Containers[containerIndex];

                            if (containerIndex != stack.Containers.Count - 1)
                            {
                                stacks += "-";
                                weights += "-";
                            }

                            if (container.ContainerType == ContainerType.Normal)
                            {
                                stacks += "1";
                            }
                            else if (container.ContainerType == ContainerType.Valuable)
                            {
                                stacks += "2";
                            }
                            else if (container.ContainerType == ContainerType.Coolable)
                            {
                                stacks += "3";
                            }
                            else if (container.ContainerType == ContainerType.ValuableCooled)
                            {
                                stacks += "4";
                            }

                            weights += container.Weight;
                        }
                    }

                    if (rowIndex < rows.Count - 1)
                    {
                        stacks += ",";
                        weights += ",";
                    }
                }

                if (stackIndex < maxStacks - 1)
                {
                    stacks += "/";
                    weights += "/";
                }
            }

            baseURL += length + width + "&" + stacks + "&" + weights;

            return baseURL;
        }
    }
}