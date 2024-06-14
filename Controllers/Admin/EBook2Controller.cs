using Kungumam.Interface.Admin;
using Kungumam.Models;
using Kungumam.Services;
using Kungumam.Services.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace Kungumam.Controllers.Admin
{
    public class EBook2Controller : Controller
    {

        IEBook2Service EBook2Service;
        IConfiguration? _configuratio;
        private string? _connectionString;
        DataTransactions datatrans;
        public EBook2Controller(IEBook2Service _EBook2Service, IConfiguration _configuratio)
        {
            EBook2Service = _EBook2Service;
            _connectionString = _configuratio.GetConnectionString("MySqlConnection");
            datatrans = new DataTransactions(_connectionString);
        }
        public IActionResult EBook2(string id)
        {
            EBook2 br = new EBook2();

            br.ChooseMagazinelst = BindMagazine();

            if (id == null)
            {

            }
            else
            {

                DataTable dt = new DataTable();
                dt = EBook2Service.GetEditEBook2(id);
                if (dt.Rows.Count > 0)
                {
                    br.ChooseMagazinelst = BindMagazine();
                    br.ChooseMagazine = dt.Rows[0]["book_id"].ToString();
                    br.url = dt.Rows[0]["url"].ToString();
                    br.IssueDate = dt.Rows[0]["issue_dt"].ToString();
                    br.EndDate = dt.Rows[0]["end_dt"].ToString();
                    br.ID = id;


                }
            }
                return View(br);

        }
        public IActionResult ListEBook2()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EBook2(EBook2 Cy, string id)
        {

            try
            {
                Cy.ID = id;
                string Strout = EBook2Service.EBook2CRUD(Cy);
                if (string.IsNullOrEmpty(Strout))
                {
                    if (Cy.ID == null)
                    {
                        TempData["notice"] = "EBook2 Inserted Successfully...!";
                    }
                    else
                    {
                        TempData["notice"] = "EBook2 Updated Successfully...!";
                    }
                    return RedirectToAction("ListEBook2");
                }

                else
                {
                    ViewBag.PageTitle = "Edit EBook2";
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
                List<SelectListItem> lstdesg = new List<SelectListItem>();
                lstdesg.Add(new SelectListItem() { Text = "Thozhi", Value = "1" });
                lstdesg.Add(new SelectListItem() { Text = "Thozhi Supplementary", Value = "2" });
                lstdesg.Add(new SelectListItem() { Text = "Anmegapalan Book 1", Value = "3" });
                lstdesg.Add(new SelectListItem() { Text = "Anmegapalan Book 2", Value = "4" });


                return lstdesg;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult MyListEBook2grid(string strStatus)
        {
            List<EBook2grid> Reg = new List<EBook2grid>();
            DataTable dtUsers = new DataTable();
            strStatus = strStatus == "" ? "Y" : strStatus;
            dtUsers = EBook2Service.GetAllEBook2(strStatus);
            for (int i = 0; i < dtUsers.Rows.Count; i++)
            {

                string DeleteRow = string.Empty;
                string EditRow = string.Empty;


                EditRow = "<a href=ebook?id=" + dtUsers.Rows[i]["news_id"].ToString() + "><img src='../Images/edit.png' alt='Edit' width='20' /></a>";
                DeleteRow = "<a href=DeleteMR?id=" + dtUsers.Rows[i]["news_id"].ToString() + "><img src='../Images/trash.png' alt='Deactivate' width='20' /></a>";


                Reg.Add(new EBook2grid
                {
                    id = Convert.ToInt64(dtUsers.Rows[i]["news_id"].ToString()),
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

            string flag = EBook2Service.StatusDeleteMR(tag, id);
            if (string.IsNullOrEmpty(flag))
            {

                return RedirectToAction("List EBook2");
            }
            else
            {
                TempData["notice"] = flag;
                return RedirectToAction("List EBook2");
            }
        }

    }


 }

    