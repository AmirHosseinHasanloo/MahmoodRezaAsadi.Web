using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities.Order
{
    public class OrderDetail
    {
        [Key]
        public int DetailId { get; set; }

        [Required]
        public int? OrderId { get; set; }

        [Required]
        public int CourseId { get; set; }
        [Required]
        public int Price { get; set; }

        //navigation property
        public Order? Order { get; set; }
        public Course.Course? Course { get; set; }
    }
}
