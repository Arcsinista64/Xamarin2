using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Phoneword
{
    public class MainPage : ContentPage
    {
        Entry phoneNumberText;
        Button translateButton;
        Button callButton;
        string translatedNumber;

        public MainPage()
        {
            this.Padding = new Thickness(20,20);

            StackLayout panel = new StackLayout
            {
                Spacing = 15
            };

            panel.Children.Add(new Label
            {
                Text = "Enter a Phoneword:",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            });

            panel.Children.Add(phoneNumberText = new Entry {
                Text = "01-800-MARCAME"
            });

            panel.Children.Add(translateButton = new Button {
                Text = "Translate"
            });

            panel.Children.Add(callButton = new Button
            {
                Text = "Call",
                IsEnabled = false
            });

            translateButton.Clicked += TranslateButton_Clicked;
            callButton.Clicked += CallButton_Clicked;
            this.Content = panel;
        }

        private async void CallButton_Clicked(object sender, EventArgs e)
        {
            if(await this.DisplayAlert("Dial a number", "Would you like to call " + translatedNumber + "?", "Yes", "No"))
            {
                var dialer = DependencyService.Get<IDialer>();
                if(dialer != null)
                {
                    await dialer.DialAsync(translatedNumber);
                }
            }
        }

        private void TranslateButton_Clicked(object sender, EventArgs e)
        {
            string enteredNumber = phoneNumberText.Text;
            translatedNumber = PhonewordTranslator.ToNumber(enteredNumber);

            if(!string.IsNullOrEmpty(translatedNumber))
            {
                callButton.IsEnabled = true;
                callButton.Text = "Call " + translatedNumber;
            }
            else
            {
                callButton.IsEnabled = false;
                callButton.Text = "Call";
            }
        }
    }
}
