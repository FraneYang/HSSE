<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonQualitySave.aspx.cs" Inherits="FineUIPro.Web.QualityAudit.PersonQualitySave" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑LEC评估</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />      
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
      <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
            <Rows>
                <f:FormRow>
                    <Items>
                        <f:Label runat="server"  ID="lbName" CssClass="highlight" Label="人员信息" LabelWidth="90px"></f:Label>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextBox ID="txtCertificateNo" runat="server" Label="证书编号" ShowRedStar="true" Required="true"
                                MaxLength="50" LabelWidth="90px" FocusOnPageLoad="true">
                        </f:TextBox>                                
                        <f:DropDownList ID="drpCertificateId" runat="server" Label="证书名称" ShowRedStar="true" Required="true"
                                LabelWidth="90px" EnableEdit="true">
                        </f:DropDownList>                    
                    </Items>
                </f:FormRow>            
                <f:FormRow>
                    <Items>
                        <f:DropDownList ID="drpProspectiveIds" runat="server" Label="准操项目" ShowRedStar="true" Required="true"
                                LabelWidth="90px" EnableMultiSelect="true" EnableCheckBoxSelect="false" EnableEdit="true" MaxLength="4000">
                        </f:DropDownList>
                    </Items>
                </f:FormRow>                             
                <f:FormRow>
                    <Items>
                        <f:DropDownList ID="drpSendUnit" runat="server" Label="发证单位" ShowRedStar="true" Required="true"
                                LabelWidth="90px" EnableEdit="true">
                        </f:DropDownList>
                    </Items>
                </f:FormRow> 
                <f:FormRow>
                    <Items>
                        <f:DatePicker ID="txtSendDate" runat="server" Label="发证时间" LabelAlign="Right"
                                EnableEdit="false" LabelWidth="90px">
                        </f:DatePicker>
                        <f:DatePicker ID="txtLimitDate" runat="server" Label="有效期" LabelAlign="Right"
                                EnableEdit="false" LabelWidth="90px">
                        </f:DatePicker>
                    </Items>
                </f:FormRow> 
                <f:FormRow>
                    <Items>
                        <f:DatePicker ID="txtLateCheckDate" runat="server" Label="复查时间" LabelAlign="Right"
                            EnableEdit="false" LabelWidth="90px">
                        </f:DatePicker>
                        <f:DatePicker ID="txtAuditDate" runat="server" Label="审核时间" LabelAlign="Right"
                            EnableEdit="false" LabelWidth="90px">
                        </f:DatePicker>
                    </Items>
                </f:FormRow> 
                <f:FormRow>
                    <Items>
                        <f:DatePicker ID="txtCompileDate" runat="server" Label="编制时间" LabelAlign="Right"
                            EnableEdit="false" LabelWidth="90px">
                        </f:DatePicker>
                        <f:DropDownList ID="drpCompileMan" runat="server" Label="编制人" EnableEdit="true"
                            LabelWidth="90px">
                        </f:DropDownList>
                    </Items>
                </f:FormRow> 
                <f:FormRow>
                    <Items>
                        <f:TextArea ID="txtRemark" runat="server" LabelWidth="90px" Label="备注" MaxLength="500" Height="50px">
                        </f:TextArea>
                    </Items>
                </f:FormRow>              
            </Rows>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                    <Items>
                        <f:Button ID="btnAttachUrl" Text="附件" ToolTip="附件上传及查看" Icon="TableCell" runat="server"
                            OnClick="btnAttachUrl_Click" ValidateForms="SimpleForm1" MarginLeft="5px">
                        </f:Button>
                        <f:Button ID="btnSeeCheck" Text="复查记录" ToolTip="复查时间查看" Icon="SystemSearch" runat="server"
                            OnClick="btnSeeCheck_Click" MarginLeft="5px"></f:Button>
                        <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                        <f:Button ID="btnSave" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm1" Hidden="true"
                            OnClick="btnSave_Click" >
                        </f:Button>
                        <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose" MarginRight="10px">
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
