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
//using static Microsoft.EntityFrameworkCore.DbLoggerWrapper;
using Kungumam.Models;

namespace Kungumam.Services
{
    public class WrapperService : IWrapperService
    {
        private readonly string _connectionString;
        DataTransactions datatrans;
        public WrapperService(IConfiguration _configuratio)
        {
            _connectionString = _configuratio.GetConnectionString("MySqlConnection");
            datatrans = new DataTransactions(_connectionString);
        }
        public DataTable GetEditWrapper(string id)
        {
            string SvSql = string.Empty;
            SvSql = "SELECT book_id,wrapper_id,book_wrapper,issue_dt,dt FROM book_wrapper where wrapper_id = '" + id + "'";
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

        public string WrapperCRUD(List<IFormFile> files, Wrapper Cy)
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

                        if (files != null && files.Count > 0)
                        {
                            string filename1 = "";
                            foreach (var file in files)
                            {
                                if (file.Length > 0)
                                {
                                    // Get the file name and combine it with the target folder path
                                    String strLongFilePath1 = file.FileName;
                                    String sFileType1 = "";
                                    sFileType1 = System.IO.Path.GetExtension(file.FileName);
                                    sFileType1 = sFileType1.ToLower();

                                    string strFleName = strLongFilePath1.Replace(sFileType1, "") + String.Format("{0:ddMMMyyyy-hhmmsstt}", DateTime.Now) + sFileType1;
                                    var fileName = Path.Combine("wwwroot/Uploads", strFleName);
                                    filename1 = filename1.Length > 0 ? filename1 + "," + fileName : fileName;
                                    var name = file.FileName;
                                    // Save the file to the target folder

                                    using (var fileStream = new FileStream(fileName, FileMode.Create))
                                    {

                                        file.CopyTo(fileStream);

                                    }
                                }

                            }
                            svSQL = "Insert into book_wrapper (book_id,book_wrapper,issue_dt,dt,flag) VALUES ('" + Cy.ChooseMagazine + "','" + filename1 + "','" + Cy.IssueDate + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")   + "','y')";
                            SqlCommand objCmds = new SqlCommand(svSQL, objConn);
                            objCmds.ExecuteNonQuery();

                        }
                    }
                    else
                    {

                        svSQL = "Update book_wrapper set book_id = '" + Cy.ChooseMagazine + "',issue_dt = '" + Cy.IssueDate + "' WHERE book_wrapper.book_id ='" + Cy.ID + "'";
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
        public DataTable GetAllWrapper(string strStatus)
        {
            string SvSql = string.Empty;
            if (strStatus == "Y" || strStatus == null)
            {
                SvSql = "select book_id,wrapper_id,book_wrapper,CONVERT(varchar, book_wrapper.issue_dt, 106) AS AddedDateFormatted from book_wrapper where book_wrapper.flag='y' ORDER BY wrapper_id DESC ";
            }
            else
            {
                SvSql = "select book_id,wrapper_id,book_wrapper,CONVERT(varchar, book_wrapper.issue_dt, 106) AS AddedDateFormatted from book_wrapper where book_wrapper.flag='n' ORDER BY wrapper_id DESC ";
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
                    svSQL = "UPDATE book_wrapper SET flag ='N' WHERE wrapper_id='" + id + "'";
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
