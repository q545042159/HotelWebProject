using Models;
using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelWebProject.Controllers
{
    public class CompanyController : Controller
    {
        // 网站首页
        public ActionResult Index()
        {
            //显示前5条最新的新闻
            List<News> newsList = new NewsManager().GetNews(5);
            ViewBag.list = newsList;
            return View();
        }
        //公司简介
        public ActionResult ComDesc()
        {
            return View();
        }

        #region 其他自行完成部分

        /// <summary>
        /// 餐厅环境
        /// </summary>
        /// <returns></returns>
        public ActionResult Environment()
        {
            return View();
        }
        /// <summary>
        /// 加盟连锁
        /// </summary>
        /// <returns></returns>
        public ActionResult JoinUs()
        {
            return View();
        }
        /// <summary>
        /// 招聘信息
        /// </summary>
        /// <returns></returns>
        public ActionResult RecruitmentList()
        {
            return View();
        }
        //招聘信息详情
        public ActionResult RecruitmentDetail(int postId)
        {
            Recruitment recruitment = new RecruitmentManager().GetPostById(postId);
            return View("RecruitmentDetail", recruitment);
        }
        /// <summary>
        /// 关于我们
        /// </summary>
        /// <returns></returns>
        public ActionResult AboutUs()
        {
            return View();
        }

        #endregion

        /// <summary>
        /// 投诉建议展示页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Suggestions()
        {
            return View();
        }

        /// <summary>
        /// 提交投诉建议
        /// </summary>
        /// <param name="suggestion"></param>
        /// <param name="vCode"></param>
        /// <returns></returns>
        public ActionResult SubmitSuggestion(Suggestion suggestion, string vCode)
        {
            if (ModelState.IsValid)
            {
                string code = Session["ValidateCode"].ToString();
                if (code != vCode.ToLower())
                {
                    ModelState.AddModelError("vCode", "验证码不正确，请重新输入！");
                    return View("Suggestions", suggestion);
                }
                else
                {
                    suggestion.StatusId = 0;//如果不设置，会产生null
                    int result = new SuggestionManager().SubmitSuggestion(suggestion);
                    if (result > 0)
                    {
                        return Content("<script>alert('投诉提交成功！');window.location='" + Url.Content("~/") + "'</script>");
                    }
                    else
                    {
                        //可以根据需要，停留到当前页面，也可以刷新当前页面...
                        return Content("<script>alert('投诉提交失败！');window.location='" +
                         Url.Content("~/Company/Suggestions") + "'</script>");
                    }
                }
            }
            else
            {
                return View("Suggestions", suggestion);
            }
        }

    }
}