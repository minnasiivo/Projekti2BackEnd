﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Padel_Kaverit.Models
{
    public class ForumPost
    {
        public long Id { get; set; }
        public string Writer { get; set; } //username
        public DateTime Published { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
       // public ForumAnswer Answers {get; set;}
       

    }
}
