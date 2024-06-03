using Microsoft.AspNetCore.Mvc;
using StackApiDemo.Handlers;
using StackApiDemo.Models.TagsModels;
using StackApiDemo.Parameters;

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

        [HttpPost("RefreshDatabase")]
        public async Task<IActionResult> RefreshDatabaseAsync()
        {
            await _stackOverflowTagsHandler.HandleRefreshDatabaseAsync();
            return Created();
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetAsync([FromQuery]TagParameters tagParameters)
        {
            var tags = await _stackOverflowTagsHandler.HandleGetAsync(tagParameters);

            if (tags == null)
                return NotFound();

            return Ok(tags);
        }

        [HttpGet("GetByName")]
        public async Task<IActionResult> GetByNameAsync(string name)
        {
            var tag = await _stackOverflowTagsHandler.HandleGetByNameAsync(name);

            if (tag == null)
                return NotFound();

            return Ok(tag);
        }

        [HttpPost("AddTag")]
        public async Task<IActionResult> AddTagsImportAsync(TagsImport tagsImport)
        {
            var result = await _stackOverflowTagsHandler.HandleAddTagsImportAsync(tagsImport);

            return result > 0 ? Created() : BadRequest();
        }

        [HttpDelete("DeleteTag")]
        public async Task<IActionResult> DeleteTagAsync(string name)
        {
            var result = await _stackOverflowTagsHandler.HandleDeleteTagAsync(name);

            return result > 0 ? NoContent() : NotFound();
        }

        [HttpPut("UpdateTag")]
        public async Task<IActionResult> UpdateTagAsync(Tag tag)
        {
            var result = await _stackOverflowTagsHandler.HandleUpdateTagAsync(tag);

            return result > 0 ? NoContent() : NotFound();
        }
    }
}
