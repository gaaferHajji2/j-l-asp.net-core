using MyFirstAPI.Models;

namespace MyFirstAPI.Services
{
    public interface IPostService
    {
        public Task CreatePost(Post item);

        public Task<Post?> UpdatePost(int id, Post item);

        public Task<Post?> GetPost(int id);

        public Task<List<Post>> GetAllPosts();

        public Task DeletePost(int id);
    }
}
