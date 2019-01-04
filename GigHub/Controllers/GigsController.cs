using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
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
                .Where(x => x.ArtistId == user && x.DateTime > DateTime.Now && !x.IsCanceled)
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

        [HttpPost]
        public ActionResult Search(GigsViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new { query = viewModel.SearchTerm });
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

            var gig = new Gig(viewModel.GetDateTime(), viewModel.Genre, viewModel.Venue)
            {
                ArtistId = User.Identity.GetUserId()
            };

            _context.Gigs.Add(gig);

            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var error in e.EntityValidationErrors)
                {
                    Debug.WriteLine(error);
                }

                throw;
            }

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
            var currentGig = _context.Gigs
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .Single(g => g.Id == viewModel.Id && g.ArtistId == userId);

            currentGig.Update(viewModel.GetDateTime(), viewModel.Genre, viewModel.Venue);

            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var error in e.EntityValidationErrors)
                {
                    Debug.WriteLine(error);
                }

                throw;
            }

            return RedirectToAction("Mine", "Gigs");
        }
    }
}