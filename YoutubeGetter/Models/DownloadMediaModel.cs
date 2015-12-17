using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using YoutubeExtractor;
using YoutubeGetter.Annotations;

namespace YoutubeGetter.Models
{
    public class DownloadMediaModel : INotifyPropertyChanged
    {
        public MediaType MediaType { get; set; }
        public string YoutubeAddress { get; set; }
        
        public string DownloadAddress { get; set; }

        public VideoFormat VideoFormat { get; set; }
        public int VideoResolution { get; set; }
        public string VideoFileExtension { get; set; }

        public AudioFormat AudioFormat { get; set; }
        public int AudioBitrate { get; set; }
        public string AudioFileExtension { get; set; }

        public VideoInfo VideoDefinition { get; set; }

        public DownloadMediaModel(
            MediaType mediaType,
            string youtubeAddress,
            string downloadUrl,
            VideoFormat videoType,
            int videoResolution,
            string videoExtension,
            AudioFormat audioType,
            int bitrate,
            string audioExtension,
            VideoInfo video
            )
        {
            MediaType = mediaType;
            YoutubeAddress = youtubeAddress;

            DownloadAddress = downloadUrl;

            VideoFormat = videoType;
            VideoResolution = videoResolution;
            VideoFileExtension = videoExtension;

            AudioFormat = audioType;
            AudioBitrate = bitrate;
            AudioFileExtension = audioExtension;

            VideoDefinition = video;
        }

        

        #region INotifyProperyChanged
        
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
