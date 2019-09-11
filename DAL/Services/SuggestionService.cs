using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Linq;
using System.Data.Entity.Validation;
using System.Data.Entity;

namespace DAL
{
    /// <summary>
    /// 投诉建议实体类
    /// </summary>
    public class SuggestionService
    {
        /// <summary>
        /// 提交投诉
        /// </summary>
        /// <param name="suggestion"></param>
        /// <returns></returns>
        public int SubmitSuggestion(Suggestion suggestion)
        {
            suggestion.SuggestionTime = DateTime.Now;//数据库默认值不起作用
            using (HotelDBEntities db = new HotelDBEntities())
            {
                db.Suggestion.Add(suggestion);
                return db.SaveChanges();
            }
        }

        /// <summary>
        /// 获取最新的建议
        /// </summary>
        /// <returns></returns>
        public List<Suggestion> GetSuggestion()
        {
            using (HotelDBEntities db = new HotelDBEntities())
            {
                return (from s in db.Suggestion where s.StatusId == 0 orderby s.SuggestionTime descending select s).ToList();
            }
        }

        /// <summary>
        /// 受理投诉
        /// </summary>
        /// <param name="suggestionId"></param>
        /// <returns></returns>
        public int HandleSuggestion(int suggestionId)
        {
            //下面的方法，执行后，会出现错误：其他信息: 对一个或多个实体的验证失败。有关详细信息，请参阅“EntityValidationErrors”属性。
            //using (HotelDBEntities db = new HotelDBEntities())
            //{
            //    Suggestion suggestion = new Suggestion { SuggestionId = suggestionId };               
            //    db.Suggestion.Attach(suggestion);
            //    suggestion.StatusId =1;//处理投诉就是将投诉的这条数据状态改成1，这部分也可以在业务逻辑里面写
            //    return db.SaveChanges();
            //}
            //错误原因：因为当时我们测试后台的时候，没有给Suggestion实体类添加MVC验证，而当我们添加后，如果更新实体必须给这些字段赋值
            //也就是不允许直接更新部分字段（请学员可以先测试上面的方法，然后对比下面的解决办法）
            //解决方法：修改前，先查询对象，保证其他字段有数据，这样才能让模型验证通过
            using (HotelDBEntities db = new HotelDBEntities())
            {
                Suggestion suggestion = (from s in db.Suggestion where s.SuggestionId == suggestionId select s).FirstOrDefault();
                db.Entry<Suggestion>(suggestion).State = EntityState.Modified;              
                suggestion.StatusId = 1;//处理投诉就是将投诉的这条数据状态改成1
                return db.SaveChanges();
            }

        }
    }
}
