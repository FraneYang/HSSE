using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.InformationProject
{
    public partial class FileCabinetAEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 上级文件柜项
        /// </summary>
        public string SupFileCabinetAId
        {
            get
            {
                return (string)ViewState["SupFileCabinetAId"];
            }
            set
            {
                ViewState["SupFileCabinetAId"] = value;
            }
        }

        /// <summary>
        /// 文件柜项
        /// </summary>
        public string FileCabinetAId
        {
            get
            {
                return (string)ViewState["FileCabinetAId"];
            }
            set
            {
                ViewState["FileCabinetAId"] = value;
            }
        }

        /// <summary>
        /// 项目id
        /// </summary>
        public string ProjectId
        {
            get
            {
                return (string)ViewState["ProjectId"];
            }
            set
            {
                ViewState["ProjectId"] = value;
            }
        }
        #endregion

        /// <summary>
        /// 角色编辑页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.FileCabinetAId = Request.Params["FileCabinetAId"];
                this.SupFileCabinetAId = Request.Params["SupFileCabinetAId"];
                this.ProjectId = Request.Params["ProjectId"];
                if (!string.IsNullOrEmpty(this.FileCabinetAId))
                {
                    var FileCabinetA = BLL.FileCabinetAService.GetFileCabinetAByID(this.FileCabinetAId);
                    if (FileCabinetA != null)
                    {
                        this.SupFileCabinetAId = FileCabinetA.SupFileCabinetAId;
                        this.txtTitle.Text = FileCabinetA.Title;
                        this.txtCode.Text = FileCabinetA.Code;
                    }
                }
            }
        }


        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string staName = this.txtTitle.Text.Trim();
            if (!string.IsNullOrEmpty(staName))
            {
                if (!BLL.FileCabinetAService.IsExistTitle(this.FileCabinetAId, this.SupFileCabinetAId, staName))
                {
                    Model.InformationProject_FileCabinetA newFileCabinetA = new Model.InformationProject_FileCabinetA
                    {
                        Title = staName,
                        SupFileCabinetAId = this.SupFileCabinetAId,
                        Code = this.txtCode.Text.Trim(),
                    };
                    if (string.IsNullOrEmpty(this.FileCabinetAId))
                    {
                        newFileCabinetA.FileCabinetAId = SQLHelper.GetNewID(typeof(Model.InformationProject_FileCabinetA));
                        BLL.FileCabinetAService.AddFileCabinetA(newFileCabinetA);
                        BLL.LogService.AddLog(this.CurrUser.UserId, "增加文件柜");
                    }
                    else
                    {
                        newFileCabinetA.FileCabinetAId = this.FileCabinetAId;
                        BLL.FileCabinetAService.UpdateFileCabinetA(newFileCabinetA);
                        BLL.LogService.AddLog( this.CurrUser.UserId, "修改文件柜");
                    }

                    PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                }
                else
                {
                    Alert.ShowInParent("文件柜名称已存在！", MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                Alert.ShowInParent("文件柜名称不能为空！");
                return;
            }
        }
    }
}