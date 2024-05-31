using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Kungumam.Models
{
    public class SubCategory
    {
        public SubCategory()
        {
            this.ChooseMagazinelst = new List<SelectListItem>();
            this.Categorylist = new List<SelectListItem>();

        }
        public List<SelectListItem> ChooseMagazinelst;
        public string ChooseMagazine { get; set; }
        public string SubCategeroy { get; set; }
        public string ID { get; set; }
        public string sbctmne { get; set; }
        public string ddlStatus { get; set; }
        public string Catageroy { get; internal set; }

        public List<SelectListItem> Categorylist;
    }
    public class SubCategorygrid
    {
        public long id { get; set; }
        public string bookid { get; set; }
        public string category { get; set; }
        public string subcatid { get; set; }
        public string sbctmne { get; set; }
        public string editrow { get; set; }
        public string delrow { get; set; }
    }
}
