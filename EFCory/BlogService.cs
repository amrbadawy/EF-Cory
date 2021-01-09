using EFCory.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using UPD.EntityFramework;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;

namespace EFCory
{
    public class BlogService : IBlogService
    {
        #region • Init •

        private readonly DatabaseContext _dbContext;
        public BlogService(DatabaseContext databaseContext)
        {
            _dbContext = databaseContext;
        }

        #endregion

        public async Task Test()
        {
            //await ResetDatabase();

            //await InitData();

            //await AddPostTags();

            var tags = new List<Tag> { Tag.CSharp_100, Tag.Architecture_106, Tag.DDD_105 };
            await SetPostTags(1, tags);
        }

        public async Task InitData()
        {
            _dbContext.Tags.AddRange(Tag.CSharp_100, Tag.EfCore_101, Tag.Queue_102, Tag.Sql_103, Tag.FSharp_104, Tag.DDD_105, Tag.Architecture_106);
            await _dbContext.SaveChangesAsync();

            var post1 = new Post
            {
                Title = "Title #01",
                Content = "111 -Content for Post",
                Tags = { Tag.CSharp_100, Tag.EfCore_101, Tag.Queue_102 }
            };

            var post2 = new Post
            {
                Title = "Title #02",
                Content = "111 -Content for Post",
                Tags = { Tag.EfCore_101, Tag.Queue_102, Tag.Sql_103 }
            };

            var post3 = new Post
            {
                Title = "Title #03",
                Content = "111 -Content for Post",
                Tags = { Tag.EfCore_101, Tag.Sql_103, Tag.FSharp_104 }
            };

            var blog = new Blog
            {
                Name = "dotnet core 5.0",
                Posts = { post1, post2, post3 }
            };

            _dbContext.Blogs.Add(blog);

            await _dbContext.SaveChangesAsync();
        }

        public async Task AddPostTags()
        {
            var post = await _dbContext.Posts
                .Where(p => p.Id == 1)
                .Include(p => p.Tags)
                .FirstOrDefaultAsync();

            var queueTag = await _dbContext.Tags.FindAsync(Tag.Queue_102.Id);
            post.Tags.Add(queueTag);

            var sqlTag = await _dbContext.Tags.FindAsync(Tag.Sql_103.Id);
            post.Tags.Add(sqlTag);

            //post.Tags.Add(Tag.Queue);
            //post.Tags.Add(Tag.Sql);

            DisplayStates();
            await _dbContext.SaveChangesAsync();
        }

        public async Task SetPostTags(int postId, List<Tag> tags)
        {
            var post = await _dbContext.Posts
                .Where(p => p.Id == postId)
                .Include(p => p.Tags)
                .FirstOrDefaultAsync();

            var deletedItems = post.Tags.Except(tags).ToList();
            deletedItems.ForEach(item => post.Tags.Remove(item));

            var newTags = await _dbContext.Tags.Where(t => tags.Contains(t)).ToListAsync();
            var addedItems = newTags.Except(post.Tags).ToList();
            addedItems.ForEach(item => post.Tags.Add(item));

            DisplayStates();
            await _dbContext.SaveChangesAsync();
        }

        private void DisplayStates()
        {
            var entries = _dbContext.ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                //Console.WriteLine($"Entity: {entry.Entity.GetType().Name}, State: { entry.State}");
                Console.WriteLine($"Entity: {entry.Entity.ToString()}, State: { entry.State}");
            }
        }

        private async Task ResetDatabase()
        {
            await _dbContext.Database.EnsureDeletedAsync();
            await _dbContext.Database.EnsureCreatedAsync();
        }
    }
}