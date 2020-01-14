
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static PeriodicTable.Model.Database.DB;

namespace PeriodicTable.Model.Entity
{
    public class StandardState
    {
        public long Id { get; set; }
        public string Value { get; set; }

        public void Save()
        {
            var sql = "INSERT INTO standardState " +
                          "(value) " +
                        "VALUES " +
                          "(@1); " +
                        "SELECT last_insert_rowid() as id;";

            var dr = con.Select(sql, new List<object> { Value });
            dr.Read();
            Id = Convert.ToInt64(dr["id"]);
            dr.Close();
        }

        public void SaveAsync(string value)
        {
            Task.Run(() =>
            {
                Save();
            });
        }
    }
}
