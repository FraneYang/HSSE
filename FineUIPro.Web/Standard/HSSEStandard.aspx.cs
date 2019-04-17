namespace FineUIPro.Web.Standard
{
    using BLL;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    /// <summary>
    /// 
    /// </summary>
    public partial class HSSEStandard : PageBase
    {
        #region 定义项
        /// <summary>
        /// 专业主键
        /// </summary>
        public string SpecialtyId
        {
            get
            {
                return (string)ViewState["SpecialtyId"];
            }
            set
            {
                ViewState["SpecialtyId"] = value;
            }
        }

        /// <summary>
        /// 标准名称主键
        /// </summary>
        public string StandardId
        {
            get
            {
                return (string)ViewState["StandardId"];
            }
            set
            {
                ViewState["StandardId"] = value;
            }
        }

        /// <summary>
        /// 管理对象主键
        /// </summary>
        public string ManagedObjectId
        {
            get
            {
                return (string)ViewState["ManagedObjectId"];
            }
            set
            {
                ViewState["ManagedObjectId"] = value;
            }
        }
        /// <summary>
        /// 管理项目主键
        /// </summary>
        public string ManagedItemId
        {
            get
            {
                return (string)ViewState["ManagedItemId"];
            }
            set
            {
                ViewState["ManagedItemId"] = value;
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ////权限按钮方法
                this.GetButtonPower();
                BLL.SpecialtyService.InitSpecialtyRadioButtonList(this.ckSpecialty);
                this.SpecialtyId = this.ckSpecialty.SelectedValue;
                this.ddlPageSize.SelectedValue = this.Grid1.PageSize.ToString();
                this.HSSEStandardDataBind();//加载树
            }
        }

        
        /// <summary>
        /// 专业选择联动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ckSpecialty_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SpecialtyId = this.ckSpecialty.SelectedValue;
            this.HSSEStandardDataBind();
            this.BindGrid();
        }

        #region 绑定树节点
        /// <summary>
        /// 绑定树节点
        /// </summary>
        private void HSSEStandardDataBind()
        {
            this.tvHSSEStandard.Nodes.Clear();
            this.tvHSSEStandard.SelectedNodeID = string.Empty;
          
            var specialty = BLL.SpecialtyService.GetSpecialtyList();
            if (specialty.Count() > 0)
            {
                var standards = BLL.StandardService.GetStandardListBySpecialtyId(this.SpecialtyId);
                if (standards.Count() > 0)
                {
                    foreach(var standard in standards)
                    {
                        TreeNode rootNode = new TreeNode
                        {
                            Text = standard.StandardName,
                            NodeID = standard.StandardId,
                            ToolTip = standard.StandardName,
                            Expanded = true,
                            EnableClickEvent = true

                        };//定义根节点
                        this.tvHSSEStandard.Nodes.Add(rootNode);
                        var managedObjects = BLL.ManagedObjectService.GetManagedObjectListByStandardId(standard.StandardId);
                        if (managedObjects.Count() > 0)
                        {
                            foreach(var managedObject in managedObjects)
                            {
                                TreeNode newNode1 = new TreeNode
                                {
                                    Text = managedObject.ManagedObjectName,
                                    NodeID = managedObject.ManagedObjectId,
                                    ToolTip = managedObject.ManagedObjectName,
                                    EnableClickEvent = true
                                };
                                rootNode.Nodes.Add(newNode1);

                                var managedItems = BLL.ManagedItemService.GetManagedItemListByManagedObjectId(managedObject.ManagedObjectId);
                                if (managedItems.Count() > 0)
                                {
                                    foreach (var managedItem in managedItems)
                                    {
                                        TreeNode newNode2 = new TreeNode
                                        {
                                            Text = managedItem.ManagedItemName,
                                            NodeID = managedItem.ManagedItemId,
                                            ToolTip = managedItem.ManagedItemName,
                                            EnableClickEvent = true
                                        };
                                        newNode1.Nodes.Add(newNode2);
                                    }
                                }
                            }                           
                        }
                    }                   
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
        protected void tvHSSEStandard_NodeCommand(object sender, TreeCommandEventArgs e)
        {          
            this.SerPageId();
        }

        /// <summary>
        /// 设置页面定义的主键值
        /// </summary>
        private void SerPageId()
        {
            this.StandardId = string.Empty;
            this.ManagedObjectId = string.Empty;
            this.ManagedItemId = string.Empty;
            var selectValue = this.tvHSSEStandard.SelectedNodeID;
            var managedItem = BLL.ManagedItemService.GetManagedItemById(selectValue);
            if (managedItem != null)
            {
                this.ManagedItemId = managedItem.ManagedItemId;
                this.ManagedObjectId = managedItem.ManagedObjectId;
                this.StandardId = this.tvHSSEStandard.SelectedNode.ParentNode.ParentNode.NodeID;
            }
            else
            {
                var managedObject = BLL.ManagedObjectService.GetManagedObjectById(selectValue);
                if (managedObject != null)
                {
                    this.ManagedObjectId = managedObject.ManagedObjectId;
                    this.StandardId = managedObject.StandardId;
                }
                else
                {
                    var standard = BLL.StandardService.GetStandardById(selectValue);
                    if (standard != null)
                    {
                        this.StandardId = standard.StandardId;
                    }
                }
            }
            this.BindGrid();
        }
        #endregion

        #region 绑定明细列表数据
        /// <summary>
        /// 绑定明细列表数据
        /// </summary>
        private void BindGrid()
        {
            this.Grid1.DataSource = null;
            this.Grid1.DataBind();
            if (!string.IsNullOrEmpty(this.ManagedItemId))
            {
                string strSql = @"SELECT HSSEStandardId,ManagedItemId,HSSEStandardName,HSSEStandardCode"
                        + @" FROM dbo.Standard_HSSEStandard WHERE ManagedItemId=@ManagedItemId";
                SqlParameter[] parameter = new SqlParameter[]
                    {
                        new SqlParameter("@ManagedItemId",this.ManagedItemId)
                    };

                DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
                this.Grid1.RecordCount = tb.Rows.Count;
                var table = this.GetPagedDataTable(this.Grid1, tb);
                this.Grid1.DataSource = table;
                this.Grid1.DataBind();
            }
        }
        #endregion

        #region gv排序翻页
        #region 页索引改变事件
        /// <summary>
        /// 页索引改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 排序
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {          
            BindGrid();
        }
        #endregion

        #region 分页选择下拉改变事件
        /// <summary>
        /// 分页选择下拉改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Grid1.PageSize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
            BindGrid();
        }
        #endregion
        #endregion

        #region 专家辅助下载原文
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            this.SerPageId();
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/Standard&type=-1", this.StandardId)));
        }
        #endregion

        #region 专家辅助维护
        /// <summary>
        /// 增加明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNewDetail_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.ManagedItemId))
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("HSSEStandardEdit.aspx?ManagedItemId={0}", this.ManagedItemId, "新增 - ")));
            }
            else
            {
                Alert.ShowInTop("请选择管理项目！", MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuModifyDetail_Click(object sender, EventArgs e)
        {
            EditData();
        }

        /// <summary>
        /// 编辑数据方法
        /// </summary>
        private void EditData()
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInParent("请至少选择一条记录！");
                return;
            }            
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("HSSEStandardEdit.aspx?HSSEStandardId={0}", Grid1.SelectedRowID, "修改 - ")));            
        }
        /// <summary>
        /// 双击修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            EditData();
        }

        /// <summary>
        /// 删除明细方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelDetail_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    var item = BLL.HSSEStandardService.GetHSSEStandardById(Grid1.DataKeys[rowIndex][0].ToString());
                    if (item != null)
                    {
                        BLL.HSSEStandardService.DeleteHSSEStandardById(item.HSSEStandardId);
                        BLL.LogService.AddLog( this.CurrUser.UserId, "删除专家辅助信息");  
                    }
                }
                this.BindGrid();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
        }
        #endregion

        #region 关闭弹出窗口
        /// <summary>
        /// 关闭弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }

        /// <summary>
        /// 关闭导入弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window2_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 导入
        /// <summary>
        /// 导入按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImport_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("HSSEStandardIn.aspx", "导入 - ")));
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.UserId, BLL.Const.HSSEStandardMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnNewDetail.Hidden = false;
                    this.btnImport.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnMenuModifyDetail.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnMenuDelDetail.Hidden = false;
                }
            }
        }
        #endregion
    }
}