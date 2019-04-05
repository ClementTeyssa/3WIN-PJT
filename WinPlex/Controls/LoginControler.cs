using System;
using Windows.Storage;

namespace WinPlex.Controls
{
    class Login
    {
        public static void StoreApiKey(string api)
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values["apiKey"] = api;
        }
    }
}