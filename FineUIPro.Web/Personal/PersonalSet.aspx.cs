using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BLL;

namespace FineUIPro.Web.Personal
{
    public partial class PersonalSet : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 照片附件路径
        /// </summary>
        public string PhotoAttachUrl
        {
            get
            {
                return (string)ViewState["PhotoAttachUrl"];
            }
            set
            {
                ViewState["PhotoAttachUrl"] = value;
            }
        }
        #endregion

        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                /// Tab1加载页面方法
                this.Tab1LoadData();
                this.BindGrid();
                if (this.CurrUser.UserId != BLL.Const.sysglyId)
                {
                    this.txtUser.Hidden = true;
                }
            }
        }

        #region Tab1
        /// <summary>
        /// Tab1加载页面方法
        /// </summary>
        private void Tab1LoadData()
        {
            //性别       
            BLL.ConstValue.InitConstValueDropDownList(this.drpSex, ConstValue.Group_Gender, true);
            //婚姻状况       
            BLL.ConstValue.InitConstValueDropDownList(this.drpMarriage, ConstValue.Group_Marriage, true);
            //民族           
            BLL.ConstValue.InitConstValueDropDownList(this.drpNation, ConstValue.Group_Nation, true);
            //所在单位
            BLL.UnitService.InitUnitDropDownList(this.drpUnit, true);          
            //文化程度         
            BLL.ConstValue.InitConstValueDropDownList(this.drpEducation, ConstValue.Group_Education, true);
            //职务          
            BLL.PositionService.InitPositionDropDownList(this.drpPosition, true);

            var user = BLL.UserService.GetUserByUserId(this.CurrUser.UserId);
            if (user != null)
            {
                this.txtUserName.Text = user.UserName;
                this.txtUserCode.Text = user.UserCode;
                if (!string.IsNullOrEmpty(user.Sex))
                {
                    this.drpSex.SelectedValue = user.Sex;
                }
                this.dpBirthDay.Text = string.Format("{0:yyyy-MM-dd}", user.BirthDay);
                if (!string.IsNullOrEmpty(user.Marriage))
                {
                    this.drpMarriage.SelectedValue = user.Marriage;
                }
                if (!string.IsNullOrEmpty(user.Nation))
                {
                    this.drpNation.SelectedValue = user.Nation;
                }
                if (!string.IsNullOrEmpty(user.UnitId))
                {
                    this.drpUnit.SelectedValue = user.UnitId;
                }
                this.txtAccount.Text = user.Account;
                this.txtIdentityCard.Text = user.IdentityCard;
                this.txtEmail.Text = user.Email;
                this.txtTelephone.Text = user.Telephone;
                if (!string.IsNullOrEmpty(user.Education))
                {
                    this.drpEducation.SelectedValue = user.Education;
                }
                this.txtHometown.Text = user.Hometown;
                if (!string.IsNullOrEmpty(user.PositionId))
                {
                    this.drpPosition.SelectedValue = user.PositionId;
                }
                this.txtPerformance.Text = user.Performance;                
                if (!string.IsNullOrEmpty(user.PhotoUrl))
                {
                    this.PhotoAttachUrl = user.PhotoUrl;
                    this.Image1.ImageUrl = "~/" + this.PhotoAttachUrl;
                }

                this.LabelName.Text = user.UserName;
                this.LabelAccount.Text = user.Account;
            }
        }

        #region 照片上传
        /// <summary>
        /// 上传照片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPhoto_Click(object sender, EventArgs e)
        {
            if (filePhoto.HasFile)
            {
                string fileName = filePhoto.ShortFileName;

                if (!ValidateFileType(fileName))
                {
                    ShowNotify("无效的文件类型！", MessageBoxIcon.Warning);
                    return;
                }
                this.PhotoAttachUrl = BLL.UploadFileService.UploadAttachment(BLL.Funs.RootPath, this.filePhoto, this.PhotoAttachUrl, UploadFileService.PersonalFilePath);
                this.Image1.ImageUrl = "~/" + this.PhotoAttachUrl;
            }
        }
        #endregion     

        #region Tab1保存按钮
        /// <summary>
        /// Tab1保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnTab1Save_Click(object sender, EventArgs e)
        {
            if (BLL.UserService.IsExistUserAccount(this.CurrUser.UserId, this.txtAccount.Text.Trim()) == true)
            {
                ShowNotify("登录账号已存在，请修改后再保存！", MessageBoxIcon.Warning);
                return;
            }
            if (!string.IsNullOrEmpty(this.txtIdentityCard.Text) && BLL.UserService.IsExistUserIdentityCard(this.CurrUser.UserId, this.txtIdentityCard.Text.Trim()) == true)
            {
                ShowNotify("身份证号码已存在，请修改后再保存！", MessageBoxIcon.Warning);
                return;
            }
            var newUser = BLL.UserService.GetUserByUserId(this.CurrUser.UserId);
            if (newUser != null)
            {
                newUser.UserName = this.txtUserName.Text.Trim();
                newUser.UserCode = this.txtUserCode.Text.Trim();
                if (this.drpSex.SelectedValue != BLL.Const._Null)
                {
                    newUser.Sex = this.drpSex.SelectedValue;
                }
                newUser.BirthDay = Funs.GetNewDateTime(this.dpBirthDay.Text);
                if (this.drpMarriage.SelectedValue != BLL.Const._Null)
                {
                    newUser.Marriage = this.drpMarriage.SelectedValue;
                }
                if (this.drpNation.SelectedValue != BLL.Const._Null)
                {
                    newUser.Nation = this.drpNation.SelectedValue;
                }
                if (this.drpUnit.SelectedValue != BLL.Const._Null)
                {
                    newUser.UnitId = this.drpUnit.SelectedValue;
                }
                newUser.Account = this.txtAccount.Text.Trim();
                newUser.IdentityCard = this.txtIdentityCard.Text.Trim();
                newUser.Email = this.txtEmail.Text.Trim();
                newUser.Telephone = this.txtTelephone.Text.Trim();
                if (this.drpEducation.SelectedValue != BLL.Const._Null)
                {
                    newUser.Education = this.drpEducation.SelectedValue;
                }
                newUser.Hometown = this.txtHometown.Text.Trim();
                if (this.drpPosition.SelectedValue != BLL.Const._Null)
                {
                    newUser.PositionId = this.drpPosition.SelectedValue;
                }
                newUser.Performance = this.txtPerformance.Text.Trim();
                newUser.PhotoUrl = this.PhotoAttachUrl;
                ShowNotify("保存成功！", MessageBoxIcon.Success);
                BLL.LogService.AddLog( this.CurrUser.UserId, "修改个人信息！");
                PageContext.RegisterStartupScript(ActiveWindow.GetHideReference());
            }            
        }
        #endregion
        #endregion
       
        #region Tab2保存按钮
        /// <summary>
        /// Tab2保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnTab2Save_Click(object sender, EventArgs e)
        {
            var user = BLL.UserService.GetUserByUserId(this.CurrUser.UserId);
            if (user != null)
            {
                if (string.IsNullOrEmpty(this.txtOldPassword.Text))
                {
                    Alert.ShowInParent("请输入原密码！");
                    return;
                }

                if (user.Password != Funs.EncryptionPassword(this.txtOldPassword.Text))
                {
                    Alert.ShowInParent("原密码输入不正确！");
                    return;
                }
                if (this.txtNewPassword.Text != this.txtConfirmPassword.Text)
                {
                    Alert.ShowInParent("确认密码输入不一致！");
                    return;
                }

                BLL.UserService.UpdatePassword(user.UserId, this.txtNewPassword.Text);
                BLL.LogService.AddLog( this.CurrUser.UserId, "修改个人密码");
                PageContext.RegisterStartupScript(ActiveWindow.GetHideReference());
            }
        }
        #endregion

        #region Tab3
        #region BindGrid
        /// <summary>
        ///  gv 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string unitName = string.Empty;
            var unit = BLL.CommonService.GetIsThisUnit();
            if (unit != null)
            {
                unitName = unit.UnitName;
            }

            string strSql = @"SELECT sysLog.LogId,sysLog.UserId,sysLog.OperationTime,sysLog.Ip,sysLog.HostName,sysLog.OperationLog,users.UserName"
                         + @",(CASE WHEN units.UnitId IS NULL THEN '" + unitName + "' ELSE units.UnitName END) AS UnitName"
                         + @" FROM dbo.Sys_Log as sysLog"
                         + @" LEFT JOIN Sys_User as users ON users.UserId=sysLog.UserId "
                         + @" LEFT JOIN Base_Unit as units on users.UnitId=units.UnitId"
                         + @" WHERE 1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (this.CurrUser.UserId != BLL.Const.sysglyId)
            {
                strSql += " AND sysLog.UserId = @UserId";
                listStr.Add(new SqlParameter("@UserId", this.CurrUser.UserId));
            }
            if (!string.IsNullOrEmpty(this.txtUnit.Text.Trim()))
            {
                strSql += " AND units.UnitName LIKE @UnitName";
                listStr.Add(new SqlParameter("@UnitName", "%" + this.txtUnit.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtUser.Text.Trim()))
            {
                strSql += " AND users.UserName LIKE @UserName";
                listStr.Add(new SqlParameter("@UserName", "%" + this.txtUser.Text.Trim() + "%"));
            }

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            
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
        }
        #endregion

        #region 分页下拉选择
        /// <summary>
        /// 分页下拉选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Grid1.PageSize = Funs.GetNewIntOrZero(this.ddlPageSize.SelectedValue);
            this.BindGrid();
        }

        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            BindGrid();
        }


        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            BindGrid();
        }
        #endregion
        #endregion
    }
}