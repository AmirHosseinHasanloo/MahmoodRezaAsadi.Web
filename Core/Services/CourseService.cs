using Core.Convertors;
using Core.DTOs;
using Core.Security;
using Core.Services.Interfaces;
using DataLayer.Context;
using DataLayer.Entities.Course;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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

        public void EditGroup(CourseGroup courseGroup)
        {
            _context.CourseGroups.Update(courseGroup);
            _context.SaveChanges();
        }

        public string GenerateFileNameWithFilePath(IFormFile file)
        {
            return NameGenerator.GenerateName() + Path.GetExtension(file.FileName);
        }

        public CourseListForAdminPanelViewModel GetAllCourseForAdminPanel(int pageId = 1, string filterName = "")
        {
            IQueryable<Course> result = _context.Courses;

            //TODO : Add Episode and show count of them
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
                    EposodeCount = 0
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

        public Course GetCourseById(int id)
        {
            return _context.Courses.Find(id);
        }

        public Course GetCourseByIdForClientSide(int id)
        {
            return _context.Courses.Include(c => c.User).Include(c => c.CourseStatus)
                .Include(c=>c.CourseGroup).
                Single(c => c.CourseId == id);
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

        public List<SelectListItem> GetSubGroupsForAdminPanel(int groupId)
        {
            return _context.CourseGroups.Where(g => g.ParentId == groupId)
                  .Select(g => new SelectListItem()
                  {
                      Text = g.GroupTitle,
                      Value = g.GroupId.ToString()
                  }).ToList();
        }

        public string SaveFile(IFormFile formFile, string path, string subPath, string? episodeName = "")
        {
            string fileName = GenerateFileNameWithFilePath(formFile);

            string filePath = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot" + "/" + path + "/" + subPath, fileName);

            if (subPath == "CourseEpisodes")
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


            IQueryable<Course> result = _context.Courses;

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

            //int pageCount = result.Include(c => c.CourseEpisodes)
            // .AsNoTracking().AsEnumerable()
            // .Select(c => new ShowCourseListItemViewModel()
            // {
            //     CourseId = c.CourseId,
            //     CourseTitle = c.CourseTitle,
            //     ImageName = c.CourseImageName,
            //     Price = c.CoursePrice,
            //     TotalTime = new TimeSpan((c.CourseEpisodes.Sum(e => e.EpisodeTime.Ticks) == 0)
            //     ? 0 : c.CourseEpisodes.Sum(e => e.EpisodeTime.Ticks))
            // }).Count() / take;


            int pageCount = result.AsNoTracking().AsEnumerable()
                .Select(c => new CourseCardViewModel()
                {
                    CourseId = c.CourseId,
                    CourseTitle = c.CourseTitle,
                    ImageName = c.CourseImageName,
                    Price = c.CoursePrice,
                    // Add Episode Time
                    TotalTime = new TimeSpan(0)
                }).Count() / take;


            var query = result.AsNoTracking().AsEnumerable()
                .Select(c => new CourseCardViewModel()
                {
                    CourseId = c.CourseId,
                    CourseTitle = c.CourseTitle,
                    ImageName = c.CourseImageName,
                    Price = c.CoursePrice,
                    // Add Episode Time
                    TotalTime = new TimeSpan(0)
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
    }
}
