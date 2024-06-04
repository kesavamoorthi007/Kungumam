using Microsoft.AspNetCore.Mvc.Rendering;

namespace Kungumam.Models
{
    public class Artical
    {
        public Artical()
        {
            this.ChooseMagazinelst = new List<SelectListItem>();
            this.ChooseCategory = new List<SelectListItem>();

        }
        internal string id;
       
        public string ChooseMagazine { get; set; }
        public string ChooseCatageroy { get; set; }
        public string MainBanner { get; set; }
        public string ForArchive { get; set; }
        public string SubBanner { get; set; }
        public string Title { get; set; }
        public string IssueDate { get; set; }
        public string EndDate { get; set; }
        public string Message { get; set; }
        public string TitleMessage { get; set; }
        public string Image { get; set; }
        public string Keyword { get; set; }
        public string KeywordEnglish { get; set; }
        public string ID { get; set; }
        public string ddlStatus { get; set; }
        public List<SelectListItem> ChooseMagazinelst { get; set; }
        public List<SelectListItem> ChooseCategory { get; set; }
    }

}