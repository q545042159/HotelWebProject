using BLL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebHotel.Models;

namespace HotelWebProject.Controllers
{
    public class DishesController : Controller
    {
        #region 自行完成部分

        /// <summary>
        /// 美食展示
        /// </summary>
        /// <returns></returns>
        public ActionResult DishesShow()
        {
            List<Dishes> dishList = new DishesMananger().GetAllDishes();
            ViewBag.dishes = dishList;
            return View();
        }

        #endregion

        /// <summary>
        /// 在线预订入口页面
        /// </summary>
        /// <returns></returns>
        public ActionResult DishesBook()
        {
            return View();
        }
        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <returns></returns>
        public ActionResult ValidateCode()
        {
            CreateValidateCode createCode = new WebHotel.Models.CreateValidateCode();
            string code = createCode.CreateRandomCode(6);
            Session["ValidateCode"] = code.ToLower();
            return File(createCode.CreateValidateGraphic(code), "images/jpeg");
        }
        /// <summary>
        ///验证码判断 
        /// </summary>
        /// <returns></returns>
        public ActionResult CheckValidate()
        {
            string txtValidateCode = Request["value"];
            if (String.Compare(Session["ValidateCode"].ToString(), txtValidateCode, true) != 0)
            {
                return Content("0");  //0代表验证码不正确
            }
            else
            {
                return Content("1"); //1代表正确
            }
        }
        //请学员自己尝试验证手机号码是否被注册过，当然我们这里面是允许重复。

        /// <summary>
        /// 在线预订
        /// </summary>
        /// <param name="dishesBook"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Book(DishesBook dishesBook)
        {
            dishesBook.BookTime = DateTime.Now;
            int result = new DishesBookManager().Book(dishesBook);
            if (result > 0)
                return Content("success");
            else
                return Content("error");
        }

    }
}