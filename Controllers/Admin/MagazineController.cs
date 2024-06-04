using Kungumam.Interface.Admin;
using Kungumam.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace Kungumam.Controllers
{
	public class MagazineController : Controller
	{
		IMagazineService MagazineService;
		IConfiguration? _configuratio;
		private string? _connectionString;
		DataTransactions datatrans;
		public MagazineController(IMagazineService _MagazineService, IConfiguration _configuratio)
		{
			MagazineService = _MagazineService;
			_connectionString = _configuratio.GetConnectionString("MySqlConnection");
			datatrans = new DataTransactions(_connectionString);
		}
		public IActionResult Magazine(string id)
		{
			Magazine br = new Magazine();
            br.ChooseMagazinelst = BindMagazine();

            if (id == null)
			{

			}
			else
			{
				
			}
			return View(br);

		}
		public IActionResult ListMagazine()
		{

			return View();
		}
		[HttpPost]
        public List<SelectListItem> BindMagazine()
        {
            try
            {
                DataTable dtDesg = MagazineService.GetMagazine();
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
        public ActionResult Magazine(Magazine Cy, string id)
		{

			try
			{
				Cy.ID = id;
				string Strout = MagazineService.MagazineCRUD(Cy);
				if (string.IsNullOrEmpty(Strout))
				{
					if (Cy.ID == null)
					{
						TempData["notice"] = "Magazine Inserted Successfully...!";
					}
					else
					{
						TempData["notice"] = "Magazine Updated Successfully...!";
					}
					return RedirectToAction("ListMagazine");
				}

				else
				{
					ViewBag.PageTitle = "Edit Magazine";
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
        public ActionResult MyListMagazinegrid()
        {
            List<Magazinegrid> Reg = new List<Magazinegrid>();
              DataTable dtUsers = new DataTable();
            dtUsers = MagazineService.GetAllMagazineService();
            for (int i = 0; i < dtUsers.Rows.Count; i++)
            {

                string DeleteRow = string.Empty;
                string EditRow = string.Empty;


                EditRow = "<a href=Magazine?id=" + dtUsers.Rows[i]["book_id"].ToString() + "><img src='../Images/editing-icon-vector.jpg' alt='Edit' width='30' /></a>";


                Reg.Add(new Magazinegrid
                {
                    id = Convert.ToInt64(dtUsers.Rows[i]["book_id"].ToString()),
                    mname = dtUsers.Rows[i]["book_name"].ToString(),
                    editrow = EditRow,

                });
            }

            return Json(new
            {
                Reg
            });

        }
    }
}
