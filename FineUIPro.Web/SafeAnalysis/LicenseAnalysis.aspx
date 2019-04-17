<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LicenseAnalysis.aspx.cs" Inherits="FineUIPro.Web.SafeAnalysis.LicenseAnalysis" %>
<%@ Register Src="~/Controls/ChartControl.ascx" TagName="ChartControl" TagPrefix="uc1" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>隐患排查</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="RegionPanel1" AjaxAspnetControls="divAnalyse,divHazard"/>
    <f:RegionPanel ID="RegionPanel1" ShowBorder="false" runat="server" Margin="5px">
        <Regions>
            <f:Region ID="Region1" ShowBorder="false" ShowHeader="false" RegionPosition="Top"
                BodyPadding="0 5 0 0" Width="200px" Layout="Fit" runat="server" EnableCollapse="true">
                <Items>
                    <f:Form ID="Form2" ShowHeader="false" ShowBorder="false" runat="server" LabelAlign="Right">
                        <Rows>
                            <f:FormRow ColumnWidths="66% 17% 17%">
                                <Items>                                                                   
                                    <f:DatePicker ID="txtStartDate" runat="server"  AutoPostBack="true" OnTextChanged="BtnAnalyse_Click"
                                         EmptyText="开始时间" LabelAlign="Right" Label="时间" LabelWidth="50px">
                                    </f:DatePicker>
                                    <f:DatePicker ID="txtEndDate" runat="server" AutoPostBack="true" OnTextChanged="BtnAnalyse_Click"
                                        EmptyText="结束时间" Label="至" LabelWidth="50px">
                                    </f:DatePicker>                                                                                                         
                                </Items>
                            </f:FormRow>
                            <f:FormRow ColumnWidths="30% 20% 20% 20% 10%">                                
                                <Items>
                                    <f:DropDownList ID="drpChartType" runat="server" LabelWidth="80px" Label="图形类型" 
                                        AutoPostBack="true" OnSelectedIndexChanged="BtnAnalyse_Click" >
                                    </f:DropDownList>
                                    <f:Label ID="Label2" runat="server">
                                    </f:Label>
                                    <f:CheckBox ID="ckbShow" runat="server" LabelWidth="80px" Label="三维效果" 
                                        AutoPostBack="true" OnCheckedChanged="ckbShow_CheckedChanged">
                                    </f:CheckBox>
                                    <f:Label ID="Label1" runat="server">
                                    </f:Label>
                                    <f:Button ID="BtnAnalyse" Text="统计" Icon="ChartPie" runat="server" OnClick="BtnAnalyse_Click"></f:Button>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                </Items>
            </f:Region>
            <f:Region ID="Region2" ShowBorder="false" ShowHeader="false" Position="Center" Layout="VBox"
                BoxConfigAlign="Stretch" BoxConfigPosition="Left" runat="server">
                <Items>
                    <f:TabStrip ID="TabStrip1" CssClass="f-tabstrip-theme-simple" Height="500px" ShowBorder="true"
                        TabPosition="Top" MarginBottom="5px" EnableTabCloseMenu="false" runat="server">
                        <Tabs>
                            <f:Tab ID="Tab1" Title="表格" BodyPadding="5px" Layout="Fit" IconFont="Bookmark" runat="server"
                                TitleToolTip="按表格样式显示">
                                <Items>  
                                    <f:ContentPanel ShowHeader="false" runat="server" ID="ContentPanel1" Margin="0 0 0 0">
                                         <div id="divHazard">                                 
                                            <asp:GridView ID="gvHazard" runat="server" AllowSorting="True" PageSize="100"
                                                HorizontalAlign="Justify" Width="100%">                                                
                                                <Columns>
                                                </Columns>                                                
                                                <RowStyle CssClass="GridRow" />
                                                <PagerStyle HorizontalAlign="Left" />
                                            </asp:GridView>
                                        </div>
                                     </f:ContentPanel>
                                </Items>
                            </f:Tab>
                            <f:Tab ID="Tab2" Title="图形" BodyPadding="5px" Layout="Fit" IconFont="Bookmark"
                                runat="server"  TitleToolTip="按图形显示">
                                <Items>
                                      <f:ContentPanel ShowHeader="false" runat="server" ID="cpCostTime" Margin="0 0 0 0">
                                        <div id="divAnalyse">
                                            <uc1:ChartControl ID="ChartCostTime" runat="server" />    
                                        </div>                                           
                                    </f:ContentPanel>
                                </Items>
                            </f:Tab>
                        </Tabs>
                    </f:TabStrip>
                </Items>
            </f:Region>
        </Regions>
    </f:RegionPanel>
    </form>
</body>
</html>
