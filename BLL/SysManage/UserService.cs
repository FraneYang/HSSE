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
            Model.Sys_User newUser = new Model.Sys_User
            {
                UserId = SQLHelper.GetNewID(typeof(Model.Sys_User)),
                InstallationId = user.InstallationId,
                InstallationName = user.InstallationName,
                UnitId = user.UnitId,
                DepartId = user.DepartId,
                Account = user.Account,
                UserCode = user.UserCode,
                Password = user.Password,
                UserName = user.UserName,
                RoleId = user.RoleId,
                WorkPostId = user.WorkPostId,
                WorkPostName = user.WorkPostName,
                IsPost = user.IsPost,
                Sex = user.Sex,
                BirthDay = user.BirthDay,
                Marriage = user.Marriage,
                Nation = user.Nation,
                IdentityCard = user.IdentityCard,
                Email = user.Email,
                Telephone = user.Telephone,
                Education = user.Education,
                Hometown = user.Hometown,
                PositionId = user.PositionId,
                PhotoUrl = user.PhotoUrl,
                Performance = user.Performance,
                PageSize = user.PageSize,
                SortIndex = user.SortIndex,
                IsEmergency = user.IsEmergency,
                EntryTime = user.EntryTime,
                IsTemp = user.IsTemp,
            };
            
            db.Sys_User.InsertOnSubmit(newUser);
            db.SubmitChanges();

            ////�����û�ʱ ����λѲ����д����ϸ��
            RiskListItemService.getRiskListItemByUser(newUser);
        }

        /// <summary>
        /// �޸���Ա��Ϣ
        /// </summary>
        /// <param name="user">��Աʵ��</param>
        public static void UpdateUser(Model.Sys_User user)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            string oldWorkPost = string.Empty;
            string newWorkPost = user.WorkPostId;
            Model.Sys_User updateUser = db.Sys_User.FirstOrDefault(e => e.UserId == user.UserId);
            if (updateUser != null)
            {
                oldWorkPost = updateUser.WorkPostId;
                updateUser.InstallationId = user.InstallationId;
                updateUser.InstallationName = user.InstallationName;
                updateUser.UnitId = user.UnitId;
                updateUser.DepartId = user.DepartId;
                updateUser.Account = user.Account;
                updateUser.UserCode = user.UserCode;
                if (!string.IsNullOrEmpty(user.Password))
                {
                    updateUser.Password = user.Password;
                }
                updateUser.UserName = user.UserName;
                updateUser.RoleId = user.RoleId;
                updateUser.WorkPostId = user.WorkPostId;
                updateUser.WorkPostName = user.WorkPostName;
                updateUser.IsPost = user.IsPost;
                updateUser.Sex = user.Sex;
                updateUser.BirthDay = user.BirthDay;
                updateUser.Marriage = user.Marriage;
                updateUser.Nation = user.Nation;
                updateUser.IdentityCard = user.IdentityCard;
                updateUser.Email = user.Email;
                if (!string.IsNullOrEmpty(user.Telephone))
                {
                    updateUser.Telephone = user.Telephone;
                }
                updateUser.Education = user.Education;
                updateUser.Hometown = user.Hometown;
                updateUser.PositionId = user.PositionId;
                updateUser.PhotoUrl = user.PhotoUrl;
                updateUser.Performance = user.Performance;
                updateUser.PageSize = user.PageSize;
                updateUser.SortIndex = user.SortIndex;
                updateUser.IsEmergency = user.IsEmergency;
                updateUser.EntryTime = user.EntryTime;
                updateUser.IsTemp = user.IsTemp;
                db.SubmitChanges();
            }

            if (oldWorkPost != newWorkPost)
            {
                ////�û���λ�仯ʱ ����λѲ����д����ϸ��
                RiskListItemService.getRiskListItemByUser(updateUser);
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
                ////ɾ����Ա Ѳ����ϸ
                BLL.RiskListItemService.DeleteRiskListItemByUserId(userId);
                ///ɾ������Ѳ��ƻ���
                BLL.PatrolPlanService.DeletePatrolPlanByUserId(userId);
                ///ɾ����־
                var logs = from x in db.Sys_Log where x.UserId == userId select x;
                if (logs.Count() > 0)
                {
                    db.Sys_Log.DeleteAllOnSubmit(logs);
                }
                ///ɾ��������Ϣ
                var pushRecord = from x in db.Sys_PushRecord
                                 where x.ReceiveManId == userId && (!x.IsResponse.HasValue || x.IsAgree.HasValue)
                                 select x;
                if (pushRecord.Count() > 0)
                {
                    db.Sys_PushRecord.DeleteAllOnSubmit(pushRecord);
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
            var list = (from x in Funs.DB.Sys_User where x.IsPost == true orderby x.UserName select x).ToList();
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
                users = users.Where(x => x.InstallationId.Contains(installationId));
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
        public static void InitFlowOperateControlUserDropDownList(FineUIPro.DropDownList dropName, string unitId, string departId, string installationId, bool isShowPlease)
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
