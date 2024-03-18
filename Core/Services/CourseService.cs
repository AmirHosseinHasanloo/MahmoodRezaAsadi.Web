using Core.Convertors;
using Core.DTOs;
using Core.Security;
using Core.Services.Interfaces;
using DataLayer.Context;
using DataLayer.Entities.Course;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.Generators;

namespace Core.Services
{
    public class CourseService : ICourseService
    {
        private DataBaseContext _context;

        public CourseService(DataBaseContext context)
        {
            _context = context;
        }

        public void AcceptComment(int commentId)
        {
            var comment = _context.CourseComment.Find(commentId);

            comment.IsAccepted = true;
            _context.SaveChanges();
        }

        public void AddComment(CourseComment comment, string userName)
        {
            comment.CreateDate = DateTime.Now;
            comment.UserId = _context.Users.Single(u => u.UserName == userName).UserId;
            comment.IsAccepted = false;

            _context.Add(comment);
            _context.SaveChanges();
        }

        public void AddCourse(Course course, IFormFile courseImage, IFormFile courseDemo)
        {
            if (courseImage != null && courseImage.IsImage())
            {
                course.CourseImageName = SaveFile(courseImage, "CourseRoot", "Images");
            }

            if (courseDemo != null)
            {
                course.DemoFileName = SaveFile(courseDemo, "CourseRoot", "CourseDemos");
            }

            _context.Add(course);
            _context.SaveChanges();
        }

        public void AddEpisode(IFormFile video, CourseEpisode episode)
        {
            episode.episodeFileName = video.FileName;
            SaveFile(video, "CourseRoot", "CourseEpisodes", video.FileName);

            _context.Add(episode);
            _context.SaveChanges();
        }

        public void AddGroup(CourseGroup courseGroup)
        {
            _context.Add(courseGroup);
            _context.SaveChanges();
        }

        public void DeleteCourseById(int courseId)
        {
            var course = _context.Courses.Find(courseId);

            string imagePath = Path.Combine(Directory.GetCurrentDirectory()
                   , "wwwroot/CourseRoot/Images", course.CourseImageName);

            if (course.CourseImageName != "course_no_image.png")
            {
                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }

                string thumbPath = Path.Combine(Directory.GetCurrentDirectory()
               , "wwwroot/CourseRoot/ThumbImages", course.CourseImageName);

                if (File.Exists(thumbPath))
                {
                    File.Delete(thumbPath);
                }
            }


            string demoPath = Path.Combine(Directory.GetCurrentDirectory()
                 , "wwwroot/CourseRoot/CourseDemos", course.DemoFileName);

            if (course.DemoFileName != null)
            {
                if (File.Exists(demoPath))
                {
                    File.Delete(demoPath);
                }
            }


            _context.Entry(course).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public void DeleteCourseGroupById(int id)
        {
            var group = _context.CourseGroups.Find(id);
            _context.CourseGroups.Remove(group);
            _context.SaveChanges();
        }

        public void DeleteEpisodeById(int episodeId)
        {
            var episode = _context.CourseEpisodes.Find(episodeId);

            string episodePath = Path.Combine(Directory.GetCurrentDirectory()
                     , "wwwroot/CourseRoot/CourseEpisodes/" + episode.episodeFileName);

            if (File.Exists(episodePath))
            {
                File.Delete(episodePath);
            }

            _context.Remove(episode);
            _context.SaveChanges();
        }

        public void EditGroup(CourseGroup courseGroup)
        {
            _context.CourseGroups.Update(courseGroup);
            _context.SaveChanges();
        }

        public string GenerateFileNameWithFilePath(IFormFile file)
        {
            return NameGenerator.GenerateName() + Path.GetExtension(file.FileName);
        }

        public List<CourseComment> GetAllCommentsByCourseId(int courseId)
        {
            return _context.CourseComment.Include(c => c.User).Where(c => c.CourseId == courseId && !c.IsAccepted).ToList();
        }

        public CourseListForAdminPanelViewModel GetAllCourseForAdminPanel(int pageId = 1, string filterName = "")
        {
            IQueryable<Course> result = _context.Courses.Include(c => c.CourseEpisodes).Include(c => c.CourseComments);

            CourseListForAdminPanelViewModel viewModel = new CourseListForAdminPanelViewModel();


            if (filterName != "")
            {
                result = result.Where(c => c.CourseTitle.Contains(filterName));
            }

            //pagging system
            int take = 6;
            int skip = (pageId - 1) * take;

            viewModel.CurrentPage = pageId;
            viewModel.CourseCount = result.Count() / take;

            viewModel.Course = result
                .OrderBy(c => c.CreateDate).Skip(skip)
                .Take(take).Select(c => new CourseAdminPanel()
                {
                    CourseId = c.CourseId,
                    CourseName = c.CourseTitle,
                    ImageName = c.CourseImageName,
                    EposodeCount = c.CourseEpisodes.Count(),
                }).ToList();


            return viewModel;
        }

        public List<SelectListItem> GetAllCourseStatuses()
        {
            return _context.CourseStatuses.Select(cs => new SelectListItem
            {
                Text = cs.StatusTitle,
                Value = cs.StatusId.ToString()
            }).ToList();
        }

        public Tuple<List<CourseComment>, int> GetCommentForCourseByCourseId(int courseId, int pageId = 1)
        {
            int take = 30;
            int skip = (pageId - 1) * take;

            int pageCount = _context.CourseComment.Where(c => c.CourseId == courseId && c.IsAccepted).Count() / take;

            if (pageCount % 2 != 0)
            {
                pageCount += 1;
            }

            return Tuple.Create(_context.CourseComment.Include(c => c.User).Where(c => c.CourseId == courseId && c.IsAccepted)
                .Skip(skip).Take(take).OrderByDescending(c => c.CreateDate).ToList(), pageCount);
        }

        public Course GetCourseById(int id)
        {
            return _context.Courses.Find(id);
        }

        public Course GetCourseByIdForClientSide(int id)
        {
            return _context.Courses.Include(c => c.User).Include(c => c.CourseStatus)
                .Include(c => c.CourseGroup).Include(c => c.CourseEpisodes)
                .Include(c => c.CourseComments).
                Single(c => c.CourseId == id);
        }

        public DeleteEpisodeViewModel GetCourseEpisodeForDelete(int episodeId)
        {
            return _context.CourseEpisodes.Where(e => e.EpisodeId == episodeId)
                .Select(e => new DeleteEpisodeViewModel()
                {
                    CourseId = Convert.ToInt32(e.CourseId),
                    EpisodeId = e.EpisodeId,
                    EpisodeTitle = e.EpisodeTitle
                }).Single();
        }

        public DeleteCourseAdminViewModel GetCourseForDeleteInAdminPanel(int courseId)
        {
            return _context.Courses.Where(c => c.CourseId == courseId)
                .Select(c => new DeleteCourseAdminViewModel()
                {
                    CourseId = c.CourseId,
                    Title = c.CourseTitle
                }).Single();
        }

        public CourseGroup GetCourseGroupById(int id)
        {
            return _context.CourseGroups.Find(id);
        }

        public List<CourseGroup> GetCourseGroups()
        {
            return _context.CourseGroups.ToList();
        }

        public List<SelectListItem> GetCourseGroupsForAdminPanel()
        {
            return _context.CourseGroups.Where(g => g.ParentId == null)
                 .Select(g => new SelectListItem()
                 {
                     Text = g.GroupTitle,
                     Value = g.GroupId.ToString(),
                 }).ToList();
        }

        public CourseEpisode GetEpisodeByEpisodeId(int episodeId)
        {
            return _context.CourseEpisodes.Find(episodeId);
        }

        public Tuple<List<CourseEpisode>, int> GetEpisodesByCourseId(int courseId, int pageId = 1, string filter = "")
        {
            IQueryable<CourseEpisode> result = _context.CourseEpisodes
                .Where(ep => ep.CourseId == courseId);

            int take = 10;
            int skip = (pageId - 1) * take;

            if (!string.IsNullOrEmpty(filter))
            {
                result = result.Where(ep => ep.EpisodeTitle.Contains(filter));
            }

            int pageCount = result.Count() / take;


            return Tuple.Create(result.Skip(skip).Take(take).ToList(), pageCount);
        }

        public List<SelectListItem> GetSubGroupsForAdminPanel(int groupId)
        {
            return _context.CourseGroups.Where(g => g.ParentId == groupId)
                  .Select(g => new SelectListItem()
                  {
                      Text = g.GroupTitle,
                      Value = g.GroupId.ToString()
                  }).ToList();
        }

        public bool IsEpisodeExists(string episodeName)
        {
            return _context.CourseEpisodes.Any(c => c.episodeFileName == episodeName);
        }

        public void RejectComment(int commentId)
        {
            var comment = _context.CourseComment.Find(commentId);

            _context.CourseComment.Remove(comment);
            _context.SaveChanges();
        }

        public string SaveFile(IFormFile formFile, string path, string subPath, string? episodeName = "")
        {
            string fileName = GenerateFileNameWithFilePath(formFile);

            string filePath = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot" + "/" + path + "/" + subPath, fileName);

            if (subPath == "CourseEpisodes" && episodeName != "")
            {
                // episodes dont need rename !
                string episodePath = Path.Combine(Directory.GetCurrentDirectory(),
             "wwwroot" + "/" + path + "/" + subPath, episodeName);

                using (var stream = new FileStream(episodePath, FileMode.Create))
                {
                    formFile.CopyTo(stream);
                }
            }

            if (subPath != "CourseEpisodes")
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    formFile.CopyTo(stream);
                }
            }

            if (subPath == "Images")
            {
                // Resize Image and make thumbnail and save it
                SaveThumbnailFile(filePath, fileName);
            }

            return fileName;
        }

        public void SaveThumbnailFile(string imagePath, string fileName)
        {
            ImageConvertor imgResizer = new ImageConvertor();


            string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot",
                "CourseRoot/ThumbImages", fileName);

            imgResizer.Image_resize(imagePath, thumbPath, 400);
        }

        public Tuple<List<CourseCardViewModel>, int> ShowCourse(int pageId = 1, string filter = "", string getType = "all"
            , string orderByType = "date", int startPrice = 0, int endPrice = 0, List<int> selectedGroups = null, int take = 0)
        {
            if (take == 0)
            {
                take = 9;
            }


            IQueryable<Course> result = _context.Courses.Include(c => c.CourseEpisodes);

            if (!string.IsNullOrEmpty(filter))
            {
                result = result.Where(c => c.CourseTitle.Contains(filter) || c.Tags.Contains(filter));
            }

            switch (getType)
            {
                case "all":
                    {
                        break;
                    }

                case "priceLess":
                    {
                        result = result.Where(c => c.CoursePrice == 0);
                        break;
                    }

                case "havePrice":
                    {
                        result = result.Where(c => c.CoursePrice != 0);
                        break;
                    }

                default:
                    break;
            }


            switch (orderByType)
            {
                case "date":
                    {
                        result = result.OrderBy(c => c.CreateDate);
                        break;
                    }

                case "updateDate":
                    {
                        result = result.OrderBy(c => c.UpdateDate);
                        break;
                    }

                default:
                    break;
            }

            if (startPrice > 0)
            {
                result = result.Where(c => c.CoursePrice > startPrice);
            }

            if (endPrice > 0)
            {
                result = result.Where(c => c.CoursePrice < endPrice);
            }

            if (selectedGroups != null && selectedGroups.Any())
            {
                foreach (var groupId in selectedGroups)
                {
                    result = result.Where(c => c.GroupId == groupId || c.SubGroupId == groupId);
                }
            }

            int skip = (pageId - 1) * take;




            int pageCount = result.AsNoTracking().AsEnumerable()
                .Select(c => new CourseCardViewModel()
                {
                    CourseId = c.CourseId,
                    CourseTitle = c.CourseTitle,
                    ImageName = c.CourseImageName,
                    Price = c.CoursePrice,
                    TotalTime = new TimeSpan((c.CourseEpisodes.Sum(e => e.EpisodeTime.Ticks) == 0)
                    ? 0 : c.CourseEpisodes.Sum(e => e.EpisodeTime.Ticks))
                }).Count() / take;


            var query = result.AsNoTracking().AsEnumerable()
                .Select(c => new CourseCardViewModel()
                {
                    CourseId = c.CourseId,
                    CourseTitle = c.CourseTitle,
                    ImageName = c.CourseImageName,
                    Price = c.CoursePrice,
                    TotalTime = new TimeSpan((c.CourseEpisodes.Sum(e => e.EpisodeTime.Ticks) == 0)
                    ? 0 : c.CourseEpisodes.Sum(e => e.EpisodeTime.Ticks))
                }).Skip(skip).Take(take).ToList();

            return Tuple.Create(query, pageCount);
        }

        public void UpdateCourse(Course course, IFormFile courseImage, IFormFile courseDemo)
        {
            course.UpdateDate = DateTime.Now;

            if (courseImage != null && courseImage.IsImage())
            {
                string imagePath = Path.Combine(Directory.GetCurrentDirectory()
                    , "wwwroot/CourseRoot/Images", course.CourseImageName);

                if (course.CourseImageName != "course_no_image.png")
                {
                    if (File.Exists(imagePath))
                    {
                        File.Delete(imagePath);
                    }

                    string thumbPath = Path.Combine(Directory.GetCurrentDirectory()
                   , "wwwroot/CourseRoot/ThumbImages", course.CourseImageName);

                    if (File.Exists(thumbPath))
                    {
                        File.Delete(thumbPath);
                    }
                }

                // save new image + make thumbnail and save it
                course.CourseImageName = SaveFile(courseImage, "CourseRoot", "Images");
            }

            if (courseDemo != null)
            {
                string demoPath = Path.Combine(Directory.GetCurrentDirectory()
                   , "wwwroot/CourseRoot/CourseDemos", course.DemoFileName);

                if (course.DemoFileName != null)
                {
                    if (File.Exists(demoPath))
                    {
                        File.Delete(demoPath);
                    }
                }
                course.DemoFileName = SaveFile(courseDemo, "CourseRoot", "CourseDemos");
            }

            _context.Courses.Update(course);
            _context.SaveChanges();
        }

        public void UpdateEpisode(IFormFile video, CourseEpisode episode)
        {

            if (video != null)
            {
                string episodePath = Path.Combine(Directory.GetCurrentDirectory()
                    , "wwwroot/CourseRoot/CourseEpisodes/" + episode.episodeFileName);

                if (File.Exists(episodePath))
                {
                    File.Delete(episodePath);
                }

                episode.episodeFileName = video.FileName;
                SaveFile(video, "CourseRoot", "CourseEpisodes", video.FileName);
            }

            _context.Update(episode);
            _context.SaveChanges();
        }
    }
}
