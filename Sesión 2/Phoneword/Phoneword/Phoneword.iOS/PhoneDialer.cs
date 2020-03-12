using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foundation;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using UIKit;
using Xamarin.Forms;
using Phoneword.iOS;

[assembly: Dependency(typeof(PhoneDialer))]
namespace Phoneword.iOS
{
    class PhoneDialer : IDialer
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