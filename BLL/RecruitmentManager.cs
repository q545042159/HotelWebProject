using DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class RecruitmentManager
    {
        private RecruitmentService recruitmentService = new RecruitmentService();

        /// <summary>
        /// 发布招聘信息
        /// </summary>
        /// <param name="recruitment"></param>
        /// <returns></returns>
        public int AddRecruitment(Recruitment recruitment)
        {
            return recruitmentService.AddRecruitment(recruitment);
        }
        /// <summary>
        /// 查询所有职位列表信息
        /// </summary>
        /// <returns></returns>
        public List<Recruitment> GetAllRecruitment()
        {
            return recruitmentService.GetAllRecruitment();
        }
        /// <summary>
        /// 根据发布编号查询详细职位信息
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public Recruitment GetPostById(int postId)
        {
            return recruitmentService.GetPostById(postId);
        }
        /// <summary>
        /// 修改招聘信息
        /// </summary>
        /// <param name="recruitment"></param>
        /// <returns></returns>
        public int ModifyRecruiment(Recruitment recruitment)
        {
            return recruitmentService.ModifyRecruiment(recruitment);
        }
        /// <summary>
        /// 删除招聘信息
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public int DeleteRecruiment(int postId)
        {
            return recruitmentService.DeleteRecruiment(postId);
        }
    }
}
