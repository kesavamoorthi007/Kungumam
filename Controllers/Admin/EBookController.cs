using Kungumam.Interface.Admin;
using Kungumam.Models;
using Kungumam.Services;
using Kungumam.Services.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;


namespace Kungumam.Controllers.Admin
{
    public class EBookController : Controller
    {

        IEBookService EBookService;
        IConfiguration? _configuratio;
        private string? _connectionString;
        DataTransactions datatrans;

        public EBookController(IEBookService _EBookService, IConfiguration _configuratio)
        {
            EBookService = _EBookService;
            _connectionString = _configuratio.GetConnectionString("MySqlConnection");
            datatrans = new DataTransactions(_connectionString);
        }
        public IActionResult EBook(string id)
        {
            EBook br = new EBook();

            br.ChooseMagazinelst = BindMagazine();


            if (id == null)
            {

            }
            else
            {

            }
            return View(br);

        }
        public IActionResult ListEBook()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EBook(EBook Cy, string id)
        {

            try
            {
                Cy.ID = id;
                string Strout = EBookService.EBookCRUD(Cy);
                if (string.IsNullOrEmpty(Strout))
                {
                    if (Cy.ID == null)
                    {
                        TempData["notice"] = "EBook Inserted Successfully...!";
                    }
                    else
                    {
                        TempData["notice"] = "EBook Updated Successfully...!";
                    }
                    return RedirectToAction("ListEBook");
                }

                else
                {
                    ViewBag.PageTitle = "Edit EBook";
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
                DataTable dtDesg = EBookService.GetMagazine();
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
        public ActionResult MyListEBookgrid(string strStatus)
        {
            List<EBookgrid> Reg = new List<EBookgrid>();
            DataTable dtUsers = new DataTable();
            strStatus = strStatus == "" ? "Y" : strStatus;
            dtUsers = EBookService.GetAllEBook(strStatus);
            for (int i = 0; i < dtUsers.Rows.Count; i++)
            {

                string DeleteRow = string.Empty;
                string EditRow = string.Empty;


                EditRow = "<a href=ebook?id=" + dtUsers.Rows[i]["ebook_id"].ToString() + "><img src='../Images/editing-icon-vector.jpg' alt='Edit' width='30' /></a>";
                DeleteRow = "<a href=DeleteMR?id=" + dtUsers.Rows[i]["ebook_id"].ToString() + "><img src='../Images/Inactive.png' alt='Deactivate' width='20' /></a>";


                Reg.Add(new EBookgrid
                {
                    id = Convert.ToInt64(dtUsers.Rows[i]["ebook_id"].ToString()),
                    book = dtUsers.Rows[i]["book_id"].ToString(),
                    url = dtUsers.Rows[i]["url"].ToString(),
                    issueDate = dtUsers.Rows[i]["AddedDateFormatted"].ToString(),
                    endDate = dtUsers.Rows[i]["AddedDateFormatted1"].ToString(),
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

            string flag = EBookService.StatusDeleteMR(tag, id);
            if (string.IsNullOrEmpty(flag))
            {

                return RedirectToAction("ListEBook");
            }
            else
            {
                TempData["notice"] = flag;
                return RedirectToAction("ListEBook");
            }
        }

    }
}
