using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace DXRFID.Class
{
    
    public class DB_Handle
    {
        DXRFID.Class.MD5_Encoding md5_encoding = new MD5_Encoding();

        
        /// <summary>
        /// 新用户插入
        /// </summary>
        /// <param name="opid"></param>
        /// <param name="name"></param>
        /// <param name="function"></param>
        /// <param name="pwd"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public string Insert_User(string opid, string name, string function, string pwd, string email)
        {
            //密码MD5加密
            md5_encoding.pwd = pwd;
            string pwd_encoding = md5_encoding.Encoding();
            return "INSERT INTO [dbo].[userinfo] ([opid],[name],[pwd],[function],e_mail)  VALUES('" + opid + "','" + name + "','" + pwd_encoding + "','" + function + "','" + email + "')";
        }

        /// <summary>
        /// 按OPID查询用户表保密信息（姓名，密码，验证邮箱,权限）
        /// </summary>
        /// <param name="opid"></param>
        /// <returns></returns>
        public string Select_User_ConfidentialInformation_ByOpid(string opid)
        {
            return "SELECT [name],[pwd],e_mail,premission  FROM [dbo].[userinfo] where opid='" + opid + "'";
        }

        /// <summary>
        /// 按OPID修改用户密码
        /// </summary>
        /// <param name="opid"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public string Update_Pwd(string opid, string pwd)
        {
            md5_encoding.pwd = pwd;
            string pwd_encoding = md5_encoding.Encoding();
            return "UPDATE [dbo].[userinfo] SET [pwd] = '" + pwd_encoding + "' WHERE opid = '" + opid + "'";
        }

        /// <summary>
        /// 查询用户表中所有信息（工号，姓名，部门，权限）
        /// </summary>
        /// <returns></returns>
        public string Select_UserAllInformation()
        {
            return "SELECT [opid] 工号,[name] 姓名,[function] 部门,[e_mail] 邮箱,(case [premission] when 'normal' then '普通用户'  when 'root' then '管理员' else '超级管理员' end) as 权限 FROM [dbo].[userinfo] where opid!='180731559' order by opid";
        }

        /// <summary>
        /// 按用户输入的值模糊查询用户信息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Select_UserInformation(string key, string value)
        {
            return @"SELECT [opid] 工号,[name] 姓名,[function] 部门,[e_mail] 邮箱,(case [premission] when 'normal' then '普通用户'  when 'root' then '管理员' else '超级管理员' end) as 权限 FROM [dbo].[userinfo]
                    where " + key + " like '%" + value + "%' order by opid";
        }

        /// <summary>
        /// 按OPID升级用户权限
        /// </summary>
        /// <param name="opid"></param>
        /// <returns></returns>
        public string Update_Premission_Up(string opid)
        {
            return "UPDATE [dbo].[userinfo] SET [premission] = 'root' WHERE opid='" + opid + "'";
        }

        /// <summary>
        /// 按OPID取消管理员权限
        /// </summary>
        /// <param name="opid"></param>
        /// <returns></returns>
        public string Update_Premission_Down(string opid)
        {
            return "UPDATE [dbo].[userinfo] SET [premission] = 'normal' WHERE opid='" + opid + "'";
        }

        /// <summary>
        /// 插入入库设备信息
        /// </summary>
        /// <param name="AssetNumber"></param>
        /// <param name="RFID"></param>
        /// <param name="EquipmentName"></param>
        /// <param name="Quantity"></param>
        /// <param name="Specification"></param>
        /// <param name="Brands"></param>
        /// <param name="StoragePlace"></param>
        /// <param name="Keepers"></param>
        /// <param name="Descriptions"></param>
        /// <param name="pic"></param>
        /// <param name="LoginName"></param>
        /// <returns></returns>
        public string Insert_EquipmentMsg(string AssetNumber, string RFID, string EquipmentName, int Quantity, string Specification, string Brands, string StoragePlace, string Keepers, string Descriptions, string pic, string LoginName)
        {
            return @"INSERT INTO [dbo].[EquipmentInformation] ([AssetNumber],[RFID],[EquipmentName],[Quantity],[Specification],[Brand],[StoragePlace],[Keeper],[Description],[picture],ruku_datetime,ruku_person,Equipmentstatus) 
                     VALUES('" + AssetNumber + "','" + RFID + "','" + EquipmentName + "','" + Quantity + "','" + Specification + "','" + Brands + "','" + StoragePlace + "','" + Keepers + "','" + Descriptions + "','" + pic + "','" + System.DateTime.Now.ToString() + "','" + LoginName + "','在库')";
        }

        /// <summary>
        /// 按RFID查询在库信息
        /// </summary>
        /// <param name="RFID"></param>
        /// <returns></returns>
        public string Select_InstoreEquipmentMsg_ByRFID(string RFID)
        {
            return @"SELECT * FROM [dbo].[EquipmentInformation] WHERE RFID='" + RFID + "' and [EquipmentStatus] = '在库'";
        }

        /// <summary>
        /// 按RFID查询设备是否借出
        /// </summary>
        /// <param name="RFID"></param>
        /// <returns></returns>
        public string Select_BorrowMsg_ByRFID(string RFID)
        {
            return @"SELECT top 1 * FROM [dbo].[EquipmentInformation] a inner join [dbo].[BorrowInformation] b on a.RFID=b.RFID and a.RFID='" + RFID + "' and a.[EquipmentStatus] = '借出' order by Borrow_Datetime desc";
        }

        /// <summary>
        /// 按RFID和状态检查设备是否存在
        /// </summary>
        /// <param name="RFID"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public string Check_RFID_ByStatus(string RFID, string Status)
        {
            return "SELECT [AssetNumber] FROM [dbo].[EquipmentInformation] where RFID='" + RFID + "' and [EquipmentStatus] = '" + Status + "'";
        }

        /// <summary>
        /// 按RFID插入借出信息
        /// </summary>
        /// <param name="RFID"></param>
        /// <param name="Borrow_Person"></param>
        /// <param name="Borrow_Reason"></param>
        /// <param name="Borrow_Cycle"></param>
        /// <param name="LoginName"></param>
        /// <returns></returns>
        public string Insert_BorrowMsg_ByRFID(string RFID, string Borrow_Person, string Borrow_Reason, int Borrow_Cycle, string LoginName)
        {
            return "INSERT INTO [dbo].[BorrowInformation] ([RFID],[Borrow_Datetime],[Borrow_Person],[Borrow_Cycle],[Borrow_Reason],[Operator]) VALUES ('" + RFID + "','" + System.DateTime.Now.ToString() + "','" + Borrow_Person + "','" + Borrow_Cycle + "','" + Borrow_Reason + "','" + LoginName + "');" + Update_EquipmentStatus_ByRFID(RFID, "借出");
        }

        /// <summary>
        /// 按RFID修改设备状态
        /// </summary>
        /// <param name="RFID"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public string Update_EquipmentStatus_ByRFID(string RFID, string Status)
        {
            return "UPDATE [dbo].[EquipmentInformation] SET  [EquipmentStatus] = '" + Status + "' WHERE RFID='" + RFID + "'";
        }

        /// <summary>
        /// 按RFID插入归还信息
        /// </summary>
        /// <param name="RFID"></param>
        /// <param name="Return_Person"></param>
        /// <param name="LoginName"></param>
        /// <returns></returns>
        public string Insert_ReturnMsg_ByRFID(string RFID, string Return_Person, string LoginName)
        {
            return "INSERT INTO [dbo].[ReturnInformation] ([RFID],[Return_Datetime],[Return_Person],[Operator]) VALUES('" + RFID + "','" + System.DateTime.Now.ToString() + "','" + Return_Person + "','" + LoginName + "');" + Update_EquipmentStatus_ByRFID(RFID, "在库");
        }

        /// <summary>
        /// 按用户输入的条件及关键字模糊查询在库信息
        /// </summary>
        public string Select_InStoreMsg_ByCustromsKeyValue(string key,string value)
        {
            return @"SELECT [AssetNumber] 资产编码,[RFID],[EquipmentName] 名称,[Specification] 规格,[Brand] 品牌,[Quantity] 数量,[Keeper] 保管人,[StoragePlace] 存放地点,[ruku_datetime] 入库时间,[ruku_person] 入库人,CheckTime as 上次校验时间,CheckPerson as 校验负责人,[EquipmentStatus] 设备状态 FROM [dbo].[EquipmentInformation] where "+ key + " LIKE '%"+ value + "%' order by RFID";
        }

        public string Select_AllInStoreMsg()
        {
            return @"SELECT [AssetNumber] 资产编码,[RFID],[EquipmentName] 名称,[Specification] 规格,[Brand] 品牌,[Quantity] 数量,[Keeper] 保管人,[StoragePlace] 存放地点,[ruku_datetime] 入库时间,[ruku_person] 入库人,CheckTime as 上次校验时间,CheckPerson as 校验负责人,[EquipmentStatus] 设备状态 FROM [dbo].[EquipmentInformation] order by RFID";
        }

        /// <summary>
        /// 按用户输入的条件及关键字模糊查询借出历史信息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Select_BorrowMsg_ByCustromsKeyValue(string key, string value)
        {
            return @"SELECT [AssetNumber] 资产编码,a.[RFID],[EquipmentName] 名称,[Specification] 规格,[Brand] 品牌,[Quantity] 数量,[Keeper] 保管人,[StoragePlace] 存放地点,b.[Borrow_Person] 借出人,
                    b.[Borrow_Datetime] 借出时间,b.[Borrow_Cycle] 借出周期,b.[Borrow_Reason] 借出原因,b.[Operator] 借出操作人 FROM [dbo].[EquipmentInformation]
                    a inner join [dbo].[BorrowInformation] b on a.[RFID] = b.[RFID] and " + key + " LIKE '%" + value + "%' order by RFID";
        }

        /// <summary>
        /// 按用户输入的条件及关键字模糊查询归还历史信息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Select_ReturnMsg_ByCustromsKeyValue(string key, string value)
        {
            return @"SELECT [AssetNumber] 资产编码,a.[RFID],[EquipmentName] 名称,[Specification] 规格,[Brand] 品牌,[Quantity] 数量,[Keeper] 保管人,[StoragePlace] 存放地点,b.[Return_Person] 归还人,
                        b.[Return_Datetime] 归还时间,b.[Operator] 归还操作人 FROM
                         [dbo].[EquipmentInformation] a inner join [dbo].[ReturnInformation] b on a.[RFID] = b.[RFID]  and " + key + " LIKE '%" + value + "%' order by RFID";
        }

        /// <summary>
        /// 检查盘点日期是否已到
        /// </summary>
        public string Check_TakeStockTime()
        {
            return @"SELECT TOP 1 [TakeStockStartTime] FROM [dbo].[TakeStock_Admin] order by [TakeStockStartTime] asc";
        }

        /// <summary>
        /// 将所有盘点绑定关系清除
        /// </summary>
        public string Delete_TakeStockAdmin()
        {
            return @"DELETE FROM [dbo].[TakeStock_Admin]";
        }

        /// <summary>
        /// 查询所有待盘点设备信息
        /// </summary>
        /// <returns></returns>
        public string Select_AllTakeStockStatus()
        {
            return @"SELECT [AssetNumber] 资产编码,[RFID],[EquipmentName] 名称,[Specification] 规格,[Brand] 品牌,[Quantity] 数量,[Keeper] 保管人,[CurrentTakeStock_DateTime] 盘点时间,[CurrentTakeStock_StoragePlace] 盘点存放位置,(case [CurrentTakeStock_Person] when '无' then '未盘点' else '已盘点' end) 盘点状态 FROM [dbo].[EquipmentInformation] a inner join [dbo].[TakeStock_Admin] b
                    on a.[StoragePlace] = b.StoragePlace_Name and b.[TakeStockStartTime] <= (" + Check_TakeStockTime() + ") order by RFID";
        }

        /// <summary>
        /// 计算盘点设备状态总数
        /// </summary>
        /// <returns></returns>
        public string Select_TakeStockCount()
        {
            return "SELECT (case [CurrentTakeStock_Person] when '无' then '未盘点' else '已盘点' end) 盘点状态,count(*) FROM [dbo].[EquipmentInformation] a inner join[dbo].[TakeStock_Admin] b on a.StoragePlace = b.StoragePlace_Name and b.[TakeStockStartTime] <= (" + Check_TakeStockTime() + ") group by[CurrentTakeStock_Person]";
        }

        /// <summary>
        /// 按盘点时间查询盘点历史
        /// </summary>
        /// <param name="TakeStockDatetime"></param>
        /// <returns></returns>
        public string Select_TakeStockHistory_ByTakeStockTime(string TakeStockDatetime)
        {
            return @"SELECT b.[AssetNumber] 资产编码,b.[RFID],b.[EquipmentName] 名称,b.[Quantity] 数量,b.[Specification] 规格,b.[Brand] 品牌,b.[StoragePlace] 系统存放地点,b.[Keeper] 保管人,a.[Current_StoragePlace] 盘点存放地点,a.[TakeStock_Person] 盘点人
                        FROM [dbo].[TakeStockInformation] a inner join [dbo].[EquipmentInformation] b on a.[EquirementRFID]=b.[RFID] and Convert(Nvarchar,a.[TakeStock_DateTime],120) like '%" + TakeStockDatetime + "%' order by b.RFID";
        }
        
        /// <summary>
        /// 盘点管理信息插入
        /// </summary>
        /// <returns></returns>
        public string Insert_TaskStockAdminMsg()
        {
            return @"INSERT INTO [dbo].[TakeStock_Admin] ([StoragePlace_RFID],[StoragePlace_Name],[TakeStock_Leader],[TakeStockStartTime]) VALUES(@区域RFID标记,@StoragePlace,@区域盘点负责人,@预计盘点开始时间)";
        }

        /// <summary>
        /// 修改设备信息
        /// </summary>
        /// <param name="StoragePlace"></param>
        /// <param name="Keeper"></param>
        /// <param name="RFID"></param>
        /// <param name="LoginName"></param>
        /// <returns></returns>
        public string Update_EquipmentInformation(string StoragePlace, string Keeper,string RFID, string LoginName,string pic)
        {
            return @"UPDATE 
SET [StoragePlace] = '" + StoragePlace + "',[Keeper] = '" + Keeper + "',[picture]='" + pic + "' where [RFID]='" + RFID + "';" +
                "INSERT INTO [dbo].[UpdateEquipmentInformationHistory] ([RFID],[StoragePlace],[Keeper],[picture],[Update_DateTime],[Update_Person]) VALUES ('" + RFID + "','" + StoragePlace + "','" + Keeper + "','" + pic + "','" + System.DateTime.Now.ToString() + "','" + LoginName + "')";
        }

        /// <summary>
        /// 按RFID查询设备修改历史信息
        /// </summary>
        /// <param name="RFID"></param>
        /// <returns></returns>
        public string Select_UpdateEquipmentInformation_ByRFID(string RFID)
        {
            return @"SELECT [AssetNumber] 资产编码,a.[RFID],[EquipmentName] 名称,[Specification] 规格,[Brand] 品牌,[Quantity] 数量,a.[Keeper] 原保管人,a.[StoragePlace] 原存放地点,b.[StoragePlace] 现存放地点,
                        b.[Keeper] 现保管人,[Update_DateTime] 修改时间,[Update_Person] 修改人 FROM [dbo].[EquipmentInformation] a inner join [dbo].[UpdateEquipmentInformationHistory] b on a.RFID=b.RFID
                         where a.RFID='" + RFID + "' order by [Update_DateTime] desc";
        }

        public string Select_EquipmentPic_ByRFID(string RFID)
        {
            return @"SELECT [AssetNumber],[EquipmentName],[picture] FROM [dbo].[EquipmentInformation] WHERE RFID='" + RFID + "'";
        }
    }
}