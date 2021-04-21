using Bootstrap.Client.DataAccess;
using Longbow.Web.Mvc;
using System.Linq;
using System;
using Bootstrap.Client.DataAccess.Helper;
using System.Collections.Generic;

namespace Bootstrap.Client.Query
{
    /// <summary>
    /// 
    /// </summary>
    public class QueryBaseVehicleOption : CustomPaginationOption
    {
        /// <summary>
        /// 
        /// </summary>
        public string VehicleKey { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string xZIP { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string xAREA_CODE { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CompanyKey { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string VehicleType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string LoadingWeight { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string LoadingCube { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Driver { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DriverPhone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int LoadingPallet { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public float CarWeight { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BoxType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public float CarHeight { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string UnloadingType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string EmployType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string xCar_Type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Receiver { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PND { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public float xAPFix { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ApproveWho { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ApproveDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ConfirmWho { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ConfirmDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Active { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AddWho { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? AddDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string EditWho { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? EditDate { get; set; }

        /// <summary>
        /// 查詢欄位
        /// </summary>
        private IEnumerable<BaseVehicle> InternalQueryData(IEnumerable<BaseVehicle> data, out int dataCount)
        {
            if (!string.IsNullOrEmpty(Search))
            {
                data = data.Where(t => t.VehicleKey.Contains(Search, StringComparison.OrdinalIgnoreCase) || t.Driver.Contains(Search, StringComparison.OrdinalIgnoreCase) || t.DriverPhone.Contains(Search, StringComparison.OrdinalIgnoreCase) || t.Receiver.Contains(Search, StringComparison.OrdinalIgnoreCase) || t.CompanyKey.Contains(Search, StringComparison.OrdinalIgnoreCase));
            }
            //BaseVehicle.js queryParams 調用
            if (!string.IsNullOrEmpty(VehicleKey))
            {
                data = data.Where(t => t.VehicleKey.Contains(VehicleKey));
            }
            if (!string.IsNullOrEmpty(Driver))
            {
                data = data.Where(t => t.Driver.Contains(Driver));
            }         
            if (!string.IsNullOrEmpty(DriverPhone))
            {
                data = data.Where(t => t.DriverPhone.Contains(DriverPhone));
            }       
            if (!string.IsNullOrEmpty(Receiver))
            {
                data = data.Where(t => t.Receiver.Contains(Receiver));
            }
            if (!string.IsNullOrEmpty(CompanyKey))
            {
                data = data.Where(t => t.CompanyKey.Contains(CompanyKey));
            }
            if (!string.IsNullOrEmpty(VehicleType))
            {
                data = data.Where(t => t.VehicleType.Contains(VehicleType));
            }
            if (!string.IsNullOrEmpty(BoxType))
            {
                data = data.Where(t => t.BoxType.Contains(BoxType));
            }
            if (!string.IsNullOrEmpty(UnloadingType))
            {
                data = data.Where(t => t.UnloadingType.Contains(UnloadingType));
            }
            if (!string.IsNullOrEmpty(EmployType))
            {
                data = data.Where(t => t.EmployType.Contains(EmployType));
            }
            if (!string.IsNullOrEmpty(Active))
            {
                data = data.Where(t => t.Active.Contains(Active));
            }
            if (!string.IsNullOrEmpty(PND))
            {
                data = data.Where(t => t.PND.Contains(PND));
            }

            dataCount = data.Count();

            switch (Sort)
            {
                case "VehicleKey":
                    data = Order == "asc" ? data.OrderBy(t => t.VehicleKey) : data.OrderByDescending(t => t.VehicleKey);
                    break;
                case "CompanyKey":
                    data = Order == "asc" ? data.OrderBy(t => t.CompanyKey) : data.OrderByDescending(t => t.CompanyKey);
                    break;
                case "VehicleType":
                    data = Order == "asc" ? data.OrderBy(t => t.VehicleType) : data.OrderByDescending(t => t.VehicleType);
                    break;
                case "LoadingWeight":
                    data = Order == "asc" ? data.OrderBy(t => t.LoadingWeight) : data.OrderByDescending(t => t.LoadingWeight);
                    break;
                case "LoadingCube":
                    data = Order == "asc" ? data.OrderBy(t => t.LoadingCube) : data.OrderByDescending(t => t.LoadingCube);
                    break;
                case "Driver":
                    data = Order == "asc" ? data.OrderBy(t => t.Driver) : data.OrderByDescending(t => t.Driver);
                    break;
                case "Phone":
                    data = Order == "asc" ? data.OrderBy(t => t.DriverPhone) : data.OrderByDescending(t => t.DriverPhone);
                    break;
                case "Description":
                    data = Order == "asc" ? data.OrderBy(t => t.Description) : data.OrderByDescending(t => t.Description);
                    break;    
                case "LoadingPallet":
                    data = Order == "asc" ? data.OrderBy(t => t.LoadingPallet) : data.OrderByDescending(t => t.LoadingPallet);
                    break;
                case "CarWeight":
                    data = Order == "asc" ? data.OrderBy(t => t.CarWeight) : data.OrderByDescending(t => t.CarWeight);
                    break;
                case "BoxType":
                    data = Order == "asc" ? data.OrderBy(t => t.BoxType) : data.OrderByDescending(t => t.BoxType);
                    break;
                case "CarHeight":
                    data = Order == "asc" ? data.OrderBy(t => t.CarHeight) : data.OrderByDescending(t => t.CarHeight);
                    break;
                case "UnloadingType":
                    data = Order == "asc" ? data.OrderBy(t => t.UnloadingType) : data.OrderByDescending(t => t.UnloadingType);
                    break;
                case "EmployType":
                    data = Order == "asc" ? data.OrderBy(t => t.EmployType) : data.OrderByDescending(t => t.EmployType);
                    break;        
                case "Receiver":
                    data = Order == "asc" ? data.OrderBy(t => t.Receiver) : data.OrderByDescending(t => t.Receiver);
                    break;  
                case "PND":
                    data = Order == "asc" ? data.OrderBy(t => t.PND) : data.OrderByDescending(t => t.PND);
                    break;
                case "ApproveWho":
                    data = Order == "asc" ? data.OrderBy(t => t.ApproveWho) : data.OrderByDescending(t => t.ApproveWho);
                    break;      
                case "ApproveDate":
                    data = Order == "asc" ? data.OrderBy(t => t.ApproveDate) : data.OrderByDescending(t => t.ApproveDate);
                    break;           
                case "ConfirmWho":
                    data = Order == "asc" ? data.OrderBy(t => t.ConfirmWho) : data.OrderByDescending(t => t.ConfirmWho);
                    break;      
                case "ConfirmDate":
                    data = Order == "asc" ? data.OrderBy(t => t.ConfirmDate) : data.OrderByDescending(t => t.ConfirmDate);
                    break;
                case "Active":
                    data = Order == "asc" ? data.OrderBy(t => t.Active) : data.OrderByDescending(t => t.Active);
                    break;
                case "AddWho":
                    data = Order == "asc" ? data.OrderBy(t => t.AddWho) : data.OrderByDescending(t => t.AddWho);
                    break;      
                case "AddDate":
                    data = Order == "asc" ? data.OrderBy(t => t.AddDate) : data.OrderByDescending(t => t.AddDate);
                    break;           
                case "EditWho":
                    data = Order == "asc" ? data.OrderBy(t => t.EditWho) : data.OrderByDescending(t => t.EditWho);
                    break;   
                case "EditDate":
                    data = Order == "asc" ? data.OrderBy(t => t.EditDate) : data.OrderByDescending(t => t.EditDate);
                    break;     
            }
            if (Limit != 0) data = data.Skip(Offset).Take(Limit);
            return data;
        }

        /// <summary>
        /// 
        /// </summary>  
        public QueryData<object> RetrieveData()
        {
            var dataCount = 0;
            var data = InternalQueryData(BaseVehicleHelper.Retrieves(), out dataCount);            
            var ret = new QueryData<object>();
            ret.total = dataCount;
            
            ret.rows = data.Select(u => new
            {
                Id = u.VehicleKey + u.Driver,
                u.VehicleKey,
                u.CompanyKey,
                u.VehicleType,
                u.LoadingWeight,
                u.LoadingCube,
                u.Driver,
                u.DriverPhone,
                u.Description,
                u.LoadingPallet,
                u.CarWeight,
                u.BoxType,
                u.CarHeight,
                u.UnloadingType,
                u.EmployType,
                u.Receiver,
                u.PND,
                u.ApproveWho,
                u.ApproveDate,
                u.ConfirmWho,
                u.ConfirmDate,
                u.Active,
                u.AddWho,
                u.AddDate,
                u.EditWho,
                u.EditDate,
            });
            return ret;
        }
        
        /// <summary>
        /// 
        /// </summary>  
        public IEnumerable<BaseVehicle> RetrievesExcel()
        {
            var dataCount = 0;
            var data = InternalQueryData(BaseVehicleHelper.Retrieves(), out dataCount);
            return data;
        }
    }
}
