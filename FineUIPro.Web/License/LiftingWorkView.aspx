<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LiftingWorkView.aspx.cs"
    Inherits="FineUIPro.Web.License.LiftingWorkView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>吊装安全作业证</title>
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
                    <f:TextBox ID="txtJobPalce" runat="server" Label="吊装地点" Readonly="true" LabelWidth="200px">
                    </f:TextBox>
                    <f:TextBox ID="txtLiftingTools" runat="server" Label="吊装工具名称" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                    <f:TextBox ID="txtLicenseCode" runat="server" Label="作业证编号" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="70% 30%">
                <Items>
                    <f:TextBox ID="txtWorkerCertificate" runat="server" Label="吊装人员及特殊工种作业证号" Readonly="true"
                        LabelWidth="200px">
                    </f:TextBox>
                    <f:TextBox ID="txtGuardian" runat="server" Label="监护人" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="70% 30%">
                <Items>
                    <f:TextBox ID="txtCommandCertificate" runat="server" Label="吊装指挥及特殊工种作业证号" Readonly="true"
                        LabelWidth="200px">
                    </f:TextBox>
                    <f:TextBox ID="txtLiftingQuality" runat="server" Label="起吊重物质量(t)" Readonly="true"
                        LabelWidth="120px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:CheckBoxList ID="cbLiftingLevel" runat="server" Readonly="true" Label="吊装级别" LabelWidth="200px">
                        <f:CheckItem Text="质量m>100t" Value="1" />
                        <f:CheckItem Text="质量40t≤m≤100t" Value="2" />
                        <f:CheckItem Text="质量m<40t" Value="3" />
                    </f:CheckBoxList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtJobTime" runat="server" Label="作业时间" Readonly="true" LabelWidth="200px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtLiftingContent" runat="server" Label="吊装内容" Readonly="true" LabelWidth="200px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtHAZIDName" runat="server" Label="危害辨识" Readonly="true" LabelWidth="200px">
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
            <f:FormRow ColumnWidths="10% 70% 20%">
                <Items>
                    <f:CheckBox ID="txtSIsUsed9" runat="server" Readonly="true" Text="9">
                    </f:CheckBox>
                    <f:TextBox ID="txtSafetyMeasures9" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtMeasuresManName9" runat="server" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="10% 70% 20%">
                <Items>
                    <f:CheckBox ID="txtSIsUsed10" runat="server" Readonly="true" Text="10">
                    </f:CheckBox>
                    <f:TextBox ID="txtSafetyMeasures10" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtMeasuresManName10" runat="server" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="10% 70% 20%">
                <Items>
                    <f:CheckBox ID="txtSIsUsed11" runat="server" Readonly="true" Text="11">
                    </f:CheckBox>
                    <f:TextBox ID="txtSafetyMeasures11" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtMeasuresManName11" runat="server" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="10% 70% 20%">
                <Items>
                    <f:CheckBox ID="txtSIsUsed12" runat="server" Readonly="true" Text="12">
                    </f:CheckBox>
                    <f:TextBox ID="txtSafetyMeasures12" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtMeasuresManName12" runat="server" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="10% 70% 20%">
                <Items>
                    <f:CheckBox ID="txtSIsUsed13" runat="server" Readonly="true" Text="13">
                    </f:CheckBox>
                    <f:TextBox ID="txtSafetyMeasures13" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtMeasuresManName13" runat="server" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="10% 70% 20%">
                <Items>
                    <f:CheckBox ID="txtSIsUsed14" runat="server" Readonly="true" Text="14">
                    </f:CheckBox>
                    <f:TextBox ID="txtSafetyMeasures14" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtMeasuresManName14" runat="server" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="10% 70% 20%">
                <Items>
                    <f:CheckBox ID="txtSIsUsed15" runat="server" Readonly="true" Text="15">
                    </f:CheckBox>
                    <f:TextBox ID="txtSafetyMeasures15" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtMeasuresManName15" runat="server" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="10% 70% 20%">
                <Items>
                    <f:CheckBox ID="txtSIsUsed16" runat="server" Readonly="true" Text="16">
                    </f:CheckBox>
                    <f:TextBox ID="txtSafetyMeasures16" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtMeasuresManName16" runat="server" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="10% 70% 20%">
                <Items>
                    <f:CheckBox ID="txtSIsUsed17" runat="server" Readonly="true" Text="17">
                    </f:CheckBox>
                    <f:TextBox ID="txtSafetyMeasures17" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtMeasuresManName17" runat="server" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="10% 70% 20%">
                <Items>
                    <f:CheckBox ID="txtSIsUsed18" runat="server" Readonly="true" Text="18">
                    </f:CheckBox>
                    <f:TextBox ID="txtSafetyMeasures18" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtMeasuresManName18" runat="server" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="10% 70% 20%">
                <Items>
                    <f:CheckBox ID="txtSIsUsed19" runat="server" Readonly="true" Text="19">
                    </f:CheckBox>
                    <f:TextBox ID="txtSafetyMeasures19" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtMeasuresManName19" runat="server" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="10% 70% 20%">
                <Items>
                    <f:CheckBox ID="txtSIsUsed20" runat="server" Readonly="true" Text="20">
                    </f:CheckBox>
                    <f:TextBox ID="txtSafetyMeasures20" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtMeasuresManName20" runat="server" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="10% 70% 20%">
                <Items>
                    <f:CheckBox ID="txtSIsUsed21" runat="server" Readonly="true" Text="21">
                    </f:CheckBox>
                    <f:TextBox ID="txtSafetyMeasures21" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtMeasuresManName21" runat="server" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="10% 70% 20%">
                <Items>
                    <f:CheckBox ID="txtSIsUsed22" runat="server" Readonly="true" Text="22">
                    </f:CheckBox>
                    <f:TextBox ID="txtSafetyMeasures22" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtMeasuresManName22" runat="server" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="10% 70% 20%">
                <Items>
                    <f:CheckBox ID="txtSIsUsed23" runat="server" Readonly="true" Text="23">
                    </f:CheckBox>
                    <f:TextBox ID="txtSafetyMeasures23" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtMeasuresManName23" runat="server" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="10% 70% 20%">
                <Items>
                    <f:CheckBox ID="txtSIsUsed24" runat="server" Readonly="true" Text="24">
                    </f:CheckBox>
                    <f:TextBox ID="txtSafetyMeasures24" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtMeasuresManName24" runat="server" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="10% 70% 20%">
                <Items>
                    <f:CheckBox ID="txtSIsUsed25" runat="server" Readonly="true" Text="25">
                    </f:CheckBox>
                    <f:TextBox ID="txtSafetyMeasures25" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtMeasuresManName25" runat="server" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="10% 70% 20%">
                <Items>
                    <f:CheckBox ID="txtSIsUsed26" runat="server" Readonly="true" Text="26">
                    </f:CheckBox>
                    <f:TextBox ID="txtSafetyMeasures26" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtMeasuresManName26" runat="server" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="10% 70% 20%">
                <Items>
                    <f:CheckBox ID="txtSIsUsed27" runat="server" Readonly="true" Text="27">
                    </f:CheckBox>
                    <f:TextBox ID="txtSafetyMeasures27" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtMeasuresManName27" runat="server" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="10% 70% 20%">
                <Items>
                    <f:CheckBox ID="txtSIsUsed28" runat="server" Readonly="true" Text="28">
                    </f:CheckBox>
                    <f:TextBox ID="txtSafetyMeasures28" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtMeasuresManName28" runat="server" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="10% 70% 20%">
                <Items>
                    <f:CheckBox ID="txtSIsUsed29" runat="server" Readonly="true" Text="29">
                    </f:CheckBox>
                    <f:TextBox ID="txtSafetyMeasures29" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtMeasuresManName29" runat="server" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="10% 70% 20%">
                <Items>
                    <f:CheckBox ID="txtSIsUsed30" runat="server" Readonly="true" Text="30">
                    </f:CheckBox>
                    <f:TextBox ID="txtSafetyMeasures30" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtMeasuresManName30" runat="server" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="10% 70% 20%">
                <Items>
                    <f:CheckBox ID="txtSIsUsed31" runat="server" Readonly="true" Text="31">
                    </f:CheckBox>
                    <f:TextBox ID="txtSafetyMeasures31" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtMeasuresManName31" runat="server" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="10% 70% 20%">
                <Items>
                    <f:CheckBox ID="txtSIsUsed32" runat="server" Readonly="true" Text="32">
                    </f:CheckBox>
                    <f:TextBox ID="txtSafetyMeasures32" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtMeasuresManName32" runat="server" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="10% 70% 20%">
                <Items>
                    <f:CheckBox ID="txtSIsUsed33" runat="server" Readonly="true" Text="33">
                    </f:CheckBox>
                    <f:TextBox ID="txtSafetyMeasures33" runat="server" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtMeasuresManName33" runat="server" Readonly="true">
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
                    <f:TextBox ID="txtProduceDepManName" runat="server" Label="生产单位安全部门负责人（签字）" Readonly="true"
                        LabelWidth="220px">
                    </f:TextBox>
                    <f:TextBox ID="txtProduceUnitManName" runat="server" Label="生产单位负责人（签字）" Readonly="true"
                        LabelWidth="220px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtJobDepManName" runat="server" Label="作业单位安全部门负责人（签字）" Readonly="true"
                        LabelWidth="220px">
                    </f:TextBox>
                    <f:TextBox ID="txtJobUnitManName" runat="server" Label="作业单位负责人（签字）" Readonly="true"
                        LabelWidth="220px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Form ID="Form2" ShowBorder="true" ShowHeader="true" AutoScroll="true" TitleAlign="Left"
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
                                    <f:Label ID="Label6" runat="server">
                                    </f:Label>
                                    <f:Label ID="txtApprovalDepManName" runat="server" Label="签字">
                                    </f:Label>
                                    <f:Label ID="txtApprovalDepTime" runat="server" Text="年 月 日">
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
