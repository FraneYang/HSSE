using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using BLL;

namespace FineUIPro.Web.Hazard
{
    public partial class LECEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// LEC评价主键
        /// </summary>
        public string LECId
        {
            get
            {
                return (string)ViewState["LECId"];
            }
            set
            {
                ViewState["LECId"] = value;
            }
        }
        #endregion

        /// <summary>
        /// LEC评价编辑页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                ///权限
                this.GetButtonPower();
                this.LECId = Request.Params["LECId"];
                BLL.InstallationService.InitInstallationDropDownList(this.drpInstallationId,string.Empty,false);
                if (!string.IsNullOrEmpty(this.LECId))
                {
                    var LEC = BLL.LECService.GetLECById(this.LECId);
                    if (LEC != null)
                    {
                        if (!string.IsNullOrEmpty(LEC.InstallationId))
                        {
                            this.drpInstallationId.SelectedValue = LEC.InstallationId;
                        }

                        this.txtEvaluatorName.Text = BLL.UserService.GetUserNameByUserId(LEC.EvaluatorId);
                        this.txtEvaluationTime.Text = string.Format("{0:yyyy-MM-dd}", LEC.EvaluationTime);
                    }
                }
            }
        }

        #region 绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = "select * from Hazard_LECItem where LECId=@LECId";
            SqlParameter[] parameter = new SqlParameter[]       
                    {
                        new SqlParameter("@LECId",this.LECId),
                    };
            DataTable table = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        #endregion

        #region 新增LEC评价
        /// <summary>
        /// 新增LEC评价
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("LECSave.aspx?LECId={0}", this.LECId)));
        }
        #endregion

        #region 删除LEC评价
        /// <summary>
        /// 删除LEC评价
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                string messages = string.Empty;
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    var riskList = Funs.DB.Hazard_RiskList.FirstOrDefault(x => x.LECId == rowID);
                    if (riskList == null)
                    {
                        BLL.LECItemService.DeleteLECItemById(rowID);
                        BLL.LogService.AddLog(this.CurrUser.UserId, "删除LEC评价明细！");
                    }
                    else
                    {
                        messages += (rowIndex+1).ToString() + "行已存在风险信息。";
                    }
                }

                BindGrid();
                if (!string.IsNullOrEmpty(messages))
                {
                    Alert.ShowInTop(messages, MessageBoxIcon.Warning);
                }
                else
                {
                    ShowNotify("删除数据成功!", MessageBoxIcon.Success);
                }
            }
        }
        #endregion

        #region 关闭弹出窗口
        /// <summary>
        /// 关闭弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.UserId, BLL.Const.LECMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnAdd.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnDelete.Hidden = false;
                }
            }
        }
        #endregion

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {            
            //Model.Base_LEC newLEC = new Model.Base_LEC();
            //newLEC.LECCode = this.txtLECCode.Text.Trim();
            //newLEC.LECName = this.txtLECName.Text.Trim();
            //newLEC.Def = this.txtDef.Text.Trim();
            //if (this.drpDepart.SelectedValue != Const._Null)
            //{
            //    newLEC.DepartId = this.drpDepart.SelectedValue;
            //}
            //newLEC.IsUsed = Convert.ToBoolean(this.drpIsUsed.SelectedValue);

            /////LEC评价负责人
            //string managerIds = string.Empty;
            //string managerNames = string.Empty;
            //foreach (var item in this.drpManagerIds.SelectedValueArray)
            //{
            //    var users = BLL.UserService.GetUserByUserId(item);
            //    if (users != null)
            //    {
            //        managerIds += users.UserId + ",";
            //        managerNames += users.UserName + ",";
            //    }
            //}
            //if (!string.IsNullOrEmpty(managerIds))
            //{
            //    newLEC.ManagerIds = managerIds.Substring(0, managerIds.LastIndexOf(","));
            //    newLEC.ManagerNames = managerNames.Substring(0, managerNames.LastIndexOf(","));
            //}

            //if (string.IsNullOrEmpty(this.LECId))
            //{
            //    newLEC.LECId = SQLHelper.GetNewID(typeof(Model.Base_LEC));
            //    BLL.LECService.AddLEC(newLEC);
            //    BLL.LogService.AddLog(this.CurrUser.UserId, "添加LEC评价");
            //}
            //else
            //{
            //    newLEC.LECId = this.LECId;
            //    BLL.LECService.UpdateLEC(newLEC);
            //    BLL.LogService.AddLog(this.CurrUser.UserId, "修改LEC评价");
            //}

            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }
    }
}