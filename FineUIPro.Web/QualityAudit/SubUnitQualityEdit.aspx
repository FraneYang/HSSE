<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubUnitQualityEdit.aspx.cs"
    Inherits="FineUIPro.Web.QualityAudit.SubUnitQualityEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑外委单位资质</title>
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
                    <f:TextBox Label="单位名称" runat="server" ID="txtUnitName" LabelAlign="Right" Readonly="true"
                        LabelWidth="120px">
                    </f:TextBox>
                    <f:TextBox ID="txtSubUnitQualityCode" runat="server" Label="资质编号" LabelAlign="Right"
                        Required="true" ShowRedStar="true" LabelWidth="120px">
                    </f:TextBox>
                    <f:TextBox ID="txtSubUnitQualityName" runat="server" Label="资质名称" LabelAlign="Right"
                        Required="true" ShowRedStar="true" LabelWidth="120px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="25% 25% 30% 20%">
                <Items>
                    <f:TextBox ID="txtBusinessLicense" runat="server" Label="营业执照" LabelAlign="Right"
                        MaxLength="50" LabelWidth="140px">
                    </f:TextBox>
                    <f:DatePicker ID="txtBL_EnableDate" runat="server" Label="营业执照有效期" LabelAlign="Right"
                        EnableEdit="false" LabelWidth="140px">
                    </f:DatePicker>
                    <f:FileUpload runat="server" ID="btnBL_ScanUrl" EmptyText="请选择附件" OnFileSelected="btnBL_ScanUrl_Click"
                        AutoPostBack="true" Label="营业执照扫描件" LabelWidth="140px">
                    </f:FileUpload>
                    <f:ContentPanel ID="cpBL" runat="server" ShowHeader="false" ShowBorder="false" Title="营业执照扫描件">
                        <table>
                            <tr style="height: 28px">
                                <td align="left">
                                    <div id="divBL_ScanUrl" runat="server">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </f:ContentPanel>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="25% 25% 30% 20%">
                <Items>
                    <f:TextBox ID="txtOrganCode" runat="server" Label="机构代码" LabelAlign="Right" MaxLength="50"
                        LabelWidth="140px">
                    </f:TextBox>
                    <f:DatePicker ID="txtO_EnableDate" runat="server" Label="机构代码有效期" LabelAlign="Right"
                        EnableEdit="false" LabelWidth="140px">
                    </f:DatePicker>
                    <f:FileUpload runat="server" ID="btnO_ScanUrl" EmptyText="请选择附件" OnFileSelected="btnO_ScanUrl_Click"
                        AutoPostBack="true" Label="机构代码扫描件" LabelWidth="140px">
                    </f:FileUpload>
                    <f:ContentPanel ID="ContentPanel1" runat="server" ShowHeader="false" ShowBorder="false"
                        Title="机构代码扫描件">
                        <table>
                            <tr style="height: 28px">
                                <td align="left">
                                    <div id="divO_ScanUrl" runat="server">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </f:ContentPanel>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="25% 25% 30% 20%">
                <Items>
                    <f:TextBox ID="txtCertificate" runat="server" Label="资质证书" LabelAlign="Right" MaxLength="50"
                        LabelWidth="140px">
                    </f:TextBox>
                    <f:DatePicker ID="txtC_EnableDate" runat="server" Label="资质证书有效期" LabelAlign="Right"
                        EnableEdit="false" LabelWidth="140px">
                    </f:DatePicker>
                    <f:FileUpload runat="server" ID="btnC_ScanUrl" EmptyText="请选择附件" OnFileSelected="btnC_ScanUrl_Click"
                        AutoPostBack="true" Label="资质证书扫描件" LabelWidth="140px">
                    </f:FileUpload>
                    <f:ContentPanel ID="ContentPanel2" runat="server" ShowHeader="false" ShowBorder="false"
                        Title="资质证书扫描件">
                        <table>
                            <tr style="height: 28px">
                                <td align="left">
                                    <div id="divC_ScanUrl" runat="server">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </f:ContentPanel>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="25% 25% 30% 20%">
                <Items>
                    <f:TextBox ID="txtQualityLicense" runat="server" Label="质量体系认证证书" LabelAlign="Right"
                        MaxLength="50" LabelWidth="140px">
                    </f:TextBox>
                    <f:DatePicker ID="txtQL_EnableDate" runat="server" Label="质量--有效期" LabelAlign="Right"
                        EnableEdit="false" LabelWidth="140px">
                    </f:DatePicker>
                    <f:FileUpload runat="server" ID="btnQL_ScanUrl" EmptyText="请选择附件" OnFileSelected="btnQL_ScanUrl_Click"
                        AutoPostBack="true" Label="质量--扫描件" LabelWidth="140px">
                    </f:FileUpload>
                    <f:ContentPanel ID="ContentPanel3" runat="server" ShowHeader="false" ShowBorder="false"
                        Title="质量--扫描件">
                        <table>
                            <tr style="height: 28px">
                                <td align="left">
                                    <div id="divQL_ScanUrl" runat="server">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </f:ContentPanel>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="25% 25% 30% 20%">
                <Items>
                    <f:TextBox ID="txtHSELicense" runat="server" Label="HSE体系认证证书" LabelAlign="Right"
                        MaxLength="50" LabelWidth="140px">
                    </f:TextBox>
                    <f:DatePicker ID="txtH_EnableDate" runat="server" Label="HSE--有效期" LabelAlign="Right"
                        EnableEdit="false" LabelWidth="140px">
                    </f:DatePicker>
                    <f:FileUpload runat="server" ID="btnH_ScanUrl" EmptyText="请选择附件" OnFileSelected="btnH_ScanUrl_Click"
                        AutoPostBack="true" Label="HSE--扫描件" LabelWidth="140px">
                    </f:FileUpload>
                    <f:ContentPanel ID="ContentPanel4" runat="server" ShowHeader="false" ShowBorder="false"
                        Title="HSE--扫描件">
                        <table>
                            <tr style="height: 28px">
                                <td align="left">
                                    <div id="divH_ScanUrl" runat="server">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </f:ContentPanel>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="25% 25% 30% 20%">
                <Items>
                    <f:TextBox ID="txtSecurityLicense" runat="server" Label="安全生产许可证" LabelAlign="Right"
                        MaxLength="50" LabelWidth="140px">
                    </f:TextBox>
                    <f:DatePicker ID="txtSL_EnableDate" runat="server" Label="有效期" LabelAlign="Right"
                        EnableEdit="false" LabelWidth="140px">
                    </f:DatePicker>
                    <f:FileUpload runat="server" ID="btnSL_ScanUrl" EmptyText="请选择附件" OnFileSelected="btnSL_ScanUrl_Click"
                        AutoPostBack="true" Label="扫描件" LabelWidth="140px">
                    </f:FileUpload>
                    <f:ContentPanel ID="ContentPanel5" runat="server" ShowHeader="false" ShowBorder="false"
                        Title="安全生产许可证扫描件">
                        <table>
                            <tr style="height: 28px">
                                <td align="left">
                                    <div id="divSL_ScanUrl" runat="server">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </f:ContentPanel>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
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
    </form>
</body>
</html>
