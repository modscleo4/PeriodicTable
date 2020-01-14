using System.Collections.Generic;
using System.Threading.Tasks;
using static PeriodicTable.Model.Database.DB;

namespace PeriodicTable.Model.Entity
{
    public class Element
    {
        public uint AtomicNumber { get; set; }
        public string Symbol { get; set; }
        public decimal AtomicMass { get; set; }
        public string Name { get; set; }
        public int? AtomicRadius { get; set; }
        public decimal? MeltingPoint { get; set; }
        public decimal? BoilingPoint { get; set; }
        public decimal? Density { get; set; }
        public int? ElectronAffinity { get; set; }
        public decimal? Electronegativity { get; set; }
        public string ElectronicConfiguration { get; set; }
        public GroupBlock GroupBlock { get; set; }
        public string IonRadius { get; set; }
        public decimal? IonizationEnergy { get; set; }
        public List<int> OxidationStates { get; set; }
        public StandardState StandardState { get; set; }
        public decimal? VanDerWaalsRadius { get; set; }
        public int? YearDiscovered { get; set; }

        public void Save()
        {
            var sql = "SELECT * " +
                        "FROM element " +
                        "WHERE atomicNumber = @1";
            var dr = con.Select(sql, new List<object> { AtomicNumber });

            if (dr.HasRows)
            {
                sql = "UPDATE element SET " +
                          "symbol = @2, " +
                          "atomicMass = @3, " +
                          "name = @4, " +
                          "atomicRadius = @5, " +
                          "meltingPoint = @6, " +
                          "boilingPoint = @7, " +
                          "density = @8, " +
                          "electronAffinity = @9, " +
                          "electronegativity = @10, " +
                          "electronicConfiguration = @11, " +
                          "groupBlock = @12, " +
                          "ionRadius = @13, " +
                          "ionizationEnergy = @14, " +
                          "oxidationStates = @15, " +
                          "standardState = @16, " +
                          "vanDerWaalsRadius = @17, " +
                          "yearDiscovered = @18 " +
                        "WHERE atomicNumber = @1";
                dr.Close();
            }
            else
            {
                sql = "INSERT INTO element " +
                          "(atomicNumber, symbol, atomicMass, name, atomicRadius, meltingPoint, boilingPoint, density, electronAffinity, electronegativity, electronicConfiguration, groupBlock, ionRadius, ionizationEnergy, oxidationStates, standardState, vanDerWaalsRadius, yearDiscovered) " +
                        "VALUES " +
                          "(@1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12, @13, @14, @15, @16, @17, @18)";
            }

            con.Run(sql, new List<object>
            {
                AtomicNumber,
                Symbol,
                AtomicMass,
                Name,
                AtomicRadius,
                MeltingPoint,
                BoilingPoint,
                Density,
                ElectronAffinity,
                Electronegativity,
                ElectronicConfiguration,
                GroupBlock.Id,
                IonRadius,
                IonizationEnergy,
                string.Join(", ", OxidationStates),
                StandardState.Id,
                VanDerWaalsRadius,
                YearDiscovered
            });
        }

        public async Task SaveAsync()
        {
            await Task.Run(() =>
            {
                Save();
            });
        }
    }
}
