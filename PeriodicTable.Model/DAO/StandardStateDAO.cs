using Microsoft.Data.Sqlite;
using PeriodicTable.Model.Entity;
using PeriodicTable.Model.Support;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static PeriodicTable.Model.Database.DB;

namespace PeriodicTable.Model.DAO
{
    public static class StandardStateDAO
    {
        private static StandardState GetObject(ref SqliteDataReader dr)
        {
            var standardState = new StandardState()
            {
                Id = Convert.ToInt64(dr["rowid"]),
                Value = dr["value"].ToString()
            };

            return standardState;
        }

        public static StandardState Select(long id)
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

        public static StandardState Select(string value)
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
                standardState = new StandardState() { Value = value };
                standardState.Save();
            }

            return standardState;
        }

        public static Task<StandardState> SelectAsync(long id)
        {
            var tcs = new TaskCompletionSource<StandardState>();
            Task.Run(() =>
            {
                var r = Select(id);
                tcs.SetResult(r);
            });

            return tcs.Task;
        }

        public static Task<StandardState> SelectAsync(string value)
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
