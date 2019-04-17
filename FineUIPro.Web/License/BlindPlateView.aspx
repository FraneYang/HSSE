<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BlindPlateView.aspx.cs"
    Inherits="FineUIPro.Web.License.BlindPlateView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>盲板抽堵安全作业票</title>
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
            <f:FormRow ColumnWidths="75% 25%">
                <Items>
                    <f:Label ID="Label1" runat="server">
                    </f:Label>
                    <f:Label ID="lbStandard" runat="server" Text="执行标准：GB-30871-2014">
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtApplyUnit" runat="server" Label="申请单位" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                    <f:TextBox ID="txtApplyManName" runat="server" Label="申请人" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                    <f:TextBox ID="txtLicenseCode" runat="server" Label="作业证编号" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="16% 14% 14% 14% 14% 14% 14%">
                <Items>
                    <f:Label ID="Label2" runat="server" Text="设备管道名称">
                    </f:Label>
                    <f:Label ID="Label3" runat="server" Text="介质">
                    </f:Label>
                    <f:Label ID="Label4" runat="server" Text="温度（℃）">
                    </f:Label>
                    <f:Label ID="Label5" runat="server" Text="压力（MPa）">
                    </f:Label>
                    <f:Label ID="Label7" runat="server" Text="盲板材质">
                    </f:Label>
                    <f:Label ID="Label8" runat="server" Text="盲板规格">
                    </f:Label>
                    <f:Label ID="Label10" runat="server" Text="盲板编号">
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="16% 14% 14% 14% 14% 14% 14%">
                <Items>
                    <f:TextBox ID="txtEquipmentName" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtMedium" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtTemperature" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtPressure" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtMaterial" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtSpecification" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtNumber" runat="server" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="16% 28% 14% 14% 14% 14%">
                <Items>
                    <f:Label ID="Label11" runat="server" Text="盲板实施时间">
                    </f:Label>
                    <f:Label ID="Label13" runat="server" Text="盲板实施内容">
                    </f:Label>
                    <f:Label ID="Label14" runat="server" Text="作业人">
                    </f:Label>
                    <f:Label ID="Label16" runat="server" Text="工艺监护人">
                    </f:Label>
                    <f:Label ID="Label17" runat="server" Text="作业负责人">
                    </f:Label>
                    <f:Label ID="Label19" runat="server" Text="单位负责人">
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="16% 28% 14% 14% 14% 14%">
                <Items>
                    <f:TextBox ID="txtEffectiveDate" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:CheckBoxList ID="cbImplement" runat="server" Readonly="true">
                        <f:CheckItem Text="抽" Value="1"/>
                        <f:CheckItem Text="堵" Value="2"/>
                    </f:CheckBoxList>
                    <f:TextBox ID="txtJobMan" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtProcessGuardian" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtJobHead" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtUnitHead" runat="server" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtOtherSpecial" runat="server" Label="涉及的其他特殊作业" Readonly="true"
                        LabelWidth="160px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Form ID="Form7" ShowBorder="true" ShowHeader="true" AutoScroll="true" TitleAlign="Left"
                        BodyPadding="5px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right"
                        Title="盲板位置图及编号">
                        <Rows>
                            <f:FormRow>
                                <Items>
                                    <f:TextArea ID="txtNumbering" runat="server" Readonly="true" Height="50px">
                                    </f:TextArea>
                                </Items>
                            </f:FormRow>
                            <f:FormRow ColumnWidths="40% 30% 30%">
                                <Items>
                                    <f:Label ID="Label20" runat="server">
                                    </f:Label>
                                    <f:Label ID="txtCompileMan1" runat="server" Label="编制人">
                                    </f:Label>
                                    <f:Label ID="txtCompileDate1" runat="server" Text="年 月 日">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
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
            <f:FormRow ColumnWidths="10% 70% 20%">
                <Items>
                    <f:CheckBox ID="txtSIsUsed5" runat="server" Readonly="true" Text="5">
                    </f:CheckBox>
                    <f:TextBox ID="txtSafetyMeasures5" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtMeasuresManName5" runat="server" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="10% 70% 20%">
                <Items>
                    <f:CheckBox ID="txtSIsUsed6" runat="server" Readonly="true" Text="6">
                    </f:CheckBox>
                    <f:TextBox ID="txtSafetyMeasures6" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtMeasuresManName6" runat="server" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="10% 70% 20%">
                <Items>
                    <f:CheckBox ID="txtSIsUsed7" runat="server" Readonly="true" Text="7">
                    </f:CheckBox>
                    <f:TextBox ID="txtSafetyMeasures7" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtMeasuresManName7" runat="server" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="10% 70% 20%">
                <Items>
                    <f:CheckBox ID="txtSIsUsed8" runat="server" Readonly="true" Text="8">
                    </f:CheckBox>
                    <f:TextBox ID="txtSafetyMeasures8" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtMeasuresManName8" runat="server" Readonly="true">
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
                        Title="生产单位意见">
                        <Rows>
                            <f:FormRow>
                                <Items>
                                    <f:TextArea ID="txtProduceUnitOpinion" runat="server" Readonly="true" Height="50px">
                                    </f:TextArea>
                                </Items>
                            </f:FormRow>
                            <f:FormRow ColumnWidths="40% 30% 30%">
                                <Items>
                                    <f:Label ID="Label6" runat="server">
                                    </f:Label>
                                    <f:Label ID="txtProduceUnitManName" runat="server" Label="签字">
                                    </f:Label>
                                    <f:Label ID="txtProduceUnitManTime" runat="server" Text="年 月 日">
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
                        Title="盲板作业单位意见">
                        <Rows>
                            <f:FormRow>
                                <Items>
                                    <f:TextArea ID="txtOperationUnitOpinion" runat="server" Readonly="true" Height="50px">
                                    </f:TextArea>
                                </Items>
                            </f:FormRow>
                            <f:FormRow ColumnWidths="40% 30% 30%">
                                <Items>
                                    <f:Label ID="Label9" runat="server">
                                    </f:Label>
                                    <f:Label ID="txtOperationUnitManName" runat="server" Label="签字">
                                    </f:Label>
                                    <f:Label ID="txtOperationUnitManTime" runat="server" Text="年 月 日">
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
                        Title="生产部门意见">
                        <Rows>
                            <f:FormRow>
                                <Items>
                                    <f:TextArea ID="txtProductionDepOpinion" runat="server" Readonly="true" Height="50px">
                                    </f:TextArea>
                                </Items>
                            </f:FormRow>
                            <f:FormRow ColumnWidths="40% 30% 30%">
                                <Items>
                                    <f:Label ID="Label12" runat="server">
                                    </f:Label>
                                    <f:Label ID="txtProductionDepManName" runat="server" Label="签字">
                                    </f:Label>
                                    <f:Label ID="txtProductionDepManTime" runat="server" Text="年 月 日">
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
                        Title="盲板作业单位确认">
                        <Rows>
                            <f:FormRow>
                                <Items>
                                    <f:TextArea ID="txtAccOperationUnitOpinion" runat="server" Readonly="true" Height="50px">
                                    </f:TextArea>
                                </Items>
                            </f:FormRow>
                            <f:FormRow ColumnWidths="40% 30% 30%">
                                <Items>
                                    <f:Label ID="Label15" runat="server">
                                    </f:Label>
                                    <f:Label ID="txtAccOperationUnitManName" runat="server" Label="签字">
                                    </f:Label>
                                    <f:Label ID="txtAccOperationUnitManTime" runat="server" Text="年 月 日">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Form ID="Form6" ShowBorder="true" ShowHeader="true" AutoScroll="true" TitleAlign="Left"
                        BodyPadding="5px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right"
                        Title="生产单位确认">
                        <Rows>
                            <f:FormRow>
                                <Items>
                                    <f:TextArea ID="txtAccProduceUnitOpinion" runat="server" Readonly="true" Height="50px">
                                    </f:TextArea>
                                </Items>
                            </f:FormRow>
                            <f:FormRow ColumnWidths="40% 30% 30%">
                                <Items>
                                    <f:Label ID="Label18" runat="server">
                                    </f:Label>
                                    <f:Label ID="txtAccProduceUnitManName" runat="server" Label="签字">
                                    </f:Label>
                                    <f:Label ID="txtAccProduceUnitManTime" runat="server" Text="年 月 日">
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
