using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DC2247A5.Controllers
{
    public class EpisodesController : Controller
    {
        private Manager m = new Manager();

        // GET: Episodes
        public ActionResult Index()
        {
            return View(m.EpisodeGetAll());
        }

        // GET: Episodes/Details/5
        public ActionResult Details(int? id)
        {
            var obj = m.EpisodeGetByIdWithShowName(id.GetValueOrDefault());

            if (obj == null)
                return HttpNotFound();
            else
                return View(obj);
        }

        [Route("Episodes/Video/{id}")]
        public ActionResult Video(int id)
        {
            var video = m.EpisodeVideoGetById(id);

            if (video == null || video.Video == null)
                return HttpNotFound();

            return File(video.Video, video.VideoContentType);
        }


        // GET: Episodes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Episodes/Create
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

        // GET: Episodes/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Episodes/Edit/5
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

        // GET: Episodes/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Episodes/Delete/5
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
