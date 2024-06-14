
﻿// See https://aka.ms/new-console-template for more information
﻿using ContainerVervoer;
int lenght = 2;
int widht = 2;
Ship ship = new Ship(lenght, widht); 

List<Container> containers = new List<Container>
        {

            new Container(10000, ContainerType.ValuableCooled),
            //new Container(30000, ContainerType.ValuableCooled),


            new Container(3000, ContainerType.Coolable),
            new Container(1600, ContainerType.Valuable),
            new Container(20000, ContainerType.Normal),

            new Container(20000, ContainerType.Normal),
            new Container(20000, ContainerType.Normal)

        };




Console.WriteLine($"Informatie \n Je schip is {lenght}meter lang en {widht} meter breedt.");
Shipyard shipyard = new Shipyard();
List<Row> rows = shipyard.LoadShip(ship, containers);
string generatedURL = Containervisualiser.GenerateURL(ship,rows);
Console.WriteLine("Generated URL: " + generatedURL);

int x = 1;
foreach (var row in rows)
{
    Console.WriteLine($"\n Containers in rij {x}:");

    PrintRow(row);
    x += 1;
}

    

 void PrintRow(Row row)
{
    for (int i = 0; i < row.Stacks.Count; i++)
    {
        Console.WriteLine($"kolom {i + 1}:");
        foreach (var container in row.Stacks[i].Containers)
        {
            Console.WriteLine($"- Container Type: {container.ContainerType}, Gewicht: {container.Weight} kg");
        }
    }
}