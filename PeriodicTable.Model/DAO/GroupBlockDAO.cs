using PeriodicTable.Model.Entity;
using PeriodicTable.Model.Support;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using static PeriodicTable.Model.Database.DB;

namespace PeriodicTable.Model.DAO
{
    public class GroupBlockDAO
    {
        private GroupBlock GetObject(ref SQLiteDataReader dr)
        {
            var groupBlock = new GroupBlock()
            {
                Id = Convert.ToInt64(dr["rowid"]),
                Name = dr["name"].ToString()
            };

            return groupBlock;
        }

        public GroupBlock Save(string name)
        {
            name = Common.ToTitleCase(name);

            var sql = "INSERT INTO groupBlock " +
                          "(name)" +
                        "VALUES " +
                          "(@1);" +

                          "SELECT last_insert_rowid() AS id";

            var dr = con.Select(sql, new List<object> { name });
            dr.Read();
            var id = Convert.ToInt64(dr["id"]);
            dr.Close();

            return Select(id);
        }

        public GroupBlock Select(long id)
        {
            GroupBlock groupBlock = null;
            var sql = "SELECT rowid, * " +
                        "FROM groupBlock " +
                        "WHERE rowid = @1";
            var dr = con.Select(sql, new List<object> { id });

            if (dr.HasRows)
            {
                dr.Read();
                groupBlock = GetObject(ref dr);
                dr.Close();
            }

            return groupBlock;
        }

        public GroupBlock Select(string name)
        {
            GroupBlock groupBlock;
            name = Common.ToTitleCase(name);
            var sql = "SELECT rowid, * " +
                        "FROM groupBlock " +
                        "WHERE name = @1";
            var dr = con.Select(sql, new List<object> { name });

            if (dr.HasRows)
            {
                dr.Read();
                groupBlock = GetObject(ref dr);
                dr.Close();
            }
            else
            {
                groupBlock = Save(name);
            }

            return groupBlock;
        }
    }
}
