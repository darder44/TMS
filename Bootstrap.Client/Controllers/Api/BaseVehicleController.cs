using Bootstrap.Client.Query;
using Bootstrap.Client.DataAccess;
using Longbow.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using Bootstrap.Client.DataAccess.Helper;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using Bootstrap.Client.Query.ExportExcelBody;

namespace Bootstrap.Admin.Controllers.Api
{
    ///<summary>
    ///計費代碼Controller
    ///</summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BaseVehicleController : ControllerBase
    {
        ///<summary>
        ///
        ///</summary>
        [HttpGet] 
        public QueryData<object> Get([FromQuery]QueryBaseVehicleOption value)
        {
            return value.RetrieveData();
        }
        ///<summary>
        ///
        ///</summary>
        [HttpGet("{RetrieveByVehicleKeyAndDriver}")]
        public BaseVehicle RetrieveByVehicleKeyAndDriver([FromQuery]string VehicleKey, string Driver)
        {
            return BaseVehicleHelper.RetrieveByVehicleKeyAndDriver(VehicleKey, Driver);
        }
        /// <summary>
        /// 新建/更新
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        [ButtonAuthorize(Url = "~/TMS/BaseVehicle", Auth = "add,edit")]
        public bool Save([FromBody]BaseVehicle value)
        {
            value.EditWho = User.Identity.Name;
            value.EditDate = DateTime.Now;            
            if (string.IsNullOrEmpty(value.Id))
            {
                value.AddWho = value.EditWho;
                value.AddDate = value.EditDate;
                return BaseVehicleHelper.Save(value);   
            }
            else
            {
                return BaseVehicleHelper.Update(value);        
            }          
        }


        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="values"></param>
        [HttpDelete]
        [ButtonAuthorize(Url = "~/TMS/BaseVehicle", Auth = "del")]
        public bool Delete([FromBody]IEnumerable<BaseVehicle> values)
        {
            return BaseVehicleHelper.Delete(values);
        }

        /// <summary>
        /// 
        /// </summary>
        [HttpPost("ExportExcel")]
        public string ExportExcel([FromServices] IMemoryCache cache, [FromBody]ExportExcelBody<QueryBaseVehicleOption> value)
        {
            if (value.Sheets.Count() != 1) return "";
            var sheet = value.Sheets.SingleOrDefault();
            value.Query.Sort = sheet.sortName;
            var data = value.Query.RetrievesExcel();            
            var param = new NpoiParam<BaseVehicle>
            {                
                Data = data,                              //資料               
                SheetName = sheet.SheetName,              //Sheet要叫什麼名子
                ColumnMapping = sheet.Columns,            //欄位對應 (處理Excel欄名、格式轉換) 
                /*       
                FontStyle = new FontStyle                 //是否需自定Excel字型大小
                {
                    FontName = "Calibri",
                    FontHeightInPoints = 11,
                },
                */
                ShowHeader = true,                        //是否畫表頭
                IsAutoFit = true,                         //是否啟用自動欄寬
            };

            List<NpoiParam<BaseVehicle>> list = new List<NpoiParam<BaseVehicle>>(){ param };

            return cache.SetDownloadCache(NpoiHelper.ExportExcel(list, value.FileName));
        }
        
    }
}
