<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestPlanStatistics.aspx.cs" Inherits="FineUIPro.Web.Training.TestPlanStatistics" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>考试计划</title>
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
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="考试计划" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="TestPlanId" AllowCellEditing="true" EnableColumnLines="true"
                ClicksToEdit="2" DataIDField="TestPlanId" AllowSorting="true" SortField="PlanCode"
                SortDirection="DESC" OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true"
                PageSize="15" OnPageIndexChange="Grid1_PageIndexChange" EnableRowDoubleClickEvent="true"
                OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" Position="Top" runat="server">
                        <Items>
                             <f:RadioButtonList ID="ckStates" runat="server" AutoPostBack="true" Label="状态" LabelAlign="Right"
                                AutoColumnWidth="true" OnSelectedIndexChanged="TextBox_TextChanged">                               
                                <f:RadioItem Text="全部" Value="-2" Selected="true"/>
                                <f:RadioItem Text="未发布" Value="0" />
                                <f:RadioItem Text="已发布" Value="1" />
                                <f:RadioItem Text="已作废" Value="-1" />
                            </f:RadioButtonList>                                                  
                            <f:TextBox ID="txtName" runat="server" Label="查询" EmptyText="输入查询条件" AutoPostBack="true"
                                OnTextChanged="TextBox_TextChanged" Width="350px" LabelWidth="80px" LabelAlign="Right">
                            </f:TextBox>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                             <f:Button ID="btnOut" OnClick="btnMenuOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                EnableAjax="false" DisableControlBeforePostBack="false">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>                   
                    <f:RenderField Width="120px" ColumnID="PlanCode" DataField="PlanCode" SortField="PlanCode"
                        HeaderText="编号" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="130px" ColumnID="PlanName" DataField="PlanName" FieldType="String"
                        HeaderText="计划名称" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                    </f:RenderField>                   
                    <f:RenderField Width="160px" ColumnID="TestStartTime" DataField="TestStartTime" 
                          HeaderText="扫码开始时间" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                     <f:RenderField Width="160px" ColumnID="TestEndTime" DataField="TestEndTime" 
                          HeaderText="扫码结束时间" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="PushCount" DataField="PushCount" FieldType="Int"
                          HeaderText="推送人数" HeaderTextAlign="Center" TextAlign="Right">
                    </f:RenderField>
                     <f:RenderField Width="120px" ColumnID="TestCount" DataField="TestCount" FieldType="Int"
                          HeaderText="实际考试人数" HeaderTextAlign="Center" TextAlign="Right">
                    </f:RenderField>
                     <f:RenderField Width="120px" ColumnID="PassTestCount" DataField="PassTestCount" FieldType="Int"
                          HeaderText="80分以上人数" HeaderTextAlign="Center" TextAlign="Right">
                    </f:RenderField>
                </Columns>
                <Listeners>
                    <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
                </Listeners>
                <PageItems>
                    <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                    </f:ToolbarSeparator>
                    <f:ToolbarText ID="ToolbarText1" runat="server" Text="每页记录数：">
                    </f:ToolbarText>
                    <f:DropDownList runat="server" ID="ddlPageSize" Width="80px" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                        <f:ListItem Text="10" Value="10" />
                        <f:ListItem Text="15" Value="15" />
                        <f:ListItem Text="20" Value="20" />
                        <f:ListItem Text="25" Value="25" />
                        <f:ListItem Text="所有行" Value="10000" />
                    </f:DropDownList>
                </PageItems>
            </f:Grid>
        </Items>
    </f:Panel>
    <f:Window ID="Window1" Title="考试人员情况" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Top" EnableResize="true" runat="server" IsModal="true"
        Width="900px" Height="540px">
    </f:Window>

    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuView" OnClick="btnMenuView_Click" EnablePostBack="true"
            runat="server" Text="查看"  Icon="TableGo">
        </f:MenuButton>  
    </f:Menu>
    </form>
    <script type="text/javascript">
        var menuID = '<%= Menu1.ClientID %>';
        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
            F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }
        function reloadGrid() {
            __doPostBack(null, 'reloadGrid');
        }
    </script>
</body>
</html>
