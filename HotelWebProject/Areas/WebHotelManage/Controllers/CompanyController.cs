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
    public class CompanyController : Controller
    {
        #region 招聘管理

        /// <summary>
        /// 【1】招聘管理页面显示列表
        /// </summary>
        /// <returns></returns>
        public ActionResult RecruitmentManager()
        {
            //获取所有的招聘职位
            List<Recruitment> list = new RecruitmentManager().GetAllRecruitment();
            ViewBag.list = list;
            return View();
        }
        /// <summary>
        /// 【2】显示要修改的招聘职位信息
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public ActionResult ModifyRecruitment(int postId)
        {
            Recruitment recruitment = new RecruitmentManager().GetPostById(postId);
            return View("RecruitmentUpdate", recruitment);
        }
        /// <summary>
        /// 【3】修改招聘信息
        /// </summary>
        /// <param name="recruiment"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ModifyRecruiment(Recruitment recruiment)
        {
            int result = new RecruitmentManager().ModifyRecruiment(recruiment);
            if (result > 0)
            {
                return Content("<script>alert('招聘信息修改成功！');location.href='" + Url.Action("RecruitmentManager") + "'</script>");
            }
            else
            {
                return Content("<script>alert('招聘信息修改失败！');location.href='" + Url.Action("RecruitmentManager") + "'</script>");
            }
        }
        /// <summary>
        /// 【4】招聘发布页面
        /// </summary>
        /// <returns></returns>
        public ActionResult RecruitmentPublish()
        {
            return View();
        }
        /// <summary>
        /// 【5】发布新的招聘信息
        /// </summary>
        /// <param name="recruiment"></param>
        /// <returns></returns>
        public ActionResult PublishRecruiment(Recruitment recruiment)
        {
            int result = new RecruitmentManager().AddRecruitment(recruiment);
            if (result > 0)
            {
                return Content("<script>alert('发布招聘成功！!');location.href='" + Url.Action("RecruitmentPublish") + "'</script>");
            }
            else
            {
                return Content("<script>alert('发布招聘失败！');location.href='" + Url.Action("RecruitmentPublish") + "'</script>");
            }
        }
        /// <summary>
        /// 【6】删除招聘信息
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public ActionResult DeleteRecruitment(int postId)
        {
            int result = new RecruitmentManager().DeleteRecruiment(postId);
            if (result > 0)
            {
                return Content("<script>alert('职位删除成功！');location.href='" + Url.Action("RecruitmentManager") + "'</script>");
            }
            else
            {
                return Content("<script>alert('职位删除失败！');location.href='" + Url.Action("RecruitmentManager") + "'</script>");
            }
        }

        #endregion

        #region 预定管理

        /// <summary>
        /// 【1】预订管理页面列表展示
        /// </summary>
        /// <returns></returns>
        public ActionResult BookManager()
        {
            //获取所有的预订列表
            List<DishesBook> bookList = new DishesBookManager().GetAllDishesBook();
            ViewBag.list = bookList;
            return View();
        }
        /// <summary>
        /// 【2】修改订单状态
        /// </summary>
        /// <param name="statId">订单状态参数</param>
        /// <param name="BookId">预订状态id</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ModifyBook(string statId, int BookId)
        {
            try
            {
                int result = new DishesBookManager().ModifyBook(BookId, statId);
                if (result > 0)
                {
                    return Content("<script>alert('订单状态修改完成！');location.href='" + Url.Action("BookManager") + "'</script>");
                }
                else
                {
                    return Content("<script>alert('订单状态修改失败！');location.href='" + Url.Action("BookManager") + "'</script>");
                }
            }
            catch (Exception ex)
            {
                return Content("<script>alert('" + ex.Message + "');location.href='" + Url.Action("BookManager") + "'</script>");
            }
        }
        #endregion       

        #region 投诉管理
       
        /// <summary>
        /// 投诉建议管理页
        /// </summary>
        /// <returns></returns>
        public ActionResult SuggestionManager()
        {
            //获取所有的未处理投诉
            List<Suggestion> list = new SuggestionManager().GetSuggestion();
            return View("SuggestionManager", list);//在这里没有用ViewBag传递，就是让大家知道，集合类型同样可以作为强类型实体的数据传递
        }
        /// <summary>
        /// 受理投诉
        /// </summary>
        /// <param name="suggestionId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult HandleSuggestion(int suggestionId)
        {
            //处理投诉就是将投诉的这条数据状态改成1，这部分也可以在业务逻辑里面写，但是本次我们放到的数据访问，请大家知道即可
            int result = new SuggestionManager().HandleSuggestion(suggestionId);
            if (result > 0)
            {
                return Content("<script>alert('投诉受理成功！');location.href='" + Url.Action("SuggestionManager") + "'</script>");
            }
            else
            {
                return Content("<script>alert('投诉受理失败！');location.href='" + Url.Action("SuggestionManager") + "'</script>");
            }
        }

        #endregion

    }
}