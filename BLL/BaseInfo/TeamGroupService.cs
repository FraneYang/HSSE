using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 班组
    /// </summary>
    public static class TeamGroupService
    {
        public static Model.HSSEDB_ENN db = Funs.DB; 

        /// <summary>
        /// 根据主键获取班组信息
        /// </summary>
        /// <param name="teamGroupId"></param>
        /// <returns></returns>
        public static Model.Base_TeamGroup GetTeamGroupById(string teamGroupId)
        {
            return Funs.DB.Base_TeamGroup.FirstOrDefault(e => e.TeamGroupId == teamGroupId);
        }

        /// <summary>
        /// 添加班组信息
        /// </summary>
        /// <param name="teamGroup"></param>
        public static void AddTeamGroup(Model.Base_TeamGroup teamGroup)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Base_TeamGroup newTeamGroup = new Model.Base_TeamGroup
            {
                TeamGroupId = teamGroup.TeamGroupId,
                UnitId = teamGroup.UnitId,
                InstallationId = teamGroup.InstallationId,
                WorkAreaId = teamGroup.WorkAreaId,
                DepartId = teamGroup.DepartId,              
                TeamGroupCode = teamGroup.TeamGroupCode,
                TeamGroupName = teamGroup.TeamGroupName,
                LeaderIds = teamGroup.LeaderIds,
                LeaderNames = teamGroup.LeaderNames,
                TeamType = teamGroup.TeamType,
                Remark = teamGroup.Remark
            };
            db.Base_TeamGroup.InsertOnSubmit(newTeamGroup);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改班组信息
        /// </summary>
        /// <param name="teamGroup"></param>
        public static void UpdateTeamGroup(Model.Base_TeamGroup teamGroup)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Base_TeamGroup newTeamGroup = db.Base_TeamGroup.FirstOrDefault(e => e.TeamGroupId == teamGroup.TeamGroupId);
            if (newTeamGroup != null)
            {
                newTeamGroup.UnitId = teamGroup.UnitId;
                newTeamGroup.InstallationId = teamGroup.InstallationId;
                newTeamGroup.WorkAreaId = teamGroup.WorkAreaId;
                newTeamGroup.DepartId = teamGroup.DepartId;         
                newTeamGroup.TeamGroupCode = teamGroup.TeamGroupCode;
                newTeamGroup.TeamGroupName = teamGroup.TeamGroupName;
                newTeamGroup.LeaderIds = teamGroup.LeaderIds;
                newTeamGroup.LeaderNames = teamGroup.LeaderNames;
                newTeamGroup.TeamType = teamGroup.TeamType;
                newTeamGroup.Remark = teamGroup.Remark;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除班组信息
        /// </summary>
        /// <param name="teamGroupId"></param>
        public static void DeleteTeamGroupById(string teamGroupId)
        {
            Model.HSSEDB_ENN db = Funs.DB;
            Model.Base_TeamGroup teamGroup = db.Base_TeamGroup.FirstOrDefault(e => e.TeamGroupId == teamGroupId);
            if (teamGroup != null)
            {
                db.Base_TeamGroup.DeleteOnSubmit(teamGroup);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据项目ID、单位ID获取班组下拉选择项
        /// </summary>
        /// <param name="p"></param>
        /// <param name="p_2"></param>
        /// <returns></returns>
        public static object GetTeamGroupListByUnitId(string unitId)
        {
            return (from x in Funs.DB.Base_TeamGroup where x.UnitId == unitId orderby x.TeamGroupCode select x).ToList();
        }

        #region 表下拉框
        /// <summary>
        ///  表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitTeamGroupProjectUnitDropDownList(FineUIPro.DropDownList dropName, string unitId, bool isShowPlease)
        {
            dropName.DataValueField = "TeamGroupId";
            dropName.DataTextField = "TeamGroupName";
            dropName.DataSource = GetTeamGroupListByUnitId(unitId);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        #endregion
    }
}
