<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SCLInfo.aspx.cs" Inherits="FineUIPro.Web.BaseInfo.SCLInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SCL评价</title>
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
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="SCL评价" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="SCLId" AllowCellEditing="true" EnableColumnLines="true"
                ClicksToEdit="2" DataIDField="SCLId" AllowSorting="true" SortField="EuipmentTypeCode,SortIndex"
                SortDirection="ASC" OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true"
                PageSize="15" OnPageIndexChange="Grid1_PageIndexChange" EnableRowDoubleClickEvent="true"
                OnRowDoubleClick="Grid1_RowDoubleClick" Width="980px" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" Position="Top" runat="server">
                         <Items>
                             <f:DropDownList ID="drpEuipmentTypeId" runat="server" Label="类型" LabelWidth="60px"
                                    EnableEdit="true" ForceSelection="false" AutoPostBack="true" OnSelectedIndexChanged="TextBox_TextChanged">
                              </f:DropDownList>
                            <f:TextBox runat="server" Label="查询" ID="txtName" EmptyText="输入查询条件" AutoPostBack="true"
                                OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="60px" LabelAlign="Right">
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
                   <f:RenderField Width="50px" ColumnID="SortIndex" DataField="SortIndex" FieldType="String"
                        HeaderText="序号" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                     <f:RenderField Width="100px" ColumnID="EuipmentTypeName" DataField="EuipmentTypeName" FieldType="String"
                        HeaderText="类型" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="120px" ColumnID="CheckItem" DataField="CheckItem" FieldType="String"
                        HeaderText="检查项目" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                     <f:RenderField Width="160px" ColumnID="Standard" DataField="Standard" 
                        FieldType="String" HeaderText="标准" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="160px" ColumnID="Consequence" DataField="Consequence" FieldType="String"
                        HeaderText="未达标准的主要后果" HeaderTextAlign="Center" TextAlign="Left" >
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="NowControlMeasures" DataField="NowControlMeasures" FieldType="String"
                        HeaderText="现有控制措施" HeaderTextAlign="Center" TextAlign="Left" >
                    </f:RenderField>
                    <f:RenderField Width="50px" ColumnID="HazardJudge_L" DataField="HazardJudge_L" FieldType="Float"
                        HeaderText="L" HeaderTextAlign="Center" TextAlign="Center" >
                    </f:RenderField>
                    <f:RenderField Width="50px" ColumnID="HazardJudge_S" DataField="HazardJudge_S" FieldType="Float"
                        HeaderText="S" HeaderTextAlign="Center" TextAlign="Center" >
                    </f:RenderField> 
                     <f:RenderField Width="50px" ColumnID="HazardJudge_R" DataField="HazardJudge_R" FieldType="Float"
                        HeaderText="R" HeaderTextAlign="Center" TextAlign="Center" >
                    </f:RenderField> 
                    <f:RenderField Width="80px" ColumnID="RiskLevelName" DataField="RiskLevelName" FieldType="String"
                        HeaderText="风险等级" HeaderTextAlign="Center" TextAlign="Left" >
                    </f:RenderField> 
                    <f:RenderField Width="250px" ColumnID="ControlMeasures" DataField="ControlMeasures" FieldType="String"
                        HeaderText="建议改正/控制措施" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
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
    <f:Window ID="Window1" Title="SCL评价" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Top" EnableResize="true" runat="server" IsModal="true" Width="800px"
        Height="450px">
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
