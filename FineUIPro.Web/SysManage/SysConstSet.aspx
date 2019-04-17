<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysConstSet.aspx.cs" Inherits="FineUIPro.Web.SysManage.SysConstSet" ValidateRequest="false" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>系统环境设置</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
     <style type="text/css">
        .f-grid-row .f-grid-cell-inner {
            white-space: normal;
            word-break: break-all;
        }     
    </style>
</head>
<body>
    <form id="form1" runat="server">   
    <f:PageManager ID="PageManager1" runat="server" />         
      <f:Panel ID="Panel2" runat="server" ShowHeader="false" ShowBorder="false" ColumnWidth="100%" MarginRight="5px">
             <Items>                
               <f:TabStrip ID="TabStrip2" CssClass="f-tabstrip-theme-simple" Height="480px" ShowBorder="true"
                    TabPosition="Top" MarginBottom="5px" EnableTabCloseMenu="false" runat="server" 
                    ActiveTabIndex="0">              
                    <Tabs>     
                        <f:Tab ID="Tab3" Title="菜单设置" BodyPadding="5px" Layout="VBox" IconFont="Bookmark" runat="server">
                           <Toolbars>
                                <f:Toolbar ID="Toolbar3" Position="Top" ToolbarAlign="Right" runat="server">
                                    <Items>
                                        <f:DropDownBox runat="server" ID="drpMenu"  EmptyText="请选择菜单" Width="500px"
                                            EnableMultiSelect="false" Label="业务菜单" AutoPostBack="true" OnTextChanged="drpMenu_TextChanged">
                                            <PopPanel>
                                                <f:Tree ID="treeMenu" ShowHeader="false" Hidden="true" runat="server" EnableSingleExpand="true">
                                                </f:Tree>
                                            </PopPanel>
                                        </f:DropDownBox>
                                    <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>                                                        
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                           <Items>                                
                                <f:TabStrip ID="TabStrip1" CssClass="f-tabstrip-theme-simple" Height="480px" ShowBorder="true"
                                    TabPosition="Top" MarginBottom="5px" EnableTabCloseMenu="false" runat="server" 
                                    ActiveTabIndex="0">              
                                    <Tabs>     
                                        <f:Tab ID="Tab2" Title="流程设置" BodyPadding="5px" Layout="VBox" IconFont="Bookmark" runat="server">                             
                                            <Items>
                                            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false"  EnableCollapse="true" EnableColumnLines="true" EnableColumnMove="true"
                                                runat="server" BoxFlex="1" DataKeyNames="FlowOperateId" AllowCellEditing="true" ClicksToEdit="2"
                                                DataIDField="FlowOperateId" AllowSorting="true" SortField="FlowStep" SortDirection="ASC" 
                                                OnSort="Grid1_Sort" AllowPaging="true"  EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick"
                                                EnableTextSelection="True" OnRowCommand="Grid1_RowCommand">
                                                <Toolbars>
                                                    <f:Toolbar ID="Toolbar4" Position="Top"  ToolbarAlign="Right" runat="server">
                                                        <Items>                                                            
                                                            <f:Button ID="btnFlowOperateNew" ToolTip="增加" Icon="Add" runat="server"  OnClick="btnFlowOperateNew_Click" />
                                                            <f:Button ID="btnFlowOperateDelete" ToolTip="删除" Hidden="true" Icon="Delete" ConfirmText="确定删除当前数据？" OnClick="btnFlowOperateDelete_Click" runat="server">
                                                            </f:Button>
                                                        </Items>
                                                    </f:Toolbar>
                                                </Toolbars>
                                                <Columns>
                                                    <f:RenderField Width="50px" ColumnID="PushGroup" DataField="PushGroup"  FieldType="Int" HeaderText="组别"  
                                                        HeaderTextAlign="Center" TextAlign="Left">
                                                    </f:RenderField>
                                                    <f:RenderField Width="50px" ColumnID="FlowStep" DataField="FlowStep" FieldType="Int" HeaderText="步骤"  HeaderTextAlign="Center" TextAlign="Center">
                                                    </f:RenderField>
                                                    <f:RenderField Width="140px" ColumnID="AuditFlowName" DataField="AuditFlowName"  FieldType="String" HeaderText="步骤名称"  HeaderTextAlign="Center" TextAlign="Left">
                                                    </f:RenderField>
                                                    <f:TemplateField Width="200px" HeaderText="审核岗位" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true"> 
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label8" runat="server"  Text='<%# ConvertWorkPost(Eval("WorkPostIds")) %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </f:TemplateField>
                                                        <f:TemplateField Width="150px" HeaderText="审核部门/中心" HeaderTextAlign="Center" TextAlign="Left" > 
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server"  Text='<%# ConvertDepart(Eval("DepartIds")) %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </f:TemplateField>
                                                        <f:TemplateField Width="150px" HeaderText="审核科室/装置" HeaderTextAlign="Center" TextAlign="Left"> 
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label2" runat="server"  Text='<%# ConvertInstallation(Eval("InstallationIds")) %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </f:TemplateField>                                       
                                                    <f:CheckBoxField Width="80px" RenderAsStaticField="true" TextAlign="Center"  DataField="IsNeed" HeaderText="填写意见" />
                                                    <f:CheckBoxField Width="80px" RenderAsStaticField="true" TextAlign="Center"  DataField="IsFlowEnd" HeaderText="流程结束" />                                         
                                                        <f:LinkButtonField Width="50px" TextAlign="Center" ConfirmText="你确定要删除此条信息？" ConfirmTarget="Top"
                                                        CommandName="Action2" Icon="Delete" />
                                                    </Columns>
                                                        <Listeners>
                                                        <f:Listener Event="dataload" Handler="onGridDataLoad" />
                                                    </Listeners>
                                                </f:Grid>
                                            </Items>             
                                        </f:Tab>  
                                        <f:Tab ID="Tab1" Title="测评分值" BodyPadding="5px" Layout="VBox" IconFont="Bookmark" runat="server">                             
                                            <Items>
                                                <f:Grid ID="Grid2" ShowBorder="true" ShowHeader="false"  EnableCollapse="true" EnableColumnLines="true" EnableColumnMove="true"
                                                    runat="server" BoxFlex="1" DataKeyNames="AppraisalId" AllowCellEditing="true" ClicksToEdit="2"
                                                    DataIDField="AppraisalId" AllowSorting="true" SortField="MenuOperation" SortDirection="ASC" 
                                                    OnSort="Grid2_Sort" AllowPaging="true"  EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid2_RowDoubleClick"
                                                    EnableTextSelection="True" OnRowCommand="Grid2_RowCommand">
                                                    <Toolbars>
                                                        <f:Toolbar ID="Toolbar1" Position="Top"  ToolbarAlign="Right" runat="server">
                                                            <Items>                                                            
                                                                <f:Button ID="btnAppraisalNew" ToolTip="增加" Icon="Add" runat="server"  OnClick="btnAppraisalNew_Click" />
                                                                <f:Button ID="btnAppraisalDelete" ToolTip="删除" Hidden="true" Icon="Delete" ConfirmText="确定删除当前数据？" OnClick="btnAppraisalDelete_Click" runat="server">
                                                                </f:Button>
                                                            </Items>
                                                        </f:Toolbar>
                                                    </Toolbars>
                                                    <Columns>
                                                        <f:RenderField Width="100px" ColumnID="MenuOperation" DataField="MenuOperation"  
                                                            FieldType="Int" HeaderText="动作序号"  HeaderTextAlign="Center" TextAlign="Center">
                                                        </f:RenderField>
                                                        <f:RenderField Width="250px" ColumnID="MenuOperationName" DataField="MenuOperationName" ExpandUnusedSpace="true"
                                                            FieldType="String" HeaderText="动作名称"  HeaderTextAlign="Center" TextAlign="Left">
                                                        </f:RenderField>
                                                        <f:RenderField Width="250px" ColumnID="Score" DataField="Score"  
                                                            FieldType="Double" HeaderText="分值"  HeaderTextAlign="Center" TextAlign="Left">
                                                        </f:RenderField>
                                                        <f:RenderField Width="150px" ColumnID="OutTime" DataField="OutTime"  
                                                            FieldType="Int" HeaderText="超时时长(分钟)"  HeaderTextAlign="Center" TextAlign="Left">
                                                        </f:RenderField>
                                                        <f:LinkButtonField Width="60px" TextAlign="Center" ConfirmText="你确定要删除此条信息？" ConfirmTarget="Top"
                                                            CommandName="Action1" Icon="Delete" />
                                                    </Columns>
                                                </f:Grid>
                                            </Items>             
                                        </f:Tab>                    
                                    </Tabs>
                                </f:TabStrip>                                 
                            </Items>
                        </f:Tab>
                        <f:Tab ID="Tab4" Title="环境设置" BodyPadding="5px" Layout="VBox" IconFont="Bookmark" runat="server">                                                     
                            <Items>
                                <f:Form ID="Form2" ShowBorder="false" ShowHeader="false" AutoScroll="true" Layout="VBox"
                                    BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                    <Rows>
                                        <f:FormRow>
                                            <Items>
                                                <f:CheckBox ID="ckbGPS" runat="server" Label="1、是否启用电子围栏" Text="是" LabelWidth="200">
                                                </f:CheckBox>
                                                 <f:Button ID="btnSetSave" Icon="SystemSave" runat="server" ToolTip="保存"
                                                    OnClick="btnSetSave_Click">
                                                </f:Button>
                                            </Items>
                                        </f:FormRow>
                                    </Rows>
                                </f:Form>
                            </Items>
                        </f:Tab>
                    </Tabs>
                </f:TabStrip>
            </Items>
                     
        </f:Panel>
        <f:Window ID="Window1" Title="流程步骤设置" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Top" EnableResize="true" runat="server" IsModal="true"  OnClose="Window1_Close"
            Width="1024px" Height="460px">  
        </f:Window>
        <f:Window ID="Window2" Title="测评分值" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Top" EnableResize="true" runat="server" IsModal="true"  OnClose="Window2_Close"
            Width="600px" Height="400px">
        </f:Window>
    </form>
    <script type="text/jscript">       
        function reloadGrid() {
            __doPostBack(null, 'reloadGrid');
        }
        function onGridDataLoad(event) {
            this.mergeColumns(['PushGroup'], { depends: true });
        }
    </script>
</body>
</html>
