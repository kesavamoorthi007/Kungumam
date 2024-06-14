using Kungumam.Interface;
using Kungumam.Interface.Admin;
using Kungumam.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using DocumentFormat.OpenXml.Bibliography;
using System.Linq;
using System.Data.SqlClient;



namespace Kungumam.Services.Admin
{
	public class MagazineService : IMagazineService
	{
		private readonly string _connectionString;
		DataTransactions datatrans;
		public MagazineService(IConfiguration _configuratio)
		{
			_connectionString = _configuratio.GetConnectionString("MySqlConnection");
			datatrans = new DataTransactions(_connectionString);
		}
        //public DataTable GetMagazine()
        //{
        //    string SvSql = string.Empty;
        //    SvSql = "select book_name,book_id from book";
        //    DataTable dtt = new DataTable();
        //    SqlDataAdapter adapter = new SqlDataAdapter(SvSql, _connectionString);
        //    SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
        //    adapter.Fill(dtt);
        //    return dtt;
        //}
        public DataTable GetEditMagazine(string id)
        {
            string SvSql = string.Empty;
            SvSql = "SELECT book_id,book_name FROM book where book_id = '" + id + "'";
            DataTable dtt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(SvSql, _connectionString);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            adapter.Fill(dtt);
            return dtt;
        }
        public DataTable GetAllMagazineService()
        {
            string SvSql = string.Empty;
            SvSql = "  select book_id,book_name from book ORDER BY book_id DESC ";
            DataTable dtt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(SvSql, _connectionString);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            adapter.Fill(dtt);
            return dtt;
        }

        public string MagazineCRUD(Magazine Cy)
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
                        svSQL = "Insert into book (book_name) VALUES ('" + Cy.MagazineName + "')";
                        SqlCommand objCmds = new SqlCommand(svSQL, objConn);
                        objCmds.ExecuteNonQuery();

                    }
                    else
                    {
                        svSQL = "Update book set book_name = '" + Cy.MagazineName + "' WHERE book.book_id ='" + Cy.ID + "'";
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
	}
}