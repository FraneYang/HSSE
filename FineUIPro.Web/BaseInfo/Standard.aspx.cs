﻿namespace FineUIPro.Web.BaseInfo
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Web.UI;
    using BLL;

    public partial class Standard : PageBase
    {
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
                ////权限按钮方法
                this.GetButtonPower();
                this.ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                this.btnNew.OnClientClick = Window1.GetShowReference("StandardEdit.aspx") + "return false;";
                // 绑定表格
                this.BindGrid();
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT Standards.StandardId,Standards.StandardName,Standards.StandardCode,Standards.Remark,Specialty.SpecialtyCode,Specialty.SpecialtyName"
                        + @" FROM dbo.Base_Standard AS Standards"
                        + @" LEFT JOIN DBO.Base_Specialty AS Specialty ON Standards.SpecialtyId =Specialty.SpecialtyId"
                        + @" WHERE 1 = 1";
            List<SqlParameter> listStr = new List<SqlParameter>();

            if (!string.IsNullOrEmpty(this.txtName.Text.Trim()))
            {
                strSql += " AND (StandardName LIKE @Name OR StandardCode LIKE @Name OR Standards.Remark LIKE @Name OR Specialty.SpecialtyName LIKE @Name)";
                listStr.Add(new SqlParameter("@Name", "%" + this.txtName.Text.Trim() + "%"));
            }      
           
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();           
        }

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid();
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.UserId, BLL.Const.StandardMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnNew.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnMenuEdit.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnMenuDelete.Hidden = false;
                }
            }
        }
        #endregion        

        #region 分页
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {           
            BindGrid();
        }

        /// <summary>
        /// 分页显示条数下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            BindGrid();
        }
        #endregion

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
            string Id = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("StandardEdit.aspx?StandardId={0}", Id, "编辑 - ")));
        }

        #region  删除数据
        /// <summary>
        /// 右键删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            this.DeleteData();
        }

        /// <summary>
        /// 删除方法
        /// </summary>
        private void DeleteData()
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    if (judgementDelete(rowID, false))
                    {
                        BLL.StandardService.DeleteStandardById(rowID);
                        BLL.LogService.AddLog(this.CurrUser.UserId, "删除标准名称");
                    }
                }
                BindGrid();
                ShowNotify("删除数据成功!");
            }
        }
        #endregion

        #region 判断是否可删除
        /// <summary>
        /// 判断是否可以删除
        /// </summary>
        /// <returns></returns>
        private bool judgementDelete(string id, bool isShow)
        {
            string content = string.Empty;
            //if (Funs.DB.License_FireWork.FirstOrDefault(x => x.Standard == id) != null)
            //{
            //    content = "该标准名称已在【动火作业票】中使用，不能删除！";
            //}
            if (string.IsNullOrEmpty(content))
            {
                return true;
            }
            else
            {
                if (isShow)
                {
                    Alert.ShowInTop(content);
                }
                return false;
            }
        }

        #endregion
    }
}