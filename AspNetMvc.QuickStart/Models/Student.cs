using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AspNetMvc.QuickStart.Models
{
    public class Student
    {
        public int ID { get; set; }

        [Display(Name = "学号")]
        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string No { get; set; }

        [Display(Name = "姓名")]
        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string Name { get; set; }

        [Display(Name="性别")]
        [Required]
        [Range(0, 1)]
        public int Gender { get; set; }

        [Display(Name = "生日")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString= "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDay { get; set; }

        public virtual ICollection<StuCourse> StuCourses { get; set; }
    }

    //public class StudentDbContext : DbContext
    //{
    //    public DbSet<Student> Students { get; set; }
    //}
}