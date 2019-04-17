<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SpecialScheme.aspx.cs"
    Inherits="FineUIPro.Web.Technique.SpecialScheme" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>施工方案</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="专项方案" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="SpecialSchemeId" AllowCellEditing="true"
                EnableColumnLines="true" ClicksToEdit="2" DataIDField="SpecialSchemeId" AllowSorting="true"
                SortField="SpecialSchemeCode" SortDirection="ASC" OnSort="Grid1_Sort" AllowPaging="true"
                IsDatabasePaging="true" AllowColumnLocking="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" AllowFilters="true"
                OnFilterChange="Grid1_FilterChange" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar3" Position="Top" runat="server">
                        <Items>
                            <f:TextBox ID="UnitName" runat="server" Label="单位" EmptyText="输入查询单位" AutoPostBack="true"
                                OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="70px">
                            </f:TextBox>
                            <f:TextBox ID="SpecialSchemeName" runat="server" Label="名称" EmptyText="输入查询名称" AutoPostBack="true"
                                OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="70px">
                            </f:TextBox>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
                            <f:Button ID="btnNew" ToolTip="新增" Icon="Add" EnablePostBack="false" runat="server"
                                Hidden="true">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                    <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                        TextAlign="Center" Locked="true" />
                    <f:TemplateField Width="100px" HeaderText="方案编号" HeaderTextAlign="Center" TextAlign="Left"
                        Locked="true" SortField="SpecialSchemeCode">
                        <ItemTemplate>
                            <asp:Label ID="lblSpecialSchemeCode" runat="server" Text='<%# Bind("SpecialSchemeCode") %>'
                                ToolTip='<%#Bind("SpecialSchemeCode") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:TemplateField Width="200px" HeaderText="方案名称" HeaderTextAlign="Center" TextAlign="Left"
                        Locked="true" SortField="SpecialSchemeName">
                        <ItemTemplate>
                            <asp:Label ID="lblSpecialSchemeName" runat="server" Text='<%# Bind("SpecialSchemeName") %>'
                                ToolTip='<%#Bind("SpecialSchemeName") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:TemplateField Width="140px" HeaderText="类型" HeaderTextAlign="Center" TextAlign="Left"
                        SortField="SpecialSchemeTypeName">
                        <ItemTemplate>
                            <asp:Label ID="lblSpecialSchemeTypeName" runat="server" Text='<%# Bind("SpecialSchemeTypeName") %>'
                                ToolTip='<%#Bind("SpecialSchemeTypeName") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:TemplateField Width="190px" HeaderText="摘要" HeaderTextAlign="Center" TextAlign="Left"
                        SortField="Summary">
                        <ItemTemplate>
                            <asp:Label ID="lblSummary" runat="server" Text='<%# Bind("Summary") %>' ToolTip='<%#Bind("Summary") %>'>
                            </asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:TemplateField Width="250px" HeaderText="单位" HeaderTextAlign="Center" TextAlign="Left"
                        SortField="UnitName">
                        <ItemTemplate>
                            <asp:Label ID="lblUnitName" runat="server" Text='<%# Bind("UnitName") %>' ToolTip='<%#Bind("UnitName") %>'>
                            </asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:WindowField TextAlign="Left" Width="70px" WindowID="WindowAtt" Text="附件" ToolTip="上传查看"
                        DataIFrameUrlFields="SpecialSchemeId" DataIFrameUrlFormatString="../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/SpecialScheme&menuId=3E2F2FFD-ED2E-4914-8370-D97A68398814" />
                    <f:RenderField Width="100px" ColumnID="CompileMan" DataField="CompileMan" SortField="CompileMan"
                        FieldType="String" HeaderText="整理人" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="CompileDate" DataField="CompileDate" SortField="CompileDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="整理时间"
                        HeaderTextAlign="Center" TextAlign="Center">
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
    <f:Window ID="Window1" Title="编辑专项方案" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="500px" Height="380px">
    </f:Window>
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server" IsModal="true" Width="670px"
        Height="460px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
            Hidden="true" runat="server" Text="编辑" Icon="Pencil">
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
