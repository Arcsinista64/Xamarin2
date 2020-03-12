using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Plugin.Connectivity;
using System.IO;

namespace ImageUploader
{
    public partial class MainPage : ContentPage
    {
        MediaFile imageFile;

        public MainPage()
        {
            InitializeComponent();
            takePhoto.Clicked += TakePhoto_Clicked;
            uploadPhoto.Clicked += UploadPhoto_Clicked;
        }

        private async void TakePhoto_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if(!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("Sin cámara", "La cámara no está disponible.", "OK");
                return;
            }

            var cameraStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
            var storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);

            if(cameraStatus != PermissionStatus.Granted || storageStatus != PermissionStatus.Granted)
            {
                var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Camera, Permission.Storage });
                cameraStatus = results[Permission.Camera];
                storageStatus = results[Permission.Storage];
            }

            if(cameraStatus == PermissionStatus.Granted && storageStatus == PermissionStatus.Granted)
            {
                imageFile = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    Directory = "SampleImages"
                });

                if (imageFile == null)
                    return;

                fileLocation.Text = imageFile.Path;

                photoToUpload.Source = ImageSource.FromStream(() =>
                {
                    var imageStream = imageFile.GetStream();
                    return imageStream;
                });

                uploadPhoto.IsEnabled = true;
            }
            else
            {
                await DisplayAlert("Permisos negados", "No se pueden tomar fotos.", "OK");
            }
        }

        private async void UploadPhoto_Clicked(object sender, EventArgs e)
        {
            if(!InternetAccess())
            {
                await DisplayAlert("Sin conexión", "No hay conexión a Internet.", "Ok");
                return;
            }

            byte[] imageBuffer = GetByteArray(imageFile);

            activityIndicator.IsRunning = true;

            await AzureStorage.UploadFile(new MemoryStream(imageBuffer));

            activityIndicator.IsRunning = false;
            uploadMessage.Text = "¡Imagen subida con éxito!";
        }

        private bool InternetAccess()
        {
            if (CrossConnectivity.Current.IsConnected)
                return true;
            else
                return false;
        }

        private byte[] GetByteArray(MediaFile mediaFile)
        {
            byte[] buffer = null;

            using (var stream = mediaFile.GetStream())
            {
                if(stream != null)
                {
                    var length = stream.Length;
                    buffer = new byte[length];
                    stream.Read(buffer, 0, (int)length);
                }
            }

            return buffer;
        }

    }
}
