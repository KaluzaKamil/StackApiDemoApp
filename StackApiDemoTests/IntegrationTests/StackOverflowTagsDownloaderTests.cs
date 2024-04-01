using Moq;
using StackApiDemo.Models.TagsModels;
using StackApiDemo.StackOverflowApiIntegration;

namespace StackApiDemoTests.IntegrationTests
{
    public class StackOverflowTagsDownloaderTests
    {
        [Fact]
        public async void Success_ImportStackOverflowTagsAsync_ReturnTagImportsList()
        {
            //Arrange
            var stackOverflowHttpClientMock = new Mock<IStackOverflowHttpClient>();
            stackOverflowHttpClientMock.Setup(c => c.GetClient()).Returns(new HttpClient(
                new HttpClientHandler()
                {
                    AutomaticDecompression = System.Net.DecompressionMethods.GZip
                }));

            var downloader = new StackOverflowTagsDownloader(stackOverflowHttpClientMock.Object);

            //Act
            var result = await downloader.ImportStackOverflowTagsAsync();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<TagsImport>>(result);
        }
    }
}
