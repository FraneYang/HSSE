using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Data.SqlClient;
using System.Data;

namespace FineUIPro.Web.QualityAudit
{
    public partial class ShowPerson : PageBase
    {
        #region  定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string TrainRecordId
        {
            get
            {
                return (string)ViewState["TrainRecordId"];
            }
            set
            {
                ViewState["TrainRecordId"] = value;
            }
        }

        /// <summary>
        /// 培训类型
        /// </summary>
        public string TrainTypeId
        {
            get
            {
                return (string)ViewState["TrainTypeId"];
            }
            set
            {
                ViewState["TrainTypeId"] = value;
            }
        }
        #endregion

        #region  页面加载
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.drpUnit.DataTextField = "UnitName";
                this.drpUnit.DataValueField = "UnitId";
                this.drpUnit.DataSource = BLL.UnitService.GetUnitDropDownList();
                this.drpUnit.DataBind();
                Funs.FineUIPleaseSelect(this.drpUnit);

                this.TrainRecordId = Request.Params["TrainRecordId"];
                this.TrainTypeId = Request.Params["TrainTypeId"];
                // 绑定表格
                BindGrid();
            }
        }
        #endregion

        #region  保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    Model.Training_TrainRecordDetail detail = new Model.Training_TrainRecordDetail
                    {
                        TrainDetailId = SQLHelper.GetNewID(typeof(Model.Training_TrainRecordDetail)),
                        TrainRecordId = this.TrainRecordId,
                        PersonId = Grid1.DataKeys[rowIndex][0].ToString(),
                        CheckResult = true,
                        CheckScore = 100
                    };
                    BLL.TrainRecordDetailService.AddTrainRecordDetail(detail);
                }
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
            else
            {
                Alert.ShowInParent("请至少选择一条记录！");
                return;
            }
        }
        #endregion

        #region  绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            List<string> notInPersonIds = new List<string>();
            List<string> personIds = (from x in Funs.DB.Training_TrainRecordDetail where x.TrainRecordId == this.TrainRecordId select x.PersonId).ToList();
            if (personIds.Count() > 0)
            {
                notInPersonIds.AddRange(personIds);
            }

            if (!string.IsNullOrEmpty(this.TrainTypeId))
            {
                List<string> TrainOKpersonIds = (from x in Funs.DB.Sys_User
                                                 join y in Funs.DB.Training_TrainRecordDetail
                                                 on x.UserId equals y.PersonId
                                                 join z in Funs.DB.Training_TrainRecord
                                                 on y.TrainRecordId equals z.TrainRecordId
                                                 where z.TrainTypeId == this.TrainTypeId && y.CheckResult == true 
                                                 select x.UserId).Distinct().ToList();
                if (TrainOKpersonIds.Count > 0)
                {
                    notInPersonIds.AddRange(TrainOKpersonIds);
                }
            }

            string strSql = "select * from View_Sys_User Where IsPost=0 ";
            List<SqlParameter> listStr = new List<SqlParameter>();

            if (notInPersonIds.Count > 0)
            {
                for (int i = 0; i < notInPersonIds.Count(); i++)
                {
                    if (i == 0)
                    {
                        strSql += " AND UserId not in (@Ids" + i;
                    }
                    else
                    {
                        strSql += ",@Ids" + i;
                    }
                    listStr.Add(new SqlParameter("@Ids" + i, notInPersonIds[i]));
                }
                strSql += ")";
            }
            if (this.drpUnit.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND UnitId=@UnitId";
                listStr.Add(new SqlParameter("@UnitId", this.drpUnit.SelectedValue));
            }
            if (!string.IsNullOrEmpty(this.txtPersonName.Text.Trim()))
            {
                strSql += " AND UserName LIKE @UserName";
                listStr.Add(new SqlParameter("@UserName", "%" + this.txtPersonName.Text.Trim() + "%"));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            
            var table1 = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table1;
            Grid1.DataBind();
        }
        #endregion

        #region 排序
        /// <summary>
        /// Grid1排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid();
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
    }
}