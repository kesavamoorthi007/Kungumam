﻿using DocumentFormat.OpenXml.Drawing.Charts;
using Kungumam.Interface.Admin;
using Kungumam.Models;
using System.Data;
using System.Data.SqlClient;

namespace Kungumam.Services.Admin
{
    public class EBook2Service : IEBook2Service
    {
        private readonly string _connectionString;
        DataTransactions datatrans;
        public EBook2Service(IConfiguration _configuratio)
        {
            _connectionString = _configuratio.GetConnectionString("MySqlConnection");
            datatrans = new DataTransactions(_connectionString);
        }
        public System.Data.DataTable GetEditEBook2(string id)
        {
            string SvSql = string.Empty;
            SvSql = "SELECT book_id,cat_id,news_id,CONVERT(varchar, ihtoebk.issue_dt, 106) AS AddedDateFormatted, CONVERT(varchar, ihtoebk.end_dt, 106) AS AddedDateFormatted1,url,img FROM ihtoebk where ihtoebk.news_id = '" + id + "'";
            System.Data.DataTable dtt = new System.Data.DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(SvSql, _connectionString);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            adapter.Fill(dtt);
            return dtt;
        }
        public System.Data.DataTable GetMagazine()
        {
            string SvSql = string.Empty;
            SvSql = "select book_id,book_name from book where book_id <>'6' and book_id<>'8' and book_id<>'5' and flag='y'";
            System.Data.DataTable dtt = new System.Data.DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(SvSql, _connectionString);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            adapter.Fill(dtt);
            return dtt;
        }
        public string EBook2CRUD(EBook2 Cy)
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
                        svSQL = "Insert into ihtoebk (book_id,news_id,url,issue_dt,end_dt,dt,flag) VALUES ('" + Cy.ChooseMagazine + "','" + Cy.id + "','" + Cy.url + "','" + Cy.IssueDate + "','" + Cy.EndDate + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','y')";
                        SqlCommand objCmds = new SqlCommand(svSQL, objConn);
                        objCmds.ExecuteNonQuery();

                    }
                    else
                    {
                        svSQL = "Update ihtoebk set book_id = '" + Cy.ChooseMagazine + "',news_id = '" + Cy.id + "',url = '" + Cy.url + "',issue_dt = '" + Cy.IssueDate + "',end_dt = '" + Cy.EndDate + "' WHERE ihtoebk.news_id ='" + Cy.ID + "'";
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

        public System.Data.DataTable GetAllEBook2(string strStatus)
        {
            string SvSql = string.Empty;
            if (strStatus == "y" || strStatus == null)
            {
                SvSql = "SELECT book.book_name,category.ctmne,news_id,url, CONVERT(varchar, ihtoebk.issue_dt, 106) AS AddedDateFormatted, CONVERT(varchar, ihtoebk.end_dt, 106) AS AddedDateFormatted1 FROM ihtoebk LEFT OUTER JOIN category ON category.cat_id=ihtoebk.cat_id LEFT OUTER JOIN book ON book.book_id=ihtoebk.book_id WHERE ihtoebk.flag = 'y' ORDER BY ihtoebk.news_id";
            }
            else
            {
                SvSql = "SELECT book.book_name,category.ctmne,news_id,url, CONVERT(varchar, ihtoebk.issue_dt, 106) AS AddedDateFormatted, CONVERT(varchar, ihtoebk.end_dt, 106) AS AddedDateFormatted1 FROM ihtoebk LEFT OUTER JOIN category ON category.cat_id=ihtoebk.cat_id LEFT OUTER JOIN book ON book.book_id=ihtoebk.book_id WHERE ihtoebk.flag = 'n' ORDER BY ihtoebk.news_id";
            }
            System.Data.DataTable dtt = new System.Data.DataTable();
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
                    svSQL = "UPDATE ihtoebk SET flag ='n' WHERE ihtoebk.news_id='" + id + "'";
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
