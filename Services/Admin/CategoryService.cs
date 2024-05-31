using Kungumam.Interface.Admin;
using Kungumam.Models;
using System.Data;
using System.Data.SqlClient;

namespace Kungumam.Services.Admin
{
    public class CategoryService : ICategoryService
    {
        private readonly string _connectionString;
        DataTransactions datatrans;
        public CategoryService(IConfiguration _configuratio)
        {
            _connectionString = _configuratio.GetConnectionString("MySqlConnection");
            datatrans = new DataTransactions(_connectionString);
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
        public string CategoryCRUD(Category Cy)
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
                        svSQL = "Insert into category (book_id,ctmne,Tamil_cat,flag) VALUES ('" + Cy.ChooseMagazine + "','" + Cy.Catageroy + "','" + Cy.Tamil + "','y')";
                        SqlCommand objCmds = new SqlCommand(svSQL, objConn);
                        objCmds.ExecuteNonQuery();

                    }
                    else
                    {
                        svSQL = "Update category set book_id = '" + Cy.ChooseMagazine + "',ctmne = '" + Cy.Catageroy + "',ctmne = '" + Cy.Tamil + "' WHERE category.cat_id ='" + Cy.ID + "'";
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

        public DataTable GetAllCategory(string strStatus)
        {
            string SvSql = string.Empty;
            if (strStatus == "Y" || strStatus == null)
            {
                SvSql = "SELECT book_id,cat_id,ctmne,Tamil_cat FROM category WHERE flag ='y' ORDER BY cat_id";
            }
            else
            {
                SvSql = "SELECT book_id,cat_id,ctmne,Tamil_cat FROM category WHERE flag ='N' ORDER BY cat_id";
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
                    svSQL = "UPDATE category SET flag ='N' WHERE cat_id='" + id + "'";
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
