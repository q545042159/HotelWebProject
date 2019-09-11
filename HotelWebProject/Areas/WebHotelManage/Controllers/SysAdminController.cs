using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Models;
using BLL;
using System.Web.Security;

namespace HotelWebProject.Areas.WebHotelManage.Controllers
{
    public class SysAdminController : Controller
    {
        //登录页面
        public ActionResult Index()
        {
            return View("AdminLogin");
        }
        /// <summary>
        /// 管理员登录
        /// </summary>
        /// <param name="sysAdmins"></param>
        /// <returns></returns>
        public ActionResult AdminLogin(SysAdmins sysAdmins)
        {
            if (ModelState.IsValid)
            {
                sysAdmins = new SysAdminManager().AdminLogin(sysAdmins);
                if (sysAdmins != null)
                {
                    Session["currentAdmin"] = sysAdmins.LoginId;
                    FormsAuthentication.SetAuthCookie(sysAdmins.LoginName, true);//签发身份票据
                    return View("AdminMain");
                }
                else
                {
                    return Content("<script>alert('用户名或密码错误！');location.href='" + Url.Action("Index") + "'</script>");
                }
            }
            else
            {
                return View("AdminLogin");
            }
        }
        /// <summary>
        /// 后台欢迎页面
        /// </summary>
        /// <returns></returns>
        public ActionResult AdminMain()
        {
            return View();
        }
        /// <summary>
        /// 退出系统
        /// </summary>
        /// <returns></returns>
        public ActionResult ExitSys()
        {
            Session["currentAdmin"] = null;
            Session.Abandon();
            FormsAuthentication.SignOut();
            return View("AdminLogin");
        }
    }
}