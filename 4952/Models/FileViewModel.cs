using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _4952.Models
{
    public class FileViewModel
    {
        [Required]
        public HttpPostedFileBase filePosted { get; set; }
        public List<Models.FileMetadata> fileMetadataList { get; set; }
    }
}