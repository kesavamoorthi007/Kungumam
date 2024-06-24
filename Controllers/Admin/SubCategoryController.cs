using DocumentFormat.OpenXml.Drawing.Diagrams;
using Kungumam.Interface;
using Kungumam.Interface.Admin;
using Kungumam.Models;
using Kungumam.Services;
using Kungumam.Services.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace Kungumam.Controllers.Admin
{
    public class SubCategoryController : Controller
    {
        ISubCategoryService SubCategoryService;
        IConfiguration? _configuratio;
        private string? _connectionString;
        DataTransactions datatrans;
        public SubCategoryController(ISubCategoryService _SubCategoryService, IConfiguration _configuratio)
        {
            SubCategoryService = _SubCategoryService;
            _connectionString = _configuratio.GetConnectionString("MySqlConnection");
            datatrans = new DataTransactions(_connectionString);
        }
        public IActionResult SubCategory(string id)
        {
            SubCategory br = new SubCategory();
            br.ChooseMagazinelst = BindMagazine();
            br.Categorylist = BindCategory();
            if (id == null)
            {

            }
            else
            {

                DataTable dt = new DataTable();
                dt = SubCategoryService.GetEditSubCategory(id);
                if (dt.Rows.Count > 0)
                {
                    br.ChooseMagazinelst = BindMagazine();
                    br.ChooseMagazine = dt.Rows[0]["book_id"].ToString();
                    br.Categorylist = BindCategory();
                    br.Catageroy = dt.Rows[0]["cat_id"].ToString();
                    br.SubCategeroy = dt.Rows[0]["sbctmne"].ToString();
                    br.ID = id;

                }
            }
            return View(br);

        }
        public IActionResult ListSubCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SubCategory(SubCategory Cy, string id)
        {

            try
            {
                Cy.ID = id;
                string Strout = SubCategoryService.SubCategoryCRUD(Cy);
                if (string.IsNullOrEmpty(Strout))
                {
                    if (Cy.ID == null)
                    {
                        TempData["notice"] = "SubCategory Inserted Successfully...!";
                    }
                    else
                    {
                        TempData["notice"] = "SubCategory Updated Successfully...!";
                    }
                    return RedirectToAction("ListSubCategory");
                }

                else
                {
                    ViewBag.PageTitle = "Edit SubCategory";
                    TempData["notice"] = Strout;
                    //return View();
                }

                // }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View(Cy);
        }
        //public JsonResult GetCategoryJSON(string supid)
        //{
        //    SubCategory model = new SubCategory();
        //    return Json(BindCategory(supid));

        //}
        public List<SelectListItem> BindMagazine()
        {
            try
            {
                DataTable dtDesg = SubCategoryService.GetMagazine();
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
        public List<SelectListItem> BindCategory()
        {
            try
            {
                DataTable dtDesg = SubCategoryService.GetAllCategory();
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
        public ActionResult MyListSubCategorygrid(string strStatus)
        {
            List<SubCategorygrid> Reg = new List<SubCategorygrid>();
            DataTable dtUsers = new DataTable();
            strStatus = strStatus == "" ? "Y" : strStatus;
            dtUsers = SubCategoryService.GetAllListCategory(strStatus);
            for (int i = 0; i < dtUsers.Rows.Count; i++)
            {

                string DeleteRow = string.Empty;
                string EditRow = string.Empty;


                EditRow = "<a href=SubCategory?id=" + dtUsers.Rows[i]["subcat_id"].ToString() + "><img src='../Images/edit.png' alt='Edit' width='20' /></a>";
                DeleteRow = "<a href=DeleteMR?id=" + dtUsers.Rows[i]["subcat_id"].ToString() + "><img src='../Images/trash.png' alt='Deactivate' width='20' /></a>";


                Reg.Add(new SubCategorygrid
                {
                    id = Convert.ToInt64(dtUsers.Rows[i]["subcat_id"].ToString()),
                    bookid = dtUsers.Rows[i]["book_name"].ToString(),
                    category = dtUsers.Rows[i]["ctmne"].ToString(),
                    sbctmne = dtUsers.Rows[i]["sbctmne"].ToString(),
                    editrow = EditRow,
                    delrow = DeleteRow,

                });
            }

            return Json(new
            {
                Reg
            });

        }
        public ActionResult DeleteMR(string tag, int id)
        {

            string flag = SubCategoryService.StatusDeleteMR(tag, id);
            if (string.IsNullOrEmpty(flag))
            {

                return RedirectToAction("ListSubCategory");
            }
            else
            {
                TempData["notice"] = flag;
                return RedirectToAction("ListSubCategory");
            }
        }
    }
}

