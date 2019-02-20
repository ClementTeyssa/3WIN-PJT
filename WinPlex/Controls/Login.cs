using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Text;
using System.Net.Http;
using System.Diagnostics;


namespace WinPlex.Controls
{
    class isLogAsync
    {
        public static String PROJECT_URL = "https://beta.todoist.com/API/v8/projects";

        public static async Task Main(string api)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + api);
                    HttpResponseMessage response = await client.GetAsync(PROJECT_URL);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    Debug.WriteLine(responseBody);
                }
                catch (HttpRequestException e)
                {
                    Debug.WriteLine("\nException Caught!");
                    Debug.WriteLine("Message :{0} ", e.Message);
                }
            }
        }
    }
}
