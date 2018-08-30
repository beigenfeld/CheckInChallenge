using CheckInWeb.Data.Entities;
using CheckInWeb.Data.Repositories;
using CheckInWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CheckInWeb.Controllers
{
    public class AwardController : Controller
    {
        private readonly IRepository repository;
        // GET: Award
        public ActionResult Index()
        {
            return View();
        }
        //Convert these Actions into a Helper Class
        public ActionResult GetUserWithCheckins()
        {
            var model = new MyCheckInViewModel();
            var username = HttpContext.User.Identity.Name;
            var user = repository.Query<ApplicationUser>().SingleOrDefault(u => u.UserName == username);
            // check to see if this user meets any achievements
            //These attributes are properties of a Customer, and should be on the Customer Model
            var allCheckins = repository.Query<CheckIn>().Where(c => c.User.Id == user.Id);
            var allAchievements = repository.Query<Achievement>();
            var allLocationIds = repository.Query<Location>().Select(l => l.Id);
            return View();
        }
        

        public ActionResult AwardTwoInOneDay()
        {
            var model = new MyCheckInViewModel();
            var username = HttpContext.User.Identity.Name;
            var user = repository.Query<ApplicationUser>().SingleOrDefault(u => u.UserName == username);
            // check to see if this user meets any achievements
            //These attributes are properties of a Customer, and should be on the Customer Model
            var allCheckins = repository.Query<CheckIn>().Where(c => c.User.Id == user.Id);
            var allAchievements = repository.Query<Achievement>();
            var allLocationIds = repository.Query<Location>().Select(l => l.Id);
            //If already awarded, break;
            if (!allAchievements.Any(a => a.Type == AchievementType.TwoInOneDay) && allCheckins.Count(c => DbFunctions.TruncateTime(c.Time) == DateTime.Today) > 2)
            {
                var twoInOneDay = new Achievement { Type = AchievementType.TwoInOneDay, User = user, TimeAwarded = DateTime.Now };
                repository.Insert(twoInOneDay);
            }
            return View();
        }

        // all locations?
        public ActionResult HasAll()
        {
            var model = new MyCheckInViewModel();
            var username = HttpContext.User.Identity.Name;
            var user = repository.Query<ApplicationUser>().SingleOrDefault(u => u.UserName == username);
            // check to see if this user meets any achievements
            //These attributes are properties of a Customer, and should be on the Customer Model
            var allCheckins = repository.Query<CheckIn>().Where(c => c.User.Id == user.Id);
            var allAchievements = repository.Query<Achievement>();
            var allLocationIds = repository.Query<Location>().Select(l => l.Id);
            var hasAll = false;
            foreach (var testLocationId in allLocationIds)
            {
                hasAll = hasAll || allCheckins.Any(c => c.Location.Id == testLocationId);
            }

            if (!allAchievements.Any(a => a.Type == AchievementType.AllLocations) && hasAll)
            {
                var allLocations = new Achievement { Type = AchievementType.AllLocations, User = user, TimeAwarded = DateTime.Now };
                repository.Insert(allLocations);
            }
            return View();
        }

        public ActionResult AwardCheckInTogether(CheckIn checkIn)
        {
            var model = new MyCheckInViewModel();
            var username = HttpContext.User.Identity.Name;
            var user = repository.Query<ApplicationUser>().SingleOrDefault(u => u.UserName == username);
            // check to see if this user meets any achievements
            //These attributes are properties of a Customer, and should be on the Customer Model
            var allCheckins = repository.Query<CheckIn>().Where(c => c.User.Id == user.Id);
            var allAchievements = repository.Query<Achievement>();
            var allLocationIds = repository.Query<Location>().Select(l => l.Id);


            //Query the CheckIns repository, WHERE the DateTime on all CheckIn Times is less than an hour of this checkin.
            //the users on those checkins and the current user receive the award

            var checkInTogetherArray = repository.Query<CheckIn>().Where(c => c.Time.Subtract(checkIn.Time).TotalHours <= 1);

            return View();
        }


    }
}