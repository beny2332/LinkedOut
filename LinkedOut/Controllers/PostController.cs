using LinkedOut.DTO;
using LinkedOut.Models;
using LinkedOut.Services;
using Microsoft.AspNetCore.Mvc;

namespace LinkedOut.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class PostController : ControllerBase
    {
        private PostService postService;

        public PostController(PostService _postService) {postService = _postService;}
        
        // get requst to get the list of all the posts.
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PostListDTO>> getAllPosts()
        {
            return Ok(await postService.getAll());
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        // get requst to get a post by its id.
        public async Task<ActionResult<PostModel>> getPost(int id)
        {
            return Ok(await postService.getPostById(id));
        }

        // post requst to create a new post.
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> createPost([FromBody] PostModel post)
        {
            try
            {
                int postId = await postService.createPost(post);
                return CreatedAtAction(nameof(getPost), new { id = postId }, postId);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating post: {ex.Message}");
            }
        }

    }
}
