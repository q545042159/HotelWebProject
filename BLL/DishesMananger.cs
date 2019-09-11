using DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class DishesMananger
    {
        private DishesService dishesService = new DishesService();


        /// <summary>
        /// 发布菜品
        /// </summary>
        /// <param name="dishes"></param>
        /// <returns></returns>
        public int AddDishes(Dishes dishes)
        {
            return dishesService.AddDishes(dishes);
        }
        /// <summary>
        /// 修改菜品
        /// </summary>
        /// <param name="dishes"></param>
        /// <returns></returns>
        public int ModifyDishes(Dishes dishes)
        {
            return dishesService.ModifyDishes(dishes);
        }
        /// <summary>
        /// 删除菜品
        /// </summary>
        /// <param name="dishesId"></param>
        /// <returns></returns>
        public int DeleteDishes(int dishesId)
        {
            return dishesService.DeleteDishes(dishesId);
        }
        /// <summary>
        /// 获取所有的菜品分类
        /// </summary>
        /// <returns></returns>
        public List<DishesCategory> GetAllDishesCategory()
        {
            return dishesService.GetAllDishesCategory();
        }
        /// <summary>
        /// 根据Id获取实体对象
        /// </summary>
        /// <param name="dishesId"></param>
        /// <returns></returns>
        public Dishes GetDishesById(int dishesId)
        {
            return dishesService.GetDishesById(dishesId);
        }       
        /// <summary>
        /// 获取所有的菜品列表
        /// </summary>
        /// <returns></returns>
        public List<Dishes> GetAllDishes(int categoryId = 0)
        {
            return dishesService.GetAllDishes(categoryId);
        }
        /// <summary>
        /// 根据菜品ID获取菜品名称
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public string GetCategoryName(int categoryId)
        {
            return dishesService.GetCategoryName(categoryId);
        }       
    }
}
