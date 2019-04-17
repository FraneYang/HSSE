namespace FineUIPro.Web.Standard
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using BLL;

    public partial class ManagedObjectEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 管理对象主键
        /// </summary>
        public string ManagedObjectId
        {
            get
            {
                return (string)ViewState["ManagedObjectId"];
            }
            set
            {
                ViewState["ManagedObjectId"] = value;
            }
        }
        #endregion

        /// <summary>
        /// 管理对象编辑页面
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
                this.ManagedObjectId = Request.Params["ManagedObjectId"];
                BLL.SpecialtyService.InitSpecialtyDropDownList(this.drpSpecialtyId, false);
                BLL.StandardService.InitStandardDropDownList(this.drpStandardId, this.drpSpecialtyId.SelectedValue, false);
                if (!string.IsNullOrEmpty(this.ManagedObjectId))
                {
                    var ManagedObject = BLL.ManagedObjectService.GetManagedObjectById(this.ManagedObjectId);
                    if (ManagedObject != null)
                    {
                        var standard = BLL.StandardService.GetStandardById(ManagedObject.StandardId);
                        if (standard != null)
                        {
                            this.drpSpecialtyId.SelectedValue = standard.SpecialtyId;
                            this.drpStandardId.Items.Clear();
                            BLL.StandardService.InitStandardDropDownList(this.drpStandardId, standard.SpecialtyId, false);
                            this.drpStandardId.SelectedValue = ManagedObject.StandardId;
                        }
                        
                        this.txtManagedObjectName.Text = ManagedObject.ManagedObjectName;
                        this.txtManagedObjectCode.Text = ManagedObject.ManagedObjectCode;
                        this.txtRemark.Text = ManagedObject.Remark;                        
                    }
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
                if (BLL.ManagedObjectService.IsExistManagedObjectName(this.drpStandardId.SelectedValue, this.ManagedObjectId, this.txtManagedObjectName.Text.Trim()))
                {
                    Alert.ShowInParent("名称已存在，请修改后再保存！", MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrEmpty(this.drpSpecialtyId.SelectedValue) || string.IsNullOrEmpty(this.drpStandardId.SelectedValue))
                {
                    Alert.ShowInParent("请先选择专业、标准，再保存！", MessageBoxIcon.Warning);
                    return;
                }
            }

            Model.Standard_ManagedObject newManagedObject = new Model.Standard_ManagedObject
            {
                ManagedObjectCode = this.txtManagedObjectCode.Text.Trim(),
                ManagedObjectName = this.txtManagedObjectName.Text.Trim(),
                Remark = this.txtRemark.Text.Trim(),
                StandardId = this.drpStandardId.SelectedValue
            };
            if (string.IsNullOrEmpty(this.ManagedObjectId))
            {
                this.ManagedObjectId = SQLHelper.GetNewID(typeof(Model.Standard_ManagedObject));
                newManagedObject.ManagedObjectId = this.ManagedObjectId;
                BLL.ManagedObjectService.AddManagedObject(newManagedObject);
                BLL.LogService.AddLog(this.CurrUser.UserId, "添加管理对象");
            }
            else
            {
                newManagedObject.ManagedObjectId = this.ManagedObjectId;
                BLL.ManagedObjectService.UpdateManagedObject(newManagedObject);
                BLL.LogService.AddLog(this.CurrUser.UserId, "修改管理对象");
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
            var buttonList = BLL.CommonService.GetAllButtonList( this.CurrUser.UserId, BLL.Const.ManagedObjectMenuId);
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
        protected void txtManagedObjectName_TextChanged(object sender, EventArgs e)
        {
            if (BLL.ManagedObjectService.IsExistManagedObjectName(this.drpStandardId.SelectedValue, this.ManagedObjectId, this.txtManagedObjectName.Text.Trim()))
            {
                Alert.ShowInParent("名称已存在，请修改后再保存！", MessageBoxIcon.Warning);
                return;
            }
        }        

        /// <summary>
        /// 专业下拉框联动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpSpecialtyId_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.drpStandardId.Items.Clear();
            BLL.StandardService.InitStandardDropDownList(this.drpStandardId, this.drpSpecialtyId.SelectedValue, false);
        }
    }
}