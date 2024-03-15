using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class CourseAdminPanel
    {
        public int CourseId { get; set; }
        public required string CourseName { get; set; }
        public required string ImageName { get; set; }
        public required int EposodeCount { get; set; } = 0;
    }

    public class CourseListForAdminPanelViewModel
    {
        public List<CourseAdminPanel> Course { get; set; }

        //Pagging system properties
        public int CourseCount { get; set; }
        public int CurrentPage { get; set; }
    }

    public class DeleteCourseAdminViewModel
    {
        public required int CourseId { get; set; }
        public required string Title { get; set; }
    }

    public class CourseCardViewModel
    {
        public int CourseId { get; set; }
        public string CourseTitle { get; set; }
        public string ImageName { get; set; }
        public int Price { get; set; }
        public TimeSpan TotalTime { get; set; }
    }
}
