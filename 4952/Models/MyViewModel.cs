using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _4952.Models
{
    public class MyViewModel
    {
        [Required]
        public HttpPostedFileBase File { get; set; }
        public List<Models.File> Files { get; set; }
    }
}