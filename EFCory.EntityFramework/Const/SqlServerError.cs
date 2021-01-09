using System;

namespace UPD.EntityFramework
{
    public class SqlServerError
    {
        public const int UniqueIndex = 2601; // Duplicate Value
        public const int UniqueConstraint = 2627; // Duplicate Value
        public const int ForeignKey = 547;
        public const int TruncatedData = 2628;
    }
}
