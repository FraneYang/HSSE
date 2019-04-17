<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HazardTypeAnalyse.aspx.cs" Inherits="FineUIPro.Web.SafeAnalysis.HazardTypeAnalyse" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>安全隐患分类统计台账</title>
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
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="安全隐患分类统计台账" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="HiddenHazardId" AllowCellEditing="true" EnableColumnLines="true"
                ClicksToEdit="2" DataIDField="HiddenHazardId" AllowSorting="true" SortField="HiddenHazardCode"
                SortDirection="ASC" OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true"
                PageSize="50" OnPageIndexChange="Grid1_PageIndexChange" Width="980px" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" ToolbarAlign="Left" runat="server">
                        <Items>
                            <f:RadioButtonList ID="ckType" runat="server" AutoPostBack="true" ColumnNumber="10"
                                AutoColumnWidth="true" OnSelectedIndexChanged="ckType_SelectedIndexChanged">                               
                            </f:RadioButtonList>
                        </Items>
                    </f:Toolbar>
                    <f:Toolbar ID="Toolbar1" Position="Top" runat="server">
                         <Items>
                             <f:TextBox runat="server" Label="查询" ID="txtName" EmptyText="输入查询条件" AutoPostBack="true"
                                OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="80px" LabelAlign="Right">
                            </f:TextBox>         
                             <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>  
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
                    <f:RenderField Width="150px" ColumnID="HiddenHazardName" DataField="HiddenHazardName" FieldType="String"
                        HeaderText="隐患名称或内容" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="80px" ColumnID="IsMajorName" DataField="IsMajorName" FieldType="String"
                        HeaderText="隐患分级" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="FindTime" DataField="FindTime" FieldType="String"
                        HeaderText="检查时间" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>                    
                     <f:RenderField Width="200px" ColumnID="ReasonAnalysis" DataField="ReasonAnalysis" FieldType="String"
                        HeaderText="原因分析" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="200px" ColumnID="CorrectMeasures" DataField="CorrectMeasures" FieldType="String"
                        HeaderText="整改措施" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="200px" ColumnID="CorrectUnitName" DataField="CorrectUnitName" FieldType="String"
                        HeaderText="整改单位" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="80px" ColumnID="CorrectManName" DataField="CorrectManName" FieldType="String"
                        HeaderText="负责人" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="120px" ColumnID="CorrectMoney" DataField="CorrectMoney" FieldType="String"
                        HeaderText="整改资金(万元)" HeaderTextAlign="Center" TextAlign="Right">
                    </f:RenderField>
                     <f:RenderField Width="150px" ColumnID="CorrectTime" DataField="CorrectTime" FieldType="String"
                        HeaderText="消除日期" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                     <f:RenderField Width="90px" ColumnID="AcceptanceManName" DataField="AcceptanceManName" FieldType="String"
                        HeaderText="复查人" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                     <f:RenderField Width="100px" ColumnID="Remark" DataField="Remark" FieldType="String"
                        HeaderText="备注" HeaderTextAlign="Center" TextAlign="Left">
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
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuOut" OnClick="btnMenuOut_Click" EnableAjax="false" DisableControlBeforePostBack="false"
           runat="server" Text="导出" Icon="FolderUp">
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
