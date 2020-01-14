using PeriodicTable.Model.Support;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using static PeriodicTable.Model.Database.DB;

namespace PeriodicTable.Model.Entity
{
    public class GroupBlock
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Color Color { get; set; }

        public void Save()
        {
            var sql = "INSERT INTO groupBlock " +
                          "(name) " +
                        "VALUES " +
                          "(@1); " +
                        "SELECT last_insert_rowid() as id;";

            var dr = con.Select(sql, new List<object> { Name });
            dr.Read();
            Id = Convert.ToInt64(dr["id"]);
            dr.Close();
        }

        public void SaveAsync()
        {
            Task.Run(() =>
            {
                Save();
            });
        }
    }
}
