using DataLayer.Entities.Course;
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
    }
}
