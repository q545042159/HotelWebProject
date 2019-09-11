using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;

namespace DAL
{
    /// <summary>
    /// 基于EF的通用数据访问类
    /// </summary>
    class EFDBHelper
    {
        private DbContext dbContex = null;

        public EFDBHelper(DbContext context)
        {
            this.dbContex = context;
        }
        /// <summary>
        /// 新增实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Add<T>(T entity) where T : class
        {
            dbContex.Entry<T>(entity).State = EntityState.Added;
            //dbContex.Set<T>().Attach(entity);
            //dbContex.Set<T>().Add(entity);
            return dbContex.SaveChanges();
        }
        /// <summary>
        /// 修改一个实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Modify<T>(T entity) where T : class
        {
            dbContex.Entry<T>(entity).State = EntityState.Modified;//此种方式设置更新的是全部字段，不适合部分字段的更新
            return dbContex.SaveChanges();
        }
        /// <summary>
        /// 删除一个实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Delete<T>(T entity) where T : class
        {
            dbContex.Entry<T>(entity).State = EntityState.Deleted;
            //dbContex.Set<T>().Attach(entity);
            //dbContex.Set<T>().Remove(entity);
            return dbContex.SaveChanges();
        }
    }
}
