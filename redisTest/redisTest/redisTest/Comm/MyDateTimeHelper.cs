using System;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

/****************************************************************
*   Copyright (c) 2018 江苏金恒,All rights reserved.
*   命名空间: LTN.CS.Base.Helper
*   模块名称: MyDateTimeHelper 
*   作用：时间工具类
*   作者：016523-kolio
*	创建时间：2018/2/5 15:39:34
*   修改历史：
*****************************************************************/
namespace redisTest.Comm
{
    public class MyDateTimeHelper
    {
        [DllImport("Kernel32.dll")]
        private static extern Boolean SetSystemTime([In, Out] SystemTime st);
        [DllImport("Kernel32.dll")]
        public static extern bool SetLocalTime([In, Out] SystemTime sysTime);
        /// <summary>
        /// 设置系统时间
        /// </summary>
        /// <param name="newdatetime">新时间</param>
        /// <returns></returns>
        public static bool SetSysTime(DateTime newdatetime)
        {
            SystemTime st = new SystemTime();
            st.year = Convert.ToUInt16(newdatetime.Year);
            st.month = Convert.ToUInt16(newdatetime.Month);
            st.day = Convert.ToUInt16(newdatetime.Day);
            st.dayofweek = Convert.ToUInt16((int)newdatetime.DayOfWeek);
            st.hour = Convert.ToUInt16(newdatetime.Hour);
            st.minute = Convert.ToUInt16(newdatetime.Minute);
            st.second = Convert.ToUInt16(newdatetime.Second);
            st.milliseconds = Convert.ToUInt16(newdatetime.Millisecond);
            return SetLocalTime(st);
        }
        /// <summary>
        ///系统时间类
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public class SystemTime
        {
            public ushort year;
            public ushort month;
            public ushort dayofweek;
            public ushort day;
            public ushort hour;
            public ushort minute;
            public ushort second;
            public ushort milliseconds;
        }


        #region 日期转换
        /// <summary>
        /// 从日期字符串转换为日期：例 YYYY年MM月 或 YYYY年M月
        /// </summary>
        /// <param name="dateStr">输入日期字符串</param>
        /// <returns></returns>
        public static DateTime GetDateTimeFromYYYYMM(string dateStr)
        {
            DateTime dt = DateTime.MinValue;
            if (dateStr.Contains("年") && dateStr.Contains("月"))
            {
                StringBuilder strYear = new StringBuilder();
                strYear.Append(dateStr.Split('年')[0]);
                strYear.Append("年");
                string month = dateStr.Split('年')[1].Split('月')[0];
                strYear.Append(month.Length == 1 ? "0" + month : month);
                strYear.Append("月");
                dt = DateTime.ParseExact(strYear.ToString(), "yyyy年MM月", null);
            }
            return dt;
        }

        /// <summary>
        /// 获取当前或者输入日期所在季度的第一天
        /// </summary>
        /// <param name="inputDate">输入日期：可以转换为DateTime的对象</param>
        /// <returns></returns>
        public static string GetStartDateOfNowQuarter(object inputDate = null)
        {
            DateTime d = ConvertToDateTimeDefaultNow(inputDate);
            string rtn = d.AddMonths(0 - ((d.Month - 1) % 3)).ToString("yyyy-MM-01");
            return rtn;
        }

        /// <summary>
        /// 获取当前或者输入日期所在季度的最后一天
        /// </summary>
        /// <param name="inputDate">输入日期：可以转换为DateTime的对象</param>
        /// <returns></returns>
        public static string GetEndDateOfNowQuarter(object inputDate = null)
        {
            DateTime d = ConvertToDateTimeDefaultNow(inputDate);
            return DateTime.Parse(d.AddMonths(3 - ((d.Month - 1) % 3)).ToString("yyyy-MM-01")).AddDays(-1).ToShortDateString();

        }

        /// <summary>
        /// 传入日期得到是哪一季度,格式不对或不传以当前日期为准
        /// </summary>
        /// <param name="inputDate">输入日期：可以转换为DateTime的对象</param>
        /// <returns></returns>
        public static int GetQuarterOfYearMonth(object inputDate = null)
        {
            int rtn = 0;
            DateTime date = ConvertToDateTimeDefaultNow(inputDate);
            int m = date.Month;
            //第一季度
            if (m >= 1 && m < 4)
            {
                rtn = 1;
            }
            //第二季度   
            if (m >= 4 && m < 7)
            {
                rtn = 2;
            }
            //第三季度
            if (m >= 7 && m < 10)
            {
                rtn = 3;
            }
            //第四季度
            if (m >= 10 && m < 13)
            {
                rtn = 4;
            }
            return rtn;
        }

        /// <summary>
        /// 传入的日期转换YYYYMMddHHmmss格式字符串
        /// </summary>
        /// <param name="inputDate">输入日期：可以转换为DateTime的对象</param>
        /// <returns></returns>
        public static string GetNowDateTimeYyyyMMddHHmmss(object inputDate = null)
        {
            DateTime date = ConvertToDateTimeDefaultNow(inputDate);
            return date.ToString("yyyyMMddHHmmss");
        }

        /// <summary>
        /// 传入的日期转换YyyyMMddHHmmssffff格式字符串
        /// </summary>
        /// <param name="inputDate">输入日期：可以转换为DateTime的对象</param>
        /// <returns></returns>
        public static string GetNowDateTimeYyyyMMddHHmmssffff(object inputDate = null)
        {
            DateTime date = ConvertToDateTimeDefaultNow(inputDate);
            return date.ToString("yyyyMMddHHmmssffff");

        }

        /// <summary>
        /// 传入的日期转换为日期型,如果格式错误返回当前日期
        /// </summary>
        /// <param name="inputDate">输入日期：可以转换为DateTime的对象</param>
        /// <returns></returns>
        public static DateTime ConvertToDateTimeDefaultNow(object inputDate)
        {
            DateTime rtn;
            bool flag = DateTime.TryParse(Convert.ToString(inputDate), out rtn);
            if (!flag)
            {
                rtn = DateTime.Now;
            }
            return rtn;

        }


        /// <summary>
        /// 传入的日期转换为日期型,如果格式错误返回当前日期
        /// </summary>
        /// <param name="inputDate">输入日期：可以转换为DateTime的对象</param>
        /// <returns></returns>
        public static DateTime ConvertToDateTimeDefaultNow(object inputDate,string format)
        {
            DateTime rtn;
            bool flag = DateTime.TryParseExact(Convert.ToString(inputDate),format, System.Globalization.CultureInfo.InvariantCulture,
                                   System.Globalization.DateTimeStyles.None,out rtn);
            if (!flag)
            {
                rtn = DateTime.Now;
            }
            return rtn;

        }

        /// <summary>
        /// 传入的字符型日期转换日期日期型，如果格式有问题,返回DateTime.MinValue
        /// </summary>
        /// <param name="inputDate">输入日期：可以转换为DateTime的对象</param>
        /// <returns></returns>
        public static DateTime ConvertToDateTimeDefaultNull(object inputDate)
        {
            DateTime rtn;
            bool flag = DateTime.TryParse(Convert.ToString(inputDate), out rtn);
            if (!flag)
            {
                rtn = DateTime.MinValue;
            }
            return rtn;
        }


        /// <summary>
        /// 传入的字符型日期转换日期日期型，如果格式有问题,返回NULL
        /// </summary>
        /// <param name="inputDate">输入日期：可以转换为DateTime的对象</param>
        /// <returns></returns>
        public static DateTime? ConvertToDateTimeNDefaultNull(object inputDate)
        {
            DateTime? rs = null;
            DateTime rtn;
            bool flag = DateTime.TryParse(Convert.ToString(inputDate), out rtn);
            if (!flag)
            {
                rs = null;
            }
            else
            {
                rs = rtn;
            }
            return rs;

        }

        /// <summary>
        /// 传入的字符型日期转换日期日期型，如果格式有问题输出异常
        /// </summary>
        /// <param name="inputDate">输入日期：可以转换为DateTime的对象</param>
        /// <returns></returns>
        public static DateTime ConvertToDateTimeDefaultException(object inputDate)
        {
            DateTime rtn;
            bool ex = DateTime.TryParse(Convert.ToString(inputDate), out rtn);
            if (!ex)
            {
                throw new FormatException("日期格式不对!");
            }
            return rtn;
        }

        /// <summary>
        /// 得到系统当前时间:Date+Time
        /// </summary>
        /// <returns></returns>
        public static DateTime GetSystemNowDateTime()
        {
            return DateTime.Now;
        }

        /// <summary>
        /// 得到系统当前日期:Date
        /// </summary>
        /// <returns></returns>
        public static DateTime GetSystemNowDate()
        {
            return DateTime.Now.Date;
        }

        /// <summary>
        /// 得到系统当前日期:LongTimeString 例：yyyy-MM-dd HH:mm:ss
        /// </summary>
        /// <returns></returns>
        public static string GetSystemNowLongTimeString()
        {
            return DateTime.Now.ToLongTimeString();
        }

        /// <summary>
        /// 得到系统当前日期:ShortTimeString 例：yyyy-MM-dd
        /// </summary>
        /// <returns></returns>
        public static string GetSystemNowShortTimeString()
        {
            return DateTime.Now.ToShortTimeString();
        }

        /// <summary>
        /// 得到系统当前日期:Date,返回字符串日期,yyyy-MM-dd
        /// </summary>
        /// <returns></returns>
        public static String GetSystemNowStrFormatDate(string parttern = "yyyy-MM-dd")
        {
            DateTime date = DateTime.Now;
            return date.ToString(parttern);
        }

        /// <summary>
        /// 得到系统当前日期:Date,返回日期格式,yyyy-MM-dd
        /// </summary>
        /// <returns></returns>
        public static DateTime GetSystemNowFormatDate(string parttern = "yyyy-MM-dd")
        {
            String format = parttern;
            DateTime date = DateTime.Now;
            return ConvertToDateTimeDefaultNow(date.ToString(format));
        }
        #endregion

        #region 获取日期部分内容
        /// <summary>
        /// 得到当前年份,可以传入一个日期格式，如果不传则以当前日期的年份
        /// </summary>
        /// <param name="inputDate">输入日期：可以转换为DateTime的对象</param>
        /// <returns></returns>
        public static int GetYearOfDateTime(object inputDate = null)
        {
            DateTime dt = ConvertToDateTimeDefaultNow(inputDate);
            return dt.Year;
        }

        /// <summary>
        /// 得到当前月份,可以传入一个日期格式，如果不传则以当前日期的月份
        /// </summary>
        /// <param name="inputDate">输入日期：可以转换为DateTime的对象</param>
        /// <returns></returns>
        public static int GetMonthOfDateTime(object inputDate = null)
        {
            DateTime dt = ConvertToDateTimeDefaultNow(inputDate);
            return dt.Month;
        }


        /// <summary>
        /// 得到当前天数,可以传入一个日期格式，如果不传则以当前日期的天数
        /// </summary>
        /// <param name="inputDate">输入日期：可以转换为DateTime的对象</param>
        /// <returns></returns>
        public static int GetDayOfDateTime(object inputDate = null)
        {
            DateTime dt = ConvertToDateTimeDefaultNow(inputDate);
            return dt.Day;
        }

        /// <summary>
        /// 得到当前小时,可以传入一个日期格式，如果不传则以当前日期的小时
        /// </summary>
        /// <param name="inputDate">输入日期：可以转换为DateTime的对象</param>
        /// <returns></returns>
        public static int GetHourByDateTime(object inputDate = null)
        {
            DateTime dt = ConvertToDateTimeDefaultNow(inputDate);
            return dt.Hour;
        }


        /// <summary>
        /// 得到当前分钟,可以传入一个日期格式，如果不传则以当前日期的分钟
        /// </summary>
        /// <param name="inputDate">输入日期：可以转换为DateTime的对象</param>
        /// <returns></returns>
        public static int GeMinuteOfDateTime(object inputDate = null)
        {
            DateTime dt = ConvertToDateTimeDefaultNow(inputDate);
            return dt.Minute;
        }

        /// <summary>
        /// 得到当前是星期几,可以传入一个日期格式，如果不传则以当前日期的星期
        /// </summary>
        /// <param name="inputDate">输入日期：可以转换为DateTime的对象</param>
        /// <returns></returns>
        public static DayOfWeek GetDayOfWeekByDateTime(object inputDate = null)
        {
            DateTime dt = ConvertToDateTimeDefaultNow(inputDate);
            return dt.DayOfWeek;
        }

        /// <summary>
        /// 得到当前是第几天,可以传入一个日期格式，如果不传则以当前日期的第几天
        /// </summary>
        /// <param name="inputDate">输入日期：可以转换为DateTime的对象</param>
        /// <returns></returns>
        public static int GetDayOfYearByDateTime(object inputDate = null)
        {
            DateTime dt = ConvertToDateTimeDefaultNow(inputDate);
            return dt.DayOfYear;
        }
        #endregion

        #region 日期加减计算
        /// <summary>
        /// 时间加n年,传入日期,加上天数后是什么日期,可以传入一个日期格式，如果不传则以当前日期
        /// num为一个数,可以正整数,也可以负整数
        /// </summary>
        /// <param name="inputDate">输入日期：可以转换为DateTime的对象</param>
        /// <returns></returns>
        public static DateTime AddYearByDateTime(object inputDate = null, int num = 0)
        {
            DateTime dt = ConvertToDateTimeDefaultNow(inputDate);
            return dt.AddYears(num);
        }

        /// <summary>
        /// 加n天,传入日期,加上天数后是什么日期,可以传入一个日期格式，如果不传则以当前日期
        /// num为一个数,可以正整数,也可以负整数
        /// </summary>
        /// <param name="inputDate">输入日期：可以转换为DateTime的对象</param>
        /// <returns></returns>
        public static DateTime AddDaysByDateTime(object inputDate = null, int num = 0)
        {
            DateTime dt = ConvertToDateTimeDefaultNow(inputDate);
            return dt.AddDays(num);
        }

        /// <summary>
        /// 加n小时,传入日期,加上天数后是什么日期,可以传入一个日期格式，如果不传则以当前日期
        /// num为一个数,可以正整数,也可以负整数
        /// </summary>
        /// <param name="inputDate">输入日期：可以转换为DateTime的对象</param>
        /// <returns></returns>
        public static DateTime AddHoursByDateTime(object inputDate = null, int num = 0)
        {
            DateTime dt = ConvertToDateTimeDefaultNow(inputDate);
            return dt.AddHours(num);
        }

        /// <summary>
        /// 加n个月,传入日期,加上天数后是什么日期,可以传入一个日期格式，如果不传则以当前日期
        /// </summary>
        /// <param name="inputDate">输入日期：可以转换为DateTime的对象</param>
        /// <returns></returns>
        public static DateTime AddMonthsByDateTime(object inputDate = null, int num = 0)
        {
            DateTime dt = ConvertToDateTimeDefaultNow(inputDate);
            return dt.AddMonths(num);
        }


        /// <summary>
        /// /加n分,传入日期,加上天数后是什么日期,可以传入一个日期格式，如果不传则以当前日期
        /// num为一个数,可以正整数,也可以负整数
        /// </summary>
        /// <param name="inputDate">输入日期：可以转换为DateTime的对象</param>
        /// <returns></returns>
        public static DateTime AddMinutesByDateTime(object inputDate = null, int num = 0)
        {
            DateTime dt = ConvertToDateTimeDefaultNow(inputDate);
            return dt.AddMinutes(num);
        }


        /// <summary>
        /// 加n秒,传入日期,加上天数后是什么日期,可以传入一个日期格式，如果不传则以当前日期
        /// num为一个数,可以正整数,也可以负整数
        /// </summary>
        /// <param name="inputDate">输入日期：可以转换为DateTime的对象</param>
        /// <returns></returns>
        public static DateTime AddSecondsByDateTime(object inputDate = null, int num = 0)
        {
            DateTime dt = ConvertToDateTimeDefaultNow(inputDate);
            return dt.AddSeconds(num);
        }
        #endregion

        #region 获取特定日期
        /// <summary>
        /// 得到某月最后一天,如果格式不对则返回0,传入日期,如果日期格式不对，则以当前日期为准
        /// </summary>
        /// <param name="inputDate">输入日期：可以转换为DateTime的对象</param>
        /// <returns></returns>
        public static int GetMonthLastDayOfDateTime(object inputDate)
        {
            int rtn = 0;

            DateTime dt = AddMonthsByDateTime(inputDate, 1);
            int year = dt.Year;
            int month = dt.Month;
            DateTime dtOk = new DateTime(year, month, 1);
            DateTime dtRtn = dtOk.AddDays(-1);
            rtn = dtRtn.Day;
            return rtn;

        }

        /// <summary>
        /// 得到当前周是今年的是第几周
        /// </summary>
        /// <returns></returns>
        public static int GetWeekOfYear()
        {
            GregorianCalendar gc = new GregorianCalendar();
            int weekOfYear = gc.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            return weekOfYear;
        }

        /// <summary>
        /// 获取当前日期指定周数的开始日期
        /// </summary>      
        /// <returns></returns>
        public static DateTime GetStartDayOfWeeks()
        {
            DateTime startDayOfWeeks = DateTime.Now.AddDays(Convert.ToDouble((0 - Convert.ToInt16(DateTime.Now.DayOfWeek))));
            return startDayOfWeeks;
        }

        /// <summary>
        /// 获取当前日期指定周数的结束日期
        /// </summary>      
        /// <returns></returns>
        public static DateTime GetEndDayOfWeeks()
        {
            DateTime endDayOfWeeks = DateTime.Now.AddDays(Convert.ToDouble((6 - Convert.ToInt16(DateTime.Now.DayOfWeek))));
            return endDayOfWeeks;
        }

        /// <summary>
        /// 获取指定日期指定周数的开始日期，如果日期格式不对则使用当前日期
        /// </summary>      
        /// <returns></returns>
        public static DateTime GetStartDayOfWeeks(object input)
        {
            DateTime dt = ConvertToDateTimeDefaultNow(input);
            DateTime startDayOfWeeks = dt.AddDays(Convert.ToDouble((0 - Convert.ToInt16(dt.DayOfWeek))));
            return startDayOfWeeks;
        }

        /// <summary>
        /// 获取指定日期指定周数的结束日期，如果日期格式不对则使用当前日期
        /// </summary>      
        /// <returns></returns>
        public static DateTime GetEndDayOfWeeks(object input)
        {
            DateTime dt = ConvertToDateTimeDefaultNow(input);
            DateTime endDayOfWeeks = dt.AddDays(Convert.ToDouble((6 - Convert.ToInt16(dt.DayOfWeek))));
            return endDayOfWeeks;
        }
        #endregion

        #region 得到指定日期上周的开始与结束日期
        /// <summary>
        /// 获取当前日期指定上周数的开始日期
        /// </summary>      
        /// <returns></returns>
        public static DateTime GetStartDayOfLastWeeks()
        {
            DateTime startDayOfWeeks = DateTime.Now.AddDays(Convert.ToDouble((0 - Convert.ToInt16(DateTime.Now.DayOfWeek))) - 7);
            return startDayOfWeeks;
        }

        /// <summary>
        /// 获取当前日期指定上周数的结束日期
        /// </summary>      
        /// <returns></returns>
        public static DateTime GetEndDayOfLastWeeks()
        {
            DateTime endDayOfWeeks = DateTime.Now.AddDays(Convert.ToDouble((6 - Convert.ToInt16(DateTime.Now.DayOfWeek))) - 7);
            return endDayOfWeeks;
        }

        /// <summary>
        /// 获取指定日期指定上周数的开始日期,如果日期格式不对，则使用当前日期
        /// </summary>      
        /// <param name="inputDate">输入日期：可以转换为DateTime的对象</param>
        /// <returns></returns>
        public static DateTime GetStartDayOfLastWeeks(object inputDate)
        {
            DateTime dt = ConvertToDateTimeDefaultNow(inputDate);
            DateTime startDayOfWeeks = dt.AddDays(Convert.ToDouble((0 - Convert.ToInt16(dt.DayOfWeek))) - 7);
            return startDayOfWeeks;
        }

        /// <summary>
        /// 获取指定日期指定上周数的结束日期，如果日期格式对,则使用当前日期
        /// </summary>      
        /// <param name="inputDate">输入日期：可以转换为DateTime的对象</param>
        /// <returns></returns>
        public static DateTime GetEndDayOfLastWeeks(object inputDate)
        {
            DateTime dt = ConvertToDateTimeDefaultNow(inputDate);
            DateTime endDayOfWeeks = dt.AddDays(Convert.ToDouble((6 - Convert.ToInt16(dt.DayOfWeek))) - 7);
            return endDayOfWeeks;
        }
        #endregion

        #region 得到下周的开始与结束日期


        /// <summary>
        /// 获取当前日期指定下周数的开始日期
        /// </summary>      
        /// <returns></returns>
        public static DateTime GetStartDayOfNextWeeks()
        {
            DateTime startDayOfWeeks = DateTime.Now.AddDays(Convert.ToDouble((0 - Convert.ToInt16(DateTime.Now.DayOfWeek))) + 7);
            return startDayOfWeeks;
        }

        /// <summary>
        /// 获取当前日期指定下周数的结束日期
        /// </summary>      
        /// <returns></returns>
        public static DateTime GetEndDayOfNextWeeks()
        {
            DateTime endDayOfWeeks = DateTime.Now.AddDays(Convert.ToDouble((6 - Convert.ToInt16(DateTime.Now.DayOfWeek))) + 7);
            return endDayOfWeeks;
        }

        /// <summary>
        /// 获取指定日期指定下周数的开始日期,如果日期格式不对，则使用当前日期
        /// </summary>      
        /// <param name="inputDate">输入日期：可以转换为DateTime的对象</param>
        /// <returns></returns>
        public static DateTime GetStartDayOfNextWeeks(object inputDate)
        {
            DateTime dt = ConvertToDateTimeDefaultNow(inputDate);
            DateTime startDayOfWeeks = dt.AddDays(Convert.ToDouble((0 - Convert.ToInt16(dt.DayOfWeek))) + 7);
            return startDayOfWeeks;
        }

        /// <summary>
        /// 获取指定日期指定下周数的结束日期，如果日期格式对,则使用当前日期
        /// </summary>     
        /// <param name="inputDate">输入日期：可以转换为DateTime的对象</param>
        /// <returns></returns>
        public static DateTime GetEndDayOfNextWeeks(object inputDate)
        {
            DateTime dt = ConvertToDateTimeDefaultNow(inputDate);
            DateTime endDayOfWeeks = dt.AddDays(Convert.ToDouble((6 - Convert.ToInt16(dt.DayOfWeek))) + 7);

            return endDayOfWeeks;
        }

        #endregion

        #region 日期相减
        /// <summary>
        /// 获得两个日期相减后的差的总计天数
        /// </summary>
        /// <param name="dtFrom">小日期</param>
        /// <param name="dtTo">大日期</param>
        /// <returns></returns>
        public static double GetDiffTotalDaysByDate(object dtFrom, object dtTo)
        {
            DateTime dt1 = ConvertToDateTimeDefaultException(dtFrom);
            DateTime dt2 = ConvertToDateTimeDefaultException(dtTo);
            TimeSpan ts = dt2 - dt1;
            return ts.TotalDays;
        }

        /// <summary>
        /// 获得两个日期相减后的差的总计小时
        /// </summary>
        /// <param name="dtFrom">小日期</param>
        /// <param name="dtTo">大日期</param>
        /// <returns></returns>
        public static double GetDiffTotalHoursByDate(object dtFrom, object dtTo)
        {
            DateTime dt1 = ConvertToDateTimeDefaultException(dtFrom);
            DateTime dt2 = ConvertToDateTimeDefaultException(dtTo);
            TimeSpan ts = dt2 - dt1;
            return ts.TotalHours;
        }

        /// <summary>
        /// 获得两个日期相减后的差的总计分钟
        /// </summary>
        /// <param name="dtFrom">小日期</param>
        /// <param name="dtTo">大日期</param>
        /// <returns></returns>
        public static double GetDiffTotalMinutesByDate(object dtFrom, object dtTo)
        {
            DateTime dt1 = ConvertToDateTimeDefaultException(dtFrom);
            DateTime dt2 = ConvertToDateTimeDefaultException(dtTo);
            TimeSpan ts = dt2 - dt1;
            return ts.TotalMinutes;
        }

        /// <summary>
        /// 获得两个日期相减后的差的总计秒钟
        /// </summary>
        /// <param name="dtFrom">小日期</param>
        /// <param name="dtTo">大日期</param>
        /// <returns></returns>
        public static double GetDiffTotalSecondsByDate(object dtFrom, object dtTo)
        {
            DateTime dt1 = ConvertToDateTimeDefaultException(dtFrom);
            DateTime dt2 = ConvertToDateTimeDefaultException(dtTo);
            TimeSpan ts = dt2 - dt1;
            return ts.TotalSeconds;
        }

        /// <summary>
        /// 获得两个日期相减后的差的总计微秒
        /// </summary>
        /// <param name="dtFrom">小日期</param>
        /// <param name="dtTo">大日期</param>
        /// <returns></returns>
        public static double GetDiffTotalMillisecondsByDate(object dtFrom, object dtTo)
        {
            DateTime dt1 = ConvertToDateTimeDefaultException(dtFrom);
            DateTime dt2 = ConvertToDateTimeDefaultException(dtTo);
            TimeSpan ts = dt2 - dt1;
            return ts.TotalMilliseconds;
        }

        /// <summary>
        /// 获得两个日期相减后的差的天数
        /// </summary>
        /// <param name="dtFrom">小日期</param>
        /// <param name="dtTo">大日期</param>
        /// <returns></returns>
        public static double GetDiffDaysByDate(object dtFrom, object dtTo)
        {
            DateTime dt1 = ConvertToDateTimeDefaultException(dtFrom);
            DateTime dt2 = ConvertToDateTimeDefaultException(dtTo);
            TimeSpan ts = dt2 - dt1;
            return ts.Days;
        }

        /// <summary>
        /// 获得两个日期相减后的差的小时
        /// </summary>
        /// <param name="dtFrom">小日期</param>
        /// <param name="dtTo">大日期</param>
        /// <returns></returns>
        public static double GetDiffHoursByDate(object dtFrom, object dtTo)
        {
            DateTime dt1 = ConvertToDateTimeDefaultException(dtFrom);
            DateTime dt2 = ConvertToDateTimeDefaultException(dtTo);
            TimeSpan ts = dt2 - dt1;
            return ts.Hours;
        }

        /// <summary>
        /// 获得两个日期相减后的差的分钟
        /// </summary>
        /// <param name="dtFrom">小日期</param>
        /// <param name="dtTo">大日期</param>
        /// <returns></returns>
        public static double GetDiffMinutesByDate(object dtFrom, object dtTo)
        {
            DateTime dt1 = ConvertToDateTimeDefaultException(dtFrom);
            DateTime dt2 = ConvertToDateTimeDefaultException(dtTo);
            TimeSpan ts = dt2 - dt1;
            return ts.Minutes;
        }

        /// <summary>
        /// 获得两个日期相减后的差的秒钟
        /// </summary>
        /// <param name="dtFrom">小日期</param>
        /// <param name="dtTo">大日期</param>
        /// <returns></returns>
        public static double GetDiffSecondsByDate(object dtFrom, object dtTo)
        {
            DateTime dt1 = ConvertToDateTimeDefaultException(dtFrom);
            DateTime dt2 = ConvertToDateTimeDefaultException(dtTo);
            TimeSpan ts = dt2 - dt1;
            return ts.Seconds;
        }

        /// <summary>
        /// 获得两个日期相减后的差的微秒
        /// </summary>
        /// <param name="dtFrom">小日期</param>
        /// <param name="dtTo">大日期</param>
        /// <returns></returns>
        public static double GetDiffMillisecondsByDate(object dtFrom, object dtTo)
        {
            DateTime dt1 = ConvertToDateTimeDefaultException(dtFrom);
            DateTime dt2 = ConvertToDateTimeDefaultException(dtTo);
            TimeSpan ts = dt2 - dt1;
            return ts.Milliseconds;
        }
        #endregion

        #region 时间戳
        /// <summary>  
        /// 将c# DateTime时间格式转换为Unix时间戳格式  
        /// </summary>  
        /// <param name="time">时间</param>  
        /// <returns>long</returns>  
        public static long ConvertDateTimeToInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            long t = (time.Ticks - startTime.Ticks) / 10000;   //除10000调整为13位      
            return t;
        }

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static string GetTimeStamp(System.DateTime time, int length = 13)
        {
            long ts = ConvertDateTimeToInt(time);
            return ts.ToString().Substring(0, length);
        }

        #endregion 
    }
}
