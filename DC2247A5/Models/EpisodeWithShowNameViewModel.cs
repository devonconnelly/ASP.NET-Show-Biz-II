using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DC2247A5.Models
{
    public class EpisodeWithShowNameViewModel : EpisodeBaseViewModel
    {
        [Display(Name = "Show Name")]
        public string ShowName {  get; set; }
    }
}