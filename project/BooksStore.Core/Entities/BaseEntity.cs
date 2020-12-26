using System;

namespace BooksStore.Core.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime TimeOfCreate { get; set; }
        public DateTime UpdateTime { get; set; }

        public BaseEntity()
        {
            TimeOfCreate = DateTime.Now;
        }
    }
}
