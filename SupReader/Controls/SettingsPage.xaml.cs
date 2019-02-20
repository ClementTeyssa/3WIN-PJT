using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Storage;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using SupReader.Classes;
using Newtonsoft.Json;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace SupReader.Controls
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            this.InitializeComponent();
            ApplicationDataContainer roamingSettings = ApplicationData.Current.RoamingSettings;
            if (!roamingSettings.Values.ContainsKey("ListeSources"))
            {
                List<Source> ListeSources = new List<Source>();
                ListeSources.Add(new Source()
                {
                    Lien = "https://www.frandroid.com/feed",
                    Titre = "Frandroid",
                    Description = "Tout ce qu'il faut savoir sur Android, et pas seulement"
                });
            }
            else
            {
                string Json = (string)roamingSettings.Values["ListeSources"];
                List<Source> ListeSources = JsonConvert.DeserializeObject<List<Source>>(Json);
            }


        }
        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }

        private void NewSourceBouton_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(NewSourceLien.Text);
            if (NewSourceLien.Text != null || NewSourceLien.Text != "")
            {
                ApplicationDataContainer roamingSettings = ApplicationData.Current.RoamingSettings;
                if (!roamingSettings.Values.ContainsKey("ListeSources"))
                {
                    List<Source> ListeSources = new List<Source>();
                }
                else
                {
                    string Json = (string)roamingSettings.Values["ListeSources"];
                    List<Source> ListeSources = JsonConvert.DeserializeObject<List<Source>>(Json);
                }



            }
            else
            {
                NewSourceErreur.Text = "Le champ n'est pas valide";
            }
        }
    }
}
