using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Kungumam.Models
{
    public class Category
    {
        public Category()
        {
            this.ChooseMagazinelst = new List<SelectListItem>();

        }
        public List<SelectListItem> ChooseMagazinelst;
        public string ChooseMagazine { get; set; }
        public string Catageroy { get; set; }
        public string Tamil { get; set; }
        public string ID { get; set; }
        public string ddlStatus { get; set; }
    }
    public class Categorygrid
    {
        internal string? chooseMagazine;
        internal string? category;

        public long id { get; set; }
        public string bookid { get; set; }
        public string cname { get; set; }
        public string tamilcat { get; set; }
        public string editrow { get; set; }
        public string delrow { get; set; }
        public string? ChooseMagazine { get; internal set; }
    }
}
