namespace BLL
{
    using System.Collections.Generic;
    using System.Linq;

    public static class UserDealInstallationService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;        

        /// <summary>
        /// ��ȡ�û���Ϣ
        /// </summary>
        /// <param name="userId">�û�Id</param>
        /// <returns>�û���Ϣ</returns>
        public static List<Model.Sys_UserDealInstallation> GetUserDealInstallationByUserId(string userId)
        {
            return (from x in Funs.DB.Sys_UserDealInstallation where x.UserId == userId select x).ToList();
        }

        /// <summary>
        /// ������Ա��Ϣ
        /// </summary>
        /// <param name="user">��Աʵ��</param>
        public static void AddUserDealInstallation(Model.Sys_UserDealInstallation user)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Sys_UserDealInstallation newUser = new Model.Sys_UserDealInstallation
            {
                UserDealInstallationId = SQLHelper.GetNewID(typeof(Model.Sys_User)),
                InstallationId = user.InstallationId,
                UserId = user.UserId,                
            };
            
            db.Sys_UserDealInstallation.InsertOnSubmit(newUser);
            db.SubmitChanges();
        }

        /// <summary>
        /// ������ԱIdɾ��һ����Ա��Ϣ
        /// </summary>
        /// <param name="userId"></param>
        public static void DeleteUserDealInstallationByUserId(string userId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            var userDealInstallation = from x in db.Sys_UserDealInstallation
                                       where x.UserId == userId
                                       select x;
            if (userDealInstallation.Count() > 0)
            {
                db.Sys_UserDealInstallation.DeleteAllOnSubmit(userDealInstallation);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// ������ԱIdɾ��һ����Ա��Ϣ
        /// </summary>
        /// <param name="userId"></param>
        public static void DeleteDealInstallationByUserDealInstallationId(string userDealInstallationId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            var userDealInstallation = db.Sys_UserDealInstallation.FirstOrDefault(x => x.UserDealInstallationId == userDealInstallationId);
                                      
            if (userDealInstallation != null)
            {
                db.Sys_UserDealInstallation.DeleteOnSubmit(userDealInstallation);
                db.SubmitChanges();
            }
        }
    }
}
