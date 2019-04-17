<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestPlanStatisticsItem.aspx.cs" Inherits="FineUIPro.Web.Training.TestPlanStatisticsItem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>考试计划详细</title>
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
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="人员情况" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="UserId" AllowCellEditing="true" EnableColumnLines="true"
                ClicksToEdit="2" DataIDField="UserId" AllowSorting="true" SortField="DepartName,InstallationName,UserName"
                SortDirection="ASC" OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true"
                PageSize="15" OnPageIndexChange="Grid1_PageIndexChange" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" Position="Top" runat="server">
                        <Items>
                             <f:RadioButtonList ID="ckStates" runat="server" AutoPostBack="true" Label="状态" LabelAlign="Right"
                                AutoColumnWidth="true" OnSelectedIndexChanged="TextBox_TextChanged">                               
                                <f:RadioItem Text="推送人员" Value="0" Selected="true"/>
                                <f:RadioItem Text="考试人员" Value="1" />
                                <f:RadioItem Text="未考试人员" Value="2" />
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
                    <f:RenderField Width="120px" ColumnID="UserCode" DataField="UserCode" SortField="UserCode"
                        HeaderText="编号" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="130px" ColumnID="UserName" DataField="UserName" FieldType="String"
                        HeaderText="姓名" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                     <f:RenderField Width="130px" ColumnID="DepartName" DataField="DepartName" FieldType="String"
                        HeaderText="部门" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>   
                    <f:RenderField Width="130px" ColumnID="InstallationName" DataField="InstallationName" FieldType="String"
                        HeaderText="装置" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                    </f:RenderField>   
                    <f:RenderField Width="150px" ColumnID="WorkPostName" DataField="WorkPostName" FieldType="String"
                        HeaderText="岗位" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>   
                     <f:RenderField Width="140px" ColumnID="Telephone" DataField="Telephone" FieldType="String"
                        HeaderText="联系方式" HeaderTextAlign="Center" TextAlign="Left">
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
    </form>
    <script type="text/javascript">        
        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
            //F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }
        function reloadGrid() {
            __doPostBack(null, 'reloadGrid');
        }
    </script>
</body>
</html>
