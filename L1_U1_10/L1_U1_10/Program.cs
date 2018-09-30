using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace L1_U1_10
{
    /// <summary>
    /// Programa pritaikyta darbui su informacija apie šaldytuvus
    /// </summary>
    class Program
    {
        private const string ChosenManufacturer = "Siemens"; // Pasirenkamas gamintojas su kuriuo skaičiuojami šaldytuvai

        private const int CapacityThreshold = 200; //Talpos kiekis pagal kurį reikia palyginti šaldytuvus

        private const string ChosenInstallationType = "Įmontuojamas"; // Pasirenkamas montavimo tipas pagal kurį daromi skaičiavimai

        static void Main(string[] args)
        {
            var p = new Program();
            var refrigerators = p.ReadRefrigeratorData();

            p.CreateAReportTable(refrigerators);
            p.SaveReportToFile(refrigerators);

            Console.WriteLine($"1.Skirtingu {ChosenManufacturer} šaldytuvų skaičius: {p.CountManufacturerRefrigerators(refrigerators)}");
            Console.ReadKey();
            Console.WriteLine();

            var Check = refrigerators.Count(r => r.Capacity.Equals(CapacityThreshold)); // Patikrinimas ar yra tokios talpos

            if (Check == 0)
            {                
                Console.WriteLine($"Tokios {CapacityThreshold} talpos nera");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine($"2.Pigiausias {ChosenInstallationType} šaldytuvas, kurio talpa didesnė už {CapacityThreshold} litrų");
                p.FindCheapestRefrigerator(refrigerators).ForEach(r => Console.WriteLine(r.PrintShortDescription()));
                Console.ReadKey();
                Console.WriteLine();
            }

            Console.WriteLine($"3.Sudarytas šaldytuvų sąrašas pagal nustatytą montavimo tipą {ChosenInstallationType} ir pagal talpa" +
                $" {CapacityThreshold} litrų ar didesnė, kuris išsaugotas į failą „Pastatomi80.csv“ ir jame įrašyti visi" +
                $" duomenys apie šiuos šaldytuvus");
            p.WriteDataAboutRightInstallationType(refrigerators);
            Console.ReadKey();
            Console.WriteLine();

            Console.WriteLine("4.Sudarytas visų šaldytuvų gamintojų sąrašas, gamintojų pavadinimai įrašyti į failą „Gamintojai.csv“");
            p.WriteDataAboutAllManufacturers(refrigerators);
            Console.ReadKey();
        }

        /// <summary>
        /// Skaito duomenys iš CSV tipo failo
        /// </summary>
        /// <returns>Sąrašą šaldytuvų</returns>
        private List<Refrigerator> ReadRefrigeratorData()
        {
            var refrigerators = new List<Refrigerator>();

            using (var reader = new StreamReader(@"L1Data.csv"))
            {
                var line = reader.ReadLine();
                while (line != null)
                {
                    var values = line.Split(',');
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

                    //Konstruktorius
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

        /// <summary>
        /// Sukuria ataskaitos lentelę ir išsaugoja į .txt failą
        /// </summary>
        /// <param name="refrigerators"> Išsaugotų šaldytuvų sąrašas </param>
        void CreateAReportTable(List<Refrigerator> refrigerators)
        {
            using (var writer = new StreamWriter(@"L1ReportTable.txt"))
            {
                writer.WriteLine("Šaldytuvų sąrašas");
                writer.WriteLine(new string('-', 190));                
                writer.WriteLine(
                    "| {0, -15} | {1, -15} | {2, -11} | {3, -10} | {4, -6} | {5, -10} | {6, -13} | {7, -15} | {8, -16} | {9, -17}" +
                    "| {10, -16} |", "Gamintojas", "Modelis", "Talpa", "Energijos klasė", "Montavimo tipas", "Spalva",
                    "Šaldiklis",
                    "Kaina", "Aukštis", "Plotis", "Gylis");
                writer.WriteLine(new string('-', 190));

                foreach (var refrigerator in refrigerators)
                {
                    writer.WriteLine(
                        "| {0, -15} | {1, -15} | {2, 11} | {3, -15} | {4, -15} | {5, -10} | {6, -13} | {7, 15} | {8, 16} | {9, 17}" +
                        "| {10, 16} |", refrigerator.Manufacturer, refrigerator.Model, refrigerator.Capacity,
                        refrigerator.EnergyLabel,
                        refrigerator.InstallationType, refrigerator.Color, refrigerator.IsThereAFreezer,
                        refrigerator.Price, refrigerator.Height,
                        refrigerator.Width, refrigerator.Depth);
                    writer.WriteLine(new string('-', 190));
                }
            }
        }

        /// <summary>
        /// Išsaugoja šaldytuvų duomenys į CSV tipo failą
        /// </summary>
        /// <param name="refrigerators"> Išsaugotų šaldytuvų sąrašas </param>
        void SaveReportToFile(List<Refrigerator> refrigerators)
        {
            using (var writer = new StreamWriter(@"L1SavedData.csv", false, Encoding.UTF8))
            {
                writer.WriteLine(
                    "Gamintojas,Modelis,Talpa,Energijos klasė,Montavimo tipas,Spalva,Šaldiklis,Kaina,Aukštis,Plotis,Gylis");
                refrigerators.ForEach(r => writer.WriteLine(r.ToString()));
            }
        }

        /// <summary>
        /// Įrašo šaldytuvus pagal nustatytą montavimo tipą ir talpą į CSV tipo failą
        /// </summary>
        /// <param name="refrigerators"> Šaldytuvų pagal nustatytą montavimo tipą ir talpą sąrašas </param>
        private void WriteDataAboutRightInstallationType(List<Refrigerator> refrigerators)
        {
            using (var writer = new StreamWriter(@"Pastatomi80.csv", false, Encoding.UTF8))
            {
                writer.WriteLine(
                    "Gamintojas,Modelis,Talpa,Energijos klasė,Montavimo tipas,Spala,Šaldiklis,Kaina,Aukštis,Plotis,Gylis");
                FindRightInstallationType(refrigerators).ForEach(r => writer.WriteLine(r.ToString()));
            }
        }

        /// <summary>
        /// Įrašo visų skirtingų gamintojų duomenis į failą
        /// </summary>
        /// <param name="refrigerators"> Šaldytuvų sąrašas </param>
        private void WriteDataAboutAllManufacturers(List<Refrigerator> refrigerators)
        {
            using (var writer = new StreamWriter(@"Gamintojai.csv", false, Encoding.UTF8))
            {
                writer.WriteLine("Gamintojas");
                FindAllRefrigeratorManufacturers(refrigerators).ForEach(m => writer.WriteLine(m));
            }
        }

        /// <summary>
        /// Skaičiuoja kiek skirtingų gamintojo šaldytuvų egzistuoja
        /// </summary>
        /// <param name="refrigerators"> Šaldytuvų sąrašas </param>
        /// <returns>Kiekį nurodyto gamintojo šaldytuvų</returns>
        private int CountManufacturerRefrigerators(List<Refrigerator> refrigerators)
        {
            return refrigerators.Count(r => r.Manufacturer.Equals(ChosenManufacturer));
        }

        /// <summary>
        /// Filtruoja pagal nustatytą talpą, daugiau arba lygu
        /// </summary>
        /// <param name="refrigerators"> Šaldytuvų sąrašas </param>
        /// <returns>Šąrašą išfiltruotas pagal talpą</returns>
        private List<Refrigerator> FilterByCapacity(List<Refrigerator> refrigerators)
        {
            return refrigerators.FindAll(r => r.Capacity >= CapacityThreshold);           
        }

        /// <summary>
        /// Suranda pigiausią šaldytuvą, pagal nustatyta talpą
        /// </summary>
        /// <param name="refrigerators"> Šaldytuvų sąrašas </param>
        /// <returns>Šąrašą pigiausių šaldytuvų</returns>
        private List<Refrigerator> FindCheapestRefrigerator(List<Refrigerator> refrigerators)
        {
            var minPrice = FilterByCapacity(refrigerators).Min(r => r.Price);
            return FilterByCapacity(refrigerators).FindAll(r => r.Price == minPrice);
        }

        /// <summary>
        /// Suranda šaldytuvus pagal montavimo tipą ir pagal nurodytą talpą 
        /// </summary>
        /// <param name="refrigerators"> Šaldytuvų sąrašas </param>
        /// <returns>Šąrašą šaldytuvų pagal montavimo tipą ir pagal nurodytą talpą </returns>
        private List<Refrigerator> FindRightInstallationType(List<Refrigerator> refrigerators)
        {
            return FilterByCapacity(refrigerators).FindAll(r => r.InstallationType.Equals(ChosenInstallationType));
        }

        /// <summary>
        /// Suranda visų šaldytuvų gamintojus
        /// </summary>
        /// <param name="refrigerators"> Gamintojų sąrašas </param>
        /// <returns>Šąrašą visų gamintojų</returns>
        private List<string> FindAllRefrigeratorManufacturers(List<Refrigerator> refrigerators)
        {
            return refrigerators.Select(r => r.Manufacturer).Distinct().ToList();
        }
    }
}