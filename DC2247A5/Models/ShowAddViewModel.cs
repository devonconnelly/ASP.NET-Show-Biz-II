using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DC2247A5.Models
{
    public class ShowAddViewModel
    {
        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Genre { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; } = DateTime.Now;

        [Required]
        [StringLength(250)]
        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        [Required]
        [StringLength(250)]
        public string Coordinator { get; set; }

        [DataType(DataType.MultilineText)]
        public string Premise { get; set; }

        public List<int> ActorIds { get; set; }
    }
}