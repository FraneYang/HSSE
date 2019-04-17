namespace FineUIPro.Web.BaseInfo
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

    public partial class RiskLevelValue : PageBase
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
                if (this.CurrUser.UserId == BLL.Const.sysglyId)
                {
                    this.btnArrowRefresh.Hidden = false;
                }

                this.ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                // 绑定表格
                this.BindGrid();
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT Value.RiskLevelValueId,Value.MinValue,Value.MaxValue,Const.ConstText AS RiskLevelName,Const.SortIndex,ControlMeasures,LimitTime,Identification"
                            + @" FROM Base_RiskLevelValue AS Value"
                            + @" LEFT JOIN Sys_Const AS Const ON Const.GroupId='" + BLL.ConstValue.Group_RiskLevel + "' AND Const.ConstValue=Value.RiskLevelId"
                            + @" WHERE 1 = 1";
            List<SqlParameter> listStr = new List<SqlParameter>();

            if (!string.IsNullOrEmpty(this.txtRiskLevelName.Text.Trim()))
            {
                strSql += " AND (Const.ConstText LIKE @RiskLevelName OR Identification LIKE @RiskLevelName OR ControlMeasures LIKE @RiskLevelName)";
                listStr.Add(new SqlParameter("@RiskLevelName", "%" + this.txtRiskLevelName.Text.Trim() + "%"));
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.UserId, BLL.Const.RiskLevelValueMenuId);
            if (buttonList.Count() > 0)
            {               
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnMenuEdit.Hidden = false;
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
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("RiskLevelValueEdit.aspx?RiskLevelValueId={0}", Id, "编辑 - ")));
        }
        
        /// <summary>
        ///  根据最新设置的风险等级值更新风险库的值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnArrowRefresh_Click(object sender, EventArgs e)
        {
            BLL.RiskLevelValueService.RefreshRiskLevelByValue();
            ShowNotify("更新成功！", MessageBoxIcon.Success);
        }
    }
}