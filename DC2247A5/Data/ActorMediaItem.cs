using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DC2247A5.Data
{
    public class ActorMediaItem
    {
        [Key]
        public int Id { get; set; }
        public string Caption { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
    }
}