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
        public async Task<ActionResult> RefreshDatabaseAsync()
        {
            await _stackOverflowTagsHandler.HandleRefreshDatabaseAsync();
            return Created();
        }

        [HttpGet("Get")]
        public ActionResult<IEnumerable<Tag>> Get([FromQuery]TagParameters tagParameters)
        {
            var tags = _stackOverflowTagsHandler.HandleGet(tagParameters);

            if (tags == null)
                return NotFound();

            return Ok(tags);
        }

        [HttpGet("GetByName")]
        public ActionResult<Tag?> GetByName(string name)
        {
            var tag = _stackOverflowTagsHandler.HandleGetByName(name);

            if (tag == null)
                return NotFound();

            return Ok(tag);
        }

        [HttpPost("AddTag")]
        public ActionResult AddTagsImport(TagsImport tagsImport)
        {
            var result = _stackOverflowTagsHandler.HandleAddTagsImport(tagsImport);

            return result > 0 ? Created() : BadRequest();
        }
    }
}
