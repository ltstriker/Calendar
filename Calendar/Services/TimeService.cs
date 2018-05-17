using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Services
{
    public class TimeService
    {
        public static string getFormatTime(DateTimeOffset time)
        {



            //今年
            if (time.Year == DateTimeOffset.Now.Year)
            {
                if (time.Month == DateTimeOffset.Now.Month)
                {
                    if (time.Day == DateTimeOffset.Now.Day - 1)
                    {
                        return "昨天 " + time.Hour + ":" + time.Minute;
                    }
                    else if (time.Day == DateTimeOffset.Now.Day)
                    {
                        return "今天 " + time.Hour + ":" + time.Minute;
                    }
                    else if (time.Day == DateTimeOffset.Now.Day + 1)
                    {
                        return "明天 " + time.Hour + ":" + time.Minute;
                    }
                    else
                    {
                        return time.Month + "月 " + time.Day + "日";
                    }
                }
                else
                {
                    return time.Month + "月 " + time.Day + "日";
                }
            }
            else
            {
                return time.Year + "年 " + time.Month + "月";
            }

        }
    }
}
