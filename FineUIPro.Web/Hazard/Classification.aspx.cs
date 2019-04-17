using BLL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FineUIPro.Web.Hazard
{
    public partial class Classification : PageBase
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
                if (this.CurrUser.UserId == BLL.Const.sysglyId)
                {
                    this.btnRiskPlan.Hidden = false;
                    this.btnPlan.Hidden = false;
                    this.btnDelRiskListItem.Hidden = false;
                    this.btnPatrolPlan.Hidden = false;
                }

                this.ddlPageSize.SelectedValue = this.Grid1.PageSize.ToString();
                this.txtStarTime.Text = string.Format("{0:yyyy-MM-dd}", System.DateTime.Now);
                this.txtEndTime.Text= string.Format("{0:yyyy-MM-dd}", System.DateTime.Now.AddMonths(3));
                this.PatrolManDataBind(); ///加载树
            }
        }

        #region 绑定树节点
        /// <summary>
        /// 绑定树节点
        /// </summary>
        private void PatrolManDataBind()
        {
            this.tvPatrolMan.Nodes.Clear();
            this.tvPatrolMan.SelectedNodeID = string.Empty;           
            var patrolMans = Funs.DB.Hazard_PatrolPlan.Select(x => x.UserId).Distinct().ToList();
            if (patrolMans.Count() > 0)
            {
                var viewUser = (from x in Funs.DB.View_Sys_User where patrolMans.Contains(x.UserId)
                                select x).Distinct();
                if (!string.IsNullOrEmpty(this.txtName.Text.Trim()))
                {
                    viewUser = viewUser.Where(x => x.UserName.Contains(this.txtName.Text.Trim()));
                }
                viewUser = viewUser.OrderBy(x => x.UserName);
                foreach (var item in viewUser)
                {                   
                    TreeNode newNode = new TreeNode
                    {
                        Text = item.UserName,
                        NodeID = item.UserId,
                        EnableClickEvent = true
                    };
                    this.tvPatrolMan.Nodes.Add(newNode);
                }
            }
        }
        #endregion

        #region 点击TreeView
        /// <summary>
        /// 点击TreeView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tvPatrolMan_NodeCommand(object sender, TreeCommandEventArgs e)
        {
            this.BindGrid();
        }
        #endregion
               
        #region GV 绑定数据
        /// <summary>
        /// GV 绑定数据
        /// </summary>
        private void BindGrid()
        {
            this.Grid1.DataSource = null;
            this.Grid1.DataBind();
            if (!string.IsNullOrEmpty(this.tvPatrolMan.SelectedNodeID))
            {
                string strSql = "SELECT PatrolPlan.PatrolPlanId,PatrolPlan.UserId,(CASE WHEN PatrolPlan.IsRiskOwner =1 THEN '风险责任人' ELSE '岗位巡检人' END) AS IsRiskOwnerName"
                                + @" ,PatrolPlan.Frequency,PatrolPlan.StartDate,PatrolPlan.EndTime,PatrolPlan.CheckDate,RiskList.RiskListId,RiskList.InstallationId,Installation.InstallationName,sysConst.ConstText AS RiskLevelName"
                                + @" ,(CASE WHEN RiskList.LECItemId IS NOT NULL AND DataType='1' THEN '作业环境' ELSE '设备设施' END) AS TypeName,RiskList.TaskActivity,RiskList.HazardDescription,RiskList.RiskLevel,RiskList.IsUsed"
                                + @" FROM Hazard_PatrolPlan AS PatrolPlan"
                                + @" LEFT JOIN Hazard_RiskList AS RiskList ON PatrolPlan.RiskListId=RiskList.RiskListId"
                                + @" LEFT JOIN Base_Installation AS Installation ON RiskList.InstallationId=Installation.InstallationId"
                                + @" LEFT JOIN Hazard_LECItem AS LECItem ON RiskList.LECItemId=LECItem.LECItemId"
                                + @" LEFT JOIN Sys_Const AS sysConst ON RiskList.RiskLevel=sysConst.ConstValue AND sysConst.GroupId='" + BLL.ConstValue.Group_RiskLevel + "'"
                            + @" WHERE PatrolPlan.UserId ='" + this.tvPatrolMan.SelectedNodeID + "'";
                List<SqlParameter> listStr = new List<SqlParameter>();               
                if (!string.IsNullOrEmpty(this.txtTitle.Text.Trim()))
                {
                    strSql += " AND (Installation.InstallationName LIKE @Title OR RiskList.TaskActivity LIKE @Title OR RiskList.HazardDescription LIKE @Title OR sysConst.ConstText LIKE @Title)";
                    listStr.Add(new SqlParameter("@Title", "%" + this.txtTitle.Text.Trim() + "%"));
                }
                if (!string.IsNullOrEmpty(this.txtStarTime.Text.Trim()))
                {
                    strSql += " AND PatrolPlan.StartDate >= @StarTime";
                    listStr.Add(new SqlParameter("@StarTime", Funs.GetNewDateTime(this.txtStarTime.Text.Trim())));
                }
                if (!string.IsNullOrEmpty(this.txtEndTime.Text.Trim()))
                {
                    strSql += " AND PatrolPlan.EndTime <= @EndTime";
                    listStr.Add(new SqlParameter("@EndTime", Funs.GetNewDateTime(this.txtEndTime.Text.Trim())));
                }

                if (this.drpState.SelectedValue == "1")
                {
                    strSql += " AND PatrolPlan.CheckDate IS NULL";
                }
                else if (this.drpState.SelectedValue == "2")
                {
                    strSql += " AND PatrolPlan.CheckDate IS NOT NULL";
                }
                else if (this.drpState.SelectedValue == "3")
                {
                    strSql += " AND PatrolPlan.CheckDate IS NULL AND PatrolPlan.EndTime < @Now";
                    listStr.Add(new SqlParameter("@Now", System.DateTime.Now));
                }
                if (this.drpIsUsed.SelectedValue == "1")
                {
                    strSql += " AND RiskList.IsUsed = 1";
                }
                else if (this.drpIsUsed.SelectedValue == "2")
                {
                    strSql += " AND (RiskList.IsUsed = 0 OR RiskList.IsUsed IS NULL)";
                }
                SqlParameter[] parameter = listStr.ToArray();
                DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
                this.Grid1.RecordCount = tb.Rows.Count;
                //
                var table = this.GetPagedDataTable(Grid1, tb);              
                this.Grid1.DataSource = table;
                this.Grid1.DataBind();
            }
        }
        #endregion

        #region GV 排序翻页
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            this.BindGrid();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Grid1.PageSize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
            this.BindGrid();
        }

        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            this.BindGrid();
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

        #region 导出按钮
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOut_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            string filename = Funs.GetNewFileName();
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("巡检计划表" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = 10000;
            this.BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }
        #endregion

        #region 生成计划表
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRiskPlan_Click(object sender, EventArgs e)
        {
            BLL.ClassificationService.getRiskPlanItem();
            this.PatrolManDataBind(); ///加载树
            this.BindGrid();
            Alert.ShowInTop("生成数据完成！", MessageBoxIcon.Warning);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPlan_Click(object sender, EventArgs e)
        {
            //var riskListItems = from x in Funs.DB.Hazard_RiskListItem
            //                    select x;
            //if (riskListItems.Count() > 0)
            //{
            //    foreach (var item in riskListItems)
            //    {
            //        BLL.PatrolPlanService.GetPatrolPlanByRiskListItem(item);
            //    }
            //}
            BLL.PatrolPlanService.DeletePatrolPlanCheckDateNull(); ///删除巡检时间为空的数据
            BLL.ClassificationService.getPatrolPlanByRiskListItem();

            this.PatrolManDataBind(); ///加载树
            this.BindGrid();
            Alert.ShowInTop("生成数据完成！", MessageBoxIcon.Warning);
        }
        #endregion

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.PatrolManDataBind(); ///加载树
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelRiskListItem_Click(object sender, EventArgs e)
        {
            var getRiskListItem = from x in Funs.DB.Hazard_RiskListItem  select x;
            if (getRiskListItem.Count() > 0)
            {
                Funs.DB.Hazard_RiskListItem.DeleteAllOnSubmit(getRiskListItem);
                Funs.DB.SubmitChanges();
                Alert.ShowInTop("清除数据完成！", MessageBoxIcon.Warning);
            }
        }

        protected void btnPatrolPlan_Click(object sender, EventArgs e)
        {

            var patrolPlan = from x in Funs.DB.Hazard_PatrolPlan
                                  where !x.CheckDate.HasValue
                                  select x;
            if (patrolPlan.Count() > 0)
            {
                Funs.DB.Hazard_PatrolPlan.DeleteAllOnSubmit(patrolPlan);
                Funs.DB.SubmitChanges();
                Alert.ShowInTop("清除数据完成！", MessageBoxIcon.Warning);
            }
        }
    }
}