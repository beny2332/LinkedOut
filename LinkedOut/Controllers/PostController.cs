using LinkedOut.DTO;
using LinkedOut.Models;
using LinkedOut.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkedOut.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class PostController : ControllerBase
    {
        private PostService postService;

        public PostController(PostService _postService) { postService = _postService; }

        // get the list of all the posts.
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PostListDTO>> getAllPosts()
        {
            return Ok(await postService.getAll());
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        // get a post by its id.
        public async Task<ActionResult<PostModel>> getPost(int id)
        {
            return Ok(await postService.getPostById(id));
        }

        // Create a new post.
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreatePost([FromBody] NewPostDTO req)
        {
            bool res = await postService.addNewPost(req);
            if (res)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // Edit Post
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> editPost([FromBody] EditPostDTO req)
        {
            string oldBody = await postService.editPostBody(req.postId, req.newBody);
            return oldBody != String.Empty ? Ok(oldBody) : NotFound();
        }

        // Delete
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> deletePost(int id)
        {
            int res = await postService.deletePostService(id);
            return res != -1 ? Ok() : NotFound();
        }

    }
}
