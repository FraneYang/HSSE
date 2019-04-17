using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.QualityAudit
{
    public partial class TrainRecordEdit :PageBase
    {
        #region 定义项
        /// <summary>
        /// 人员培训ID
        /// </summary>
        private string TrainRecordId
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
        /// 定义培训明细集合
        /// </summary>
        private static List<Model.View_Training_TrainRecordDetail> trainRecordDetails = new List<Model.View_Training_TrainRecordDetail>();
        #endregion

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
                GetButtonPower();//权限设置
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();

                this.drpUnits.DataTextField = "UnitName";
                this.drpUnits.DataValueField = "UnitId";
                this.drpUnits.DataSource = BLL.UnitService.GetUnitDropDownList();
                this.drpUnits.DataBind();

                this.drpTrainTypeId.DataTextField = "TrainTypeName";
                this.drpTrainTypeId.DataValueField = "TrainTypeId";
                this.drpTrainTypeId.DataSource = BLL.TrainTypeService.GetTrainTypeList();
                this.drpTrainTypeId.DataBind();
                Funs.FineUIPleaseSelect(this.drpTrainTypeId);

                this.TrainRecordId = Request.Params["TrainRecordId"];
                if (!string.IsNullOrEmpty(this.TrainRecordId))
                {
                    Model.Training_TrainRecord trainRecord = BLL.TrainRecordService.GetTrainRecordById(this.TrainRecordId);
                    if (trainRecord != null)
                    {
                        this.txtTrainingCode.Text = trainRecord.TrainingCode;
                        this.txtTrainTitle.Text = trainRecord.TrainTitle;
                        if (!string.IsNullOrEmpty(trainRecord.TrainTypeId))
                        {
                            this.drpTrainTypeId.SelectedValue = trainRecord.TrainTypeId;
                        }
                        this.txtTrainContent.Text = trainRecord.TrainContent;
                        if (trainRecord.TrainStartDate != null)
                        {
                            this.txtStartDate.Text = string.Format("{0:yyyy-MM-dd}", trainRecord.TrainStartDate);
                        }
                        if (trainRecord.TrainEndDate != null)
                        {
                            this.txtEndDate.Text = string.Format("{0:yyyy-MM-dd}", trainRecord.TrainEndDate);
                        }
                        if (trainRecord.TeachHour != null)
                        {
                            this.txtTeachHour.Text = trainRecord.TeachHour.ToString();
                        }
                        this.txtTeachMan.Text = trainRecord.TeachMan;
                        this.txtTeachAddress.Text = trainRecord.TeachAddress;
                        this.txtRemark.Text = trainRecord.Remark;
                        if (trainRecord.TrainPersonNum != null)
                        {
                            this.txtTrainPersonNum.Text = trainRecord.TrainPersonNum.ToString();
                        }
                        if (!string.IsNullOrEmpty(trainRecord.UnitIds))
                        {
                            this.drpUnits.SelectedValueArray = trainRecord.UnitIds.Split(',');
                        }
                    }
                    trainRecordDetails = BLL.TrainRecordDetailService.GetTrainRecordDetailByTrainRecordId(this.TrainRecordId);
                    this.Grid1.DataSource = trainRecordDetails;
                    this.Grid1.DataBind();

                    if (trainRecord.States == BLL.Const.State_1)
                    {
                        this.btnSave.Hidden = true;
                        this.btnSubmit.Hidden = true;
                        this.btnSelect.Hidden = true;
                    }
                }
            }
        }
        #endregion

        #region 保存、提交
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveData(true,BLL.Const.BtnSave);
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="isClose"></param>
        private void SaveData(bool isClose,string type)
        {
            Model.Training_TrainRecord trainRecord = new Model.Training_TrainRecord
            {
                TrainingCode = this.txtTrainingCode.Text.Trim(),
                TrainTitle = this.txtTrainTitle.Text.Trim()
            };
            if (this.drpTrainTypeId.SelectedValue!=BLL.Const._Null)
            {
                trainRecord.TrainTypeId = this.drpTrainTypeId.SelectedValue;
            }
            trainRecord.TrainContent = this.txtTrainContent.Text.Trim();
            trainRecord.TrainStartDate = Funs.GetNewDateTime(this.txtStartDate.Text.Trim());
            trainRecord.TrainEndDate = Funs.GetNewDateTime(this.txtEndDate.Text.Trim());
            trainRecord.TeachHour = Funs.GetNewDecimal(this.txtTeachHour.Text.Trim());
            trainRecord.TeachMan = this.txtTeachMan.Text.Trim();
            trainRecord.TeachAddress = this.txtTeachAddress.Text.Trim();
            trainRecord.Remark = this.txtRemark.Text.Trim();
            //培训单位
            string unitIds = string.Empty;
            foreach (var item in this.drpUnits.SelectedValueArray)
            {
                unitIds += item + ",";
            }
            if (!string.IsNullOrEmpty(unitIds))
            {
                unitIds = unitIds.Substring(0, unitIds.LastIndexOf(","));
            }
            trainRecord.UnitIds = unitIds;
            trainRecord.CompileMan = this.CurrUser.UserId;
            trainRecord.TrainPersonNum = Funs.GetNewInt(this.txtTrainPersonNum.Text.Trim());
            if (type == BLL.Const.BtnSubmit)
            {
                trainRecord.States = BLL.Const.State_1;
            }
            else
            {
                trainRecord.States = BLL.Const.State_0;
            }
            if (!string.IsNullOrEmpty(this.TrainRecordId))
            {
                trainRecord.TrainRecordId = this.TrainRecordId;
                BLL.TrainRecordService.UpdateTrainRecord(trainRecord);
                BLL.LogService.AddLog(this.CurrUser.UserId, "修改人员培训");
            }
            else
            {
                this.TrainRecordId = SQLHelper.GetNewID(typeof(Model.Training_TrainRecord));
                trainRecord.TrainRecordId = this.TrainRecordId;
                BLL.TrainRecordService.AddTrainRecord(trainRecord);
                BLL.LogService.AddLog(this.CurrUser.UserId, "添加人员培训");
            }
            for (int i = 0; i < this.Grid1.Rows.Count; i++)
            {
                Model.Training_TrainRecordDetail detail = BLL.TrainRecordDetailService.GetTrainRecordDetailById(this.Grid1.Rows[i].DataKeys[0].ToString());
                if (detail != null)
                {
                    System.Web.UI.WebControls.DropDownList drpCheckResult = (System.Web.UI.WebControls.DropDownList)(this.Grid1.Rows[i].FindControl("drpCheckResult"));
                    detail.CheckResult = Convert.ToBoolean(drpCheckResult.SelectedValue);
                    detail.CheckScore = Funs.GetNewDecimalOrZero(this.Grid1.Rows[i].Values[4].ToString());
                    BLL.TrainRecordDetailService.UpdateTrainRecordDetail(detail);

                    if (type == BLL.Const.BtnSubmit)
                    {
                        if (detail.CheckResult == true)
                        {
                            var user = BLL.UserService.GetUserByUserId(detail.PersonId);
                            if (user!=null)
                            {
                                user.IsPost = true;
                                BLL.UserService.UpdateUser(user);
                            }
                        }
                    }
                }
            }
            if (isClose)
            {
                PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
            }
        }

        /// <summary>
        /// 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SaveData(true,BLL.Const.BtnSubmit);
        }
        #endregion

        #region 关闭弹出窗
        /// <summary>
        /// 关闭弹出窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, EventArgs e)
        {
            trainRecordDetails = (from x in Funs.DB.View_Training_TrainRecordDetail where x.TrainRecordId == this.TrainRecordId orderby x.UnitName select x).ToList();
            Grid1.DataSource = trainRecordDetails;
            Grid1.DataBind();
            this.txtTrainPersonNum.Text = trainRecordDetails.Count.ToString();
        }
        #endregion

        #region 删除
        /// <summary>
        /// 批量删除
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
                    BLL.TrainRecordDetailService.DeleteTrainRecordDetailById(rowID);
                }
                trainRecordDetails = (from x in Funs.DB.View_Training_TrainRecordDetail where x.TrainRecordId == this.TrainRecordId orderby x.UnitName select x).ToList();
                this.Grid1.DataSource = trainRecordDetails;
                this.Grid1.DataBind();
                this.txtTrainPersonNum.Text = trainRecordDetails.Count.ToString();
                this.ShowNotify("删除数据成功!（表格数据已重新绑定）");
            }
        }
        #endregion

        #region 选择按钮
        /// <summary>
        /// 选择按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            if (this.drpTrainTypeId.SelectedValue == BLL.Const._Null)
            {
                ShowNotify("请选择培训类型！", MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(this.TrainRecordId))
            {
                SaveData(false,BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ShowPerson.aspx?TrainRecordId={0}&TrainTypeId={1}", this.TrainRecordId, this.drpTrainTypeId.SelectedValue, "编辑 - ")));
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.TrainRecordId))
            {
                this.SaveData(false, BLL.Const.BtnSave);
            }

            if (this.btnSave.Hidden)
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/TrainRecordAttachUrl&menuId={1}&type=-1", this.TrainRecordId, BLL.Const.TrainRecordMenuId)));
            }
            else
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/TrainRecordAttachUrl&menuId={1}", this.TrainRecordId, BLL.Const.TrainRecordMenuId)));
            }
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.UserId, BLL.Const.TrainRecordMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                    this.btnSelect.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnSubmit))
                {
                    this.btnSubmit.Hidden = false;
                }
            }
        }
        #endregion
    }
}