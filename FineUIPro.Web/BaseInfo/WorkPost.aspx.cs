using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using BLL;

namespace FineUIPro.Web.BaseInfo
{
    public partial class WorkPost : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ////权限按钮方法
                this.GetButtonPower();
                BLL.ConstValue.InitConstValueDropDownList(this.drpRiskLevel, ConstValue.Group_RiskLevel, true);
                //岗位类型            
                BLL.ConstValue.InitConstValueDropDownList(this.drpPostType, ConstValue.Group_PostType, true);
                if (this.CurrUser != null && this.CurrUser.PageSize.HasValue)
                {
                    Grid1.PageSize = this.CurrUser.PageSize.Value;
                }
                this.ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                // 绑定表格
                BindGrid();
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT  WorkPostId, WorkPostName, WorkPostCode, Remark, PostType, IsHsse, Const.ConstText AS PostTypeName, (CASE WHEN IsHsse=1 THEN '是' ELSE '否' END) AS IsHsseStr,IsAuditFlow,RiskLevelId,Frequency,RiskLevelName"
                   + @" FROM Base_WorkPost AS WorkPost"
                   + @" LEFT JOIN Sys_Const AS Const ON Const.GroupId='" + BLL.ConstValue.Group_PostType + "'  AND  WorkPost.PostType= Const.ConstValue"
                   + @" WHERE 1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();

            //if (!string.IsNullOrEmpty(this.txtWorkAreaName.Text.Trim()))
            //{
            //    strSql += " AND WorkAreaName LIKE @WorkAreaName";
            //    listStr.Add(new SqlParameter("@WorkAreaName", "%" + this.txtWorkAreaName.Text.Trim() + "%"));
            //}
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();

            //var q = from x in Funs.DB.Base_WorkPost
            //        join y in Funs.DB.Sys_Const on x.PostType equals y.ConstValue
               //     where y.GroupId == BLL.ConstValue.Group_PostType && x.IsAuditFlow == true
            //        orderby x.WorkPostCode
            //        select new { x.WorkPostId, x.WorkPostName, x.WorkPostCode, x.Remark, x.PostType, x.IsHsse, PostTypeName = y.ConstText, IsHsseStr = (x.IsHsse == true ? '是' : '否') };
            //Grid1.RecordCount = q.Count();
            //// 2.获取当前分页数据
            //var table = GetPagedDataTable(Grid1.PageIndex, Grid1.PageSize);
            //Grid1.DataSource = table;
            //Grid1.DataBind();
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <returns></returns>
        private List<Model.Base_WorkPost> GetPagedDataTable(int pageIndex, int pageSize)
        {
            List<Model.Base_WorkPost> source = (from x in BLL.Funs.DB.Base_WorkPost orderby x.PostType, x.WorkPostCode select x).ToList();
            List<Model.Base_WorkPost> paged = new List<Model.Base_WorkPost>();

            int rowbegin = pageIndex * pageSize;
            int rowend = (pageIndex + 1) * pageSize;
            if (rowend > source.Count())
            {
                rowend = source.Count();
            }

            for (int i = rowbegin; i < rowend; i++)
            {
                paged.Add(source[i]);
            }

            return paged;
        }

        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        /// <summary>
        /// 分页下拉选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            BLL.WorkPostService.DeleteWorkPostById(hfFormID.Text);
            BLL.LogService.AddLog( this.CurrUser.UserId, "删除岗位信息");
            // 重新绑定表格，并模拟点击[新增按钮]
            BindGrid();
            PageContext.RegisterStartupScript("onNewButtonClick();");
        }

        /// <summary>
        /// 右键删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();

                    BLL.WorkPostService.DeleteWorkPostById(rowID);
                    BLL.LogService.AddLog( this.CurrUser.UserId, "删除岗位信息");
                }
                BindGrid();
                PageContext.RegisterStartupScript("onNewButtonClick();");
            }
        }

        /// <summary>
        /// 右键编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuEdit_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string Id = Grid1.SelectedRowID;
            var workPost = BLL.WorkPostService.GetWorkPostById(Id);
            if (workPost != null)
            {
                this.txtWorkPostCode.Text = workPost.WorkPostCode;
                this.txtWorkPostName.Text = workPost.WorkPostName;
                if (!string.IsNullOrEmpty(workPost.PostType))
                {
                    this.drpPostType.SelectedValue = workPost.PostType;
                }
                if (workPost.IsAuditFlow == true)
                {
                    this.chkIsAuditFlow.Checked = true;
                }
                else
                {
                    this.chkIsAuditFlow.Checked = false;
                }
                if (!string.IsNullOrEmpty(workPost.RiskLevelId))
                {
                    this.drpRiskLevel.SelectedValueArray = workPost.RiskLevelId.Split(',');
                }
                this.txtFrequency.Text = workPost.Frequency.ToString();
                //if (workPost.IsHsse == true)
                //{
                //    this.ckbIsHsse.Checked = true;
                //}
                this.txtRemark.Text = workPost.Remark;
                hfFormID.Text = Id;
                this.btnDelete.Enabled = true;
            }
        }

        protected void Grid1_FilterChange(object sender, EventArgs e)
        {
            BindGrid();
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.drpPostType.SelectedValue == BLL.Const._Null)
            {
                ShowNotify("请选择类型！", MessageBoxIcon.Warning);
                return;
            }
            string strRowID = hfFormID.Text;
            Model.Base_WorkPost workPost = new Model.Base_WorkPost();
            workPost.WorkPostCode = this.txtWorkPostCode.Text.Trim();
            workPost.WorkPostName = this.txtWorkPostName.Text.Trim();
            workPost.PostType = this.drpPostType.SelectedValue;
            workPost.IsAuditFlow = Convert.ToBoolean(this.chkIsAuditFlow.Checked);
            workPost.Remark = txtRemark.Text.Trim();
            workPost.Frequency = Funs.GetNewInt(this.txtFrequency.Text);

            ///巡检级别
            string riskLevelIds = string.Empty;
            string riskLevelNames = string.Empty;
            foreach (var item in this.drpRiskLevel.SelectedValueArray)
            {
                var role = BLL.ConstValue.GetConstByConstValueAndGroupId(item, BLL.ConstValue.Group_RiskLevel);
                if (role != null)
                {
                    riskLevelIds += role.ConstValue + ",";
                    riskLevelNames += role.ConstText + ",";
                }
            }
            if (!string.IsNullOrEmpty(riskLevelIds))
            {
                workPost.RiskLevelId = riskLevelIds.Substring(0, riskLevelIds.LastIndexOf(","));
                workPost.RiskLevelName = riskLevelNames.Substring(0, riskLevelNames.LastIndexOf(","));
            }

            if (string.IsNullOrEmpty(strRowID))
            {
                workPost.WorkPostId = SQLHelper.GetNewID(typeof(Model.Base_WorkPost));
                BLL.WorkPostService.AddWorkPost(workPost);
                BLL.LogService.AddLog( this.CurrUser.UserId, "添加岗位信息");
            }
            else
            {
                workPost.WorkPostId = strRowID;
                BLL.WorkPostService.UpdateWorkPost(workPost);
                BLL.LogService.AddLog( this.CurrUser.UserId, "修改岗位信息");
            }
            this.SimpleForm1.Reset();
            // 重新绑定表格，并点击当前编辑或者新增的行
            BindGrid();
            PageContext.RegisterStartupScript(String.Format("F('{0}').selectRow('{1}');", Grid1.ClientID, workPost.WorkPostId));
        }

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList( this.CurrUser.UserId, BLL.Const.WorkPostMenuId);
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
                    this.btnDelete.Hidden = false;
                    this.btnMenuDelete.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
            }
        }
        #endregion

        #region 验证岗位名称、编号是否存在
        /// <summary>
        /// 验证岗位名称、编号是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = Funs.DB.Base_WorkPost.FirstOrDefault(x => x.WorkPostCode == this.txtWorkPostCode.Text.Trim() && (x.WorkPostId != hfFormID.Text || (hfFormID.Text == null && x.WorkPostId != null)));
            if (q != null)
            {
                ShowNotify("输入的岗位编号已存在！", MessageBoxIcon.Warning);
            }

            var q2 = Funs.DB.Base_WorkPost.FirstOrDefault(x => x.WorkPostName == this.txtWorkPostName.Text.Trim() && (x.WorkPostId != hfFormID.Text || (hfFormID.Text == null && x.WorkPostId != null)));
            if (q2 != null)
            {
                ShowNotify("输入的岗位名称已存在！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 格式化字符串
        /// <summary>
        /// 获取工作阶段
        /// </summary>
        /// <param name="WorkStage"></param>
        /// <returns></returns>
        protected string ConvertPostType(object PostType)
        {
            string name = string.Empty;
            if (PostType != null)
            {
                string postType = PostType.ToString().Trim();
                Model.Sys_Const c = ConstValue.drpConstItemList(ConstValue.Group_PostType).FirstOrDefault(x => x.ConstValue == postType);
                if (c != null)
                {
                    name = c.ConstText;
                }
            }
            return name;
        }
        #endregion
    }
}