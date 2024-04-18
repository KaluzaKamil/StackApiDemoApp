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

        [HttpPut("RefreshDatabase")]
        public async Task<ActionResult<int>> RefreshDatabaseAsync()
        {
            return new ActionResult<int>(await _stackOverflowTagsHandler.HandleRefreshDatabaseAsync());
        }

        [HttpGet("Get")]
        public ActionResult<IEnumerable<Tag>> Get([FromQuery]TagParameters tagParameters)
        {
            return new ActionResult<IEnumerable<Tag>>(_stackOverflowTagsHandler.HandleGet(tagParameters));
        }

        [HttpGet("GetByName")]
        public ActionResult<Tag?> GetByName(string name)
        {
            var tag = _stackOverflowTagsHandler.HandleGetByName(name);

            if (tag == null)
                return NotFound();

            return new ActionResult<Tag?>(tag);
        }
    }
}
