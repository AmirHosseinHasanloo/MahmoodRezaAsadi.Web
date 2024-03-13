﻿using DataLayer.Entities.Course;
using DataLayer.Entities.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Context
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Only Show UnBanned Users
            modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsBanned);


            // disable cascade delete =>
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
     .SelectMany(t => t.GetForeignKeys())
     .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
        }



        #region User
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        #endregion

        #region Course
        public DbSet<CourseGroup> CourseGroups { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseStatus> CourseStatuses { get; set; }

        #endregion

    }
}
