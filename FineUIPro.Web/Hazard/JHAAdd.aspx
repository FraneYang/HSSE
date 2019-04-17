<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JHAAdd.aspx.cs" Inherits="FineUIPro.Web.Hazard.JHAAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑JHA评价</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
     <style type="text/css">
         .f-grid-row.Blue {
             background-color: blue;
         }
         .f-grid-row.Yellow {
             background-color: yellow;
         }
         .f-grid-row.Orange {
             background-color: orange;
         }
         .f-grid-row.Red {
             background-color: red;
         } 
            .f-grid-row .f-grid-cell-inner {
            white-space: normal;
            word-break: break-all;
        }  
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true" Layout="VBox" 
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right" EnableTableStyle="true">
        <Rows>           
            <f:FormRow runat="server" ID="fRow0">
                <Items>                   
                   <f:TextBox Label="作业活动" runat="server" ID="txtJobActivityName" Readonly="true" LabelWidth="120px">
                     </f:TextBox>
                     <f:TextBox Label="编号" runat="server" ID="txtJobActivityCode" Readonly="true" LabelWidth="120px">
                     </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox Label="装置" runat="server" ID="txtInstallation" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                    <f:TextBox Label="区域单元" runat="server" ID="txtWorkArea" Readonly="true" LabelWidth="120px">
                    </f:TextBox>                         
                </Items>
            </f:FormRow>                         
            <f:FormRow>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="JHA评价" EnableCollapse="true" ClicksToEdit="2"  
                        runat="server" BoxFlex="1" DataKeyNames="JHAItemId" AllowCellEditing="true" EnableColumnLines="true" 
                        DataIDField="JHAItemId"  SortField="SortIndex"  OnRowCommand="Grid1_RowCommand" SortDirection="ASC" AllowPaging="false"
                        IsDatabasePaging="true" PageSize="1000" Height="350" EnableTextSelection="True"> 
                        <Toolbars>
                            <f:Toolbar ID="Toolbar1" Position="Top" ToolbarAlign="Right" runat="server">
                                <Items>      
                                     <f:Button ID="btnAdd" Icon="Add" runat="server"
                                        OnClick="btnAdd_Click" Hidden="true">
                                    </f:Button>
                                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" 
                                        OnClick="btnSave_Click" Hidden="true">
                                    </f:Button>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Columns>  
                            <f:RenderField Hidden="true" ColumnID="JHAItemId" DataField="JHAItemId" FieldType="String">
                            </f:RenderField>                                                          
                            <f:LinkButtonField Width="40px" ConfirmText="删除选中行？" ConfirmTarget="Top" CommandName="Delete"
                                Icon="Delete" TextAlign="Center" />
                            <f:RenderField Width="60px" ColumnID="SortIndex" DataField="SortIndex" FieldType="Int"
                                HeaderText="序号" HeaderTextAlign="Center" TextAlign="Center">
                                <Editor>
                                    <f:NumberBox ID="txtSortIndex" runat="server" NoDecimal="true" NoNegative="true">
                                    </f:NumberBox>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="120px" ColumnID="JobStep" DataField="JobStep" FieldType="String"
                                HeaderText="工作步骤" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                                <Editor>
                                    <f:TextBox ID="txtJobStep" MaxLength="50" runat="server" >
                                    </f:TextBox>
                                </Editor>
                            </f:RenderField>                        
                             <%--<f:RenderField Width="160px" ColumnID="PossibleAccidents" DataField="PossibleAccidents" FieldType="String"
                                HeaderText="危害或潜在事件" HeaderTextAlign="Center" TextAlign="Left">
                                <Editor>
                                    <f:TextBox ID="txtPossibleAccidents" MaxLength="500" runat="server" >
                                    </f:TextBox>
                                </Editor>
                            </f:RenderField>
                           <f:RenderField Width="180px" ColumnID="NowControlMeasures" DataField="NowControlMeasures" FieldType="String"
                                HeaderText="现有控制措施" HeaderTextAlign="Center" TextAlign="Left">
                                  <Editor>
                                    <f:TextBox ID="txtNowControlMeasures" MaxLength="800" runat="server">
                                    </f:TextBox>
                                </Editor>
                            </f:RenderField>
                           <f:RenderField Width="120px" ColumnID="HazardJudge_L1" DataField="HazardJudge_L1" FieldType="String"
                                HeaderText="偏差发生频率" HeaderTextAlign="Center" TextAlign="Left">
                                  <Editor>
                                    <f:DropDownList ID="drpHazardJudge_L1" runat="server">
                                        <f:ListItem Text="在正常情况下经常发生此类事故或事件(5)" Value="5.0" />
                                        <f:ListItem Text="危害常发生或在预期情况下发生(4)" Value="4.0" />
                                        <f:ListItem Text="过去曾经发生或在异常情况下发生类似事故事件(3)" Value="3.0" />
                                        <f:ListItem Text="过去偶尔发生危险事故事件(2)" Value="2.0" />
                                        <f:ListItem Text="及不可能发生事故或事件(1)" Value="1.0"  />
                                    </f:DropDownList>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="HazardJudge_L2" DataField="HazardJudge_L2" FieldType="String"
                                HeaderText="管理措施" HeaderTextAlign="Center" TextAlign="Left">
                                  <Editor>
                                    <f:DropDownList ID="drpHazardJudge_L2" runat="server">
                                         <f:ListItem Text="从来没有检查，没有操作规程(5)" Value="5.0" />
                                        <f:ListItem Text="偶尔检查或大检查，有操作规程，但只是偶尔执行（或操作规程内容不完善）(4)" Value="4.0" />
                                        <f:ListItem Text="月检，有操作规程，只是部分执行(3)" Value="3.0" />
                                        <f:ListItem Text="周检，有操作规程，但偶尔不执行(2)" Value="2.0" />
                                        <f:ListItem Text="日检，有操作规程，且严格执行(1)" Value="1.0"  />
                                    </f:DropDownList>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="140px" ColumnID="HazardJudge_L3" DataField="HazardJudge_L3" FieldType="String"
                                HeaderText="员工胜任程度</br>意识、技能、经验" HeaderTextAlign="Center" TextAlign="Left">
                                  <Editor>
                                    <f:DropDownList ID="drpHazardJudge_L3" runat="server">
                                         <f:ListItem Text="不胜任（无任何培训、无经验、无上岗资格）(5)" Value="5.0" />
                                        <f:ListItem Text="不够胜任（有上岗资格，但没有接受有效培训）(4)" Value="4.0" />
                                        <f:ListItem Text="一般胜任（有培训、有上岗资格、但经验不足，多次出现差错）(3)" Value="3.0" />
                                        <f:ListItem Text="胜任（但偶尔出现差错）(2)" Value="2.0" />
                                        <f:ListItem Text="高度胜任（培训充分，经验丰富，意识强）(1)" Value="1.0"  />
                                    </f:DropDownList>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="130px" ColumnID="HazardJudge_L4" DataField="HazardJudge_L4" FieldType="String"
                                HeaderText="监测、控制、报警</br>联锁、补救措施" HeaderTextAlign="Center" TextAlign="Left">
                                  <Editor>
                                    <f:DropDownList ID="drpHazardJudge_L4" runat="server">
                                         <f:ListItem Text="不胜任（无任何培训、无经验、无上岗资格）(5)" Value="5.0" />
                                        <f:ListItem Text="不够胜任（有上岗资格，但没有接受有效培训）(4)" Value="4.0" />
                                        <f:ListItem Text="一般胜任（有培训、有上岗资格、但经验不足，多次出现差错）(3)" Value="3.0" />
                                        <f:ListItem Text="胜任（但偶尔出现差错）(2)" Value="2.0" />
                                        <f:ListItem Text="高度胜任（培训充分，经验丰富，意识强）(1)" Value="1.0"  />
                                    </f:DropDownList>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="60px" ColumnID="HazardJudge_L" DataField="HazardJudge_L" FieldType="String"
                                HeaderText="L" HeaderTextAlign="Center" TextAlign="Left">
                                  <Editor>
                                    <f:NumberBox ID="txtHazardJudge_L" runat="server"
                                        NoDecimal="true" NoNegative="true" Readonly="true">
                                    </f:NumberBox>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="120px" ColumnID="HazardJudge_S1" DataField="HazardJudge_S1" FieldType="String"
                                HeaderText="人员伤亡程度" HeaderTextAlign="Center" TextAlign="Left">
                                  <Editor>
                                    <f:DropDownList ID="drpHazardJudge_S1" runat="server">
                                        <f:ListItem Text="●死亡●终身残废●丧失劳动能力(5)" Value="5.0" />
                                        <f:ListItem Text="●部分丧失劳动能力●职业病●慢性病●住院治疗(4)" Value="4.0" />
                                        <f:ListItem Text="需要去医院治疗，但不需住院(3)" Value="3.0" />
                                        <f:ListItem Text="●皮外伤●短时间身体不适(2)" Value="2.0" />
                                        <f:ListItem Text="没有受伤(1)" Value="1.0"  />
                                    </f:DropDownList>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="120px" ColumnID="HazardJudge_S2" DataField="HazardJudge_S2" FieldType="String"
                                HeaderText="财产损失/万元" HeaderTextAlign="Center" TextAlign="Left">
                                  <Editor>
                                    <f:DropDownList ID="drpHazardJudge_S2" runat="server">
                                         <f:ListItem Text="≥50(5)" Value="5.0" />
                                        <f:ListItem Text="≥25(4)" Value="4.0" />
                                        <f:ListItem Text="＞10(3)" Value="3.0" />
                                        <f:ListItem Text="＜10(2)" Value="2.0" />
                                        <f:ListItem Text="无损失(1)" Value="1.0"  />
                                    </f:DropDownList>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="HazardJudge_S3" DataField="HazardJudge_S3" FieldType="String"
                                HeaderText="停工时间" HeaderTextAlign="Center" TextAlign="Left">
                                  <Editor>
                                    <f:DropDownList ID="drpHazardJudge_S3" runat="server">
                                        <f:ListItem Text="三套以上装置停工(5)" Value="5.0" />
                                        <f:ListItem Text="二套装置停工(4)" Value="4.0" />
                                        <f:ListItem Text="一套装置停工(3)" Value="3.0" />
                                        <f:ListItem Text="影响不大，装置局部停工(2)" Value="2.0" />
                                        <f:ListItem Text="没有停工(1)" Value="1.0"  />
                                    </f:DropDownList>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="120px" ColumnID="HazardJudge_S4" DataField="HazardJudge_S4" FieldType="String"
                                HeaderText="法规及规章制度</br>符合状况" HeaderTextAlign="Center" TextAlign="Left">
                                  <Editor>
                                    <f:DropDownList ID="drpHazardJudge_S4" runat="server">
                                         <f:ListItem Text="违反法律、法规和标准(5)" Value="5.0" />
                                        <f:ListItem Text="潜在违反法律、法规和标准(4)" Value="4.0" />
                                        <f:ListItem Text="不符合集团公司规章制度标准(3)" Value="3.0" />
                                        <f:ListItem Text="不符合公司规章制度(2)" Value="2.0" />
                                        <f:ListItem Text="完全符合(1)" Value="1.0"  />
                                    </f:DropDownList>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="110px" ColumnID="HazardJudge_S5" DataField="HazardJudge_S5" FieldType="String"
                                HeaderText="形象受损程度" HeaderTextAlign="Center" TextAlign="Left">
                                  <Editor>
                                    <f:DropDownList ID="drpHazardJudge_S5" runat="server">
                                         <f:ListItem Text="全国性影响(5)" Value="5.0" />
                                        <f:ListItem Text="地区性、行业内影响(4)" Value="4.0" />
                                        <f:ListItem Text="集团公司范围内影响(3)" Value="3.0" />
                                        <f:ListItem Text="公司内影响(2)" Value="2.0" />
                                        <f:ListItem Text="无影响(1)" Value="1.0"  />
                                    </f:DropDownList>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="60px" ColumnID="HazardJudge_S" DataField="HazardJudge_S" FieldType="String"
                                HeaderText="S" HeaderTextAlign="Center" TextAlign="Left">
                                <Editor>
                                    <f:NumberBox ID="txtHazardJudge_S" runat="server" 
                                         NoDecimal="true" NoNegative="true" Readonly="true">
                                    </f:NumberBox>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="60px" ColumnID="HazardJudge_R" DataField="HazardJudge_R" FieldType="String"
                                HeaderText="R" HeaderTextAlign="Center" TextAlign="Left">
                                 <Editor>
                                    <f:NumberBox ID="txtHazardJudge_R" runat="server" 
                                        NoDecimal="true" NoNegative="true" Readonly="true">
                                    </f:NumberBox>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="120px" ColumnID="ControlMeasures" DataField="ControlMeasures" FieldType="String"
                                HeaderText="工程技术措施" HeaderTextAlign="Center" TextAlign="Left">
                                <Editor>
                                    <f:TextBox ID="txtControlMeasures" MaxLength="800" runat="server">
                                    </f:TextBox>
                                </Editor>
                            </f:RenderField> 
                            <f:RenderField Width="120px" ColumnID="ManagementMeasures" DataField="ManagementMeasures" FieldType="String"
                                HeaderText="管理措施" HeaderTextAlign="Center" TextAlign="Left">
                                <Editor>
                                    <f:TextBox ID="txtManagementMeasures" MaxLength="800" runat="server">
                                    </f:TextBox>
                                </Editor>
                            </f:RenderField> 
                             <f:RenderField Width="120px" ColumnID="ProtectiveMeasures" DataField="ProtectiveMeasures" FieldType="String"
                                HeaderText="防护措施" HeaderTextAlign="Center" TextAlign="Left">
                                <Editor>
                                    <f:TextBox ID="txtProtectiveMeasures" MaxLength="800" runat="server">
                                    </f:TextBox>
                                </Editor>
                            </f:RenderField> 
                             <f:RenderField Width="120px" ColumnID="OtherMeasures" DataField="OtherMeasures" FieldType="String"
                                HeaderText="其他措施" HeaderTextAlign="Center" TextAlign="Left">
                                <Editor>
                                    <f:TextBox ID="txtOtherMeasures" MaxLength="800" runat="server">
                                    </f:TextBox>
                                </Editor>
                            </f:RenderField> --%>
                        </Columns> 
                        <Listeners>
                            <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
                            <%--<f:Listener Event="afteredit" Handler="onGridAfterEdit" />--%>
                        </Listeners>
                  </f:Grid>
                </Items>
            </f:FormRow>          
        </Rows>       
    </f:Form>  
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>  
    </form>
    <script type="text/jscript">        
        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
           // F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }

        function reloadGrid() {
            __doPostBack(null, 'reloadGrid');
        }

//        function onGridAfterEdit(event, value, params) {
//            var me = this, columnId = params.columnId, rowId = params.rowId;
//            if (columnId === 'HazardJudge_L1' || columnId === 'HazardJudge_L2' || columnId === 'HazardJudge_L3' || columnId === 'HazardJudge_L4'
//             || columnId === 'HazardJudge_S1' || columnId === 'HazardJudge_S2' || columnId === 'HazardJudge_S3' || columnId === 'HazardJudge_S4' || columnId === 'HazardJudge_S5') {
//                var l1 = me.getCellValue(rowId, 'HazardJudge_L1');
//                var l2 = me.getCellValue(rowId, 'HazardJudge_L2');
//                var l3 = me.getCellValue(rowId, 'HazardJudge_L3');
//                var l4 = me.getCellValue(rowId, 'HazardJudge_L4');
//                var lValue = l1;
//                if (parseFloat(l1) < parseFloat(l2)) {
//                    lValue = l2;
//                }
//                if (parseFloat(l2) < parseFloat(l3)) {
//                    lValue = l3;
//                }
//                if (parseFloat(l3) < parseFloat(l4)) {
//                    lValue = l4;
//                }
//                me.updateCellValue(rowId, 'HazardJudge_L', lValue);

//                var s1 = me.getCellValue(rowId, 'HazardJudge_S1');
//                var s2 = me.getCellValue(rowId, 'HazardJudge_S2');
//                var s3 = me.getCellValue(rowId, 'HazardJudge_S3');
//                var s4 = me.getCellValue(rowId, 'HazardJudge_S4');
//                var s5 = me.getCellValue(rowId, 'HazardJudge_S5');
//                var sValue = s1;
//                if (parseFloat(s1) < parseFloat(s2)) {
//                    sValue = s2;
//                }
//                if (parseFloat(s2) < parseFloat(s3)) {
//                    sValue = s3;
//                }
//                if (parseFloat(s3) < parseFloat(s4)) {
//                    sValue = s4;
//                }
//                if (parseFloat(s4) < parseFloat(s5)) {
//                    sValue = s5;
//                }

//                var rValue= (sValue * lValue).toFixed(1);
//                me.updateCellValue(rowId, 'HazardJudge_S', sValue);
//                me.updateCellValue(rowId, 'HazardJudge_R', rValue);
//            }
//        }
        
    </script>
</body>
</html>