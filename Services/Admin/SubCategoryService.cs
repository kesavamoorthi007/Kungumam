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
            SvSql = "SELECT book_id,cat_id,subcat_id,sbctmne FROM sctegy where cat_id = '" + id + "'";
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

                //if (Cy.ID == null)
                //{
                //    svSQL = " SELECT Count(C_Name) as cnt FROM TMCategory_N WHERE C_Name = LTRIM(RTRIM('" + Cy.C_Name + "')) ";
                //    if (datatrans.GetDataId(svSQL) > 0)
                //    {
                //        msg = "Category Name(Tamil) Already Existed";
                //        return msg;
                //    }
                //}
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
            SvSql = "SELECT cat_id,book_id,subcat_id FROM sctegy ";
            DataTable dtt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(SvSql, _connectionString);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            adapter.Fill(dtt);
            return dtt;
        }
        public DataTable GetAllListCategory(string strStatus)
        {
            string SvSql = string.Empty;
            if (strStatus == "Y" || strStatus == null)
            {
                SvSql = "SELECT book_id,cat_id,subcat_id,sbctmne FROM sctegy WHERE flag ='y' ORDER BY cat_id";
            }
            else
            {
                SvSql = "SELECT book_id,cat_id,subcat_id,sbctmne FROM sctegy WHERE flag ='N' ORDER BY cat_id";
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
                    svSQL = "UPDATE sctegy SET flag ='N' WHERE subcat_id='" + id + "'";
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

        public DataTable GetAllCategory(string id)
        {
            throw new NotImplementedException();
        }
    }
}
