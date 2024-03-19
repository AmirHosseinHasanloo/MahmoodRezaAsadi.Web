using DataLayer.Entities.Course;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities.User
{
    public class UserCourses
    {
        [Key]
        public int UserCourseId { get; set; }
        public int? UserId { get; set; }
        public int? CourseId { get; set; }


        //navigation property
        public Course.Course? Course { get; set; }
        public User? User { get; set; }
    }
}
