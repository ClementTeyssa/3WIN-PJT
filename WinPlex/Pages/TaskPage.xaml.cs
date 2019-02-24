using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WinPlex.Controls;
using WinPlex.Classes;
using System.Diagnostics;
using Windows.Storage;
using System.Threading.Tasks ; 

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace WinPlex.Pages
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class TaskPage : Page
    {
        List<Classes.Task> ListTasks { get; set; }
        public TaskPage()
        {

            ListTasks = TaskControler.GetTasks();
            this.InitializeComponent();
            taskListView.ItemsSource = ListTasks;
            
        }
        private void TaskListView_LayoutUpdated(object sender, object e)
        {

        }
        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }

        private async void Check_CheckedAsync(object sender, RoutedEventArgs e)
        {
            Classes.Task task = (Classes.Task) taskListView.SelectedItem;
            TaskControler.closeTaskAsync(task);
            this.Frame.Navigate(typeof(MainPage));
            this.Frame.Navigate(typeof(TaskPage));
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            int priority = (int) sliderAdd.Value;
            string content = (string)contentAdd.Text;
            TaskControler.addTaskAsync(priority, content);
            this.Frame.Navigate(typeof(MainPage));
            this.Frame.Navigate(typeof(TaskPage));

        }
    }
}
