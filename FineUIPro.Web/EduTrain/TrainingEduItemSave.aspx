<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrainingEduItemSave.aspx.cs" Inherits="FineUIPro.Web.EduTrain.TrainingEduItemSave" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" AjaxAspnetControls="divPictureUrl,divBeImageUrl,divAttachUrl" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtTrainingEduItemCode" runat="server" Label="编号" Required="true" ShowRedStar="true"
                        FocusOnPageLoad="true" MaxLength="50">
                    </f:TextBox>
                     <f:TextBox ID="txtTrainingEduItemName" runat="server" Label="名称" Required="true" ShowRedStar="true"
                         MaxLength="100" AutoPostBack="true" OnTextChanged="TextBox_TextChanged">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                 <Items>
                  <f:DropDownBox runat="server" Label="适合岗位</br>(装置/科室)" ID="DropDownBox1" 
                      DataControlID="RadioButtonList1" EnableMultiSelect="true" >
                    <PopPanel>
                        <f:SimpleForm ID="SimpleForm2" BodyPadding="10px" runat="server" AutoScroll="true"
                            ShowBorder="True" ShowHeader="false" Hidden="true">
                           <Items>
                               <f:Label ID="Label1" runat="server" Text="请选择适合的岗位(装置/科室)："></f:Label>
                                <f:CheckBoxList ID="RadioButtonList1" ColumnNumber="3" runat="server"> 
                                </f:CheckBoxList>
                           </Items>
                        </f:SimpleForm>
                    </PopPanel>
                </f:DropDownBox>
                </Items> 
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:HtmlEditor runat="server" Label="内容" ID="txtSummary" ShowLabel="false"
                        Editor="UMEditor" BasePath="~/res/umeditor/" ToolbarSet="Full" Height="330" LabelAlign="Right">
                    </f:HtmlEditor>
                </Items>
            </f:FormRow>            
            <f:FormRow>
                <Items>
                    <f:FileUpload runat="server" ID="btnPictureUrl" EmptyText="请选择图片" OnFileSelected="btnPictureUrl_Click"
                        AutoPostBack="true" Label="图片" >
                    </f:FileUpload>
                    <f:ContentPanel ID="ContentPanel1" runat="server" ShowHeader="false" ShowBorder="false"
                        Title="图片">
                        <table>
                            <tr style="height: 25px">
                                <td align="left">
                                    <div id="divPictureUrl" runat="server">
                                    </div>
                                </td>
                            </tr>
                             <tr style="height:130px" runat="server" id="trImageUrl" visible="false">
                                <td style="text-align:left">
                                    <div id="divBeImageUrl" runat="server">
                                    </div>
                                </td>                               
                            </tr>
                        </table>
                    </f:ContentPanel>
                    <f:Button ID="btnPictureUrlDelete" Icon="Delete" runat="server" OnClick="btnPictureUrlDelete_Click" ToolTip="删除">
                    </f:Button>
                </Items>
            </f:FormRow>  
            <f:FormRow runat="server" Hidden="true">
                <Items>
                    <f:FileUpload runat="server" ID="btnAttachUrl" EmptyText="请选择附件" OnFileSelected="btnAttachUrl_Click"
                        AutoPostBack="true" Label="附件" >
                    </f:FileUpload>
                    <f:ContentPanel ID="ContentPanel2" runat="server" ShowHeader="false" ShowBorder="false"
                        Title="附件">
                        <table>
                            <tr style="height: 25px">
                                <td align="left">
                                    <div id="divAttachUrl" runat="server">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </f:ContentPanel>
                    <f:Button ID="btnAttachUrlDelete" Icon="Delete" runat="server" OnClick="btnAttachUrlDelete_Click" ToolTip="删除">
                    </f:Button>
                </Items>
            </f:FormRow>  
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                     <f:Button ID="Button1" Text="附件" ToolTip="附件上传及查看" Icon="TableCell" runat="server"
                        OnClick="btnAttachUrlC_Click" ValidateForms="SimpleForm1" MarginLeft="5px">
                    </f:Button>
                    <f:ToolbarFill ID="ToolbarFill1" runat="server">
                    </f:ToolbarFill>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click" Hidden="true">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    <f:Window ID="WindowAtt" Title="附件" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    </form>
</body>
</html>
