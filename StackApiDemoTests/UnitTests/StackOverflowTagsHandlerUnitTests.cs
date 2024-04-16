using Microsoft.Extensions.Logging;
using Moq;
using StackApiDemo.Handlers;
using StackApiDemo.Models.TagsModels;
using StackApiDemo.Parameters;
using StackApiDemo.Repositories;
using StackApiDemo.StackOverflowApiIntegration;

namespace StackApiDemoTests.UnitTests
{
    public class StackOverflowTagsHandlerUnitTests
    {
        [Fact]
        public async void Success_HandleRefreshDatabaseAsync_ReturnsSavedRecords()
        {
            //Arrange
            var loggerMock = new Mock<ILogger<StackOverflowTagsHandler>>();
            var repositoryMock = new Mock<IStackOverflowTagsRepository>();
            var downloaderMock = new Mock<IStackOverflowTagsDownloader>();
            var testTagImportsList = new List<TagsImport>(){ new TagsImport() };

            downloaderMock.Setup(d => d.ImportStackOverflowTagsAsync())
                .Returns(Task.FromResult(testTagImportsList));

            repositoryMock.Setup(r => r.AddTagsImports(testTagImportsList)).Returns(1);

            var handler = new StackOverflowTagsHandler(loggerMock.Object, repositoryMock.Object, downloaderMock.Object);

            //Act
            var result = await handler.HandleRefreshDatabaseAsync();

            //Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public void Success_HandleGetTags_ReturnsTags()
        {
            //Arrange
            var loggerMock = new Mock<ILogger<StackOverflowTagsHandler>>();
            var repositoryMock = new Mock<IStackOverflowTagsRepository>();
            var downloaderMock = new Mock<IStackOverflowTagsDownloader>();
            var testTagsList = new List<Tag>() { new Tag() };
            var tagParameters = new TagParameters();

            repositoryMock.Setup(r => r.Get(tagParameters)).Returns(testTagsList);

            var handler = new StackOverflowTagsHandler(loggerMock.Object, repositoryMock.Object, downloaderMock.Object);

            //Act
            var result = handler.HandleGet(tagParameters);

            //Assert
            Assert.Equal(testTagsList, result);
        }
    }
}