<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="FineUIPro.Web.SysManage.UserList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>用户信息</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
     <style type="text/css">
        .f-grid-row .f-grid-cell-inner
        {
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
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="用户信息" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="UserId" AllowCellEditing="true" EnableColumnLines="true"
                ClicksToEdit="2" DataIDField="UserId" AllowSorting="true" SortField="UnitCode,SortIndex"
                SortDirection="ASC" OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true"
                PageSize="15" OnPageIndexChange="Grid1_PageIndexChange" EnableRowDoubleClickEvent="true"
                OnRowDoubleClick="Grid1_RowDoubleClick" Width="980px" EnableTextSelection="True">
                <Toolbars>
                   
                    <f:Toolbar ID="Toolbar1" Position="Top" runat="server">
                        <Items>                          
                             <f:TextBox runat="server" ID="txtName" EmptyText="输入查询条件" AutoPostBack="true"
                                OnTextChanged="TextBox_TextChanged" Width="250px">
                            </f:TextBox> 
                            <f:RadioButtonList ID="rbUsrType" runat="server" AutoPostBack="true" Label="用户类型" LabelAlign="Right"
                                AutoColumnWidth="true" OnSelectedIndexChanged="rbUsrType_SelectedIndexChanged">                               
                                <f:RadioItem Text="本单位" Value="0" Selected="true"/>
                                <f:RadioItem Text="外委" Value="1" />
                            </f:RadioButtonList>
                            <f:DropDownList ID="drpIsPost" runat="server" Label="在岗" EnableEdit="false" LabelWidth="60px"
                                Required="true" ShowRedStar="true" AutoPostBack="true" OnSelectedIndexChanged="rbUsrType_SelectedIndexChanged">
                            </f:DropDownList>
                            <f:ToolbarFill runat="server">
                            </f:ToolbarFill>
                            <f:Button ID="btnNew" ToolTip="新增" Icon="Add" EnablePostBack="false" runat="server"
                                Hidden="true">
                            </f:Button>
                            <f:Button ID="btnImport" ToolTip="导入" Icon="ApplicationAdd" Hidden="true" runat="server"
                                    OnClick="btnImport_Click">
                            </f:Button>
                            <f:Button ID="btnOut" OnClick="btnMenuOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                EnableAjax="false" DisableControlBeforePostBack="false">
                            </f:Button>
                            <f:Button ID="btnRefresh1" ToolTip="按列表名称刷新" Icon="ArrowRefresh" runat="server" OnClick="Refresh1_Click"
                                Hidden="true">
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
                    <f:RenderField Width="85px" ColumnID="UserCode" DataField="UserCode" SortField="UserCode"
                        FieldType="String" HeaderText="用户编号" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="UserName" DataField="UserName" SortField="UserName"
                        FieldType="String" HeaderText="姓名" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="180px" ColumnID="UnitName" DataField="UnitName" SortField="UnitName"
                        FieldType="String" HeaderText="单位" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="Account" DataField="Account" SortField="Account"
                        FieldType="String" HeaderText="登录账号" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                   <%-- <f:RenderField Width="160px" ColumnID="IdentityCard" DataField="IdentityCard" SortField="IdentityCard"
                        FieldType="String" HeaderText="身份证号码" HeaderTextAlign="Center" TextAlign="Right" >
                    </f:RenderField>--%>
                     <f:RenderField Width="120px" ColumnID="DepartName" DataField="DepartName" SortField="DepartName"
                        FieldType="String" HeaderText="部门(中心)" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                     <f:RenderField Width="150px" ColumnID="InstallationName" DataField="InstallationName" SortField="InstallationName"
                        FieldType="String" HeaderText="科室/装置" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                     <f:RenderField Width="140px" ColumnID="WorkPostName" DataField="WorkPostName" SortField="WorkPostName"
                        FieldType="String" HeaderText="岗位" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="RoleName" DataField="RoleName" SortField="RoleName"
                        FieldType="String" HeaderText="角色" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="110px" ColumnID="Telephone" DataField="Telephone" SortField="Telephone"
                        FieldType="String" HeaderText="手机" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:CheckBoxField Width="55px" SortField="IsPost" RenderAsStaticField="true" DataField="IsPost"
                        HeaderText="在岗" HeaderTextAlign="Center" TextAlign="Center">
                    </f:CheckBoxField> 
                    <f:CheckBoxField Width="55px" SortField="IsEmergency" RenderAsStaticField="true" DataField="IsEmergency"
                        HeaderText="应急" HeaderTextAlign="Center" TextAlign="Center">
                    </f:CheckBoxField>   
                    <f:RenderField Width="100px" ColumnID="EntryTime" DataField="EntryTime" FieldType="Date"
                        Renderer="Date" HeaderText="登记时间" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>      
                     <f:CheckBoxField Width="55px" SortField="IsTemp" RenderAsStaticField="true" DataField="IsTemp"
                        HeaderText="临时" HeaderTextAlign="Center" TextAlign="Center">
                    </f:CheckBoxField>       
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
    <f:Window ID="Window1" Title="用户信息" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Top" EnableResize="true" runat="server" IsModal="true" Width="800px"
        Height="520px">
    </f:Window>
    <f:Window ID="Window2" Title="导入人员信息" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window2_Close" IsModal="false"
        CloseAction="HidePostBack" Width="1000px" Height="560px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
            Hidden="true" runat="server" Text="编辑" Icon="TableEdit">
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
