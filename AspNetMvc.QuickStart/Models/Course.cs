using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace AspNetMvc.QuickStart.Models
{
    /// <summary>
    /// 课程表
    /// id no name class credit desc
    /// </summary>
    public class Course
    {
        public int ID { get; set;}

        [Display(Name = "课程号")]
        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string No { get; set; }

        [Display(Name = "课程名")]
        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string Name { get; set; }

        [Display(Name = "课时")]
        [Range(1, 50)]
        public int Class { get; set; }

        [Display(Name = "学分")]
        [Range(0.5, 100)]
        public float Credit { get; set; }

        [Display(Name = "课程描述")]
        [StringLength(500)]
        public string Desc { get; set; }

        public virtual ICollection<StuCourse> StuCourses { get; set; }
    }

    //public class CourseDbContext : DbContext
    //{
    //    public DbSet<Course> Courses { get; set; }

    //}
}