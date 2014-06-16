using System.Collections.Specialized;
using System.ComponentModel;
using System.Net.Mime;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeGetter.Models;


namespace YoutubeGetterTests
{
    [TestFixture]
    public class DownloadMediaModelTests
    {
        [Test]
        public void CanCreateInstanceOfDownloadMedia()
        {
            var mediaType = MediaType.Video;
            var address = "http://www.youtube.com/123456";

            var downloadUrl = "http://testurl";
            var resolution = 720;
            var videoExtension = ".mp4";
            var videoType = VideoFormat.Mp4;
            var bitrate = 96;
            var audioExtension = ".mp3";
            var audioType = AudioFormat.Mp3;

            //event
            //var changeListnerMock = new Mock<PropertyChangedEventHandler>();
            //changeListnerMock.Setup(o => o.Invoke(It.IsAny<object>(), It.IsAny<PropertyChangedEventArgs>())).Verifiable();

            DownloadMediaModel model = new DownloadMediaModel(
                mediaType, 
                address,
                downloadUrl,
                videoType,
                resolution,
                videoExtension,
                audioType,
                bitrate,
                audioExtension
                );
            //model.PropertyChanged += changeListnerMock.Object;

            Assert.IsNotNull(model);
            Assert.IsInstanceOf<INotifyPropertyChanged>(model);

            Assert.AreEqual(mediaType, model.MediaType);
            Assert.AreEqual(address, model.YoutubeAddress);

            Assert.AreEqual(downloadUrl, model.DownloadAddress);
            Assert.AreEqual(videoType, model.VideoFormat);
            Assert.AreEqual(resolution, model.VideoResolution);
            Assert.AreEqual(videoExtension, model.VideoFileExtension);
            Assert.AreEqual(audioType, model.AudioFormat);
            Assert.AreEqual(bitrate, model.AudioBitrate);
            Assert.AreEqual(audioExtension, model.AudioFileExtension);

            

            //changeListnerMock.Verify(o => o.Invoke(It.IsAny<object>(), It.IsAny<PropertyChangedEventArgs>()));
            
        }
    }
}
