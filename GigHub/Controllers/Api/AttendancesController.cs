using System.Linq;
using System.Web.Http;
using GigHub.DTO;
using GigHub.Models;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto attendance)
        {
            var userId = User.Identity.GetUserId();

            if (_context.Attendances.Any(x => x.AttendeeId == userId && x.GigId == attendance.GigId))
                return BadRequest("The attendance alredy exist");

            _context.Attendances.Add(new Attendance
            {
                GigId = attendance.GigId,
                AttendeeId = userId
            });
            _context.SaveChanges();

            return Ok();
        }
    }
}