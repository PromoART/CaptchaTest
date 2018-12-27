using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GigsController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize]
        public ActionResult Mine()
        {
            var user = User.Identity.GetUserId();
            var gigs = _context.Gigs
                .Where(x => x.ArtistId == user && x.DateTime > DateTime.Now)
                .Include(x => x.Genre)
                .ToList();

            return View(gigs);
        }

        [Authorize]
        public ActionResult Attending()
        {
            var user = User.Identity.GetUserId();
            var upcomingGigs = _context.Attendances
                .Where(a => a.AttendeeId == user)
                .Select(x => x.Gig)
                .Include(x => x.Artist)
                .Include(x => x.Genre)
                .ToList();

            var viewModel = new GigsViewModel
            {
                UpcomingGigs = upcomingGigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs I'm attending"
            };

            return View("Gigs", viewModel);
        }

        [Authorize]
        public ActionResult Following()
        {
            var user = User.Identity.GetUserId();

            var artists = _context.FollowAttendances
                .Where(x => x.FollowerId == user)
                .Select(x => x.Followee)
                .ToList();

            return View(artists);
        }

        [Authorize]
        public ActionResult Create()
        {
            return View("GigForm", new GigFormViewModel
            {
                Genres = _context.Genres.ToList(),
                Heading = "Add a gig"
            });
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var user = User.Identity.GetUserId();
            var gig = _context.Gigs.Single(x => x.Id == id && x.ArtistId == user);

            return View("GigForm", new GigFormViewModel
            {
                Genres = _context.Genres.ToList(),
                Id = gig.Id,
                Date = gig.DateTime.ToString("d MMM yyyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                Genre = gig.GenreId,
                Venue = gig.Venue,
                Heading = "Edit a gig"
            });
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("GigForm", viewModel);
            }

            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue
            };

            _context.Gigs.Add(gig);
            _context.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("GigForm", viewModel);
            }

            var userId = User.Identity.GetUserId();
            var currentGig = _context.Gigs.Single(x => x.Id == viewModel.Id && x.ArtistId == userId);

            currentGig.DateTime = viewModel.GetDateTime();
            currentGig.GenreId = viewModel.Genre;
            currentGig.Venue = viewModel.Venue;

            _context.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }
    }
}