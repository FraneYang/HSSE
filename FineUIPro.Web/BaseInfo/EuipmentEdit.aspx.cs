namespace FineUIPro.Web.BaseInfo
{
    using BLL;
    using System;
    using System.Linq;

    public partial class EuipmentEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 设备设施主键
        /// </summary>
        public string EuipmentId
        {
            get
            {
                return (string)ViewState["EuipmentId"];
            }
            set
            {
                ViewState["EuipmentId"] = value;
            }
        }
        #endregion

        /// <summary>
        /// 设备设施编辑页面
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
                this.EuipmentId = Request.Params["EuipmentId"];
                BLL.InstallationService.InitInstallationByDepartDropDownList(this.drpInstallationId, string.Empty, false);
                BLL.WorkAreaService.InitWorkAreaDropDownList(this.drpWorkAreaId, this.drpInstallationId.SelectedValue, true);
                BLL.EuipmentTypeService.InitEuipmentTypeDropDownList(this.drpEuipmentTypeId, true);
                if (!string.IsNullOrEmpty(this.EuipmentId))
                {
                    var Euipment = BLL.EuipmentService.GetEuipmentByEuipmentId(this.EuipmentId);
                    if (Euipment != null)
                    {
                        if (!string.IsNullOrEmpty(Euipment.InstallationId))
                        {
                            this.drpInstallationId.SelectedValue = Euipment.InstallationId;
                            this.drpWorkAreaId.Items.Clear();
                            BLL.WorkAreaService.InitWorkAreaDropDownList(this.drpWorkAreaId, this.drpInstallationId.SelectedValue, true);
                            if (!string.IsNullOrEmpty(Euipment.WorkAreaId))
                            {
                                this.drpWorkAreaId.SelectedValue = Euipment.WorkAreaId;
                            }
                        }
                        
                        if (!string.IsNullOrEmpty(Euipment.EuipmentTypeId))
                        {
                            this.drpEuipmentTypeId.SelectedValue = Euipment.EuipmentTypeId;
                        }
                        this.txtEuipmentNo.Text = Euipment.EuipmentNo;
                        this.txtEuipmentName.Text = Euipment.EuipmentName;
                        this.txtEuipmentCode.Text = Euipment.EuipmentCode;
                        this.txtRemark.Text = Euipment.Remark;
                        if (!string.IsNullOrEmpty(Euipment.Identification))
                        {
                            this.drpIdentification.SelectedValueArray = Euipment.Identification.Split(',');
                        }
                    }
                }
                else
                {
                   // this.txtEuipmentCode.Text = Funs.GetMaxIndex("Base_Euipment", "EuipmentCode", string.Empty, string.Empty).ToString();
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
                if (BLL.EuipmentService.IsExistEuipmentName(this.drpInstallationId.SelectedValue, this.drpWorkAreaId.SelectedValue, this.EuipmentId, this.txtEuipmentName.Text.Trim()))
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

            Model.Base_Euipment newEuipment = new Model.Base_Euipment
            {
                EuipmentCode = this.txtEuipmentCode.Text.Trim(),
                EuipmentName = this.txtEuipmentName.Text.Trim(),
                Remark = this.txtRemark.Text.Trim(),
                EuipmentNo = this.txtEuipmentNo.Text.Trim(),
                InstallationId = this.drpInstallationId.SelectedValue,                           
            };

            ///评价方法
            string identification = string.Empty;
            foreach (var item in this.drpIdentification.SelectedValueArray)
            {
                identification += item + ",";
            }
            if (!string.IsNullOrEmpty(identification))
            {
                newEuipment.Identification = identification.Substring(0, identification.LastIndexOf(","));                
            }
            if (this.drpWorkAreaId.SelectedValue != BLL.Const._Null && !string.IsNullOrEmpty(this.drpWorkAreaId.SelectedValue))
            {
                newEuipment.WorkAreaId = this.drpWorkAreaId.SelectedValue;
            }
            if (this.drpEuipmentTypeId.SelectedValue != BLL.Const._Null && !string.IsNullOrEmpty(this.drpEuipmentTypeId.SelectedValue))
            {
                newEuipment.EuipmentTypeId = this.drpEuipmentTypeId.SelectedValue;
            }

            if (string.IsNullOrEmpty(this.EuipmentId))
            {
                this.EuipmentId = SQLHelper.GetNewID(typeof(Model.Base_Euipment));
                newEuipment.EuipmentId = this.EuipmentId;
                BLL.EuipmentService.AddEuipment(newEuipment);
                BLL.LogService.AddLog(this.CurrUser.UserId, "添加设备设施");
            }
            else
            {
                newEuipment.EuipmentId = this.EuipmentId;
                BLL.EuipmentService.UpdateEuipment(newEuipment);
                BLL.LogService.AddLog(this.CurrUser.UserId, "修改设备设施");
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
            var buttonList = BLL.CommonService.GetAllButtonList( this.CurrUser.UserId, BLL.Const.EuipmentMenuId);
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
        protected void txtEuipmentName_TextChanged(object sender, EventArgs e)
        {
            if (BLL.EuipmentService.IsExistEuipmentName(this.drpInstallationId.SelectedValue,this.drpWorkAreaId.SelectedValue, this.EuipmentId, this.txtEuipmentName.Text.Trim()))
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