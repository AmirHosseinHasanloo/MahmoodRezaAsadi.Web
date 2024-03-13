using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities.Course
{
    public class CourseGroup
    {
        [Key]
        public int GroupId { get; set; }

        [Display(Name = "عنوان گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(150, ErrorMessage = "فیلد {0} نمی تواند بیش از {1} کاراکتر باشد.")]
        public string GroupTitle { get; set; }

        [Display(Name = "زیر گروه")]
        public int? ParentId { get; set; }

        // navigation properties
        [ForeignKey("ParentId")]
        public ICollection<CourseGroup>? CourseGroups { get; set; }

        [InverseProperty("CourseGroup")]
        public ICollection<Course>? Courses { get; set; }

        [InverseProperty("SubGroup")]
        public ICollection<Course>? SubGroup { get; set; }


    }
}
