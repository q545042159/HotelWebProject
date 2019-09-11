using DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class SysAdminManager
    {
        private SysAdminService adminService = new SysAdminService();
        public SysAdmins AdminLogin(SysAdmins sysAdmin)
        {
            return adminService.AdminLogin(sysAdmin);
        }
    }
}
