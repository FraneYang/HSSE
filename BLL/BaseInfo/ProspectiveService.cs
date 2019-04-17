using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
 /// <summary>
 /// 准操项目服务类
 /// </summary>
 public static class ProspectiveService
    {
        public static Model.HSSEDB_ENN db = Funs.DB;

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.Base_Prospective GetProspectiveById(string ProspectiveId)
        {
            var dep = Funs.DB.Base_Prospective.FirstOrDefault(x => x.ProspectiveId == ProspectiveId);
            return dep;
        }

        /// <summary>
        /// 添加准操项目信息
        /// </summary>
        /// <param name="Prospective"></param>
        public static void AddProspective(Model.Base_Prospective Prospective)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Base_Prospective newProspective = new Model.Base_Prospective
            {
                ProspectiveId = Prospective.ProspectiveId,
                ProspectiveCode = Prospective.ProspectiveCode,
                ProspectiveName = Prospective.ProspectiveName,
                Remark = Prospective.Remark,
            };
            db.Base_Prospective.InsertOnSubmit(newProspective);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="teamGroup"></param>
        public static void UpdateProspective(Model.Base_Prospective Prospective)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Base_Prospective newProspective = db.Base_Prospective.FirstOrDefault(e => e.ProspectiveId == Prospective.ProspectiveId);
            if (newProspective != null)
            {
                newProspective.ProspectiveCode = Prospective.ProspectiveCode;
                newProspective.ProspectiveName = Prospective.ProspectiveName;
                newProspective.Remark = Prospective.Remark;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="ProspectiveId"></param>
        public static void DeleteProspectiveById(string ProspectiveId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Base_Prospective Prospective = db.Base_Prospective.FirstOrDefault(e => e.ProspectiveId == ProspectiveId);
            {
                db.Base_Prospective.DeleteOnSubmit(Prospective);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 获取类别下拉项
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_Prospective> GetProspectiveList()
        {
            var list = (from x in Funs.DB.Base_Prospective                       
                        orderby x.ProspectiveCode select x).ToList();
            return list;
        }

        #region 表下拉框
        /// <summary>
        ///  表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitProspectiveDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "ProspectiveId";
            dropName.DataTextField = "ProspectiveName";
            dropName.DataSource = BLL.ProspectiveService.GetProspectiveList();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        #endregion

        /// <summary>
        /// 得到准操项目名称字符串
        /// </summary>
        /// <param name="bigType"></param>
        /// <returns></returns>
        public static string getProspectiveNamesProspectiveIds(object ProspectiveIds)
        {
            string ProspectiveName = string.Empty;
            if (ProspectiveIds != null)
            {
                string[] roles = ProspectiveIds.ToString().Split(',');
                foreach (string roleId in roles)
                {
                    var q = GetProspectiveById(roleId);
                    if (q != null)
                    {
                        ProspectiveName += q.ProspectiveName + ",";
                    }
                }
                if (ProspectiveName != string.Empty)
                {
                    ProspectiveName = ProspectiveName.Substring(0, ProspectiveName.Length - 1); ;
                }
            }

            return ProspectiveName;
        }
    }
}