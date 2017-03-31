using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using AspNetMvc.QuickStart.Models;

namespace AspNetMvc.QuickStart.DAL
{
    public class EduContext : DbContext
    {
        public EduContext() : base("EduContext")
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<StuCourse> StuCourses { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //表名小写
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}