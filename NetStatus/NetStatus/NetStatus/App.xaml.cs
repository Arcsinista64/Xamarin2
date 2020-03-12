using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace NetStatus
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = CrossConnectivity.Current.IsConnected ? (Page) new NetworkViewPage() : new NoNetworkPage();
        }

        protected override void OnStart()
        {
            base.OnStart();
            CrossConnectivity.Current.ConnectivityChanged += HandleConnectivityChanged;
        }

        private void HandleConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            Type currentPage = this.MainPage.GetType();
            if(e.IsConnected && currentPage != typeof(NetworkViewPage))
            {
                this.MainPage = new NetworkViewPage();
            }
            else if(!e.IsConnected && currentPage != typeof(NoNetworkPage))
            {
                this.MainPage = new NoNetworkPage();
            }
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
