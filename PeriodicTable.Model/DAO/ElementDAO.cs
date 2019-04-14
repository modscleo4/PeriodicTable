using PeriodicTable.Model.Entity;
using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;

namespace PeriodicTable.Model.DAO
{
    public class ElementDAO
    {
        private JavaScriptSerializer serializer = new JavaScriptSerializer();

        private GroupBlockDAO GroupBlockDAO = new GroupBlockDAO();
        private StandardStateDAO StandardStateDAO = new StandardStateDAO();

        public Element GetObject(dynamic data)
        {
            if (data.ContainsKey("message"))
            {
                throw new Exception("Element does not exist!");
            }

            var element = new Element();
            if (data.ContainsKey("atomicMass"))
            {
                if (data["atomicMass"].GetType().IsArray)
                {
                    data["atomicMass"] = data["atomicMass"][0];
                }

                element.AtomicMass = string.IsNullOrWhiteSpace(data["atomicMass"].ToString()) ? null :
                    Convert.ToDecimal(Regex.Replace(data["atomicMass"].ToString(), @"\(.*\)", ""), CultureInfo.InvariantCulture);
            }

            if (data.ContainsKey("symbol"))
            {
                element.Symbol = data["symbol"].ToString();
            }

            if (data.ContainsKey("atomicNumber"))
            {
                element.AtomicNumber = string.IsNullOrWhiteSpace(data["atomicNumber"].ToString()) ? null :
                    Convert.ToInt32(data["atomicNumber"]);
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
                    Convert.ToDecimal(data["density"], CultureInfo.InvariantCulture);
            }

            if (data.ContainsKey("electronAffinity"))
            {
                element.ElectronAffinity = string.IsNullOrWhiteSpace(data["electronAffinity"].ToString()) ? null :
                    Convert.ToInt32(data["electronAffinity"]);
            }

            if (data.ContainsKey("electronegativity"))
            {
                element.ElectronNegativity = string.IsNullOrWhiteSpace(data["electronegativity"].ToString()) ? null :
                    Convert.ToDecimal(data["electronegativity"], CultureInfo.InvariantCulture);
            }

            if (data.ContainsKey("electronicConfiguration"))
            {
                element.ElectronicConfiguration = data["electronicConfiguration"].ToString();
            }

            if (data.ContainsKey("groupBlock"))
            {
                element.GroupBlock = GroupBlockDAO.GetObject(data["groupBlock"].ToString());
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
                element.OxidationStates = data["oxidationStates"].ToString();
            }

            if (data.ContainsKey("standardState"))
            {
                element.StandardStates = StandardStateDAO.GetObject(data["standardState"].ToString());
            }

            if (data.ContainsKey("vanDelWaalsRadius"))
            {
                element.VanDerWallsRadius = string.IsNullOrWhiteSpace(data["vanDelWaalsRadius"].ToString()) ? null :
                    Convert.ToDecimal(data["vanDelWaalsRadius"], CultureInfo.InvariantCulture);
            }

            if (data.ContainsKey("yearDiscovered"))
            {
                element.YearDiscovered = (data["yearDiscovered"].ToString().Trim() == "Ancient") ? null : Convert.ToInt32(data["yearDiscovered"]);
            }

            return element;
        }

        private string Get(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            var content = new MemoryStream();

            try
            {
                WebResponse response = request.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.GetEncoding("utf-8"));
                    string errorText = reader.ReadToEnd();
                    // log errorText
                }

                throw new Exception("Unable to get information from the server");
            }
        }

        public Element ByNumber(int atomicNumber)
        {
            var url = $"https://neelpatel05.pythonanywhere.com/element/atomicnumber?atomicnumber={atomicNumber}";
            dynamic data = serializer.DeserializeObject(Get(url));

            return GetObject(data);
        }

        public Element BySymbol(string symbol)
        {
            var url = $"https://neelpatel05.pythonanywhere.com/element/symbol?symbol={symbol}";
            dynamic data = serializer.DeserializeObject(Get(url));

            if (data.ContainsKey("message"))
            {
                url = $"https://neelpatel05.pythonanywhere.com/element/atomicname?atomicname={symbol}";
                data = serializer.DeserializeObject(Get(url));
            }

            return GetObject(data);
        }
    }
}
