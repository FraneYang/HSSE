namespace FineUIPro.Web.QualityAudit
{
    using BLL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    public partial class PersonQualityEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 评价对象主键
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
        #endregion

        /// <summary>
        /// 人员资质编辑页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ///权限
                this.GetButtonPower();
                this.PersonId = Request.Params["PersonId"];
                var person = BLL.UserService.GetUserByUserId(this.PersonId);
                if (person != null)
                {
                    this.txtUserName.Text = person.UserName;
                    this.txtUserCode.Text = person.UserCode;
                    this.txtWorkPostName.Text = person.WorkPostName;
                    this.txtUnitName.Text = BLL.UnitService.GetUnitNameByUnitId(person.UnitId);
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
            if (!string.IsNullOrEmpty(this.PersonId))
            {
                string strSql = @"SELECT PersonQuality.PersonQualityId,PersonQuality.PersonId,PersonQuality.CertificateId,PersonQuality.CertificateNo,Certificate.CertificateName,PersonQuality.SendUnit,Unit.UnitName AS SendUnitName,PersonQuality.ProspectiveIds,PersonQuality.ProspectiveNames"
                        + @",PersonQuality.SendDate,PersonQuality.LimitDate,PersonQuality.LateCheckDate,PersonQuality.AuditDate,PersonQuality.Remark,PersonQuality.CompileMan,CompileManUser.UserName AS CompileManName,PersonQuality.CompileDate"
                        + @" FROM dbo.QualityAudit_PersonQuality AS PersonQuality"
                        + @" LEFT JOIN Base_Certificate AS Certificate ON PersonQuality.CertificateId=Certificate.CertificateId "
                        + @" LEFT JOIN Base_Unit AS Unit ON PersonQuality.SendUnit=Unit.UnitId"
                        + @" LEFT JOIN Sys_User AS CompileManUser ON PersonQuality.CompileMan=CompileManUser.UserId"
                        + @" WHERE PersonQuality.PersonId=@PersonId ";
                List<SqlParameter> listStr = new List<SqlParameter>();
                listStr.Add(new SqlParameter("@PersonId", this.PersonId));
                if (!string.IsNullOrEmpty(this.txtName.Text))
                {
                    strSql += " AND (PersonQuality.CertificateNo LIKE @Name OR Certificate.CertificateName LIKE @Name OR Unit.UnitName LIKE @Name OR PersonQuality.ProspectiveNames LIKE @Name)";
                    listStr.Add(new SqlParameter("@Name", "%" + this.txtName.Text.Trim() + "%"));
                }
                strSql += " ORDER BY PersonQuality.CompileDate";
                SqlParameter[] parameter = listStr.ToArray();
                DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);                
                Grid1.DataSource = tb;
                Grid1.DataBind();
                for (int i = 0; i < Grid1.Rows.Count; i++)
                {
                    var personQuality = BLL.PersonQualityService.GetPersonQualityById(Grid1.Rows[i].DataKeys[0].ToString());
                    if (personQuality != null && personQuality.LimitDate < System.DateTime.Now)
                    {
                        Grid1.Rows[i].RowCssClass = "Red";                    
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

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid();
            this.Grid1.PageIndex = 0;
        }
        #endregion 

        #region 新增人员资质
        /// <summary>
        /// 新增人员资质
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("PersonQualitySave.aspx?PersonId={0}", this.PersonId)));
        }
        #endregion

        #region 删除人员资质
        /// <summary>
        /// 删除人员资质
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
                    BLL.PersonQualityService.DeletePersonQualityById(rowID);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "删除人员资质明细！");
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

            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("PersonQualitySave.aspx?PersonQualityId={0}&PersonId={1}", Grid1.SelectedRowID, this.PersonId, "编辑 - ")));
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.UserId, BLL.Const.PersonQualityMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnMenuEdit.Hidden = false;
                    this.btnAdd.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnMenuDelete.Hidden = false;
                }
            }
        }
        #endregion
    }
}