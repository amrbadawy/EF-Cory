using System;
using System.Collections.Generic;

namespace EFCory.Entities.Blogs
{
    public class Post : Entity<int>
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public Blog Blog { get; set; }
        public List<Tag> Tags { get; set; } = new List<Tag>();

        public DateTime CreatedAt { get; set; }
    }
}
