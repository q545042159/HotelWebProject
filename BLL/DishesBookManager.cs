using DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class DishesBookManager
    {
        private DishesBookService dishBookService = new DishesBookService();

        /// <summary>
        /// 客户预定
        /// </summary>
        /// <param name="objDishBook"></param>
        /// <returns></returns>
        public int Book(DishesBook dishBook)
        {
            return dishBookService.Book(dishBook);
        }
        /// <summary>
        /// 根据预定id修改订单状态
        /// </summary>
        /// <param name="bookId"></param>
        /// <param name="orderStatus"></param>
        /// <returns></returns>
        public int ModifyBook(int bookId, string orderStatus)
        {
            return dishBookService.ModifyBook(bookId, orderStatus);
        }
        /// <summary>
        /// 获取未关闭的订单
        /// </summary>
        /// <returns></returns>
        public List<DishesBook> GetAllDishesBook()
        {
            return dishBookService.GetAllDishesBook();
        }
    }
}
