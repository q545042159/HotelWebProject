using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;

namespace DAL
{
    /// <summary>
    /// 菜品操作数据访问类
    /// </summary>
    public class DishesService
    {
        /// <summary>
        /// 发布菜品
        /// </summary>
        /// <param name="dishes"></param>
        /// <returns></returns>
        public int AddDishes(Dishes dishes)
        {
            //编写后台的时候，使用的方法
            //using (HotelDBEntities db = new HotelDBEntities())
            //{
            //    db.Dishes.Add(dishes);
            //    return db.SaveChanges();
            //}

            //优化后：能返回标识列的值
            using (HotelDBEntities db = new HotelDBEntities())
            {
                db.Dishes.Add(dishes);
                db.SaveChanges();
                return dishes.DishesId;//返回标识列的值
            }
        }
        /// <summary>
        /// 修改菜品（当前是全部修改）
        /// </summary>
        /// <param name="dishes"></param>
        /// <returns></returns>
        public int ModifyDishes(Dishes dishes)
        {
            using (HotelDBEntities db = new HotelDBEntities())
            {
                // db.Dishes.Attach(dishes);
                db.Entry<Dishes>(dishes).State = EntityState.Modified;
                return db.SaveChanges();
            }
        }
        /// <summary>
        /// 删除菜品
        /// </summary>
        /// <param name="dishesId"></param>
        /// <returns></returns>
        public int DeleteDishes(int dishesId)
        {
            using (HotelDBEntities db = new HotelDBEntities())
            {
                Dishes dishes = new Dishes { DishesId = dishesId };
                db.Dishes.Attach(dishes);
                db.Dishes.Remove(dishes);
                return db.SaveChanges();
            }
        }
        /// <summary>
        /// 获取菜品列表（根据分类编号）
        /// </summary>
        /// <param name="categoryId">有默认值的参数，调用的时候可以不写</param>
        /// <returns></returns>
        public List<Dishes> GetAllDishes(int categoryId = 0)
        {
            #region 前面的写法
            //using (HotelDBEntities db = new HotelDBEntities())
            //{
            //    if (categoryId == 0)
            //        return (from d in db.Dishes select d).ToList();
            //    else
            //        return (from d in db.Dishes where d.CategoryId == categoryId select d).ToList();
            //}
            #endregion

            //优化后，通过关联对象查询的写法
            using (HotelDBEntities db = new HotelDBEntities())
            {
                //需要查询关联对象
                var dishesList = from d in db.Dishes
                                 select new
                                 {
                                     d.DishesId,
                                     d.DishesName,
                                     d.UnitPrice,
                                     d.CategoryId,
                                     d.DishesCategory.CategoryName
                                 };
                if (categoryId != 0)
                {
                    dishesList = from d in dishesList where d.CategoryId == categoryId select d;
                }
                //转换成具体对象
                List<Dishes> list = new List<Dishes>();
                foreach (var item in dishesList)
                {
                    list.Add(new Dishes
                    {
                        DishesId = item.DishesId,
                        DishesName = item.DishesName,
                        UnitPrice = item.UnitPrice,
                        CategoryId = item.CategoryId,
                        DishesCategory = new DishesCategory { CategoryName = item.CategoryName }
                    });
                }
                return list;
            }
        }
        /// <summary>
        /// 获取所有的菜品分类（下拉框填充使用）
        /// </summary>
        /// <returns></returns>
        public List<DishesCategory> GetAllDishesCategory()
        {
            using (HotelDBEntities db = new HotelDBEntities())
            {
                return (from c in db.DishesCategory select c).ToList();
            }
        }
        /// <summary>
        /// 根据菜品分类ID查询分类名称
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public string GetCategoryName(int categoryId)
        {
            using (HotelDBEntities db = new HotelDBEntities())
            {
                return (from c in db.DishesCategory
                        where c.CategoryId == categoryId
                        select c.CategoryName).FirstOrDefault();
            }
        }
        /// <summary>
        /// 根据菜品ID获取菜品对象
        /// </summary>
        /// <param name="dishesId"></param>
        /// <returns></returns>
        public Dishes GetDishesById(int dishesId)
        {
            using (HotelDBEntities db = new HotelDBEntities())
            {
                return (from d in db.Dishes where d.DishesId == dishesId select d).FirstOrDefault();
            }
        }
    }
}
