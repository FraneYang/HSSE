﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using BLL;

namespace FineUIPro.Web.Training
{
    public partial class Plan : PageBase
    {
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
                this.GetButtonPower();             
                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                // 绑定表格
                BindGrid();
            }
            else
            {
                if (GetRequestEventArgument() == "reloadGrid")
                {
                    BindGrid();
                }
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT Plans.PlanId,Plans.PlanCode, Plans.PlanName, Plans.DesignerId, Plans.DesignerDate, Plans.WorkPostId,
                                     (CASE Plans.States WHEN '0' THEN '待提交' WHEN '1' THEN '已提交' WHEN '-1' THEN '已作废' END ) AS States,
                                     Users.UserName AS DesignerName,InstallationIds,InstallationNames"
                             + @" FROM dbo.Training_Plan AS Plans "
                             + @" LEFT JOIN Sys_User AS Users ON Users.UserId = Plans.DesignerId "
                             + @" WHERE 1=1";
            List<SqlParameter> listStr = new List<SqlParameter>();

            if (!string.IsNullOrEmpty(this.txtName.Text.Trim()))
            {
                strSql += " AND (PlanCode LIKE @name OR PlanName LIKE @name)";
                listStr.Add(new SqlParameter("@name", "%" + this.txtName.Text.Trim() + "%"));
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

        #region 分页、关闭窗口
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
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
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid();
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, EventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 编辑
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
        protected void btnMenuView_Click(object sender, EventArgs e)
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
                Alert.ShowInTop("请选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("PlanView.aspx?PlanId={0}", Grid1.SelectedRowID, "编辑 - ")));
        }
        #endregion

        #region 删除
        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            this.DeleteData();
        }

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
                string alterString = string.Empty;
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    string content = judgementDelete(rowID);
                    if (string.IsNullOrEmpty(content))
                    {
                        BLL.PlanItemService.DeletePlanItemByPlanId(rowID);
                        BLL.PlanService.DeletePlanById(rowID);
                    }
                    else
                    {
                        alterString += "第" + (rowIndex + 1) + "行" + content;
                    }
                }
                BLL.LogService.AddLog(this.CurrUser.UserId, "删除培训计划");
                BindGrid();
                if (string.IsNullOrEmpty(alterString))
                {
                    ShowNotify("删除数据成功!", MessageBoxIcon.Success);
                }
                else
                {
                    Alert.ShowInTop(alterString, MessageBoxIcon.Warning);
                }
            }
        }
        #endregion

        #region 输入框查询事件
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

        #region 获取权限按钮
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.UserId, BLL.Const.PlanMenuId);
            if (buttonList.Count() > 0)
            {               
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnMenuDelete.Hidden = false;
                }
            }
        }
        #endregion       

        #region 判断是否可删除
        /// <summary>
        /// 判断是否可以删除
        /// </summary>
        /// <returns></returns>
        private string judgementDelete(string id)
        {
            string content = string.Empty;
            if (Funs.DB.Training_Task.FirstOrDefault(x => x.PlanId == id) != null)
            {
                content = "该计划已存在【培训任务】，不能删除！";
            }

            return content;
        }
        #endregion
    }
}