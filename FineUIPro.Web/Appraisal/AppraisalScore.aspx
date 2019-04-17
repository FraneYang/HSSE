<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppraisalScore.aspx.cs" Inherits="FineUIPro.Web.Appraisal.AppraisalScore" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>人员测评</title>
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
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="人员测评记录" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="UserId" AllowCellEditing="true" EnableColumnLines="true"
                ClicksToEdit="2" DataIDField="UserId" AllowSorting="true" SortField="ToltalScore"
                SortDirection="DESC" OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true" 
                PageSize="15" OnPageIndexChange="Grid1_PageIndexChange" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" Position="Top" runat="server">
                         <Items>                                                      
                            <f:TextBox runat="server" Label="查询" ID="txtName" EmptyText="输入查询条件" AutoPostBack="true"
                                OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="80px" LabelAlign="Right">
                            </f:TextBox>   
                            <f:DropDownList ID="drpYear" runat="server" Label="年" ForceSelection="false" 
                                AutoPostBack="true" OnSelectedIndexChanged="TextBox_TextChanged"  Width="200px" LabelWidth="50px">
                            </f:DropDownList>
                            <f:DropDownList ID="drpMonth" runat="server" Label="月" ForceSelection="false" 
                                AutoPostBack="true" OnSelectedIndexChanged="TextBox_TextChanged"  Width="200px" LabelWidth="50px">
                            </f:DropDownList>
                              <f:DropDownList ID="drpDepart" runat="server" Label="部门" ForceSelection="false" 
                                AutoPostBack="true" OnSelectedIndexChanged="drpDepart_TextChanged"  Width="200px" LabelWidth="50px">
                            </f:DropDownList>
                              <f:DropDownList ID="drpInstallation" runat="server" Label="装置" ForceSelection="false" 
                                AutoPostBack="true" OnSelectedIndexChanged="TextBox_TextChanged"  Width="200px" LabelWidth="50px">
                            </f:DropDownList>
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
                      <f:RenderField Width="160px" ColumnID="DepartName" DataField="DepartName" FieldType="String"
                        HeaderText="部门" HeaderTextAlign="Center" TextAlign="Left" SortField="DepartName">
                    </f:RenderField>
                     <f:RenderField Width="180px" ColumnID="InstallationName" DataField="InstallationName" FieldType="String"
                        HeaderText="装置" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true" SortField="InstallationName">
                    </f:RenderField> 
                     <f:RenderField Width="160px" ColumnID="UserName" DataField="UserName" FieldType="String"
                        HeaderText="姓名" HeaderTextAlign="Center" TextAlign="Left" SortField="UserName">
                    </f:RenderField>
                    <%--<f:RenderField Width="160px" ColumnID="OperationTime" DataField="OperationTime" FieldType="String"
                        HeaderText="月份" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>--%>  
                    <f:RenderField Width="120px" ColumnID="ToltalScore" DataField="ToltalScore" FieldType="Double"
                        HeaderText="业务" HeaderTextAlign="Center" TextAlign="Left" >
                    </f:RenderField> 
                    <f:RenderField Width="100px" ColumnID="TestScores" DataField="TestScores" FieldType="Double"
                        HeaderText="考试成绩" HeaderTextAlign="Center" TextAlign="Left" SortField="TestScores">
                    </f:RenderField> 
                    <f:RenderField Width="120px" ColumnID="PersonScore" DataField="PersonScore" FieldType="Double"
                        HeaderText="人员测评(扣分)" HeaderTextAlign="Center" TextAlign="Left" SortField="PersonScore">
                    </f:RenderField> 
                    <f:RenderField Width="120px" ColumnID="PersonGetScore" DataField="PersonGetScore" FieldType="Double"
                        HeaderText="人员测评(得分)" HeaderTextAlign="Center" TextAlign="Left" SortField="PersonGetScore">
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
    </form>
    <script type="text/jscript">       
        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
            //F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }

        function reloadGrid() {
            __doPostBack(null, 'reloadGrid');
        }
    </script>
</body>
</html>
