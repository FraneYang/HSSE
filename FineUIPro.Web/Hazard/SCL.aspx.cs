namespace FineUIPro.Web.Hazard
{
    using BLL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    public partial class SCL : PageBase
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
                this.InitTreeMenu();
                // 绑定表格
                this.BindGrid();
            }
        }

        #region 初始化树
        /// <summary>
        /// 初始化树
        /// </summary>
        private void InitTreeMenu()
        {
            trInstall.Nodes.Clear();
            var dt = BLL.InstallationService.GetInstallationByInstallTypeList("2");
            if (dt.Count() > 0)
            {
                foreach (var dr in dt)
                {
                    TreeNode rootNode = new TreeNode
                    {
                        Text = dr.InstallationName,
                        NodeID = dr.InstallationId,
                        EnableClickEvent = true,
                        ToolTip = dr.InstallationName
                    };

                    this.trInstall.Nodes.Add(rootNode);
                }
            }
        }        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void trInstall_NodeCommand(object sender, FineUIPro.TreeCommandEventArgs e)
        {
            this.BindGrid();
        }
        #endregion

        #region 绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string installationId = string.Empty;
            if (this.trInstall.SelectedNode != null && this.trInstall.SelectedNode.NodeID != "0")
            {
                installationId = this.trInstall.SelectedNode.NodeID;
            }
            string strSql = @"SELECT Euipments.EuipmentId,Euipments.EuipmentName,Euipments.EuipmentCode,Euipments.Remark,WorkAreas.WorkAreaCode,WorkAreas.WorkAreaName,Installation.InstallationCode,Installation.InstallationName,EuipmentNo,EuipmentType.EuipmentTypeName,Identification"
                           + @" FROM dbo.Base_Euipment AS Euipments"
                           + @" LEFT JOIN DBO.Base_Installation AS Installation ON Euipments.InstallationId =Installation.InstallationId"
                           + @" LEFT JOIN DBO.Base_WorkArea AS WorkAreas ON Euipments.WorkAreaId =WorkAreas.WorkAreaId"
                           + @" LEFT JOIN DBO.Base_EuipmentType AS EuipmentType ON Euipments.EuipmentTypeId =EuipmentType.EuipmentTypeId"
                           + @" WHERE 1 = 1";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(installationId))
            {
                strSql += " AND Euipments.InstallationId = @InstallationId";
                listStr.Add(new SqlParameter("@InstallationId", installationId));
            }
            if (!string.IsNullOrEmpty(this.txtName.Text.Trim()))
            {
                strSql += " AND (Euipments.EuipmentName LIKE @Name OR Euipments.EuipmentCode LIKE @Name OR WorkAreas.WorkAreaName LIKE @Name OR Installation.InstallationName LIKE @Name)";
                listStr.Add(new SqlParameter("@Name", "%" + this.txtName.Text.Trim() + "%"));
            }
            if (this.ckStates.SelectedValue == "0")
            {
                strSql += " AND (SELECT COUNT(SCLItemId) FROM  Hazard_SCLItem WHERE EuipmentId=Euipments.EuipmentId) =0";
            }
            else
            {
                strSql += " AND (SELECT COUNT(SCLItemId) FROM  Hazard_SCLItem WHERE EuipmentId=Euipments.EuipmentId) >0";
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

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.UserId, BLL.Const.SCLMenuId);
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
            string EuipmentId = Grid1.SelectedRowID;
            var Euipment = BLL.EuipmentService.GetEuipmentByEuipmentId(EuipmentId);
            if (Euipment != null)
            {
                var SCLItem = BLL.SCLItemService.GetSCLItemListByEuipmentId(EuipmentId);
                if (SCLItem.Count() == 0)
                {
                    var baseSCL = BLL.SCLInfoService.GetSCLInfoList(Euipment.EuipmentTypeId);
                    if (baseSCL.Count() > 0)
                    {
                        foreach (var item in baseSCL)
                        {
                            Model.Hazard_SCLItem newSCLItem = new Model.Hazard_SCLItem
                            {
                                SortIndex = item.SortIndex,
                                CheckItem = item.CheckItem,
                                Standard = item.Standard,
                                Consequence = item.Consequence,
                                NowControlMeasures = item.NowControlMeasures,
                                HazardJudge_L = item.HazardJudge_L,
                                HazardJudge_S = item.HazardJudge_S,
                                HazardJudge_R = item.HazardJudge_R,
                                ControlMeasures = item.ControlMeasures,
                                RiskLevel = item.RiskLevel,
                                EuipmentId = EuipmentId,
                                SCLItemId = SQLHelper.GetNewID(typeof(Model.Hazard_SCLItem)),
                            };
                            BLL.SCLItemService.AddSCLItem(newSCLItem);

                            Model.Hazard_SCLItemRecord newSCLItemRecord = new Model.Hazard_SCLItemRecord
                            {
                                SCLItemRecordId = BLL.SQLHelper.GetNewID(typeof(Model.Hazard_SCLItemRecord)),
                                SCLItemId = newSCLItem.SCLItemId,
                                EvaluationTime = System.DateTime.Now,
                                EvaluatorId = this.CurrUser.UserId,
                                RiskLevel = newSCLItem.RiskLevel,
                            };

                            BLL.SCLItemRecordService.AddSCLItemRecord(newSCLItemRecord);
                            ///插入综合测评
                            //  BLL.AppraisalScoreService.GetAppraisalScore(this.CurrUser.UserId, BLL.Const.SCLMenuId, 1, newSCLItem.SCLItemId);
                            ///评价提交时，更新设备设施、作业环境的风险等级。同时写入风险信息库。
                            BLL.SCLItemService.SetSubmitInfo(newSCLItem, this.CurrUser.UserId);
                        }

                        PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("SCLEdit.aspx?EuipmentId={0}", EuipmentId, "编辑 - ")));
                    }
                    else
                    {
                        Alert.ShowInTop("请在基础信息中先维护该设备类型下的SCL项！", MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("SCLEdit.aspx?EuipmentId={0}", EuipmentId, "编辑 - ")));
                }
            }
            else
            {
                Alert.ShowInTop("您选择的设备设施存在异常！", MessageBoxIcon.Warning);
                return;
            }
        }

        /// <summary>
        /// 状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ckDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }
    }
}