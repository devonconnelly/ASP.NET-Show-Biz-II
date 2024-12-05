using DC2247A5.Data;
using DC2247A5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DC2247A5.Controllers
{
    public class ActorsController : Controller
    {
        private Manager m = new Manager();

        // GET: Actors
        public ActionResult Index()
        {
            return View(m.ActorGetAll());
        }

        // GET: Actors/Details/5
        public ActionResult Details(int? id)
        {
            var obj = m.ActorGetByIdWithShowInfo(id.GetValueOrDefault());


            if(obj.MediaItems != null)
            {
                obj.Photos = obj.MediaItems
                .Where(mi => mi.ContentType.StartsWith("image/"))
                .OrderBy(mi => mi.Caption)
                .ToList();

                obj.Documents = obj.MediaItems
                    .Where(mi => mi.ContentType == "application/pdf")
                    .OrderBy(mi => mi.Caption)
                    .ToList();

                obj.AudioClips = obj.MediaItems
                    .Where(mi => mi.ContentType.StartsWith("audio/"))
                    .OrderBy(mi => mi.Caption)
                    .ToList();

                obj.VideoClips = obj.MediaItems
                    .Where(mi => mi.ContentType.StartsWith("video/"))
                    .OrderBy(mi => mi.Caption)
                    .ToList();
            }
            

            if (obj == null)
                return HttpNotFound();
            else
                return View(obj);
        }

        // GET: Actors/Create
        [Authorize(Roles = "Executive")]
        public ActionResult Create()
        {
            return View(new ActorAddViewModel());
        }

        // POST: Actors/Create
        [HttpPost]
        [Authorize(Roles = "Executive")]
        [ValidateInput(false)]
        public ActionResult Create(ActorAddViewModel newItem)
        {
            try
            {
                var addedItem = m.ActorAdd(newItem);
                if (addedItem == null)
                    return View(newItem);
                else
                    return RedirectToAction("Details", "Actors", new { id = addedItem.Id });
            }
            catch
            {
                return View(newItem);
            }
        }

        [Authorize(Roles = "Coordinator")]
        [Route("Actors/{id}/AddShow")]
        public ActionResult AddShow(int? id)
        {
            var defaultActor = m.ActorGetByIdWithShowInfo(id.GetValueOrDefault());
            if (defaultActor == null)
                return null;

            var actors = m.ActorGetAll();
            var genres = m.GenreGetAll();


            var formViewModel = new ShowAddFormViewModel
            {
                ActorName = defaultActor.Name,
                Actors = new SelectList(actors, "Id", "Name"),
                Genres = new SelectList(genres, "Name", "Name")
            };

            return View(formViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Coordinator")]
        [Route("Actors/{id}/AddShow")]
        [ValidateInput(false)] 
        public ActionResult AddShow(ShowAddViewModel newItem)
        {
            try
            {
                var addedItem = m.ShowAdd(newItem);
                if (addedItem == null)
                    return View(newItem);
                else
                    return RedirectToAction("Details", "Shows", new { id = addedItem.Id });
            }
            catch
            {
                return View(newItem);
            }
        }

        [Authorize(Roles = "Executive")]
        [Route("Actors/{id}/AddMediaItem")]
        public ActionResult AddMediaItem(int? id)
        {
            var actor = m.ActorGetByIdWithShowInfo(id.GetValueOrDefault());
            if (actor == null) return HttpNotFound();

            var formViewModel = new ActorMediaItemAddFormViewModel
            {
                ActorId = actor.Id,
                ActorName = actor.Name
            };

            return View(formViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Executive")]
        [Route("Actors/{id}/AddMediaItem")]
        public ActionResult AddMediaItem(ActorMediaItemAddViewModel newItem)
        {
            try
            {
                var addedItem = m.ActorMediaItemAdd(newItem);
                if (addedItem == null) 
                    return View(newItem);
                else
                    return RedirectToAction("Details", new { id = newItem.ActorId });
            }
            catch
            {
                return View(newItem);
            }

            
        }

        [Route("Actors/MediaItem/{id}")]
        public ActionResult MediaItemDownload(int id)
        {
            var mediaItem = m.ActorMediaItemGetById(id);
            if (mediaItem == null) return HttpNotFound();

            return File(mediaItem.Content, mediaItem.ContentType, mediaItem.Caption);
        }



        // GET: Actors/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Actors/Edit/5
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

        // GET: Actors/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Actors/Delete/5
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
