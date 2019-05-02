using Microsoft.Data.Sqlite;
using PeriodicTable.Model.Entity;
using PeriodicTable.Model.Support;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using static PeriodicTable.Model.Database.DB;

namespace PeriodicTable.Model.DAO
{
    public class GroupBlockDAO
    {
        private GroupBlock GetObject(ref SqliteDataReader dr)
        {
            var groupBlock = new GroupBlock()
            {
                Id = Convert.ToInt64(dr["rowid"]),
                Name = dr["name"].ToString()
            };

            switch (groupBlock.Name)
            {
                case "Nonmetal":
                    groupBlock.Color = Color.FromArgb(255, 160, 255, 160);
                    break;

                case "Halogen":
                    groupBlock.Color = Color.FromArgb(255, 255, 255, 153);
                    break;

                case "Noble Gas":
                    groupBlock.Color = Color.FromArgb(255, 192, 255, 255);
                    break;

                case "Alkali Metal":
                    groupBlock.Color = Color.FromArgb(255, 255, 102, 102);
                    break;

                case "Alkaline Earth Metal":
                    groupBlock.Color = Color.FromArgb(255, 255, 222, 173);
                    break;

                case "Transition Metal":
                    groupBlock.Color = Color.FromArgb(255, 255, 192, 192);
                    break;

                case "Metal":
                    groupBlock.Color = Color.FromArgb(255, 204, 204, 204);
                    break;

                case "Metalloid":
                    groupBlock.Color = Color.FromArgb(255, 204, 204, 153);
                    break;

                case "Post-Transition Metal":
                    groupBlock.Color = Color.FromArgb(255, 232, 232, 232);
                    break;

                case "Lanthanoid":
                    groupBlock.Color = Color.FromArgb(255, 255, 191, 255);
                    break;

                case "Actinoid":
                    groupBlock.Color = Color.FromArgb(255, 255, 153, 204);
                    break;
            }

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

        public Task<GroupBlock> SaveAsync(string name)
        {
            var tcs = new TaskCompletionSource<GroupBlock>();
            Task.Run(() =>
            {
                var r = Save(name);
                tcs.SetResult(r);
            });

            return tcs.Task;
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

        public Task<GroupBlock> SelectAsync(long id)
        {
            var tcs = new TaskCompletionSource<GroupBlock>();
            Task.Run(() =>
            {
                var r = Select(id);
                tcs.SetResult(r);
            });

            return tcs.Task;
        }

        public Task<GroupBlock> SelectAsync(string name)
        {
            var tcs = new TaskCompletionSource<GroupBlock>();
            Task.Run(() =>
            {
                var r = Select(name);
                tcs.SetResult(r);
            });

            return tcs.Task;
        }
    }
}
