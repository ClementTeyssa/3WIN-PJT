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

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace WinPlex.Pages
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class TaskPage : Page
    {
        List<Task> ListTasks
        {
            get { return TaskControler.GetTasks(); }
            set { }
        }
        public static readonly DependencyProperty ArticlesProperty =
            DependencyProperty.Register("ListTasks", typeof(List<Task>), typeof(TaskPage), new PropertyMetadata(new List<Task>(), new PropertyChangedCallback(OnTasksChanged)));
        public TaskPage()
        {
            this.InitializeComponent();
            TaskControler.fetchTaskAsync();
            ListTasks = TaskControler.GetTasks();
        }

        private void TaskListView_LayoutUpdated(object sender, object e)
        {

        }
        private static void OnTasksChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }
        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }
    }
}
