using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web.UI;
using BLL;

namespace FineUIPro.Web.SysManage
{
    public partial class UserIn : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 上传预设的虚拟路径
        /// </summary>
        private string initPath = Const.ExcelUrl;

        /// <summary>
        /// 人员集合
        /// </summary>
        public static List<Model.View_Sys_User> userViews = new List<Model.View_Sys_User>();

        /// <summary>
        /// 错误集合
        /// </summary>
        public static string errorInfos = string.Empty;
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
                this.hdFileName.Text = string.Empty;
                this.hdCheckResult.Text = string.Empty;
                if (userViews != null)
                {
                    userViews.Clear();
                }
                errorInfos = string.Empty;
            }
        }
        #endregion

        #region 审核
        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAudit_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.fuAttachUrl.HasFile == false)
                {
                    ShowNotify("请您选择Excel文件！", MessageBoxIcon.Warning);
                    return;
                }
                string IsXls = Path.GetExtension(this.fuAttachUrl.FileName).ToString().Trim().ToLower();
                if (IsXls != ".xls")
                {
                    ShowNotify("只可以选择Excel文件！", MessageBoxIcon.Warning);
                    return;
                }
                if (userViews != null)
                {
                    userViews.Clear();
                }
                if (!string.IsNullOrEmpty(errorInfos))
                {
                    errorInfos = string.Empty;
                }
                string rootPath = Server.MapPath("~/");
                string initFullPath = rootPath + initPath;
                if (!Directory.Exists(initFullPath))
                {
                    Directory.CreateDirectory(initFullPath);
                }

                this.hdFileName.Text = BLL.Funs.GetNewFileName() + IsXls;
                string filePath = initFullPath + this.hdFileName.Text;
                this.fuAttachUrl.PostedFile.SaveAs(filePath);
                //PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("PersonDataAudit.aspx?FileName={0}&ProjectId={1}", this.hdFileName.Text, Request.Params["ProjectId"], "审核 - ")));
                ImportXlsToData(rootPath + initPath + this.hdFileName.Text);
            }
            catch (Exception ex)
            {
                ShowNotify("'" + ex.Message + "'", MessageBoxIcon.Warning);
            }
        }

        #region 读Excel提取数据
        /// <summary>
        /// 从Excel提取数据--》Dataset
        /// </summary>
        /// <param name="filename">Excel文件路径名</param>
        private void ImportXlsToData(string fileName)
        {
            try
            {
                userViews.Clear();
                string oleDBConnString = String.Empty;
                oleDBConnString = "Provider=Microsoft.Jet.OLEDB.4.0;";
                oleDBConnString += "Data Source=";
                oleDBConnString += fileName;
                oleDBConnString += ";Extended Properties=Excel 8.0;";
                OleDbConnection oleDBConn = null;
                OleDbDataAdapter oleAdMaster = null;
                DataTable m_tableName = new DataTable();
                DataSet ds = new DataSet();

                oleDBConn = new OleDbConnection(oleDBConnString);
                oleDBConn.Open();
                m_tableName = oleDBConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                if (m_tableName != null && m_tableName.Rows.Count > 0)
                {

                    m_tableName.TableName = m_tableName.Rows[0]["TABLE_NAME"].ToString().Trim();

                }
                string sqlMaster;
                sqlMaster = " SELECT *  FROM [" + m_tableName.TableName + "]";
                oleAdMaster = new OleDbDataAdapter(sqlMaster, oleDBConn);
                oleAdMaster.Fill(ds, "m_tableName");
                oleAdMaster.Dispose();
                oleDBConn.Close();
                oleDBConn.Dispose();

                AddDatasetToSQL(ds.Tables[0]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 将Dataset的数据导入数据库
        /// <summary>
        /// 将Dataset的数据导入数据库
        /// </summary>
        /// <param name="pds">数据集</param>
        /// <param name="Cols">数据集行数</param>
        /// <returns></returns>
        private bool AddDatasetToSQL(DataTable pds)
        {
            string result = string.Empty;
            int ic, ir;
            ic = pds.Columns.Count;
            ir = pds.Rows.Count;
            if (pds != null && ir > 0)
            {
                var units = from x in Funs.DB.Base_Unit select x;
                var depart = from x in Funs.DB.Base_Depart select x;
                var role = from x in Funs.DB.Sys_Role select x;
                var installation = from x in Funs.DB.Base_Installation select x;
                var workPost = from x in Funs.DB.Base_WorkPost select x;

                for (int i = 0; i < ir; i++)
                {
                    Model.View_Sys_User newSysUser = new Model.View_Sys_User
                    {
                        SortIndex = pds.Rows[i][0].ToString().Trim(),
                        UserCode = pds.Rows[i][1].ToString().Trim()
                    };
                    string col2 = pds.Rows[i][2].ToString().Trim();
                    if (string.IsNullOrEmpty(col2))
                    {
                        result += "第" + (i + 2).ToString() + "行," + "人员姓名" + "," + "此项为必填项！" + "|";
                    }
                    //else
                    //{
                    //    var userName = userViews.FirstOrDefault(x => x.UserName == col2);
                    //    var userName2 = Funs.DB.Sys_User.FirstOrDefault(x => x.UserName == col2);

                    //    if (userName != null || userName2 != null)
                    //    {
                    //        result += "第" + (i + 2).ToString() + "行," + "人员姓名" + "," + "已存在！" + "|";
                    //    }
                    //}

                    newSysUser.UserName = col2;

                    string col3 = pds.Rows[i][3].ToString().Trim();
                    if (string.IsNullOrEmpty(col3))
                    {
                        result += "第" + (i + 2).ToString() + "行," + "登录账号" + "," + "此项为必填项！" + "|";
                    }
                    newSysUser.Account = col3;

                    string col4 = pds.Rows[i][4].ToString().Trim();
                    if (!string.IsNullOrEmpty(col4))
                    {
                        var unit = units.FirstOrDefault(e => e.UnitName == col4);
                        if (unit == null)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "所属单位" + "," + "[" + col4 + "]不在单位表中！" + "|";
                        }
                        else
                        {
                            newSysUser.UnitId = unit.UnitId;
                            newSysUser.UnitName = unit.UnitName;
                        }

                    }
                    else
                    {
                        result += "第" + (i + 2).ToString() + "行," + "所属单位" + "," + "此项为必填项！" + "|";
                    }

                    string col5 = pds.Rows[i][5].ToString().Trim();
                    if (!string.IsNullOrEmpty(col5))
                    {
                        var d = depart.FirstOrDefault(e => e.DepartName == col5);
                        if (d == null)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "部门" + "," + "[" + col5 + "]错误！" + "|";
                        }
                        else
                        {
                            newSysUser.DepartId = d.DepartId;
                            newSysUser.DepartName = d.DepartName;
                        }
                    }
                    else
                    {
                        result += "第" + (i + 2).ToString() + "行," + "部门" + "," + "此项为必填项！" + "|";
                    }
                    ///////待处理科室/装置 岗位
                    string col6 = pds.Rows[i][6].ToString().Trim();
                    if (!string.IsNullOrEmpty(col6) && !String.IsNullOrEmpty(newSysUser.DepartId))
                    {
                        List<string> installs = Funs.GetStrListByStr(col6, ',');
                        if (installs.Count() > 0)
                        {
                            string ids = string.Empty;
                            foreach (var item in installs)
                            {
                                var install = installation.FirstOrDefault(x => x.InstallationName == item);
                                if (install != null)
                                {
                                    ids += install.InstallationId + ",";
                                }
                                else
                                {
                                    Model.Base_Installation newInstallation = new Model.Base_Installation
                                    {
                                        InstallationId = SQLHelper.GetNewID(typeof(Model.Base_Installation)),
                                        DepartId = newSysUser.DepartId,
                                        InstallationName = item,
                                        IsUsed = true,
                                        Def = "系统导入"
                                    };
                                    BLL.InstallationService.AddInstallation(newInstallation);
                                    ids += newInstallation.InstallationId + ",";
                                }
                            }
                            if (!string.IsNullOrEmpty(ids))
                            {
                                newSysUser.InstallationName = col6;
                                newSysUser.InstallationId = ids.Substring(0, ids.LastIndexOf(","));
                            }
                        }
                    }


                    string col7 = pds.Rows[i][7].ToString().Trim();
                    if (!string.IsNullOrEmpty(col7))
                    {
                        List<string> workPosts = Funs.GetStrListByStr(col7, ',');
                        if (workPosts.Count() > 0)
                        {
                            string ids = string.Empty;
                            foreach (var item in workPosts)
                            {
                                var wp = workPost.FirstOrDefault(x => x.WorkPostName == item);
                                if (wp != null)
                                {
                                    ids += wp.WorkPostId + ",";
                                }
                                else
                                {
                                    Model.Base_WorkPost newWorkPost = new Model.Base_WorkPost
                                    {
                                        WorkPostId = SQLHelper.GetNewID(typeof(Model.Base_WorkPost)),
                                        WorkPostName = item,
                                        PostType = "1",
                                        IsAuditFlow = false,
                                        IsUsed = true,
                                        Remark = "系统导入"
                                    };
                                    BLL.WorkPostService.AddWorkPost(newWorkPost);
                                    ids += newWorkPost.WorkPostId + ",";
                                }
                            }
                            if (!string.IsNullOrEmpty(ids))
                            {
                                newSysUser.WorkPostName = col7;
                                newSysUser.WorkPostId = ids.Substring(0, ids.LastIndexOf(","));
                            }
                        }
                    }

                    string col8 = pds.Rows[i][8].ToString().Trim();
                    if (!string.IsNullOrEmpty(col8))
                    {
                        var r = role.FirstOrDefault(e => e.RoleName == col8);
                        if (r == null)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "角色" + "," + "[" + col8 + "]错误！" + "|";
                        }
                        else
                        {
                            newSysUser.RoleId = r.RoleId;
                            newSysUser.RoleName = r.RoleName;
                        }

                    }
                    newSysUser.IdentityCard = pds.Rows[i][9].ToString().Trim();
                    newSysUser.Telephone = pds.Rows[i][10].ToString().Trim();

                    string col11 = pds.Rows[i][11].ToString().Trim();
                    if (string.IsNullOrEmpty(col11))
                    {
                        result += "第" + (i + 2).ToString() + "行," + "在岗" + "," + "此项为必填项！" + "|";
                    }
                    if (col11 == "否")
                    {
                        newSysUser.IsPost = false;
                    }
                    else
                    {
                        newSysUser.IsPost = true;
                    }
                    ///加入用户试图
                    userViews.Add(newSysUser);

                }
                if (!string.IsNullOrEmpty(result))
                {
                    userViews.Clear();
                    result = result.Substring(0, result.LastIndexOf("|"));
                    errorInfos = result;
                    Alert alert = new Alert
                    {
                        Message = result,
                        Target = Target.Self
                    };
                    alert.Show();
                }
                else
                {
                    errorInfos = string.Empty;
                    if (userViews.Count > 0)
                    {
                        this.Grid1.Hidden = false;
                        this.Grid1.DataSource = userViews;
                        this.Grid1.DataBind();
                        Alert.ShowInTop("审核完成,请点击保存！", MessageBoxIcon.Success);
                    }
                    else
                    {
                        Alert.ShowInTop("导入数据为空！", MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                Alert.ShowInTop("导入数据为空！", MessageBoxIcon.Warning);
            }
            return true;
        }
        #endregion
        #endregion
       
        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(errorInfos))
            {
                int a = userViews.Count();
                for (int i = 0; i < a; i++)
                {
                    Model.Sys_User newUser = new Model.Sys_User
                    {
                        SortIndex = userViews[i].SortIndex,
                        UserCode = userViews[i].UserCode,
                        UserName = userViews[i].UserName,
                        Account = userViews[i].Account,
                        UnitId = userViews[i].UnitId,
                        DepartId = userViews[i].DepartId,
                        InstallationId = userViews[i].InstallationId,
                        InstallationName = userViews[i].InstallationName,
                        WorkPostId = userViews[i].WorkPostId,
                        WorkPostName = userViews[i].WorkPostName,
                        RoleId = userViews[i].RoleId,
                        IdentityCard = userViews[i].IdentityCard,
                        Telephone = userViews[i].Telephone,
                        IsPost = userViews[i].IsPost,                        
                    };
                    var getUser = Funs.DB.Sys_User.FirstOrDefault(x => x.Account == userViews[i].Account);
                    if (getUser == null)
                    {
                        newUser.Password = Funs.EncryptionPassword(Const.Password);
                        BLL.UserService.AddUser(newUser);
                    }
                    else
                    {
                        newUser.UserId = getUser.UserId;
                        BLL.UserService.UpdateUser(newUser);
                    }
                }
                string rootPath = Server.MapPath("~/");
                string initFullPath = rootPath + initPath;
                string filePath = initFullPath + this.hdFileName.Text;
                if (filePath != string.Empty && System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);//删除上传的XLS文件
                }
                ShowNotify("导入成功！", MessageBoxIcon.Success);
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
            else
            {
                Alert.ShowInTop("请先将错误数据修正，再重新导入保存！", MessageBoxIcon.Warning);
            }
        }
        #endregion        

        #region 关闭弹出窗口
        /// <summary>
        /// 关闭导入弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window2_Close(object sender, WindowCloseEventArgs e)
        {
            if (Session["persons"] != null)
            {
                userViews = Session["persons"] as List<Model.View_Sys_User>;
            }
            if (userViews.Count > 0)
            {
                this.Grid1.Hidden = false;
                this.Grid1.DataSource = userViews;
                this.Grid1.DataBind();
            }
        }
        
        #endregion

        #region 下载模板
        /// <summary>
        /// 下载模板按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDownLoad_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Confirm.GetShowReference("确定下载导入模板吗？", String.Empty, MessageBoxIcon.Question, PageManager1.GetCustomEventReference(false, "Confirm_OK"), PageManager1.GetCustomEventReference("Confirm_Cancel")));
        }

        /// <summary>
        /// 下载导入模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void PageManager1_CustomEvent(object sender, CustomEventArgs e)
        {
            if (e.EventArgument == "Confirm_OK")
            {
                string rootPath = Server.MapPath("~/");
                string uploadfilepath = rootPath + Const.UserTemplateUrl;
                string filePath = Const.UserTemplateUrl;
                string fileName = Path.GetFileName(filePath);
                FileInfo info = new FileInfo(uploadfilepath);
                long fileSize = info.Length;
                Response.ClearContent();
                Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
                Response.ContentType = "excel/plain";
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                Response.AddHeader("Content-Length", fileSize.ToString().Trim());
                Response.TransmitFile(uploadfilepath, 0, fileSize);
                Response.End();
            }
        }
        #endregion
    }
}