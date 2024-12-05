using DC2247A5.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DC2247A5.Models
{
    public class ActorAddViewModel
    {
        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [StringLength(150)]
        [Display(Name = "Alternate Name")]
        public string AlternateName { get; set; }

        [Display(Name = "Birth Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Height (m)")]
        public double? Height { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        [Required]
        [StringLength(250)]
        public string Executive { get; set; }

        [DataType(DataType.MultilineText)]
        public string Biography { get; set; }

        //public ICollection<Show> Shows { get; set; }
    }
}