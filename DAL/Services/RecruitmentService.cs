using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DAL
{
    /// <summary>
    /// 招聘信息数据访问类
    /// </summary>
    public class RecruitmentService
    {
        /// <summary>
        /// 发布招聘信息
        /// </summary>
        /// <param name="recruitment"></param>
        /// <returns></returns>
        public int AddRecruitment(Recruitment recruitment)
        {
            using (HotelDBEntities db = new HotelDBEntities())
            {
                db.Recruitment.Add(recruitment);
                return db.SaveChanges();
            }
        }
        /// <summary>
        /// 修改招聘信息
        /// </summary>
        /// <param name="recruitment"></param>
        /// <returns></returns>
        public int ModifyRecruiment(Recruitment recruitment)
        {
            using (HotelDBEntities db = new HotelDBEntities())
            {
                db.Recruitment.Attach(recruitment);
                db.Entry<Recruitment>(recruitment).State = EntityState.Modified;
                return db.SaveChanges();
            }
        }
        /// <summary>
        /// 删除招聘信息
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public int DeleteRecruiment(int postId)
        {          
            using (HotelDBEntities db = new HotelDBEntities())
            {
                Recruitment recruitment = new Recruitment { PostId = postId };       
                db.Recruitment.Attach(recruitment);
                db.Recruitment.Remove(recruitment);
                return db.SaveChanges();
            }
        }

        /// <summary>
        /// 查询所有职位列表信息
        /// </summary>
        /// <returns></returns>      
        public List<Recruitment> GetAllRecruitment()
        {
            using (HotelDBEntities db = new HotelDBEntities())
            {
                return (from m in db.Recruitment select m).ToList();
            }
        }
        /// <summary>
        /// 根据发布编号查询详细职位信息
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public Recruitment GetPostById(int postId)
        {
            using (HotelDBEntities db = new HotelDBEntities())
            {
                return (from n in db.Recruitment where n.PostId == postId select n).FirstOrDefault();
            }
        }

    }
}

