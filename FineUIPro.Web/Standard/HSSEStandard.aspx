<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HSSEStandard.aspx.cs" Inherits="FineUIPro.Web.Standard.HSSEStandard" %>

<!DOCTYPE html>
<html>
<head runat="server">   
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .customlabel span
        {
            color: red;
            font-weight: bold;
        }        
        .f-grid-row .f-grid-cell-inner {
            white-space: normal;
            word-break: break-all;
        }        
    </style>
</head>
  
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Top" ToolbarAlign="Left" runat="server">
                <Items>
                    <f:RadioButtonList ID="ckSpecialty" runat="server" AutoPostBack="true" ColumnNumber="8"
                        AutoColumnWidth="true" OnSelectedIndexChanged="ckSpecialty_SelectedIndexChanged" >                               
                    </f:RadioButtonList>
                </Items>
            </f:Toolbar>
        </Toolbars>
        <Items>
            <f:Panel runat="server" ID="panelLeftRegion" RegionPosition="Left" RegionSplit="true"
                EnableCollapse="true" Width="300px" Title="专家辅助" TitleToolTip="右键点击下原文" ShowBorder="true"
                ShowHeader="false" AutoScroll="true" BodyPadding="1px" IconFont="ArrowCircleLeft" Layout="Fit">                                
                <Items>
                    <f:Tree ID="tvHSSEStandard" KeepCurrentSelection="true"
                        ShowHeader="false" OnNodeCommand="tvHSSEStandard_NodeCommand" runat="server"
                        ShowBorder="false" EnableSingleClickExpand="true">
                        <Listeners>
                            <f:Listener Event="beforenodecontextmenu" Handler="onTreeNodeContextMenu" />
                        </Listeners>
                    </f:Tree>
                </Items>
            </f:Panel>
            <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="专家辅助"
                AutoScroll="true">
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" EnableCollapse="true" runat="server"
                        BoxFlex="1" DataKeyNames="HSSEStandardId" ClicksToEdit="2"
                        DataIDField="HSSEStandardId" AllowSorting="true" SortField="HSSEStandardCode" SortDirection="ASC"
                        OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true" EnableColumnLines="true"
                        PageSize="15" OnPageIndexChange="Grid1_PageIndexChange"
                        EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar3" Position="Top" ToolbarAlign="Right" runat="server">
                                <Items>                                    
                                    <f:Button ID="btnNewDetail" ToolTip="新增" Icon="Add" runat="server"
                                        OnClick="btnNewDetail_Click" Hidden="true">
                                    </f:Button>  
                                    <f:Button ID="btnImport" ToolTip="导入" Icon="ApplicationAdd" Hidden="true" runat="server"
                                            OnClick="btnImport_Click">
                                    </f:Button>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Columns>                           
                            <f:RenderField ColumnID="HSSEStandardCode" DataField="HSSEStandardCode" SortField="HSSEStandardCode" Width="80px"
                                FieldType="String" HeaderText="编号" HeaderTextAlign="Center" TextAlign="left">
                            </f:RenderField>
                            <f:RenderField ColumnID="HSSEStandardName" DataField="HSSEStandardName" SortField="HSSEStandardName" Width="200px" ExpandUnusedSpace="true"
                                FieldType="String" HeaderText="具体要求" HeaderTextAlign="Center" TextAlign="left">
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
                                <f:ListItem Text="15" Value="15" />
                                <f:ListItem Text="20" Value="20" />
                                <f:ListItem Text="25" Value="25" />
                                <f:ListItem Text="30" Value="30" />
                                <f:ListItem Text="所有行" Value="10000" />
                            </f:DropDownList>
                        </PageItems>
                    </f:Grid>
                </Items>
            </f:Panel>
        </Items>
    </f:Panel>
    <f:Menu ID="Menu1" runat="server">
        <Items>
            <f:MenuButton ID="btnAttachUrl" EnablePostBack="true" runat="server"  Text="原文下载" Icon="TableCell"
                OnClick="btnAttachUrl_Click">
            </f:MenuButton>
        </Items>
    </f:Menu>
    <f:Menu ID="Menu2" runat="server">
        <Items>
            <f:MenuButton ID="btnMenuModifyDetail" EnablePostBack="true" runat="server" Icon="BulletEdit"  Text="修改"
                OnClick="btnMenuModifyDetail_Click">
            </f:MenuButton>
            <f:MenuButton ID="btnMenuDelDetail" EnablePostBack="true" runat="server"  Icon="Delete" Text="删除" ConfirmText="确认删除选中内容？"
                OnClick="btnMenuDelDetail_Click">
            </f:MenuButton>
        </Items>
    </f:Menu>        
    <f:Window ID="Window1" Title="文件内容" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="800px" Height="540px">
    </f:Window>
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>    
    <f:Window ID="Window2" Title="导入信息" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Top" EnableResize="true" runat="server" OnClose="Window2_Close" IsModal="false"
        CloseAction="HidePostBack" Width="1200px" Height="560px">
    </f:Window>
    </form>
    <script type="text/javascript">
        <%--  var treeID = '<%= tvHSSEStandard.ClientID %>';--%>
        var menuID = '<%= Menu1.ClientID %>';
        var menu2ID = '<%= Menu2.ClientID %>';
        // 保存当前菜单对应的树节点ID
        var currentNodeId;
        // 返回false，来阻止浏览器右键菜单
        function onTreeNodeContextMenu(event, nodeId) {
            currentNodeId = nodeId;
            F(menuID).show();
            return false;
        }
        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
            F(menu2ID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }
    </script>
</body>
</html>
