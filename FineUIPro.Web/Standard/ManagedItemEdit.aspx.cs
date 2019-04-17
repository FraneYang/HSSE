namespace FineUIPro.Web.Standard
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using BLL;

    public partial class ManagedItemEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 管理项目主键
        /// </summary>
        public string ManagedItemId
        {
            get
            {
                return (string)ViewState["ManagedItemId"];
            }
            set
            {
                ViewState["ManagedItemId"] = value;
            }
        }
        #endregion

        /// <summary>
        /// 管理项目编辑页面
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
                this.ManagedItemId = Request.Params["ManagedItemId"];
                this.BindGrid();
                if (!string.IsNullOrEmpty(this.ManagedItemId))
                {
                    var ManagedItem = BLL.ManagedItemService.GetManagedItemById(this.ManagedItemId);
                    if (ManagedItem != null)
                    {
                        this.drpManagedObject.Value = ManagedItem.ManagedObjectId;
                        var ob = BLL.ManagedObjectService.GetManagedObjectById(ManagedItem.ManagedObjectId);
                        if (ob != null)
                        {
                            this.drpManagedObject.Text = ob.ManagedObjectName;
                        }
                        this.txtManagedItemName.Text = ManagedItem.ManagedItemName;
                        this.txtManagedItemCode.Text = ManagedItem.ManagedItemCode;
                        this.txtRemark.Text = ManagedItem.Remark;                        
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
                if (BLL.ManagedItemService.IsExistManagedItemName(this.drpManagedObject.Value,this.ManagedItemId, this.txtManagedItemName.Text.Trim()))
                {
                    Alert.ShowInParent("名称已存在，请修改后再保存！", MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrEmpty(this.drpManagedObject.Value))
                {
                    Alert.ShowInParent("请先选择管理对象，再保存！", MessageBoxIcon.Warning);
                    return;
                }
            }

            Model.Standard_ManagedItem newManagedItem = new Model.Standard_ManagedItem
            {
                ManagedItemCode = this.txtManagedItemCode.Text.Trim(),
                ManagedItemName = this.txtManagedItemName.Text.Trim(),
                Remark = this.txtRemark.Text.Trim(),
                ManagedObjectId = this.drpManagedObject.Value
            };

            if (string.IsNullOrEmpty(this.ManagedItemId))
            {
                this.ManagedItemId = SQLHelper.GetNewID(typeof(Model.Standard_ManagedItem));
                newManagedItem.ManagedItemId = this.ManagedItemId;
                BLL.ManagedItemService.AddManagedItem(newManagedItem);
                BLL.LogService.AddLog(this.CurrUser.UserId, "添加管理项目");
            }
            else
            {
                newManagedItem.ManagedItemId = this.ManagedItemId;
                BLL.ManagedItemService.UpdateManagedItem(newManagedItem);
                BLL.LogService.AddLog(this.CurrUser.UserId, "修改管理项目");
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
            var buttonList = BLL.CommonService.GetAllButtonList( this.CurrUser.UserId, BLL.Const.ManagedItemMenuId);
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
        protected void txtManagedItemName_TextChanged(object sender, EventArgs e)
        {
            if (BLL.ManagedItemService.IsExistManagedItemName(this.drpManagedObject.Value, this.ManagedItemId, this.txtManagedItemName.Text.Trim()))
            {
                Alert.ShowInParent("名称已存在，请修改后再保存！", MessageBoxIcon.Warning);
                return;
            }
        }

        #region 下拉框查询
        /// <summary>
        /// 下拉框查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }
        #endregion 

        #region 下拉框绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT ManagedObjects.ManagedObjectId,ManagedObjects.ManagedObjectName,ManagedObjects.ManagedObjectCode,ManagedObjects.Remark,Standards.StandardCode,Standards.StandardName,Specialty.SpecialtyCode,Specialty.SpecialtyName"
                       + @" FROM dbo.Standard_ManagedObject AS ManagedObjects"
                       + @" LEFT JOIN DBO.Base_Standard AS Standards ON ManagedObjects.StandardId =Standards.StandardId"
                       + @" LEFT JOIN DBO.Base_Specialty AS Specialty ON Standards.SpecialtyId =Specialty.SpecialtyId"
                       + @" WHERE 1 = 1";
            List<SqlParameter> listStr = new List<SqlParameter>();

            if (!string.IsNullOrEmpty(this.txtName.Text.Trim()))
            {
                strSql += " AND (ManagedObjectName LIKE @Name OR ManagedObjectCode LIKE @Name OR ManagedObjects.Remark LIKE @Name OR Standards.StandardName LIKE @Name OR Specialty.SpecialtyName LIKE @Name)";
                listStr.Add(new SqlParameter("@Name", "%" + this.txtName.Text.Trim() + "%"));
            }

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            //
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        #endregion
    }
}