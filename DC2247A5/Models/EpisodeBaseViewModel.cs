using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DC2247A5.Models
{
    public class EpisodeBaseViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Season")]
        public int SeasonNumber { get; set; }

        [Required]
        [Display(Name = "Episode")]
        public int EpisodeNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string Genre { get; set; }

        [Required]
        [Display(Name = "Date Aired")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime AirDate { get; set; } = DateTime.Now;

        [Required]
        [StringLength(250)]
        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        [Required]
        [StringLength(250)]
        public string Clerk { get; set; }

        public int ShowId { get; set; }
    }
}