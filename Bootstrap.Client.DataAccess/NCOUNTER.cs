using Bootstrap.Security.DataAccess;
using PetaPoco;
using System;
using System.Collections.Generic;
using Longbow.Web.Mvc;
using System.Linq;

namespace Bootstrap.Client.DataAccess
{
    /// <summary>
    /// NCOUNTER
    /// </summary>
    [TableName("NCOUNTER")]
    public class NCOUNTER
    {
        /// <summary>
        /// keyname
        /// </summary>
        public string keyname { get; set; }
        /// <summary>
        /// keycount
        /// </summary>
        public int keycount { get; set; }
       
        public virtual NCOUNTER RetrievesBykeyname(string keyname) 
        {
            NCOUNTER nc = null; 
            var db = DbManager.Create("bestlogtms");
            try
            {
                db.BeginTransaction();                
                nc = db.FetchProc<NCOUNTER>("SPNCOUNTER", new { keyname = keyname }).SingleOrDefault();
                db.CompleteTransaction();
            }
            catch (Exception ex)
            {
                db.AbortTransaction();
                throw ex;
            }
            return nc;
        }
    } 
}
