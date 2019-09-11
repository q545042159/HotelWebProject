using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using BLL;

namespace TestProject
{
    class Program
    {
        static NewsManager newsManager = new NewsManager();
        static DishesBookManager bookManager = new DishesBookManager();

        #region 测试新闻模块方法

        //static void Main(string[] args)
        //{
        //    News news = new News { NewsTitle = "MVC+EF项目正式开讲！", NewsContents = "常老师详细讲解", CategoryId = 1 };
        //    Console.WriteLine(newsManager.PublishNews(news));
        //    Console.Read();
        //}

        //static void Main(string[] args)
        //{
        //    //封装修改实体对象的时候注意主键必须要有
        //    News news = new News { NewsId = 1010, NewsTitle = "ORM项目正式开讲！", NewsContents = "常老师详细讲解,每周晚上", CategoryId = 1 };
        //    Console.WriteLine(newsManager.ModifyNews(news));
        //    Console.Read();
        //}

        //static void Main(string[] args)
        //{            
        //    Console.WriteLine(newsManager.DeleteNews(1010));
        //    Console.Read();
        //}
        //查询方法（其他方法，请大家自己测试）
        //static void Main(string[] args)
        //{
        //    List<News> list = newsManager.GetNews(3);
        //    foreach (var item in list)
        //    {
        //        Console.WriteLine(item.NewsId + "   " + item.NewsTitle);
        //    }
        //    Console.Read();
        //}

        #endregion

        static void Main(string[] args)
        {
            Console.WriteLine(bookManager.ModifyBook(10003, "1"));

            Console.ReadLine();
        }
    }
}
