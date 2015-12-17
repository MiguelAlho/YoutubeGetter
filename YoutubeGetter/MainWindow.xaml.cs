using System.Collections.ObjectModel;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using YoutubeExtractor;
using YoutubeGetter.Models;

namespace YoutubeGetter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        //ObservableCollection<DownloadMediaModel> mediaList = new ObservableCollection<DownloadMediaModel>();

        public ObservableCollection<DownloadMediaModel> MediaList { get; private set; }

        string savepath = @"C:\Download\";
        
        public MainWindow()
        {
            if (!Directory.Exists(savepath))
                Directory.CreateDirectory(savepath);

            MediaList = new ObservableCollection<DownloadMediaModel>();
            this.DataContext = MediaList;
        }

        private void GetVideoButtonClick(object formsender, RoutedEventArgs e)
        {
            // Our test youtube link
            string link = YoutubeLink.Text;

            /*
             * Get the available video formats.
             * We'll work with them in the video and audio download examples.
             */
            IEnumerable<VideoInfo> videoInfos = DownloadUrlResolver.GetDownloadUrls(link);

            /*
             * Select the first .mp4 video with 360p resolution
             */
            VideoInfo video = videoInfos
                .OrderByDescending(o => o.Resolution)
                .FirstOrDefault(info => info.VideoType == VideoType.Mp4 && info.AudioType != AudioType.Unknown);

            //TODO: Null Check!!! use first or default, maybe

            if (video != null)
            {
                DownloadMediaModel model = new DownloadMediaModel(
                    MediaType.Video,
                    link,
                    video.DownloadUrl,
                    GetVideoFormat(video.VideoType),
                    video.Resolution,
                    video.VideoExtension,
                    GetAudioFormat(video.AudioType),
                    video.AudioBitrate,
                    video.AudioExtension,
                    video
                    );
                MediaList.Add(model);

                DownloadVideo(video);
            }
            
        }
        
        private void GetAudioButtonClick(object formsender, RoutedEventArgs e)
        {
            // Our test youtube link
            string link = YoutubeLink.Text;

            /*
             * Get the available video formats.
             * We'll work with them in the video and audio download examples.
             */
            IEnumerable<VideoInfo> videoInfos = DownloadUrlResolver.GetDownloadUrls(link);

            /*
             * We want the first extractable video with the highest audio quality.
             */
            VideoInfo video = videoInfos
                .Where(info => info.CanExtractAudio)
                .OrderByDescending(info => info.AudioBitrate)
                .First();

            DownloadMediaModel model = new DownloadMediaModel(
                    MediaType.Audio,
                    link,
                    video.DownloadUrl,
                    GetVideoFormat(video.VideoType),
                    video.Resolution,
                    video.VideoExtension,
                    GetAudioFormat(video.AudioType),
                    video.AudioBitrate,
                    video.AudioExtension,
                    video
                    );
            MediaList.Add(model);


            DownloadAudio(video);
        }

        private AudioFormat GetAudioFormat(AudioType audioType)
        {
            switch (audioType)
            {
                case AudioType.Aac:
                    return AudioFormat.Aac;
                case AudioType.Mp3:
                    return AudioFormat.Mp3;
                case AudioType.Vorbis:
                    return AudioFormat.Vorbis;
                default:
                    return AudioFormat.Unkown;
            }
        }

        private VideoFormat GetVideoFormat(VideoType videoType)
        {
            switch (videoType)
            {
                case VideoType.Flash:
                    return VideoFormat.Flash;
                case VideoType.Mobile:
                    return VideoFormat.Mobile;
                case VideoType.Mp4:
                    return VideoFormat.Mp4;
                case VideoType.WebM:
                    return VideoFormat.WebM;
                default:
                    return VideoFormat.Unkown;
            }
        }

        private void DownloadVideo(VideoInfo video)
        {
            /*
             * Create the video downloader.
             * The first argument is the video to download.
             * The second argument is the path to save the video file.
             */
            var videoDownloader = new VideoDownloader(video,
                System.IO.Path.Combine(savepath, video.Title + video.VideoExtension));

            // Register the ProgressChanged event and print the current progress
            videoDownloader.DownloadProgressChanged += (sender, args) => Console.WriteLine(args.ProgressPercentage);

            /*
             * Execute the video downloader.
             * For GUI applications note, that this method runs synchronously.
             */
            videoDownloader.Execute();
        }

        private void DownloadAudio(VideoInfo video)
        {
            /*
             * Create the audio downloader.
             * The first argument is the video where the audio should be extracted from.
             * The second argument is the path to save the audio file.
             */
            var audioDownloader = new AudioDownloader(video,
                System.IO.Path.Combine(savepath, video.Title + video.AudioExtension));

            // Register the progress events. We treat the download progress as 85% of the progress and the extraction progress only as 15% of the progress,
            // because the download will take much longer than the audio extraction.
            audioDownloader.DownloadProgressChanged += (sender, args) => Console.WriteLine(args.ProgressPercentage*0.85);
            audioDownloader.AudioExtractionProgressChanged +=
                (sender, args) => Console.WriteLine(85 + args.ProgressPercentage*0.15);

            /*
             * Execute the audio downloader.
             * For GUI applications note, that this method runs synchronously.
             */
            audioDownloader.Execute();
        }
    }
}
