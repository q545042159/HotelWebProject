using DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class NewsManager
    {
        private NewsService newsService = new NewsService();

        /// <summary>
        /// 根据指定条数查询新闻信息
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<News> GetNews(int count)
        {
            return newsService.GetNews(count);
        }
        public string GetCategoryName(int categoryId)
        {
            return newsService.GetCategoryName(categoryId);
        }
        /// <summary>
        /// 更新新闻
        /// </summary>
        /// <param name="news"></param>
        /// <returns></returns>
        public int ModifyNews(News news)
        {
            return newsService.ModifyNews(news);
        }
        /// <summary>
        /// 获取新闻所有分类
        /// </summary>
        /// <returns></returns>
        public List<NewsCategory> GetAllCategory()
        {
            return newsService.GetAllCategory();
        }
        /// <summary>
        /// 删除新闻
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        public int DeleteNews(int newsId)
        {
            return newsService.DeleteNews(newsId);
        }
        /// <summary>
        /// 根据id获取详情
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        public News GetNewsById(int newsId)
        {
            return newsService.GetNewsById(newsId);
        }
        /// <summary>
        /// 发布新闻
        /// </summary>
        /// <param name="news"></param>
        /// <returns></returns>
        public int PublishNews(News news)
        {
            return newsService.PublishNews(news);
        }
    }
}
