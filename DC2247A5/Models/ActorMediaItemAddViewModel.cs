using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DC2247A5.Models
{
    public class ActorMediaItemAddViewModel
    {
        public int ActorId { get; set; }
        public string Caption { get; set; }
        public HttpPostedFileBase ContentUpload { get; set; }
    }
}