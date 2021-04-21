using Bootstrap.Security.DataAccess;
using PetaPoco;
using System;
using System.Collections.Generic;
using Longbow.Web.Mvc;
using System.Linq;
using Longbow.Data;
using Bootstrap.Client.DataAccess.Helper;

namespace Bootstrap.Client.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    public class NormalRouteList
    {
        /// <summary>
        /// DockNo
        /// </summary>
        public string DockNo { get; set; }
        /// <summary>
        /// ToFacility
        /// </summary>
        public string ToFacility { get; set; }
        /// <summary>
        /// FacilityName
        /// </summary>
        public string FacilityName { get; set; }
        /// <summary>
        /// OrderType
        /// </summary>
        public string OrderType { get; set; }
        /// <summary>
        /// ExpectDate
        /// </summary>
        public DateTime DoRouteDate { get; set; }
        /// <summary>
        /// ExpectDate
        /// </summary>
        public DateTime ExpectDate { get; set; }
        /// <summary>
        /// ExpectTime
        /// </summary>
        public string ExpectTime { get; set; }
        /// <summary>
        /// Receiver
        /// </summary>
        public string Receiver { get; set; }
        /// <summary>
        /// Driver
        /// </summary>
        public string Driver { get; set; }
        /// <summary>
        /// DriverPhone
        /// </summary>
        public string DriverPhone { get; set; }
        /// <summary>
        /// Notes
        /// </summary>
        public string Notes { get; set; }
        /// <summary>
        /// VehicleKey
        /// </summary>
        public string VehicleKey { get; set; }
        /// <summary>
        /// DriveTimes
        /// </summary>
        public int DriveTimes { get; set; }
        /// <summary>
        /// VLListCount
        /// </summary>
        public int VLListCount { get; set; }
        /// <summary>
        /// VLListPrintDate
        /// </summary>
        public DateTime? VLListPrintDate { get; set; }
        /// <summary>
        /// AddDate
        /// </summary>
        public DateTime? AddDate { get; set; }
        /// <summary>
        /// AddWho
        /// </summary>
        public string AddWho { get; set; }
        /// <summary>
        /// EditDate
        /// </summary>
        public DateTime? EditDate { get; set; }
        /// <summary>
        /// EditWho
        /// </summary>
        public string EditWho { get; set; }
        /// <summary>
        /// ToWMS
        /// </summary>
        public string ToWMS { get; set; }
        /// <summary>
        /// RouteNo
        /// </summary>
        public string RouteNo { get; set; }
        /// <summary>
        /// Storerkey
        /// </summary>
        public string StorerKey { get; set; }
        /// <summary>
        /// TMSKey
        /// </summary>
        public string TMSKey { get; set; }
        /// <summary>
        /// ExternOrderkey
        /// </summary>
        public string ExternOrderKey { get; set; }
        /// <summary>
        /// ExternOrderkey
        /// </summary>
        public DateTime? OrderDate { get; set; }
        /// <summary>
        /// ExternOrderkey
        /// </summary>
        public DateTime? DeliveryDate { get; set; }
        /// <summary>
        /// ExternOrderkey
        /// </summary>
        public string ConsigneeKey { get; set; }
        /// <summary>
        /// ExternOrderkey
        /// </summary>
        public string ShortName { get; set; }
        /// <summary>
        /// ExternOrderkey
        /// </summary>
        public string ShipToAddress { get; set; }
        /// <summary>
        /// OrderQty
        /// </summary>
        public double OrderQty { get; set; }
        /// <summary>
        /// CaseQty
        /// </summary>
        public double CaseQty { get; set; }
        /// <summary>
        /// PalletQty
        /// </summary>
        public double PalletQty { get; set; }
        /// <summary>
        /// ShipQty
        /// </summary>
        public int ShipQty { get; set; }
        /// <summary>
        /// Weight
        /// </summary>
        public double Weight { get; set; }
        /// <summary>
        /// Cube
        /// </summary>
        public double Cube { get; set; }

        public virtual IEnumerable<NormalRouteList> Retrieves(string facility) {
            var strSQL = new Sql( 
                "select distinct " +
                "RouteNo = rh.RouteNo " +
                ",OrderType = o.OrderType " +
                ",DoRouteDate = convert(char(10),br.DoRouteDate,111) " +
                ",ExpectDate = convert(char(10),br.ExpectDate,111) " +
                ",ExpectTime = br.ExpectTime " +
                ",CaseQty = br.CaseQty " +
                ",PalletQty = br.PalletQty " +
                ",Cube = br.Cube " +
                ",Weight = br.Weight " +
                ",DockNo = rtrim(br.DockNo) " +
                ",VehicleKey = br.VehicleKey " +
                ",Driver = rtrim(br.Driver) " +
                ",DriverPhone = rtrim(br.DriverPhone) " +
                ",Notes = rtrim(br.Notes) " +
                ",DriveTimes = br.DriveTimes " +
                ",VLListCount = br.VLListCount " +
                ",br.VLListPrintDate " +
                ",AddDate = br.AddDate " +
                ",AddWho = rtrim(br.AddWho) " +
                ",EditDate = br.EditDate " +
                ",EditWho = rtrim(br.EditWho) " +
                ",ToWMS = br.ToWMS " +
                ",ToFacility = rtrim(o.ToFacility) " +
                ",FacilityName = rtrim(bf.Description) " +
            "from " +
                "Orders o " +
                "join RouteHeader rh on o.TMSKey = rh.TMSKey " +
                "join BestRoute br on rh.RouteNo = br.RouteNo " +
                "left join BaseFacility bf on o.ToFacility = bf.Facility ");

            strSQL.Where("o.Status = '2' and br.Status = '0'");

            if (!string.IsNullOrEmpty(facility)) 
            {
                strSQL.Where("rh.Facility = @0", facility);
            }

            return DbManager.Create("bestlogtms").Fetch<NormalRouteList>(strSQL);

        }

        public virtual IEnumerable<NormalRouteList> RetrieveDetailByRouteNo(string RouteNo, string facility) {
            var strSQL = new Sql(
                "select " +
                    "RouteNo = rd.RouteNo " +
                    ",StorerKey = rtrim(rd.StorerKey) " +
                    ",TMSKey = rd.TMSKey " +
                    ",ExternOrderKey = rtrim(rd.ExternOrderKey) " +
                    ",OrderDate = convert(char(10),o.OrderDate,111) " +
                    ",DeliveryDate = convert(char(10),o.DeliveryDate,111) " +
                    ",ConsigneeKey = rtrim(o.ConsigneeKey) " +
                    ",ShortName = rtrim(o.ShortName) " +
                    ",ShipToAddress = rtrim(o.ShipToAddress) " +
                    ",OrderQty = sum(rd.OrderQty) " +
                    ",ShipQty = sum(rd.ShipQty) " +
                    ",CaseQty = sum(rd.CaseQty) " +
                    ",PalletQTY = sum(rd.PalletQty) " +
                    ",Cube = sum(rd.Cube) " +
                    ",Weight = sum(rd.Weight) " +
                    ",Notes = rtrim(o.Notes) " +
                "from " +
                    "Orders o " +
                    "join RouteDetail rd on o.TMSKey = rd.TMSKey " +
                    "join BestRoute br on rd.RouteNo = br.RouteNo "
            );
            strSQL.Where("o.Status = '2' and br.Status = '0'");
            strSQL.Where("rd.RouteNo = @0", RouteNo);
            strSQL.GroupBy("rd.RouteNo,rtrim(rd.Storerkey),rd.TMSKey,rtrim(rd.ExternOrderkey),convert(char(10),o.OrderDate,111) " +
                    ",convert(char(10),o.DeliveryDate,111),rtrim(o.ConsigneeKey),rtrim(o.ShortName),rtrim(o.ShipToAddress),rtrim(o.Notes)");
            return DbManager.Create("bestlogtms").Fetch<NormalRouteList>(strSQL);
        } 
 

        public virtual bool DeleteRoutes(IEnumerable<string> Routes, string UserName, IEnumerable<string> Role)
        {
            bool ret = false;
            if (string.IsNullOrEmpty(UserName)) return ret; //20200928 Modified by Andy. 使用者為空值則直接跳過
            var db = DbManager.Create("bestlogtms"); 
            string strSQL = ""; 
            var blRole = Role.Contains("TMS_Supervisor");
            var routes = string.Join(",", Routes.Select(p => string.Format("'{0}'", p.ToString())).ToArray());
            try
            {
                db.BeginTransaction(); 

                //每個排車者只能刪除自己建立的路編
                //20200928 Modified by Andy. DISTINCT 取得排車者
                strSQL = 
                    "select DISTINCT AddWho " +
                        "from BestRoute " + 
                        "where RouteNo in (" + routes + ")";
                var CreateUser = db.Fetch<string>(strSQL);

                if(CreateUser.Count != 1 && !blRole) //20200928 Modified by Andy. 必須要等於1 如果回傳為0也不能繼續
                {
                    ret = false;
                    return ret;
                }
                string strCreateUser = CreateUser.FirstOrDefault(); //20200928 Modified by Andy. 改為Linq抓取值
                if(string.IsNullOrEmpty(strCreateUser) || strCreateUser.Trim().ToUpper() != UserName.Trim().ToUpper() && !blRole)
                {
                    ret = false;
                    return ret;
                }
                
                //將訂單資料回復; //更新為1：原倉別待排，更新為3：轉運站待排
                //需在排車資料刪除前更新
                db.Execute($"update orders set Status = case when o.Facility = rh.Facility then '1' else '3' end,ToFacility = rh.Facility from Orders o join RouteHeader rh on o.TMSKey = rh.TMSKey where rh.RouteNo in ({routes})");

                //刪除排車資料
                db.Execute($"delete from BestRoute where RouteNo in ({routes})");
                db.Execute($"delete from RouteHeader where RouteNo in ({routes})");
                db.Execute($"delete from RouteDetail where RouteNo in ({routes})");

                db.CompleteTransaction();
                ret = true;
            }
            catch (Exception ex)
            {
                db.AbortTransaction();
                throw ex;
            }
            return ret;
        }

        public virtual bool CheckBeforeCarLeave(IEnumerable<string> Routes, string facility)
        {
            var ret = true;
            var db = DbManager.Create("bestlogtms");
            if (Routes.Count() == 0) return ret;
            string strSQL = "";
            var routes = string.Join(",", Routes.Select(p => string.Format("'{0}'", p.ToString())).ToArray());

            //檢查I單是否已經扣帳
            strSQL = 
                "select * " + 
                "from Orders o " + 
                "join BestLogWMS..Orders wo on o.UpdateSource = wo.OrderKey " + 
                "join RouteHeader rh on o.TMSKey = rh.TMSKey " + 
                "where rh.RouteNo in (" + routes + ") and o.OrderType = 'I' and wo.status <> '9' ";
            var checkroute = db.Fetch<string>(strSQL);
            if(checkroute.Count > 0) ret = false;
            return ret;
        }
        public virtual bool CheckCarLeave(IEnumerable<string> Routes, string facility)
        {
            var ret = true;
            var db = DbManager.Create("bestlogtms");
            if (Routes.Count() == 0) ret = false;
            string strSQL = "";
            var routes = string.Join(",", Routes.Select(p => string.Format("'{0}'", p.ToString())).ToArray());

            //檢查是否已出車
            strSQL = 
                "select * from OrderTrack " + 
                "where RouteNo in (" + routes + ") ";
            var CheckRouteNo = db.Fetch<string>(strSQL);
            if(CheckRouteNo.Count > 0) ret = false;
            return ret;
        }

        public virtual bool CarLeave(IEnumerable<string> Routes, string UserName)
        {
            bool ret = false;
            var db = DbManager.Create("bestlogtms");  
            string strSQL = "";
            var routes = string.Join(",", Routes.Select(p => string.Format("'{0}'", p.ToString())).ToArray());
            try
            {
                db.BeginTransaction(); 



                //更新為4：出車至送貨點(排車未勾選轉運，o.ToFacility會更新為空值)，更新為3：出車至轉運站
                strSQL = 
                    "update Orders set " + 
                        "Status = case when o.ToFacility = '' then '4' else '3' end " + 
                        ",OriginalRouteNo = rh.RouteNo " + 
                    "from Orders o " + 
                        "join RouteHeader rh on o.TMSKey = rh.TMSKey " + 
                    "where rh.RouteNo in (" + routes + ") and rtrim(o.Status) = '2'";
                db.Execute(strSQL);

                //更新出車狀態、出車時間
                strSQL = "update BestRoute set CarLeaveDate = getdate(),Status = '1' where RouteNo in (" + routes + ")";
                db.Execute(strSQL);

                //Insert OrderTrack(訂單歷史紀錄)
                strSQL = 
                    "insert into OrderTrack (RouteNo,TMSKey,Sequence,FromFacility,ToFacility,StartAddress,EndAddress,Driver,VehicleKey,Receiver,OrderType,AddDate,AddWho) " + 
                    "select " +
                        "RouteNo = rh.RouteNo " +
                        ",TMSKey = o.TMSKey " +
                        ",Sequence = isnull(max(ot.Sequence),0) + 1 " +
                        ",FromFacility = rtrim(rh.Facility) " +
                        ",ToFacility = rtrim(o.ToFacility) " +
                        ",StartAddress = rtrim(bff.Address) " +
                        ",EndAddress = case when o.Status = '4' then rtrim(o.ShipToAddress) else rtrim(bft.Address) end " +
                        ",Driver = rtrim(br.Driver) " +
                        ",VehicleKey = rtrim(br.VehicleKey) " +
                        ",Receiver = rtrim(br.Receiver) " +
                        ",OrderType = rtrim(o.OrderType) " +
                        ",AddDate = GETDATE() " +
                        ",AddWho = '" + UserName + "' " +
                    "from RouteHeader rh " +
                        "join Orders o on rh.TMSKey = o.TMSKey " +
                        "join BestRoute br on rh.RouteNo = br.RouteNo " +
                        "join BaseFacility bff on bff.Facility = rh.Facility " +
                        "left join BaseFacility bft on bft.Facility = o.ToFacility " +
                        "left join OrderTrack ot on o.TMSKey = ot.TMSKey and ot.RouteNo = rh.RouteNo " +
                    "where rh.RouteNo in (" + routes + ") " + 
                    "group by rh.RouteNo,o.TMSKey,rtrim(rh.Facility),rtrim(o.ToFacility) " +
                        ",rtrim(br.Driver),rtrim(br.VehicleKey),rtrim(o.OrderType),rtrim(br.Receiver) " +
                        ",rtrim(bff.Address),case when o.Status = '4' then rtrim(o.ShipToAddress) else rtrim(bft.Address) end";
                db.Execute(strSQL);


                //RC單在出車後將出貨量更新為訂單量
                strSQL = 
                    $@"update OrderDetail set ShipQty = OrderQty 
                        from Orders o 
                            join OrderDetail od on o.TMSKey = od.TMSKey 
                            join OrderTrack ot on o.TMSKey = o.TMSKey 
                        where o.OrderType = 'RC' and ot.RouteNo in ({routes}) ";
                db.Execute(strSQL);

                db.CompleteTransaction();
                ret = true;
            }
            catch (Exception ex)
            {
                db.AbortTransaction();
                throw ex;
            }
            return ret;
        }

        public virtual bool ReturnWMS(IEnumerable<string> Routes, string UserName)
        {
            bool ret = false;
            var db = DbManager.Create("bestlogtms");
            string strSQL = "";  
            var routes = string.Join(",", Routes.Select(p => string.Format("'{0}'", p.ToString())).ToArray());
            try
            {
                db.BeginTransaction(); 

                //更新回傳狀態，及WMS Orders的 Route欄位
                strSQL = 
                    "update RouteHeader set " + 
                        "ToWMS = '1' " + 
                    "from Orders o " + 
                        "join RouteHeader rh on o.TMSKey = rh.TMSKey " + 
                    "where o.ToWMS = '0' and o.Status = '2' and rh.RouteNo in (" + routes + ")";
                db.Execute(strSQL);
                
                //路編因排車系統統一可能會導致流水號3碼不夠，所以增加1碼流水號，變為11碼
                //Exceed Orders Route欄位 僅有10碼，所以更新時拿掉'F'
                strSQL = 
                    "update BestLogWMS..Orders set Route = replace(rh.RouteNo,'F',''),Door = rtrim(br.DockNo) " + 
                    "from Orders o " +
                        "join RouteHeader rh on o.TMSKey = rh.TMSKey " +
                        "join BestRoute br on rh.RouteNo = br.RouteNo " + 
                        "join BestLogWMS..Orders os on o.UpdateSource = os.OrderKey " + 
                    "where o.ToWMS = '0' and o.Status = '2' and rh.RouteNo in (" + routes + ")";
                db.Execute(strSQL);

                //更新回傳狀態
                strSQL = 
                    "update BestRoute set ToWMS = '1' " +
                    "from Orders o " +
                    "join RouteHeader rh on o.TMSKey = rh.TMSKey " +
                    "join BestRoute br on br.RouteNo = rh.RouteNo " + 
                    "where o.ToWMS = '0' and o.Status = '2' and rh.RouteNo in (" + routes + ")";

                db.Execute(strSQL);
                
                //Orders 的 ToWMS須最後更新 否則上面兩個SQL會無法成立
                strSQL = 
                    "update Orders set " + 
                        "ToWMS = '1' " + 
                    "from Orders o " + 
                        "join RouteHeader rh on o.TMSKey = rh.TMSKey " + 
                    "where o.ToWMS = '0' and o.Status = '2' and rh.RouteNo in (" + routes + ")";
                db.Execute(strSQL);


                db.CompleteTransaction();
                ret = true;
            }
            catch (Exception ex)
            {
                db.AbortTransaction();
                throw ex;
            }
            return ret;
        }


        public virtual bool UpdateRouteNo(UpdateRouteNoBody value, string UserName)
        {
            bool ret = false;
            var db = DbManager.Create("bestlogtms");
            try
            {
                db.BeginTransaction();
                //更新路編資料
                db.Execute(@"
                    UPDATE BestRoute SET
                        DoRouteDate = @0, DockNo = @1, ExpectDate = @2, ExpectTime = @3, Receiver = @4
                        , Driver = @5, DriverPhone = @6, CompanyKey = @7, VehicleKey = @8
                        , EditDate = @9, EditWho = @10
                    WHERE RouteNo = @11
                    ",
                    value.DoRouteDate, value.DockNo, value.ExpectDate, value.ExpectTime, value.Driver.Receiver,
                    value.Driver.Driver, value.Driver.DriverPhone, value.Driver.CompanyKey, value.Driver.VehicleKey,
                    DateTime.Now, UserName, 
                    value.RouteNo
                );
                db.CompleteTransaction();
                ret = true;
            }
            catch (Exception ex)
            {
                db.AbortTransaction();
                throw ex;
            }
            return ret;
        }
    }
}

