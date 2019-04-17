using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using BLL;

namespace FineUIPro.Web.Controls
{
    public partial class FlowOperateControl : System.Web.UI.UserControl
    {
        #region 定义项
        /// <summary>
        /// 单据ID
        /// </summary>
        public string DataId
        {
            get;
            set;
        }
        /// <summary>
        /// 菜单ID
        /// </summary>
        public string MenuId
        {
            get;
            set;
        }
        /// <summary>
        /// 项目ID
        /// </summary>
        public string ProjectId
        {
            get;
            set;
        }

        /// <summary>
        /// 单位iID
        /// </summary>
        public string UnitId
        {
            get;
            set;
        }
        /// <summary>
        /// 下一步骤
        /// </summary>
        public string NextStep
        {
            get
            {
                if (!string.IsNullOrEmpty(this.rblFlowOperate.SelectedValue))
                {
                    return this.rblFlowOperate.SelectedValue;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 下一步办理人
        /// </summary>
        public string NextPerson
        {
            get
            {
                return this.drpPerson.SelectedValue;
            }
        }      
        #endregion

        #region 定义页面项
        /// <summary>
        /// 单据ID
        /// </summary>
        public string getDataId
        {
            get
            {
                return (string)ViewState["getDataId"];
            }
            set
            {
                ViewState["getDataId"] = value;
            }
        }

        /// <summary>
        /// 菜单ID
        /// </summary>
        public string getMenuId
        {
            get
            {
                return (string)ViewState["getMenuId"];
            }
            set
            {
                ViewState["getMenuId"] = value;
            }
        }
        #endregion

        #region 加载页面
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {       
                this.getDataId = this.DataId;
                this.getMenuId = this.MenuId;
                BLL.UserService.InitFlowOperateControlUserDropDownList(this.drpPerson, this.UnitId, null, null, true);
                this.GroupPanel1.TitleToolTip += BLL.MenuFlowOperateService.GetFlowOperateName(this.getMenuId);
                var flowOperate = Funs.DB.Sys_FlowOperate.FirstOrDefault(x => x.DataId == this.getDataId && x.State == BLL.Const.State_2 && x.IsClosed == true);
                if (flowOperate != null)
                {
                    this.GroupPanel1.Hidden = true;
                    this.GroupPanel2.Collapsed = false;
                }
                this.BindGrid();
            }
        }
        #endregion

        #region 流程列表绑定数据
        /// <summary>
        /// 流程列表绑定数据
        /// </summary>
        private void BindGrid()
        {
            DataTable getDataTable = new DataTable();
            if (!string.IsNullOrEmpty(this.getDataId))
            {
                string strSql = @"SELECT FlowOperateId,MenuId,DataId,SortIndex,AuditFlowName,OperaterId,Users.UserName AS OperaterName,OperaterTime,Opinion,IsClosed "
                    + @" FROM dbo.Sys_FlowOperate LEFT JOIN Sys_User AS Users ON OperaterId = Users.UserId"
                    + @" WHERE IsClosed = 1 AND DataId=@DataId ORDER BY SortIndex DESC";
                List<SqlParameter> listStr = new List<SqlParameter>();
                listStr.Add(new SqlParameter("@DataId", this.getDataId));
                SqlParameter[] parameter = listStr.ToArray();
                getDataTable = SQLHelper.GetDataTableRunText(strSql, parameter);
            }

            Grid1.DataSource = getDataTable;
            Grid1.DataBind();
        }
        #endregion

        #region 下拉联动事件
        /// <summary>
        ///  处理方式下拉事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rblFlowOperate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.rblFlowOperate.SelectedValue == BLL.Const.State_2)
            {
                this.drpPerson.SelectedValue = BLL.Const._Null;
                this.drpPerson.Hidden = true;
                this.IsFileCabinetA.Hidden = false;
                //var codeTemplateRule = Funs.DB.Sys_CodeTemplateRule.FirstOrDefault(x => x.MenuId == this.getMenuId && x.IsFileCabinetA == true);
                //if (codeTemplateRule != null)
                //{
                //    this.IsFileCabinetA.Checked = true;
                //}
            }
            else
            {
                this.drpPerson.Hidden = false;
                this.IsFileCabinetA.Hidden = true;
                this.IsFileCabinetA.Checked = false;
            }
        }

        /// <summary>
        /// 得到角色名称字符串
        /// </summary>
        /// <param name="bigType"></param>
        /// <returns></returns>
        protected string ConvertRole(string roleIds)
        {
            return BLL.RoleService.getRoleNamesRoleIds(roleIds);
        }
        #endregion

        #region 组面板 折叠展开事件
        /// <summary>
        /// 组面板 折叠展开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GroupPanel_Collapse(object sender, EventArgs e)
        {
            if (this.GroupPanel1.Collapsed)
            {
                this.GroupPanel2.Collapsed = false;
            }
        }

        /// <summary>
        /// 组面板 折叠展开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GroupPanel2_Collapse(object sender, EventArgs e)
        {
            if (this.GroupPanel2.Collapsed)
            {
                this.GroupPanel1.Collapsed = false;
            }
        }

        /// <summary>
        /// 组面板 折叠展开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GroupPanel_Expand(object sender, EventArgs e)
        {
            if (this.GroupPanel1.Expanded)
            {
                this.GroupPanel2.Expanded = false;
            }
        }

        /// <summary>
        /// 组面板 折叠展开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GroupPanel2_Expand(object sender, EventArgs e)
        {
            if (this.GroupPanel2.Expanded)
            {
                this.GroupPanel1.Expanded = false;
            }
        }
        #endregion

        #region 保存数据
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="menuId">菜单id</param>
        /// <param name="dataId">主键id</param>
        /// <param name="isClosed">是否关闭这步流程</param>
        /// <param name="content">单据内容</param>
        /// <param name="url">路径</param>
        public void btnSaveData(string projectId, string menuId, string dataId, bool isClosed, string content, string url)
        {
            Model.Sys_FlowOperate newFlowOperate = new Model.Sys_FlowOperate();
            newFlowOperate.MenuId = this.getMenuId;
            newFlowOperate.DataId = dataId;
            newFlowOperate.OperaterId = ((Model.Sys_User)Session["CurrUser"]).UserId;
            newFlowOperate.State = this.rblFlowOperate.SelectedValue;
            newFlowOperate.IsClosed = isClosed;
            newFlowOperate.Opinion = this.txtOpinions.Text.Trim();
            newFlowOperate.Url = url;
            var user = BLL.UserService.GetUserByUserId(newFlowOperate.OperaterId);
            if (user != null)
            {
                var roles = BLL.RoleService.GetRoleByRoleId(user.RoleId);
                if (roles != null && !string.IsNullOrEmpty(roles.RoleName))
                {
                    newFlowOperate.AuditFlowName = "[" + roles.RoleName + "]" + "审核/审批";

                }
                else
                {
                    newFlowOperate.AuditFlowName = "[" + user.UserName + "]" + "审核/审批";
                }
            }

            var updateFlowOperate = from x in Funs.DB.Sys_FlowOperate
                                    where x.DataId == newFlowOperate.DataId && (x.IsClosed == false || !x.IsClosed.HasValue)
                                    select x;
            if (updateFlowOperate.Count() > 0)
            {
                foreach (var item in updateFlowOperate)
                {
                    item.OperaterId = newFlowOperate.OperaterId;
                    item.OperaterTime = System.DateTime.Now;
                    item.State = newFlowOperate.State;
                    item.Opinion = newFlowOperate.Opinion;
                    item.AuditFlowName = newFlowOperate.AuditFlowName;
                    if (newFlowOperate.State == BLL.Const.State_2)
                    {
                        item.AuditFlowName += "完成";
                    }
                    item.IsClosed = newFlowOperate.IsClosed;
                    Funs.DB.SubmitChanges();
                }
            }
            else
            {
                int maxSortIndex = 1;
                var flowSet = Funs.DB.Sys_FlowOperate.Where(x => x.DataId == newFlowOperate.DataId);
                var sortIndex = flowSet.Select(x => x.SortIndex).Max();
                if (sortIndex.HasValue)
                {
                    maxSortIndex = sortIndex.Value + 1;
                }
                newFlowOperate.FlowOperateId = SQLHelper.GetNewID(typeof(Model.Sys_FlowOperate));
                newFlowOperate.SortIndex = maxSortIndex;
                newFlowOperate.OperaterTime = System.DateTime.Now;
                newFlowOperate.AuditFlowName = "编制单据";
                if (newFlowOperate.State == BLL.Const.State_2)
                {
                    newFlowOperate.AuditFlowName = "单据完成";
                }
                Funs.DB.Sys_FlowOperate.InsertOnSubmit(newFlowOperate);
                Funs.DB.SubmitChanges();
            }

            if (newFlowOperate.IsClosed == true)
            {
                if (newFlowOperate.State != BLL.Const.State_2) ///未审核完成的时 增加下一步办理
                {
                    int maxSortIndex = 1;
                    var flowSet = Funs.DB.Sys_FlowOperate.Where(x => x.DataId == newFlowOperate.DataId);
                    var sortIndex = flowSet.Select(x => x.SortIndex).Max();
                    if (sortIndex.HasValue)
                    {
                        maxSortIndex = sortIndex.Value + 1;
                    }

                    Model.Sys_FlowOperate newNextFlowOperate = new Model.Sys_FlowOperate();
                    newNextFlowOperate.FlowOperateId = SQLHelper.GetNewID(typeof(Model.Sys_FlowOperate));
                    newNextFlowOperate.MenuId = newFlowOperate.MenuId;
                    newNextFlowOperate.DataId = newFlowOperate.DataId;
                    newNextFlowOperate.Url = newFlowOperate.Url;
                    newNextFlowOperate.SortIndex = maxSortIndex;
                    if (this.drpPerson.SelectedValue != BLL.Const._Null)
                    {
                        newNextFlowOperate.OperaterId = this.drpPerson.SelectedValue;
                    }
                    else
                    {
                        newNextFlowOperate.OperaterId = newFlowOperate.OperaterId;
                    }
                    var operaterUsers = BLL.UserService.GetUserByUserId(newNextFlowOperate.OperaterId);
                    if (operaterUsers != null)
                    {
                        var operaterRoles = BLL.RoleService.GetRoleByRoleId(operaterUsers.RoleId);
                        if (operaterRoles != null && !string.IsNullOrEmpty(operaterRoles.RoleName))
                        {
                            newNextFlowOperate.AuditFlowName = "[" + operaterRoles.RoleName + "]" + "审核/审批";
                        }
                        else
                        {
                            newNextFlowOperate.AuditFlowName = "[" + operaterUsers.UserName + "]" + "审核/审批";
                        }
                    }
                    newNextFlowOperate.IsClosed = false;
                    newNextFlowOperate.State = BLL.Const.State_1;
                    Funs.DB.Sys_FlowOperate.InsertOnSubmit(newNextFlowOperate);
                    Funs.DB.SubmitChanges();
                }                
            }
        }
        #endregion

        

        
    }
}