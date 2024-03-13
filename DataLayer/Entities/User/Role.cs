using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities.User
{
    public class Role
    {
        public Role()
        {
            
        }
        [Key]
        public int RoleId { get; set; }
        [Display(Name = "عنوان نقش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(120, ErrorMessage = "فیلد {0} نمی تواند بیش از {1} کرکتر باشد")]
        public string RoleTitle { get; set; }
        [Display(Name = "عنوان سیستمی نقش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(120, ErrorMessage = "فیلد {0} نمی تواند بیش از {1} کرکتر باشد")]
        public string RoleName { get; set; }


        #region Relations

        public ICollection<User> Users { get; set; }

        #endregion
    }
}
