using Kungumam.Interface.Admin;
using Kungumam.Models;
using System.Data;
using System.Data.SqlClient;
using static Kungumam.Services.Admin.EBookService;

namespace Kungumam.Services.Admin
{
    public class EBookService : IEBookService
    {
        private readonly string _connectionString;
        DataTransactions datatrans;
        public EBookService(IConfiguration _configuratio)
        {
            _connectionString = _configuratio.GetConnectionString("MySqlConnection");
            datatrans = new DataTransactions(_connectionString);
        }
        public DataTable GetEditEBook(string id)
        {
            string SvSql = string.Empty;
            SvSql = "SELECT book_id,ebook_id,url,issue_dt,end_dt FROM Book_url where ebook_id = '" + id + "'";
            DataTable dtt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(SvSql, _connectionString);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            adapter.Fill(dtt);
            return dtt;
        }
        public DataTable GetMagazine()
        {
            string SvSql = string.Empty;
            SvSql = "select book_id,book_name from book where book_id <>'6' and book_id<>'8' and book_id<>'5' and flag='y'";
            DataTable dtt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(SvSql, _connectionString);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            adapter.Fill(dtt);
            return dtt;
        }
        public string EBookCRUD(EBook Cy)
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
                        svSQL = "Insert into Book_url (book_id,ebook_id,url,issue_dt,end_dt,dt,flag) VALUES ('" + Cy.ChooseMagazine + "','" + Cy.id + "','" + Cy.url + "','" + Cy.IssueDate + "','" + Cy.EndDate + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','y')";
                        SqlCommand objCmds = new SqlCommand(svSQL, objConn);
                        objCmds.ExecuteNonQuery();

                    }
                    else
                    {
                        svSQL = "Update Book_url set book_id = '" + Cy.ChooseMagazine + "',ebook_id = '" + Cy.id + "',url = '" + Cy.url + "',issue_dt = '" + Cy.IssueDate + "',end_dt = '" + Cy.EndDate + "' WHERE Book_url.ebook_id ='" + Cy.ID + "'";
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
       

        public DataTable GetAllEBook(string strStatus)
        {
            string SvSql = string.Empty;
            if (strStatus == "Y" || strStatus == null)
            {
                SvSql = "  SELECT book_id,ebook_id,url,CONVERT(varchar, Book_url.issue_dt, 106) AS AddedDateFormatted,CONVERT(varchar, Book_url.end_dt, 106) AS AddedDateFormatted1 FROM Book_url WHERE flag ='y' ORDER BY book_id";
            }
            else
            {
                SvSql = "  SELECT book_id,ebook_id,url,CONVERT(varchar, Book_url.issue_dt, 106) AS AddedDateFormatted,CONVERT(varchar, Book_url.end_dt, 106) AS AddedDateFormatted1 FROM Book_url WHERE flag ='N' ORDER BY book_id";
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
                    svSQL = "UPDATE Book_url SET flag ='N' WHERE url='" + id + "'";
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
