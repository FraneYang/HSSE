<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AInterlockingView.aspx.cs"
    Inherits="FineUIPro.Web.License.AInterlockingView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>A级联锁变更审批单</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style>
        .formtitle .f-field-body
        {
            text-align: center;
            margin: 10px 0;
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
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtApplyDate" runat="server" Label="申请日期" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtInterlockName" runat="server" Label="联锁名称" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                    <f:TextBox ID="txtInterlockLevel" runat="server" Label="级别" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtApplyUnit" runat="server" Label="申请单位" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                    <f:TextBox ID="txtUnitHead" runat="server" Label="单位主管" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtPerformUnitName" runat="server" Label="执行单位" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                    <f:TextBox ID="txtChangeOperator" runat="server" Label="变更操作人" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtChangeTime" runat="server" Label="连锁变更时间" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                    <f:TextBox ID="txtLicenseCode" runat="server" Label="变更连锁编号" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Form ID="Form7" ShowBorder="true" ShowHeader="true" AutoScroll="true" TitleAlign="Left"
                        BodyPadding="5px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right"
                        Title="申请理由及内容">
                        <Rows>
                            <f:FormRow>
                                <Items>
                                    <f:TextArea ID="txtReason" runat="server" Readonly="true" Height="150px">
                                    </f:TextArea>
                                </Items>
                            </f:FormRow>
                            <f:FormRow ColumnWidths="60% 40%">
                                <Items>
                                    <f:Label ID="Label2" runat="server">
                                    </f:Label>
                                    <f:Label ID="txtApplyManName" runat="server" Label="申请人">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Form ID="Form2" ShowBorder="true" ShowHeader="true" AutoScroll="true" TitleAlign="Left"
                    BodyPadding="5px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right" Title="装置意见">
                    <Rows>
                        <f:FormRow>
                            <Items>
                                 <f:TextArea ID="txtInstallationOpinion" runat="server"  Readonly="true" Height="50px">
                                  </f:TextArea>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ColumnWidths="20% 40% 40%">
                            <Items>
                                  <f:Label ID="Label6" runat="server" >
                                  </f:Label>
                                  <f:Label ID="txtInstallationOpinionName" runat="server" Label="签字">
                                  </f:Label>
                                  <f:Label ID="txtInstallationOpinionTime" runat="server" Text="年月日时分">
                                  </f:Label>
                            </Items>
                        </f:FormRow>
                     </Rows>
                    </f:Form>
                    <f:Form ID="Form3" ShowBorder="true" ShowHeader="true" AutoScroll="true" TitleAlign="Left"
                    BodyPadding="5px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right" Title="设备组意见">
                    <Rows>
                        <f:FormRow>
                            <Items>
                                 <f:TextArea ID="txtEquipmentOpinion" runat="server"  Readonly="true" Height="50px">
                                  </f:TextArea>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ColumnWidths="20% 40% 40%">
                            <Items>
                                  <f:Label ID="Label1" runat="server" >
                                  </f:Label>
                                  <f:Label ID="txtEquipmentOpinionName" runat="server" Label="签字">
                                  </f:Label>
                                  <f:Label ID="txtEquipmentOpinionTime" runat="server" Text="年月日时分">
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
                    BodyPadding="5px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right" Title="生产运行中心经理意见">
                    <Rows>
                        <f:FormRow>
                            <Items>
                                 <f:TextArea ID="txtProductionOpinion" runat="server"  Readonly="true" Height="50px">
                                  </f:TextArea>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ColumnWidths="20% 40% 40%">
                            <Items>
                                  <f:Label ID="Label3" runat="server" >
                                  </f:Label>
                                  <f:Label ID="txtProductionOpinionName" runat="server" Label="签字">
                                  </f:Label>
                                  <f:Label ID="txtProductionOpinionTime" runat="server" Text="年月日时分">
                                  </f:Label>
                            </Items>
                        </f:FormRow>
                     </Rows>
                    </f:Form>
                     <f:Form ID="Form5" ShowBorder="true" ShowHeader="true" AutoScroll="true" TitleAlign="Left"
                    BodyPadding="5px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right" Title="总工程师意见">
                    <Rows>
                        <f:FormRow>
                            <Items>
                                 <f:TextArea ID="txtChiefEngineerOpinion" runat="server"  Readonly="true" Height="50px">
                                  </f:TextArea>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ColumnWidths="20% 40% 40%">
                            <Items>
                                  <f:Label ID="Label7" runat="server" >
                                  </f:Label>
                                  <f:Label ID="txtChiefEngineerOpinionName" runat="server" Label="签字">
                                  </f:Label>
                                  <f:Label ID="txtChiefEngineerOpinionTime" runat="server" Text="年月日时分">
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
