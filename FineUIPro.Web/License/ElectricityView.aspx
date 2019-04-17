<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ElectricityView.aspx.cs"
    Inherits="FineUIPro.Web.License.ElectricityView" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>临时用电安全作业证</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
     <style>
        .formtitle .f-field-body {
            text-align: center;           
            margin: 10px 0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="true" ShowHeader="false" AutoScroll="true" TitleAlign="Center"
        BodyPadding="5px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right" EnableTableStyle="true">
        <Rows>             
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
             <f:FormRow>
                 <Items>
                    <f:TextBox ID="txtJobStartTime" runat="server" Label="作业时间" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                 </Items>
             </f:FormRow>
             <f:FormRow>
                 <Items>
                    <f:TextBox ID="txtJobPalce" runat="server" Label="作业地点" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                 </Items>
             </f:FormRow>
             <f:FormRow>
                 <Items>
                    <f:TextBox ID="txtAccessPoint" runat="server" Label="电源接入点" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                      <f:TextBox ID="txtWorkingVoltage" runat="server" Label="工作电压" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                 </Items>
             </f:FormRow> 
            <f:FormRow>
                 <Items>
                    <f:TextBox ID="txtEquipmentPower" runat="server" Label="用电设备及功率" Readonly="true" LabelWidth="140px">
                    </f:TextBox>
                      <f:TextBox ID="txtMeterReading" runat="server" Label="表底数" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                 </Items>
             </f:FormRow>             
             <f:FormRow>
                 <Items>
                    <f:TextBox ID="txtJobManName" runat="server" Label="作业人" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                     <f:TextBox ID="txtJobManCode" runat="server" Label="电工证号" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                 </Items>
             </f:FormRow>
            <f:FormRow>
                 <Items>
                    <f:TextArea ID="txtHAZIDName" runat="server" Label="危害辨识" Readonly="true" LabelWidth="120px" Height="40">
                    </f:TextArea>                   
                 </Items>
             </f:FormRow>
           <f:FormRow ColumnWidths="10% 70% 20%">
                 <Items>
                    <f:Label ID="序号" runat="server" Text="序号" Readonly="true">
                    </f:Label>
                    <f:Label ID="安全措施" runat="server" Text="安全措施" Readonly="true" >
                    </f:Label>
                    <f:Label ID="确认人" runat="server" Text="确认人" Readonly="true" >
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
           
             <f:FormRow>
                 <Items>
                    <f:TextBox ID="txtSafeEduManName" runat="server" Label="实施安全教育人" Readonly="true" LabelWidth="130px">
                    </f:TextBox>
                    <f:Label ID="TextBox33" runat="server"  Readonly="true" >
                    </f:Label>
                     <f:Label ID="TextBox34" runat="server" Readonly="true">
                    </f:Label>
                 </Items>
             </f:FormRow>
             <f:FormRow>
                 <Items>
                   <f:Form ID="Form2" ShowBorder="true" ShowHeader="true" AutoScroll="true" TitleAlign="Left"
                    BodyPadding="5px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right" Title="作业单位意见">
                    <Rows>
                        <f:FormRow>
                            <Items>
                                 <f:TextArea ID="txtOperationUnitOpinion" runat="server"  Readonly="true" Height="50px">
                                  </f:TextArea>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ColumnWidths="40% 30% 30%">
                            <Items>
                                  <f:Label ID="Label6" runat="server" >
                                  </f:Label>
                                  <f:Label ID="txtOperationUnitName" runat="server" Label="签字">
                                  </f:Label>
                                  <f:Label ID="txtOperationUnitTime" runat="server" Text="年月日时分">
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
                    BodyPadding="5px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right" Title="配送电单位意见">
                    <Rows>
                        <f:FormRow>
                            <Items>
                                <f:TextArea ID="txtElectricUnitOpinion" runat="server"  Readonly="true" Height="50px">
                                  </f:TextArea>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ColumnWidths="40% 30% 30%">
                            <Items>
                                  <f:Label ID="Label9" runat="server" >
                                  </f:Label>
                                  <f:Label ID="txtElectricUnitName" runat="server" Label="签字">
                                  </f:Label>
                                  <f:Label ID="txtElectricUnitTime" runat="server" Text="年月日时分">
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
                    BodyPadding="5px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right" Title="审批部门意见">
                    <Rows>
                        <f:FormRow>
                            <Items>
                                <f:TextArea ID="txtAuditDepOpinion" runat="server"  Readonly="true" Height="50px">
                                  </f:TextArea>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ColumnWidths="40% 30% 30%">
                            <Items>
                                  <f:Label ID="Label12" runat="server" >
                                  </f:Label>
                                  <f:Label ID="txtAuditDepName" runat="server" Label="签字">
                                  </f:Label>
                                  <f:Label ID="txtAuditDepTime" runat="server" Text="年月日时分">
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
                    BodyPadding="5px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right" Title="完工验收">
                        <Rows>
                            <f:FormRow>
                                <Items>
                                    <f:TextArea ID="txtAcceptanceOpinion" runat="server"  Readonly="true" Height="50px">
                                    </f:TextArea>
                                </Items>
                            </f:FormRow>
                            <f:FormRow ColumnWidths="40% 30% 30%">
                                <Items>
                                    <f:Label ID="Label15" runat="server" >
                                    </f:Label>
                                    <f:Label ID="txtAcceptanceName" runat="server" Label="签字">
                                    </f:Label>
                                    <f:Label ID="txtAcceptanceTime" runat="server" Text="年月日时分">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                </Items>
            </f:FormRow>
          <f:FormRow>
                <Items>
                    <f:Label ID="lblBottom" runat="server" >
                    </f:Label>
                </Items>
            </f:FormRow>
        </Rows>
    </f:Form>
    </form>
</body>
</html>
