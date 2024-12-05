using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DC2247A5.Models
{
    public class ActorMediaItemAddFormViewModel
    {
        public int ActorId { get; set; }
        public string ActorName { get; set; }
        public string Caption { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Attachment")]
        public string ContentUpload { get; set; }
    }
}