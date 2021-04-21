using System.Security.Claims;
using System.Linq;


namespace Bootstrap.Client.DataAccess.Helper
{
    public class BestHelper
    {
        public static string GetGroupCode(string id)
        {
            var groupcode = "";
            var groups = GroupHelper.RetrievesByUserId(id);
            var group = groups.Where(g => g.Checked.ToLowerInvariant() == "checked");
            if(group.Count() > 0) 
            {
                groupcode = group.FirstOrDefault().GroupCode;
            } 
            return groupcode;
        }

        public static string GetFacility(ClaimsPrincipal User)
        {
            var user = UserHelper.Retrieves().FirstOrDefault(u => u.UserName.ToLowerInvariant() == User.Identity.Name.ToLowerInvariant());
            if (user == null) return "";
            return BestHelper.GetWareHouse(BestHelper.GetGroupCode(user.Id));
        }

        public static string GetWareHouse(string groupcode)
        {
            var wh = "";
            if(string.IsNullOrEmpty(groupcode) || !groupcode.StartsWith("BEST_")) return wh;
            wh = groupcode.Trim();
            var whs = wh.Split('_');
            if(whs.Count() < 2) return "";
            return whs[1];
        }  

        public static string GetPrintReportUserName(string name)
        {
            var username = name;
            username += "_" + UserHelper.RetrieveUserByUserName(username).DisplayName;
            return username;
        }
    }
}