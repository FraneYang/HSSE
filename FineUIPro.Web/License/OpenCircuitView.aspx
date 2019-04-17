<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OpenCircuitView.aspx.cs"
    Inherits="FineUIPro.Web.License.OpenCircuitView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>断路安全作业证</title>
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
    <f:Form ID="SimpleForm1" ShowBorder="true" ShowHeader="true" AutoScroll="true" TitleAlign="Center"
        BodyPadding="5px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right"
        EnableTableStyle="true">
        <Rows>
            <f:FormRow ColumnWidths="40% 30% 30%">
                <Items>
                    <f:TextBox ID="txtApplyUnit" runat="server" Label="申请单位(施工所在车间)" Readonly="true"
                        LabelWidth="180px">
                    </f:TextBox>
                    <f:TextBox ID="txtApplyManName" runat="server" Label="申请人" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                    <f:TextBox ID="txtLicenseCode" runat="server" Label="作业证编号" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="70% 30%">
                <Items>
                    <f:TextBox ID="txtJobUnit" runat="server" Label="作业单位（施工单位）" Readonly="true" LabelWidth="180px">
                    </f:TextBox>
                    <f:TextBox ID="txtJobHead" runat="server" Label="作业单位负责人" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtRelatedUnitDep" runat="server" Label="涉及相关单位（部门）" Readonly="true"
                        LabelWidth="180px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtOpenCircuitCause" runat="server" Label="断路原因" Readonly="true" LabelWidth="180px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtOpenCircuitTime" runat="server" Label="断路时间" Readonly="true" LabelWidth="180px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Form ID="Form6" ShowBorder="true" ShowHeader="true" AutoScroll="true" TitleAlign="Left"
                        BodyPadding="5px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right"
                        Title="断路地段示意图及相关说明">
                        <Rows>
                            <f:FormRow>
                                <Items>
                                    <f:TextArea ID="txtJobContent" runat="server" Readonly="true" Height="50px">
                                    </f:TextArea>
                                </Items>
                            </f:FormRow>
                            <f:FormRow ColumnWidths="40% 30% 30%">
                                <Items>
                                    <f:Label ID="Label8" runat="server">
                                    </f:Label>
                                    <f:Label ID="txtJobContentName" runat="server" Label="签字">
                                    </f:Label>
                                    <f:Label ID="txtJobContentTime" runat="server" Text="年 月 日">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtHAZIDName" runat="server" Label="危害辨识" Readonly="true" LabelWidth="160px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="10% 70% 20%">
                <Items>
                    <f:Label ID="序号" runat="server" Text="序号" Readonly="true">
                    </f:Label>
                    <f:Label ID="安全措施" runat="server" Text="安全措施" Readonly="true">
                    </f:Label>
                    <f:Label ID="确认人" runat="server" Text="确认人" Readonly="true">
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="10% 70% 20%">
                <Items>
                    <f:CheckBox ID="txtSIsUsed1" runat="server" Readonly="true" Text="1">
                    </f:CheckBox>
                    <f:TextBox ID="txtSafetyMeasures1" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtMeasuresManName1" runat="server" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="10% 70% 20%">
                <Items>
                    <f:CheckBox ID="txtSIsUsed2" runat="server" Readonly="true" Text="2">
                    </f:CheckBox>
                    <f:TextBox ID="txtSafetyMeasures2" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtMeasuresManName2" runat="server" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="10% 70% 20%">
                <Items>
                    <f:CheckBox ID="txtSIsUsed3" runat="server" Readonly="true" Text="3">
                    </f:CheckBox>
                    <f:TextBox ID="txtSafetyMeasures3" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtMeasuresManName3" runat="server" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="10% 70% 20%">
                <Items>
                    <f:CheckBox ID="txtSIsUsed4" runat="server" Readonly="true" Text="4">
                    </f:CheckBox>
                    <f:TextBox ID="txtSafetyMeasures4" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtMeasuresManName4" runat="server" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtSafeEduManName" runat="server" Label="实施安全教育人" Readonly="true"
                        LabelWidth="120px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Form ID="Form2" ShowBorder="true" ShowHeader="true" AutoScroll="true" TitleAlign="Left"
                        BodyPadding="5px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right"
                        Title="申请单位意见（施工所在车间）">
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
                                    <f:Label ID="txtApplyUnitMan" runat="server" Label="签字">
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
                        Title="作业单位意见（施工单位）">
                        <Rows>
                            <f:FormRow>
                                <Items>
                                    <f:TextArea ID="txtJobUnitOpinion" runat="server" Readonly="true" Height="50px">
                                    </f:TextArea>
                                </Items>
                            </f:FormRow>
                            <f:FormRow ColumnWidths="40% 30% 30%">
                                <Items>
                                    <f:Label ID="Label9" runat="server">
                                    </f:Label>
                                    <f:Label ID="txtJobUnitMan" runat="server" Label="签字">
                                    </f:Label>
                                    <f:Label ID="txtJobUnitTime" runat="server" Text="年 月 日">
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
                        Title="审批部门意见">
                        <Rows>
                            <f:FormRow>
                                <Items>
                                    <f:TextArea ID="txtApprovalDepOpinion" runat="server" Readonly="true" Height="50px">
                                    </f:TextArea>
                                </Items>
                            </f:FormRow>
                            <f:FormRow ColumnWidths="40% 30% 30%">
                                <Items>
                                    <f:Label ID="Label1" runat="server">
                                    </f:Label>
                                    <f:Label ID="txtApprovalDepMan" runat="server" Label="签字">
                                    </f:Label>
                                    <f:Label ID="txtApprovalDepTime" runat="server" Text="年 月 日">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Form ID="Form5" ShowBorder="true" ShowHeader="true" AutoScroll="true" TitleAlign="Left"
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
                                    <f:Label ID="Label4" runat="server">
                                    </f:Label>
                                    <f:Label ID="txtAcceptanceMan" runat="server" Label="签字">
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
