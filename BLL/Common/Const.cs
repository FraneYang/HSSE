using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public static class Const
    {        
        #region 查询字段：系统设置
        /// <summary>
        /// 系统管理员ID
        /// </summary>
        public const string sysglyId = "AF17168B-87BD-4GLY-1111-F0A0A1158F9B";

        /// <summary>
        /// 项目管理员登录名
        /// </summary>
        public const string adminAccount = "admin";

        /// <summary>
        /// null 字符串
        /// </summary>
        public const string _Null = "null";

        /// <summary>
        /// all 字符串
        /// </summary>
        public const string _ALL = "all";

        /// <summary>
        /// 默认用户密码
        /// </summary>
        public const string Password = "123";

        #endregion

        #region 按钮描述
        /// <summary>
        /// 添加
        /// </summary>
        public const string BtnAdd = "增加";
        /// <summary>
        /// 修改
        /// </summary>
        public const string BtnModify = "修改";
        /// <summary>
        /// 删除
        /// </summary>
        public const string BtnDelete = "删除";
        /// <summary>
        /// 保存
        /// </summary>
        public const string BtnSave = "保存";
        /// <summary>
        /// 提交
        /// </summary>
        public const string BtnSubmit = "提交";
        /// <summary>
        /// 保存并上报
        /// </summary>
        public const string BtnSaveUp = "保存并上报";
        /// <summary>
        /// 打印
        /// </summary>
        public const string BtnPrint = "打印";
        /// <summary>
        /// 上传资源
        /// </summary>
        public const string BtnUploadResources = "上传资源";
        /// <summary>
        /// 下载
        /// </summary>
        public const string BtnDownload = "下载";
        /// <summary>
        /// 选择
        /// </summary>
        public const string BtnSelect = "选择";
        /// <summary>
        /// 数据库备份
        /// </summary>
        public const string BtnDataBak = "数据库备份";
        /// <summary>
        /// 审核
        /// </summary>
        public const string BtnAuditing = "审核";
        /// <summary>
        /// 取消审核
        /// </summary>
        public const string BtnCancelAuditing = "取消审核";
        /// <summary>
        /// 导入
        /// </summary>
        public const string BtnIn = "导入";
        /// <summary>
        /// 导出
        /// </summary>
        public const string BtnOut = "导出";
        /// <summary>
        /// 统计分析
        /// </summary>
        public const string BtnAnalyse = "统计";
        /// <summary>
        /// 数据同步
        /// </summary>
        public const string BtnSyn = "同步";
        /// <summary>
        /// 发布
        /// </summary>
        public const string BtnIssuance = "发布";
        /// <summary>
        /// 确认
        /// </summary>
        public const string BtnConfirm = "确认";
        /// <summary>
        /// 提示
        /// </summary>
        public const string BtnToolTip = "提示";
        /// <summary>
        /// 响应
        /// </summary>
        public const string BtnResponse = "响应";
        /// <summary>
        /// 发卡
        /// </summary>
        public const string BtnSendCard = "发卡";
        #endregion

        #region 定义常量
        /// <summary>
        /// 新奥单位id
        /// </summary>
        public const string UnitId_XA = "24633275-2a4c-484f-986a-93d6c34857a9";  

        #region 项目单位类型
        /// <summary>
        /// 总包
        /// </summary>
        public const string ProjectUnitType_1 = "1";
        /// <summary>
        /// 施工分包
        /// </summary>
        public const string ProjectUnitType_2 = "2";
        /// <summary>
        /// 监理
        /// </summary>
        public const string ProjectUnitType_3 = "3"; 
        /// <summary>
        /// 业主
        /// </summary>
        public const string ProjectUnitType_4 = "4"; 
        /// <summary>
        /// 其他
        /// </summary>
        public const string ProjectUnitType_5 = "5";
        #endregion

        #region 岗位
        /// <summary>
        /// 一般管理岗位
        /// </summary>
        public const string PostType_1 = "1";
        /// <summary>
        /// 特种作业人员
        /// </summary>
        public const string PostType_2 = "2";
        /// <summary>
        /// 一般作业岗位
        /// </summary>
        public const string PostType_3 = "3";
        /// <summary>
        /// 特种管理人员
        /// </summary>
        public const string PostType_4 = "4";
        #endregion
        
        #region 菜单模块类型常量
        /// <summary>
        /// 个人设置菜单
        /// </summary>
        public const string Menu_Personal = "Menu_Personal";        
        /// <summary>
        /// 业务功能菜单
        /// </summary>
        public const string Menu_Project = "Menu_Project";
        /// <summary>
        /// 资源管理菜单
        /// </summary>
        public const string Menu_Resource = "Menu_Resource";
        /// <summary>
        /// 基础信息菜单
        /// </summary>
        public const string Menu_BaseInfo = "Menu_BaseInfo";
        /// <summary>
        /// 系统设置菜单
        /// </summary>
        public const string Menu_SystemSet = "Menu_SystemSet";
        #endregion
        #endregion

        #region 菜单id
        #region 系统设置
        /// <summary>
        /// 单位设置
        /// </summary>
        public const string UnitMenuId = "8IDKGJE2-09B1-4607-BC6D-865CE48F0002";
        /// <summary>
        /// 部门信息
        /// </summary>
        public const string DepartMenuId = "D9F1F0BD-D48F-4C5B-AF26-7F4C561D1352";
        /// <summary>
        /// 装置/科室
        /// </summary>
        public const string InstallationMenuId = "81D18BC5-8D19-487B-A5DD-7554DCC896C5";
        /// <summary>
        /// 岗位信息
        /// </summary>
        public const string WorkPostMenuId = "D4FC3583-31A7-49B3-8F32-007E9756D678";
        /// <summary>
        /// 角色管理
        /// </summary>
        public const string RoleMenuId = "EBAD373C-8EB4-49A1-91F6-6794FFCAF9B6";
        /// <summary>
        /// 用户
        /// </summary>
        public const string UserMenuId = "E6F0167E-B0FD-4A32-9C47-25FB9E0FDC4E";
        /// <summary>
        /// 角色授权
        /// </summary>
        public const string RolePowerMenuId = "2231022B-3519-42FC-A2E6-1DB9A98039DD";
        /// <summary>
        /// 报表设计
        /// </summary>
        public const string PrintDesignerMenuId = "0C67F4A8-1BE7-40BE-9621-B02A28FD13ED";
        /// <summary>
        /// 环境设置
        /// </summary>
        public const string SysConstSetMenuId = "E4BFDCFD-2B1F-49C5-B02B-1C91BFFAAC6E";
        /// <summary>
        /// 数据同步
        /// </summary>
        public const string SynchronizationMenuId = "6EDFBE24-9419-4E73-AC2E-CAD30A754A73";
        #endregion

        #region 基础信息
        /// <summary>
        /// 单位类别
        /// </summary>
        public const string UnitTypeMenuId = "685F1E0D-987E-491C-9DC7-014098DEE0C3";  
        /// <summary>
        /// 职务信息
        /// </summary>
        public const string PositionMenuId = "1E81DF97-809E-479F-1111-508F2043BA69";
        /// <summary>
        /// 职称信息
        /// </summary>
        public const string PostTitleMenuId = "2E424093-81B8-421A-963F-D85D17B1E82A";
        /// <summary>
        /// 特岗证书
        /// </summary>
        public const string CertificateMenuId = "3A40AF0B-C9B8-4AF9-A683-FEADD8CC3A1C";
        /// <summary>
        /// 危害辨识
        /// </summary>
        public const string HAZIDMenuId = "9016BB7D-4306-46E9-B3A7-4902E7B82BF6";
        /// <summary>
        /// 隐患类别
        /// </summary>
        public const string HiddenHazardTypeMenuId = "D2E5854B-97D0-44EE-A6E3-E15AFF1C951E";
        /// <summary>
        /// 安全措施
        /// </summary>
        public const string SafetyMeasuresMenuId = "1AC5BEBD-BA17-4EED-8F3B-E260FF3495C1";
        /// <summary>
        /// 风险等级对应值
        /// </summary>
        public const string RiskLevelValueMenuId = "732182BD-35CA-4F47-8970-FFBE070445F1";        
        /// <summary>
        /// SCL菜单id
        /// </summary>
        public const string SCLInfoMenuId = "22F3B0D5-BFDF-4766-B473-9664B2568229";
        #endregion

        #region 公共资源
        #region 安全合规
        /// <summary>
        /// 安全法律法规
        /// </summary>
        public const string LawRegulationListMenuId = "F4B02718-0616-4623-ABCE-885698DDBEB1";

        /// <summary>
        /// 安全标准规范
        /// </summary>
        public const string HSSEStandardListMenuId = "EFDSFVDE-RTHN-7UMG-4THA-5TGED48F8IOL";

        /// <summary>
        /// 政府部门安全规章制度
        /// </summary>
        public const string RulesRegulationsMenuId = "DF1413F3-4CE5-40B3-A574-E01CE64FEA25";

        /// <summary>
        /// 安全管理规定
        /// </summary>
        public const string ManageRuleMenuId = "56960940-81A8-43D1-9565-C306EC7AFD12";
        #endregion

        #region 安全体系
        /// <summary>
        /// 安全组织体系
        /// </summary>
        public const string HSSEOrganizeMenuId = "8IDKGJE2-09B1-6UIO-3EFM-5TGED48F0001"; 
        /// <summary>
        /// 安全管理机构
        /// </summary>
        public const string HSSEManageMenuId = "32F5CC8C-E0F4-456C-AB88-77E36269FA50";
        /// <summary>
        /// 安全主体责任
        /// </summary>
        public const string HSSEMainDutyMenuId = "3ACE25CE-C5CE-4CEC-AD27-0D5CF1DF2F01";
        /// <summary>
        /// 安全制度
        /// </summary>
        public const string ServerSafetyInstitutionMenuId = "499E23C1-057C-4B04-B92A-973B1DACD546";
        #endregion

        #region 安全教育
        /// <summary>
        /// 培训教材库
        /// </summary>
        public const string TrainDBMenuId = "9D99A981-7380-4085-84FA-8C3B1AFA6202";

        /// <summary>
        /// 安全试题库
        /// </summary>
        public const string TrainTestDBMenuId = "F58EE8ED-9EB5-47C7-9D7F-D751EFEA44CA";

        /// <summary>
        /// 事故案例库
        /// </summary>
        public const string AccidentCaseMenuId = "D86917DB-D00A-4E18-9793-C290B5BBA84C";

        /// <summary>
        /// 应知应会库
        /// </summary>
        public const string KnowledgeDBMenuId = "AB7A3D78-2D89-4488-97E3-8F8616BDDE30";
        #endregion

        #region 在线学习考核
        /// <summary>
        /// 试题库维护
        /// </summary>
        public const string TestDBMenuId = "C7454B8C-017F-4C8A-B9C7-D9D2F46F3CB1";

        /// <summary>
        /// 生成试卷
        /// </summary>
        public const string BuildTestMenuId = "86E75631-BB9B-432E-BE5C-0F8C7C58BDE3";

        /// <summary>
        /// 查看试卷
        /// </summary>
        public const string SeeTestMenuId = "F62D1C56-8FB0-480D-9702-84B168D0A89F";

        /// <summary>
        /// 考生信息
        /// </summary>
        public const string ExamineeMenuId = "8787A0B6-9F9B-40A6-B122-E8A8287A84B9";

        /// <summary>
        /// 考试系统
        /// </summary>
        public const string TestSystemMenuId = "C91CD0C5-6609-40BD-B5E6-C2DF148B198B";
        #endregion

        #region 安全技术
        /// <summary>
        /// 危险源清单
        /// </summary>
        public const string HazardListMenuId = "66A76F90-96A7-4C1F-B8D9-125DDEACEF52";
        /// <summary>
        /// 环境因素危险源
        /// </summary>
        public const string EnvironmentalMenuId = "773B59F9-61F9-4F5E-9D68-A1BF9322AFFA";
        /// <summary>
        /// 安全隐患
        /// </summary>
        public const string RectifyMenuId = "88CDDC68-54DE-4E24-9524-A33B80EC0E12";
        /// <summary>
        /// 检查项
        /// </summary>
        public const string TechniqueCheckItemSetMenuId = "4D92095C-8222-49D2-AF96-CD1972D4F4F8";
        /// <summary>
        /// HAZOP管理
        /// </summary>
        public const string HAZOPMenuId = "41C22E63-36B7-4C44-89EC-F765BFBB7C55";
        /// <summary>
        /// 安全评价
        /// </summary>
        public const string AppraiseMenuId = "0ADD01FB-8088-4595-BB40-6A73F332A229";
        /// <summary>
        /// 安全专家
        /// </summary>
        public const string ExpertMenuId = "05495F29-B583-43D9-89D3-3384D6783A3F";
        /// <summary>
        /// 应急预案
        /// </summary>
        public const string EmergencyMenuId = "575C5154-A135-4737-8682-A129EA717660";
        /// <summary>
        /// 施工方案
        /// </summary>
        public const string SpecialSchemeMenuId = "3E2F2FFD-ED2E-4914-8370-D97A68398814";
        #endregion

        #region 标牌管理
        /// <summary>
        /// 标牌管理
        /// </summary>
        public const string SignManageMenuId = "022CA9C1-70F0-4C07-996C-0736D32B442A";
        #endregion

        #region 参考资料
        /// <summary>
        /// 参考资料
        /// </summary>
        public const string ResourcesDataMenuId = "EC1BED24-CDA6-4041-9B2A-BEB5E354D58F";

        /// <summary>
        /// 问题及答案管理
        /// </summary>
        public const string ProblemAnswerMenuId = "37EB5621-A9D3-405A-854B-4722B045CC1E";
        #endregion

        #region 安全交流
        /// <summary>
        /// 注册管理
        /// </summary>
        public const string RegisterMenuId = "8FA7237E-DB0C-436C-9BC6-8C3A560EE688";

        /// <summary>
        /// 内容管理
        /// </summary>
        public const string ContentMenuId = "7CCD36CB-6BFE-4FD7-8497-4DACB565298E";
        #endregion
        #endregion

        #region 个人设置菜单
        /// <summary>
        /// 个人信息
        /// </summary>
        public const string PersonalInfoMenuId = "42368A1C-EE84-423D-9003-B0CAD0FF169D";

        /// <summary>
        /// 操作日志
        /// </summary>
        public const string RunLogMenuId = "D363BD9D-4DEC-45D8-89C8-B0E49DEF61B4";

        /// <summary>
        /// 个人文件夹
        /// </summary>
        public const string PersonalFolderMenuId = "A6994B53-6237-4C2B-BDC5-E7E79A1E7F88";
        #endregion

        #region 本部菜单管理
        #region 项目管理
        /// <summary>
        /// 项目信息
        /// </summary>
        public const string SeverProjectSetMenuId = "B830399C-CA36-4C23-A170-21E556D052DD";

        /// <summary>
        /// 项目单位
        /// </summary>
        public const string SeverProjectUnitMenuId = "EB9C4E5E-15DB-426A-9800-6B03E64EC5DE";

        /// <summary>
        /// 项目用户
        /// </summary>
        public const string SeverProjectUserMenuId = "0CF5F6A1-4AEB-4034-9C3B-591838E1290A";
        #endregion

        #region 安全报表（集团）
        #region 安全信息上报
        /// <summary>
        /// 百万工时安全统计月报表
        /// </summary>
        public const string MillionsMonthlyReportMenuId = "3156A9F0-276D-4AD4-BF84-45CF6DFC215C";

        /// <summary>
        /// 职工伤亡事故原因分析报表
        /// </summary>
        public const string AccidentCauseReportMenuId = "4BC71D2E-7D94-48C1-A61A-139637825AA5";

        /// <summary>
        /// 安全生产数据季报
        /// </summary>
        public const string SafetyQuarterlyReportMenuId = "A3894BAD-3F4A-4BB4-98CF-A76C588AE53F";

        /// <summary>
        /// 应急演练开展情况季报表
        /// </summary>
        public const string DrillConductedQuarterlyReportMenuId = "7985C759-8EB9-4B7D-AC65-00541280B46C";

        /// <summary>
        /// 应急演练工作计划半年报
        /// </summary>
        public const string DrillPlanHalfYearReportMenuId = "70DEB27A-D6FF-4D57-879B-0270F2967FA0";
        #endregion

        #region 安全信息分析
        /// <summary>
        /// 人工时费用分析
        /// </summary>
        public const string AnalyseWorkTimeCostMenuId = "598568A0-A338-499F-888C-1B73665837F9";

        /// <summary>
        /// 安全事故分析
        /// </summary>
        public const string AnalyseSafeAccidentMenuId = "8396C9E2-3376-4144-978A-CC6041EC6C6A";

        /// <summary>
        /// 安全隐患分析
        /// </summary>
        public const string AnalyseHiddenDangerMenuId = "5B645281-A055-4AA1-9245-DACBD984C76F";

        /// <summary>
        /// 资源来源统计
        /// </summary>
        public const string AnalyseResourceMenuId = "195D508D-E929-4B91-891E-307DC4E4338F";
        #endregion
        #endregion

        #region HSSE管理工作报告
        /// <summary>
        /// 管理月报
        /// </summary>
        public const string ServerMonthReportMenuId = "26CE4208-7DEE-46A2-A1D2-9C182D9C1DFC";
        /// <summary>
        /// 总部管理月报B
        /// </summary>
        public const string ServerMonthReportBMenuId = "B995396A-B01C-4F03-858A-FFDC853BA4B8";
        /// <summary>
        /// HSSE月总结
        /// </summary>
        public const string ServerManagerTotalMonthMenuId = "8051C9AA-801D-4001-9CB6-833CB407A169";
        /// <summary>
        /// 报表上报情况
        /// </summary>
        public const string ServerReportRemindMenuId = "D67D1C85-3798-47A9-A0DB-B4DB47FF2E7D";

        #endregion

        #region 企业安全大检查
        /// <summary>
        /// 安全监督检查报告
        /// </summary>
        public const string SuperviseCheckReportMenuId = "1C6F9CA9-FDAC-4CE5-A19C-5536538851E1";

        /// <summary>
        /// 安全监督检查整改
        /// </summary>
        public const string SuperviseCheckRectifyMenuId = "55976B16-2C33-406E-B514-2FE42D031071";
        #endregion

        #region 集团安全监督
        /// <summary>
        /// 企业上报监督检查报告
        /// </summary>
        public const string UpCheckReportMenuId = "B9950CB5-C47A-4C0A-A6CC-C7DDBBDE7D1E";

        /// <summary>
        /// 企业安全文件上报
        /// </summary>
        public const string SubUnitReportMenuId = "3D1CFA31-96A9-496E-9A94-318670C9D658";

        /// <summary>
        /// 集团下发监督检查整改
        /// </summary>
        public const string CheckRectifyMenuId = "4A87774E-FEA5-479A-97A3-9BBA09E4862E";

        /// <summary>
        /// 集团下发监督检查报告
        /// </summary>
        public const string CheckInfoReportMenuId = "091D7D24-E706-465A-95FD-8EF359CB8667";
        #endregion

        #region 绩效评价
        /// <summary>
        /// 绩效评价
        /// </summary>
        public const string ProjectEvaluationMenuId = "DEE90726-E00D-462B-A4BF-7E36180DD5B8";
        #endregion

        #region 职业健康
        /// <summary>
        /// 危害检测
        /// </summary>
        public const string ServerHazardDetectionMenuId = "D4802FF6-0AE0-4F9B-9D69-FD895CF9F5C0";

        /// <summary>
        /// 体检管理
        /// </summary>
        public const string ServerPhysicalExaminationMenuId = "DB06084F-742F-49F1-A9B9-1100919E49DB";

        /// <summary>
        /// 职业病事故
        /// </summary>
        public const string ServerOccupationalDiseaseAccidentMenuId = "52DA3277-DCC1-4612-8083-A576BF85953A";
        #endregion

        #region 环境保护
        /// <summary>
        /// 环境监测数据
        /// </summary>
        public const string ServerEnvironmentalMonitoringMenuId = "FD4E234C-265F-4B45-A35A-C9659AF9C173";

        /// <summary>
        /// 突发环境事件
        /// </summary>
        public const string ServerUnexpectedEnvironmentalMenuId = "6C36DBFF-E765-4FC9-B978-51ADBE696C10";

        /// <summary>
        /// 环境事件应急预案
        /// </summary>
        public const string ServerEnvironmentalEmergencyPlanMenuId = "6A8EAA9C-08E9-4F1F-B824-67B60D49258A";

        /// <summary>
        /// 环评报告
        /// </summary>
        public const string ServerEIAReportMenuId = "FB943BD9-33A5-4680-82C1-29A4741D8636";

        #endregion

        #region 安全事故
        /// <summary>
        /// 事故快报
        /// </summary>
        public const string ServerAccidentReportMenuId = "DC871FCA-FBA8-4533-B5D6-DF64BCE40287";

        /// <summary>
        /// 事故处理
        /// </summary>
        public const string ServerAccidentStatisticsMenuId = "BE2F6161-7C17-41FF-A314-8C0AE323D5A4";

        /// <summary>
        /// 事故统计
        /// </summary>
        public const string ServerAccidentAnalysisMenuId = "71A5556F-1590-4D4C-9A31-703DCD5C2910";

        /// <summary>
        /// 事故台账
        /// </summary>
        public const string ServerAccidentDataListMenuId = "6F2C0F0A-3CF6-4B28-AFE2-FB7415ECDB91";
        #endregion

        #region 信息管理
        /// <summary>
        /// 管理通知
        /// </summary>
        public const string ServerNoticeMenuId = "E2F56879-5337-4BEF-8113-62845DF616EF";

        /// <summary>
        /// 项目图片
        /// </summary>
        public const string ServerPictureMenuId = "278DF0FE-35E2-470F-9AE4-23C57EDF797F";
        #endregion

        #region 信息统计
        #endregion

        #region 企业安全管理资料
        /// <summary>
        /// 企业安全管理资料设置
        /// </summary>
        public const string ServerSafetyDataMenuId = "60E00925-3357-441E-BD2F-2DF8C91BDDE6";

        /// <summary>
        /// 企业安全管理资料考核计划
        /// </summary>
        public const string ServerSafetyDataPlanMenuId = "039BD08A-9C38-4C28-81EE-A6CA86F580B2";

        /// <summary>
        /// 项目企业安全管理资料
        /// </summary>
        public const string ServerProjectSafetyDataMenuId = "74A51BC9-EE10-4534-A4A7-37889B07753C";

        /// <summary>
        /// 企业安全管理资料考核
        /// </summary>
        public const string ServerSafetyDataCheckMenuId = "2A405839-FD14-4398-8AEE-48B44BFDA1F6";

        /// <summary>
        /// 公司安全人工时管理
        /// </summary>
        public const string ServerAccidentDataMenuId = "A139FF69-8B74-489B-AB5F-526B2207DD89";
        #endregion

        #region 文件柜
        /// <summary>
        /// 文件柜1(集团检查类)
        /// </summary>
        public const string ServerFileCabinetMenuId = "6621CF4A-EAD4-40AF-9FFE-51DA4348C10C";

        /// <summary>
        /// 文件柜1(内业)
        /// </summary>
        public const string ServerFileCabinetBMenuId = "DDD1CE30-F8B9-4011-A20F-7AC60B34788C";
        #endregion
        #endregion

        #region 项目菜单
        /// <summary>
        /// 班组信息
        /// </summary>
        public const string TeamGroupMenuId = "2C970C89-8C69-4A6C-B832-8A64B8A701CA";
        /// <summary>
        /// 作业区域
        /// </summary>
        public const string WorkAreaMenuId = "CBA3833A-C705-4B4E-A4A7-ACC27D0ECDCE";
        
        #region 隐患巡检
        /// <summary>
        /// 隐患登记
        /// </summary>
        public const string HiddenHazardMenuId = "F38CA739-CC67-4D9F-8293-77291AE7F056";

        /// <summary>
        /// 隐患记录
        /// </summary>
        public const string HiddenHazardRecordMenuId = "45ED2862-36B3-4F13-8670-31DFA7D5A174";

        /// <summary>
        /// 延期申请单
        /// </summary>
        public const string HiddenHazardDelayMenuId = "167E9508-3539-4CE8-827A-A1B35BDC6A0F";
        #endregion
 
        /// <summary>
        /// 特殊作业票父级菜单id
        /// </summary>
        public const string SuperLicenseMenuId = "2EAB7911-5788-4514-B95C-E1134315451D";

        /// <summary>
        /// LEC评价
        /// </summary>
        public const string LECMenuId = "0D8DC662-E052-4282-9FAE-3135320B65C9";
        /// <summary>
        /// SCL评价
        /// </summary>
        public const string SCLMenuId = "F3BCA935-3552-4307-AD14-A29C0CBA3AE2";
        /// <summary>
        /// JHA评价
        /// </summary>
        public const string JHAMenuId = "13C30DD1-740B-4D39-A889-B83E81CB6EDC";
        #endregion

        /// <summary>
        /// 不参与规则设置菜单
        /// </summary>
        public static List<string> noSysSetMenusList = new List<string>
       {  
           HiddenHazardRecordMenuId
       };

        #endregion

        #region 模版文件原始的虚拟路径
        /// <summary>
        /// 数据导入模版文件原始的虚拟路径
        /// </summary>
        public const string DataInTemplateUrl = "File\\Excel\\DataIn\\数据导入模版.xls";
        /// <summary>
        /// 人员模版文件原始的虚拟路径
        /// </summary>
        public const string PersonTemplateUrl = "File\\Excel\\DataIn\\人员信息模版.xls";
       
        #endregion

        #region 初始化上传路径

        /// <summary>
        /// Excel附件路径
        /// </summary>
        public const string ExcelUrl = "File\\Excel\\";

        #endregion
        
        #region 通用流程定义
        /// <summary>
        /// 待提交
        /// </summary>
        public const string State_0 = "0";
        /// <summary>
        /// 已提交
        /// </summary>
        public const string State_1 = "1";
        /// <summary>
        /// 已完成
        /// </summary>
        public const string State_2 = "2";
        /// <summary>
        /// 重新申请
        /// </summary>
        public const string State_3 = "3";
        #endregion
    }
}