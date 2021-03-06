namespace EverythingFuneral.Web.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Web.Models.DatabaseModels;

    internal sealed class Configuration : DbMigrationsConfiguration<EverythingFuneral.Web.Models.DatabaseModels.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            AddRecordStatuses(context);
            AddCategories(context);
            AddRole(context);
            AddAdminUser(context);
            AssignUserRole(context);
        }

        private void AssignUserRole(ApplicationDbContext context) {
            var store = new UserStore<ApplicationUser>(context);
            var manager = new UserManager<ApplicationUser>(store);
            var user = context.Users.FirstOrDefault(x => string.Compare(x.UserName, "EFAdmin", true) == 0);
            if (!manager.IsInRole(user.Id, "Admin")) {
                manager.AddToRoles(user.Id, "Admin");
            }
        }

        private void AddAdminUser(ApplicationDbContext context) {
            var store = new UserStore<ApplicationUser>(context);
            var manager = new UserManager<ApplicationUser>(store);
            var user = new ApplicationUser() {
                UserName = "EFAdmin",
                Email = "Info@EverythingFuneral.co.za",
                EmailConfirmed = true
            };
            if (!context.Users.Any(x => x.UserName == user.UserName)) {
                var saveUser = manager.Create(user, "MESmtm3152#");
            }
        }

        private static void AddRole(ApplicationDbContext _context) {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_context));
            var role = new IdentityRole();
            role.Name = "Admin";
            if (!roleManager.RoleExists(role.Name)) {
                roleManager.Create(role);
            }
        }

        private static void AddCategories(ApplicationDbContext context) {
            context.FuneralCategories.AddOrUpdate(c => new { c.Category, c.DisplayName },
                new FuneralCategory {
                    Category = "Dignity Funeral", PayoutAmount = 20000M, DisplayName = "Dignity Funeral R20 000", RecordCode = "ACT", CreatedDate = DateTime.Now
                },
                new FuneralCategory {
                    Category = "Classic funeral", PayoutAmount = 50000M, DisplayName = "Dignity Funeral R50 000", RecordCode = "ACT", CreatedDate = DateTime.Now
                },
                new FuneralCategory {
                    Category = "Royal funeral", PayoutAmount = 150000M, DisplayName = "Royal funeral R150 000", RecordCode = "ACT", CreatedDate = DateTime.Now
                }
            );
        }

        private static void AddRecordStatuses(ApplicationDbContext context) {
            context.RecordStattuses.AddOrUpdate(rs => new { rs.RecordCode, rs.Name },
                new RecordStatus {
                    RecordCode = "ACT", Name = "ACTIVE", CreatedDate = DateTime.Now
                },
                new RecordStatus {
                    RecordCode = "NA", Name = "NOT ACTIVE", CreatedDate = DateTime.Now
                }
            );
            context.SaveChanges();
        }
    }
}
