<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LimitedSpaceView.aspx.cs"
    Inherits="FineUIPro.Web.License.LimitedSpaceView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>受限空间安全作业证</title>
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
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="true" ShowHeader="false" AutoScroll="true" TitleAlign="Center"
        BodyPadding="5px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right"
        EnableTableStyle="true">
        <Rows>
            <f:FormRow ColumnWidths="40% 30% 30%">
                <Items>
                    <f:TextBox ID="txtApplyUnit" runat="server" Label="申请单位" Readonly="true" LabelWidth="160px">
                    </f:TextBox>
                    <f:TextBox ID="txtApplyManName" runat="server" Label="申请人" Readonly="true" LabelWidth="180px">
                    </f:TextBox>
                    <f:TextBox ID="txtLicenseCode" runat="server" Label="作业证编号" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="40% 60%">
                <Items>
                    <f:TextBox ID="txtLimitedSpaceUnitName" runat="server" Label="受限空间所属单位" Readonly="true"
                        LabelWidth="160px">
                    </f:TextBox>
                    <f:TextBox ID="txtLimitedSpaceName" runat="server" Label="受限空间名称" Readonly="true"
                        LabelWidth="180px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="40% 60%">
                <Items>
                    <f:TextBox ID="txtJobContent" runat="server" Label="作业内容" Readonly="true" LabelWidth="160px">
                    </f:TextBox>
                    <f:TextBox ID="txtMedium" runat="server" Label="受限空间内原有介质名称" Readonly="true" LabelWidth="180px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtJobTime" runat="server" Label="作业时间" Readonly="true" LabelWidth="160px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="40% 30% 30%">
                <Items>
                    <f:TextBox ID="txtJobUnitHead" runat="server" Label="作业单位负责人" Readonly="true" LabelWidth="160px">
                    </f:TextBox>
                    <f:TextBox ID="txtJobMan" runat="server" Label="作业人" Readonly="true" LabelWidth="180px">
                    </f:TextBox>
                    <f:TextBox ID="txtGuardian" runat="server" Label="监护人(工艺和设备各一人)" Readonly="true"
                        LabelWidth="210px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtOtherSpecial" runat="server" Label="涉及的其他特殊作业" Readonly="true"
                        LabelWidth="160px" EmptyText="无">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtHAZName" runat="server" Label="危害因素辨识" Readonly="true" LabelWidth="160px" Height="40px" EmptyText="无">
                    </f:TextArea>
                </Items>
            </f:FormRow>
            <%--<f:FormRow>
                <Items>
                    <f:Form ID="Form7" ShowBorder="true" ShowHeader="true" AutoScroll="true" TitleAlign="Left"
                        BodyPadding="5px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right"
                        Title="分析">
                        <Rows>
                            <f:FormRow >
                                <Items>
                                    <f:Label ID="Label20" runat="server" Text="分析项目">
                                    </f:Label>
                                    <f:Label ID="Label21" runat="server" Text="有毒有害介质">
                                    </f:Label>
                                    <f:Label ID="Label22" runat="server" Text="可燃气">
                                    </f:Label>
                                    <f:Label ID="Label1" runat="server" Text="氧含量">
                                    </f:Label>
                                    <f:Label ID="Label2" runat="server" Text="时间">
                                    </f:Label>
                                    <f:Label ID="Label3" runat="server" Text="部位">
                                    </f:Label>
                                    <f:Label ID="Label4" runat="server" Text="分析人">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                </Items>
            </f:FormRow>--%>
            <f:FormRow ColumnWidths="10% 60% 30%">
                <Items>
                    <f:Label ID="序号" runat="server" Text="序号" Readonly="true">
                    </f:Label>
                    <f:Label ID="安全措施" runat="server" Text="安全措施" Readonly="true">
                    </f:Label>
                    <f:Label ID="确认人" runat="server" Text="确认人（受限空间所属单位班长）" Readonly="true">
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow  ColumnWidths="10% 60% 30%">
                <Items>
                    <f:CheckBox ID="txtSIsUsed1" runat="server" Readonly="true" Text="1">
                    </f:CheckBox>
                    <f:TextBox ID="txtSafetyMeasures1" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtMeasuresManName1" runat="server" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow  ColumnWidths="10% 60% 30%">
                <Items>
                    <f:CheckBox ID="txtSIsUsed2" runat="server" Readonly="true" Text="2">
                    </f:CheckBox>
                    <f:TextBox ID="txtSafetyMeasures2" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtMeasuresManName2" runat="server" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow  ColumnWidths="10% 60% 30%">
                <Items>
                    <f:CheckBox ID="txtSIsUsed3" runat="server" Readonly="true" Text="3">
                    </f:CheckBox>
                    <f:TextBox ID="txtSafetyMeasures3" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtMeasuresManName3" runat="server" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow  ColumnWidths="10% 60% 30%">
                <Items>
                    <f:CheckBox ID="txtSIsUsed4" runat="server" Readonly="true" Text="4">
                    </f:CheckBox>
                    <f:TextBox ID="txtSafetyMeasures4" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtMeasuresManName4" runat="server" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow  ColumnWidths="10% 60% 30%">
                <Items>
                    <f:CheckBox ID="txtSIsUsed5" runat="server" Readonly="true" Text="5">
                    </f:CheckBox>
                    <f:TextBox ID="txtSafetyMeasures5" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtMeasuresManName5" runat="server" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow  ColumnWidths="10% 60% 30%">
                <Items>
                    <f:CheckBox ID="txtSIsUsed6" runat="server" Readonly="true" Text="6">
                    </f:CheckBox>
                    <f:TextBox ID="txtSafetyMeasures6" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtMeasuresManName6" runat="server" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow  ColumnWidths="10% 60% 30%">
                <Items>
                    <f:CheckBox ID="txtSIsUsed7" runat="server" Readonly="true" Text="7">
                    </f:CheckBox>
                    <f:TextBox ID="txtSafetyMeasures7" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtMeasuresManName7" runat="server" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow  ColumnWidths="10% 60% 30%">
                <Items>
                    <f:CheckBox ID="txtSIsUsed8" runat="server" Readonly="true" Text="8">
                    </f:CheckBox>
                    <f:TextBox ID="txtSafetyMeasures8" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtMeasuresManName8" runat="server" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow  ColumnWidths="10% 60% 30%">
                <Items>
                    <f:CheckBox ID="txtSIsUsed9" runat="server" Readonly="true" Text="8">
                    </f:CheckBox>
                    <f:TextBox ID="txtSafetyMeasures9" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtMeasuresManName9" runat="server" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow  ColumnWidths="10% 60% 30%">
                <Items>
                    <f:CheckBox ID="txtSIsUsed10" runat="server" Readonly="true" Text="8">
                    </f:CheckBox>
                    <f:TextBox ID="txtSafetyMeasures10" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtMeasuresManName10" runat="server" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow  ColumnWidths="10% 60% 30%">
                <Items>
                    <f:CheckBox ID="txtSIsUsed11" runat="server" Readonly="true" Text="8">
                    </f:CheckBox>
                    <f:TextBox ID="txtSafetyMeasures11" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtMeasuresManName11" runat="server" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtSafeEduManName" runat="server" Label="实施安全教育人" Readonly="true"
                        LabelWidth="130px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Form ID="Form2" ShowBorder="true" ShowHeader="true" AutoScroll="true" TitleAlign="Left"
                        BodyPadding="5px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right"
                        Title="申请单位意见">
                        <Rows>
                            <f:FormRow>
                                <Items>
                                    <f:TextArea ID="txtApplyUnitOpinion" runat="server" Readonly="true" Height="50px">
                                    </f:TextArea>
                                </Items>
                            </f:FormRow>
                            <f:FormRow ColumnWidths="40% 30% 30%">
                                <Items>
                                    <f:Label ID="Label6" runat="server">
                                    </f:Label>
                                    <f:Label ID="txtApplyUnitManName" runat="server" Label="签字">
                                    </f:Label>
                                    <f:Label ID="txtApplyUnitTime" runat="server" Text="年 月 日">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Form ID="Form3" ShowBorder="true" ShowHeader="true" AutoScroll="true" TitleAlign="Left"
                        BodyPadding="5px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right"
                        Title="审批单位意见">
                        <Rows>
                            <f:FormRow>
                                <Items>
                                    <f:TextArea ID="txtApproveUnitOpinion" runat="server" Readonly="true" Height="50px">
                                    </f:TextArea>
                                </Items>
                            </f:FormRow>
                            <f:FormRow ColumnWidths="40% 30% 30%">
                                <Items>
                                    <f:Label ID="Label9" runat="server">
                                    </f:Label>
                                    <f:Label ID="txtApproveUnitManName" runat="server" Label="签字">
                                    </f:Label>
                                    <f:Label ID="txtApproveUnitTime" runat="server" Text="年 月 日">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Form ID="Form4" ShowBorder="true" ShowHeader="true" AutoScroll="true" TitleAlign="Left"
                        BodyPadding="5px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right"
                        Title="完工验收">
                        <Rows>
                            <f:FormRow>
                                <Items>
                                    <f:TextArea ID="txtAcceptanceOpinion" runat="server" Readonly="true" Height="50px">
                                    </f:TextArea>
                                </Items>
                            </f:FormRow>
                            <f:FormRow ColumnWidths="40% 30% 30%">
                                <Items>
                                    <f:Label ID="txtAcceptanceJobMan" runat="server" Label="作业单位签字" LabelWidth="120px">
                                    </f:Label>
                                    <f:Label ID="txtAcceptanceOperationMan" runat="server" Label="生产单位签字" LabelWidth="120px">
                                    </f:Label>
                                    <f:Label ID="txtAcceptanceTime" runat="server" Text="年 月 日">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                </Items>
            </f:FormRow>
        </Rows>
    </f:Form>
    </form>
</body>
</html>
