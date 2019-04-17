<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SCL.aspx.cs" Inherits="FineUIPro.Web.Hazard.SCL" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SCL评价</title>
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
    <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
        <Items>
            <f:Panel runat="server" ID="panelLeftRegion" RegionPosition="Left" RegionSplit="true"
                EnableCollapse="true" Width="220" Title="所属单位" TitleToolTip="所属单位" ShowBorder="true"
                ShowHeader="false" AutoScroll="true" BodyPadding="1px" IconFont="ArrowCircleLeft"
                Layout="Fit">
                <Items>                   
                   <f:Tree ID="trInstall" EnableCollapse="true" ShowHeader="True" Title="所属单位" OnNodeCommand="trInstall_NodeCommand"
                        AutoLeafIdentification="true" runat="server" EnableTextSelection="True" EnableSingleClickExpand="true" AutoScroll="true">                       
                    </f:Tree>
                </Items>
            </f:Panel>
            <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="SCL评价"
                TitleToolTip="SCL评价" AutoScroll="true">
                <Items>
                   <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="SCL评价" EnableCollapse="true"
                        runat="server" BoxFlex="1" DataKeyNames="EuipmentId" AllowCellEditing="true" EnableColumnLines="true"
                        ClicksToEdit="2" DataIDField="EuipmentId" AllowSorting="true" SortField="InstallationCode,WorkAreaCode,EuipmentCode"
                        SortDirection="ASC" OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true"
                        PageSize="15" OnPageIndexChange="Grid1_PageIndexChange" EnableRowDoubleClickEvent="true"
                        OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar1" Position="Top" runat="server">
                                 <Items>     
                                     <f:RadioButtonList ID="ckStates" runat="server" AutoPostBack="true" Label="评价状态" LabelAlign="Right"
                                        AutoColumnWidth="true" OnSelectedIndexChanged="ckDataType_SelectedIndexChanged">                               
                                        <f:RadioItem Text="未评价" Value="0" Selected="true"/>
                                        <f:RadioItem Text="已评价" Value="1" />
                                    </f:RadioButtonList>
                                    <f:TextBox runat="server" Label="查询" ID="txtName" EmptyText="输入查询条件" AutoPostBack="true"
                                        OnTextChanged="TextBox_TextChanged" LabelWidth="60px" LabelAlign="Right">
                                    </f:TextBox>  
                                     <f:ToolbarFill runat="server" ID="fill">
                                     </f:ToolbarFill>
                                     <f:Button ID="btnHelp" runat="server" ToolTip="点击下载风险评价方法及说明" Text="SCL评价说明" Icon="Help">
                                        <Listeners>
                                            <f:Listener Event="click" Handler="onToolSourceCodeClick" />
                                        </Listeners>
                                    </f:Button>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Columns>                   
                          <%--   <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                                TextAlign="Center" />  --%>
                             <f:RenderField Width="110px" ColumnID="EuipmentCode" DataField="EuipmentCode" FieldType="String"
                                HeaderText="编号" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                             <f:RenderField Width="130px" ColumnID="EuipmentName" DataField="EuipmentName" FieldType="String"
                                HeaderText="设备设施" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField> 
                            <f:RenderField Width="120px" ColumnID="InstallationName" DataField="InstallationName" FieldType="String"
                                HeaderText="所属单位(装置)" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                            <f:RenderField Width="120px" ColumnID="WorkAreaName" DataField="WorkAreaName" FieldType="String"
                                HeaderText="所属单元(区域)" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>  
                            <f:RenderField Width="90px" ColumnID="Identification" DataField="Identification" FieldType="String"
                                HeaderText="评价方法" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                            <f:RenderField Width="350px" ColumnID="Remark" DataField="Remark" FieldType="String"
                                HeaderText="描述" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                            </f:RenderField>  
                            <f:RenderField Width="75px" ColumnID="EuipmentNo" DataField="EuipmentNo" FieldType="String"
                                HeaderText="位号" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                              <f:RenderField Width="80px" ColumnID="EuipmentTypeName" DataField="EuipmentTypeName" FieldType="String"
                                HeaderText="类型" HeaderTextAlign="Center" TextAlign="Left">
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
    <f:Window ID="Window1" Title="SCL评价" Hidden="true" EnableIFrame="true" EnableMaximize="true" 
        Target="Top" EnableResize="true" runat="server" IsModal="true"  Height="560px" Width="1200px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
            Hidden="true" runat="server" Text="评价" Icon="TableEdit">
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
        // 评价方法文档
        function onToolSourceCodeClick(event) {
            window.open('../File/Word/风险评价方法及说明/SCL风险评价法使用说明.doc', '_blank');
        }
    </script>
</body>
</html>
