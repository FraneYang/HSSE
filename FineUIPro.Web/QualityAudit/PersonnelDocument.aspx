<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonnelDocument.aspx.cs"
    Inherits="FineUIPro.Web.QualityAudit.PersonnelDocument" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>人员建档</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="Panel1" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" EnableCollapse="true" runat="server"
                BoxFlex="1" DataKeyNames="UserId" EnableColumnLines="true" DataIDField="UserId"
                AllowSorting="true" SortField="UserCode" SortDirection="DESC" OnSort="Grid1_Sort"
                AllowPaging="true" IsDatabasePaging="true" PageSize="15" OnPageIndexChange="Grid1_PageIndexChange"
                AllowFilters="true" EnableTextSelection="True" EnableRowDoubleClickEvent="true"
                OnRowDoubleClick="Grid1_RowDoubleClick">
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:RadioButtonList ID="rblIsPost" runat="server" Label="是否在岗" AutoPostBack="true"
                                OnSelectedIndexChanged="TextBox_TextChanged" LabelAlign="Right">
                                <f:RadioItem Text="是" Value="1" Selected="true" />
                                <f:RadioItem Text="否" Value="0" />
                            </f:RadioButtonList>
                            <f:TextBox ID="txtUserName" runat="server" Label="姓名" LabelAlign="Right" AutoPostBack="true"
                                OnTextChanged="TextBox_TextChanged">
                            </f:TextBox>
                            <f:TextBox ID="txtUnitName" runat="server" Label="单位" LabelAlign="Right" AutoPostBack="true"
                                OnTextChanged="TextBox_TextChanged">
                            </f:TextBox>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                    <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                        TextAlign="Center" />
                    <f:RenderField Width="90px" ColumnID="UserCode" DataField="UserCode" SortField="UserCode"
                        FieldType="String" HeaderText="编号" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="UserName" DataField="UserName" SortField="UserName"
                        FieldType="String" HeaderText="姓名" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="200px" ColumnID="UnitName" DataField="UnitName" SortField="UnitName"
                        FieldType="String" HeaderText="单位" HeaderTextAlign="Center" TextAlign="Left"
                        ExpandUnusedSpace="true">
                    </f:RenderField>
                    <f:RenderField Width="160px" ColumnID="IdentityCard" DataField="IdentityCard" SortField="IdentityCard"
                        FieldType="String" HeaderText="身份证号码" HeaderTextAlign="Center" TextAlign="Right">
                    </f:RenderField>
                    <f:RenderField Width="110px" ColumnID="DepartName" DataField="DepartName" SortField="DepartName"
                        FieldType="String" HeaderText="部门(中心)" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="140px" ColumnID="InstallationName" DataField="InstallationName"
                        SortField="InstallationName" FieldType="String" HeaderText="科室/装置" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="120px" ColumnID="WorkPostName" DataField="WorkPostName" SortField="WorkPostName"
                        FieldType="String" HeaderText="岗位" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="110px" ColumnID="Telephone" DataField="Telephone" SortField="Telephone"
                        FieldType="String" HeaderText="联系电话" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                     <f:RenderField Width="90px" ColumnID="EntryTime" DataField="EntryTime" FieldType="Date"
                        Renderer="Date" HeaderText="登记时间" HeaderTextAlign="Center" TextAlign="Left">
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
                    </f:DropDownList>
                </PageItems>
            </f:Grid>
        </Items>
    </f:Panel>
    <f:Window ID="Window1" Title="人员建档" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Top" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="300px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
            Hidden="true" runat="server" Text="编辑" Icon="TableEdit">
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
