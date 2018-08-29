using CheckInWeb.Data.Entities;
using CheckInWeb.Data.Repositories;
using CheckInWeb.Models;
using System;
using System.Collections.Generic;
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
        
        public ActionResult GetUsersCheckins()
        {
            var model = new MyCheckInViewModel();
            var username = HttpContext.User.Identity.Name;
            // check to see if this user meets any achievements
            //These attributes are properties of a Customer, and should be on the Customer Model
            var allCheckins = repository.Query<CheckIn>().Where(c => c.User.Id == user.Id);
            var allAchievements = repository.Query<Achievement>();
            var allLocationIds = repository.Query<Location>().Select(l => l.Id);
            return View();
        }
        

        public ActionResult AwardTwoInOneDay()
        {
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

        public ActionResult AwardCheckInTogether()
        {
            //check in together
            //Query the CheckIns repository, WHERE the DateTime on all CheckIn Times is less than an hour of this checkin.
            //If it returns any (i.e. the count of the returns >0), 
            //the users on those checkins and the current user receive the award
            //repository.Query<CheckIn>().Where(checkInSpan.TotalHours <= 1)

            return View();
        }


    }
}