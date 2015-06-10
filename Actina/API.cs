using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Script.Serialization;

namespace LectorApp
{
    class API
    {
        static public FMDList GetFMDList()
        {
            try
            {
                HttpClient client = new HttpClient();

                var byteArray = Encoding.ASCII.GetBytes("admin@admin.com:admin");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                string getStringTask = client.GetStringAsync("http://127.0.0.1/API/usuarios/FMD").Result;
                string json_string = "{ 'data': " + getStringTask + " }";

                JavaScriptSerializer serializer = new JavaScriptSerializer();

                FMDList fmd = new JavaScriptSerializer().Deserialize<FMDList>(json_string);
                return fmd;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
                return null;
            }
        }
    }
}
