<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkPost.aspx.cs" Inherits="FineUIPro.Web.BaseInfo.WorkPost" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>岗位信息</title>
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
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" Title="岗位信息" ShowHeader="false"
        Layout="HBox">
        <Items>
            <f:Grid ID="Grid1" Title="岗位信息" ShowHeader="false" EnableCollapse="true" PageSize="15"
                EnableColumnLines="true" ShowBorder="true" AllowPaging="true" IsDatabasePaging="true"
                runat="server" Width="810px" DataKeyNames="WorkPostId" DataIDField="WorkPostId" AllowSorting="true"
                SortField="WorkPostCode" SortDirection="ASC"
                OnPageIndexChange="Grid1_PageIndexChange" AllowFilters="true" OnFilterChange="Grid1_FilterChange"
                EnableTextSelection="True">
                <Columns>
                    <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="45px" HeaderTextAlign="Center"
                        TextAlign="Center" />
                    <f:RenderField Width="80px" ColumnID="WorkPostCode" DataField="WorkPostCode" FieldType="String"
                        HeaderText="编号" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="WorkPostName" DataField="WorkPostName" FieldType="String"
                        HeaderText="岗位名称" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:TemplateField Width="110px" HeaderText="类型" HeaderTextAlign="Center" TextAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# ConvertPostType(Eval("PostType")) %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>  
                    <f:CheckBoxField Width="70px" RenderAsStaticField="true" TextAlign="Center"  
                        DataField="IsAuditFlow" HeaderText="审批" /> 
                    <f:CheckBoxField Width="80px" RenderAsStaticField="true" TextAlign="Center"  
                        DataField="IsShowChart" HeaderText="隐患图表" />
                     <f:RenderField Width="120px" ColumnID="RiskLevelName" DataField="RiskLevelName" FieldType="String"
                        HeaderText="巡检级别" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                     <f:RenderField Width="80px" ColumnID="Frequency" DataField="Frequency" FieldType="Int"
                        HeaderText="巡检频率</br>(天/次)" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>               
                    <f:RenderField Width="100px" ColumnID="Remark" DataField="Remark" FieldType="String" Hidden="true"
                        ExpandUnusedSpace="true" HeaderText="备注" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="1px" ColumnID="PostType" DataField="PostType" FieldType="String"
                        HeaderText="类型" Hidden="true" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="1px" ColumnID="IsAuditFlow" DataField="IsAuditFlow" FieldType="Boolean"
                        HeaderText="审批" Hidden="true" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                     <f:RenderField Width="1px" ColumnID="IsShowChart" DataField="IsShowChart" FieldType="Boolean"
                        HeaderText="隐患图表" Hidden="true" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="1px" ColumnID="RiskLevelId" DataField="RiskLevelId" FieldType="String"
                        HeaderText="巡检级别" Hidden="true" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>                    
                </Columns>
                <Listeners>
                    <f:Listener Event="rowselect" Handler="onGridRowSelect" />
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
                        <f:ListItem Text="所有行" Value="100000" />
                    </f:DropDownList>
                </PageItems>
            </f:Grid>
            <f:SimpleForm ID="SimpleForm1" runat="server" ShowBorder="true" ShowHeader="false"
                LabelWidth="80px" BodyPadding="5px" Width="330px" EnableCollapse="true" Collapsed="true">
                <Items>
                    <f:HiddenField ID="hfFormID" runat="server">
                    </f:HiddenField>
                    <f:TextBox ID="txtWorkPostCode" Label="编号" ShowRedStar="true" Required="true" runat="server"
                        MaxLength="50" LabelAlign="right" AutoPostBack="true" OnTextChanged="TextBox_TextChanged"
                        LabelWidth="80px">
                    </f:TextBox>
                    <f:TextBox ID="txtWorkPostName" Label="名称" ShowRedStar="true" Required="true" runat="server"
                        MaxLength="100" LabelAlign="right" AutoPostBack="true" OnTextChanged="TextBox_TextChanged"
                        LabelWidth="80px">
                    </f:TextBox>
                    <f:DropDownList ID="drpPostType" runat="server" Label="类型" LabelAlign="Right" Required="true"
                        ShowRedStar="true" LabelWidth="80px">
                    </f:DropDownList>
                    <f:CheckBox ID="chkIsAuditFlow" MarginLeft="40px" runat="server" Text="参与审批" Checked="false">
                    </f:CheckBox>
                    <f:CheckBox ID="chkIsShowChart" MarginLeft="40px" runat="server" Text="隐患图表" Checked="false">
                    </f:CheckBox>  
                     <f:DropDownList ID="drpRiskLevel" runat="server" Label="巡检级别" EnableMultiSelect="true" 
                        EnableCheckBoxSelect="true" LabelAlign="Right" LabelWidth="80px">
                    </f:DropDownList>    
                     <f:NumberBox ID="txtFrequency" runat="server" Label="巡检频率</br>(天/次)" NoDecimal="true" NoNegative="true" 
                        LabelWidth="80px" LabelAlign="Right" MinValue="1"></f:NumberBox>           
                    <f:TextArea ID="txtRemark" runat="server" Label="备注" LabelAlign="right" MaxLength="200"
                        LabelWidth="80px" Hidden="true">
                    </f:TextArea>
                    <f:Label ID="lb1" runat="server" Text="岗位类型说明：" LabelWidth="120px">
                    </f:Label>
                    <f:Label ID="Label4" runat="server" Text="1、一般管理岗位和特种管理人员为管理人员；">
                    </f:Label>
                    <f:Label ID="Label2" runat="server" Text="2、若选中安全管理人员即为安全专职人员；">
                    </f:Label>
                    <f:Label ID="Label3" runat="server" Text="3、特种作业人员和一般作业岗位为单位作业人员。">
                    </f:Label>
                </Items>
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Center" runat="server">
                        <Items>
                        <f:Button ID="btnNew" Icon="Add" ToolTip="新增" EnablePostBack="false" Hidden="true"
                            runat="server">
                            <Listeners>
                                <f:Listener Event="click" Handler="onNewButtonClick" />
                            </Listeners>
                        </f:Button>
                        <f:Button ID="btnDelete" Enabled="false" ToolTip="删除" Icon="Delete" ConfirmText="确定删除当前数据？"
                            Hidden="true" OnClick="btnDelete_Click" runat="server">
                        </f:Button>                       
                        <f:Button ID="btnSave" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm1"
                            Hidden="true" OnClick="btnSave_Click">
                        </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
            </f:SimpleForm>
        </Items>
    </f:Panel>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
            Hidden="true" runat="server" Text="编辑">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
            Hidden="true" ConfirmText="删除选中行？" ConfirmTarget="Top" runat="server" Text="删除">
        </f:MenuButton>
    </f:Menu>
    </form>
    <script type="text/javascript">
        function renderIsHsse(value) {
            return value == true ? '是' : '否';
        }
        var menuID = '<%= Menu1.ClientID %>';
        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
            F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }

        function reloadGrid() {
            __doPostBack(null, 'reloadGrid');
        }

        var gridClientID = '<%= Grid1.ClientID %>';
        var btnDeleteClientID = '<%= btnDelete.ClientID %>';
        var btnSaveClientID = '<%= btnSave.ClientID %>';   
        var formClientID = '<%= SimpleForm1.ClientID %>';
        var hfFormIDClientID = '<%= hfFormID.ClientID %>';
        var txtCodeClientID = '<%= txtWorkPostCode.ClientID %>';
        var txtNameClientID = '<%= txtWorkPostName.ClientID %>';
        var drpPostTypeClientID = '<%= drpPostType.ClientID %>';
        var chkIsAuditFlowClientID = '<%= chkIsAuditFlow.ClientID %>';
        var chkIsShowChartClientID = '<%= chkIsShowChart.ClientID %>';
        var drpRiskLevelClientID = '<%= drpRiskLevel.ClientID %>';
        var txtRemarkClientID = '<%=txtRemark.ClientID %>';
        var txtFrequencyClientID = '<%=txtFrequency.ClientID %>';

        function onGridRowSelect(event, rowId) {
            var grid = F(gridClientID);

            // 启用删除按钮
            F(btnDeleteClientID).enable();  
            // 当前行数据
            var rowValue = grid.getRowValue(rowId);

            // 使用当前行数据填充表单字段
            F(hfFormIDClientID).setValue(rowId);
            F(txtCodeClientID).setValue(rowValue['WorkPostCode']);
            F(txtNameClientID).setValue(rowValue['WorkPostName']);
            F(drpPostTypeClientID).setValue(rowValue['PostType']);
            F(chkIsAuditFlowClientID).setValue(rowValue['IsAuditFlow']);
            F(chkIsShowChartClientID).setValue(rowValue['IsShowChart']);
            F(drpRiskLevelClientID).setValue(rowValue['RiskLevelId']);
            F(txtRemarkClientID).setValue(rowValue['Remark']);
            F(txtFrequencyClientID).setValue(rowValue['Frequency']);

            // 更新保存按钮文本
            //            F(btnSaveClientID).setText('保存数据（编辑）');
        }

        function onNewButtonClick() {
            // 重置表单字段
            F(formClientID).reset();
            // 清空表格选中行
            F(gridClientID).clearSelections();
            // 禁用删除按钮
            F(btnDeleteClientID).disable();

            // 更新保存按钮文本
            //            F(btnSaveClientID).setText('保存数据（新增）');
        }
    </script>
</body>
</html>
