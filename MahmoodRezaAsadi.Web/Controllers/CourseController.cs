using Core.Services.Interfaces;
using DataLayer.Entities.Course;
using Microsoft.AspNetCore.Mvc;
using SharpCompress.Archives;
using System.IO.Compression;

namespace MahmoodRezaAsadi.Web.Controllers
{
    public class CourseController : Controller
    {
        private ICourseService _courseService;
        private IUserService _userService;
        public CourseController(ICourseService courseService, IUserService userService)
        {
            _courseService = courseService;
            _userService = userService;
        }


        [Route("ShowCourse/{id}")]
        public IActionResult Index(int id, int episode = 0)
        {
            var course = _courseService.GetCourseByIdForClientSide(id);

            if (course == null)
            {
                return BadRequest();
            }

            if (course.CoursePrice == 0)
            {
                ViewBag.IsFree = true;
            }

            ViewBag.CourseTime = new TimeSpan((course.CourseEpisodes.Sum(e => e.EpisodeTime.Ticks) == 0) ?
           0 : course.CourseEpisodes.Sum(e => e.EpisodeTime.Ticks));

            return View(course);
        }

        [Route("DownloadFile/{episodeId}")]
        public IActionResult DownloadFile(int episodeId)
        {
            var episode = _courseService.GetEpisodeByEpisodeId(episodeId);

            string fileName = episode.episodeFileName;
            string filePath = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot/CourseRoot/CourseEpisodes", fileName);

            if (episode.IsFree)
            {
                byte[] file = System.IO.File.ReadAllBytes(filePath);
                return File(file, "application/force-download", fileName);
            }

            if (User.Identity.IsAuthenticated)
            {
                if (_userService.IsUserBuyedCourse((int)episode.CourseId, User.Identity.Name))
                {
                    byte[] file = System.IO.File.ReadAllBytes(filePath);
                    return File(file, "application/force-download", fileName);
                }
            }

            return Forbid();
        }


        public IActionResult ShowEpisode(int id, int episode)
        {
            var course = _courseService.GetCourseByIdForClientSide(id);

            ViewBag.ImageName = course.CourseImageName;

            if (episode != 0 && User.Identity.IsAuthenticated)
            {
                if (course.CourseEpisodes.All(e => e.EpisodeId != episode))
                {
                    return BadRequest();
                }


                if (!course.CourseEpisodes.First(e => e.EpisodeId == episode).IsFree)
                {
                    if (!_userService.IsUserBuyedCourse(course.CourseId, User.Identity.Name))
                    {
                        return BadRequest();
                    }
                }

                var courseEpisode = course.CourseEpisodes.Single(e => e.EpisodeId == episode);

                string filePath = "";
                string checkFilePath = "";


                // typeId = 1 is video .mp4
                if (courseEpisode.IsFree && courseEpisode.TypeId == 1)
                {
                    filePath = "/FreeEpisodes/" + courseEpisode.episodeFileName.Replace(".rar", ".mp4");
                    checkFilePath = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot/FreeEpisodes", courseEpisode.episodeFileName.Replace(".rar", ".mp4"));
                }
                else if (!courseEpisode.IsFree && courseEpisode.TypeId == 1)
                {
                    filePath = "/Episodes/" + courseEpisode.episodeFileName.Replace(".rar", ".mp4");
                    checkFilePath = Path.Combine(Directory.GetCurrentDirectory(),
        "wwwroot/Episodes", courseEpisode.episodeFileName.Replace(".rar", ".mp4"));
                }


                // typeId = 2 is mp3 voice
                if (courseEpisode.IsFree && courseEpisode.TypeId == 2)
                {
                    filePath = "/FreeEpisodes/" + courseEpisode.episodeFileName.Replace(".rar", ".mp3");
                    checkFilePath = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot/FreeEpisodes", courseEpisode.episodeFileName.Replace(".rar", ".mp3"));
                }
                else if (!courseEpisode.IsFree && courseEpisode.TypeId == 2)
                {
                    filePath = "/Episodes/" + courseEpisode.episodeFileName.Replace(".rar", ".mp3");
                    checkFilePath = Path.Combine(Directory.GetCurrentDirectory(),
        "wwwroot/Episodes", courseEpisode.episodeFileName.Replace(".rar", ".mp3"));
                }

                if (!System.IO.File.Exists(checkFilePath))
                {
                    string targetPass = Directory.GetCurrentDirectory();


                    var rarPath = Path.Combine(Directory.GetCurrentDirectory(),
                      "wwwroot/CourseRoot/CourseEpisodes", courseEpisode.episodeFileName);

                    // typeId = 1 is video
                    if (courseEpisode.IsFree && courseEpisode.TypeId == 1)
                    {
                        targetPass = System.IO.Path.Combine(targetPass, "wwwroot/FreeEpisodes");

                        _courseService.UnZipFile(rarPath, targetPass, courseEpisode.EpisodeId, ".mp4");
                    }
                    else if (!courseEpisode.IsFree && courseEpisode.TypeId == 1)
                    {
                        targetPass = System.IO.Path.Combine(targetPass, "wwwroot/Episodes");
                        _courseService.UnZipFile(rarPath, targetPass, courseEpisode.EpisodeId, ".mp4");
                    }


                    // typeId = 2 is mp3
                    if (courseEpisode.IsFree && courseEpisode.TypeId == 2)
                    {
                        targetPass = System.IO.Path.Combine(targetPass, "wwwroot/FreeEpisodes");

                        _courseService.UnZipFile(rarPath, targetPass, courseEpisode.EpisodeId, ".mp3");
                    }
                    else if (!courseEpisode.IsFree && courseEpisode.TypeId == 2)
                    {
                        targetPass = System.IO.Path.Combine(targetPass, "wwwroot/Episodes");
                        _courseService.UnZipFile(rarPath, targetPass, courseEpisode.EpisodeId, ".mp3");
                    }
                }
                ViewBag.EpisodeFilePath = filePath;

                return PartialView(courseEpisode);
            }

            return null;
        }


        [HttpPost]
        public IActionResult CreateComment(CourseComment comment)
        {
            _courseService.AddComment(comment, User.Identity.Name);
            return PartialView("ShowComment", _courseService.GetCommentForCourseByCourseId(comment.CourseId.Value));
        }

        public IActionResult ShowComment(int id, int pageId = 1)
        {
            return PartialView(_courseService.GetCommentForCourseByCourseId(id, pageId));
        }

        public IActionResult Filter(int pageId = 1, string filter = "", string getType = "all"
           , string orderByType = "date", int startPrice = 0, int endPrice = 0, List<int> selectedGroups = null, int take = 6)
        {
            ViewBag.courseGroups = _courseService.GetCourseGroups();
            ViewBag.selectedGroups = selectedGroups;
            ViewBag.pageId = pageId;

            return View(_courseService.ShowCourse(pageId, filter, getType, orderByType, startPrice, endPrice, selectedGroups, take));
        }




    }
}
