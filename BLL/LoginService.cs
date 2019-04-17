namespace BLL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Security;

    public static class LoginService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;       

        /// <summary>
        /// �û���¼�ɹ�����
        /// </summary>
        /// <param name="loginname">��¼�ɹ���</param>
        /// <param name="password">δ��������</param>
        /// <param name="rememberMe">��ס�ҿ���</param>
        /// <param name="page">����ҳ��</param>
        /// <returns>�Ƿ��¼�ɹ�</returns>
        public static bool UserLogOn(string account, string password, bool rememberMe, System.Web.UI.Page page)
        {
            List<Model.Sys_User> x = (from y in Funs.DB.Sys_User
                    where y.Account == account && y.IsPost == true && y.Password == Funs.EncryptionPassword(password)
                    select y).ToList();
            if (x.Any())
            {
                FormsAuthentication.SetAuthCookie(account, false);
                page.Session[SessionName.CurrUser] = x.First();
                if (rememberMe)
                {
                    System.Web.HttpCookie u = new System.Web.HttpCookie("UserInfo");
                    u["username"] = account;
                    u["password"] = password;                   
                    // Cookies����ʱ������Ϊһ��.
                    u.Expires = DateTime.Now.AddYears(1);
                    page.Response.Cookies.Add(u);
                }
                else
                {
                    // ��ѡ�񲻱����û���ʱ,Cookies����ʱ������Ϊ����.
                    page.Response.Cookies["UserInfo"].Expires = DateTime.Now.AddDays(-1);
                }

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}