using NoahOnlineLibrary.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoahOnlineLibrary.Domain.Entities
{
    public class Book : BaseEntity
    {
        public string Name { get; set; }
        public int PageCount { get; set; }
        public int AuthorId { get; set; }
        public Author? Author { get; set; }
        public List<ReservedItem>? ReservedItems { get; set; }
    }
}
