namespace FineUIPro.Web.BaseInfo
{
    using BLL;
    using System;
    using System.Linq;

    public partial class JobEnvironmentEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 作业环境主键
        /// </summary>
        public string JobEnvironmentId
        {
            get
            {
                return (string)ViewState["JobEnvironmentId"];
            }
            set
            {
                ViewState["JobEnvironmentId"] = value;
            }
        }
        #endregion

        /// <summary>
        /// 作业环境编辑页面
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
                this.JobEnvironmentId = Request.Params["JobEnvironmentId"];
                BLL.InstallationService.InitInstallationByDepartDropDownList(this.drpInstallationId, string.Empty, false);
                BLL.WorkAreaService.InitWorkAreaDropDownList(this.drpWorkAreaId, this.drpInstallationId.SelectedValue, true);
                if (!string.IsNullOrEmpty(this.JobEnvironmentId))
                {
                    var JobEnvironment = BLL.JobEnvironmentService.GetJobEnvironmentByJobEnvironmentId(this.JobEnvironmentId);
                    if (JobEnvironment != null)
                    {
                        if (!string.IsNullOrEmpty(JobEnvironment.InstallationId))
                        {
                            this.drpInstallationId.SelectedValue = JobEnvironment.InstallationId;
                            this.drpWorkAreaId.Items.Clear();
                            BLL.WorkAreaService.InitWorkAreaDropDownList(this.drpWorkAreaId, this.drpInstallationId.SelectedValue, true);
                            if (!string.IsNullOrEmpty(JobEnvironment.WorkAreaId))
                            {
                                this.drpWorkAreaId.SelectedValue = JobEnvironment.WorkAreaId;
                            }
                        }

                        this.txtJobEnvironmentName.Text = JobEnvironment.JobEnvironmentName;
                        this.txtJobEnvironmentCode.Text = JobEnvironment.JobEnvironmentCode;
                        this.txtRemark.Text = JobEnvironment.Remark;
                        this.drpIdentification.SelectedValue = JobEnvironment.Identification;
                    }
                }
                else
                {
                   // this.txtJobEnvironmentCode.Text = Funs.GetMaxIndex("Base_JobEnvironment", "JobEnvironmentCode", string.Empty, string.Empty).ToString();
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
                if (BLL.JobEnvironmentService.IsExistJobEnvironmentName(this.drpInstallationId.SelectedValue, this.drpWorkAreaId.SelectedValue, this.JobEnvironmentId, this.txtJobEnvironmentName.Text.Trim()))
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

            Model.Base_JobEnvironment newJobEnvironment = new Model.Base_JobEnvironment
            {
                JobEnvironmentCode = this.txtJobEnvironmentCode.Text.Trim(),
                JobEnvironmentName = this.txtJobEnvironmentName.Text.Trim(),
                Remark = this.txtRemark.Text.Trim(),
                InstallationId = this.drpInstallationId.SelectedValue,
                Identification=this.drpIdentification.SelectedValue,
            };
            if(this.drpWorkAreaId.SelectedValue != BLL.Const._Null && !string.IsNullOrEmpty(this.drpWorkAreaId.SelectedValue))
            {
                newJobEnvironment.WorkAreaId = this.drpWorkAreaId.SelectedValue;
            }            

            if (string.IsNullOrEmpty(this.JobEnvironmentId))
            {
                this.JobEnvironmentId = SQLHelper.GetNewID(typeof(Model.Base_JobEnvironment));
                newJobEnvironment.JobEnvironmentId = this.JobEnvironmentId;
                BLL.JobEnvironmentService.AddJobEnvironment(newJobEnvironment);
                BLL.LogService.AddLog(this.CurrUser.UserId, "添加作业环境");
            }
            else
            {
                newJobEnvironment.JobEnvironmentId = this.JobEnvironmentId;
                BLL.JobEnvironmentService.UpdateJobEnvironment(newJobEnvironment);
                BLL.LogService.AddLog(this.CurrUser.UserId, "修改作业环境");
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
            var buttonList = BLL.CommonService.GetAllButtonList( this.CurrUser.UserId, BLL.Const.JobEnvironmentMenuId);
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
        protected void txtJobEnvironmentName_TextChanged(object sender, EventArgs e)
        {
            if (BLL.JobEnvironmentService.IsExistJobEnvironmentName(this.drpInstallationId.SelectedValue,this.drpWorkAreaId.SelectedValue, this.JobEnvironmentId, this.txtJobEnvironmentName.Text.Trim()))
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