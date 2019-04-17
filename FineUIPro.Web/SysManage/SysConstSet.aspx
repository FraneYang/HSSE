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
          <Toolbars>
                <f:Toolbar ID="Toolbar3" Position="Top" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:DropDownBox runat="server" ID="drpMenu" Values="henan" EmptyText="请选择菜单" Width="500px"
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
               <f:TabStrip ID="TabStrip1" CssClass="f-tabstrip-theme-simple" Height="500px" ShowBorder="true"
                    TabPosition="Top" MarginBottom="5px" EnableTabCloseMenu="false" runat="server" 
                    ActiveTabIndex="0">              
                    <Tabs>     
                        <f:Tab ID="Tab2" Title="流程设置" BodyPadding="5px" Layout="VBox" IconFont="Bookmark" runat="server">                             
                            <Items>
                                <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false"  EnableCollapse="true" EnableColumnLines="true" EnableColumnMove="true"
                                    runat="server" BoxFlex="1" DataKeyNames="FlowOperateId" AllowCellEditing="true" ClicksToEdit="2"
                                    DataIDField="FlowOperateId" AllowSorting="true" SortField="FlowStep" SortDirection="ASC" 
                                    OnSort="Grid1_Sort" AllowPaging="true"  EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick"
                                    EnableTextSelection="True">
                                    <Toolbars>
                                        <f:Toolbar ID="Toolbar4" Position="Top"  ToolbarAlign="Right" runat="server">
                                            <Items>                                                            
                                                <f:Button ID="btnFlowOperateNew" ToolTip="增加" Icon="Add" runat="server"  OnClick="btnFlowOperateNew_Click" />
                                                <f:Button ID="btnFlowOperateDelete" ToolTip="删除" Icon="Delete" ConfirmText="确定删除当前数据？" OnClick="btnFlowOperateDelete_Click" runat="server">
                                                </f:Button>
                                            </Items>
                                        </f:Toolbar>
                                    </Toolbars>
                                    <Columns>
                                        <f:RenderField Width="100px" ColumnID="FlowStep" DataField="FlowStep" FieldType="Int" HeaderText="审核步骤"  HeaderTextAlign="Center" TextAlign="Center">
                                        </f:RenderField>
                                        <f:RenderField Width="250px" ColumnID="AuditFlowName" DataField="AuditFlowName"  FieldType="String" HeaderText="步骤名称"  HeaderTextAlign="Center" TextAlign="Left">
                                        </f:RenderField>
                                        <f:TemplateField Width="300px" HeaderText="审核岗位" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true"> 
                                            <ItemTemplate>
                                                <asp:Label ID="Label8" runat="server"  Text='<%# ConvertWorkPost(Eval("WorkPostIds")) %>' ></asp:Label>
                                            </ItemTemplate>
                                        </f:TemplateField>
                                        <f:CheckBoxField Width="120px" RenderAsStaticField="true" TextAlign="Center"  DataField="IsNeed" HeaderText="填写意见" />
                                        <f:CheckBoxField Width="100px" RenderAsStaticField="true" TextAlign="Center"  DataField="IsFlowEnd" HeaderText="流程结束" />
                                         <f:RenderField Width="90px" ColumnID="PushGroup" DataField="PushGroup"  FieldType="Int" HeaderText="组别"  
                                            HeaderTextAlign="Center" TextAlign="Left">
                                        </f:RenderField>
                                    </Columns>
                                </f:Grid>
                            </Items>             
                        </f:Tab>  
                        <f:Tab ID="Tab1" Title="测评分值" BodyPadding="5px" Layout="VBox" IconFont="Bookmark" runat="server">                             
                            <Items>
                                <f:Grid ID="Grid2" ShowBorder="true" ShowHeader="false"  EnableCollapse="true" EnableColumnLines="true" EnableColumnMove="true"
                                    runat="server" BoxFlex="1" DataKeyNames="AppraisalId" AllowCellEditing="true" ClicksToEdit="2"
                                    DataIDField="AppraisalId" AllowSorting="true" SortField="SortIndex" SortDirection="ASC" 
                                    OnSort="Grid2_Sort" AllowPaging="true"  EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid2_RowDoubleClick"
                                    EnableTextSelection="True">
                                    <Toolbars>
                                        <f:Toolbar ID="Toolbar1" Position="Top"  ToolbarAlign="Right" runat="server">
                                            <Items>                                                            
                                                <f:Button ID="btnAppraisalNew" ToolTip="增加" Icon="Add" runat="server"  OnClick="btnAppraisalNew_Click" />
                                                <f:Button ID="btnAppraisalDelete" ToolTip="删除" Icon="Delete" ConfirmText="确定删除当前数据？" OnClick="btnAppraisalDelete_Click" runat="server">
                                                </f:Button>
                                            </Items>
                                        </f:Toolbar>
                                    </Toolbars>
                                    <Columns>
                                        <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="120px"
                                             HeaderTextAlign="Center" TextAlign="Center"/>
                                        <f:RenderField Width="250px" ColumnID="MenuOperation" DataField="MenuOperation" ExpandUnusedSpace="true"
                                            FieldType="String" HeaderText="页面操作"  HeaderTextAlign="Center" TextAlign="Center">
                                        </f:RenderField>
                                        <f:RenderField Width="300px" ColumnID="Score" DataField="Score"  
                                            FieldType="Int" HeaderText="分值"  HeaderTextAlign="Center" TextAlign="Left">
                                        </f:RenderField>
                                    </Columns>
                                </f:Grid>
                            </Items>             
                        </f:Tab>                    
                    </Tabs>
                </f:TabStrip>
              </Items>          
        </f:Panel>
        <f:Window ID="Window1" Title="流程步骤设置" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Top" EnableResize="true" runat="server" IsModal="true"  OnClose="Window1_Close"
            Width="800px" Height="520px">  
        </f:Window>
        <f:Window ID="Window2" Title="测评分值" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Top" EnableResize="true" runat="server" IsModal="true"  OnClose="Window2_Close"
            Width="500px" Height="320px">
        </f:Window>
    </form>
</body>
</html>
