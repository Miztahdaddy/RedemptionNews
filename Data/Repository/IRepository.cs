using RedemptionNews.Models;
using RedemptionNews.Models.Comments;
using RedemptionNews.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedemptionNews.Repository
{
    public interface IRepository
    {
        void AddPost(Post post);
        Post GetPost(int id);

        IndexViewModel GetAllPosts(int pageNumber);
        List<Post> GetAllPosts();
        void UpdatePost(Post post);

        void RemovePost(int id);

        void AddSubComment(SubComment comment);

        Task<bool> SaveChangesAsync();

    }
}
