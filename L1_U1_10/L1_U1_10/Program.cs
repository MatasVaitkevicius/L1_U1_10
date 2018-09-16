using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace L1_U1_10
{
    class Program
    {
        private const int CapacityThreshold = 80;
         static void Main(string[] args)
        {
            var p = new Program();
            var refrigerators = p.ReadRefrigeratorData();
            Console.WriteLine($"Skirtingu Siemens šaldytuvų skaičius:{p.CountSiemensRefrigerators(refrigerators)}");
            Console.WriteLine("Pigiausias(i) pastatomas šaldytuvas(i), kurio(ų) talpa didesnė už 80 litrų");
            p.FindCheapestRefrigerator(refrigerators).ForEach(r => Console.WriteLine(r.PrintShortDescription()));
            Console.WriteLine();
            p.SaveDataAboutRightInstallationType(refrigerators);
            p.SaveDataAboutAllManufacturers(refrigerators);
        }

        private List<Refrigerator> ReadRefrigeratorData()
        {
            var refrigerators = new List<Refrigerator>();

            using(var reader = new StreamReader(@"L1Data.csv"))
            {
                var line = reader.ReadLine();
                while (line != null)
                {
                    var values = line.Split(';');
                    var manufacturer = values[0];
                    var model = values[1];
                    var capacity = int.Parse(values[2]);
                    var energyLabel = values[3];
                    var installationType = values[4];
                    var color = values[5];
                    var isThereAFreezer = bool.Parse(values[6]);
                    var price = int.Parse(values[7]);
                    var height = int.Parse(values[8]);
                    var width = int.Parse(values[9]);
                    var depth = int.Parse(values[10]);

                    refrigerators.Add(new Refrigerator
                    {
                        Manufacturer = manufacturer,
                        Model = model,
                        Capacity = capacity,
                        EnergyLabel = energyLabel,
                        InstallationType = installationType,
                        Color = color,
                        IsThereAFreezer = isThereAFreezer,
                        Price = price,
                        Height = height,
                        Width = width,
                        Depth = depth
                    });

                    line = reader.ReadLine();
                }
            }
            return refrigerators;
        }

        private  void SaveDataAboutRightInstallationType(List<Refrigerator> refrigerators)
        {
            using (var writer = new StreamWriter(@"Pastatomi80.csv", false, Encoding.UTF8))
            {
               FindRightInstallationType(refrigerators).ForEach(r => writer.WriteLine(r.ToString()));
            }
        }

        private void SaveDataAboutAllManufacturers(List<Refrigerator> refrigerators)
        {
            using (var writer = new StreamWriter(@"Gamintojai.csv", false, Encoding.UTF8)) 
            {
                FindAllRefrigeratorManufacturers(refrigerators).ForEach(m => writer.WriteLine(m));
            }
        }

        private int CountSiemensRefrigerators(List<Refrigerator> refrigerators)
        {
            return refrigerators.Count(r => r.Manufacturer.Equals("Siemens"));
        }

        private List<Refrigerator> FilterByCapacity(List<Refrigerator> refrigerators)
        {
            return refrigerators.FindAll(r => r.Capacity >= CapacityThreshold);
        }

        private List<Refrigerator> FindCheapestRefrigerator(List<Refrigerator> refrigerators)
        {
            var minPrice = FilterByCapacity(refrigerators).Min(r => r.Price);
            return FilterByCapacity(refrigerators).FindAll(r => r.Price == minPrice);
        }

        private  List<Refrigerator> FindRightInstallationType(List<Refrigerator> refrigerators)
        {
            return FilterByCapacity(refrigerators).FindAll(r => r.InstallationType.Equals("Pastatomas"));
        }

        private List<string> FindAllRefrigeratorManufacturers(List<Refrigerator> refrigerators)
        {
            return refrigerators.Select(r => r.Manufacturer).Distinct().ToList();
        }       
    }
}
