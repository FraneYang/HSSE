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
        /// <summary>
        /// 归档
        /// </summary>
        public const string BtnFile = "归档";
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
        /// 单元
        /// </summary>
        public const string WorkAreaMenuId = "2CF9FBC1-66AF-408A-B38E-F5F4409D65F8";
        /// <summary>
        /// 班组信息
        /// </summary>
        public const string TeamGroupMenuId = "A1343045-0CDE-4F22-9C77-1C54EEEB2FEB";
        /// <summary>
        /// 职务信息
        /// </summary>
        public const string PositionMenuId = "1E81DF97-809E-479F-1111-508F2043BA69";
        /// <summary>
        /// 职称信息
        /// </summary>
        public const string PostTitleMenuId = "2E424093-81B8-421A-963F-D85D17B1E82A";
        /// <summary>
        /// 专业信息
        /// </summary>
        public const string SpecialtyMenuId = "724E082C-EDDA-40FF-B396-F5441C5F6B15";
        /// <summary>
        /// 标准信息
        /// </summary>
        public const string StandardMenuId = "5C9F0DA8-B6EE-4A19-954D-E57EBC880F7F";
        /// <summary>
        /// 特岗证书
        /// </summary>
        public const string CertificateMenuId = "3A40AF0B-C9B8-4AF9-A683-FEADD8CC3A1C";
        /// <summary>
        /// 准操项目
        /// </summary>
        public const string ProspectiveMenuId = "BB7788DF-2201-4AB2-BB98-028D6AC6679C";
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
        /// 作业风险对应值
        /// </summary>
        public const string OverhaulRiskGradeMenuId = "11829B4A-933C-448F-AB20-6CF9D939B964";       
        /// <summary>
        /// SCL菜单id
        /// </summary>
        public const string SCLInfoMenuId = "22F3B0D5-BFDF-4766-B473-9664B2568229";
        /// <summary>
        /// 动火方式菜单id
        /// </summary>
        public const string FireWorkModeMenuId = "B1F6FDD8-69B7-4EA9-B8CD-F4BBDA49F4DB";
        /// <summary>
        /// 作业活动菜单id
        /// </summary>
        public const string JobActivityMenuId = "7E1DC7F8-BB0C-44C8-8473-5EB7F11A3C33";
        /// <summary>
        /// 作业活动菜单id
        /// </summary>
        public const string JobEnvironmentMenuId = "A3807DF2-FA54-4DA6-A9D6-3CDF5C614BC8";
        /// <summary>
        /// 设备设施类型菜单id
        /// </summary>
        public const string EuipmentTypeMenuId = "F3642FBF-2941-4940-92A7-C0267CD5C1E7";
        /// <summary>
        /// 设备设施菜单id
        /// </summary>
        public const string EuipmentMenuId = "148B1CA0-0D3E-4783-B383-24ADD17A77A3";
        /// <summary>
        /// 动火分析点数据菜单id
        /// </summary>
        public const string FireWorkAnalysisDataMenuId = "854843F4-A58D-42F9-AB9E-0E35EB7252E1";
        /// <summary>
        /// 受限分析点数据菜单id
        /// </summary>
        public const string LimitedSpaceAnalysisDataMenuId = "9EFDE353-3EA2-4DA2-B67E-DCACB7649E29";
        /// <summary>
        /// 测评考核项
        /// </summary>
        public const string AppraisalItemMenuId = "ED77B573-7D87-4C52-ADBD-EFA9B9632F62";
        /// <summary>
        /// 施工方案类别
        /// </summary>
        public const string SpecialSchemeTypeMenuId = "999C0568-280B-469D-A0B6-F969515F207C";
        /// <summary>
        /// 应急救援类型
        /// </summary>
        public const string EmergencyTypeMenuId = "33A68820-9B17-4B34-AE43-061AEF3347EE";
        #endregion

        #region 公共资源
        #region 专家辅助
        /// <summary>
        /// 管理对象
        /// </summary>
        public const string ManagedObjectMenuId = "2F339420-ED38-443F-A10A-35328EB024A7";
        /// <summary>
        /// 管理项目
        /// </summary>
        public const string ManagedItemMenuId = "556BD50D-90C2-4D25-B170-A30EDB43155F";
        /// <summary>
        /// 安全标准
        /// </summary>
        public const string HSSEStandardMenuId = "F4B02718-0616-4623-ABCE-885698DDBEB1";        
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
        public const string TrainingEduMenuId = "5A02AC0C-5203-4AE9-A354-BFE6D4ED28D5";
        /// <summary>
        /// 考试试题库
        /// </summary>
        public const string TrainDBMenuId = "9D99A981-7380-4085-84FA-8C3B1AFA6202";      
        /// <summary>
        /// 事故案例库
        /// </summary>
        public const string AccidentCaseMenuId = "D86917DB-D00A-4E18-9793-C290B5BBA84C";
        /// <summary>
        /// 应知应会库
        /// </summary>
        public const string KnowledgeDBMenuId = "AB7A3D78-2D89-4488-97E3-8F8616BDDE30";
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

        #region 新闻动态
        /// <summary>
        /// 新闻动态
        /// </summary>
        public const string NewsMenuId = "8D728939-4A60-48CE-BFB0-984FA33939EE";
        #endregion

        #region 公文公告
        /// <summary>
        /// 公文公告
        /// </summary>
        public const string NoticesMenuId = "5392D9AB-2625-46EF-A960-5839A3C370B4";
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
        
        #region 业务菜单
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

        #region 作业票
        /// <summary>
        /// 特殊作业票父级菜单id
        /// </summary>
        public const string SuperLicenseMenuId = "2EAB7911-5788-4514-B95C-E1134315451D";
        /// <summary>
        /// 动火作业票菜单id
        /// </summary>
        public const string FireWorkMenuId = "27147897-4091-4EC5-96D3-2914ECDE0384";
        /// <summary>
        /// 检修作业票菜单id
        /// </summary>
        public const string OverhaulMenuId = "656C760C-27A8-402C-B875-E2B80CC6E577";
        /// <summary>
        /// 临时用电安全作业证
        /// </summary>
        public const string ElectricityMenuId = "68E502C6-3993-4669-8E4C-64E820AD5100";
        /// <summary>
        /// 盲板抽堵安全作业票
        /// </summary>
        public const string BlindPlateMenuId = "6A583619-B499-48A1-8BA1-6BC140741C06";
        /// <summary>
        /// 高处安全作业证
        /// </summary>
        public const string HeightWorkMenuId = "45A92A2E-0AD7-400F-A309-F70BBEF01617";
        /// <summary>
        /// 受限空间安全作业证
        /// </summary>
        public const string LimitedSpaceMenuId = "E8B4B410-74A3-45D7-8A73-9088FCB56959";
        /// <summary>
        /// 断路安全作业证
        /// </summary>
        public const string OpenCircuitMenuId = "20DFBC0F-2D53-4A3F-800F-CC4383A0B328";
        /// <summary>
        /// 动土安全作业证
        /// </summary>
        public const string BreakGroundMenuId = "94D5A962-CB10-4392-84ED-E62DF7D1778D";
        /// <summary>
        /// 吊装安全作业证
        /// </summary>
        public const string LiftingWorkMenuId = "D04F5626-7646-430F-9DBA-811B78401982";
        /// <summary>
        /// A级联锁变更审批单
        /// </summary>
        public const string AInterlockingMenuId = "F12FD53C-FE7F-4145-B138-EC641B138D5E";
        /// <summary>
        /// B级联锁变更审批单
        /// </summary>
        public const string BInterlockingMenuId = "EA045753-CD5C-4E4E-8FB6-80F556341885";
        #endregion

        #region 风险评价
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
        /// <summary>
        /// 风险信息库
        /// </summary>
        public const string RiskListMenuId = "5B0C3726-4D02-4576-B1F3-2EC9025DB72F";
        /// <summary>
        /// 分级管控预警
        /// </summary>
        public const string ClassificationMenuId = "302FF7DB-25AF-4D32-BC00-F6E617F13D76";
        /// <summary>
        /// 风险巡检记录
        /// </summary>
        public const string RoutingInspectionMenuId = "936B6704-B271-417F-B179-A3C7D0158864";
        #endregion

        #region 人员及资质管理
        /// <summary>
        /// 培训类别
        /// </summary>
        public const string TrainTypeMenuId = "DB977CEA-483E-45B5-A6CC-54FC5B1D5965";
        /// <summary>
        /// 信息采集
        /// </summary>
        public const string InformationCollectionMenuId = "BF4A3C5E-2249-400E-AD6B-949BF32F2BEF";
        /// <summary>
        /// 人员培训
        /// </summary>
        public const string TrainRecordMenuId = "D217B3D2-9DC6-4EBE-859F-288E78572FA0";
        /// <summary>
        /// 人员建档
        /// </summary>
        public const string PersonnelDocumentMenuId = "0F699545-F014-4EF0-8AD9-4D70FEA6D4C5";
        /// <summary>
        /// 进出场管理
        /// </summary>
        public const string UserEntryRecordMenuId = "A87DDA30-3667-4FBC-B3B5-94398299F93C";
        /// <summary>
        /// 人员资质
        /// </summary>
        public const string PersonQualityMenuId = "19F1A7DD-29DE-42DD-888C-528FF766B89F";
        /// <summary>
        /// 特种设备资质
        /// </summary>
        public const string EquipmentQualityMenuId = "D31F5ADF-E120-4F91-A823-A3FEB56111D9";
        /// <summary>
        /// 外委单位资质
        /// </summary>
        public const string SubUnitQualityMenuId = "70642242-A068-490C-B000-B03BA3A20C00";
        #endregion

        #region 核心部位禁动管理
        /// <summary>
        /// 电子锁分布台账
        /// </summary>
        public const string SmartLockMenuId = "F4B542CB-F53C-446D-BF8A-D9A3F8E23869";
        #endregion

        #region 培训与考试管理
        /// <summary>
        /// 培训计划
        /// </summary>
        public const string PlanMenuId = "B782A26B-D85C-4F84-8B45-F7AA47B3159E";
        /// <summary>
        /// 培训任务
        /// </summary>
        public const string TaskMenuId = "E108F75D-89D0-4DCA-8356-A156C328805C";
        /// <summary>
        /// 培训试题
        /// </summary>
        public const string TrainTestRecordMenuId = "6C314522-AF62-4476-893E-5F42C09C3077";
        /// <summary>
        /// 考试计划
        /// </summary>
        public const string TestPlanMenuId = "FAF7F4A4-A4BC-4D94-9E88-0CF5A380DB34";
        /// <summary>
        /// 考试记录
        /// </summary>
        public const string TestRecordMenuId = "0EEB138D-84F9-4686-8CBB-CAEAA6CF1B2A";
        /// <summary>
        /// 考试统计
        /// </summary>
        public const string TestStatisticsMenuId = "6FF941C1-8A00-4A74-8111-C892FC30A8DA";
        #endregion

        #region 综合测评
        /// <summary>
        /// 测评记录
        /// </summary>
        public const string PersonAppraisalMenuId = "E0EE52AF-05D9-4790-B265-14EA1E662E8B";
        #endregion

        #region 文件柜
        /// <summary>
        /// 文件柜
        /// </summary>
        public const string FileCabinetAMenuId = "C8C08302-17FF-4A50-8B10-5189E71588D9";
        #endregion

        #endregion

        #endregion

        #region 模版文件原始的虚拟路径
        /// <summary>
        /// 数据导入模版文件原始的虚拟路径
        /// </summary>
        public const string DataInTemplateUrl = "File\\Excel\\DataIn\\数据导入模版.xls";
        /// <summary>
        /// 人员模版文件原始的虚拟路径
        /// </summary>
        public const string UserTemplateUrl = "File\\Excel\\DataIn\\人员信息模版.xls";
        /// <summary>
        /// 人员资质模版文件原始的虚拟路径
        /// </summary>
        public const string PersonQualityTemplateUrl = "File\\Excel\\DataIn\\人员资质模版.xls";
        /// <summary>
        /// 专家辅助模版文件原始的虚拟路径
        /// </summary>
        public const string HSSEStandardTemplateUrl = "File\\Excel\\DataIn\\专家辅助模版.xls";
        /// <summary>
        /// 考试试题模版文件原始的虚拟路径
        /// </summary>
        public const string TrainingTemplateUrl = "File\\Excel\\DataIn\\考试试题模版.xls";
        /// <summary>
        /// 培训教材模版文件原始的虚拟路径
        /// </summary>
        public const string TrainingEduTemplateUrl = "File\\Excel\\DataIn\\培训教材模版.xls";
        /// <summary>
        /// 测评考核项模版文件原始的虚拟路径
        /// </summary>
        public const string AppraisalItemTemplateUrl = "File\\Excel\\DataIn\\测评考核项模版.xls";
        /// <summary>
        /// APP下载地址
        /// </summary>
        public const string APPImageUrl = "File\\Image\\APP下载地址.png";
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