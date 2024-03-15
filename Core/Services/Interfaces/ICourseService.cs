using Core.DTOs;
using DataLayer.Entities.Course;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface ICourseService
    {
        #region CourseGroup
        List<CourseGroup> GetCourseGroups();
        CourseGroup GetCourseGroupById(int id);

        void DeleteCourseGroupById(int id);
        void AddGroup(CourseGroup courseGroup);
        void EditGroup(CourseGroup courseGroup);
        #endregion


        #region Working with files
        string SaveFile(IFormFile formFile, string path, string subPath, string? episodeName);
        void SaveThumbnailFile(string imagePath, string fileName);
        string GenerateFileNameWithFilePath(IFormFile file);

        #endregion


        #region Course

        #region Get
        Course GetCourseById(int id);
        Course GetCourseByIdForClientSide(int id);
        Tuple<List<CourseCardViewModel>, int> ShowCourse(int pageId = 1, string filter = "", string getType = "all"
           , string orderByType = "date", int startPrice = 0, int endPrice = 0, List<int> selectedGroups = null, int take = 0);
        DeleteCourseAdminViewModel GetCourseForDeleteInAdminPanel(int courseId);
        void DeleteCourseById(int courseId);
        List<SelectListItem> GetCourseGroupsForAdminPanel();
        List<SelectListItem> GetSubGroupsForAdminPanel(int groupId);
        List<SelectListItem> GetAllCourseStatuses();
        CourseListForAdminPanelViewModel GetAllCourseForAdminPanel(int pageId = 1, string filterName = "");
        #endregion


        #region Create & edit
        void AddCourse(Course course, IFormFile courseImage, IFormFile courseDemo);
        void UpdateCourse(Course course, IFormFile courseImage, IFormFile courseDemo);
        #endregion

        #endregion
    }
}
