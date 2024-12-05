using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DC2247A5.Models
{
    public class EpisodeBaseViewModel : EpisodeAddViewModel
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Video")]
        public string VideoUrl
        {
            get
            {
                return $"/video/{Id}";
            }
        }
    }
}