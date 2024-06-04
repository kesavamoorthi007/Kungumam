using Microsoft.AspNetCore.Mvc.Rendering;

namespace Kungumam.Models
{
	public class Magazine
	{
		public Magazine()
		{
			this.ChooseMagazinelst = new List<SelectListItem>();
		}
        public List<SelectListItem> ChooseMagazinelst { get; set; }
        public string ChooseMagazine { get; set; }

        public string ID { get; set; }
		public string MagazineName { get; set; }
        public string IssueDate { get; set; }
        public string EndDate { get; set; }
    }
	public class Magazinegrid
	{
		public long id { get; set; }
		public string mname { get; set; }
		public string editrow { get; set; }
        public string IssueDate { get; set; }
        public string EndDate { get; set; }
    }
}
