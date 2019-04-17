namespace BLL
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    /// ͨ�÷����ࡣ
    /// </summary>
    public static class Funs
    {
        /// <summary>
        /// ά��һ��DB����
        /// </summary>
        private static Dictionary<int, Model.HSSEDB_ENN> dataBaseLinkList = new System.Collections.Generic.Dictionary<int, Model.HSSEDB_ENN>();


        /// <summary>
        /// ά��һ��DB����
        /// </summary>
        public static System.Collections.Generic.Dictionary<int, Model.HSSEDB_ENN> DBList
        {
            get
            {
                return dataBaseLinkList;
            }
        }

        /// <summary>
        /// ���ݿ������ַ���
        /// </summary>
        private static string connString;

        /// <summary>
        /// ���ݿ������ַ�����
        /// </summary>
        public static string ConnString
        {
            get
            {
                if (connString == null)
                {
                    throw new NotSupportedException("�����������ַ�����");
                }

                return connString;
            }

            set
            {
                if (connString != null)
                {
                    throw new NotSupportedException("���������ã�");
                }
                
                connString = value;
            }
        }

        /// <summary>
        /// ϵͳ����
        /// </summary>
        public static string SystemName
        {
            get;
            set;
        }

        /// <summary>
        /// ������·��
        /// </summary>
        public static string RootPath
        {
            get;
            set;
        }
        
        /// <summary>
        /// ����汾
        /// </summary>
        public static string SystemVersion
        {
            get;
            set;
        }
        /// <summary>
        /// APP���ص�ַ
        /// </summary>
        public static string APPUrl
        {
            get;
            set;
        }
        /// <summary>
        /// ���ݿ������ġ�
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
        /// ��������
        /// </summary>
        /// <param name="password">����ǰ������</param>
        /// <returns>���ܺ������</returns>
        public static string EncryptionPassword(string password)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");
        }

        ///// <summary>
        ///// ΪĿ����������� "��ѡ��" ��
        ///// </summary>
        ///// <param name="DLL">Ŀ��������</param>
        //public static void PleaseSelect(System.Web.UI.WebControls.DropDownList DDL)
        //{
        //    DDL.Items.Insert(0, new System.Web.UI.WebControls.ListItem("- ��ѡ�� -", "0"));
        //    return;
        //}

        /// <summary>
        /// ΪĿ����������� "��ѡ��" ��
        /// </summary>
        /// <param name="DLL">Ŀ��������</param>
        public static void FineUIPleaseSelect(FineUIPro.DropDownList DDL)
        {
            DDL.Items.Insert(0, new FineUIPro.ListItem("- ��ѡ�� -", BLL.Const._Null));
            return;
        }
              
        /// <summary>
        /// ΪĿ�����������ѡ������
        /// </summary>
        /// <param name="DLL">Ŀ��������</param>
        public static void FineUIPleaseSelect(FineUIPro.DropDownList DDL, string text)
        {
            DDL.Items.Insert(0, new FineUIPro.ListItem(text, BLL.Const._Null));
            return;
        }

        /// <summary>
        /// ΪĿ����������� "���±���" ��
        /// </summary>
        /// <param name="DLL">Ŀ��������</param>
        public static void ReCompileSelect(System.Web.UI.WebControls.DropDownList DDL)
        {
            DDL.Items.Insert(0, new System.Web.UI.WebControls.ListItem("���±���", "0"));
            return;
        }

        /// <summary>
        /// �ַ����Ƿ�Ϊ������
        /// </summary>
        /// <param name="decimalStr">Ҫ�����ַ���</param>
        /// <returns>�����ǻ��</returns>
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
        /// �ж�һ���ַ����Ƿ�������
        /// </summary>
        /// <param name="integerStr">Ҫ�����ַ���</param>
        /// <returns>�����ǻ��</returns>
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
        /// ��ȡ�µ�����
        /// </summary>
        /// <param name="number">Ҫת��������</param>
        /// <returns>�µ�����</returns>
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
        /// �ж��ַ����ӵ�nλ��ʼ�Ժ��Ƿ�Ϊ0
        /// </summary>
        /// <param name="number">Ҫ�жϵ��ַ���</param>
        /// <param name="n">��ʼ��λ��</param>
        /// <returns>false����Ϊ0��true��Ϊ0</returns>
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
        /// ��ȡ�ַ�������
        /// </summary>
        /// <param name="str">Ҫ��ȡ���ַ���</param>
        /// <param name="n">����</param>
        /// <returns>��ȡ���ַ���</returns>
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
        /// ���ݱ�ʶ�����ַ���list
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

        #region ����ת��
        /// <summary>
        /// �����ı�ת����������
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
        /// �����ı�ת����������
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
        /// �����ı�ת����������
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
        /// �����ı�ת����������
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
        /// ָ���ϴ��ļ�������
        /// </summary>
        /// <returns></returns>
        public static string GetNewFileName()
        {
            Random rm = new Random(System.Environment.TickCount);
            return System.DateTime.Now.ToString("yyyyMMddhhmmss") + rm.Next(1000, 9999).ToString();
        }

        #region ʱ��ת��
        /// <summary>
        /// �����ı�ת��ʱ������
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
        /// �����ı�ת��ʱ�����ͣ���ʱ��Ĭ�ϵ�ǰʱ�䣩
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
        /// ����ʱ���ȡ���ĸ�����
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
                name = "��һ����";
            }
            else if (month >= 4 && month <= 6)
            {
                name = "�ڶ�����";
            }
            else if (month >= 7 && month <= 9)
            {
                name = "��������";
            }
            else if (month >= 10 && month <= 12)
            {
                name = "���ļ���";
            }

            quarterly = yearName + "��" + name;
            return quarterly;
        }

        /// <summary>
        /// ����ʱ���ȡ���ĸ�����
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
        /// �����»�ȡ���ĸ�����
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
        /// ����ʱ���ȡ���ĸ�����
        /// </summary>
        /// <returns></returns>
        public static string GetQuarterlyNameByMonth(int month)
        {
            string name = string.Empty;
            if (month >= 1 && month <= 3)
            {
                name = "��һ����";
            }
            else if (month >= 4 && month <= 6)
            {
                name = "�ڶ�����";
            }
            else if (month >= 7 && month <= 9)
            {
                name = "��������";
            }
            else if (month >= 10 && month <= 12)
            {
                name = "���ļ���";
            }

            return name;
        }
        /// <summary>
        /// ����ʱ���ȡ���ϡ��°���
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
        /// ����ʱ���ȡ���ϡ��°���
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
        /// ����ʱ���ȡ���ϡ��°���
        /// </summary>
        /// <returns></returns>
        public static string GetNowHalfYearNameByTime(DateTime time)
        {
            string quarterly = "�ϰ���";
            int month = time.Month;
            if (month >= 1 && month <= 6)
            {
                quarterly = "�ϰ���";
            }
            else
            {
                quarterly = "�°���";
            }

            return quarterly;
        }
        #endregion

        /// <summary>
        /// ����·�
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
        /// �õ�ID�ַ���
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
        /// �õ��������ֵ+1
        /// </summary>
        /// <param name="tableName">����</param>
        /// <param name="columnName">�������</param>
        /// <param name="sortName">��������</param>
        /// <param name="sortValue">����ֵ</param>
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

