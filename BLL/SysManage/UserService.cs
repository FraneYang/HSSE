namespace BLL
{
    using System.Collections.Generic;
    using System.Linq;

    public static class UserService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;        

        /// <summary>
        /// ��ȡ�û���Ϣ
        /// </summary>
        /// <param name="userId">�û�Id</param>
        /// <returns>�û���Ϣ</returns>
        public static Model.Sys_User GetUserByUserId(string userId)
        {
            return Funs.DB.Sys_User.FirstOrDefault(e => e.UserId == userId);
        }

        /// <summary>
        /// ��ȡ�û��˺��Ƿ����
        /// </summary>
        /// <param name="userId">�û�id</param>
        /// <param name="account">�˺�</param>
        /// <returns>�Ƿ����</returns>
        public static bool IsExistUserAccount(string userId, string account)
        {
            bool isExist = false;
            var role = Funs.DB.Sys_User.FirstOrDefault(x => x.Account == account && x.UserId != userId);
            if (role != null)
            {
                isExist = true;
            }
            return isExist;
        }

        /// <summary>
        /// ��ȡ�û��˺��Ƿ����
        /// </summary>
        /// <param name="userId">�û�id</param>
        /// <param name="identityCard">���֤����</param>
        /// <returns>�Ƿ����</returns>
        public static bool IsExistUserIdentityCard(string userId, string identityCard)
        {
            bool isExist = false;
            var role = Funs.DB.Sys_User.FirstOrDefault(x => x.IdentityCard == identityCard && x.UserId != userId);
            if (role != null)
            {
                isExist = true;
            }
            return isExist;
        }

        /// <summary>
        /// �����û���ȡ����
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static string GetPasswordByUserId(string userId)
        {
            Model.Sys_User m = Funs.DB.Sys_User.FirstOrDefault(e => e.UserId == userId);
            return m.Password;
        }

        /// <summary>
        /// �����û���ȡ�û�����
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static string GetUserNameByUserId(string userId)
        {
            string userName = string.Empty;
            Model.Sys_User user = Funs.DB.Sys_User.FirstOrDefault(e => e.UserId == userId);
            if (user != null)
            {
                userName = user.UserName; 
            }

            return userName;
        }

        /// <summary>
        /// �޸�����
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        public static void UpdatePassword(string userId, string password)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Sys_User m = db.Sys_User.FirstOrDefault(e => e.UserId == userId);
            if (m != null)
            {
                m.Password = Funs.EncryptionPassword(password);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// ������Ա��Ϣ
        /// </summary>
        /// <param name="user">��Աʵ��</param>
        public static void AddUser(Model.Sys_User user)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.Sys_User));
            Model.Sys_User newUser = new Model.Sys_User();
            newUser.UserId = newKeyID;
            newUser.InstallationId = user.InstallationId;
            newUser.UnitId = user.UnitId;
            newUser.DepartId = user.DepartId;
            newUser.Account = user.Account;
            newUser.UserCode = user.UserCode;
            newUser.Password = user.Password;
            newUser.UserName = user.UserName;
            newUser.RoleId = user.RoleId;
            newUser.WorkPostId = user.WorkPostId;
            newUser.IsPost = user.IsPost;
            newUser.Sex = user.Sex;
            newUser.BirthDay = user.BirthDay;
            newUser.Marriage = user.Marriage;
            newUser.Nation = user.Nation;
            newUser.IdentityCard = user.IdentityCard;
            newUser.Email = user.Email;
            newUser.Telephone = user.Telephone;
            newUser.Education = user.Education;
            newUser.Hometown = user.Hometown;
            newUser.PositionId = user.PositionId;
            newUser.PhotoUrl = user.PhotoUrl;
            newUser.Performance = user.Performance;
            newUser.PageSize = user.PageSize;          
            db.Sys_User.InsertOnSubmit(newUser);
            db.SubmitChanges();
        }

        /// <summary>
        /// �޸���Ա��Ϣ
        /// </summary>
        /// <param name="user">��Աʵ��</param>
        public static void UpdateUser(Model.Sys_User user)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Sys_User newUser = db.Sys_User.FirstOrDefault(e => e.UserId == user.UserId);
            if (newUser != null)
            {
                newUser.InstallationId = user.InstallationId;
                newUser.UnitId = user.UnitId;
                newUser.DepartId = user.DepartId;
                newUser.Account = user.Account;
                newUser.UserCode = user.UserCode;
                if (!string.IsNullOrEmpty(user.Password))
                {
                    newUser.Password = user.Password;
                }
                newUser.UserName = user.UserName;
                newUser.RoleId = user.RoleId;
                newUser.WorkPostId = user.WorkPostId;
                newUser.IsPost = user.IsPost;
                newUser.Sex = user.Sex;
                newUser.BirthDay = user.BirthDay;
                newUser.Marriage = user.Marriage;
                newUser.Nation = user.Nation;
                newUser.IdentityCard = user.IdentityCard;
                newUser.Email = user.Email;
                newUser.Telephone = user.Telephone;
                newUser.Education = user.Education;
                newUser.Hometown = user.Hometown;
                newUser.PositionId = user.PositionId;
                newUser.PhotoUrl = user.PhotoUrl;
                newUser.Performance = user.Performance;
                newUser.PageSize = user.PageSize;
                db.SubmitChanges();
            }
        }
        
        /// <summary>
        /// ������ԱIdɾ��һ����Ա��Ϣ
        /// </summary>
        /// <param name="userId"></param>
        public static void DeleteUser(string userId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Sys_User user = db.Sys_User.FirstOrDefault(e => e.UserId == userId);
            if (user != null)
            {
                var logs = from x in db.Sys_Log where x.UserId == userId select x;
                if (logs.Count() > 0)
                {
                    db.Sys_Log.DeleteAllOnSubmit(logs);
                }
                db.Sys_User.DeleteOnSubmit(user);
                db.SubmitChanges();
            }
        }
        
        /// <summary>
        /// ��ȡ�û�����ѡ��
        /// </summary>
        /// <returns></returns>
        public static List<Model.Sys_User> GetUserList()
        {
            var list = (from x in Funs.DB.Sys_User orderby x.UserName select x).ToList();
            return list;
        }

        /// <summary>
        /// ��ȡ�û�����ѡ��  ��Ŀ ��ɫ �ҿ�����
        /// </summary>
        /// <returns></returns>
        public static List<Model.SpSysUserItem> GetRoleUserList(string unitId, string departId, string installationId)
        {
            IQueryable<Model.SpSysUserItem> users = (from x in Funs.DB.Sys_User
                                                     where x.IsPost == true
                                                     orderby x.UserName
                                                     select new Model.SpSysUserItem
                                                     {
                                                         UnitId = x.UnitId,
                                                         DepartId = x.DepartId,
                                                         InstallationId = x.InstallationId,
                                                         UserId = x.UserId,
                                                         UserName = x.UserName,
                                                     });
            if (!string.IsNullOrEmpty(unitId))
            {
                users = users.Where(x => x.UnitId == unitId);
            }
            if (!string.IsNullOrEmpty(departId))
            {
                users = users.Where(x => x.DepartId == departId);
            }
            if (!string.IsNullOrEmpty(installationId))
            {
                users = users.Where(x => x.InstallationId == installationId);
            }

            return users.ToList();
        }

        /// <summary>
        /// ������Ŀ�ź͵�λId��ȡ�û�����ѡ��
        /// </summary>
        /// <returns></returns>
        public static List<Model.Sys_User> GetUserListByProjectIdAndUnitId(string projectId, string unitId)
        {
            string setUnitId = BLL.CommonService.GetUnitId(unitId);
            List<Model.Sys_User> list = new List<Model.Sys_User>();
            list = (from x in Funs.DB.Sys_User
                    where x.UnitId == unitId
                    orderby x.UserName
                    select x).ToList();

            return list;
        }

       
        #region �û�������
        /// <summary>
        /// �û�������
        /// </summary>
        /// <param name="dropName">����������</param>
        /// <param name="projectId">��Ŀid</param>
        /// <param name="isShowPlease">�Ƿ���ʾ��ѡ��</param>
        public static void InitUserDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "UserId";
            dropName.DataTextField = "UserName";
            dropName.DataSource = BLL.UserService.GetUserList();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }

        /// <summary>
        /// ����ɫ�û������� 
        /// </summary>
        /// <param name="dropName">����������</param>
        /// <param name="projectId">��Ŀid</param>
        /// <param name="isShowPlease">�Ƿ���ʾ��ѡ��</param>
        public static void InitFlowOperateControlUserDropDownList(FineUIPro.DropDownList dropName, string unitId,string departId, string installationId, bool isShowPlease)
        {
            dropName.DataValueField = "UserId";
            dropName.DataTextField = "UserName";
            dropName.DataSource = BLL.UserService.GetRoleUserList(unitId, departId, installationId);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }

        #endregion
    }
}
