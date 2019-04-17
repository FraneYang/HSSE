using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.InformationProject
{
    public partial class FileCabinetAChange : PageBase
    {
        #region 定义项
        /// <summary>
        /// 文件明细ID
        /// </summary>
        public string FileCabinetAItemId
        {
            get
            {
                return (string)ViewState["FileCabinetAItemId"];
            }
            set
            {
                ViewState["FileCabinetAItemId"] = value;
            }
        }

        /// <summary>
        /// 文件内容
        /// </summary>
        public string values
        {
            get
            {
                return (string)ViewState["values"];
            }
            set
            {
                ViewState["values"] = value;
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
                this.FileCabinetAItemId = Request.Params["FileCabinetAItemId"];
                this.values = Request.Params["values"];
                this.FileCabinetADataBind();//加载树

            }
        }

        #region 绑定树节点
        /// <summary>
        /// 绑定树节点
        /// </summary>
        private void FileCabinetADataBind()
        {
            this.tvFileCabinetA.Nodes.Clear();
            this.tvFileCabinetA.SelectedNodeID = string.Empty;
            TreeNode rootNode = new TreeNode
            {
                Text = "文件柜",
                Expanded = true,
                NodeID = "0"
            };//定义根节点
            this.tvFileCabinetA.Nodes.Add(rootNode);
            var fileCabinetAList = BLL.FileCabinetAService.GetFileCabinetAList();
            if (fileCabinetAList.Count() > 0)
            {
                this.GetNodes(fileCabinetAList, rootNode);
            }
        }

        #region  遍历节点方法
        /// <summary>
        /// 遍历节点方法
        /// </summary>
        /// <param name="nodes">节点集合</param>
        /// <param name="parentId">父节点</param>
        private void GetNodes(List<Model.InformationProject_FileCabinetA> fileCabinetAList, TreeNode node)
        {
            var FileCabinetAs = fileCabinetAList.Where(x => x.SupFileCabinetAId == node.NodeID);
            foreach (var item in FileCabinetAs)
            {
                TreeNode newNode = new TreeNode
                {
                    Text = item.Title,
                    NodeID = item.FileCabinetAId
                };
                node.Nodes.Add(newNode);
                GetNodes(fileCabinetAList, newNode);
            }
        }
        #endregion
        #endregion

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.tvFileCabinetA.SelectedNode != null && !string.IsNullOrEmpty(this.tvFileCabinetA.SelectedNodeID))
            {
                string selectId = this.tvFileCabinetA.SelectedNodeID;
                if (!string.IsNullOrEmpty(this.FileCabinetAItemId))
                {
                    var IDList = Funs.GetStrListByStr(this.FileCabinetAItemId, '|');
                    if (IDList.Count() > 0)
                    {
                        foreach (var item in IDList)
                        {
                            var fileCabinetAItem = BLL.FileCabinetAItemService.GetFileCabinetAItemByID(item);
                            if (fileCabinetAItem != null)
                            {
                                fileCabinetAItem.FileCabinetAId = selectId;
                                BLL.FileCabinetAItemService.UpdateFileCabinetAItem(fileCabinetAItem);
                            }
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(values))
                    {
                        string menuId = Request.Params["menuId"];
                        if (menuId == BLL.Const.TestRecordMenuId)
                        {
                            var rowLists = Funs.GetStrListByStr(values, '|');
                            foreach (var idItem in rowLists)
                            {
                                ////考试记录
                                var testRecord = BLL.TestRecordService.GetTestRecordById(idItem);
                                if (testRecord != null)
                                {
                                    string content = string.Empty;
                                    var sysUser = BLL.UserService.GetUserByUserId(testRecord.TestManId);
                                    if (sysUser != null)
                                    {
                                        content += sysUser.UserName + "；";
                                    }
                                    var testPlan = BLL.TestPlanService.GetTestPlanById(testRecord.TestPlanId);
                                    if (testPlan != null)
                                    {
                                        content += testPlan.PlanName + "；";
                                    }
                                    else
                                    {
                                        var training = BLL.TrainingService.GetTrainingById(testRecord.TestType);
                                        if (training != null)
                                        {
                                            content += training.TrainingName + "；";
                                        }
                                    }
                                    if (testRecord.TestStartTime.HasValue)
                                    {
                                        content += "时间：" + string.Format("{0:yyyy-MM-dd hh:mm:ss}", testRecord.TestStartTime);
                                    }
                                    if (testRecord.TestEndTime.HasValue)
                                    {
                                        content += "至：" + string.Format("{0:yyyy-MM-dd hh:mm:ss}", testRecord.TestEndTime) + "；";
                                    }
                                    if (testRecord.TestScores.HasValue)
                                    {
                                        content += "成绩：" + testRecord.TestScores.ToString() + "。";
                                    }
                                    if (!string.IsNullOrEmpty(testRecord.TemporaryUser))
                                    {
                                        content += "备注：" + testRecord.TemporaryUser + "。";
                                    }

                                    BLL.FileCabinetAService.AddFileCabinetA(menuId, idItem, content, String.Format("../Training/TestRecordItem.aspx?TestRecordId={0}", idItem, "查看 - "), string.Format("{0:yyyy-MM-dd}", testRecord.TestStartTime), selectId);
                                    testRecord.IsFiled = true;
                                    BLL.TestRecordService.UpdateTestRecord(testRecord);
                                    BLL.LogService.AddLog(this.CurrUser.UserId, "考试记录归档");
                                }
                            }
                        }
                        else if (menuId == BLL.Const.HiddenHazardMenuId)
                        {
                            var rowLists = Funs.GetStrListByStr(values, '|');
                            foreach (var idItem in rowLists)
                            {
                                ////隐患巡检记录
                                var getHiddenHazard = BLL.HiddenHazardService.GetHiddenHazardById(idItem);
                                if (getHiddenHazard != null)
                                {
                                    string content = getHiddenHazard.HiddenHazardName;
                                    if (!string.IsNullOrEmpty(getHiddenHazard.HiddenHazardName))
                                    {
                                        content += "名称：" + getHiddenHazard.HiddenHazardName + "；";
                                    }
                                    var type = BLL.HiddenHazardTypeService.GetHiddenHazardTypeByHiddenHazardTypeId(getHiddenHazard.HiddenHazardTypeId);
                                    if (type != null)
                                    {
                                        content += "类别：" + type.HiddenHazardTypeName + "；";
                                    }
                                    if (!string.IsNullOrEmpty(getHiddenHazard.InstallationId))
                                    {
                                        content += "位置：" + BLL.InstallationService.GetInstallationNameByInstallationId(getHiddenHazard.InstallationId) + "，" + getHiddenHazard.HiddenHazardPlace;
                                    }
                                    else
                                    {
                                        content += "位置：" + getHiddenHazard.HiddenHazardPlace;
                                    }

                                    BLL.FileCabinetAService.AddFileCabinetA(menuId, idItem, content, String.Format("../Hazard/HiddenHazardView.aspx?HiddenHazardId={0}", idItem, "查看 - "), getHiddenHazard.HiddenHazardCode, selectId);
                                    getHiddenHazard.IsFiled = true;
                                    BLL.HiddenHazardService.UpdateHiddenHazard(getHiddenHazard);
                                    BLL.LogService.AddLog(this.CurrUser.UserId, "隐患巡检记录归档");
                                }
                            }
                        }
                        else if (menuId == BLL.Const.RoutingInspectionMenuId)
                        {
                            var rowLists = Funs.GetStrListByStr(values, '|');
                            foreach (var idItem in rowLists)
                            {
                                ////风险巡检记录
                                var getRoutingInspection = BLL.RoutingInspectionService.GetViewRoutingInspectionById(idItem);
                                if (getRoutingInspection != null)
                                {
                                    string content ="巡检人["+ getRoutingInspection.PatrolManName+"]；";
                                    if (getRoutingInspection.PatrolTime.HasValue)
                                    {
                                        content += "时间：" + string.Format("{0:yyyy-MM-dd HH:mm:ss}", getRoutingInspection.PatrolTime) + "；";
                                    }
                                    content += "所属单位：" + getRoutingInspection.InstallationName + "；设备设施作业活动名称：" + getRoutingInspection.TaskActivity + "；巡检结果："+ getRoutingInspection.PatrolResultName+"。";

                                    BLL.FileCabinetAService.AddFileCabinetA(menuId, idItem, content, String.Format("../Hazard/RoutingInspectionView.aspx?RoutingInspectionId={0}", idItem, "查看 - "), string.Format("{0:yyyy-MM-dd HH:mm:ss}", getRoutingInspection.PatrolTime), selectId);
                                    
                                    BLL.RoutingInspectionService.UpdateRoutingInspection(getRoutingInspection.RoutingInspectionId, true);
                                    BLL.LogService.AddLog(this.CurrUser.UserId, "风险巡检记录归档");
                                }
                            }
                        }
                    }
                }

                ShowNotify("保存成功！", MessageBoxIcon.Success);
                PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
            }
            else
            {
                Alert.ShowInTop("请先选择文件柜目录！", MessageBoxIcon.Warning);
            }
        }
    }
}