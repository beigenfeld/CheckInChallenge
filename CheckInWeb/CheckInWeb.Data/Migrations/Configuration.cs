using System;
using System.Data.Entity.Migrations;
using System.Linq;
using CheckInWeb.Data.Context;
using CheckInWeb.Data.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CheckInWeb.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<CheckInDatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CheckInDatabaseContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.

            var userStore = new UserStore<ApplicationUser>(context);
            var passwordHasher = new PasswordHasher();

            var users = new[]
            {
                "AlexHardin",
                "IanFox",
                "MichaelWeinand",
                "GrahamMueller",
                "BrandonSalmon",
            }
            .Select(x => new ApplicationUser { UserName = x, PasswordHash = passwordHasher.HashPassword(x) })
            .ToArray();

            context.Users.AddOrUpdate(p => p.UserName, users);

            var locations = new[]
            {
                "Miller Park",
                "Milwaukee Art Museum",
                "Discovery World",
                "Milwaukee County Zoo",
                "Mitchell Park Horticultural Conservatory",
                "Harley-Davidson Museum",
                "Milwaukee Public Museum",
                "Pabst Mansion",
                "Lake Park",
                "Potawatomi Hotel & Casino",
                "Milwaukee Public Market",
                "Bronze Fonz",
                "Pabst Theater",
                "Marcus Center"
            }.Select(x => new Location { Name = x })
            .ToArray();

            context.Locations.AddOrUpdate(l => l.Name, locations);
            context.SaveChanges();

            users = context.Users.ToArray();
            locations = context.Locations.ToArray();

            // Add 100 random checkins
            var r = new Random();
            for (int i = 0; i < 100; i++)
            {
                var user = users[r.Next(users.Length)];
                var location = locations[r.Next(locations.Length)];
                var time = DateTime.Now.AddMinutes(-1 * r.Next(60 * 24 * 100));

                context.CheckIns.Add(new CheckIn { User = user, Location = location, Time = time });
            }
        }
    }
}
