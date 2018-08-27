using System.Linq;
using System.Web.Mvc;
using CheckInWeb.Data.Context;
using CheckInWeb.Data.Entities;
using CheckInWeb.Data.Repositories;

namespace CheckInWeb.Controllers
{
    public class AchievementController : Controller
    {
        //
        // GET: /Achievement/
        [Authorize]
        public ActionResult Index()
        {
            // Get the data
            var repository = new Repository(new CheckInDatabaseContext());
            var username = HttpContext.User.Identity.Name;
            ViewBag.Achievements = repository.Query<Achievement>().
                Where(c => c.User.UserName == username).
                OrderByDescending(c => c.TimeAwarded).
                ToList();
            return this.View();
        }
    }
}