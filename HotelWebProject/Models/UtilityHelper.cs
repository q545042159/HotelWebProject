using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebHotel.Models
{
    public class UtilityHelper
    {
        #region  类型转换
        public static decimal ParseDecimal(string num)
        {
            decimal value = 0;
            try
            {
                value = Convert.ToDecimal(num);
            }
            catch (Exception)
            {
                value = 0;
            }
            return value;
        }
        public static double ParseDouble(string ss)
        {
            double value = 0;
            try
            {
                value = Convert.ToDouble(ss);
            }
            catch (Exception)
            {
                value = 0;
            }
            return value;
        }
        public static int ParseInt(string num)
        {
            int value = 0;
            try
            {
                value = Convert.ToInt32(num);
            }
            catch (Exception)
            {

                value = 0;
            }
            return value;
        }
        #endregion
       
        /// <summary>
        /// 截断字符串
        /// </summary>
        /// <param name="content"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string CutString(object content, int num)
        {
            if (content.ToString().Length > num - 2)
                return content.ToString().Substring(0, num - 2) + "......";
            else
                return content.ToString();
        }
        /// <summary>
        /// 货币
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToMoney(object obj)
        {
            return String.Format("{0:C}", obj);
        }
        /// <summary>
        /// 格式化日期
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToShortDate(object obj)
        {
            return Convert.ToDateTime(obj).ToString("yyyy-MM-dd");
        }
    }
}