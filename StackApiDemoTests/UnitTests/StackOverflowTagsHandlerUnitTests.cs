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

            repositoryMock.Setup(r => r.AddTagsImportsAsync(testTagImportsList)).ReturnsAsync(1);

            var handler = new StackOverflowTagsHandler(loggerMock.Object, repositoryMock.Object, downloaderMock.Object);

            //Act
            var result = await handler.HandleRefreshDatabaseAsync();

            //Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task Success_HandleGetTags_ReturnsTagsAsync()
        {
            //Arrange
            var loggerMock = new Mock<ILogger<StackOverflowTagsHandler>>();
            var repositoryMock = new Mock<IStackOverflowTagsRepository>();
            var downloaderMock = new Mock<IStackOverflowTagsDownloader>();
            var testTagsList = new List<Tag>() { new Tag() };
            var tagParameters = new TagParameters();

            repositoryMock.Setup(r => r.GetTagsAsync(tagParameters)).ReturnsAsync(testTagsList);

            var handler = new StackOverflowTagsHandler(loggerMock.Object, repositoryMock.Object, downloaderMock.Object);

            //Act
            var result = await handler.HandleGetAsync(tagParameters);

            //Assert
            Assert.Equal(testTagsList, result);
        }

        [Fact]
        public async Task TagExists_HandleGetTagByName_ReturnsTagAsync()
        {
            //Arrange
            var loggerMock = new Mock<ILogger<StackOverflowTagsHandler>>();
            var repositoryMock = new Mock<IStackOverflowTagsRepository>();
            var downloaderMock = new Mock<IStackOverflowTagsDownloader>();
            var tag = new Tag();
            var name = "test";

            repositoryMock.Setup(r => r.GetTagByNameAsync(name)).ReturnsAsync(tag);

            var handler = new StackOverflowTagsHandler(loggerMock.Object, repositoryMock.Object, downloaderMock.Object);

            //Act
            var result = await handler.HandleGetByNameAsync(name);

            //Assert
            Assert.Equal(tag, result);
        }

        [Fact]
        public async Task TagNotExists_HandleGetTagByName_ReturnsNullAsync()
        {
            //Arrange
            var loggerMock = new Mock<ILogger<StackOverflowTagsHandler>>();
            var repositoryMock = new Mock<IStackOverflowTagsRepository>();
            var downloaderMock = new Mock<IStackOverflowTagsDownloader>();
            Tag? tag = null;
            var name = "test";

            repositoryMock.Setup(r => r.GetTagByNameAsync(name)).ReturnsAsync(tag);

            var handler = new StackOverflowTagsHandler(loggerMock.Object, repositoryMock.Object, downloaderMock.Object);

            //Act
            var result = await handler.HandleGetByNameAsync(name);

            //Assert
            Assert.Equal(tag, result);
        }
    }
}