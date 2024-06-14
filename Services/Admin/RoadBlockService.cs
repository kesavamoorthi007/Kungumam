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
using Kungumam.Controllers;

namespace Kungumam.Services
{
    public class RoadBlockService : IRoadBlockService
    {
        private readonly string _connectionString;
        DataTransactions datatrans;
        public RoadBlockService(IConfiguration _configuratio)
        {
            _connectionString = _configuratio.GetConnectionString("MySqlConnection");
            datatrans = new DataTransactions(_connectionString);
        }
        public DataTable GetEditRoadBlock(string id)
        {
            string SvSql = string.Empty;
            SvSql = "SELECT rd_id,url,img,issue_dt,end_dt FROM rd_blk where url = '" + id + "'";
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
        public string RoadBlockCRUD(List<IFormFile> files, RoadBlock Cy)
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
                            svSQL = "Insert into rd_blk (url,img,issue_dt,end_dt,dt,flag) VALUES ('" + Cy.url + "','" + filename1 + "','" + Cy.IssueDate +"','" + Cy.end_dt + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','y')";
                            SqlCommand objCmds = new SqlCommand(svSQL, objConn);
                            objCmds.ExecuteNonQuery();

                        }
                    }
                    else
                    {

                        svSQL = "Update rd_blk set url = '" + Cy.url + "',issue_dt = '" + Cy.IssueDate + "',end_dt = '" + Cy.end_dt + "' WHERE rd_blk.rd_id ='" + Cy.ID + "'";
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
        public DataTable GetAllRoadBlock(string strStatus)
        {
            string SvSql = string.Empty;
            if (strStatus == "Y" || strStatus == null)
            {
                SvSql = "SELECT rd_id,url,img,CONVERT(varchar, rd_blk.issue_dt, 106) AS AddedDateFormatted,CONVERT(varchar, rd_blk.end_dt, 106) AS AddedDateFormatted1 from rd_blk where rd_blk.flag='y' ORDER BY rd_id DESC ";
            }
            else
            {
                SvSql = "SELECT rd_id,url,img,CONVERT(varchar, rd_blk.issue_dt, 106) AS AddedDateFormatted,CONVERT(varchar, rd_blk.end_dt, 106) AS AddedDateFormatted1 from rd_blk where rd_blk.flag='N' ORDER BY rd_id DESC ";
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
                    svSQL = "UPDATE rd_blk SET flag ='N' WHERE rd_id='" + id + "'";
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
