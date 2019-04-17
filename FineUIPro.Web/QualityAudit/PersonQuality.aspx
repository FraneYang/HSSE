<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonQuality.aspx.cs"
    Inherits="FineUIPro.Web.QualityAudit.PersonQuality" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>人员资质</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
     <style type="text/css">
       .f-grid-row.Red {
             background-color: red;
         }  
        .f-grid-row .f-grid-cell-inner {
            white-space: normal;
            word-break: break-all;
        }        
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="Panel1" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" EnableCollapse="true" runat="server"
                BoxFlex="1" DataKeyNames="ID" EnableColumnLines="true" DataIDField="ID"
                AllowSorting="true" SortField="CompileDate" SortDirection="DESC" OnSort="Grid1_Sort"
                AllowPaging="true" IsDatabasePaging="true" PageSize="15" OnPageIndexChange="Grid1_PageIndexChange"
                EnableRowDoubleClickEvent="true" AllowFilters="true" 
                EnableTextSelection="True" OnRowDoubleClick="Grid1_RowDoubleClick">
                <Toolbars>
                    <f:Toolbar ID="Toolbar3" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:TextBox runat="server" ID="txtName" EmptyText="输入查询条件" AutoPostBack="true"
                                    OnTextChanged="TextBox_TextChanged" Width="200px" LabelAlign="Right">
                            </f:TextBox> 
                            <f:DropDownList ID="drpPersonQuality" runat="server" Label="特岗资质"
                                AutoPostBack="true" OnSelectedIndexChanged="TextBox_TextChanged"  Width="180px" LabelAlign="Right" LabelWidth="75px">
                                <f:ListItem Text="请选择" Value="0" />
                                <f:ListItem Text="是" Value="1" Selected="true"/>
                                <f:ListItem Text="否" Value="2" />
                            </f:DropDownList> 
                            <f:CheckBox runat="server" Label="离岗" ID="isPost" AutoPostBack="true" OnCheckedChanged="isPost_CheckedChanged" LabelWidth="50px" Width="70px">
                            </f:CheckBox>
                            <f:DropDownList ID="drpDataType" runat="server" Label="时间类型"
                                AutoPostBack="true" OnSelectedIndexChanged="TextBox_TextChanged"  Width="180px" LabelAlign="Right" LabelWidth="80px">
                                <f:ListItem Text="请选择" Value="0" />
                                <f:ListItem Text="有效期" Value="1" />
                                <f:ListItem Text="编制日期" Value="2" />
                                <f:ListItem Text="复查时间" Value="3" />
                            </f:DropDownList> 
                            <f:DatePicker ID="txtStartDate" runat="server" Width="130px" LabelAlign="Right"
                                EnableEdit="false" AutoPostBack="true" OnTextChanged="TextBox_TextChanged">
                            </f:DatePicker>
                            <f:DatePicker ID="txtEndDate" runat="server" Width="150px" LabelAlign="Right" LabelWidth="30px"
                                EnableEdit="false"  Label="至" AutoPostBack="true" OnTextChanged="TextBox_TextChanged">
                            </f:DatePicker>
                            <f:ToolbarFill runat="server"></f:ToolbarFill>
                             <f:Button ID="btnOut" OnClick="btnMenuOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                EnableAjax="false" DisableControlBeforePostBack="false">
                            </f:Button>
                             <f:Button ID="btnImport" ToolTip="导入" Icon="ApplicationAdd" Hidden="true" runat="server"
                                    OnClick="btnImport_Click">
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
                    <f:RenderField Width="100px" ColumnID="UnitName" DataField="UnitName" SortField="UnitName"
                        FieldType="String" HeaderText="单位" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="85px" ColumnID="UserCode" DataField="UserCode" SortField="UserCode"
                        FieldType="String" HeaderText="用户编号" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="85px" ColumnID="UserName" DataField="UserName" SortField="UserName"
                        FieldType="String" HeaderText="姓名" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>                  
                    <f:RenderField Width="100px" ColumnID="WorkPostName" DataField="WorkPostName" SortField="WorkPostName"
                        FieldType="String" HeaderText="所属岗位" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="CertificateNo" DataField="CertificateNo" SortField="CertificateNo"
                        FieldType="String" HeaderText="证书编号" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="120px" ColumnID="CertificateName" DataField="CertificateName"
                        SortField="CertificateName" FieldType="String" HeaderText="证书名称" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="240px" ColumnID="SendUnit" DataField="SendUnit" SortField="SendUnit"
                        FieldType="String" HeaderText="发证单位" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="SendDate" DataField="SendDate" SortField="SendDate"
                        FieldType="Date" Renderer="Date" HeaderText="发证时间" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="LimitDate" DataField="LimitDate" SortField="LimitDate"
                        FieldType="Date" Renderer="Date" HeaderText="有效期至" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="CompileDate" DataField="CompileDate" SortField="CompileDate"
                        FieldType="Date" Renderer="Date" HeaderText="编制日期" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField> 
                     <f:WindowField TextAlign="Center" Width="60px" WindowID="WindowAtt" HeaderText="证书"
                        Text="查看" ToolTip="证书查看" DataIFrameUrlFields="PersonQualityId" DataIFrameUrlFormatString="../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/PersonQualityAttachUrl&type=-1"
                        Title="证书" />                      
                </Columns>
                <Listeners>
                    <f:Listener Event="dataload" Handler="onGridDataLoad" />
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
    <f:Window ID="Window1" Title="人员资质" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Top" EnableResize="true" runat="server" IsModal="true" Width="1200px"
        Height="520px" OnClose="Window1_Close">
    </f:Window>
    <f:Window ID="WindowAtt" Title="附件页面" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
            Hidden="true" runat="server" Text="编辑" Icon="TableEdit">
        </f:MenuButton>
         <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
            Hidden="true" runat="server" Text="删除" Icon="Delete"  ConfirmText="删除选中行？">
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
        function onGridDataLoad(event) {
            this.mergeColumns(['UserCode'], { depends: false });
            this.mergeColumns(['UserName'], { depends: false });
            this.mergeColumns(['UnitName'], { depends: false });
        }
    </script>
</body>
</html>
