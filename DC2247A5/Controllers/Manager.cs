using AutoMapper;
using DC2247A5.Data;
using DC2247A5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

// ************************************************************************************
// WEB524 Project Template V2 == 2241-4774a983-efe8-4174-b91f-31ff5a5549d5
//
// By submitting this assignment you agree to the following statement.
// I declare that this assignment is my own work in accordance with the Seneca Academic
// Policy. No part of this assignment has been copied manually or electronically from
// any other source (including web sites) or distributed to other students.
// ************************************************************************************

namespace DC2247A5.Controllers
{
    public class Manager
    {

        // Reference to the data context
        private ApplicationDbContext ds = new ApplicationDbContext();

        // AutoMapper instance
        public IMapper mapper;

        // Request user property...

        // Backing field for the property
        private RequestUser _user;

        // Getter only, no setter
        public RequestUser User
        {
            get
            {
                // On first use, it will be null, so set its value
                if (_user == null)
                {
                    _user = new RequestUser(HttpContext.Current.User as ClaimsPrincipal);
                }
                return _user;
            }
        }

        // Default constructor...
        public Manager()
        {
            // If necessary, add constructor code here

            // Configure the AutoMapper components
            var config = new MapperConfiguration(cfg =>
            {
                // Define the mappings below, for example...
                // cfg.CreateMap<SourceType, DestinationType>();
                // cfg.CreateMap<Product, ProductBaseViewModel>();

                cfg.CreateMap<Models.RegisterViewModel, Models.RegisterViewModelForm>();

                cfg.CreateMap<Genre, GenreBaseViewModel>();

                cfg.CreateMap<Actor, ActorBaseViewModel>()
                .ForMember(dest => dest.Height, opt => opt.MapFrom(src => src.Height == 0 ? (double?)null : src.Height));

                cfg.CreateMap<ActorBaseViewModel, ActorWithShowInfoViewModel>();

                cfg.CreateMap<Actor, ActorWithShowInfoViewModel>()
                .ForMember(dest => dest.Height, opt => opt.MapFrom(src => src.Height == 0 ? (double?)null : src.Height));

                cfg.CreateMap<ActorAddViewModel, Actor>();

                cfg.CreateMap<Show, ShowBaseViewModel>();

                cfg.CreateMap<ShowBaseViewModel, ShowWithInfoViewModel>();

                cfg.CreateMap<Show, ShowWithInfoViewModel>();

                cfg.CreateMap<ShowAddViewModel, Show>();

                cfg.CreateMap<ShowBaseViewModel, ShowAddFormViewModel>();

                cfg.CreateMap<Episode, EpisodeBaseViewModel>();

                cfg.CreateMap<EpisodeBaseViewModel, EpisodeWithShowNameViewModel>();

                cfg.CreateMap<Episode, EpisodeWithShowNameViewModel>();

                cfg.CreateMap<EpisodeAddViewModel, Episode>();

                cfg.CreateMap<EpisodeBaseViewModel, EpisodeAddFormViewModel>();
            });

            mapper = config.CreateMapper();

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }


        // Add your methods below and call them from controllers. Ensure that your methods accept
        // and deliver ONLY view model objects and collections. When working with collections, the
        // return type is almost always IEnumerable<T>.
        //
        // Remember to use the suggested naming convention, for example:
        // ProductGetAll(), ProductGetById(), ProductAdd(), ProductEdit(), and ProductDelete().

        public IEnumerable<GenreBaseViewModel> GenreGetAll()
        {
            var genres = mapper.Map<IEnumerable<Genre>, IEnumerable<GenreBaseViewModel>>(ds.Genres);
            return genres.OrderBy(g => g.Name);
        }

        public IEnumerable<ActorBaseViewModel> ActorGetAll()
        {
            var actors = mapper.Map<IEnumerable<Actor>, IEnumerable<ActorBaseViewModel>>(ds.Actors);
            return actors.OrderBy(a => a.Name);
        }

        public ActorBaseViewModel ActorAdd(ActorAddViewModel newActor)
        {
            var user = HttpContext.Current.User.Identity.Name;

            var addedItem = mapper.Map<ActorAddViewModel, Actor>(newActor);
            addedItem.Executive = user;

            ds.Actors.Add(addedItem);
            ds.SaveChanges();

            return addedItem == null ? null : mapper.Map<Actor, ActorBaseViewModel>(addedItem);
        }

        public ActorWithShowInfoViewModel ActorGetByIdWithShowInfo(int id)
        {
            var obj = ds.Actors
                .Include("Shows")
                .FirstOrDefault(a => a.Id == id);

            return obj == null ? null : mapper.Map<ActorWithShowInfoViewModel>(obj);
        }

        public ShowBaseViewModel ShowAdd(ShowAddViewModel newShow)
        {
            var user = HttpContext.Current.User.Identity.Name;

            var addedItem = mapper.Map<ShowAddViewModel, Show>(newShow);
            addedItem.Coordinator = user;

            if (newShow.ActorIds != null && newShow.ActorIds.Any())
            {
                foreach (var actorId in newShow.ActorIds)
                {
                    var actor = ds.Actors.Find(actorId);
                    if (actor != null)
                    {
                        addedItem.Actors.Add(actor);
                    }
                }
            }

            ds.Shows.Add(addedItem);
            ds.SaveChanges();

            return addedItem == null ? null : mapper.Map<Show, ShowBaseViewModel>(addedItem);
        }

        public IEnumerable<ShowBaseViewModel> ShowGetAll()
        {
            var shows = mapper.Map<IEnumerable<Show>, IEnumerable<ShowBaseViewModel>>(ds.Shows);
            return shows.OrderBy(s => s.Name);
        }

        public ShowWithInfoViewModel ShowGetByIdWithInfo(int id)
        {
            var obj = ds.Shows
                .Include("Actors")
                .Include("Episodes")
                .FirstOrDefault(a => a.Id == id);

            return obj == null ? null : mapper.Map<ShowWithInfoViewModel>(obj);
        }

        public EpisodeBaseViewModel EpisodeAdd(EpisodeAddViewModel newEpisode)
        {
            var user = HttpContext.Current.User.Identity.Name;
            var show = ds.Shows.Find(newEpisode.ShowId);
            if (show == null)
                return null;

            var addedItem = mapper.Map<EpisodeAddViewModel, Episode>(newEpisode);
            addedItem.Clerk = user;
            addedItem.Show = show;

            ds.Episodes.Add(addedItem);
            ds.SaveChanges();

            return addedItem == null ? null : mapper.Map<Episode, EpisodeBaseViewModel>(addedItem);
        }

        public IEnumerable<EpisodeWithShowNameViewModel> EpisodeGetAll()
        {
            var episodes = ds.Episodes
                   .Include("Show");

            return mapper.Map<IEnumerable<Episode>, IEnumerable<EpisodeWithShowNameViewModel>>(episodes)
                         .OrderBy(e => e.ShowName)
                         .ThenBy(e => e.SeasonNumber)
                         .ThenBy(e => e.EpisodeNumber);
        }

        public EpisodeWithShowNameViewModel EpisodeGetByIdWithShowName(int id)
        {
            var obj = ds.Episodes
                .Include("Show")
                .FirstOrDefault(a => a.Id == id);

            return obj == null ? null : mapper.Map<EpisodeWithShowNameViewModel>(obj);
        }


        // *** Add your methods ABOVE this line **

        #region Role Claims

        public List<string> RoleClaimGetAllStrings()
        {
            return ds.RoleClaims.OrderBy(r => r.Name).Select(r => r.Name).ToList();
        }

        #endregion

        #region Load Data Methods

        // Add some programmatically-generated objects to the data store
        // Write a method for each entity and remember to check for existing
        // data first.  You will call this/these method(s) from a controller action.
        [Authorize(Roles = "Administrator")]
        public bool LoadRoles()
        {
            // User name
            var user = HttpContext.Current.User.Identity.Name;

            // Monitor the progress
            bool done = false;

            // *** Role claims ***
            if (ds.RoleClaims.Count() == 0)
            {
                ds.RoleClaims.Add(new RoleClaim() { Name = "Administrator" });
                ds.RoleClaims.Add(new RoleClaim() { Name = "Executive" });
                ds.RoleClaims.Add(new RoleClaim() { Name = "Coordinator" });
                ds.RoleClaims.Add(new RoleClaim() { Name = "Clerk" });

                ds.SaveChanges();
                done = true;
            }

            return done;
        }

        public bool RemoveData()
        {
            try
            {
                foreach (var e in ds.RoleClaims)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }

                // Remove additional entities as needed.

                ds.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveDatabase()
        {
            try
            {
                return ds.Database.Delete();
            }
            catch (Exception)
            {
                return false;
            }
        }

        [Authorize(Roles = "Administrator")]
        public bool LoadGenres()
        {
            var user = HttpContext.Current.User.Identity.Name;

            bool done = false;

            if (ds.Genres.Count() == 0)
            {
                ds.Genres.Add(new Genre() { Name = "Drama" });
                ds.Genres.Add(new Genre() { Name = "Animation" });
                ds.Genres.Add(new Genre() { Name = "Sci-Fi & Fantasy" });
                ds.Genres.Add(new Genre() { Name = "Action & Adventure" });

                ds.SaveChanges();
                done = true;
            }

            return done;
        }

        [Authorize(Roles = "Administrator")]
        public bool LoadActors()
        {
            var user = HttpContext.Current.User.Identity.Name;

            bool done = false;

            if (ds.Actors.Count() == 0)
            {
                ds.Actors.Add(new Actor() { Name = "Steven Yeun", BirthDate = new DateTime(1983, 12, 1), Height = 1.75, ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/bb/StevenYeun2015ComicCon.jpg/220px-StevenYeun2015ComicCon.jpg", Executive = user });
                ds.Actors.Add(new Actor() { Name = "Aaron Paul", BirthDate = new DateTime(1979, 8, 27), Height = 1.72, ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/06/Aaron_Paul_SDDC_2019.jpg/800px-Aaron_Paul_SDDC_2019.jpg", Executive = user });
                ds.Actors.Add(new Actor() { Name = "Pedro Pascal", BirthDate = new DateTime(1975, 4, 2), Height = 1.79, ImageUrl = "https://cdn.britannica.com/41/240741-050-D4777963/Pedro-Pascal-attends-premiere-The-Last-of-US-January-2023.jpg", Executive = user });

                ds.SaveChanges();
                done = true;
            }

            return done;
        }

        [Authorize(Roles = "Administrator")]
        public bool LoadShows()
        {
            var user = HttpContext.Current.User.Identity.Name;

            bool done = false;

            var stevenYeun = ds.Actors.SingleOrDefault(a => a.Name == "Steven Yeun");

            if (ds.Shows.Count() == 0)
            {
                ds.Shows.Add(new Show() { Actors = new Actor[] { stevenYeun }, Name = "The Walking Dead", Genre = "Action & Adventure", ReleaseDate = new DateTime(2010, 10, 31), ImageUrl = "https://www.celebdirtylaundry.com/wp-content/uploads/the-walking-dead-season-5-wish-list.jpg", Coordinator = user });
                ds.Shows.Add(new Show() { Actors = new Actor[] { stevenYeun }, Name = "Invincible", Genre = "Animation", ReleaseDate = new DateTime(2021, 3, 26), ImageUrl = "https://1.bp.blogspot.com/-kDMGF0kvjAg/YAtOTfwN4gI/AAAAAAAEtCk/J-iFKrPAFtYupF9Uqa4dyTnOL2FpbC8iwCLcBGAsYHQ/s1481/Invincible_Amazon_Original_key_art.jpg", Coordinator = user });

                ds.SaveChanges();
                done = true;
            }

            return done;
        }

        [Authorize(Roles = "Administrator")]
        public bool LoadEpisodes()
        {
            var user = HttpContext.Current.User.Identity.Name;

            bool done = false;

            var TWD = ds.Shows.SingleOrDefault(a => a.Name == "The Walking Dead");
            var Invincible = ds.Shows.SingleOrDefault(a => a.Name == "Invincible");

            if (ds.Episodes.Count() == 0)
            {
                ds.Episodes.Add(new Episode() { Name = "Days Gone Bye", SeasonNumber = 1, EpisodeNumber = 1, Genre = "Action & Adventure", AirDate = new DateTime(2010, 10, 31), ImageUrl = "https://pictures.betaseries.com/banners/episodes/1275/0ff6894014aae6ea1b9aac2be4309b15.jpg", Clerk = user, Show = TWD });
                ds.Episodes.Add(new Episode() { Name = "The Day Will Come When You Won't Be", SeasonNumber = 7, EpisodeNumber = 1, Genre = "Action & Adventure", AirDate = new DateTime(2016, 10, 23), ImageUrl = "https://tv-fanatic-res.cloudinary.com/iu/s--9wuS8D1---/t_full_episode_show/cs_srgb,f_auto,fl_strip_profile.lossy,q_auto:420/v1455159746/daryls-in-town-the-walking-dead-season-6-episode-9.jpg", Clerk = user, Show = TWD });
                ds.Episodes.Add(new Episode() { Name = "No Way Out", SeasonNumber = 6, EpisodeNumber = 9, Genre = "Action & Adventure", AirDate = new DateTime(2016, 2, 14), ImageUrl = "https://media.gq.com/photos/580de4c6aafbc05a239a5848/master/pass/TWD_701_GP_0506_0072-RT.jpg", Clerk = user, Show = TWD });
                ds.Episodes.Add(new Episode() { Name = "Its About Time", SeasonNumber = 1, EpisodeNumber = 1, Genre = "Animation", AirDate = new DateTime(2021, 3, 26), ImageUrl = "https://telltaletv.com/wp-content/uploads/2021/02/INVI_S1_FG_101_00205322_Still001.jpg", Clerk = user, Show = Invincible });
                ds.Episodes.Add(new Episode() { Name = "We Need To Talk", SeasonNumber = 1, EpisodeNumber = 7, Genre = "Animation", AirDate = new DateTime(2021, 4, 21), ImageUrl = "https://readysteadycut.com/wp-content/uploads/2021/04/Screenshot-2021-04-22-at-20.00.23-2048x1119.jpg", Clerk = user, Show = Invincible });
                ds.Episodes.Add(new Episode() { Name = "Where I Really Come From", SeasonNumber = 1, EpisodeNumber = 8, Genre = "Animation", AirDate = new DateTime(2021, 4, 30), ImageUrl = "https://telltaletv.com/wp-content/uploads/2024/04/MV5BNTFjZmYyOTgtN2M0NC00MThhLWE5YmItYjJjOWZlNTczZjVkXkEyXkFqcGdeQXVyMzE5MDUxODM@._V1_.jpg", Clerk = user, Show = Invincible });

                ds.SaveChanges();
                done = true;
            }

            return done;
        }
    }

        #endregion

        #region RequestUser Class

        // This "RequestUser" class includes many convenient members that make it
        // easier work with the authenticated user and render user account info.
        // Study the properties and methods, and think about how you could use this class.

        // How to use...
        // In the Manager class, declare a new property named User:
        //    public RequestUser User { get; private set; }

        // Then in the constructor of the Manager class, initialize its value:
        //    User = new RequestUser(HttpContext.Current.User as ClaimsPrincipal);

        public class RequestUser
        {
            // Constructor, pass in the security principal
            public RequestUser(ClaimsPrincipal user)
            {
                if (HttpContext.Current.Request.IsAuthenticated)
                {
                    Principal = user;

                    // Extract the role claims
                    RoleClaims = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

                    // User name
                    Name = user.Identity.Name;

                    // Extract the given name(s); if null or empty, then set an initial value
                    string gn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.GivenName).Value;
                    if (string.IsNullOrEmpty(gn)) { gn = "(empty given name)"; }
                    GivenName = gn;

                    // Extract the surname; if null or empty, then set an initial value
                    string sn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Surname).Value;
                    if (string.IsNullOrEmpty(sn)) { sn = "(empty surname)"; }
                    Surname = sn;

                    IsAuthenticated = true;
                    // You can change the string value in your app to match your app domain logic
                    IsAdmin = user.HasClaim(ClaimTypes.Role, "Administrator") ? true : false;
                }
                else
                {
                    RoleClaims = new List<string>();
                    Name = "anonymous";
                    GivenName = "Unauthenticated";
                    Surname = "Anonymous";
                    IsAuthenticated = false;
                    IsAdmin = false;
                }

                // Compose the nicely-formatted full names
                NamesFirstLast = $"{GivenName} {Surname}";
                NamesLastFirst = $"{Surname}, {GivenName}";
            }

            // Public properties
            public ClaimsPrincipal Principal { get; private set; }

            public IEnumerable<string> RoleClaims { get; private set; }

            public string Name { get; set; }

            public string GivenName { get; private set; }

            public string Surname { get; private set; }

            public string NamesFirstLast { get; private set; }

            public string NamesLastFirst { get; private set; }

            public bool IsAuthenticated { get; private set; }

            public bool IsAdmin { get; private set; }

            public bool HasRoleClaim(string value)
            {
                if (!IsAuthenticated) { return false; }
                return Principal.HasClaim(ClaimTypes.Role, value) ? true : false;
            }

            public bool HasClaim(string type, string value)
            {
                if (!IsAuthenticated) { return false; }
                return Principal.HasClaim(type, value) ? true : false;
            }
        }

        #endregion

    }
