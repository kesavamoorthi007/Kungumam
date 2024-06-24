using DocumentFormat.OpenXml.Drawing.Diagrams;
using Kungumam.Interface;
using Kungumam.Interface.Admin;
using Kungumam.Models;
using Kungumam.Models;
using Kungumam.Services;
using Kungumam.Services.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;


namespace Kungumam.Controllers.Admin
{
    public class GalleryController : Controller
    {

        IGalleryService GalleryService;
        IConfiguration? _configuratio;
        private string? _connectionString;
        DataTransactions datatrans;
        public GalleryController(IGalleryService _GalleryService, IConfiguration _configuratio)
        {
            GalleryService = _GalleryService;
            _connectionString = _configuratio.GetConnectionString("MySqlConnection");
            datatrans = new DataTransactions(_connectionString);
        }
        public IActionResult Gallery(string id)
        {

            Gallery br = new Gallery();
            br.ChooseMagazinelst = BindMagazine();
            br.ChooseCategory = BindCategory("");
            br.ChooseSubCategory = BindSubCategory("");


            if (id == null)
            {

            }
            else
            {

            }
            return View(br);

        }
        public IActionResult ListGallery()
        {
            return View();
        }
        [HttpPost]
        public List<SelectListItem> BindMagazine()
        {
            try
            {
                DataTable dtDesg = GalleryService.GetMagazine();
                List<SelectListItem> lstdesg = new List<SelectListItem>();
                for (int i = 0; i < dtDesg.Rows.Count; i++)
                {
                    lstdesg.Add(new SelectListItem() { Text = dtDesg.Rows[i]["book_name"].ToString(), Value = dtDesg.Rows[i]["book_id"].ToString() });
                }
                return lstdesg;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<SelectListItem> BindCategory(string id)
        {
            try
            {
                DataTable dtDesg = GalleryService.GetAllCategory(id);
                List<SelectListItem> lstdesg = new List<SelectListItem>();
                for (int i = 0; i < dtDesg.Rows.Count; i++)
                {
                    lstdesg.Add(new SelectListItem() { Text = dtDesg.Rows[i]["ctmne"].ToString(), Value = dtDesg.Rows[i]["cat_id"].ToString() });
                }
                return lstdesg;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<SelectListItem> BindSubCategory(string id)
        {
            try
            {
                DataTable dtDesg = GalleryService.GetAllSubCategory(id);
                List<SelectListItem> lstdesg = new List<SelectListItem>();
                for (int i = 0; i < dtDesg.Rows.Count; i++)
                {
                    lstdesg.Add(new SelectListItem() { Text = dtDesg.Rows[i]["sbctmne"].ToString(), Value = dtDesg.Rows[i]["subcat_id"].ToString() });
                }
                return lstdesg;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public JsonResult GetCategoryJSON(string supid)
        {
            Gallery model = new Gallery();
            model.ChooseCategory = BindCategory(supid);
            return Json(BindCategory(supid));

        }
        public JsonResult GetSubCategoryJSON(string supid)
        {
            Gallery model = new Gallery();
            model.ChooseSubCategory = BindSubCategory(supid);
            return Json(BindSubCategory(supid));

        }

    }
}


    
