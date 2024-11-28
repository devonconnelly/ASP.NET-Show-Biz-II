using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DC2247A5.Controllers
{
    public class GenresController : Controller
    {
        private Manager m = new Manager();

        // GET: Genres
        public ActionResult Index()
        {
            return View(m.GenreGetAll());
        }
        
    }
}
