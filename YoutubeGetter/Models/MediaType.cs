using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeGetter.Models
{
    public enum MediaType
    {
        Video,
        Audio
    }
    
    public enum VideoFormat
    {
        Unkown,
        Mp4,
        Flash,
        Mobile,
        WebM,
        
    }

    public enum AudioFormat
    {
        Unkown,
        Aac,
        Mp3,
        Vorbis,
    }
}
