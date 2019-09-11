using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;

namespace DAL
{
    public class NewsService
    {
        //创建一个通用类
        private EFDBHelper helper = new EFDBHelper(new HotelDBEntities());

        /// <summary>
        /// 发布新闻：添加
        /// </summary>
        /// <param name="news"></param>
        /// <returns></returns>
        //public int PublishNews(News news)
        //{
        //    //using (HotelDBEntities db = new HotelDBEntities())
        //    //{
        //    //    db.News.Add(news);
        //    //    return db.SaveChanges();
        //    //}

        //    return helper.Add<News>(news);
        //}
        //【下面是使用存储过程】
        public int PublishNews(News news)
        {
            SqlParameter[] param = new SqlParameter[]
             {
                new SqlParameter("@NewsTitle",news.NewsTitle),
                new SqlParameter("@NewsContents",news.NewsContents),
                new SqlParameter("@CategoryId",news.CategoryId)
             };
            using (HotelDBEntities db = new DAL.HotelDBEntities())
            {
                return db.Database.ExecuteSqlCommand("execute usp_AddNews @NewsTitle,@NewsContents,@CategoryId", param);
            }
        }

        /// <summary>
        /// 修改新闻
        /// </summary>
        /// <param name="news"></param>
        /// <returns></returns>
        public int ModifyNews(News news)
        {
            return helper.Modify<News>(news);
        }
        /// <summary>
        /// 删除新闻
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        public int DeleteNews(int newsId)
        {
            News news = new News { NewsId = newsId };
            return helper.Delete<News>(news);
        }

        /// <summary>
        /// 查询指定前几条新闻
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<News> GetNews(int count)
        {
            using (HotelDBEntities db = new DAL.HotelDBEntities())
            {
                //下面这条语句没有管理对象的查询
                //return (from n in db.News
                //        orderby n.PublishTime descending
                //        select n).Take(count).ToList();

                //通过下面发方法查询关联对象
                var newsList = (from n in db.News
                                orderby n.PublishTime descending
                                select new
                                {
                                    n.NewsId,
                                    n.NewsTitle,
                                    n.PublishTime,
                                    n.NewsContents,
                                    n.CategoryId,
                                    n.NewsCategory.CategoryName
                                }).Take(count);
                //转换成具体对象
                List<News> list = new List<News>();
                foreach (var item in newsList)
                {
                    list.Add(new News
                    {
                        NewsId = item.NewsId,
                        NewsTitle = item.NewsTitle,
                        NewsContents = item.NewsContents,
                        PublishTime = item.PublishTime,
                        CategoryId = item.CategoryId,
                        NewsCategory = new NewsCategory { CategoryName = item.CategoryName }
                    });
                }
                return list;
            }
        }
        /// <summary>
        /// 根据新闻Id查询新闻详细
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        public News GetNewsById(int newsId)
        {
            using (HotelDBEntities db = new DAL.HotelDBEntities())
            {
                return (from n in db.News where n.NewsId == newsId select n).FirstOrDefault();
            }
        }
        /// <summary>
        /// 获取所有的新闻分类
        /// </summary>
        /// <returns></returns>
        public List<NewsCategory> GetAllCategory()
        {
            using (HotelDBEntities db = new DAL.HotelDBEntities())
            {
                return (from c in db.NewsCategory select c).ToList();
            }
        }

        /// <summary>
        /// 根据新闻分类ID查询分类名称
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public string GetCategoryName(int categoryId)
        {
            using (HotelDBEntities db = new DAL.HotelDBEntities())
            {
                return (from c in db.NewsCategory
                        where c.CategoryId == categoryId
                        select c.CategoryName).FirstOrDefault();
            }
        }

    }
}
