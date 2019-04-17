<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OverhaulView.aspx.cs"
    Inherits="FineUIPro.Web.License.OverhaulView" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>检修工作票</title>
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
            <f:FormRow ColumnWidths="80% 20%">
                <Items>
                 <f:Label ID="Label1" runat="server"></f:Label>
                <f:Label ID="lbRiskGrade" runat="server" Text="作业风险（）级">
                </f:Label>
                 </Items>
             </f:FormRow> 
             <f:FormRow ColumnWidths="66% 34%">
                 <Items>
                    <f:Label ID="lbSendTicketTime" runat="server" Label="送票时间" LabelWidth="120px"></f:Label>
                    <f:Label ID="lbLicenseCode" runat="server" Label="检修编号" LabelWidth="120px"></f:Label>
                 </Items>
             </f:FormRow>       
             <f:FormRow>
                 <Items>
                    <f:TextBox ID="txtInstallationName" runat="server" Label="设备所在车间" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                      <f:TextBox ID="txtDevicePositionNum" runat="server" Label="设备位号" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                     <f:TextBox ID="txtDeviceName" runat="server" Label="设备名称" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                 </Items>
             </f:FormRow> 
             <f:FormRow>
                 <Items>
                    <f:TextBox ID="txtUnitName" runat="server" Label="检修单位" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                      <f:TextBox ID="txtOverhaulCategory" runat="server" Label="检修类别" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                     <f:TextBox ID="txtIsMonthlyPlan" runat="server" Label="是否月计划" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                 </Items>
             </f:FormRow> 
            
            <f:FormRow>
                 <Items>
                    <f:TextBox ID="txtEstimateTime" runat="server" Label="预计工时" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                 </Items>
             </f:FormRow> 
             <f:FormRow>
                 <Items>
                    <f:TextBox ID="txtActualTime" runat="server" Label="实际工时" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                 </Items>
             </f:FormRow>
             <f:FormRow>
                 <Items>
                    <f:TextBox ID="txtStartDate" runat="server" Text="检修内容及技术要求" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                 </Items>
             </f:FormRow>
             <f:FormRow>
                 <Items>
                    <f:TextArea ID="txtOverhaulContent" runat="server" Readonly="true" Height="72px">
                    </f:TextArea>
                 </Items>
             </f:FormRow>
             <f:FormRow>
                 <Items>
                    <f:TextBox ID="txtCompileMan" runat="server" Label="编制人" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                     <f:TextBox ID="txtAuditor" runat="server" Label="审核人" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                 </Items>
             </f:FormRow>
             <f:FormRow>
                 <Items>
                    <f:TextBox ID="TextBox5" runat="server" Text="安全注意事项及工艺处理措施" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                 </Items>
             </f:FormRow>
             <f:FormRow>
                 <Items>
                    <f:CheckBox runat="server" ID="CheckBox1" Text="办理断电手续" Readonly="true">
                    </f:CheckBox>   
                    <f:CheckBox runat="server" ID="CheckBox2" Text="排放、清洗、置换合格" Readonly="true">
                    </f:CheckBox>   
                    <f:CheckBox runat="server" ID="CheckBox3" Text="登高作业、系安全带" Readonly="true">
                    </f:CheckBox>   
                    <f:CheckBox runat="server" ID="CheckBox4" Text="使用安全电压灯" Readonly="true">
                    </f:CheckBox>                   
                 </Items>
             </f:FormRow>
            <f:FormRow>
                 <Items>
                    <f:CheckBox runat="server" ID="CheckBox5" Text="强制通风"  Readonly="true">
                    </f:CheckBox>   
                    <f:CheckBox runat="server" ID="CheckBox6" Text="使用防爆工器具" Readonly="true">
                    </f:CheckBox>   
                    <f:CheckBox runat="server" ID="CheckBox7" Text="设置安全警示标识"  Readonly="true">
                    </f:CheckBox>   
                    <f:CheckBox runat="server" ID="CheckBox8" Text="有效隔离"  Readonly="true">
                    </f:CheckBox>                   
                 </Items>
             </f:FormRow>
             <f:FormRow>
                 <Items>
                    <f:CheckBox runat="server" ID="CheckBox9" Text="禁油、脱脂"  Readonly="true">
                    </f:CheckBox>   
                    <f:CheckBox runat="server" ID="CheckBox10" Text="办理入塔入罐证" Readonly="true">
                    </f:CheckBox>   
                    <f:CheckBox runat="server" ID="CheckBox11" Text="佩戴防毒面具"  Readonly="true">
                    </f:CheckBox>   
                    <f:CheckBox runat="server" ID="CheckBox12" Text="戴护目镜"  Readonly="true">
                    </f:CheckBox>                   
                 </Items>
             </f:FormRow>
             <f:FormRow>
                 <Items>
                    <f:CheckBox runat="server" ID="CheckBox13" Text="戴防尘口罩"  Readonly="true">
                    </f:CheckBox>   
                    <f:CheckBox runat="server" ID="CheckBox14" Text="穿长袖工作服" Readonly="true">
                    </f:CheckBox>   
                    <f:CheckBox runat="server" ID="CheckBox15" Text="办理动火证"  Readonly="true">
                    </f:CheckBox>   
                    <f:CheckBox runat="server" ID="CheckBox16" Text="穿连体防护服"  Readonly="true">
                    </f:CheckBox>                   
                 </Items>
             </f:FormRow>
            <f:FormRow ColumnWidth="75% 25%">
                 <Items>
                    <f:CheckBox runat="server" ID="CheckBox17" Text="气防监护"  Readonly="true">
                    </f:CheckBox>   
                    <f:Label runat="server"></f:Label>                 
                 </Items>
             </f:FormRow>
            <f:FormRow>
                 <Items>
                    <f:TextArea ID="txtRemarks" runat="server" Label="备注" Readonly="true" LabelWidth="120px" Height="40">
                    </f:TextArea>                   
                 </Items>
             </f:FormRow>
            <f:FormRow ColumnWidths="75% 25%">
                 <Items>
                     <f:Label runat="server"></f:Label>
                     <f:TextBox ID="txtProcessMan" runat="server" Label="工艺签字" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                 </Items>
             </f:FormRow>
             <f:FormRow>
                 <Items>
                    <f:TextBox ID="txtOverhaulHead" runat="server" Label="检修负责人" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                     <f:TextBox ID="txtOverhaulManIds" runat="server" Label="检修人员" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                 </Items>
             </f:FormRow>
              <f:FormRow>
                 <Items>
                    <f:TextBox ID="txtQualifiedTime" runat="server" Label="工艺处理合格交出时间" Readonly="true" LabelWidth="180px">
                    </f:TextBox>
                     <f:TextBox ID="txtProcessMonitor" runat="server" Label="工艺班长" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                 </Items>
             </f:FormRow>
              <f:FormRow>
                 <Items>
                    <f:TextArea ID="txtHAZIDName" runat="server" Label="危险因素辨识" Readonly="true" LabelWidth="120px" Height="40">
                    </f:TextArea>                   
                 </Items>
             </f:FormRow>
             <f:FormRow>
                 <Items>
                    <f:TextArea ID="txtTrainEdu" runat="server" Label="培训教育" Readonly="true" LabelWidth="120px" Height="40">
                    </f:TextArea>                   
                 </Items>
             </f:FormRow>
              <f:FormRow>
                 <Items>                    
                    <f:TextBox ID="txtYS1" runat="server" Label="完工验收" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                    <f:TextBox ID="txtCheckOverhaulHead" runat="server" Label="检修负责人" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                 </Items>
             </f:FormRow>
               <f:FormRow>
                 <Items>
                    <f:TextBox ID="txtYS2" runat="server" Label="完工验收" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                    <f:TextBox ID="txtCheckProcessMonitor" runat="server" Label="工艺班长" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                 </Items>
             </f:FormRow>            
        </Rows>
    </f:Form>
    </form>
</body>
</html>
