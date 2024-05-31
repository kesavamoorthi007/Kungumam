using Microsoft.AspNetCore.Mvc.Rendering;

namespace Kungumam.Models
{
    public class Wrapper
    {
        public Wrapper()
        {
            this.ChooseMagazinelst = new List<SelectListItem>();

        }
        public List<SelectListItem> ChooseMagazinelst;
        public string ID { get; set; }
        public string ChooseMagazine { get; set; }
        public string wrapper { get; set; }
        public string IssueDate { get; set; }
        public string ddlStatus { get; set; }

    }
    public class Wrappergrid
    {
        public long id { get; set; }
        public string bookid { get; set; }
        public string bookwrapper { get; set; }
        public string issusedate { get; set; }
        public string editrow { get; set; }
        public string delrow { get; set; }

    }
}
