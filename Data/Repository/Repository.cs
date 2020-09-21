using Microsoft.EntityFrameworkCore;
using RedemptionNews.Data;
using RedemptionNews.Models;
using RedemptionNews.Models.Comments;
using RedemptionNews.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedemptionNews.Repository
{
    public class Repository : IRepository
    {
        private AppDbContext _ctx;

        public Repository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public void AddPost(Post post)
        {

            _ctx.Posts.Add(post);
        }

        public void AddSubComment(SubComment comment)
        {
            _ctx.SubComments.Add(comment);
        }

        public List<Post> GetAllPosts()
        {
            return _ctx.Posts.ToList();
        }

        public IndexViewModel GetAllPosts(int pageNumber)
        {
            int pageSize = 5;
            int skipAmount = pageSize * (pageNumber - 1);
            int postsCount = _ctx.Posts.Count();
            int capacity = skipAmount + pageSize;

            var Query = _ctx.Posts.AsQueryable();
                

            return new IndexViewModel
            {
                PageNumber = pageNumber,
                PageCount = (int) Math.Ceiling((double) postsCount / pageSize),
                NextPage = postsCount > capacity,
               Posts = Query
               .Skip(skipAmount)
               .Take(pageSize)
               .ToList()
            };
        }

        public Post GetPost(int id)
        {
            return _ctx.Posts.Include(p => p.MainComments)
                .ThenInclude(mc => mc.SubComments)
                .FirstOrDefault(p => p.Id == id);
        }

        public void RemovePost(int id)
        {
            _ctx.Posts.Remove(GetPost(id));
        }

        public async Task<bool> SaveChangesAsync()
        {
            if (await _ctx.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }

        public void UpdatePost(Post post)
        {
            _ctx.Posts.Update(post);
        }
    }
}
