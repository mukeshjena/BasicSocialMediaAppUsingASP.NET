using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SocialMedia.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public int UserId { get; set; }

        [Required(ErrorMessage = "Post content is required")]
        public string PostContent { get; set; }

        public DateTime PostTime { get; set; }
    }
}


