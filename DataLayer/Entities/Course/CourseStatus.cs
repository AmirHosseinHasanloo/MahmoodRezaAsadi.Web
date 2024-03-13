﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities.Course
{
    public class CourseStatus
    {
        [Key]
        public int StatusId { get; set; }

        [Required]
        [MaxLength(100)]
        public string StatusTitle { get; set; }

        #region Relation
        public ICollection<Course> Courses { get; set; }
        #endregion
    }
}
