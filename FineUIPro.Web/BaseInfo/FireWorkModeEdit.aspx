﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FireWorkModeEdit.aspx.cs" Inherits="FineUIPro.Web.BaseInfo.FireWorkModeEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑动火方式</title>
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
                   <f:TextBox ID="txtFireWorkModeCode" runat="server" Label="编号" MaxLength="50" FocusOnPageLoad="true">
                   </f:TextBox>
                    
                </Items>
            </f:FormRow>     
             <f:FormRow>
                <Items>    
                  <f:TextBox ID="txtFireWorkModeName" runat="server" Label="动火方式" MaxLength="100" Required="true" ShowRedStar="true">
                   </f:TextBox>
                    </Items>
            </f:FormRow>     
             <f:FormRow>
                <Items>  
                    <f:TextBox ID="txtRemark" runat="server" Label="备注" MaxLength="200">
                   </f:TextBox>
                </Items>
            </f:FormRow>          
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm1" Hidden="true"
                        OnClick="btnSave_Click">
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
