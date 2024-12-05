using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DC2247A5.Models
{
    public class ActorWithShowInfoViewModel : ActorBaseViewModel
    {
        public ICollection<ShowBaseViewModel> Shows { get; set; }
        public ICollection<ActorMediaItemBaseViewModel> MediaItems { get; set; }
        public ICollection<ActorMediaItemBaseViewModel> Photos { get; set; }
        public ICollection<ActorMediaItemBaseViewModel> Documents { get; set; }
        public ICollection<ActorMediaItemBaseViewModel> AudioClips { get; set; }
        public ICollection<ActorMediaItemBaseViewModel> VideoClips { get; set; }
    }
}