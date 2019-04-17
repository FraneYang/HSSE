using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FineUIPro.Web.ProjectData
{
    public partial class TeamGroup : PageBase
    {
        #region 加载
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
                this.btnNew.OnClientClick = Window1.GetShowReference("TeamGroupEdit.aspx") + "return false;";
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
            string strSql = @"SELECT TeamGroup.TeamGroupId,TeamGroup.UnitId,Unit.UnitCode,Unit.UnitName,TeamGroup.InstallationId,Installation.InstallationCode,Installation.InstallationName,TeamGroup.WorkAreaId,WorkArea.WorkAreaCode,WorkArea.WorkAreaName "
                            + @" ,TeamGroup.DepartId,Depart.DepartCode,Depart.DepartName,TeamGroup.TeamGroupCode,TeamGroup.TeamGroupName,TeamGroup.Remark,TeamGroup.LeaderIds,TeamGroup.LeaderNames,TeamGroup.TeamType"
                            + @" FROM dbo.Base_TeamGroup AS TeamGroup"
                            + @" LEFT JOIN Base_Unit AS Unit ON TeamGroup.UnitId=Unit.UnitId"
                            + @" LEFT JOIN Base_Depart AS Depart ON TeamGroup.DepartId=Depart.DepartId"
                            + @" LEFT JOIN Base_Installation AS Installation ON TeamGroup.InstallationId=Installation.InstallationId"
                            + @" LEFT JOIN Base_WorkArea AS WorkArea ON TeamGroup.WorkAreaId=WorkArea.WorkAreaId"
                            + @" WHERE 1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();           
            if (!string.IsNullOrEmpty(this.txtName.Text.Trim()))
            {
                strSql += " AND (TeamGroupName LIKE @Name OR UnitName LIKE @Name OR InstallationName LIKE @Name OR DepartName LIKE @Name)";
                listStr.Add(new SqlParameter("@Name", "%" + this.txtName.Text.Trim() + "%"));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;            
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        /// <summary>
        /// 改变索引事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {            
            BindGrid();
        }
        
        /// <summary>
        /// 分页下拉选择事件
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

        /// <summary>
        /// 关闭弹出窗口
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
        /// 双击事件
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
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }

            //if (this.btnMenuEdit.Hidden)   ////双击事件 编辑权限有：编辑页面，无：查看页面 或者状态是完成时查看页面
            //{
            //    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("TeamGroupView.aspx?TeamGroupId={0}", Grid1.SelectedRowID, "查看 - ")));
            //}
            //else
            //{
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("TeamGroupEdit.aspx?TeamGroupId={0}", Grid1.SelectedRowID, "编辑 - ")));
            //}                        
        }
        #endregion

        #region 删除
        /// <summary>
        /// 右键删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                bool isShow = false;
                if (Grid1.SelectedRowIndexArray.Length == 1)
                {
                    isShow = true;
                }
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    if (this.judgementDelete(rowID, isShow))
                    {
                        var TeamGroup = BLL.TeamGroupService.GetTeamGroupById(rowID);
                        if (TeamGroup != null)
                        {
                            BLL.LogService.AddLog( this.CurrUser.UserId, "删除班组信息");
                            BLL.TeamGroupService.DeleteTeamGroupById(rowID);
                        }                                                
                    }
                }
                BindGrid();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
        }

        /// <summary>
        /// 判断是否可以删除
        /// </summary>
        /// <returns></returns>
        private bool judgementDelete(string id, bool isShow)
        {
            string content = string.Empty;
           
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

        #region 格式化字符串     
        /// <summary>
        /// 根据单位Id获取单位名称字符串
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        protected string ConvertTeamType(object teamType)
        {
            string name = string.Empty;
            if (teamType != null)
            {
                if (teamType.ToString() == "1")
                {
                    name = "生产班组";
                }
                else if (teamType.ToString() == "2")
                {
                    name = "安全督察";
                }
                else if (teamType.ToString() == "3")
                {
                    name = "检修班";
                }
                else if (teamType.ToString() == "4")
                {
                    name = "电工";
                }
                else
                {
                    name = "其他";
                }
            }
            return name;
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
            if (Request.Params["value"] == "0")  ///文件柜时 没有操作权限
            {
                return;
            }
            var buttonList = BLL.CommonService.GetAllButtonList( this.CurrUser.UserId, BLL.Const.TeamGroupMenuId);
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

        #region 导出按钮
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOut_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            string filename = Funs.GetNewFileName();
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("班组信息" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = 500;
            this.BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }        
        #endregion        
    }
}