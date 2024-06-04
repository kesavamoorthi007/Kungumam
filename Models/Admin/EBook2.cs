using Microsoft.AspNetCore.Mvc.Rendering;

namespace Kungumam.Models
{
    public class EBook2
    {
        internal string id;
        internal string book;

        public EBook2()
        {
            this.ChooseMagazinelst = new List<SelectListItem>();
        }
        public List<SelectListItem> Magazinelst { get; set; }
        public string ChooseMagazine { get; set; }

        public string url { get; set; }
        public string IssueDate { get; set; }
        public string EndDate { get; set; }
        public string ddlStatus { get; set; }
        public string ID { get;  set; }
        public List<SelectListItem> ChooseMagazinelst { get; internal set; }
    }
    public class EBook2grid
    {
        public long id { get; set; }

        public string book { get; set; }
        public string url { get; set; }
        public string issueDate { get; set; }
        public string endDate { get; set; }
        public string editrow { get; set; }
        public string delrow { get; set; }
    }
}