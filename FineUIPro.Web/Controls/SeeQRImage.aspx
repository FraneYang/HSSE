﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SeeQRImage.aspx.cs" Inherits="FineUIPro.Web.Controls.SeeQRImage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>二维码查看</title>
    <link href="../../res/css/common.css" rel="stylesheet" type="text/css" />
     <style type="text/css">
        .userphoto .f-field-label
        {
            margin-top: 0;
        }
        
        .userphoto img
        {
            width: 260px;
            height: 260px;            
        }
        
        .uploadbutton .f-btn
        {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">   
    <f:PageManager ID="PageManager1" runat="server" />
        <f:ContentPanel ID="ContentPanel1" runat="server" ShowHeader="false" ShowBorder="false">
            <table>                            
                <tr runat="server" id="trImageUrl">
                    <td style="text-align:left;width:100%">
                        <div id="divBeImageUrl" runat="server">
                        </div>
                    </td>                               
                </tr>
                <tr style="height:20px">
                    <td style="width:100%">
                        <label runat ="server" id="txtName"></label>
                    </td>
                </tr>
            </table>
        </f:ContentPanel>
    </form>
</body>
</html>