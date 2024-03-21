using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities.Course
{
    public class CourseEpisode
    {
        [Key]
        public int EpisodeId { get; set; }
        public int? CourseId { get; set; }
        public int? TypeId { get; set; }

        [Display(Name = "عنوان فصل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(120, ErrorMessage = "فیلد {0} نمی تواند بیش از {1} کرکتر باشد")]
        public string EpisodeTitle { get; set; }

        [Display(Name = "زمان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public TimeSpan EpisodeTime { get; set; }

        [Display(Name = "فایل")]
        public string? episodeFileName { get; set; }

        [Display(Name = "رایگان")]
        public bool IsFree { get; set; }


        // navigation property
        public Course? Course { get; set; }
        public EpisodeType? EpisodeType { get; set; }
    }
}
