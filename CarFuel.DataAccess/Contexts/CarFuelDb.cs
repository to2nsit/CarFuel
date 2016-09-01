using CarFuel.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CarFuel.DataAccess.Contexts {
    public class CarFuelDb : DbContext {

        public DbSet<Car> Cars { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {

            // EF Fluent API

            modelBuilder.Properties<DateTime>()
                .Configure(c => c.HasColumnType("datetime2"));

        }

    }
}