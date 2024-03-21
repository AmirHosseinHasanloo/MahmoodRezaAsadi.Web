using Core.DTOs;
using DataLayer.Entities.Course;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Query.Internal;
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


        #region Course Episode

        #region Create & Edit & delete
        void AddEpisode(IFormFile video, CourseEpisode episode);
        void UpdateEpisode(IFormFile video, CourseEpisode episode);
        void DeleteEpisodeById(int episodeId);
        #endregion


        #region Get

        CourseEpisode GetEpisodeByEpisodeId(int episodeId);
        DeleteEpisodeViewModel GetCourseEpisodeForDelete(int episodeId);
        List<SelectListItem> GetEpisodeTipes();
        Tuple<List<CourseEpisode>, int> GetEpisodesByCourseId(int courseId, int pageId = 1, string filter = "");
        #endregion

        #region security
        bool IsEpisodeExists(string episodeName);
        #endregion
        #endregion


        #region Course Comment
        void AddComment(CourseComment comment, string userName);
        void AcceptComment(int commentId);
        void RejectComment(int commentId);
        Tuple<List<CourseComment>, int> GetCommentForCourseByCourseId(int courseId, int pageId = 1);

        List<CourseComment> GetAllCommentsByCourseId(int courseId);

        #endregion

        #region Archivement

        void UnZipFile(string rarFilePath,string targetPass, int episodeId,string format);
        #endregion
    }
}
