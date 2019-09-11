using System.Web.Mvc;

namespace HotelWebProject.Areas.WebHotelManage
{
    public class WebHotelManageAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "WebHotelManage";
            }
        }
        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "WebHotelManage_default",
                "WebHotelManage/{controller}/{action}/{id}",
                new { Controller="SysAdmin", action = "Index", id = UrlParameter.Optional },

                //如果项目和分区项目有完全相同的控制器，必须增加一个命名空间来区分，否则出错
                namespaces:new string[] { "HotelWebProject.Areas.WebHotelManage.Controllers" }
            );
        }
    }
}