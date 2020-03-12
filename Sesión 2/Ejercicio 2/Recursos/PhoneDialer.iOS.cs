using System.Threading.Tasks;
using Foundation;
using UIKit;
using Xamarin.Forms;

namespace Phoneword.iOS
{
    public class PhoneDialer : IDialer
    {
        public Task<bool> DialAsync(string phoneNumber)
        {
            return Task.FromResult(
                UIApplication.SharedApplication.OpenUrl(
                new NSUrl("tel:" + phoneNumber))
            );
        }
    }
}