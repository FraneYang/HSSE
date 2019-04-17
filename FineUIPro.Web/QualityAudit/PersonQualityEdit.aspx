<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonQualityEdit.aspx.cs" Inherits="FineUIPro.Web.QualityAudit.PersonQualityEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑人员资质</title>
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
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true" Layout="VBox" 
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right" EnableTableStyle="true">
        <Rows>           
            <f:FormRow runat="server" ID="fRow0">
                <Items>                   
                     <f:TextBox Label="姓名" runat="server" ID="txtUserName" Readonly="true" LabelWidth="80px">
                     </f:TextBox>
                     <f:TextBox Label="编号" runat="server" ID="txtUserCode" Readonly="true" LabelWidth="80px">
                     </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox Label="单位" runat="server" ID="txtUnitName" Readonly="true" LabelWidth="80px">
                    </f:TextBox>
                    <f:TextBox Label="岗位" runat="server" ID="txtWorkPostName" Readonly="true" LabelWidth="80px">
                    </f:TextBox>                         
                </Items>
            </f:FormRow>    
            <f:FormRow>
                <Items>
                      <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="人员资质" EnableCollapse="true"
                        runat="server" BoxFlex="1" DataKeyNames="PersonQualityId" AllowCellEditing="true" EnableColumnLines="true"
                        ClicksToEdit="2" DataIDField="PersonQualityId" AllowSorting="true" SortField="CompileDate"
                        SortDirection="DESC" OnSort="Grid1_Sort" AllowPaging="false" IsDatabasePaging="true"
                        PageSize="1000" EnableRowDoubleClickEvent="true" Height="390px"
                        OnRowDoubleClick="Grid1_RowDoubleClick" Width="900px" EnableTextSelection="True">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Right">
                                <Items>   
                                    <f:TextBox runat="server" ID="txtName" Label="查询" EmptyText="输入查询条件" AutoPostBack="true"
                                        OnTextChanged="TextBox_TextChanged" Width="250px" LabelAlign="Right">
                                    </f:TextBox>      
                                    <f:ToolbarFill ID="ToolbarFill1" runat="server">
                                    </f:ToolbarFill>
                                    <f:Button ID="btnAdd" ToolTip="新增" Icon="Add" ValidateForms="SimpleForm1" runat="server" OnClick="btnAdd_Click" Hidden="true">
                                    </f:Button>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Columns>      
                        <f:RenderField Width="85px" ColumnID="CertificateNo" DataField="CertificateNo" FieldType="String"
                            HeaderText="证书编号" HeaderTextAlign="Center" TextAlign="Left">
                        </f:RenderField>
                        <f:RenderField Width="130px" ColumnID="CertificateName" DataField="CertificateName" FieldType="String"
                            HeaderText="证书名称" HeaderTextAlign="Center" TextAlign="Left" >
                        </f:RenderField>
                        <f:RenderField Width="220px" ColumnID="SendUnitName" DataField="SendUnitName" FieldType="String"
                            HeaderText="发证单位" HeaderTextAlign="Center" TextAlign="Left" >
                        </f:RenderField>
                        <f:RenderField Width="90px" ColumnID="SendDate" DataField="SendDate" SortField="SendDate"
                            FieldType="Date" Renderer="Date" HeaderText="发证时间" HeaderTextAlign="Center" TextAlign="Left">
                        </f:RenderField>
                         <f:RenderField Width="90px" ColumnID="LimitDate" DataField="LimitDate" SortField="LimitDate"
                            FieldType="Date" Renderer="Date" HeaderText="有效期至" HeaderTextAlign="Center" TextAlign="Left">
                        </f:RenderField>
                        <f:RenderField Width="170px" ColumnID="ProspectiveNames" DataField="ProspectiveNames" FieldType="String"
                            HeaderText="准操项目" HeaderTextAlign="Center" TextAlign="Left">
                        </f:RenderField>
                        <f:RenderField Width="90px" ColumnID="LateCheckDate" DataField="LateCheckDate" SortField="LateCheckDate"
                            FieldType="Date" Renderer="Date" HeaderText="复查日期" HeaderTextAlign="Center" TextAlign="Left">
                        </f:RenderField>
                        <f:RenderField Width="90px" ColumnID="AuditDate" DataField="AuditDate" SortField="AuditDate"
                            FieldType="Date" Renderer="Date" HeaderText="审核日期" HeaderTextAlign="Center" TextAlign="Left">
                        </f:RenderField>
                        <f:RenderField Width="95px" ColumnID="CompileDate" DataField="CompileDate" SortField="CompileDate"
                            FieldType="Date" Renderer="Date" HeaderText="编制日期" HeaderTextAlign="Center" TextAlign="Left">
                        </f:RenderField>
                       <f:RenderField Width="80px" ColumnID="Remark" DataField="Remark" FieldType="String"
                            HeaderText="备注" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                        </f:RenderField>
                    </Columns> 
                    <Listeners>
                        <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
                    </Listeners>
                  </f:Grid>
                </Items>
            </f:FormRow>          
        </Rows>       
    </f:Form>
    <f:Window ID="Window1" Title="信息维护页面" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Top" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        CloseAction="HidePostBack" Width="800px" Height="420px">
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