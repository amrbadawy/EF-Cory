using System;
using System.Collections.Generic;

namespace EFCory.Entities.Blogs
{
    public class Blog : Entity<int>
    {
        public string Name { get; set; }
        public List<Post> Posts { get; set; } = new List<Post>();
        public DateTime CreatedAt { get; set; }

        public static class Metadata
        {
            public static readonly StringPropertyMetadata Name = new()
            {
                IsRequired = true,
                MaxLength = 250
            };
        }
    }



    public class PropertyMetadata 
    {
        public bool IsRequired { get; init; } = false;
    }

    public class StringPropertyMetadata: PropertyMetadata
    {
        public int MaxLength { get; init; }
    }
    public class DecimalPropertyMetadata: PropertyMetadata
    {
        public int Precision { get; init; }
        public int Scale { get; init; }
    }
}
