using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using BLL;

namespace FineUIPro.Web.QualityAudit
{
    public partial class PersonQualitySave : PageBase
    {
        #region 定义项
        /// <summary>
        /// 人员主键
        /// </summary>
        public string PersonId
        {
            get
            {
                return (string)ViewState["PersonId"];
            }
            set
            {
                ViewState["PersonId"] = value;
            }
        }

        /// <summary>
        /// 资质明细主键
        /// </summary>
        public string PersonQualityId
        {
            get
            {
                return (string)ViewState["PersonQualityId"];
            }
            set
            {
                ViewState["PersonQualityId"] = value;
            }
        }
        #endregion

        /// <summary>
        /// 装置/科室编辑页面
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
                BLL.CertificateService.InitCertificateDropDownList(this.drpCertificateId, true);
                BLL.ProspectiveService.InitProspectiveDropDownList(this.drpProspectiveIds, true);
                BLL.UnitService.InitUnitDropDownList(this.drpSendUnit, true);
                BLL.UserService.InitUserDropDownList(this.drpCompileMan, true);
                this.PersonQualityId = Request.Params["PersonQualityId"];
                this.PersonId = Request.Params["PersonId"];
                if (!string.IsNullOrEmpty(this.PersonQualityId))
                {
                    var personQuality = BLL.PersonQualityService.GetPersonQualityById(this.PersonQualityId);
                    if (personQuality != null)
                    {
                        this.PersonId = personQuality.PersonId;
                        this.txtCertificateNo.Text = personQuality.CertificateNo;
                        if (!string.IsNullOrEmpty(personQuality.CertificateId))
                        {
                            this.drpCertificateId.SelectedValue = personQuality.CertificateId;
                        }
                        if (!string.IsNullOrEmpty(personQuality.ProspectiveIds))
                        {
                            this.drpProspectiveIds.SelectedValueArray = personQuality.ProspectiveIds.Split(',');
                        }
                        if (!string.IsNullOrEmpty(personQuality.SendUnit))
                        {
                            this.drpSendUnit.SelectedValue = personQuality.SendUnit;
                        }
                        this.txtSendDate.Text = string.Format("{0:yyyy-MM-dd}", personQuality.SendDate);
                        this.txtLimitDate.Text = string.Format("{0:yyyy-MM-dd}", personQuality.LimitDate);
                        this.txtLateCheckDate.Text = string.Format("{0:yyyy-MM-dd}", personQuality.LateCheckDate);
                        this.txtAuditDate.Text = string.Format("{0:yyyy-MM-dd}", personQuality.AuditDate);
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", personQuality.CompileDate);
                        if (!string.IsNullOrEmpty(personQuality.CompileMan))
                        {
                            this.drpCompileMan.SelectedValue = personQuality.CompileMan;
                        }

                        this.txtRemark.Text = personQuality.Remark;                        
                    }
                }
                else
                {
                    this.drpCompileMan.SelectedValue = this.CurrUser.UserId;
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", System.DateTime.Now);
                }

                var person = BLL.UserService.GetUserByUserId(this.PersonId);
                if (person != null)
                {
                    this.lbName.Text += person.UserName;
                    this.lbName.Text += "；" + person.UserCode;
                    this.lbName.Text += "；" + BLL.UnitService.GetUnitNameByUnitId(person.UnitId);
                    this.lbName.Text += "；" + person.WorkPostName;
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
            this.SaveData();
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        private void SaveData()
        {
            Model.QualityAudit_PersonQuality newPersonQuality = new Model.QualityAudit_PersonQuality
            {
                PersonId = this.PersonId,
                CertificateNo = this.txtCertificateNo.Text,
                SendDate = Funs.GetNewDateTime(this.txtSendDate.Text),
                LimitDate = Funs.GetNewDateTime(this.txtLimitDate.Text),
                LateCheckDate = Funs.GetNewDateTime(this.txtLateCheckDate.Text),
                AuditDate = Funs.GetNewDateTime(this.txtAuditDate.Text),
                CompileDate = Funs.GetNewDateTime(this.txtCompileDate.Text),
                Remark=this.txtRemark.Text.Trim(),
            };

            if (this.drpCertificateId.SelectedValue != Const._Null && !string.IsNullOrEmpty(this.drpCertificateId.SelectedValue))
            {
                newPersonQuality.CertificateId = this.drpCertificateId.SelectedValue;
            }
          
            //准操项目
            string prospectiveId = string.Empty;
            string prospectiveName = string.Empty;
            foreach (var item in this.drpProspectiveIds.SelectedValueArray)
            {
                var prospective = BLL.ProspectiveService.GetProspectiveById(item);
                if (prospective != null)
                {
                    prospectiveId += prospective.ProspectiveId + ",";
                    prospectiveName += prospective.ProspectiveName + ",";
                }
            }
            if (!string.IsNullOrEmpty(prospectiveId))
            {
                newPersonQuality.ProspectiveIds = prospectiveId.Substring(0, prospectiveId.LastIndexOf(","));
                newPersonQuality.ProspectiveNames = prospectiveName.Substring(0, prospectiveName.LastIndexOf(","));
            }
            if (this.drpSendUnit.SelectedValue != Const._Null && !string.IsNullOrEmpty(this.drpSendUnit.SelectedValue))
            {
                newPersonQuality.SendUnit = this.drpSendUnit.SelectedValue;
            }
            if (this.drpCompileMan.SelectedValue != Const._Null && !string.IsNullOrEmpty(this.drpCompileMan.SelectedValue))
            {
                newPersonQuality.CompileMan = this.drpCompileMan.SelectedValue;
            }

            if (string.IsNullOrEmpty(this.PersonQualityId))
            {
                this.PersonQualityId = SQLHelper.GetNewID(typeof(Model.QualityAudit_PersonQuality));
                newPersonQuality.PersonQualityId = this.PersonQualityId;
                BLL.PersonQualityService.AddPersonQuality(newPersonQuality);
                BLL.LogService.AddLog(this.CurrUser.UserId, "添加人员资质明细");
            }
            else
            {
                newPersonQuality.PersonQualityId = this.PersonQualityId;
                BLL.PersonQualityService.UpdatePersonQuality(newPersonQuality);
                BLL.LogService.AddLog(this.CurrUser.UserId, "修改人员资质明细");
            }

            BLL.PersonQualityService.SavePersonQualityItem(newPersonQuality.PersonQualityId, newPersonQuality.LateCheckDate);
        }

        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.PersonQualityId))
            {
                this.SaveData();
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/PersonQualityAttachUrl&menuId={1}&type=0", this.PersonQualityId, BLL.Const.PersonQualityMenuId)));
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSeeCheck_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("PersonQualityItem.aspx?PersonQualityId={0}", this.PersonQualityId, BLL.Const.PersonQualityMenuId), "复查时间", 400, 300));
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
            var buttonList = BLL.CommonService.GetAllButtonList( this.CurrUser.UserId, BLL.Const.PersonQualityMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
            }
        }
        #endregion
    }
}