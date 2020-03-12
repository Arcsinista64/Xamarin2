using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MyTunes.NetStandard;

namespace MyTunes
{
    class StreamLoader : IStreamLoader
    {
        private Context context;

        public StreamLoader(Context context)
        {
            this.context = context;
        }

        public Stream GetStreamForFilename(string filename)
        {
            return context.Assets.Open(filename);
        }
    }
}