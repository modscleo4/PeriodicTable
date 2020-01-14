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
    public static class GroupBlockDAO
    {
        private static GroupBlock GetObject(ref SqliteDataReader dr)
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

                default:
                    groupBlock.Color = Color.FromArgb(255, 32, 32, 32);
                    break;
            }

            return groupBlock;
        }

        public static GroupBlock Select(long id)
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

        public static GroupBlock Select(string name)
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
                groupBlock = new GroupBlock() { Name = name };
                groupBlock.Save();
            }

            return groupBlock;
        }

        public static Task<GroupBlock> SelectAsync(long id)
        {
            var tcs = new TaskCompletionSource<GroupBlock>();
            Task.Run(() =>
            {
                var r = Select(id);
                tcs.SetResult(r);
            });

            return tcs.Task;
        }

        public static Task<GroupBlock> SelectAsync(string name)
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
