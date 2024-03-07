using Core.Services.Interfaces;
using DataLayer.Context;
using DataLayer.Entities.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class CourseService : ICourseService
    {
        private DataBaseContext _context;

        public CourseService(DataBaseContext context)
        {
            _context = context;
        }

        public void AddGroup(CourseGroup courseGroup)
        {
           _context.Add(courseGroup);
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

        public CourseGroup GetCourseGroupById(int id)
        {
            return _context.CourseGroups.Find(id);
        }

        public List<CourseGroup> GetCourseGroups()
        {
            return _context.CourseGroups.ToList();
        }
    }
}
