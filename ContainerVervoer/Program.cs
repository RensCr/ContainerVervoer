
﻿// See https://aka.ms/new-console-template for more information
﻿using ContainerVervoer;
int lenght = 4;
int widht = 2;
Ship ship = new Ship(lenght, widht); 

List<Container> containers = new List<Container>
        {
    //new Container(40000, ContainerType.Valuable),
    //    new Container(40001, ContainerType.Valuable),
    //        new Container(40002, ContainerType.Valuable),
    //                    new Container(40003, ContainerType.Valuable),
    //                                new Container(40004, ContainerType.Valuable),
    //                                new Container(40000,ContainerType.Normal),
    //                                new Container(40001,ContainerType.Normal),
    //                                                                    new Container(40002,ContainerType.Normal),
    //                                                                                                        new Container(40000,ContainerType.Normal),
    //                                new Container(40001,ContainerType.Normal),
    //                                                                    new Container(40002,ContainerType.Normal),
    //                                                                                                        new Container(40000,ContainerType.Normal),
    //                                new Container(40001,ContainerType.Normal),
    //                                                                    new Container(40002,ContainerType.Normal),
    //                                                                                                   new Container(40000,ContainerType.Normal),
    //                                new Container(40001,ContainerType.Normal),
    //                                                                    new Container(40002,ContainerType.Normal),
    //new Container(14000, ContainerType.ValuableCooled), // Valuable Cooled
    //new Container(14600, ContainerType.Coolable),       // Coolable
    //new Container(14600, ContainerType.Valuable),       // Valuable
    //new Container(5600, ContainerType.Normal),          // Normal
    //new Container(16000, ContainerType.ValuableCooled), // Valuable Cooled
    //new Container(16000, ContainerType.Coolable),       // Coolable
    //new Container(1600, ContainerType.Valuable),       // Valuable
    //new Container(5000, ContainerType.Normal),          // Normal
    //new Container(100, ContainerType.ValuableCooled), // Valuable Cooled
    //new Container(16000, ContainerType.Coolable),       // Coolable
    //new Container(1600, ContainerType.Valuable),       // Valuable
    //new Container(5000, ContainerType.Normal),          // Normal
    //new Container(16000, ContainerType.ValuableCooled), // Valuable Cooled
    //new Container(200, ContainerType.Coolable),       // Coolable
    //new Container(1600, ContainerType.Valuable),       // Valuable
    //new Container(5000, ContainerType.Normal),          // Normal
    //new Container(10000, ContainerType.ValuableCooled),  // Valuable Cooled
    //new Container(1000, ContainerType.Coolable),        // Coolable
    //new Container(1000, ContainerType.Valuable),        // Valuable
    //new Container(1000, ContainerType.Normal),          // Normal
    //new Container(1000, ContainerType.ValuableCooled),  // Valuable Cooled
    //new Container(1200, ContainerType.Coolable),        // Coolable
    //new Container(1400, ContainerType.Valuable),        // Valuable
    //new Container(1600, ContainerType.Normal),          // Normal
    //new Container(1000, ContainerType.ValuableCooled),  // Valuable Cooled
    //new Container(1200, ContainerType.Coolable),        // Coolable
    //new Container(1400, ContainerType.Valuable),        // Valuable
    //new Container(160, ContainerType.Normal),
    //    new Container(1600, ContainerType.Normal),
    //        new Container(1600, ContainerType.Normal),
    //            new Container(16000, ContainerType.Normal),
    //                new Container(16000, ContainerType.Normal) ,
    //                                new Container(16000, ContainerType.Normal),
    //                new Container(16000, ContainerType.Normal) ,
    //            new Container(16000, ContainerType.Normal),
    //                new Container(16, ContainerType.Normal) ,
new Container(2000, ContainerType.Normal),
new Container(2100, ContainerType.Normal),
new Container(2200, ContainerType.Normal),
new Container(2300, ContainerType.Normal),
new Container(2400, ContainerType.Normal),
new Container(2500, ContainerType.Normal),
new Container(2600, ContainerType.Normal),
new Container(2700, ContainerType.Normal),
new Container(2800, ContainerType.Normal),
new Container(2900, ContainerType.Normal),
new Container(3000, ContainerType.Normal),
new Container(3100, ContainerType.Normal),
new Container(3200, ContainerType.Normal),
new Container(3300, ContainerType.Normal),
new Container(3400, ContainerType.Normal),
new Container(3500, ContainerType.Normal),
new Container(3600, ContainerType.Normal),
new Container(3700, ContainerType.Normal),
new Container(3800, ContainerType.Normal),
new Container(3900, ContainerType.Normal),
new Container(4000, ContainerType.Normal),
new Container(4100, ContainerType.Normal),
new Container(4200, ContainerType.Normal),
new Container(4300, ContainerType.Normal),
new Container(4400, ContainerType.Normal),
new Container(4500, ContainerType.Normal),
new Container(4600, ContainerType.Normal),
new Container(4700, ContainerType.Normal),
new Container(4800, ContainerType.Normal),
new Container(4900, ContainerType.Normal),
new Container(5000, ContainerType.ValuableCooled), // Valuable Cooled
new Container(5100, ContainerType.ValuableCooled), // Valuable Cooled
new Container(5200, ContainerType.Coolable),       // Coolable
new Container(5300, ContainerType.Valuable),       // Valuable
new Container(5400, ContainerType.ValuableCooled), // Valuable Cooled
new Container(5500, ContainerType.ValuableCooled), // Valuable Cooled
new Container(5600, ContainerType.Coolable),       // Coolable
new Container(5700, ContainerType.Valuable),       // Valuable
new Container(5800, ContainerType.ValuableCooled), // Valuable Cooled
new Container(5900, ContainerType.ValuableCooled),  // Valuable Cooled
new Container(6000, ContainerType.Normal),
new Container(6100, ContainerType.Normal),
new Container(6200, ContainerType.Normal),
new Container(6300, ContainerType.Normal),
new Container(6400, ContainerType.Normal),
new Container(6500, ContainerType.Normal),
new Container(6600, ContainerType.Normal),
new Container(6700, ContainerType.Normal),
new Container(6800, ContainerType.Normal),
new Container(6900, ContainerType.Normal),
new Container(7000, ContainerType.Normal),
new Container(7100, ContainerType.Normal),
new Container(7200, ContainerType.Normal),
new Container(7300, ContainerType.Normal),
new Container(7400, ContainerType.Normal),
new Container(7500, ContainerType.Normal),
new Container(7600, ContainerType.Normal),
new Container(7700, ContainerType.Normal),
new Container(7800, ContainerType.Normal),
new Container(7900, ContainerType.Normal),
new Container(8000, ContainerType.ValuableCooled), // Valuable Cooled
new Container(8100, ContainerType.ValuableCooled), // Valuable Cooled
new Container(8200, ContainerType.Coolable),       // Coolable
new Container(8300, ContainerType.Valuable),       // Valuable
new Container(8400, ContainerType.ValuableCooled), // Valuable Cooled
new Container(8500, ContainerType.ValuableCooled), // Valuable Cooled
new Container(8600, ContainerType.Coolable),       // Coolable
new Container(8700, ContainerType.Valuable),       // Valuable
new Container(8800, ContainerType.ValuableCooled), // Valuable Cooled
new Container(8900, ContainerType.ValuableCooled), // Valuable Cooled





                    // Normal          // Normal
//            new Container(10000, ContainerType.ValuableCooled),
//            new Container(10001, ContainerType.ValuableCooled),


//            new Container(10002, ContainerType.Coolable),
//            new Container(10003, ContainerType.Valuable),
//            new Container(10004, ContainerType.Normal),

//            new Container(10005, ContainerType.Normal),
//            new Container(10006, ContainerType.Normal),
//                        new Container(10000, ContainerType.ValuableCooled),
//            new Container(10001, ContainerType.ValuableCooled),


//            new Container(10002, ContainerType.Coolable),
//            new Container(10003, ContainerType.Valuable),
//            new Container(10004, ContainerType.Normal),

//            new Container(10005, ContainerType.Normal),
//            new Container(10006, ContainerType.Normal),
//            new Container(10007, ContainerType.ValuableCooled),
//new Container(10008, ContainerType.ValuableCooled),
//new Container(10009, ContainerType.Coolable),
//new Container(10010, ContainerType.Valuable),
//new Container(10011, ContainerType.Normal),

//new Container(10012, ContainerType.Normal),
//new Container(10013, ContainerType.Normal),
//new Container(10014, ContainerType.ValuableCooled),
//new Container(10015, ContainerType.ValuableCooled),
//new Container(10016, ContainerType.Coolable),

//new Container(10017, ContainerType.Valuable),
//new Container(10018, ContainerType.Normal),
//new Container(10019, ContainerType.Normal),
//new Container(10020, ContainerType.Normal),
//new Container(10021, ContainerType.ValuableCooled),

//new Container(10022, ContainerType.ValuableCooled),
//new Container(10023, ContainerType.Coolable),
//new Container(10024, ContainerType.Valuable),
//new Container(10025, ContainerType.Normal),
//new Container(10026, ContainerType.Normal),




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