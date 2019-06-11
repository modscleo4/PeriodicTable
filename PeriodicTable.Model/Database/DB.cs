using System;
using System.IO;

namespace PeriodicTable.Model.Database
{
    /// <summary>
    /// Classe estática para a conexão com o banco de dados
    /// </summary>
    public static class DB
    {
        private static string home = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        private static string dataSource = Path.Combine(home, "PeriodicTable.db");

        public static Connection con = new Connection(dataSource);

        static DB()
        {
            var sql = "CREATE TABLE IF NOT EXISTS groupBlock (" +
                          "rowid INTEGER NOT NULL PRIMARY KEY, " +
                          "name TEXT NOT NULL" +
                        ")";
            con.Run(sql);

            sql = "CREATE TABLE IF NOT EXISTS standardState (" +
                          "rowid INTEGER NOT NULL PRIMARY KEY, " +
                          "value TEXT NOT NULL" +
                        ")";
            con.Run(sql);

            sql = "CREATE TABLE IF NOT EXISTS element (" +
                          "rowid INTEGER NOT NULL PRIMARY KEY, " +
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
