using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MyTunes.NetStandard
{
    public interface IStreamLoader
    {
        Stream GetStreamForFilename(string filename);
    }
}
