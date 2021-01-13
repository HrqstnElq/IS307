using System;
using System.IO;
using Xamarin.Forms;

namespace IS307
{
    public partial class App : Application
    {
        private static Database database;

        public static Database Database
        {
            get
            {
                if (database == null)
                {
                    database = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Food.db"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();

            string token = null;
            try
            {
                token = App.Current.Properties["token"].ToString();
            }
            catch
            {
                App.Current.Properties["token"] = null;
            }

            if (token == null)
                Shell.Current.GoToAsync("//LoginPage");
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}