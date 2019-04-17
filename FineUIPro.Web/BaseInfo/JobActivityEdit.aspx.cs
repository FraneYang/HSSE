namespace FineUIPro.Web.BaseInfo
{
    using BLL;
    using System;
    using System.Linq;

    public partial class JobActivityEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 作业活动主键
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
        /// 作业活动编辑页面
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
                this.JobActivityId = Request.Params["JobActivityId"];
                BLL.InstallationService.InitInstallationByDepartDropDownList(this.drpInstallationId, String.Empty, false);
                BLL.WorkAreaService.InitWorkAreaDropDownList(this.drpWorkAreaId, this.drpInstallationId.SelectedValue, true);
                if (!string.IsNullOrEmpty(this.JobActivityId))
                {
                    var jobActivity = BLL.JobActivityService.GetJobActivityByJobActivityId(this.JobActivityId);
                    if (jobActivity != null)
                    {
                        if (!string.IsNullOrEmpty(jobActivity.InstallationId))
                        {
                            this.drpInstallationId.SelectedValue = jobActivity.InstallationId;
                            this.drpWorkAreaId.Items.Clear();
                            BLL.WorkAreaService.InitWorkAreaDropDownList(this.drpWorkAreaId, this.drpInstallationId.SelectedValue, true);
                            if (!string.IsNullOrEmpty(jobActivity.WorkAreaId))
                            {
                                this.drpWorkAreaId.SelectedValue = jobActivity.WorkAreaId;
                            }
                        }

                        this.txtJobActivityName.Text = jobActivity.JobActivityName;
                        this.txtJobActivityCode.Text = jobActivity.JobActivityCode;
                        this.txtRemark.Text = jobActivity.Remark;
                        this.drpIdentification.SelectedValue = jobActivity.Identification;
                    }
                }
                else
                {
                   // this.txtJobActivityCode.Text = Funs.GetMaxIndex("Base_JobActivity", "JobActivityCode", string.Empty, string.Empty).ToString();
                }
            }
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveData(true);
        }

        /// <summary>
        /// 保存方法
        /// </summary>
        private void SaveData(bool isColose)
        {
            if (isColose)
            {
                if (BLL.JobActivityService.IsExistJobActivityName(this.drpInstallationId.SelectedValue, this.drpWorkAreaId.SelectedValue, this.JobActivityId, this.txtJobActivityName.Text.Trim()))
                {
                    Alert.ShowInParent("名称已存在，请修改后再保存！", MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrEmpty(this.drpInstallationId.SelectedValue))
                {
                    Alert.ShowInParent("请先选择装置，再保存！", MessageBoxIcon.Warning);
                    return;
                }
            }

            Model.Base_JobActivity newJobActivity = new Model.Base_JobActivity
            {
                JobActivityCode = this.txtJobActivityCode.Text.Trim(),
                JobActivityName = this.txtJobActivityName.Text.Trim(),
                Remark = this.txtRemark.Text.Trim(),
                InstallationId = this.drpInstallationId.SelectedValue,
                Identification=this.drpIdentification.SelectedValue,
            };
            if(this.drpWorkAreaId.SelectedValue != BLL.Const._Null && !string.IsNullOrEmpty(this.drpWorkAreaId.SelectedValue))
            {
                newJobActivity.WorkAreaId = this.drpWorkAreaId.SelectedValue;
            }            

            if (string.IsNullOrEmpty(this.JobActivityId))
            {
                this.JobActivityId = SQLHelper.GetNewID(typeof(Model.Base_JobActivity));
                newJobActivity.JobActivityId = this.JobActivityId;
                BLL.JobActivityService.AddJobActivity(newJobActivity);
                BLL.LogService.AddLog(this.CurrUser.UserId, "添加作业活动");
            }
            else
            {
                newJobActivity.JobActivityId = this.JobActivityId;
                BLL.JobActivityService.UpdateJobActivity(newJobActivity);
                BLL.LogService.AddLog(this.CurrUser.UserId, "修改作业活动");
            }
            if (isColose)
            {
                PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
            }
        }

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList( this.CurrUser.UserId, BLL.Const.JobActivityMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
            }
        }
        #endregion

        /// <summary>
        /// 检验名称不能重复
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtJobActivityName_TextChanged(object sender, EventArgs e)
        {
            if (BLL.JobActivityService.IsExistJobActivityName(this.drpInstallationId.SelectedValue,this.drpWorkAreaId.SelectedValue, this.JobActivityId, this.txtJobActivityName.Text.Trim()))
            {
                Alert.ShowInParent("名称已存在，请修改后再保存！", MessageBoxIcon.Warning);
                return;
            }
        }        

        /// <summary>
        /// 装置下拉框联动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpInstallationId_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.drpWorkAreaId.Items.Clear();
            BLL.WorkAreaService.InitWorkAreaDropDownList(this.drpWorkAreaId, this.drpInstallationId.SelectedValue, true);
        }
    }
}