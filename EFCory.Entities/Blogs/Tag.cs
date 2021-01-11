using System;
using System.Collections.Generic;

namespace EFCory.Entities.Blogs
{
    public class Tag : Entity<int>, IEquatable<Tag>
    {
        public string Name { get; set; }
        public List<Post> Posts { get; set; }
        public DateTime CreatedAt { get; set; }

        public bool Equals(Tag other)
        {
            if (other is null)
                return false;

            return Id == other.Id;
        }
        public override bool Equals(object obj) => Equals(obj as Tag);
        public override int GetHashCode() => (Id).GetHashCode();


        public static readonly Tag CSharp_100 = new Tag { Id = 100, Name = "C#" };
        public static readonly Tag EfCore_101 = new Tag { Id = 101, Name = "ef-core" };
        public static readonly Tag Queue_102 = new Tag { Id = 102, Name = "queue" };
        public static readonly Tag Sql_103 = new Tag { Id = 103, Name = "sql" };
        public static readonly Tag FSharp_104 = new Tag { Id = 104, Name = "F#" };
        
        public static readonly Tag DDD_105 = new Tag { Id = 105, Name = "DDD" };
        public static readonly Tag Architecture_106 = new Tag { Id = 106, Name = "Architecture" };
    }
}
