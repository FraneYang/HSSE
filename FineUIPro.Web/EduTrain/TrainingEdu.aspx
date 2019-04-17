<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrainingEdu.aspx.cs" Inherits="FineUIPro.Web.EduTrain.TrainingEdu" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
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
      <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
        <Items>
              <f:Panel runat="server" ID="panelLeftRegion" RegionPosition="Left" RegionSplit="true"
                EnableCollapse="true" Width="250" Title="培训教材库" TitleToolTip="培训教材库" ShowBorder="true"
                ShowHeader="false" AutoScroll="true" BodyPadding="5px" IconFont="ArrowCircleLeft" 
                Layout="HBox">
                <Items>                    
                    <f:Tree ID="trTraining" EnableCollapse="true" ShowHeader="true" Title="培训教材库" OnNodeCommand="trTraining_NodeCommand"
                        AutoLeafIdentification="true" runat="server" EnableTextSelection="True">
                        <Listeners>
                            <f:Listener Event="beforenodecontextmenu" Handler="onTreeNodeContextMenu" />
                        </Listeners>
                    </f:Tree>
                </Items>
            </f:Panel>
            <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="培训教材明细"
                TitleToolTip="培训教材明细" AutoScroll="true">
                <Items>
                    <f:Grid ID="Grid1" Width="870px" ShowBorder="true" ShowHeader="false" EnableCollapse="true"
                        runat="server" BoxFlex="1" DataKeyNames="TrainingEduItemId" AllowCellEditing="true"
                        ClicksToEdit="2" DataIDField="TrainingEduItemId" AllowSorting="true" SortField="TrainingEduItemCode"
                        SortDirection="DESC" OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true"
                        PageSize="15" OnPageIndexChange="Grid1_PageIndexChange" EnableRowDoubleClickEvent="true"
                        OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True" EnableColumnLines="true">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar3" Position="Top" runat="server">
                                <Items>
                                    <f:TextBox ID="txtName" runat="server" Label="查询" EmptyText="输入查询条件"
                                        AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="80px"
                                        LabelAlign="Right">
                                    </f:TextBox>                                  
                                    <f:ToolbarFill ID="ToolbarFill1" runat="server">
                                    </f:ToolbarFill>
                                    <f:Button ID="btnNewDetail" ToolTip="新增" Icon="Add" runat="server" OnClick="btnNewDetail_Click"
                                        Hidden="true">
                                    </f:Button>                                    
                                    <f:Button ID="btnImport" ToolTip="导入" Icon="ApplicationAdd" Hidden="true" runat="server"
                                            OnClick="btnImport_Click">
                                    </f:Button>                                     
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Columns>                         
                            <f:RenderField ColumnID="TrainingEduItemCode" DataField="TrainingEduItemCode" SortField="TrainingEduItemCode" 
                                Width="90px" FieldType="String" HeaderText="编号" HeaderTextAlign="Center" TextAlign="left">
                            </f:RenderField>
                             <f:RenderField ColumnID="TrainingEduItemName" DataField="TrainingEduItemName" SortField="TrainingEduItemName" 
                                Width="250px"  FieldType="String" HeaderText="名称" HeaderTextAlign="Center" TextAlign="left">
                            </f:RenderField>  
                         <%--   <f:RenderField ColumnID="Summary" DataField="Summary" SortField="Summary" 
                                Width="150px" ExpandUnusedSpace="true" FieldType="String" HeaderText="教材内容" HeaderTextAlign="Center" TextAlign="left">
                            </f:RenderField>--%>
                             <f:RenderField ColumnID="InstallationNames" DataField="InstallationNames" SortField="InstallationNames" 
                                Width="250px" ExpandUnusedSpace="true"  FieldType="String" HeaderText="适合岗位(装置/科室)" HeaderTextAlign="Center" TextAlign="left">
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
        </Items>
    </f:Panel>
    <f:Window ID="Window1" Title="培训教材" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Top" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="400px" Height="200px">
    </f:Window>
    <f:Window ID="Window2" Title="培训教材科详情" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Top" EnableResize="true" runat="server" OnClose="Window2_Close" IsModal="true"
        Width="900px" Height="620px">
    </f:Window>   
     <f:Window ID="Window3" Title="导入信息" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Top" EnableResize="true" runat="server" OnClose="Window3_Close" IsModal="false"
        CloseAction="HidePostBack" Width="1200px" Height="560px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
      
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
            runat="server" Text="编辑" Hidden="true" Icon="TableEdit">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
            Icon="Delete" ConfirmText="删除选中行？" ConfirmTarget="Top" runat="server" Text="删除"
            Hidden="true">
        </f:MenuButton>
    </f:Menu>
    <f:Menu ID="Menu2" runat="server">
         <f:MenuButton ID="btnMenuADD" OnClick="btnMenuADD_Click" EnablePostBack="true"
            runat="server" Text="新增" Icon="Add" Hidden="true">
        </f:MenuButton>
        <f:MenuButton ID="btnTreeMenuEdit" OnClick="btnTreeMenuEdit_Click" EnablePostBack="true"
            runat="server" Text="编辑" Hidden="true" Icon="TableEdit">
        </f:MenuButton>
        <f:MenuButton ID="btnTreeMenuDelete" OnClick="btnTreeMenuDelete_Click" EnablePostBack="true"
            Icon="Delete" ConfirmText="删除选中节点？" ConfirmTarget="Top" runat="server" Text="删除"
            Hidden="true">
        </f:MenuButton>
    </f:Menu>
    </form>
    <script type="text/javascript">    
        var menuID = '<%= Menu1.ClientID %>';
        var menuID2 = '<%= Menu2.ClientID %>';
        // 保存当前菜单对应的树节点ID
        var currentNodeId;

        // 返回false，来阻止浏览器右键菜单
        function onTreeNodeContextMenu(event, nodeId) {
            currentNodeId = nodeId;
            F(menuID2).show();
            return false;
        }
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
