using System.Collections.Generic;
using System.Data;
using Kungumam.Interface;
using Kungumam.Interface.Admin;
using Kungumam.Models;
using Kungumam.Services;
using Kungumam.Services.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DocumentFormat.OpenXml.Bibliography;
using System.Text;


namespace Kungumam.Controllers
{
    public class WrapperController : Controller
    {
        IWrapperService WrapperService;
        IConfiguration? _configuratio;
        private string? _connectionString;
        DataTransactions datatrans;
        public WrapperController(IWrapperService _WrapperService, IConfiguration _configuratio)
        {
            WrapperService = _WrapperService;
            _connectionString = _configuratio.GetConnectionString("MySqlConnection");
            datatrans = new DataTransactions(_connectionString);
        }
        public IActionResult Wrapper(string id)
        {
            Wrapper br = new Wrapper();
            br.ChooseMagazinelst = BindMagazine();


            if (id == null)
            {

            }
            else
            {
               
            }
            return View(br);

        }
        public IActionResult ListWrapper()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Wrapper(List<IFormFile> file, Wrapper Cy, string id)
        {

            try
            {
                Cy.ID = id;
                string Strout = WrapperService.WrapperCRUD(file,Cy);
                if (string.IsNullOrEmpty(Strout))
                {
                    if (Cy.ID == null)
                    {
                        TempData["notice"] = "Wrapper Inserted Successfully...!";
                    }
                    else
                    {
                        TempData["notice"] = "Wrapper Updated Successfully...!";
                    }
                    return RedirectToAction("ListWrapper");
                }

                else
                {
                    ViewBag.PageTitle = "Edit Wrapper";
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
                DataTable dtDesg = WrapperService.GetMagazine();
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
        public ActionResult MyListWrappergrid(string strStatus)
        {
            List<Wrappergrid> Reg = new List<Wrappergrid>();
            DataTable dtUsers = new DataTable();
            strStatus = strStatus == "" ? "Y" : strStatus;
            dtUsers = WrapperService.GetAllWrapper(strStatus);
            for (int i = 0; i < dtUsers.Rows.Count; i++)
            {

                string DeleteRow = string.Empty;
                string EditRow = string.Empty;


                EditRow = "<a href=Wrapper?id=" + dtUsers.Rows[i]["wrapper_id"].ToString() + "><img src='../Images/editing-icon-vector.jpg' alt='Edit' width='30' /></a>";
                DeleteRow = "<a href=DeleteMR?id=" + dtUsers.Rows[i]["wrapper_id"].ToString() + "><img src='../Images/Inactive.png' alt='Deactivate' width='20' /></a>";


                Reg.Add(new Wrappergrid
                {
                    id = Convert.ToInt64(dtUsers.Rows[i]["wrapper_id"].ToString()),
                    bookid = dtUsers.Rows[i]["book_id"].ToString(),
                    bookwrapper = dtUsers.Rows[i]["book_wrapper"].ToString(),
                    issusedate = dtUsers.Rows[i]["AddedDateFormatted"].ToString(),
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

            string flag = WrapperService.StatusDeleteMR(tag, id);
            if (string.IsNullOrEmpty(flag))
            {

                return RedirectToAction("ListWrapper");
            }
            else
            {
                TempData["notice"] = flag;
                return RedirectToAction("ListWrapper");
            }
        }
    }
}
