using System;
using System.Collections.Generic;

namespace moviedb.Models
{
    public partial class Users
    {
        public Users()
        {
            Comment = new HashSet<Comment>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }

        //[JsonIgnore]
        //[IgnoreDataMember]
        public virtual ICollection<Comment> Comment { get; set; }
    }
}
