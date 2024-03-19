using Core.Services.Interfaces;
using DataLayer.Context;
using DataLayer.Entities.Course;
using DataLayer.Entities.Order;
using DataLayer.Entities.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class OrderService : IOrderService
    {
        private DataBaseContext _context;

        public OrderService(DataBaseContext context)
        {
            _context = context;
        }

        public int AddOrder(int courseId, string userName)
        {
            int userId = _context.Users.Single(u => u.UserName == userName).UserId;

            Order order = _context.Orders
                .SingleOrDefault(o => o.UserId == userId && !o.IsFainaly);

            Course course = _context.Courses.Find(courseId);

            if (course != null)
            {
                if (order != null)
                {
                    OrderDetail orderDetail = _context.OrderDetails
                        .SingleOrDefault(o => o.OrderId == order.OrderId && o.CourseId == courseId);

                    if (orderDetail == null)
                    {
                        _context.OrderDetails.Add(new OrderDetail()
                        {
                            OrderId = order.OrderId,
                            CourseId = course.CourseId,
                            Price = course.CoursePrice,
                        });
                        _context.SaveChanges();
                    }
                }
                else
                {
                    order = new Order()
                    {
                        UserId = userId,
                        CreateDate = DateTime.Now,
                        IsFainaly = false,
                        OrderSum = course.CoursePrice,
                        OrderDetails = new List<OrderDetail>()
                    {
                        new OrderDetail()
                        {
                            CourseId = course.CourseId,
                            Price = course.CoursePrice,
                        }
                    }
                    };

                    _context.Add(order);
                    _context.SaveChanges();
                }
            }
            return order.OrderId;
        }

        public void DeleteCourseFromOrder(int orderId, int courseId, string userName)
        {
            int userId = _context.Users.Single(u => u.UserName == userName).UserId;

            Order order = _context.Orders.Include(o => o.OrderDetails)
               .Single(o => o.UserId == userId && !o.IsFainaly);

            var detail = order.OrderDetails
                .SingleOrDefault(od => od.OrderId == orderId && od.CourseId == courseId);
            if (detail != null)
            {
                _context.OrderDetails.Remove(detail);
                _context.SaveChanges();
            }
        }

        public void FinalyOrder(int orderId, string userName)
        {
            int userId = _context.Users.Single(u => u.UserName == userName).UserId;

            Order order = _context.Orders.Include(o => o.OrderDetails)
                .Single(o => o.OrderId == orderId && !o.IsFainaly && o.UserId == userId);

            if (order != null && order.OrderDetails != null)
            {
                foreach (var item in order.OrderDetails)
                {
                    _context.UserCourses.Add(new DataLayer.Entities.User.UserCourses()
                    {
                        CourseId = item.CourseId,
                        UserId = userId
                    });
                }

                order.IsFainaly = true;
                _context.Update(order);

                _context.SaveChanges();
            }
        }

        public Order GetOrderById(int orderId)
        {
            return _context.Orders.Find(orderId);
        }

        public Tuple<List<Order>, int> GetOrdersForUser(string userName, int pageId = 1)
        {
            int userId = _context.Users.Single(u => u.UserName == userName).UserId;

            IQueryable<Order> result = _context.Orders.Where(o => o.UserId == userId).Include(o => o.User);



            int take = 4;
            int skip = (pageId - 1) * take;

            int pageCount = result.Count() / take;

            return Tuple.Create(result.Skip(skip).Take(take)
                .OrderByDescending(o => o.CreateDate).ToList(), pageCount);
        }

        public int UpdateOrderPrice(int orderId)
        {
            var order = _context.Orders.Single(o => o.OrderId == orderId);

            order.OrderSum = _context.OrderDetails
                .Where(o => o.OrderId == order.OrderId)
                .Sum(o => o.Price);

            _context.Orders.Update(order);
            _context.SaveChanges();

            return order.OrderSum;
        }
    }
}
