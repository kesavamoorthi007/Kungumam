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
using Kungumam.Models;

namespace Kungumam.Controllers
{
    public class RoadBlockController : Controller
    {
        IRoadBlockService RoadBlockService;
        IConfiguration? _configuratio;
        private string? _connectionString;
        DataTransactions datatrans;
        public RoadBlockController(IRoadBlockService _RoadBlockService, IConfiguration _configuratio)
        {
            RoadBlockService = _RoadBlockService;
            _connectionString = _configuratio.GetConnectionString("MySqlConnection");
            datatrans = new DataTransactions(_connectionString);
        }
        public IActionResult RoadBlock(string id)
        {
            RoadBlock br = new RoadBlock();
            //br.ChooseMagazinelst = BindMagazine();



            if (id == null)
            {

            }
            else
            {
                DataTable dt = new DataTable();
                dt = RoadBlockService.GetEditRoadBlock(id);
                if (dt.Rows.Count > 0)
                {

                    br.url = dt.Rows[0]["url"].ToString();
                    br.Image = dt.Rows[0]["img"].ToString();
                    br.IssueDate = dt.Rows[0]["AddedDateFormatted"].ToString();
                    br.end_dt = dt.Rows[0]["AddedDateFormatted1"].ToString();
                    br.ID = id;


                }
                

            }
            return View(br);
        }
        public IActionResult ListRoadBlock()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RoadBlock(List<IFormFile> file, RoadBlock Cy, string id)
        {

            try
            {
                Cy.ID = id;
                string Strout = RoadBlockService.RoadBlockCRUD(file, Cy);
                if (string.IsNullOrEmpty(Strout))
                {
                    if (Cy.ID == null)
                    {
                        TempData["notice"] = "RoadBlock Inserted Successfully...!";
                    }
                    else
                    {
                        TempData["notice"] = "RoadBlock Updated Successfully...!";
                    }
                    return RedirectToAction("ListRoadBlock");
                }

                else
                {
                    ViewBag.PageTitle = "Edit RoadBlock";
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
        public ActionResult MyListRoadBlockgrid(string strStatus)

        {
            List<RoadBlockgrid> Reg = new List<RoadBlockgrid>();
            DataTable dtUsers = new DataTable();
            strStatus = strStatus == "" ? "Y" : strStatus;
            dtUsers = RoadBlockService.GetAllRoadBlock(strStatus);
            for (int i = 0; i < dtUsers.Rows.Count; i++)
            {

                string DeleteRow = string.Empty;
                string EditRow = string.Empty;


                EditRow = "<a href=RoadBlock?id=" + dtUsers.Rows[i]["rd_id"].ToString() + "><img src='../Images/edit.png' alt='Edit' width='20' /></a>";
                DeleteRow = "<a href=DeleteMR?id=" + dtUsers.Rows[i]["rd_id"].ToString() + "><img src='../Images/trash.png' alt='Deactivate' width='20' /></a>";


                Reg.Add(new RoadBlockgrid
                {
                    id = Convert.ToInt64(dtUsers.Rows[i]["rd_id"].ToString()),
                    urlRoadBlock = dtUsers.Rows[i]["url"].ToString(),
                    imageRoadBlock = dtUsers.Rows[i]["img"].ToString(),
                    issusedate = dtUsers.Rows[i]["AddedDateFormatted"].ToString(),
                    endate = dtUsers.Rows[i]["AddedDateFormatted1"].ToString(),
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

            string flag = RoadBlockService.StatusDeleteMR(tag, id);
            if (string.IsNullOrEmpty(flag))
            {

                return RedirectToAction("ListRoadBlock");
            }
            else
            {
                TempData["notice"] = flag;
                return RedirectToAction("ListRoadBlock");
            }
        }
    }
}


