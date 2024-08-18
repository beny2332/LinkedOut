using LinkedOut.DAL;
using LinkedOut.Models;

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
            db.Posts.Add(post);
            db.SaveChanges();
            PostModel created = db.Posts.FirstOrDefault(p => p.body == post.body);

            return created.id;
        }


    }
}
