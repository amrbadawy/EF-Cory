using EFCory.Entities.Blogs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using UPD.EntityFramework;
using System.Linq;

namespace EFCory.Blogs
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
            //await _dbContext.ResetDatabase();
            //await InitData();

            //await AddPostTags();

            var tags = new List<Tag> { Tag.Queue_102, Tag.EfCore_101, Tag.DDD_105 };
            //var tags = new List<Tag> { Tag.CSharp_100, Tag.Architecture_106, Tag.DDD_105 };
            await SetPostTags(1, tags);
        }

        public async Task InitData()
        {
            _dbContext.Tags.AddRange(Tag.CSharp_100, Tag.EfCore_101, Tag.Queue_102, Tag.Sql_103, Tag.FSharp_104, Tag.DDD_105, Tag.Architecture_106);
            await _dbContext.SaveChangesAsync();

            var post1 = new Post
            {
                Title = "Post Title #01",
                Content = "111 -Content for Post",
                Tags = { Tag.CSharp_100, Tag.EfCore_101, Tag.Queue_102 }
            };

            var post2 = new Post
            {
                Title = "Post Title #02",
                Content = "111 -Content for Post",
                Tags = { Tag.EfCore_101, Tag.Queue_102, Tag.Sql_103 }
            };

            var post3 = new Post
            {
                Title = "Post Title #03",
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

            await SaveChangesAsync();
        }

        public async Task SetPostTags(int postId, List<Tag> tags)
        {
            var post = await _dbContext.Posts
                .Where(p => p.Id == postId)
                .Include(p => p.Tags)
                .FirstOrDefaultAsync();

            var deletedItems = post.Tags.Except(tags).ToList();
            deletedItems.ForEach(item => post.Tags.Remove(item));

            //var newTags = await _dbContext.Tags.Where(t => tags.Contains(t)).ToListAsync();
            //var addedItems = newTags.Except(post.Tags).ToList();
            //addedItems.ForEach(item => post.Tags.Add(item));

            var addedItems = tags.Except(post.Tags).ToList();
            addedItems.ForEach(item =>
            {
                _dbContext.Attach(item);
                //_dbContext.Entry(item).State = EntityState.Unchanged;
                post.Tags.Add(item);
            });

            await SaveChangesAsync();
        }

        private async Task SaveChangesAsync()
        {
            _dbContext.DisplayChanges();
            await _dbContext.SaveChangesAsync();
        }
    }
}