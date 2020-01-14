using System.Globalization;
using System.Net;
using System.Threading.Tasks;

namespace PeriodicTable.Model.Support
{
    public static class Common
    {
        public static string ToTitleCase(string title)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(title.ToLower());
        }

        public async static Task<bool> RemoteFileExists(string url)
        {
            HttpWebResponse response = null;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            var ret = true;

            try
            {
                response = (HttpWebResponse)await request.GetResponseAsync();
            }
            catch (WebException)
            {
                ret = false;
            }
            finally
            {
                // Don't forget to close your response.
                if (response != null)
                {
                    response.Close();
                }
            }

            return ret;
        }
    }
}
