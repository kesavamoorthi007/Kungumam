using Kungumam.Interface.Admin;
using Kungumam.Models;
using System.Data;
using System.Data.SqlClient;
namespace Kungumam.Services.Admin
{
    public class GalleryService : IGalleryService
    {
        private readonly string _connectionString;
        DataTransactions datatrans;
        public GalleryService(IConfiguration _configuratio)
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
        
        public DataTable GetAllCategory()
        {
            string SvSql = string.Empty;
            SvSql = "SELECT cat_id,book_id,ctmne FROM category ";
            DataTable dtt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(SvSql, _connectionString);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            adapter.Fill(dtt);
            return dtt;
        }

        public DataTable GetAllSubCategory()
        {
            string SvSql = string.Empty;
            SvSql = "SELECT subcat_id,sbctmne,book_id FROM sctegy ";
            DataTable dtt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(SvSql, _connectionString);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            adapter.Fill(dtt);
            return dtt;
        }
    }
}

