using Microsoft.AspNetCore.Mvc;
using MyFirstAPI.Models;
using MyFirstAPI.Services;
using System.Threading.Tasks;

namespace MyFirstAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : Controller
    {
        private readonly IPostsService _postsService;

        public PostsController(IPostService postsService)
        {
            _postsService = postsService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Post>>> GetPosts()
        {
            var posts = _postsService.GetAllPosts();

            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(int id)
        {
            var post = await _postsService.GetPost(id);

            if (post == null)
            {
                return NotFound();  
            }

            return Ok(post);
        }

        [HttpPost]
        public async Task<ActionResult<Post>> CreatePost(Post post)
        {
            await _postsService.CreatePost(post);

            return CreatedAtAction(nameof(GetPost), new { id = post.Id}, post);
        }

        [HttpPut("id")]
        public async Task<ActionResult> UpdatePost(int id, Post post)
        {
            if(id != post.Id)
            {
                return BadRequest();
            }

            var updatedPost = await _postsService.UpdatePost(id, post);

            if(updatedPost == null)
            {
                return NotFound();
            }

            return Ok(updatedPost);
        }

        [HttpDelete("id")]
        public async Task<ActionResult> DeletePost(int id)
        {
            var post = await _postsService.GetPost(id);

            if(post == null)
            {
                return NotFound();
            }

            await _postsService.DeletePost(id);

            return NoContent();
        }
    }
}
