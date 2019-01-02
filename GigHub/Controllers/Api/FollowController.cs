using System.Linq;
using System.Web.Http;
using GigHub.DTO;
using GigHub.Models;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class FollowController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public FollowController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Follow(FollowDto follow)
        {
            var userId = User.Identity.GetUserId();

            if (_context.FollowAttendances.Any(x => x.FollowerId == userId && x.FolloweeId == follow.FollowId))
                return BadRequest("The attendance alredy exist");

            _context.FollowAttendances.Add(new FollowAttendance
            {
                FolloweeId = follow.FollowId,
                FollowerId = userId
            });
            _context.SaveChanges();

            return Ok();
        }
    }
}