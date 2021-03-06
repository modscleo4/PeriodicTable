using Microsoft.Data.Sqlite;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PeriodicTable.Model.Entity;
using PeriodicTable.Model.Support;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static PeriodicTable.Model.Database.DB;

namespace PeriodicTable.Model.DAO
{
    public static class ElementDAO
    {
        private static Element GetObject(ref SqliteDataReader dr)
        {
            var element = new Element()
            {
                AtomicNumber = Convert.ToUInt32(dr["atomicNumber"]),
                Symbol = dr["symbol"].ToString(),
                AtomicMass = Convert.ToDecimal(dr["atomicMass"]),
                Name = dr["name"].ToString()
            };

            if (!Convert.IsDBNull(dr["atomicRadius"]))
            {
                element.AtomicRadius = Convert.ToInt32(dr["atomicRadius"]);
            }

            if (!Convert.IsDBNull(dr["meltingPoint"]))
            {
                element.MeltingPoint = Convert.ToDecimal(dr["meltingPoint"], CultureInfo.InvariantCulture);
            }

            if (!Convert.IsDBNull(dr["boilingPoint"]))
            {
                element.BoilingPoint = Convert.ToDecimal(dr["boilingPoint"], CultureInfo.InvariantCulture);
            }

            if (!Convert.IsDBNull(dr["density"]))
            {
                element.Density = Convert.ToDecimal(dr["density"], CultureInfo.InvariantCulture);
            }

            if (!Convert.IsDBNull(dr["electronAffinity"]))
            {
                element.ElectronAffinity = Convert.ToInt32(dr["electronAffinity"]);
            }

            if (!Convert.IsDBNull(dr["electronegativity"]))
            {
                element.Electronegativity = Convert.ToDecimal(dr["electronegativity"], CultureInfo.InvariantCulture);
            }

            if (!Convert.IsDBNull(dr["electronicConfiguration"]))
            {
                element.ElectronicConfiguration = dr["electronicConfiguration"].ToString();
            }

            if (!Convert.IsDBNull(dr["groupBlock"]))
            {
                element.GroupBlock = GroupBlockDAO.Select(Convert.ToInt64(dr["groupBlock"]));
            }

            if (!Convert.IsDBNull(dr["ionRadius"]))
            {
                element.IonRadius = dr["ionRadius"].ToString();
            }

            if (!Convert.IsDBNull(dr["ionizationEnergy"]))
            {
                element.IonizationEnergy = Convert.ToDecimal(dr["ionizationEnergy"], CultureInfo.InvariantCulture);
            }

            if (!Convert.IsDBNull(dr["oxidationStates"]))
            {
                element.OxidationStates = new List<int>();
                var oxidationStates = dr["oxidationStates"].ToString().Split(',');

                foreach (var oxidationState in oxidationStates)
                {
                    if (!string.IsNullOrEmpty(oxidationState))
                    {
                        element.OxidationStates.Add(Convert.ToInt32(oxidationState));
                    }
                }
            }

            if (!Convert.IsDBNull(dr["standardState"]))
            {
                element.StandardState = StandardStateDAO.Select(Convert.ToInt64(dr["standardState"]));
            }

            if (!Convert.IsDBNull(dr["vanDerWaalsRadius"]))
            {
                element.VanDerWaalsRadius = Convert.ToDecimal(dr["vanDerWaalsRadius"], CultureInfo.InvariantCulture);
            }

            if (!Convert.IsDBNull(dr["yearDiscovered"]))
            {
                element.YearDiscovered = Convert.ToInt32(dr["yearDiscovered"]);
            }

            return element;
        }

        private static Element GetObjectFromJSON(dynamic data)
        {
            if (data.ContainsKey("message"))
            {
                throw new PeriodicTableException("Element does not exist!");
            }

            var element = new Element();

            if (data.ContainsKey("atomicNumber"))
            {
                element.AtomicNumber = Convert.ToUInt32(data["atomicNumber"]);
            }

            if (data.ContainsKey("symbol"))
            {
                element.Symbol = data["symbol"].ToString();
            }

            if (data.ContainsKey("atomicMass"))
            {
                if (data["atomicMass"] is JArray)
                {
                    data["atomicMass"] = data["atomicMass"][0];
                }

                element.AtomicMass = string.IsNullOrWhiteSpace(data["atomicMass"].ToString()) ? null :
                    Convert.ToDecimal(Regex.Replace(data["atomicMass"].ToString(), @"\(.*\)", ""), CultureInfo.InvariantCulture);
            }

            if (data.ContainsKey("name"))
            {
                element.Name = data["name"].ToString();
            }

            if (data.ContainsKey("atomicRadius"))
            {
                element.AtomicRadius = string.IsNullOrWhiteSpace(data["atomicRadius"].ToString()) ? null :
                    Convert.ToInt32(data["atomicRadius"]);
            }

            if (data.ContainsKey("meltingPoint"))
            {
                element.MeltingPoint = string.IsNullOrWhiteSpace(data["meltingPoint"].ToString()) ? null :
                    Convert.ToDecimal(data["meltingPoint"], CultureInfo.InvariantCulture);
            }

            if (data.ContainsKey("boilingPoint"))
            {
                element.BoilingPoint = string.IsNullOrWhiteSpace(data["boilingPoint"].ToString()) ? null :
                    Convert.ToDecimal(data["boilingPoint"], CultureInfo.InvariantCulture);
            }

            if (data.ContainsKey("density"))
            {
                element.Density = string.IsNullOrWhiteSpace(data["density"].ToString()) ? null :
                    decimal.Parse(data["density"].ToString(), NumberStyles.Any, CultureInfo.InvariantCulture);
            }

            if (data.ContainsKey("electronAffinity"))
            {
                element.ElectronAffinity = string.IsNullOrWhiteSpace(data["electronAffinity"].ToString()) ? null :
                    Convert.ToInt32(data["electronAffinity"]);
            }

            if (data.ContainsKey("electronegativity"))
            {
                element.Electronegativity = string.IsNullOrWhiteSpace(data["electronegativity"].ToString()) ? null :
                    Convert.ToDecimal(data["electronegativity"], CultureInfo.InvariantCulture);
            }

            if (data.ContainsKey("electronicConfiguration"))
            {
                element.ElectronicConfiguration = data["electronicConfiguration"].ToString();
            }

            if (data.ContainsKey("groupBlock"))
            {
                element.GroupBlock = GroupBlockDAO.Select(data["groupBlock"].ToString());
            }

            if (data.ContainsKey("ionRadius"))
            {
                element.IonRadius = string.IsNullOrWhiteSpace(data["ionRadius"].ToString()) ? null :
                    data["ionRadius"].ToString();
            }

            if (data.ContainsKey("ionizationEnergy"))
            {
                element.IonizationEnergy = string.IsNullOrWhiteSpace(data["ionizationEnergy"].ToString()) ? null :
                    Convert.ToDecimal(data["ionizationEnergy"], CultureInfo.InvariantCulture);
            }

            if (data.ContainsKey("oxidationStates"))
            {
                element.OxidationStates = new List<int>();
                var oxidationStates = data["oxidationStates"].ToString().Split(',');

                foreach (var oxidationState in oxidationStates)
                {
                    if (!string.IsNullOrEmpty(oxidationState))
                    {
                        element.OxidationStates.Add(Convert.ToInt32(oxidationState));
                    }
                }
            }

            if (data.ContainsKey("standardState"))
            {
                element.StandardState = StandardStateDAO.Select(data["standardState"].ToString());
            }

            if (data.ContainsKey("vanDelWaalsRadius"))
            {
                element.VanDerWaalsRadius = string.IsNullOrWhiteSpace(data["vanDelWaalsRadius"].ToString()) ? null :
                    Convert.ToDecimal(data["vanDelWaalsRadius"], CultureInfo.InvariantCulture);
            }

            if (data.ContainsKey("yearDiscovered"))
            {
                element.YearDiscovered = (data["yearDiscovered"].ToString().Trim() == "Ancient") ? null : Convert.ToInt32(data["yearDiscovered"]);
            }

            return element;
        }

        public static async Task UpdateFromAPI(IProgress<int> progress = null)
        {
            var url = $"https://neelpatel05.pythonanywhere.com/";
            var webClient = new WebClient();

            if (progress != null)
            {
                progress.Report(-1);
            }

            if (await Common.RemoteFileExists(url))
            {
                var content = await webClient.DownloadStringTaskAsync(new Uri(url));
                dynamic data = await Task.Run(() => JsonConvert.DeserializeObject(content));

                int count = 0;
                foreach (dynamic innerData in data)
                {
                    Element element = await Task.Run(() => GetObjectFromJSON(innerData));
                    await element.SaveAsync();

                    if (progress != null)
                    {
                        progress.Report(count * 100 / data.Count);
                        count++;
                    }
                }
            }
            else
            {
                throw new WebException("Could not update from API. Using cached data.");
            }
        }

        public static List<Element> Select()
        {
            var elements = new List<Element>();
            var sql = "SELECT * " +
                        "FROM element";
            var dr = con.Select(sql);
            while (dr.Read())
            {
                elements.Add(GetObject(ref dr));
            }
            dr.Close();

            return elements;
        }

        public static Element Select(int atomicNumber)
        {
            Element element = null;
            var sql = "SELECT * " +
                        "FROM element " +
                        "WHERE atomicNumber = @1";
            var dr = con.Select(sql, new List<object> { atomicNumber });
            if (dr.HasRows)
            {
                dr.Read();
                element = GetObject(ref dr);
                dr.Close();
            }

            return element;
        }

        public static Element Select(string symbol)
        {
            Element element = null;
            var sql = "SELECT * " +
                        "FROM element " +
                        "WHERE UPPER(symbol) = @1 " +
                          "OR UPPER(name) = @1";
            var dr = con.Select(sql, new List<object> { symbol.ToUpper() });
            if (dr.HasRows)
            {
                dr.Read();
                element = GetObject(ref dr);
                dr.Close();
            }

            return element;
        }

        public static Task<List<Element>> SelectAsync()
        {
            var tcs = new TaskCompletionSource<List<Element>>();
            Task.Run(() =>
            {
                var r = Select();
                tcs.SetResult(r);
            });

            return tcs.Task;
        }

        public static Task<Element> SelectAsync(int atomicNumber)
        {
            var tcs = new TaskCompletionSource<Element>();
            Task.Run(() =>
            {
                var r = Select(atomicNumber);
                tcs.SetResult(r);
            });

            return tcs.Task;
        }

        public static Task<Element> SelectAsync(string symbol)
        {
            var tcs = new TaskCompletionSource<Element>();
            Task.Run(() =>
            {
                var r = Select(symbol);
                tcs.SetResult(r);
            });

            return tcs.Task;
        }
    }
}
