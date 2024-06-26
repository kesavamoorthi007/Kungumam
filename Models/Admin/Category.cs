﻿using Microsoft.AspNetCore.Mvc.Rendering;
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

        public long id { get; set; }
        public string book { get; set; }
        public string tamil { get; set; }
        public string name { get; set; }
        public string editrow { get; set; }
        public string delrow { get; set; }
       
    }
}
