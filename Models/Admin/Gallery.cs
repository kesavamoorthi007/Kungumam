using Microsoft.AspNetCore.Mvc.Rendering;


namespace Kungumam.Models
{
    public class Gallery
    {
        public Gallery()
        {
            this.ChooseMagazinelst = new List<SelectListItem>();
            this.ChooseCategory = new List<SelectListItem>();
            this.ChooseSubCategory = new List<SelectListItem>();

        }
        public List<SelectListItem> ChooseMagazinelst { get; set; }
        public string ChooseMagazine { get; set; }
        public List<SelectListItem> ChooseCategory { get; set; }
        public string Catageroy { get; set; }
        public List<SelectListItem> ChooseSubCategory { get; set; }
        public string SubCategory { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string IssueDate { get; set; }
        public string EndDate { get; set; }
        public string ddlStatus { get; set; }
    }
}
