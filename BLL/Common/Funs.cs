namespace BLL
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    /// 通用方法类。
    /// </summary>
    public static class Funs
    {
        /// <summary>
        /// 维护一个DB集合
        /// </summary>
        private static Dictionary<int, Model.HSSEDB_ENN> dataBaseLinkList = new System.Collections.Generic.Dictionary<int, Model.HSSEDB_ENN>();


        /// <summary>
        /// 维护一个DB集合
        /// </summary>
        public static System.Collections.Generic.Dictionary<int, Model.HSSEDB_ENN> DBList
        {
            get
            {
                return dataBaseLinkList;
            }
        }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        private static string connString;

        /// <summary>
        /// 数据库连结字符串。
        /// </summary>
        public static string ConnString
        {
            get
            {
                if (connString == null)
                {
                    throw new NotSupportedException("请设置连接字符串！");
                }

                return connString;
            }

            set
            {
                if (connString != null)
                {
                    throw new NotSupportedException("连接已设置！");
                }
                
                connString = value;
            }
        }

        /// <summary>
        /// 系统名称
        /// </summary>
        public static string SystemName
        {
            get;
            set;
        }

        /// <summary>
        /// 服务器路径
        /// </summary>
        public static string RootPath
        {
            get;
            set;
        }
        
        /// <summary>
        /// 软件版本
        /// </summary>
        public static string SystemVersion
        {
            get;
            set;
        }
        /// <summary>
        /// APP下载地址
        /// </summary>
        public static string APPUrl
        {
            get;
            set;
        }
        /// <summary>
        /// 数据库上下文。
        /// </summary>
        public static Model.HSSEDB_ENN DB
        {
            get
            {
                if (!DBList.ContainsKey(System.Threading.Thread.CurrentThread.ManagedThreadId))
                {
                    DBList.Add(System.Threading.Thread.CurrentThread.ManagedThreadId, new Model.HSSEDB_ENN(connString));
                }

                // DBList[System.Threading.Thread.CurrentThread.ManagedThreadId].CommandTimeout = 1200;
                return DBList[System.Threading.Thread.CurrentThread.ManagedThreadId];
            }
        }

        /// <summary>
        /// 加密密码
        /// </summary>
        /// <param name="password">加密前的密码</param>
        /// <returns>加密后的密码</returns>
        public static string EncryptionPassword(string password)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");
        }

        ///// <summary>
        ///// 为目标下拉框加上 "请选择" 项
        ///// </summary>
        ///// <param name="DLL">目标下拉框</param>
        //public static void PleaseSelect(System.Web.UI.WebControls.DropDownList DDL)
        //{
        //    DDL.Items.Insert(0, new System.Web.UI.WebControls.ListItem("- 请选择 -", "0"));
        //    return;
        //}

        /// <summary>
        /// 为目标下拉框加上 "请选择" 项
        /// </summary>
        /// <param name="DLL">目标下拉框</param>
        public static void FineUIPleaseSelect(FineUIPro.DropDownList DDL)
        {
            DDL.Items.Insert(0, new FineUIPro.ListItem("- 请选择 -", BLL.Const._Null));
            return;
        }
              
        /// <summary>
        /// 为目标下拉框加上选择内容
        /// </summary>
        /// <param name="DLL">目标下拉框</param>
        public static void FineUIPleaseSelect(FineUIPro.DropDownList DDL, string text)
        {
            DDL.Items.Insert(0, new FineUIPro.ListItem(text, BLL.Const._Null));
            return;
        }

        /// <summary>
        /// 为目标下拉框加上 "重新编制" 项
        /// </summary>
        /// <param name="DLL">目标下拉框</param>
        public static void ReCompileSelect(System.Web.UI.WebControls.DropDownList DDL)
        {
            DDL.Items.Insert(0, new System.Web.UI.WebControls.ListItem("重新编制", "0"));
            return;
        }

        /// <summary>
        /// 字符串是否为浮点数
        /// </summary>
        /// <param name="decimalStr">要检查的字符串</param>
        /// <returns>返回是或否</returns>
        public static bool IsDecimal(string decimalStr)
        {
            if (String.IsNullOrEmpty(decimalStr))
            {
                return false;
            }

            try
            {
                Convert.ToDecimal(decimalStr, NumberFormatInfo.InvariantInfo);
                return true;
            }
            catch (Exception ex)
            {
                ErrLogInfo.WriteLog(string.Empty, ex);
                return false;
            }
        }

        /// <summary>
        /// 判断一个字符串是否是整数
        /// </summary>
        /// <param name="integerStr">要检查的字符串</param>
        /// <returns>返回是或否</returns>
        public static bool IsInteger(string integerStr)
        {
            if (String.IsNullOrEmpty(integerStr))
            {
                return false;
            }

            try
            {
                Convert.ToInt32(integerStr, NumberFormatInfo.InvariantInfo);
                return true;
            }
            catch (Exception ex)
            {
                ErrLogInfo.WriteLog(string.Empty, ex);
                return false;
            }
        }

        /// <summary>
        /// 获取新的数字
        /// </summary>
        /// <param name="number">要转换的数字</param>
        /// <returns>新的数字</returns>
        public static string InterceptDecimal(object number)
        {
            if (number == null)
            {
                return null;
            }
            decimal newNumber = 0;
            string newNumberStr = "";
            int an = -1;
            string numberStr = number.ToString();
            int n = numberStr.IndexOf(".");
            if (n == -1)
            {
                return numberStr;
            }
            for (int i = n + 1; i < numberStr.Length; i++)
            {
                string str = numberStr.Substring(i, 1);
                if (str == "0")
                {
                    if (GetStr(numberStr, i))
                    {
                        an = i;
                        break;
                    }
                }
            }
            if (an == -1)
            {
                newNumber = Convert.ToDecimal(numberStr);
            }
            else if (an == n + 1)
            {

                newNumberStr = numberStr.Substring(0, an - 1);
                newNumber = Convert.ToDecimal(newNumberStr);
            }
            else
            {
                newNumberStr = numberStr.Substring(0, an);
                newNumber = Convert.ToDecimal(newNumberStr);
            }
            return newNumber.ToString();
        }

        /// <summary>
        /// 判断字符串从第n位开始以后是否都为0
        /// </summary>
        /// <param name="number">要判断的字符串</param>
        /// <param name="n">开始的位数</param>
        /// <returns>false不都为0，true都为0</returns>
        public static bool GetStr(string number, int n)
        {
            for (int i = n; i < number.Length; i++)
            {
                if (number.Substring(i, 1) != "0")
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 截取字符串长度
        /// </summary>
        /// <param name="str">要截取的字符串</param>
        /// <param name="n">长度</param>
        /// <returns>截取后字符串</returns>
        public static string GetSubStr(object str, object n)
        {
            if (str != null)
            {
                if (str.ToString().Length > Convert.ToInt32(n))
                {
                    return str.ToString().Substring(0, Convert.ToInt32(n)) + "....";
                }
                else
                {
                    return str.ToString();
                }
            }
            return "";
        }

        /// <summary>
        /// 根据标识返回字符串list
        /// </summary>
        /// <param name="str"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static List<string> GetStrListByStr(string str, char n)
        {
            List<string> strList = new List<string>();
            if (!string.IsNullOrEmpty(str))
            {
                strList.AddRange(str.Split(n));
            }

            return strList;
        }

        #region 数字转换
        /// <summary>
        /// 输入文本转换数字类型
        /// </summary>
        /// <returns></returns>
        public static decimal GetNewDecimalOrZero(string value)
        {
            decimal revalue = 0;
            if (!String.IsNullOrEmpty(value))
            {
                try
                {
                    revalue = decimal.Parse(value);
                }
                catch (Exception ex)
                {
                    ErrLogInfo.WriteLog(string.Empty, ex);

                }
            }

            return revalue;
        }

        /// <summary>
        /// 输入文本转换数字类型
        /// </summary>
        /// <returns></returns>
        public static decimal? GetNewDecimal(string value)
        {
            decimal? revalue = null;
            if (!String.IsNullOrEmpty(value))
            {
                try
                {
                    revalue = decimal.Parse(value);
                }
                catch (Exception ex)
                {
                    ErrLogInfo.WriteLog(string.Empty, ex);

                }
            }

            return revalue;
        }

        /// <summary>
        /// 输入文本转换数字类型
        /// </summary>
        /// <returns></returns>
        public static int? GetNewInt(string value)
        {
            if (!String.IsNullOrEmpty(value))
            {
                try
                {
                    return Int32.Parse(value);
                }
                catch (Exception ex)
                {
                    ErrLogInfo.WriteLog(string.Empty, ex);
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 输入文本转换数字类型
        /// </summary>
        /// <returns></returns>
        public static int GetNewIntOrZero(string value)
        {
            int revalue = 0;
            if (!String.IsNullOrEmpty(value))
            {
                try
                {
                    revalue = Int32.Parse(value);
                }
                catch (Exception ex)
                {
                    ErrLogInfo.WriteLog(string.Empty, ex);

                }
            }

            return revalue;
        }
        #endregion

        /// <summary>
        /// 指定上传文件的名称
        /// </summary>
        /// <returns></returns>
        public static string GetNewFileName()
        {
            Random rm = new Random(System.Environment.TickCount);
            return System.DateTime.Now.ToString("yyyyMMddhhmmss") + rm.Next(1000, 9999).ToString();
        }

        #region 时间转换
        /// <summary>
        /// 输入文本转换时间类型
        /// </summary>
        /// <returns></returns>
        public static DateTime? GetNewDateTime(string time)
        {
            try
            {
                if (!String.IsNullOrEmpty(time))
                {
                    return DateTime.Parse(time);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                //ErrLogInfo.WriteLog(string.Empty, ex);
                return null;
            }
        }

        /// <summary>
        /// 输入文本转换时间类型（空时：默认当前时间）
        /// </summary>
        /// <returns></returns>
        public static DateTime GetNewDateTimeOrNow(string time)
        {
            try
            {
                if (!String.IsNullOrEmpty(time))
                {
                    return DateTime.Parse(time);
                }
                else
                {
                    return System.DateTime.Now;
                }
            }
            catch (Exception ex)
            {
               // ErrLogInfo.WriteLog(string.Empty, ex);
                return System.DateTime.Now;
            }
        }

        /// <summary>
        /// 根据时间获取是哪个季度
        /// </summary>
        /// <returns></returns>
        public static string GetQuarterlyByTime(DateTime time)
        {
            string quarterly = string.Empty;
            string yearName = time.Year.ToString();
            int month = time.Month;
            string name = string.Empty;
            if (month >= 1 && month <= 3)
            {
                name = "第一季度";
            }
            else if (month >= 4 && month <= 6)
            {
                name = "第二季度";
            }
            else if (month >= 7 && month <= 9)
            {
                name = "第三季度";
            }
            else if (month >= 10 && month <= 12)
            {
                name = "第四季度";
            }

            quarterly = yearName + "年" + name;
            return quarterly;
        }

        /// <summary>
        /// 根据时间获取是哪个季度
        /// </summary>
        /// <returns></returns>
        public static int GetNowQuarterlyByTime(DateTime time)
        {
            int quarterly = 0;
            int month = time.Month;
            if (month >= 1 && month <= 3)
            {
                quarterly = 1;
            }
            else if (month >= 4 && month <= 6)
            {
                quarterly = 2;
            }
            else if (month >= 7 && month <= 9)
            {
                quarterly = 3;
            }
            else if (month >= 10 && month <= 12)
            {
                quarterly = 4;
            }

            return quarterly;
        }

        /// <summary>
        /// 根据月获取是哪个季度
        /// </summary>
        /// <returns></returns>
        public static int GetNowQuarterlyByMonth(int month)
        {
            int quarterly = 0;
            if (month >= 1 && month <= 3)
            {
                quarterly = 1;
            }
            else if (month >= 4 && month <= 6)
            {
                quarterly = 2;
            }
            else if (month >= 7 && month <= 9)
            {
                quarterly = 3;
            }
            else if (month >= 10 && month <= 12)
            {
                quarterly = 4;
            }

            return quarterly;
        }
        /// <summary>
        /// 根据时间获取是哪个季度
        /// </summary>
        /// <returns></returns>
        public static string GetQuarterlyNameByMonth(int month)
        {
            string name = string.Empty;
            if (month >= 1 && month <= 3)
            {
                name = "第一季度";
            }
            else if (month >= 4 && month <= 6)
            {
                name = "第二季度";
            }
            else if (month >= 7 && month <= 9)
            {
                name = "第三季度";
            }
            else if (month >= 10 && month <= 12)
            {
                name = "第四季度";
            }

            return name;
        }
        /// <summary>
        /// 根据时间获取是上、下半年
        /// </summary>
        /// <returns></returns>
        public static int GetNowHalfYearByTime(DateTime time)
        {
            int quarterly = 1;
            int month = time.Month;
            if (month >= 1 && month <= 6)
            {
                quarterly = 1;
            }
            else
            {
                quarterly = 2;
            }

            return quarterly;
        }

        /// <summary>
        /// 根据时间获取是上、下半年
        /// </summary>
        /// <returns></returns>
        public static int GetNowHalfYearByMonth(int month)
        {
            int halfYear = 1;
            if (month >= 1 && month <= 6)
            {
                halfYear = 1;
            }
            else
            {
                halfYear = 2;
            }

            return halfYear;
        }

        /// <summary>
        /// 根据时间获取是上、下半年
        /// </summary>
        /// <returns></returns>
        public static string GetNowHalfYearNameByTime(DateTime time)
        {
            string quarterly = "上半年";
            int month = time.Month;
            if (month >= 1 && month <= 6)
            {
                quarterly = "上半年";
            }
            else
            {
                quarterly = "下半年";
            }

            return quarterly;
        }
        #endregion

        /// <summary>
        /// 相差月份
        /// </summary>
        /// <param name="datetime2"></param>
        /// <param name="datetime2"></param>
        /// <returns></returns>
        public static int CompareMonths(DateTime datetime1, DateTime datetime2)
        {
            DateTime dt = datetime1;
            DateTime dt2 = datetime2;
            if (DateTime.Compare(dt, dt2) < 0)
            {
                dt2 = dt;
                dt = datetime2;
            }
            int year = dt.Year - dt2.Year;
            int month = dt.Month - dt2.Month;
            month = year * 12 + month;
            if (dt.Day - dt2.Day < -15)
            {
                month--;
            }
            else if (dt.Day - dt2.Day > 14)
            {
                month++;
            }
            return month;
        }

        /// <summary>
        /// 得到ID字符串
        /// </summary>
        /// <param name="bigType"></param>
        /// <returns></returns>
        public static string ConvertString(string[] StringIds)
        {
            string stringValues = null;
            if (StringIds != null && StringIds.Count() > 0)
            {
                foreach (string id in StringIds)
                {
                    if (id != BLL.Const._Null)
                    {
                        stringValues += id + ",";
                    }
                }
                if (!string.IsNullOrEmpty(stringValues))
                {
                    stringValues = stringValues.Substring(0, stringValues.Length - 1); ;
                }
            }

            return stringValues;
        }

        /// <summary>
        /// 得到最大排序值+1
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columnName">序号列名</param>
        /// <param name="sortName">条件列名</param>
        /// <param name="sortValue">条件值</param>
        /// <returns></returns>
        public static int GetMaxIndex(string tableName, string columnName, string sortName,string sortValue)
        {
            int maxSortIndex = 0;
            string str = "SELECT (ISNULL(MAX(" + columnName + "),0)+1) FROM " + tableName;
            if (!string.IsNullOrEmpty(sortName))
            {
                str += " WHERE "+ sortName + "='" + sortValue + "'";
            }

            maxSortIndex = BLL.SQLHelper.getIntValue(str);
            return maxSortIndex;
        }
    }
}

