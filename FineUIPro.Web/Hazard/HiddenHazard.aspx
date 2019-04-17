<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HiddenHazard.aspx.cs" Inherits="FineUIPro.Web.Hazard.HiddenHazard" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>隐患巡检</title>
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
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="隐患巡检" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="HiddenHazardId" AllowCellEditing="true" EnableColumnLines="true"
                ClicksToEdit="2" DataIDField="HiddenHazardId" AllowSorting="true" SortField="HiddenHazardCode"
                SortDirection="DESC" OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true" PageSize="15" OnPageIndexChange="Grid1_PageIndexChange"
                EnableTextSelection="True" EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server">
                        <Items>
                            <f:TextBox runat="server" ID="txtName" EmptyText="输入查询条件" 
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px">
                             </f:TextBox>                            
                            <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>                    
                    <f:RenderField Width="120px" ColumnID="HiddenHazardCode" DataField="HiddenHazardCode" EnableFilter="true"
                        SortField="HiddenHazardCode" FieldType="String" HeaderText="编号" HeaderTextAlign="Center"
                        TextAlign="Left">                      
                    </f:RenderField>    
                      <f:RenderField Width="120px" ColumnID="HiddenHazardTypeName" DataField="HiddenHazardTypeName" EnableFilter="true"
                        SortField="HiddenHazardTypeName" FieldType="String" HeaderText="隐患类别" HeaderTextAlign="Center"
                        TextAlign="Left">                      
                    </f:RenderField>    
                    <f:CheckBoxField Width="50px" RenderAsStaticField="true" TextAlign="Center"  DataField="IsMajor" HeaderText="重大" />                         
                    <f:RenderField Width="180px" ColumnID="HiddenHazardName" DataField="HiddenHazardName" 
                        SortField="HiddenHazardName" FieldType="String" HeaderText="隐患名称" HeaderTextAlign="Center"
                        TextAlign="Left">                       
                    </f:RenderField> 
                    <f:RenderField Width="120px" ColumnID="CorrectMeasures" DataField="CorrectMeasures" 
                        SortField="CorrectMeasures" FieldType="String" HeaderText="整改措施" HeaderTextAlign="Center"
                        TextAlign="Left">                       
                    </f:RenderField>  
                    <f:TemplateField ColumnID="tfImageUrl1" Width="100px" HeaderText="整改前图片" HeaderTextAlign="Center"
                        TextAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lbImageUrl" runat="server" Text='<%# ConvertImageUrlByImage(Eval("BePohotoUrl")) %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:TemplateField ColumnID="tfImageUrl2" Width="100px" HeaderText="整改后图片" HeaderTextAlign="Center"
                        TextAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# ConvertImageUrlByImage(Eval("AfPohotoUrl")) %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:RenderField  ColumnID="ReasonAnalysis" DataField="ReasonAnalysis" SortField="ReasonAnalysis" FieldType="String" Width="120px"
                        HeaderText="原因分析" HeaderTextAlign="Center" TextAlign="Left" >
                    </f:RenderField>
                    <f:RenderField  ColumnID="LimitTime" DataField="LimitTime" SortField="LimitTime" Width="150px"
                        HeaderText="整改期限" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>   
                     <f:RenderField  ColumnID="CorrectManName" DataField="CorrectManName" SortField="CorrectManName" FieldType="String" Width="80px"
                        HeaderText="责任人" HeaderTextAlign="Center" TextAlign="Left" >
                    </f:RenderField>
                      <f:RenderField Width="120px" ColumnID="HiddenHazardPalce" DataField="HiddenHazardPalce" 
                        SortField="HiddenHazardPalce" FieldType="String" HeaderText="隐患位置" HeaderTextAlign="Center"
                        TextAlign="Left">                       
                    </f:RenderField>   
                    <f:RenderField  ColumnID="OperateManNames" DataField="OperateManNames" SortField="OperateManNames" FieldType="String" Width="110px"
                        HeaderText="作业人" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>                                  
                    <f:RenderField  ColumnID="StatesName" DataField="StatesName" SortField="StatesName" FieldType="String" Width="70px"
                        HeaderText="状态" HeaderTextAlign="Center" TextAlign="Left">
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
                        <f:ListItem Text="20" Value="20" />
                        <f:ListItem Text="50" Value="50" />
                        <f:ListItem Text="100" Value="100" />
                        <f:ListItem Text="所有行" Value="10000" />
                    </f:DropDownList>
                </PageItems>
            </f:Grid>
        </Items>
    </f:Panel>
   <f:Window ID="Window1" Title="隐患详细" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Top" EnableResize="true" runat="server" IsModal="true"
        Width="900px" Height="600px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
         <f:MenuButton ID="btnMenuView" OnClick="btnMenuView_Click" EnablePostBack="true"
            runat="server" Text="查看"  Icon="TableGo">
        </f:MenuButton>   
         <f:MenuButton ID="btnFile" OnClick="btnMenuFile_Click" EnablePostBack="true"
            runat="server" Text="归档" Icon="FolderPage" Hidden="true">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"  Icon="Delete"
            ConfirmText="删除选中行？" ConfirmTarget="Top" runat="server" Text="删除" Hidden ="true">
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
