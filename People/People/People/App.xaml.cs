using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace People
{
    public partial class App : Application
    {
        public static PersonRepository PersonRepo { get; set; }

        public App(string dbPath)
        {
            InitializeComponent();

            PersonRepo = new PersonRepository(dbPath);

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
