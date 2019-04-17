<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoutingInspection.aspx.cs" Inherits="FineUIPro.Web.Hazard.RoutingInspection" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>风险巡检记录</title>
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
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="风险巡检记录" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="RoutingInspectionId" AllowCellEditing="true" EnableColumnLines="true"
                ClicksToEdit="2" DataIDField="RoutingInspectionId" AllowSorting="true" SortField="PatrolTime"
                SortDirection="DESC" OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true" EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick"
                PageSize="15" OnPageIndexChange="Grid1_PageIndexChange" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" Position="Top" runat="server">
                         <Items>                                                      
                            <f:TextBox runat="server"  ID="txtName" EmptyText="输入查询条件" AutoPostBack="true"
                                OnTextChanged="TextBox_TextChanged" Width="250px" >
                            </f:TextBox>      
                             <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
                              <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                EnableAjax="false" DisableControlBeforePostBack="false">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>                   
                     <f:TemplateField ColumnID="tfNumber" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="labNumber" runat="server" Text=' <%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1%>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>                     
                     <f:RenderField Width="150px" ColumnID="PatrolTime" DataField="PatrolTime" FieldType="String"
                        HeaderText="巡检时间" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                     <f:RenderField Width="75px" ColumnID="PatrolManName" DataField="PatrolManName" FieldType="String"
                        HeaderText="巡检人" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField> 
                    <f:RenderField Width="120px" ColumnID="InstallationName" DataField="InstallationName" FieldType="String"
                        HeaderText="所属单位" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="280px" ColumnID="TaskActivity" DataField="TaskActivity" FieldType="String"
                        HeaderText="设备设施</br>作业活动名称" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>  
                    <f:RenderField Width="280px" ColumnID="HazardDescription" DataField="HazardDescription" FieldType="String"
                        HeaderText="风险点" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="65px" ColumnID="OldRiskLevel" DataField="OldRiskLevel" FieldType="String"
                        HeaderText="原等级" HeaderTextAlign="Center" TextAlign="Left" >
                    </f:RenderField>
                     <f:RenderField Width="65px" ColumnID="NowRiskLevel" DataField="NowRiskLevel" FieldType="String"
                        HeaderText="现等级" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="80px" ColumnID="PatrolResultName" DataField="PatrolResultName" FieldType="String"
                        HeaderText="巡检结果" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <%--  <f:RenderField Width="75px" ColumnID="StatesName" DataField="StatesName" FieldType="String"
                        HeaderText="单据状态" HeaderTextAlign="Center" TextAlign="Left" Hidden="true">
                    </f:RenderField>--%>
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
    <f:Window ID="Window1" Title="风险巡检记录" Hidden="true" EnableIFrame="true" EnableMaximize="true" 
        Target="Top" EnableResize="true" runat="server" IsModal="true"  Height="400px" Width="1000px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuView" OnClick="btnMenuView_Click" EnablePostBack="true"
           runat="server" Text="查看" Icon="TableGo">
        </f:MenuButton>
         <f:MenuButton ID="btnFile" OnClick="btnMenuFile_Click" EnablePostBack="true"
            Hidden="true" runat="server" Text="归档" Icon="FolderPage">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
            Hidden="true" ConfirmText="删除选中行？" ConfirmTarget="Top" runat="server" Text="删除"
            Icon="Delete">
        </f:MenuButton>
    </f:Menu>
    </form>
    <script type="text/jscript">
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
