using Kungumam.Interface;
using Kungumam.Interface.Admin;
using Kungumam.Models;
using Kungumam.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace Kungumam.Controllers.Admin
{
    public class CategoryController : Controller
    {
        ICategoryService CategoryService;
        IConfiguration? _configuratio;
        private string? _connectionString;
        DataTransactions datatrans;
        public CategoryController(ICategoryService _CategoryService, IConfiguration _configuratio)
        {
            CategoryService = _CategoryService;
            _connectionString = _configuratio.GetConnectionString("MySqlConnection");
            datatrans = new DataTransactions(_connectionString);
        }
        public IActionResult Category(string id)
        {
            Category br = new Category();
            br.ChooseMagazinelst = BindMagazine();

            if (id == null)
            {


            }
            else
            {
                DataTable dt = new DataTable();
                dt = CategoryService.GetEditCategory(id);
                if (dt.Rows.Count > 0)
                {
                    br.ChooseMagazinelst = BindMagazine();
                    br.ChooseMagazine = dt.Rows[0]["book_id"].ToString();
                    br.Catageroy = dt.Rows[0]["ctmne"].ToString();
                    br.Tamil = dt.Rows[0]["Tamil_cat"].ToString();
                    br.ID = id;

                }
            }
            return View(br);

        }
        public IActionResult ListCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Category(Category Cy, string id)
        {

            try
            {
                Cy.ID = id;
                string Strout = CategoryService.CategoryCRUD(Cy);
                if (string.IsNullOrEmpty(Strout))
                {
                    if (Cy.ID == null)
                    {
                        TempData["notice"] = "Category Inserted Successfully...!";
                    }
                    else
                    {
                        TempData["notice"] = "Category Updated Successfully...!";
                    }
                    return RedirectToAction("ListCategory");
                }

                else
                {
                    ViewBag.PageTitle = "Edit Category";
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
        public List<SelectListItem> BindMagazine()
        {
            try
            {
                DataTable dtDesg = CategoryService.GetMagazine();
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
        public ActionResult MyListCategorygrid(string strStatus)
        {
            List<Categorygrid> Reg = new List<Categorygrid>();
            DataTable dtUsers = new DataTable();
            strStatus = strStatus == "" ? "Y" : strStatus;
            dtUsers = CategoryService.GetAllCategory(strStatus);
            for (int i = 0; i < dtUsers.Rows.Count; i++)
            {

                string DeleteRow = string.Empty;
                string EditRow = string.Empty;


                EditRow = "<a href=Category?id=" + dtUsers.Rows[i]["cat_id"].ToString() + "><img src='../Images/edit.png' alt='Edit' width='20' /></a>";
                DeleteRow = "<a href=DeleteMR?id=" + dtUsers.Rows[i]["cat_id"].ToString() + "><img src='../Images/trash.png' alt='Deactivate' width='20' /></a>";


                Reg.Add(new Categorygrid
                {
                    id = Convert.ToInt64(dtUsers.Rows[i]["cat_id"].ToString()),
                    book = dtUsers.Rows[i]["book_name"].ToString(),
                    name = dtUsers.Rows[i]["ctmne"].ToString(),
                    tamil = dtUsers.Rows[i]["Tamil_cat"].ToString(),
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

            string flag = CategoryService.StatusDeleteMR(tag, id);
            if (string.IsNullOrEmpty(flag))
            {

                return RedirectToAction("ListCategory");
            }
            else
            {
                TempData["notice"] = flag;
                return RedirectToAction("ListCategory");
            }
        }
    }
}
