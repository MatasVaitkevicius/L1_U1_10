namespace L1_U1_10
{
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

        public override string ToString()
        {
            return $"Gamintojas:{Manufacturer}| Modelis:{Model}| Talpa:{Capacity}| Energijos klasė:{EnergyLabel}| Montavimo tipas:{InstallationType}|"
        +
            $"Spalva:{Color}| Požymis:{(IsThereAFreezer ? "Yra" : "Nera")}| Kaina:{Price}| Aukštis:{Height}| Plotis:{Width}| Gylis:{Depth}| ";
        }

        public string PrintShortDescription()
        {
            return $"Gamintojas: {Manufacturer}| Modelis:{Model}| Talpa:{Capacity}| Kaina:{Price}|";
        }
    }
}
