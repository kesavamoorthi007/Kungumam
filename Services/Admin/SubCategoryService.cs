using Kungumam.Interface.Admin;
using Kungumam.Models;
using System.Data;
using System.Data.SqlClient;

namespace Kungumam.Services.Admin
{
    public class SubCategoryService : ISubCategoryService
    { 

        private readonly string _connectionString;
        DataTransactions datatrans;
        public SubCategoryService(IConfiguration _configuratio)
        {
            _connectionString = _configuratio.GetConnectionString("MySqlConnection");
            datatrans = new DataTransactions(_connectionString);
        }
        public DataTable GetEditSubCategory(string id)
        {
            string SvSql = string.Empty;
            SvSql = "SELECT book_id,cat_id,subcat_id,sbctmne FROM sctegy where subcat_id = '" + id + "'";
            DataTable dtt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(SvSql, _connectionString);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            adapter.Fill(dtt);
            return dtt;
        }
        public DataTable GetMagazine()
        {
            string SvSql = string.Empty;
            SvSql = "select book_id,book_name from book";
            DataTable dtt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(SvSql, _connectionString);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            adapter.Fill(dtt);
            return dtt;
        }
        public string SubCategoryCRUD(SubCategory Cy)
        {
            string msg = "";
            try
            {
                string StatementType = string.Empty;
                string svSQL = "";

                using (SqlConnection objConn = new SqlConnection(_connectionString))
                {
                    objConn.Open();
                    if (Cy.ID == null)
                    {
                        svSQL = "Insert into sctegy (book_id,cat_id,sbctmne,flag) VALUES ('" + Cy.ChooseMagazine + "','" + Cy.Catageroy + "','" + Cy.SubCategeroy + "','y')";
                        SqlCommand objCmds = new SqlCommand(svSQL, objConn);
                        objCmds.ExecuteNonQuery();

                    }
                    else
                    {
                        svSQL = "Update sctegy set book_id = '" + Cy.ChooseMagazine + "',cat_id = '" + Cy.Catageroy + "',sbctmne = '" + Cy.SubCategeroy + "' WHERE sctegy.subcat_id ='" + Cy.ID + "'";

                        SqlCommand objCmds = new SqlCommand(svSQL, objConn);
                        objCmds.ExecuteNonQuery();
                    }
                    objConn.Close();
                }


            }
            catch (Exception ex)
            {
                msg = "Error Occurs, While inserting / updating Data";
                throw ex;
            }

            return msg;
        }
        public DataTable GetAllCategory()
        {
            string SvSql = string.Empty;
            SvSql = "SELECT ctmne,cat_id FROM category";
            DataTable dtt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(SvSql, _connectionString);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            adapter.Fill(dtt);
            return dtt;
        }
        public DataTable GetAllListCategory(string strStatus)
        {
            string SvSql = string.Empty;
            if (strStatus == "y" || strStatus == null)
            {
                SvSql = "SELECT book.book_name,category.ctmne,subcat_id,sbctmne FROM sctegy LEFT OUTER JOIN book ON book.book_id=sctegy.book_id LEFT OUTER JOIN category ON category.cat_id=sctegy.cat_id WHERE sctegy.flag ='y' ORDER BY sctegy.cat_id";
            }
            else
            {
                SvSql = "SELECT book.book_name,category.ctmne,subcat_id,sbctmne FROM sctegy LEFT OUTER JOIN book ON book.book_id=sctegy.book_id LEFT OUTER JOIN category ON category.cat_id=sctegy.cat_id WHERE sctegy.flag ='n' ORDER BY sctegy.cat_id";
            }
            DataTable dtt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(SvSql, _connectionString);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            adapter.Fill(dtt);
            return dtt;
        }
        public string StatusDeleteMR(string tag, int id)
        {

            try
            {
                string svSQL = string.Empty;
                using (SqlConnection objConnT = new SqlConnection(_connectionString))
                {
                    svSQL = "UPDATE sctegy SET flag ='n' WHERE subcat_id='" + id + "'";
                    SqlCommand objCmds = new SqlCommand(svSQL, objConnT);
                    objConnT.Open();
                    objCmds.ExecuteNonQuery();
                    objConnT.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return "";
        }
       
    }
}
