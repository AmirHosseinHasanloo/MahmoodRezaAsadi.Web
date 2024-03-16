using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities.Course
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        [Required]
        public int GroupId { get; set; }

        public int? SubGroupId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int StatusId { get; set; }

        [Display(Name = "نام دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(350, ErrorMessage = "فیلد {0} نمی تواند بیش از {1} کرکتر باشد")]
        public string CourseTitle { get; set; }

        [Display(Name = "توضیحات مرتبط با دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string CourseDescription { get; set; }

        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int CoursePrice { get; set; }

        [Display(Name = "کلمات کلیدی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(600, ErrorMessage = "فیلد {0} نمی تواند بیش از {1} کرکتر باشد")]
        public string Tags { get; set; }

        [MaxLength(50)]
        public string? CourseImageName { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        [MaxLength(100)]
        public string? DemoFileName { get; set; }


        [Display(Name = "توضیحات متا")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "فیلد {0} نمی تواند بیش از {1} کرکتر باشد")]
        public string MetaDescription { get; set; }


        // navigation properties

        [ForeignKey("UserId")]
        public User.User? User { get; set; }

        [ForeignKey("GroupId")]
        public CourseGroup? CourseGroup { get; set; }

        [ForeignKey("SubGroupId")]
        public CourseGroup? SubGroup { get; set; }

        [ForeignKey("StatusId")]
        public CourseStatus? CourseStatus { get; set; }

        public ICollection<CourseEpisode>? CourseEpisodes { get; set; }
    }
}
