using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities.Order
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public int? UserId { get; set; }

        [Required]
        public int OrderSum { get; set; }

        public bool IsFainaly { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }


        //navigation property
        public User.User? User { get; set; }
        public ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
