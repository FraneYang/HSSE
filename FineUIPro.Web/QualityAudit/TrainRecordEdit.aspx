<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrainRecordEdit.aspx.cs"
    Inherits="FineUIPro.Web.QualityAudit.TrainRecordEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑人员培训</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        Layout="VBox" BodyPadding="10px" runat="server" RedStarPosition="BeforeText"
        LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtTrainingCode" runat="server" Label="培训编号" LabelAlign="Right" Required="true"
                        ShowRedStar="true" MaxLength="50">
                    </f:TextBox>
                    <f:TextBox ID="txtTrainTitle" runat="server" Label="标题" LabelAlign="Right" Required="true"
                        ShowRedStar="true" MaxLength="200">
                    </f:TextBox>
                    <f:DropDownList ID="drpTrainTypeId" runat="server" Label="培训类型" LabelAlign="Right"
                        Required="true" ShowRedStar="true">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DatePicker ID="txtStartDate" runat="server" Label="开始时间" LabelAlign="Right" EnableEdit="false">
                    </f:DatePicker>
                    <f:DatePicker ID="txtEndDate" runat="server" Label="结束时间" LabelAlign="Right" EnableEdit="false">
                    </f:DatePicker>
                    <f:NumberBox ID="txtTrainPersonNum" NoDecimal="true" NoNegative="true" MinValue="0"
                        Readonly="true" runat="server" Label="培训人数">
                    </f:NumberBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtTeachHour" NoDecimal="false" NoNegative="true" MaxValue="100"
                        DecimalPrecision="1" MinValue="0" runat="server" Label="学时" ShowRedStar="true"
                        Required="true" LabelAlign="Right">
                    </f:NumberBox>
                    <f:TextBox ID="txtTeachMan" runat="server" Label="授课人" MaxLength="50" LabelAlign="Right">
                    </f:TextBox>
                    <f:TextBox ID="txtTeachAddress" runat="server" Label="培训地点" MaxLength="100" LabelAlign="Right">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpUnits" runat="server" Label="培训单位" EnableCheckBoxSelect="true"
                        EnableMultiSelect="true">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtTrainContent" runat="server" Label="培训内容" LabelAlign="Right" MaxLength="500">
                    </f:TextArea>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtRemark" runat="server" Label="备注" LabelAlign="Right" MaxLength="150">
                    </f:TextBox>
                </Items>
            </f:FormRow>
             <f:FormRow>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" runat="server" ClicksToEdit="1" AllowCellEditing="true"
                        DataIDField="TrainDetailId" DataKeyNames="TrainDetailId" EnableMultiSelect="false"
                        ShowGridHeader="true" Height="220px" EnableColumnLines="true">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Right">
                                <Items>
                                    <f:Button ID="btnSelect" Icon="SystemSearch" runat="server" ToolTip="选择培训人员" ValidateForms="SimpleForm1"
                                        OnClick="btnSelect_Click" Hidden="true">
                                    </f:Button>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Columns>
                            <f:RowNumberField HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center" />
                            <f:RenderField Width="180px" ColumnID="UnitName" DataField="UnitName" SortField="UnitName" ExpandUnusedSpace="true"
                                FieldType="String" HeaderText="单位" TextAlign="Left" HeaderTextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="UserName" DataField="UserName" SortField="UserName"
                                FieldType="String" HeaderText="培训人员" TextAlign="Left" HeaderTextAlign="Center">
                            </f:RenderField>
                            <f:TemplateField Width="150px" HeaderText="考核结果" HeaderTextAlign="Center" TextAlign="Center">
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpCheckResult" runat="server" Height="23px" SelectedValue='<%# Eval("CheckResult")==null?"True":Eval("CheckResult") %>'
                                        Style="border: 0px;">
                                        <asp:ListItem Value="True">通过</asp:ListItem>
                                        <asp:ListItem Value="False">未通过</asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:RenderField Width="120px" ColumnID="CheckScore" DataField="CheckScore" FieldType="Double"
                                HeaderText="考试成绩" HeaderTextAlign="Center" TextAlign="Left">
                                <Editor>
                                    <f:NumberBox ID="txtCheckScore" NoDecimal="false" NoNegative="true" MinValue="0"
                                        DecimalPrecision="1" runat="server" Required="true">
                                    </f:NumberBox>
                                </Editor>
                            </f:RenderField>
                        </Columns>
                        <Listeners>
                            <f:Listener Event="dataload" Handler="onGridDataLoad" />
                            <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
                        </Listeners>
                    </f:Grid>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnAttachUrl" Text="附件" ToolTip="附件上传及查看" Icon="TableCell" runat="server"
                        OnClick="btnAttachUrl_Click" ValidateForms="SimpleForm1" MarginLeft="5px">
                    </f:Button>
                    <f:ToolbarFill ID="ToolbarFill1" runat="server">
                    </f:ToolbarFill>
                    <f:Button ID="btnSave" Icon="SystemSave" ToolTip="保存" runat="server" ValidateForms="SimpleForm1"
                        Hidden="true" OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnSubmit" Icon="SystemSaveNew" ToolTip="提交" runat="server" ValidateForms="SimpleForm1"
                        Hidden="true" OnClick="btnSubmit_Click">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    <f:Window ID="Window1" Title="选择培训人员" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Top" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="900px" Height="520px">
    </f:Window>
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
            Icon="Delete" ConfirmText="确定删除当前数据？" ConfirmTarget="Top" runat="server" Text="删除">
        </f:MenuButton>
    </f:Menu>
    </form>    
    <script type="text/javascript">
        var menuID = '<%= Menu1.ClientID %>';

        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
            F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }

        function onGridDataLoad(event) {
            this.mergeColumns(['UnitName']);
        }
    </script>
</body>
</html>
