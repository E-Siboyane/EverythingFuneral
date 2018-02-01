using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EverythingFuneral.Web.Models.DatabaseModels {
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> {
        public ApplicationDbContext() : base("EverythingFuneralConnectionString") {
            base.Configuration.ProxyCreationEnabled = true;
        }

        public static ApplicationDbContext Create() {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<RecordStatus> RecordStattuses { get; set; }
        public DbSet<ClientDetail> ClientDetails { get; set; }
        public DbSet<FuneralCategory> FuneralCategories { get; set; }
    }
}