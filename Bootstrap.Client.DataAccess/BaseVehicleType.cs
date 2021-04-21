using Bootstrap.Security.DataAccess;
using PetaPoco;
using System;
using System.Collections.Generic;
using Longbow.Web.Mvc;
using System.Linq;

namespace Bootstrap.Client.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    [TableName("BaseVehicleType")]
    public class BaseVehicleType
    {
        public string VehicleType { get; set; }
        public string Description { get; set; }
        public string ChargeType { get; set; }
        public string AddWho { get; set; }
        public string AddDate { get; set; }


        //查詢 BaseVehicleType 車種
        public virtual IEnumerable<BaseVehicleType> Retrieves() => DbManager.Create("bestlogtms").Fetch<BaseVehicleType>("SELECT * FROM BaseVehicleType order by VehicleType");


    }

}

