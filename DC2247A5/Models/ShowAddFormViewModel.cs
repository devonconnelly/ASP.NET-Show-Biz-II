using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DC2247A5.Models
{
    public class ShowAddFormViewModel : ShowAddViewModel
    {
        public string ActorName { get; set; }

        public MultiSelectList Actors { get; set; }

        public SelectList Genres { get; set; }

    }
}