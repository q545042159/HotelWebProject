using BLL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelWebProject.Areas.WebHotelManage.Controllers
{
    [Authorize]
    public class DishesController : Controller
    {
        #region 菜品管理：查询、删除

        /// <summary>
        /// 【1】菜品管理页面
        /// </summary>
        /// <returns></returns>
        public ActionResult DishesManager()
        {
            //获取所有的菜品集合
            List<Dishes> dishesList = new DishesMananger().GetAllDishes();
            ViewBag.dishesList = dishesList;
            //获取下拉列表的数据集合
            List<DishesCategory> categoryList = new DishesMananger().GetAllDishesCategory();
            SelectList slist = new SelectList(categoryList, "CategoryId", "CategoryName", categoryList[0].CategoryId);
            return View("DishesManager", slist);//通过强类型视图传递
        }
        /// <summary>
        /// 【2】根据分类查询菜品
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public ActionResult QueryDishes(int categoryId)
        {
            List<Dishes> dishesList = new DishesMananger().GetAllDishes(categoryId);
            ViewBag.dishesList = dishesList;
            List<DishesCategory> categoryList = new DishesMananger().GetAllDishesCategory();
            SelectList slist = new SelectList(categoryList, "CategoryId", "CategoryName", categoryId);
            return View("DishesManager", slist);
        }
        /// <summary>
        /// 【3】删除菜品对象
        /// </summary>
        /// <param name="dishesId"></param>
        /// <returns></returns>
        public ActionResult DeleteDishes(int dishesId)
        {
            DishesMananger dishesManager = new DishesMananger();
            Dishes dishes = dishesManager.GetDishesById(dishesId);
            string filePath = Server.MapPath("~/Images/dishes/" + dishes.DishesImg);
            int result = dishesManager.DeleteDishes(dishesId);
            if (result > 0)
            {
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                return Content("success");
            }
            else
            {
                return Content("error");
            }
        }

        #endregion

        #region 添加新菜品

        /// <summary>
        /// 【1】添加新菜品页面
        /// </summary>
        /// <returns></returns>
        public ActionResult DishesPublish()
        {
            List<DishesCategory> categoryList = new DishesMananger().GetAllDishesCategory();
            SelectList slist = new SelectList(categoryList, "CategoryId", "CategoryName", categoryList[0].CategoryId);
            return View("DishesPublish", slist);//通过强类型视图传递
        }
        /// <summary>
        /// 【2】新菜品发布
        /// </summary>
        /// <param name="dishes"></param>
        /// <param name="dishesImg"></param>
        /// <returns></returns>
        public ActionResult PublishDishes(Dishes dishes, HttpPostedFileBase dishesImg)
        {
            try
            {
                if (dishesImg != null && dishesImg.FileName != "") //检查是否选择了图片
                {
                    //判断文件大小是否符合要求
                    double fileLength = dishesImg.ContentLength / (1024.0 * 1024.0);
                    if (fileLength > 2.0)
                    {
                        return Content("<script>alert('图片最大不能超过2MB');loaction.href='" + Url.Action("DishesPublish") + "'</script>");
                    }
                }
                else //如果没有上传的图片
                {
                    return Content("<script>alert('请选择上传的图片！');location.href='" + Url.Action("DishesPublish") + "'</script>");
                }
                int result = new DishesMananger().AddDishes(dishes);
                if (result > 0)  //如果数据库操作成功，则上传图片到服务指定文件夹
                {
                    //注意后台添加菜品对象的时候，需要返回标识列的值，因为这个值需要作为图片的名称
                    string filePath = Server.MapPath("~/Images/dishes/" + result + ".png");
                    dishesImg.SaveAs(filePath);
                }
                return Content("<script>alert('菜品上传成功！');location.href='" + Url.Action("DishesPublish") + "'</script>");
            }
            catch (Exception ex)
            {
                return Content("<script>alert('程序出错：'" + ex.Message + "');</script>");
            }
        }

        #endregion

        #region 修改菜品

        /// <summary>
        /// 【1】修改菜品页面
        /// </summary>
        /// <param name="dishesId"></param>
        /// <returns></returns>
        public ActionResult DishesUpdate(int dishesId)
        {
            //获取要修改的菜品对象
            DishesMananger dishesManager = new DishesMananger();
            Dishes dishes = dishesManager.GetDishesById(dishesId);

            //获取菜品分类下来列表数据集合
            List<DishesCategory> categoryList = new DishesMananger().GetAllDishesCategory();
            SelectList slist = new SelectList(categoryList, "CategoryId", "CategoryName", dishes.CategoryId);
            ViewBag.sList = slist;

            return View("DishesUpdate", dishes);
        }
        /// <summary>
        /// 【2】修改菜品
        /// </summary>
        /// <param name="dishes"></param>
        /// <param name="dishesImg"></param>
        /// <returns></returns>
        public ActionResult UpdateDishes(Dishes dishes, HttpPostedFileBase dishesImg)
        {
            try
            {
                int result = new DishesMananger().ModifyDishes(dishes);
                if (result > 0)
                {
                    if (dishesImg != null && dishesImg.FileName != "")   //判断是否需要修改图片
                    {
                        //判断文件大小是否符合要求
                        double fileLength = dishesImg.ContentLength / (1024.0 * 1024.0);
                        if (fileLength > 2.0)
                        {
                            return Content("<script>alert('图片最大不能超过2MB');loaction.href='" + Url.Action("DishesPublish") + "'</script>");
                        }
                        string filePath = Server.MapPath("~/Images/dishes/" + dishes.DishesId + ".png");
                        dishesImg.SaveAs(filePath);
                    }
                }
                return Content("<script>alert('菜品修改成功！');location.href='" + Url.Action("DishesManager") + "'</script>");
            }
            catch (Exception ex)
            {
                return Content("<script>alert('程序出错：'" + ex.Message + "');</script>");
            }
        }
        #endregion
    }
}