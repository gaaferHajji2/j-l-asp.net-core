using Microsoft.AspNetCore.Mvc;
using MyFirstAPI.Models;
using MyFirstAPI.Services;
using System.Threading.Tasks;

namespace MyFirstAPI.Controllers
{
    // Using primary constructor to init the service
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController(IPostService postService) : Controller
    {
        //private readonly IPostService _postsService;

        //public PostsController(IPostService postsService)
        //{
        //    _postsService = postsService;
        //}

        [HttpGet]
        public async Task<ActionResult<List<Post>>> GetPosts()
        {
            var posts = await postService.GetAllPosts();

            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(int id)
        {
            var post = await postService.GetPost(id);

            if (post == null)
            {
                return NotFound();  
            }

            return Ok(post);
        }

        [HttpPost]
        public async Task<ActionResult<Post>> CreatePost(Post post)
        {
            await postService.CreatePost(post);

            return CreatedAtAction(nameof(GetPost), new { id = post.Id}, post);
        }

        [HttpPut("id")]
        public async Task<ActionResult> UpdatePost(int id, Post post)
        {
            if(id != post.Id)
            {
                return BadRequest();
            }

            var updatedPost = await postService.UpdatePost(id, post);

            if(updatedPost == null)
            {
                return NotFound();
            }

            return Ok(updatedPost);
        }

        [HttpDelete("id")]
        public async Task<ActionResult> DeletePost(int id)
        {
            var post = await postService.GetPost(id);

            if(post == null)
            {
                return NotFound();
            }

            await postService.DeletePost(id);

            return NoContent();
        }
    }
}
