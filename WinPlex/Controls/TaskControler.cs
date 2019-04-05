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
using System.Net;

namespace WinPlex.Controls
{
    class TaskControler
    {
        public static string PROJECT_URL = "https://beta.todoist.com/API/v8/tasks";
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
                        double id = (double) jsonArr.GetObjectAt(i).GetNamedNumber("id");
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

        public static async void closeTaskAsync(Classes.Task task)
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            string api = (string)localSettings.Values["apiKey"];
            
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + api);
                    HttpResponseMessage response = await client.PostAsync(PROJECT_URL + "/"+task.id+"/close", null);
                    response.EnsureSuccessStatusCode();

                    List<Classes.Task> tasks = GetTasksFromJson(((string)localSettings.Values["tasks"]));
                    tasks.Remove(task);
                    
                    localSettings.Values["tasks"] = JsonConvert.SerializeObject(tasks);
                }
                catch (HttpRequestException e)
                {
                    Debug.WriteLine("\nException Caught!");
                    Debug.WriteLine("Message :{0} ", e.Message);
                }
            }
        }

        public static async void addTaskAsync(int p_priority, string p_content)
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            string api = (string)localSettings.Values["apiKey"];

            List<Classes.Task> tasks = new List<Classes.Task>();
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    Random rnd = new Random();
                    
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", "Bearer " + api);

                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    //client.DefaultRequestHeaders.Add("Content-Type", "application/json");
                    client.DefaultRequestHeaders.Add("X-Request-Id", (2).ToString());

                    ToAddTask addTask = new ToAddTask() {
                        content = p_content,
                        priority = p_priority
                    };
                    var stringContent = new StringContent(JsonConvert.SerializeObject(addTask));
                    HttpResponseMessage response = await client.PostAsync(PROJECT_URL, stringContent);

                    response.EnsureSuccessStatusCode();

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
            List<Classes.Task> tasks = GetTasksFromJson(serializedTasks);
            return tasks;
        }

        public static List<Classes.Task> GetTasksFromJson(string json)
        {
            List<Classes.Task> tasks = new List<Classes.Task>();
            JsonArray jsonArr = JsonArray.Parse(json).GetArray();
            for (uint i = 0; i < jsonArr.Count; i++)
            {
                double id = (double)jsonArr.GetObjectAt(i).GetNamedNumber("id");
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
