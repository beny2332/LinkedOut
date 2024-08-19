using LinkedOut.DAL;
using LinkedOut.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace LinkedOut.Services
{
    public class PostService
    {
        private DataLayer db;
        public PostService(DataLayer _db) {  db = _db; }

        public async Task<List<PostModel>> getAll()
        { 
            return db.Posts.ToList();
        }
        public async Task<PostModel> getPostById(int id)
        {
            return db.Posts.Find(id);
        }

        public async Task<int> createPost(PostModel post)
        {
            var existingUser = await db.Users.FirstOrDefaultAsync(u => u.id == post.user.id);
            if (existingUser == null) 
            {
                throw new Exception("User does not exist. Cannot create post for non-existent user.");
            }

            post.user = existingUser;
            
            db.Posts.Add(post);
            await db.SaveChangesAsync();

            return post.id;
        }
    }
}
