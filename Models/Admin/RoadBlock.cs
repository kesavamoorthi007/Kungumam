using Microsoft.AspNetCore.Mvc.Rendering;

namespace Kungumam.Models
{
    public class RoadBlock
    {
        
        public string ID { get; set; }
       
        public string url { get; set; }
        public string IssueDate { get; set; }
        public string end_dt { get; set; }
        public string ddlStatus { get; set; }
        public string? Url { get; internal set; }
        public string? Image { get; internal set; }
    }
    public class RoadBlockgrid
    {

        internal string? chooseMagazine;
       

        public long id { get; set; }
        public string urlRoadBlock { get; set; }
        public string imageRoadBlock { get; set; }
        public string endate { get; set; }
        public string issusedate { get; set; }

        public string editrow { get; set; }
        public string delrow { get; set; }

    }
}
