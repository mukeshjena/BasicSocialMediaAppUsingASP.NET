using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PracticeEF.Models
{
    public class NameModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please Enter Your Name")]
        [StringLength(100)]
        public string Name { get; set; }
    }
}