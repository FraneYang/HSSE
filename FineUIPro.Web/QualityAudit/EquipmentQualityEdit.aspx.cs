using System;
using System.Linq;
using BLL;

namespace FineUIPro.Web.QualityAudit
{
    public partial class EquipmentQualityEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 特种设备资质ID
        /// </summary>
        private string EquipmentQualityId
        {
            get
            {
                return (string)ViewState["EquipmentQualityId"];
            }
            set
            {
                ViewState["EquipmentQualityId"] = value;
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
                GetButtonPower();//权限设置
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();

                this.drpUnitId.DataTextField = "UnitName";
                this.drpUnitId.DataValueField = "UnitId";
                this.drpUnitId.DataSource = BLL.UnitService.GetUnitDropDownList();
                this.drpUnitId.DataBind();
                //Funs.FineUIPleaseSelect(this.drpUnitId);
                this.drpUnitId.SelectedValue = BLL.UnitService.GetThisUnit();

                this.drpInstallationId.DataTextField = "InstallationName";
                this.drpInstallationId.DataValueField = "InstallationId";
                this.drpInstallationId.DataSource = BLL.InstallationService.GetInstallationList();
                this.drpInstallationId.DataBind();
                Funs.FineUIPleaseSelect(this.drpInstallationId);

                this.drpEuipmentId.DataTextField = "EuipmentName";
                this.drpEuipmentId.DataValueField = "EuipmentId";
                this.drpEuipmentId.DataSource = BLL.EuipmentService.GetEquipmentList();
                this.drpEuipmentId.DataBind();
                Funs.FineUIPleaseSelect(this.drpEuipmentId);

                this.EquipmentQualityId = Request.Params["equipmentQualityId"];
                if (!string.IsNullOrEmpty(this.EquipmentQualityId))
                {
                    Model.QualityAudit_EquipmentQuality equipmentQuality = BLL.EquipmentQualityService.GetEquipmentQualityById(this.EquipmentQualityId);
                    if (equipmentQuality != null)
                    {
                        if (!string.IsNullOrEmpty(equipmentQuality.InstallationId))
                        {
                            this.drpInstallationId.SelectedValue = equipmentQuality.InstallationId;
                        }
                        if (!string.IsNullOrEmpty(equipmentQuality.UnitId))
                        {
                            this.drpUnitId.SelectedValue = equipmentQuality.UnitId;
                        }
                        if (!string.IsNullOrEmpty(equipmentQuality.EuipmentId))
                        {
                            this.drpEuipmentId.SelectedValue = equipmentQuality.EuipmentId;
                        }
                        this.txtEquipmentQualityCode.Text = equipmentQuality.EquipmentQualityCode;
                        this.txtEquipmentQualityName.Text = equipmentQuality.EquipmentQualityName;
                        this.txtSizeModel.Text = equipmentQuality.SizeModel;
                        this.txtFactoryCode.Text = equipmentQuality.FactoryCode;
                        this.txtCertificateCode.Text = equipmentQuality.CertificateCode;
                        if (equipmentQuality.CheckDate != null)
                        {
                            this.txtCheckDate.Text = string.Format("{0:yyyy-MM-dd}", equipmentQuality.CheckDate);
                        }
                        if (equipmentQuality.LimitDate != null)
                        {
                            this.txtLimitDate.Text = string.Format("{0:yyyy-MM-dd}", equipmentQuality.LimitDate);
                        }
                        if (equipmentQuality.InDate != null)
                        {
                            this.txtInDate.Text = string.Format("{0:yyyy-MM-dd}", equipmentQuality.InDate);
                        }
                        if (equipmentQuality.OutDate != null)
                        {
                            this.txtOutDate.Text = string.Format("{0:yyyy-MM-dd}", equipmentQuality.OutDate);
                        }
                        this.txtApprovalPerson.Text = equipmentQuality.ApprovalPerson;
                        this.txtCarNum.Text = equipmentQuality.CarNum;
                        this.txtRemark.Text = equipmentQuality.Remark;
                    }
                }
            }
        }
        #endregion

        #region 保存
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
        /// 保存数据
        /// </summary>
        /// <param name="isClose"></param>
        private void SaveData(bool isClose)
        {
            Model.QualityAudit_EquipmentQuality equipmentQuality = new Model.QualityAudit_EquipmentQuality();
            if (this.drpInstallationId.SelectedValue != BLL.Const._Null)
            {
                equipmentQuality.InstallationId = this.drpInstallationId.SelectedValue;
            }
            //if (this.drpUnitId.SelectedValue != BLL.Const._Null)
            //{
                equipmentQuality.UnitId = this.drpUnitId.SelectedValue;
            //}
            if (this.drpEuipmentId.SelectedValue != BLL.Const._Null)
            {
                equipmentQuality.EuipmentId = this.drpEuipmentId.SelectedValue;
            }
            equipmentQuality.EquipmentQualityCode = this.txtEquipmentQualityCode.Text.Trim();
            equipmentQuality.EquipmentQualityName = this.txtEquipmentQualityName.Text.Trim();
            equipmentQuality.SizeModel = this.txtSizeModel.Text.Trim();
            equipmentQuality.FactoryCode = this.txtFactoryCode.Text.Trim();
            equipmentQuality.CertificateCode = this.txtCertificateCode.Text.Trim();
            equipmentQuality.CheckDate = Funs.GetNewDateTime(this.txtCheckDate.Text.Trim());
            equipmentQuality.LimitDate = Funs.GetNewDateTime(this.txtLimitDate.Text.Trim());
            equipmentQuality.InDate = Funs.GetNewDateTime(this.txtInDate.Text.Trim());
            equipmentQuality.OutDate = Funs.GetNewDateTime(this.txtOutDate.Text.Trim());
            equipmentQuality.ApprovalPerson = this.txtApprovalPerson.Text.Trim();
            equipmentQuality.CarNum = this.txtCarNum.Text.Trim();
            equipmentQuality.Remark = this.txtRemark.Text.Trim();
            if (!string.IsNullOrEmpty(this.EquipmentQualityId))
            {
                equipmentQuality.EquipmentQualityId = this.EquipmentQualityId;
                BLL.EquipmentQualityService.UpdateEquipmentQuality(equipmentQuality);
                BLL.LogService.AddLog(this.CurrUser.UserId, "修改特种设备资质");
            }
            else
            {
                this.EquipmentQualityId = SQLHelper.GetNewID(typeof(Model.QualityAudit_EquipmentQuality));
                equipmentQuality.EquipmentQualityId = this.EquipmentQualityId;
                BLL.EquipmentQualityService.AddEquipmentQuality(equipmentQuality);
                BLL.LogService.AddLog(this.CurrUser.UserId, "添加特种设备资质");
            }
            if (isClose)
            {
                PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
            }
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.EquipmentQualityId))
            {
                this.SaveData(false);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/EquipmentQualityAttachUrl&menuId={1}&type=0", this.EquipmentQualityId, BLL.Const.EquipmentQualityMenuId)));
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.UserId, BLL.Const.EquipmentQualityMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
            }
        }
        #endregion

        #region 下拉选择事件
        /// <summary>
        /// 单位下拉选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpUnitId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.drpUnitId.SelectedValue == BLL.UnitService.GetThisUnit())
            {
                this.drpInstallationId.Hidden = false;
            }
            else
            {
                this.drpInstallationId.Hidden = true;
            }
        }
        #endregion
    }
}