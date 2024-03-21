using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities.Course
{
    public class EpisodeType
    {
        [Key]
        public int TypeId { get; set; }

        [Required]
        [MaxLength(100)]
        public string TypeTitle { get; set; }

        //navigation property
        public ICollection<CourseEpisode> CourseEpisodes { get; set; }
    }
}
