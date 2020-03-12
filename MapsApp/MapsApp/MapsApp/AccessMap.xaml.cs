using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace MapsApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AccessMap : ContentPage
	{
		public AccessMap ()
		{
			InitializeComponent ();
		}

        private async void OpenMapBtn_Clicked(object sender, EventArgs e)
        {
            if(!double.TryParse(EntryLatitude.Text, out double latitud))
            {
                await DisplayAlert("Valor incorrecto", "La latitud debe ser un valor numérico.", "OK");
                return;
            }
            if (!double.TryParse(EntryLongitude.Text, out double longitud))
            {
                await DisplayAlert("Valor incorrecto", "La longityd debe ser un valor numérico.", "OK");
                return;
            }

            var location = new Location(latitud, longitud);
            var options = new MapLaunchOptions
            {
                Name = EntryName.Text,
                NavigationMode = NavigationMode.None
            };

            await Map.OpenAsync(location, options);
        }
    }
}