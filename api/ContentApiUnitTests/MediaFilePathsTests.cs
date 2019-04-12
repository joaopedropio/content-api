using ContentApi.MediaFiles;
using NUnit.Framework;

namespace ContentApiUnitTests
{
    public class MediaFilePathsTests
    {
        [Test]
        public void Should_RemoveBasePath()
        {
            var fullPath = "/content/rapaziada.mpd";
            var basePath = "/content";
            var expected = "/rapaziada.mpd";

            var result = MediaFileHelper.RemoveBasePath(fullPath, basePath);

            Assert.AreEqual(expected, result);
        }
    }
}
