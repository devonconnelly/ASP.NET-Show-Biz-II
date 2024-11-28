using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DC2247A5.Models
{
    public class EpisodeAddFormViewModel : EpisodeAddViewModel
    {
        public string ShowName { get; set; }

        public SelectList Genres { get; set; }
    }
}