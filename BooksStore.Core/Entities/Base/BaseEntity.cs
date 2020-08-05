using System;
using System.Collections.Generic;
using System.Text;

namespace BooksStore.Core.Entities.Base
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
