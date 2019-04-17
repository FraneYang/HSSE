<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HiddenHazardView.aspx.cs"
    Inherits="FineUIPro.Web.Hazard.HiddenHazardView" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>隐患详细</title>
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
                    <f:TextBox ID="txtHiddenHazardName" runat="server" Label="隐患名称" Readonly="true">
                    </f:TextBox>
                 </Items>
             </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtHiddenHazardCode" runat="server" Label="编号" Readonly="true">
                    </f:TextBox>                   
                    <f:TextBox ID="txtIsMajor" runat="server" Label="隐患级别" Readonly="true">
                    </f:TextBox>
                     <f:TextBox ID="txtLimitTime" runat="server" Label="整改时限" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>            
            <f:FormRow>
                <Items>                   
                    <f:ContentPanel ID="ContentPanel2" runat="server" ShowHeader="false" ShowBorder="true"
                        Title="整改图片">
                        <table style="width:100%">
                            <tr style="vertical-align:top;height:20px" >
                                <td style="text-align:left;width:50%">
                                    整改前：<div id="divBeImageUrl" runat="server">
                                    </div>
                                </td>
                                <td style="text-align:left;width:50%">
                                    整改后：<div id="divAfImageUrl" runat="server">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </f:ContentPanel>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>      
                    <f:TextBox ID="txtHiddenHazardTypeName" runat="server" Label="隐患类别" Readonly="true">
                    </f:TextBox>                    
                    <f:TextBox ID="txtFindTime" runat="server" Label="发现时间" Readonly="true">
                    </f:TextBox>  
                     <f:TextBox ID="txtFindManName" runat="server" Label="发现人" Readonly="true">
                    </f:TextBox>    
                 </Items>
            </f:FormRow> 
             <f:FormRow>
                <Items>
                    <f:TextBox ID="txtHiddenHazardPlace" runat="server" Label="隐患位置" Readonly="true">
                    </f:TextBox>                       
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtDescription" runat="server" Label="内容描述" Readonly="true" Height="40">
                    </f:TextArea>
                </Items>
            </f:FormRow>             
              <f:FormRow>
                <Items>
                    <f:TextArea ID="txtControlMeasures" runat="server" Label="控制措施" Readonly="true" Height="40">
                    </f:TextArea>
                </Items>
            </f:FormRow> 
             <f:FormRow>
                <Items>
                    <f:TextArea ID="txtCorrectMeasures" runat="server" Label="整改措施" Readonly="true" Height="40">
                    </f:TextArea>
                </Items>
            </f:FormRow>
             <f:FormRow>
                <Items>
                    <f:TextBox ID="txtCorrectMoney" runat="server" Label="整改资金" Readonly="true">
                    </f:TextBox>
                    <f:Label runat="server" Text="(万元)"></f:Label>
                </Items>
            </f:FormRow> 
              <f:FormRow>
                <Items>
                    <f:TextBox ID="txtOperateManNames" runat="server" Label="作业人" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow> 
             <f:FormRow>
                <Items>
                    <f:TextBox ID="txtCorrectScheme" runat="server" Label="整改方案" Readonly="true">
                    </f:TextBox>
                     <f:ContentPanel ID="ContentPanel11" runat="server" ShowHeader="false" ShowBorder="false"
                        Title="整改方案" MarginLeft="100px">
                        <table style="width:100%">
                            <tr style="vertical-align:top;">
                                <td style="text-align:left;width:100%">
                                        <div id="divCorrectSchemeUrl" runat="server">
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
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>        
    </form>
</body>
</html>
