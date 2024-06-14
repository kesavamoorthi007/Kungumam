using Microsoft.AspNetCore.Mvc.Rendering;

namespace Kungumam.Models
{
    public class EBook
    {
        internal string id;
        internal string book;

        public EBook()
        {
            this.ChooseMagazinelst = new List<SelectListItem>();
        }
        public List<SelectListItem> ChooseMagazinelst { get; set; }
        public string ChooseMagazine { get; set; }

        public string url { get; set; }
        public string IssueDate { get; set; }
        public string EndDate { get; set; }
        public string ddlStatus { get; set; }
        public object ID { get; internal set; }
    }
    public class EBookgrid
    {
        public long id { get; set; }
        public string book { get; set; }
        public string chooseMagazine { get; set; }
        public string url { get; set; }
        public string issueDate { get; set; }
        public string endDate { get; set; }
        public string editrow { get; set; }
        public string delrow { get; set; }
    }
}
