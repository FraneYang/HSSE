<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EquipmentQualityEdit.aspx.cs"
    Inherits="FineUIPro.Web.QualityAudit.EquipmentQualityEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑特种资质设备</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        Layout="VBox" BodyPadding="10px" runat="server" RedStarPosition="BeforeText"
        LabelAlign="Right">
        <Rows>
            <f:FormRow ColumnWidths="50% 50%">
                <Items>
                    <f:DropDownList ID="drpUnitId" runat="server" Label="单位名称" LabelAlign="Right" LabelWidth="140px"
                        AutoPostBack="true" OnSelectedIndexChanged="drpUnitId_SelectedIndexChanged">
                    </f:DropDownList>
                    <f:DropDownList ID="drpInstallationId" runat="server" Label="装置/科室" LabelAlign="Right"
                        LabelWidth="140px">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpEuipmentId" runat="server" Label="设备" LabelAlign="Right" LabelWidth="140px">
                    </f:DropDownList>
                    <f:TextBox ID="txtEquipmentQualityCode" runat="server" Label="特种设备资质编号" LabelAlign="Right"
                        Required="true" ShowRedStar="true" LabelWidth="140px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtEquipmentQualityName" runat="server" Label="特种设备资质名称" LabelAlign="Right"
                        LabelWidth="140px">
                    </f:TextBox>
                    <f:TextBox ID="txtSizeModel" runat="server" Label="规格型号" LabelAlign="Right" LabelWidth="140px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtFactoryCode" runat="server" Label="出厂编号" LabelAlign="Right" LabelWidth="140px">
                    </f:TextBox>
                    <f:TextBox ID="txtCertificateCode" runat="server" Label="合格证编号" LabelAlign="Right"
                        LabelWidth="140px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DatePicker ID="txtCheckDate" runat="server" Label="检验时间" LabelAlign="Right" LabelWidth="140px"
                        EnableEdit="false">
                    </f:DatePicker>
                    <f:DatePicker ID="txtLimitDate" runat="server" Label="有效期" LabelAlign="Right" LabelWidth="140px"
                        EnableEdit="false">
                    </f:DatePicker>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DatePicker ID="txtInDate" runat="server" Label="入场时间" LabelAlign="Right" LabelWidth="140px"
                        EnableEdit="false">
                    </f:DatePicker>
                    <f:DatePicker ID="txtOutDate" runat="server" Label="出场时间" LabelAlign="Right" LabelWidth="140px"
                        EnableEdit="false">
                    </f:DatePicker>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtApprovalPerson" runat="server" Label="审批人" LabelAlign="Right" LabelWidth="140px">
                    </f:TextBox>
                    <f:TextBox ID="txtCarNum" runat="server" Label="车辆编号" LabelAlign="Right" LabelWidth="140px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtRemark" runat="server" Label="备注" LabelAlign="Right" LabelWidth="140px">
                    </f:TextArea>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnAttachUrl" Text="证件扫描件" ToolTip="附件上传及查看" Icon="TableCell" runat="server"
                        OnClick="btnAttachUrl_Click" ValidateForms="SimpleForm1" MarginLeft="5px">
                    </f:Button>
                    <f:ToolbarFill ID="ToolbarFill1" runat="server">
                    </f:ToolbarFill>
                    <f:Button ID="btnSave" Icon="SystemSaveNew" ToolTip="保存" runat="server" ValidateForms="SimpleForm1"
                        Hidden="true" OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    </form>
</body>
</html>
