using DataLayer.Entities.Course;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities.User
{
    public class User
    {
        public User()
        {
            
        }

        [Key]
        public int UserId { get; set; }

        [Display(Name = "نقش")]
        [Required]
        public int RoleId { get; set; }

        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(120, ErrorMessage = "فیلد {0} نمی تواند بیش از {1} کرکتر باشد")]
        public string UserName { get; set; }
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [EmailAddress(ErrorMessage ="لطفا یک ایمیل معتبر وارد کنید")]
        public string Email { get; set; }
        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(120, ErrorMessage = "فیلد {0} نمی تواند بیش از {1} کرکتر باشد")]
        public string Password { get; set; }

        [Display(Name = "شماره تماس")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "کد فعالسازی")]
        [Required]
        public string ActiveCode { get; set; }

        [Display(Name = "وضعیت حساب")]
        [Required]
        public bool IsActive { get; set; }

        [Display(Name = "آواتار")]
        public string UserAvatar { get; set; }

        [Display(Name = "تاریخ ثبت نام")]
        [Required]
        public DateTime CreateDate { get; set; }

        [Display(Name = "بن شده؟")]
        [Required]
        public bool IsBanned { get; set; }


        #region Relations
        public ICollection<Course.Course> Courses { get; set; }
        public ICollection<CourseComment>? CourseComments { get; set; }
        public Role Role { get; set; }
        #endregion
    }
}
