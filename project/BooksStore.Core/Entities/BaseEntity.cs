using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksStore.Core.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime TimeOfCreate { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
