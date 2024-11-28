using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DC2247A5.Models
{
    public class ActorWithShowInfoViewModel : ActorBaseViewModel
    {
        public ICollection<ShowBaseViewModel> Shows { get; set; }
    }
}