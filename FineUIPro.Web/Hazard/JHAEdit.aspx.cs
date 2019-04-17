namespace FineUIPro.Web.Hazard
{
    using BLL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    public partial class JHAEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 设备设施主键
        /// </summary>
        public string JobActivityId
        {
            get
            {
                return (string)ViewState["JobActivityId"];
            }
            set
            {
                ViewState["JobActivityId"] = value;
            }
        }
        #endregion

        /// <summary>
        /// JHA评价编辑页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ///权限
                this.GetButtonPower();
                this.JobActivityId = Request.Params["JobActivityId"];
                this.loadData();
            }
        }

        /// <summary>
        /// 页面初始化
        /// </summary>
        private void loadData()
        {
            if (!string.IsNullOrEmpty(this.JobActivityId))
            {
                var jobActivity = BLL.JobActivityService.GetJobActivityByJobActivityId(this.JobActivityId);
                if (jobActivity != null)
                {
                    this.txtJobActivityName.Text = jobActivity.JobActivityName;
                    this.txtJobActivityCode.Text = jobActivity.JobActivityCode;
                    var installation = BLL.InstallationService.GetInstallationByInstallationId(jobActivity.InstallationId);
                    if (installation != null)
                    {
                        this.txtInstallation.Text = installation.InstallationName;
                    }
                    var workArea = BLL.WorkAreaService.GetWorkAreaByWorkAreaId(jobActivity.WorkAreaId);
                    if (workArea != null)
                    {
                        this.txtWorkArea.Text = workArea.WorkAreaName;
                    }
                    if (jobActivity.States == "2")
                    {
                        this.btnSubmit.Hidden = true;
                    }
                }

                this.BindGrid();
            }
        }

        #region 绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            if (!string.IsNullOrEmpty(this.JobActivityId))
            {
                string strSql = @"SELECT JHAItem.JHAItemId,JHAItem.JobActivityId,JHAItem.SortIndex,JHAItem.JobStep,JHAItem.PossibleAccidents,JHAItem.NowControlMeasures"
                        + @" ,JHAItem.HazardJudge_L,JHAItem.HazardJudge_S,JHAItem.HazardJudge_R,JHAItem.RiskLevel,sysConst.ConstText AS RiskLevelName,JHAItem.ControlMeasures,JHAItem.ManagementMeasures,JHAItem.ProtectiveMeasures,JHAItem.OtherMeasures"
                        + @" FROM Hazard_JHAItem AS JHAItem"
                        + @" LEFT JOIN Sys_Const AS sysConst ON JHAItem.RiskLevel=sysConst.ConstValue AND sysConst.GroupId='" + BLL.ConstValue.Group_RiskLevel + "'"
                        + @" WHERE JHAItem.JobActivityId=@JobActivityId ORDER BY JHAItem.SortIndex";
                SqlParameter[] parameter = new SqlParameter[]
                        {
                            new SqlParameter("@JobActivityId",this.JobActivityId),
                        };
                DataTable table = SQLHelper.GetDataTableRunText(strSql, parameter);
                Grid1.DataSource = table;
                Grid1.DataBind();
                for (int i = 0; i < Grid1.Rows.Count; i++)
                {
                    var JHAItem = BLL.JHAItemService.GetJHAItemById(Grid1.Rows[i].DataKeys[0].ToString());
                    if (JHAItem != null)
                    {
                        if (JHAItem.RiskLevel == "1")
                        {
                            Grid1.Rows[i].RowCssClass = "Red";
                        }
                        else if (JHAItem.RiskLevel == "2")
                        {
                            Grid1.Rows[i].RowCssClass = "Orange";
                        }
                        else if (JHAItem.RiskLevel == "3")
                        {
                            Grid1.Rows[i].RowCssClass = "Yellow";
                        }                        
                        else if (JHAItem.RiskLevel == "4")
                        {
                            Grid1.Rows[i].RowCssClass = "Blue";
                        }
                    }
                }
            }
            else
            {
                Grid1.DataSource = null;
                Grid1.DataBind();
            }
        }
        #endregion

        #region 新增JHA评价
        /// <summary>
        /// 新增JHA评价
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("JHASave.aspx?JobActivityId={0}", this.JobActivityId)));
        }
        #endregion

        #region 删除JHA评价
        /// <summary>
        /// 删除JHA评价
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                string messages = string.Empty;
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    BLL.JHAItemService.DeleteJHAItemById(rowID);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "删除JHA评价明细！");
                }

                BindGrid();
                var jhaItem = Funs.DB.Hazard_JHAItem.FirstOrDefault(x => x.JobActivityId == this.JobActivityId);
                if (jhaItem == null)
                {
                    var jobActivity = BLL.JobActivityService.GetJobActivityByJobActivityId(this.JobActivityId);
                    if (jobActivity != null)
                    {
                        jobActivity.States = "0";
                        BLL.JobActivityService.UpdateJobActivity(jobActivity);
                    }
                }
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

        #region 数据编辑
        /// <summary>
        /// Grid行双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 右键编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuEdit_Click(object sender, EventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 编辑数据方法
        /// </summary>
        private void EditData()
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInParent("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }

            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("JHASave.aspx?JHAItemId={0}", Grid1.SelectedRowID, "编辑 - ")));
        }
        #endregion

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            BindGrid();
        }

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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.UserId, BLL.Const.JHAMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnMenuEdit.Hidden = false;
                    this.btnSubmit.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnMenuADD.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnMenuDelete.Hidden = false;
                }
            }
        }
        #endregion


        /// <summary>
        /// 右键编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuADD_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("JHAAdd.aspx?JobActivityId={0}", this.JobActivityId, "编辑 - ")));
        }

        /// <summary>
        /// 提交按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            var jhaItemNUll = Funs.DB.Hazard_JHAItem.FirstOrDefault(x => x.JobActivityId == this.JobActivityId);
            if (jhaItemNUll != null)
            {
                var jobActivity = BLL.JobActivityService.GetJobActivityByJobActivityId(this.JobActivityId);
                if (jobActivity != null)
                {
                    jobActivity.States = "2";
                    BLL.JobActivityService.UpdateJobActivity(jobActivity);
                    PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
                }
            }
            else
            {
                Alert.ShowInTop("当前作业活动没有评价步骤，不能提交！", MessageBoxIcon.Warning);
                return;
            }
        }
    }
}