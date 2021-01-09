using System;
using System.Collections.Generic;

namespace EFCory.Entities
{
    public class Blog : Entity<int>
    {
        public string Name { get; set; }
        public List<Post> Posts { get; set; } = new List<Post>();
        public DateTime CreatedAt { get; set; }
    }
}
