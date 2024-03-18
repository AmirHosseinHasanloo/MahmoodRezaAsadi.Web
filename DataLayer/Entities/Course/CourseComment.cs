using DataLayer.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities.Course
{
    public class CourseComment
    {
        [Key]
        public int CommentId { get; set; }

        public int? CourseId { get; set; }

        public int? UserId { get; set; }

        [Display(Name = "نظر ارزشمند شما")]
        [Required(ErrorMessage = "لطفا نظر ارزشمند خود را وارد کنید")]
        [MaxLength(120, ErrorMessage = "فیلد {0} نمی تواند بیش از {1} کرکتر باشد")]
        public string Comment { get; set; }
        public int? ParentId { get; set; }

        [Display(Name ="تاییده شده؟")]
        public bool IsAccepted { get; set; } = false;

        [Display(Name ="تاریخ ثبت نظر")]
        public DateTime CreateDate { get; set; }

        //navigation property

        public Course? Course { get; set; }
        public User.User? User { get; set; }

    }
}
