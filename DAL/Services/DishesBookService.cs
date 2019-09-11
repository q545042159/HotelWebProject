using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Models;

namespace DAL
{
    /// <summary>
    /// 菜品预定管理
    /// </summary>
    public class DishesBookService
    {
        /// <summary>
        /// 客户提交订单
        /// </summary>
        /// <param name="dishesBook"></param>
        /// <returns></returns>
        public int Book(DishesBook dishesBook)
        {
            dishesBook.OrderStatus = 0;//数据库默认值不起作用，需要在这个地方设置
            using (HotelDBEntities db = new HotelDBEntities())
            {
                db.DishesBook.Add(dishesBook);
                return db.SaveChanges();
            }
        }
        /// <summary>
        /// 根据预定ID修改订单状态（审核通过、取消、关单-1）
        /// </summary>
        /// <param name="bookId"></param>
        /// <param name="orderStatus"></param>
        /// <returns></returns>
        public int ModifyBook(int bookId, string orderStatus)
        {
            //方式1：封装一个实体，所有字段都写上（最麻烦）
            //方式2：查询到要修改的实体，然后修改对应的值（有一个查询的过程）
            //方法3：创建一个对象-->给主键赋值-->附加到上下文-->给要修改的字段赋值-->提交保存
            using (HotelDBEntities db = new HotelDBEntities())
            {
                DishesBook dishesBook = new DishesBook();
                dishesBook.BookId = bookId;
                db.DishesBook.Attach(dishesBook);
                //请在下面写出要修改的字段，并赋值
                dishesBook.OrderStatus = Convert.ToInt32(orderStatus);
                return db.SaveChanges();
            }
        }
        /// <summary>
        /// 获取未审核和未关闭的订单
        /// </summary>
        /// <returns></returns>
        public List<DishesBook> GetAllDishesBook()
        {
            using (HotelDBEntities db = new HotelDBEntities())
            {
                return (from b in db.DishesBook
                        where b.OrderStatus == 0 || b.OrderStatus == 1
                        orderby b.BookTime descending
                        select b).ToList();
            }
        }

    }
}
