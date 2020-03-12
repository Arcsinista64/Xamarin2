using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;

namespace MapsApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MapPage : ContentPage
	{
		public MapPage ()
		{
			InitializeComponent ();
            myMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(25.665392, -100.341156), Distance.FromMiles(1)));
		}

        private void StreetBtn_Clicked(object sender, EventArgs e)
        {
            myMap.MapType = MapType.Street;
        }

        private void HybridBtn_Clicked(object sender, EventArgs e)
        {
            myMap.MapType = MapType.Hybrid;
        }

        private void SatelliteBtn_Clicked(object sender, EventArgs e)
        {
            myMap.MapType = MapType.Satellite;
        }
    }
}