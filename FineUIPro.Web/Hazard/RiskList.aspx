<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RiskList.aspx.cs" Inherits="FineUIPro.Web.Hazard.RiskList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>风险信息库</title>
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
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="风险信息库" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="RiskListId" AllowCellEditing="true" EnableColumnLines="true"
                ClicksToEdit="2" DataIDField="RiskListId" AllowSorting="true" SortField="EvaluationTime"
                SortDirection="DESC" OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true"
                PageSize="15" OnPageIndexChange="Grid1_PageIndexChange" EnableRowDoubleClickEvent="true"
                OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" Position="Top" runat="server">
                         <Items>
                            <f:RadioButtonList ID="rbRiskLevel" runat="server" AutoPostBack="true" Label="风险级别" LabelAlign="Right"
                                AutoColumnWidth="true" OnSelectedIndexChanged="ckStates_SelectedIndexChanged">                               
                                <f:RadioItem Text="一级" Value="1" Selected="true"/>
                                <f:RadioItem Text="二级" Value="2" />
                                <f:RadioItem Text="三级" Value="3" />
                                <f:RadioItem Text="四级" Value="4" />
                                <f:RadioItem Text="五级" Value="5" />
                            </f:RadioButtonList>
                            <f:RadioButtonList ID="ckStates" runat="server" AutoPostBack="true" Label="风险状态" LabelAlign="Right"
                                AutoColumnWidth="true" OnSelectedIndexChanged="ckStates_SelectedIndexChanged">                               
                                <f:RadioItem Text="存在" Value="0" Selected="true"/>
                                <f:RadioItem Text="已取消" Value="1" />
                            </f:RadioButtonList>
                            <f:RadioButtonList ID="ckIsUsed" runat="server" AutoPostBack="true" Label="启用" LabelAlign="Right"
                                AutoColumnWidth="true" OnSelectedIndexChanged="ckStates_SelectedIndexChanged">    
                                <f:RadioItem Text="全部" Value="0" Selected="true"/>
                                <f:RadioItem Text="是" Value="1"/>
                                <f:RadioItem Text="否" Value="2" />
                            </f:RadioButtonList>
                            <f:ToolbarSeparator runat="server"></f:ToolbarSeparator>
                            <f:TextBox runat="server"  ID="txtName" EmptyText="输入查询条件" AutoPostBack="true" 
                                OnTextChanged="TextBox_TextChanged" Width="200px" >
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
                    <f:RenderField Width="120px" ColumnID="InstallationName" DataField="InstallationName" SortField="InstallationName"
                        FieldType="String" HeaderText="所属单位" HeaderTextAlign="Center" TextAlign="Left" >
                    </f:RenderField>
                    <f:RenderField Width="120px" ColumnID="TaskActivity" DataField="TaskActivity" SortField="TaskActivity"
                        FieldType="String" HeaderText="设备设施</br>作业活动名称" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField> 
                    <f:RenderField Width="80px" ColumnID="TypeName" DataField="TypeName" SortField="TypeName"
                        FieldType="String" HeaderText="属性" HeaderTextAlign="Center" TextAlign="Left" >
                    </f:RenderField>
                    <f:RenderField Width="300px" ColumnID="HazardDescription" DataField="HazardDescription" SortField="HazardDescription"
                        FieldType="String" HeaderText="风险点" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="220px" ColumnID="PossibleAccidents" DataField="PossibleAccidents" SortField="PossibleAccidents"
                        FieldType="String" HeaderText="危害</br>潜在事件" HeaderTextAlign="Center" TextAlign="Left" >
                    </f:RenderField>
                     <%--<f:RenderField Width="75px" ColumnID="RiskLevelName" DataField="RiskLevelName" SortField="RiskLevelName"
                        FieldType="String" HeaderText="评价级别" HeaderTextAlign="Center" TextAlign="Left" >
                    </f:RenderField>   --%>                                       
                    <f:RenderField Width="150px" ColumnID="HazardJudgeName" DataField="HazardJudgeName" SortField="HazardJudgeName"
                        FieldType="String" HeaderText="评价值" HeaderTextAlign="Center" TextAlign="Left" >
                    </f:RenderField>  
                    <f:RenderField Width="80px" ColumnID="EvaluationMethod" DataField="EvaluationMethod" SortField="EvaluationMethod"
                        FieldType="String" HeaderText="评价方法" HeaderTextAlign="Center" TextAlign="Left" >
                    </f:RenderField>
                     <f:RenderField Width="300px" ColumnID="ControlMeasures" DataField="ControlMeasures" SortField="ControlMeasures"
                        FieldType="String" HeaderText="工程技术措施" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="350px" ColumnID="ManagementMeasures" DataField="ManagementMeasures" FieldType="String"
                        HeaderText="管理措施" HeaderTextAlign="Center" TextAlign="Left" >
                    </f:RenderField>
                        <f:RenderField Width="150px" ColumnID="ProtectiveMeasures" DataField="ProtectiveMeasures" FieldType="String"
                        HeaderText="防护措施" HeaderTextAlign="Center" TextAlign="Left" >
                    </f:RenderField>
                        <f:RenderField Width="250px" ColumnID="OtherMeasures" DataField="OtherMeasures" FieldType="String"
                        HeaderText="其他措施" HeaderTextAlign="Center" TextAlign="Left" >
                    </f:RenderField>
                      <f:RenderField  ColumnID="EvaluationTime" DataField="EvaluationTime" SortField="EvaluationTime" Width="100px"
                        HeaderText="评价时间" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="RiskManName" DataField="RiskManName" SortField="RiskManName"
                        FieldType="String" HeaderText="评价人" HeaderTextAlign="Center" TextAlign="Left" >
                    </f:RenderField>
                     <f:RenderField Width="130px" ColumnID="RiskOwnerNames" DataField="RiskOwnerNames" SortField="RiskOwnerNames"
                        FieldType="String" HeaderText="风险责任人" HeaderTextAlign="Center" TextAlign="Left" >
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="StartDate" DataField="StartDate" SortField="StartDate"
                        HeaderText="启用时间" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                   <f:CheckBoxField Width="60px" RenderAsStaticField="true" TextAlign="Center"  DataField="IsUsed" HeaderText="启用" />
                   <%-- <f:RenderField Width="120px" ColumnID="ControlInstallationName" DataField="ControlInstallationName" SortField="ControlInstallationName"
                        FieldType="String" HeaderText="控制单位" HeaderTextAlign="Center" TextAlign="Left" >
                    </f:RenderField>                    
                    <f:RenderField Width="90px" ColumnID="PatrolFrequency" DataField="PatrolFrequency" SortField="PatrolFrequency"
                        FieldType="String" HeaderText="巡查频次" HeaderTextAlign="Center" TextAlign="Left" >
                    </f:RenderField>--%>
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
    <f:Window ID="Window1" Title="风险信息库" Hidden="true" EnableIFrame="true" EnableMaximize="true" 
        Target="Top" EnableResize="true" runat="server" IsModal="true"  OnClose="Window1_Close"
        CloseAction="HidePostBack" Height="600px" Width="1100px">
    </f:Window>
     <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Top" EnableResize="true" runat="server" IsModal="true" Width="670px"
        Height="460px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
          <f:MenuButton ID="btnMenuView" OnClick="btnMenuView_Click" EnablePostBack="true"
            runat="server" Text="查看"  Icon="TableGo">
        </f:MenuButton>   
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
            Hidden="true" ConfirmText="删除选中行？" ConfirmTarget="Top" runat="server" Text="删除"
            Icon="Delete">
        </f:MenuButton>
        <f:MenuButton ID="MenuButton1" OnClick="btnQView_Click" EnablePostBack="true"
            runat="server" Text="二维码"  Icon="Shading">
        </f:MenuButton>
        <f:MenuButton ID="btnSurvey" OnClick="btnSurvey_Click" EnablePostBack="true" Hidden="true"
            runat="server" Text="二次评估"  Icon="TableEdit">
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
