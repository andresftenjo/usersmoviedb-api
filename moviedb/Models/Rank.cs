﻿using System;
using System.Collections.Generic;

namespace moviedb.Models
{
    public partial class Rank
    {
        public int Id { get; set; }
        public int IdMovie { get; set; }
        public int IdUser { get; set; }
        public int Rank1 { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}
