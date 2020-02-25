using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace moviedb.Models
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public int IdMovie { get; set; }
        public string Comment { get; set; }
        public string UserName { get; set; }
    }
}