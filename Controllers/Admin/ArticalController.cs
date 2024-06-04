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
    public class ArticalController : Controller
    {
        IArticalService ArticalService;
        IConfiguration? _configuratio;


        private string? _connectionString;
        DataTransactions datatrans;
        public ArticalController(IArticalService _ArticalService, IConfiguration _configuratio)
        {
            ArticalService = _ArticalService;
            _connectionString = _configuratio.GetConnectionString("MySqlConnection");
            datatrans = new DataTransactions(_connectionString);
        }
        public IActionResult Artical(string id)
        {
            Artical br = new Artical();

            br.ChooseMagazinelst = BindMagazine();
            br.ChooseCategory = BindCategory();

            if (id == null)
            {

            }
            else
            {

            }
            return View(br);

        }
        public IActionResult ListArtical()
        {
            return View();
        }
        public List<SelectListItem> BindMagazine()
        {
            try
            {
                DataTable dtDesg = ArticalService.GetMagazine();
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
                DataTable dtDesg = ArticalService.GetAllCategory();
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

    }
}