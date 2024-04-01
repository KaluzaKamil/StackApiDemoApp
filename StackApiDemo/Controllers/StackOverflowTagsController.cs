using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackApiDemo.Handlers;
using StackApiDemo.Models.TagsModels;
using StackApiDemo.Parameters;
using System.Text.Json;

namespace StackApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StackOverflowTagsController : ControllerBase
    {
        private readonly ILogger<StackOverflowTagsController> _logger;
        private readonly IStackOverflowTagsHandler _stackOverflowTagsHandler;

        public StackOverflowTagsController(ILogger<StackOverflowTagsController> logger, IStackOverflowTagsHandler _handler)
        {
            _logger = logger;
            _stackOverflowTagsHandler = _handler;
        }

        [HttpPut("RefreshDatabase", Name = "RefreshDatabase")]
        public async Task<int> RefreshDatabaseAsync()
        {
            return await _stackOverflowTagsHandler.HandleRefreshDatabaseAsync();
        }

        [HttpGet("GetTags", Name = "GetTags")]
        public IEnumerable<Tag> GetTags([FromQuery]TagParameters tagParameters)
        {
            return _stackOverflowTagsHandler.HandleGetTags(tagParameters);
        }
    }
}
