using PeriodicTable.Model.Entity;
using PeriodicTable.Model.Support;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;
using static PeriodicTable.Model.Database.DB;

namespace PeriodicTable.Model.DAO
{
    public class StandardStateDAO
    {
        private StandardState GetObject(ref SQLiteDataReader dr)
        {
            var standardState = new StandardState()
            {
                Id = Convert.ToInt64(dr["rowid"]),
                Value = dr["value"].ToString()
            };

            return standardState;
        }

        public StandardState Save(string value)
        {
            value = Common.ToTitleCase(value);

            var sql = "INSERT INTO standardState " +
                          "(value)" +
                        "VALUES " +
                          "(@1); " +

                          "SELECT last_insert_rowid() AS id";

            var dr = con.Select(sql, new List<object> { value });
            dr.Read();
            var id = Convert.ToInt64(dr["id"]);
            dr.Close();

            return Select(id);
        }

        public Task<StandardState> SaveAsync(string value)
        {
            var tcs = new TaskCompletionSource<StandardState>();
            Task.Run(() =>
            {
                var r = Save(value);
                tcs.SetResult(r);
            });

            return tcs.Task;
        }

        public StandardState Select(long id)
        {
            StandardState standardState = null;
            var sql = "SELECT rowid, * " +
                        "FROM standardState " +
                        "WHERE rowid = @1";
            var dr = con.Select(sql, new List<object> { id });

            if (dr.HasRows)
            {
                dr.Read();
                standardState = GetObject(ref dr);
                dr.Close();
            }

            return standardState;
        }

        public StandardState Select(string value)
        {
            StandardState standardState;
            value = Common.ToTitleCase(value);
            var sql = "SELECT rowid, * " +
                        "FROM standardState " +
                        "WHERE value = @1";
            var dr = con.Select(sql, new List<object> { value });

            if (dr.HasRows)
            {
                dr.Read();
                standardState = GetObject(ref dr);
                dr.Close();
            }
            else
            {
                standardState = Save(value);
            }

            return standardState;
        }

        public Task<StandardState> SelectAsync(long id)
        {
            var tcs = new TaskCompletionSource<StandardState>();
            Task.Run(() =>
            {
                var r = Select(id);
                tcs.SetResult(r);
            });

            return tcs.Task;
        }

        public Task<StandardState> SelectAsync(string value)
        {
            var tcs = new TaskCompletionSource<StandardState>();
            Task.Run(() =>
            {
                var r = Select(value);
                tcs.SetResult(r);
            });

            return tcs.Task;
        }
    }
}
