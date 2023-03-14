using Microsoft.AspNetCore.Mvc;

namespace ABC.Controllers {

    [ApiController]
    [Route("api/v1/news")]
    public class NewsController : ControllerBase {

        readonly NewsService newsService;

        public NewsController(NewsService newsService) {
            this.newsService = newsService;
        }

        [HttpGet("{Type}/{Start}")]
        public async Task<IActionResult> GetStoryIDs(string Type, int Start) {

            return Ok(await newsService.GetStoryIDs(Type, Start));
        }
        
        
        [HttpGet("stories/{Type}/{Start}")]
        public async Task<IActionResult> GetStories(string Type, int Start) {

            return Ok(await newsService.GetStories(Type, Start));
        }

        [HttpPost("comments")]
        public async Task<IActionResult> GetStories([FromBody] List<int> storyIDs) {

            return Ok(await newsService.GetComments(storyIDs));
        }




    }

}
