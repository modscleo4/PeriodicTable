namespace PeriodicTable.Model.Entity
{
    public class Element
    {
        public decimal? AtomicMass { get; set; }
        public string Symbol { get; set; }
        public int? AtomicNumber { get; set; }
        public string Name { get; set; }
        public int? AtomicRadius { get; set; }
        public decimal? MeltingPoint { get; set; }
        public decimal? BoilingPoint { get; set; }
        public decimal? Density { get; set; }
        public int? ElectronAffinity { get; set; }
        public decimal? ElectronNegativity { get; set; }
        public string ElectronicConfiguration { get; set; }
        public GroupBlock GroupBlock { get; set; }
        public string IonRadius { get; set; }
        public decimal? IonizationEnergy { get; set; }
        public string OxidationStates { get; set; }
        public StandardState StandardStates { get; set; }
        public decimal? VanDerWallsRadius { get; set; }
        public int? YearDiscovered { get; set; }
    }
}
