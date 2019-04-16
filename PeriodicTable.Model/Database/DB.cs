using System;
using System.IO;

namespace PeriodicTable.Model.Database
{
    /// <summary>
    /// Classe estática para a conexão com o banco de dados
    /// </summary>
    public static class DB
    {
        private static string dataSource = Path.Combine(Environment.CurrentDirectory, "PeriodicTable.db");

        public static Connection con = new Connection(dataSource);

        static DB()
        {
            var sql = "CREATE TABLE IF NOT EXISTS groupBlock (" +
                          "name TEXT NOT NULL" +
                        ")";
            con.Run(sql);

            sql = "CREATE TABLE IF NOT EXISTS standardState (" +
                          "value TEXT NOT NULL" +
                        ")";
            con.Run(sql);

            sql = "CREATE TABLE IF NOT EXISTS element (" +
                          "atomicNumber INTEGER NOT NULL, " +
                          "symbol TEXT, " +
                          "atomicMass REAL, " +
                          "name TEXT, " +
                          "atomicRadius INTEGER, " +
                          "meltingPoint REAL, " +
                          "boilingPoint REAL, " +
                          "density REAL, " +
                          "electronAffinity INTEGER, " +
                          "electronegativity REAL, " +
                          "electronicConfiguration TEXT, " +
                          "groupBlock INTEGER REFERENCES groupBlock(rowid), " +
                          "ionRadius TEXT, " +
                          "ionizationEnergy REAL, " +
                          "oxidationStates TEXT, " +
                          "standardState INTEGER REFERENCES standardState(rowid), " +
                          "vanDerWaalsRadius REAL, " +
                          "yearDiscovered INTEGER" +
                        ")";
            con.Run(sql);
        }
    }
}
