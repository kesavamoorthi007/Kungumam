using Microsoft.AspNetCore.Mvc.Rendering;

namespace Kungumam.Models
{
	public class Magazine
	{
		public string ID { get; set; }
		public string MagazineName { get; set; }
	}
	public class Magazinegrid
	{
		public long id { get; set; }
		public string mname { get; set; }
		public string editrow { get; set; }
	}
}
