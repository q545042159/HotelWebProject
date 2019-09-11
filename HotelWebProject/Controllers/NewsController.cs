using Models;
using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelWebProject.Controllers
{
    public class NewsController : Controller
    {
        // 新闻动态主页
        public ActionResult NewsList()
        {
            List<News> newsList = new NewsManager().GetNews(10);
            ViewBag.newsList = newsList;
            return View();
        }
        //新闻详情展示
        public ActionResult NewsDetail(int newsId)
        {
            News news = new NewsManager().GetNewsById(newsId);
            return View("NewsDetail", news);
        }

    }
}