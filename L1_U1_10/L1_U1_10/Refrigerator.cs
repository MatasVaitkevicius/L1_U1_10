namespace L1_U1_10
{
    /// <summary>
    /// Klasė skirta aprašyti duomenys apie šaldytuvus
    /// </summary>
    class Refrigerator
    {
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public int Capacity { get; set; }
        public string EnergyLabel { get; set; }
        public string InstallationType { get; set; }
        public string Color { get; set; }
        public bool IsThereAFreezer { get; set; }
        public int Price { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int Depth { get; set; }

        /// <summary>
        /// Konvertuoja šaldytuvų objektą į suformatuotą simbolių eilutę
        /// </summary>
        /// <returns> Suformatuotą simbolių eilutę</returns>
        public override string ToString()
        {
            return $" {Manufacturer},{Model},{Capacity},{EnergyLabel},{InstallationType},{Color},{IsThereAFreezer},{Price},{Height},{Width},{Depth}";
        }

        /// <summary>
        /// Trumpas aprašymas
        /// </summary>
        /// <returns>Trumpą parašymą</returns>
        public string PrintShortDescription()
        {
            return $"Gamintojas: {Manufacturer} | Modelis:{Model} | Talpa:{Capacity} | Kaina:{Price}|";
        }
    }
}
