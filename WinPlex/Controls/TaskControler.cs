using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Data.Json;
using WinPlex.Classes;
using Newtonsoft.Json;

namespace WinPlex.Controls
{
    class TaskControler
    {
        public static String PROJECT_URL = "https://beta.todoist.com/API/v8/tasks";
        public static async void fetchTaskAsync()
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            string api = (string) localSettings.Values["apiKey"];

            List<Classes.Task> tasks = new List<Classes.Task>();
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + api);
                    HttpResponseMessage response = await client.GetAsync(PROJECT_URL);
                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();
                    JsonArray jsonArr = JsonArray.Parse(responseBody).GetArray();
                    for (uint i = 0; i < jsonArr.Count; i++)
                    {
                        int id = (int)jsonArr.GetObjectAt(i).GetNamedNumber("id");
                        int project_id = (int)jsonArr.GetObjectAt(i).GetNamedNumber("project_id");
                        string content = jsonArr.GetObjectAt(i).GetNamedString("content");
                        bool completed = jsonArr.GetObjectAt(i).GetNamedBoolean("completed");
                        int order = (int)jsonArr.GetObjectAt(i).GetNamedNumber("order");
                        int indent = (int)jsonArr.GetObjectAt(i).GetNamedNumber("indent");
                        int priority = (int)jsonArr.GetObjectAt(i).GetNamedNumber("priority");
                        string url = jsonArr.GetObjectAt(i).GetNamedString("url");
                        Classes.Task task = new Classes.Task()
                        {
                            id = id,
                            project_id = project_id,
                            content = content,
                            completed = completed,
                            order = order,
                            indent = indent,
                            priority = priority,
                            url = url,
                        };
                        tasks.Add(task);
                    }
                    string serializedTasks = JsonConvert.SerializeObject(tasks);
                    localSettings.Values["tasks"] = JsonConvert.SerializeObject(tasks);
                }
                catch (HttpRequestException e)
                {
                    Debug.WriteLine("\nException Caught!");
                    Debug.WriteLine("Message :{0} ", e.Message);
                }
            }
        }

        public static List<Classes.Task> GetTasks()
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            string serializedTasks = (string) localSettings.Values["tasks"];
            Debug.WriteLine(serializedTasks);
            List<Classes.Task> tasks = GetTasksFromJson(serializedTasks);
            return tasks;
        }

        private static List<Classes.Task> GetTasksFromJson(string json)
        {
            List<Classes.Task> tasks = new List<Classes.Task>();
            JsonArray jsonArr = JsonArray.Parse(json).GetArray();
            for (uint i = 0; i < jsonArr.Count; i++)
            {
                int id = (int)jsonArr.GetObjectAt(i).GetNamedNumber("id");
                int project_id = (int)jsonArr.GetObjectAt(i).GetNamedNumber("project_id");
                string content = jsonArr.GetObjectAt(i).GetNamedString("content");
                bool completed = jsonArr.GetObjectAt(i).GetNamedBoolean("completed");
                int order = (int)jsonArr.GetObjectAt(i).GetNamedNumber("order");
                int indent = (int)jsonArr.GetObjectAt(i).GetNamedNumber("indent");
                int priority = (int)jsonArr.GetObjectAt(i).GetNamedNumber("priority");
                string url = jsonArr.GetObjectAt(i).GetNamedString("url");
                Classes.Task task = new Classes.Task()
                {
                    id = id,
                    project_id = project_id,
                    content = content,
                    completed = completed,
                    order = order,
                    indent = indent,
                    priority = priority,
                    url = url,
                };
                tasks.Add(task);
            }
            return tasks;
        }
    }
}
