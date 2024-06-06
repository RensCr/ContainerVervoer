// See https://aka.ms/new-console-template for more information
using ContainerVervoer;

Ship ship = new Ship(3, 3); 
List<Container> containers = new List<Container>
        {

            new Container(10000, ContainerType.ValuableCooled),
            new Container(30000, ContainerType.ValuableCooled),
                        new Container(20000, ContainerType.Normal),


        };

 List<Row> rows = Shipyard.LoadShip(ship, containers);
int x = 1;
foreach (var row in rows)
{
    Console.WriteLine($"\n Containers in deel {x}:");

    PrintRow(row);
    x += 1;
}

    

 void PrintRow(Row row)
{
    for (int i = 0; i < row.Stacks.Count; i++)
    {
        Console.WriteLine($"Stack {i + 1}:");
        foreach (var container in row.Stacks[i].Containers)
        {
            Console.WriteLine($"- Container Type: {container.ContainerType}, Gewicht: {container.Weight} kg");
        }
    }
}