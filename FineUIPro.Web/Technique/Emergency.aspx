<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Emergency.aspx.cs" Inherits="FineUIPro.Web.Technique.Emergency" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>应急救援库</title>
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
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="应急救援库" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="EmergencyId" AllowCellEditing="true"
                ClicksToEdit="2" DataIDField="EmergencyId" AllowSorting="true" SortField="EmergencyCode"
                SortDirection="ASC" OnSort="Grid1_Sort"  AllowPaging="true"
                IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick"  EnableTextSelection="True" EnableColumnLines="true">
                <Toolbars>
                    <f:Toolbar ID="Toolbar3" Position="Top" runat="server">
                        <Items>
                            <f:TextBox ID="EmergencyCode" runat="server" Label="编号" EmptyText="输入查询条件"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="210px" LabelWidth="60px"
                                LabelAlign="Right">
                            </f:TextBox>
                            <f:TextBox ID="EmergencyName" runat="server" Label="名称" EmptyText="输入查询条件"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="210px" LabelWidth="60px"
                                LabelAlign="Right">
                            </f:TextBox>
                            <f:TextBox ID="EmergencyTypeName" runat="server" Label="类型" EmptyText="输入查询条件"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="210px" LabelWidth="60px"
                                LabelAlign="Right">
                            </f:TextBox>
                            <f:ToolbarFill runat="server"></f:ToolbarFill>
                             <f:Button ID="btnNew" ToolTip="新增" Icon="Add" EnablePostBack="false" runat="server"
                                Hidden="true">
                            </f:Button>                                                      
                           <f:Button ID="btnOut" OnClick="btnMenuOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
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
                     <f:RenderField ColumnID="EmergencyCode" DataField="EmergencyCode" SortField="EmergencyCode" Width="120px"
                        FieldType="String" HeaderText="编号" HeaderTextAlign="Center" TextAlign="left">
                    </f:RenderField>
                    <f:RenderField ColumnID="EmergencyName" DataField="EmergencyName" SortField="EmergencyName" Width="220px"
                        FieldType="String" HeaderText="名称" HeaderTextAlign="Center" TextAlign="left">
                    </f:RenderField>
                     <f:RenderField ColumnID="EmergencyTypeName" DataField="EmergencyTypeName" SortField="EmergencyTypeName" Width="120px"
                        FieldType="String" HeaderText="类型" HeaderTextAlign="Center" TextAlign="left">
                    </f:RenderField>
                    <f:RenderField ColumnID="Summary" DataField="Summary" SortField="Summary" Width="150px"
                        FieldType="String" HeaderText="摘要" HeaderTextAlign="Center" TextAlign="left">
                    </f:RenderField>
                      <f:RenderField ColumnID="Remark" DataField="Remark" SortField="Remark" Width="150px"
                        FieldType="String" HeaderText="备注" HeaderTextAlign="Center" TextAlign="left" ExpandUnusedSpace="true">
                    </f:RenderField>
                    <f:WindowField TextAlign="Left" Width="80px" WindowID="WindowAtt" Text="附件" 
                        ToolTip="附件上传查看" DataIFrameUrlFields="EmergencyId" DataIFrameUrlFormatString="../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/Emergency&menuId=575C5154-A135-4737-8682-A129EA717660"/>
                    <f:RenderField Width="100px" ColumnID="CompileMan" DataField="CompileMan" SortField="CompileMan"
                        FieldType="String" HeaderText="整理人" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="CompileDate" DataField="CompileDate" SortField="CompileDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="整理日期"
                        HeaderTextAlign="Center" TextAlign="Left">
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
                        <f:ListItem Text="所有行" Value="100000" />
                    </f:DropDownList>
                </PageItems>
            </f:Grid>
        </Items>
    </f:Panel>
    <f:Window ID="Window1" Title="编辑应急救援库" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="760px" Height="450px">
    </f:Window> 
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server" IsModal="true" Width="670px"
        Height="460px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
            runat="server" Text="编辑" Icon="TableEdit">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
            ConfirmText="删除选中行？" ConfirmTarget="Top" runat="server" Text="删除" Icon="Delete">
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
