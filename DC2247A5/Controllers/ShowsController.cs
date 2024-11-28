using DC2247A5.Data;
using DC2247A5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DC2247A5.Controllers
{
    public class ShowsController : Controller
    {
        private Manager m = new Manager();

        // GET: Shows
        public ActionResult Index()
        {
            return View(m.ShowGetAll());
        }

        // GET: Shows/Details/5
        public ActionResult Details(int? id)
        {
            var obj = m.ShowGetByIdWithInfo(id.GetValueOrDefault());

            if (obj == null)
                return HttpNotFound();
            else
                return View(obj);
        }

        [Authorize(Roles = "Clerk")]
        [Route("Actors/{id}/AddEpisode")]
        public ActionResult AddEpisode(int? id)
        {
            var show = m.ShowGetByIdWithInfo(id.GetValueOrDefault());
            if (show == null)
                return null;

            var genres = m.GenreGetAll();

            var formViewModel = new EpisodeAddFormViewModel
            {
                ShowId = show.Id,
                ShowName = show.Name,
                Genres = new SelectList(genres, "Name", "Name")
            };

            return View(formViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Clerk")]
        [Route("Actors/{id}/AddEpisode")]
        public ActionResult AddEpisode(EpisodeAddViewModel newItem)
        {
            try
            {
                var addedItem = m.EpisodeAdd(newItem);
                
                if (addedItem == null)
                    return View(newItem);
                else
                    return RedirectToAction("Details", "Episodes", new { id = addedItem.Id });
            }
            catch
            {
                return View(newItem);
            }
        }

        // GET: Shows/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Shows/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Shows/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Shows/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Shows/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Shows/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
