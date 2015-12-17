using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeExtractor;

namespace YoutubeGetter.Models
{
    public interface IYoutubeFacade
    {
        VideoInfo GetDownloadableVideoInfo(string youtubeUrl);
        VideoInfo GetDownloadableAudioInfo(string youtubeUrl);
    }

    public interface IVideoInfo
    {
        AdaptiveType AdaptiveType { get; }
        int AudioBitrate { get; }
        string AudioExtension { get; }
        bool CanExtractAudio { get; }
        string DownloadUrl { get; }
        int FormatCode { get; }
        bool Is3D { get; }
        bool RequiresDecryption { get; }
        int Resolution { get; }
        string Title { get; }
        string VideoExtension { get; }
        VideoType VideoType { get; }
    }

    public class VideoInfoWrapper : IVideoInfo
    {
        private VideoInfo info;

        public AdaptiveType AdaptiveType { get { return info.AdaptiveType; } }
        public int AudioBitrate { get { return info.AudioBitrate; } }
        public string AudioExtension { get { return info.AudioExtension; } }
        public bool CanExtractAudio { get { return info.CanExtractAudio; } }
        public string DownloadUrl {get { return info.DownloadUrl; }}
        public int FormatCode { get { return info.FormatCode; }}
        public bool Is3D {get { return info.Is3D; }}
        public bool RequiresDecryption { get { return info.RequiresDecryption; }}
        public int Resolution { get { return info.Resolution; }}
        public string Title {get { return info.Title; }}
        public string VideoExtension {get { return info.VideoExtension; }}
        public VideoType VideoType {get { return info.VideoType; }}
    }

    public class FakeYoutubeFacade : IYoutubeFacade
    {

        public VideoInfo GetDownloadableVideoInfo(string youtubeUrl)
        {
            

            throw new NotImplementedException();
        }

        public VideoInfo GetDownloadableAudioInfo(string youtubeUrl)
        {
            throw new NotImplementedException();
        }
    }
}
