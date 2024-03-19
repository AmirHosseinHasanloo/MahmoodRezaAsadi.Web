using DataLayer.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface IOrderService
    {
        int AddOrder(int courseId, string userName);
        Order GetOrderById(int orderId);
        void DeleteCourseFromOrder(int orderId, int courseId, string userName);
        int UpdateOrderPrice(int orderId);
        void FinalyOrder(int orderId, string userName);
        Tuple<List<Order>, int> GetOrdersForUser(string userName, int pageId = 1);

    }
}
