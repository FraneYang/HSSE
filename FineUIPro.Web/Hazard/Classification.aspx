<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Classification.aspx.cs" Inherits="FineUIPro.Web.Hazard.Classification" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>风险巡检计划</title>
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
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1"  runat="server" />
        <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
            <Items>               
                <f:Panel runat="server" ID="panelLeftRegion" RegionPosition="Left" RegionSplit="true" EnableCollapse="true"
                    Width="160px" Title="巡检人" TitleToolTip="巡检人信息" ShowBorder="true" ShowHeader="false" 
                    BodyPadding="5px" Layout="HBox"> 
                    <Toolbars>
                         <f:Toolbar ID="Toolbar1" Position="Top" ToolbarAlign="Left" runat="server">
                                <Items>
                                     <f:TextBox runat="server" ID="txtName" EmptyText="输入查询条件" 
                                        LabelAlign="Left" Width="100px">
                                    </f:TextBox>
                                    <f:Button runat="server" ID="btnSearch" OnClick="btnSearch_Click" Icon="SystemSearch"></f:Button>
                                </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Items>                        
                        <f:Tree ID="tvPatrolMan" KeepCurrentSelection="true" Width="150px"
                            ShowHeader="false" OnNodeCommand="tvPatrolMan_NodeCommand" runat="server"
                            ShowBorder="false" EnableSingleClickExpand="true">                            
                        </f:Tree>
                    </Items>
                </f:Panel>
                <f:Panel runat="server" ID="panel2" RegionPosition="Center" ShowBorder="true"  AutoScroll="true"
                    Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="项目考核项">                       
                        <Toolbars>
                            <f:Toolbar ID="Toolbar2" Position="Top" ToolbarAlign="Left" runat="server">
                                <Items>       
                                     <f:DropDownList ID="drpState" runat="server" Label="状态" AutoPostBack="true" OnSelectedIndexChanged="TextBox_TextChanged" 
                                        LabelWidth="50px" Width="120px">
                                        <f:ListItem Text="所有" Value="0"/>
                                        <f:ListItem Text="未巡检" Value="1" Selected="true"/>
                                        <f:ListItem Text="已巡检" Value="2"/>
                                        <f:ListItem Text="超期未巡检" Value="3"/>
                                    </f:DropDownList>                                   
                                    <f:TextBox runat="server" Label="查询" ID="txtTitle" EmptyText="输入查询条件"
                                        AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="180px" LabelWidth="50px"
                                        LabelAlign="right">
                                    </f:TextBox>
                                     <f:DatePicker runat="server" Label="考核时间" ID="txtStarTime" EnableEdit="true" 
                                         AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="160px" LabelWidth="80px"
                                        LabelAlign="right"></f:DatePicker>
                                    <f:DatePicker runat="server" Label="至" ID="txtEndTime" EnableEdit="true" 
                                        AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="140px" LabelWidth="40px"
                                        LabelAlign="right"></f:DatePicker> 
                                     <f:DropDownList ID="drpIsUsed" runat="server" Label="启用" 
                                         AutoPostBack="true" OnSelectedIndexChanged="TextBox_TextChanged" 
                                        LabelWidth="50px" Width="120px">
                                        <f:ListItem Text="全部" Value="0" Selected="true"/>
                                        <f:ListItem Text="是" Value="1"/>
                                        <f:ListItem Text="否" Value="2"/>
                                    </f:DropDownList> 
                                    <f:ToolbarFill runat="server" ID="lbTemp1"></f:ToolbarFill>
                                    <f:Button ID="btnRiskPlan" ToolTip="生成巡检明细" Icon="FolderPage"
                                        runat="server" OnClick="btnRiskPlan_Click" Hidden="true">
                                    </f:Button>
                                    <f:Button ID="btnPlan" ToolTip="生成当年巡检计划" Icon="FolderTable" 
                                        runat="server" OnClick="btnPlan_Click" Hidden="true">
                                    </f:Button>
                                    <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                        EnableAjax="false" DisableControlBeforePostBack="false">
                                    </f:Button>   
                                     <f:Button ID="btnDelRiskListItem" ToolTip="删除巡检明细" Icon="Delete"
                                        runat="server" OnClick="btnDelRiskListItem_Click" Hidden="true">
                                    </f:Button>
                                    <f:Button ID="btnPatrolPlan" ToolTip="删除巡检计划" Icon="TabDelete"
                                        runat="server" OnClick="btnPatrolPlan_Click" Hidden="true">
                                    </f:Button>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                    <Items>                         
                        <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" EnableCollapse="true" runat="server"
                            BoxFlex="1" DataKeyNames="PatrolPlanId" ClicksToEdit="2"
                            DataIDField="PatrolPlanId" AllowSorting="true" SortField="StartDate" SortDirection="ASC"
                            OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true" EnableColumnLines="true"
                            PageSize="100" OnPageIndexChange="Grid1_PageIndexChange" EnableTextSelection="True">                           
                            <Columns>       
                                <f:RenderField Width="100px" ColumnID="InstallationName" DataField="InstallationName" SortField="InstallationName"
                                    FieldType="String" HeaderText="所属单位" HeaderTextAlign="Center" TextAlign="Left" >
                                </f:RenderField>
                                <f:RenderField Width="120px" ColumnID="TaskActivity" DataField="TaskActivity" SortField="TaskActivity"
                                    FieldType="String" HeaderText="设备设施</br>作业活动名称" HeaderTextAlign="Center" TextAlign="Left">
                                </f:RenderField> 
                                <f:RenderField Width="250px" ColumnID="HazardDescription" DataField="HazardDescription" SortField="HazardDescription"
                                    FieldType="String" HeaderText="风险点" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                                </f:RenderField>                              
                                <f:RenderField Width="60px" ColumnID="RiskLevelName" DataField="RiskLevelName" SortField="RiskLevelName"
                                    FieldType="String" HeaderText="级别" HeaderTextAlign="Center" TextAlign="Left" >
                                </f:RenderField>
                                 <f:RenderField Width="100px" ColumnID="StartDate" DataField="StartDate"  Renderer="Date"
                                    SortField="StartDate" HeaderText="开始日期" HeaderTextAlign="Center"  FieldType="Date"
                                    TextAlign="Left" >
                                </f:RenderField>    
                                 <f:RenderField Width="90px" ColumnID="EndTime" DataField="EndTime"  Renderer="Date"
                                    SortField="EndTime" HeaderText="结束日期" HeaderTextAlign="Center"  FieldType="Date"
                                    TextAlign="Left" >
                                </f:RenderField>   
                                 <f:RenderField Width="90px" ColumnID="CheckDate" DataField="CheckDate"  Renderer="Date"
                                    SortField="CheckDate" HeaderText="巡检日期" HeaderTextAlign="Center"  FieldType="Date"
                                    TextAlign="Left" >
                                </f:RenderField>  
                                 <f:RenderField Width="50px" ColumnID="Frequency" DataField="Frequency" SortField="Frequency" 
                                     FieldType="Double"  HeaderText="频次" HeaderTextAlign="Center" TextAlign="Right">
                                </f:RenderField>
                                 <f:RenderField Width="90px" ColumnID="IsRiskOwnerName" DataField="IsRiskOwnerName" SortField="IsRiskOwnerName"
                                     FieldType="String" HeaderText="巡检类型" HeaderTextAlign="Center" TextAlign="Left" >
                                </f:RenderField> 
                                <f:CheckBoxField Width="75px" RenderAsStaticField="true" TextAlign="Center"  DataField="IsUsed" HeaderText="启用" />
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
                                    <f:ListItem Text="15" Value="15" />
                                    <f:ListItem Text="20" Value="20" />
                                    <f:ListItem Text="25" Value="25" />
                                    <f:ListItem Text="30" Value="30" />
                                    <f:ListItem Text="100" Value="100" />
                                    <f:ListItem Text="所有行" Value="100000" />
                                </f:DropDownList>
                            </PageItems>
                        </f:Grid>                      
                    </Items>
                </f:Panel>               
            </Items>
        </f:Panel>    
    </form>    
    <script type="text/javascript">  
        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
           // F(menu2ID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }

        function reloadGrid() {
            __doPostBack(null, 'reloadGrid');
        }

         function onGridDataLoad(event) {
            this.mergeColumns(['InstallationName']);
            this.mergeColumns(['TaskActivity']);
        }
    </script>
</body>
</html>

