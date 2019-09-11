using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class SysAdminService
    {
        /// <summary>
        /// 管理员登录
        /// </summary>
        /// <param name="sysAdmin"></param>
        /// <returns></returns>
        public SysAdmins AdminLogin(SysAdmins sysAdmin)
        {
            using (HotelDBEntities db = new HotelDBEntities())
            {
                return (from b in db.SysAdmins where b.LoginId == sysAdmin.LoginId && b.LoginPwd == sysAdmin.LoginPwd select b).FirstOrDefault();
            }
        }
    }
}
