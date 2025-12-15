using AjaxControlToolkit;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
namespace WebApplication1 
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            if (!IsPostBack)
            {
                mpWelcome.Show();

                MarksGridView1_RowCommand();
                //MarksGridView2_RowCommand();
                ParenGridView_RowCommand();

                BindGrid3();
                BindReport();
            }
        }






        protected void MarksGridView1_RowCommand ()
        {
            string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(cs)) {

                SqlCommand cmd = new SqlCommand("RegisterCrud", conn);
                cmd.CommandType = CommandType.StoredProcedure;   // ✅ IMPORTANT
                cmd.Parameters.AddWithValue("@Name", DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", DBNull.Value);
                cmd.Parameters.AddWithValue("@Mobile", DBNull.Value);
                cmd.Parameters.AddWithValue("@Gender", DBNull.Value);
                cmd.Parameters.AddWithValue("@CityID", DBNull.Value);
                cmd.Parameters.AddWithValue("@Image", DBNull.Value);
                cmd.Parameters.AddWithValue("@AdhaarNo", DBNull.Value);
                cmd.Parameters.AddWithValue("@ID", 0);
                cmd.Parameters.AddWithValue("@Transaction", 'L');

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                da.Fill(dt);

                MarksGridView1.DataSource = dt;
                MarksGridView1.DataBind();
                
               
            }
        }
        
        //For loading parent grid view data 
        protected void ParenGridView_RowCommand()
        {
            string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(cs))
            {

                SqlCommand cmd = new SqlCommand("SELECT R.ID, R.Name, R.Email, R.Mobile, R.Gender, C.CityName, R.AdhaarNo  FROM TblRegistration R   INNER JOIN TblCity C ON R.CityID = C.CID", conn);
             

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                da.Fill(dt);

                gvParent.DataSource = dt;
                gvParent.DataBind();
            }
        }

        //for finding the child grid  for each parent row and bind the each child grid view with each student id of parent row
        protected void gvParent_RowDataBound(object sender, GridViewRowEventArgs e) 
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int studentId = Convert.ToInt32(gvParent.DataKeys[e.Row.RowIndex].Value);

                GridView gvChild = (GridView)e.Row.FindControl("gvChild");

                BindChildGrid(gvChild, studentId);
            }
        }


        private void BindChildGrid(GridView gvChild, int studentId)
        {
            string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("SELECT * from TblMarks where RID=@RID", con);
        
                cmd.Parameters.AddWithValue("@RID", studentId);

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvChild.DataSource = dt;
                gvChild.DataBind();
            }
        }


        private void BindReport()
        {

            DataTable dt1 = new DataTable();
           
            dt1.Columns.Add("Name");
            dt1.Columns.Add("Age");
            dt1.Columns.Add("DOJ");
            dt1.Columns.Add("Salary");
            dt1.Columns.Add("Designation");
            
            dt1.Rows.Add( "Arun Singh", "21",  "01-01-2025","800000","Developer" );
            dt1.Rows.Add( "sonu Singh", "23",  "03-01-2025","200000","SalesMan" );
            dt1.Rows.Add( "Kapil sharma", "31",  "11-01-2025","200000","HR" );
            dt1.Rows.Add( "Manu Singh", "28",  "09-01-2025","500000","Manager" );



            string cs = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
            DataTable dt2 = new DataTable();
            using (SqlConnection conn = new SqlConnection(cs)) {
                SqlCommand cmd = new SqlCommand("Select Name, Marks, Subject, MaxMarks, RID from TblMarks", conn);
                SqlDataAdapter da= new SqlDataAdapter(cmd);
                da.Fill(dt2);

            }




            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Report1.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();

            ReportDataSource rds = new ReportDataSource("DataSet1", dt1);

            ReportDataSource rds2 = new ReportDataSource("DataSet2", dt2);
            ReportViewer1.LocalReport.DataSources.Add(rds);
            ReportViewer1.LocalReport.DataSources.Add(rds2);

            ReportViewer1.LocalReport.Refresh();


        }



     


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string subject = ddlSubject.SelectedValue;
            int maxMarks = int.Parse(txtMaxMarks.Text);
            foreach (GridViewRow row in MarksGridView1.Rows)
            {
                int studenId = Convert.ToInt32(MarksGridView1.DataKeys[row.RowIndex].Value);
                string studentName = row.Cells[0].Text;
                TextBox txtMarks = (TextBox)row.FindControl("txtMarks");

                decimal marks = decimal.Parse(txtMarks.Text);

                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand("MarksCRUD", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@RID", studenId);
                    cmd.Parameters.AddWithValue("@MID", 99);

                    cmd.Parameters.AddWithValue("@Name", studentName);
                    cmd.Parameters.AddWithValue("@Marks", marks);
                    cmd.Parameters.AddWithValue("@Subject", subject);
                    cmd.Parameters.AddWithValue("@MaxMarks", maxMarks);
                    cmd.Parameters.AddWithValue("@Transaction", "A");
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            Response.Write("<script>alert('Marks submitted successfully!');</script>");
        }


       
        protected void BindGrid3()
        {
            string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("GetStudentReport", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

               
                ReportGridView1.DataSource = dt;
                ReportGridView1.DataBind();
            }
        }

   

        protected void txtMarks_TextChanged(object sender, EventArgs e)
        {
            
            TextBox txt = sender as TextBox;
            GridViewRow row = (GridViewRow)txt.NamingContainer;

            Label lbl = row.FindControl("lblError") as Label;

            decimal marks;
            if (decimal.TryParse(txt.Text, out marks))
            {
                int maxMarks = 0;
                int.TryParse(txtMaxMarks.Text, out maxMarks);

                if (marks > maxMarks)
                {
                    lbl.Text = "Marks cannot exceed" +maxMarks ;
                    lbl.ForeColor = System.Drawing.Color.Red;
                    txt.Text = "";
                }

                else 

                    lbl.Text = "";
            }
            else
            {
                lbl.Text = "Enter valid number";
            }
        }



        protected void txtMaxMarks_TextChanged(object sender, EventArgs e)
        {
           
            int maxMarks;
            if (int.TryParse(txtMaxMarks.Text, out maxMarks))
            {
                if (maxMarks > 500)
                {
                    lblMaxError.Text = "Maximum marks cannot exceed 500.";
                    lblMaxError.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    lblMaxError.Text = "";  
                }
            }
            else
            {
                lblMaxError.Text = "Enter a valid number";
                lblMaxError.ForeColor = System.Drawing.Color.Red;
            }
        }

        //protected void deleteChildGridRow(object sender, GridViewCommandEventArgs e)
        //{

        //    int mid = Convert.ToInt32(e.CommandArgument);
        //    if (e.CommandName == "deleteRow")
        //    {
        //        deleteNestedGridRow(mid);
        //        ParenGridView_RowCommand();
        //    }

        //}

        protected void deleteChildGridRow(object sender, GridViewCommandEventArgs e)
        {
            int mid = Convert.ToInt32(e.CommandArgument);

            // STEP 1: Show popup when Delete button is clicked
            if (e.CommandName == "deleteRow")
            {
                GridViewRow row = (e.CommandSource as Control).NamingContainer as GridViewRow;

                ModalPopupExtender mp = (ModalPopupExtender)row.FindControl("mpDeletePopup");
                mp.Show();
            }

            // STEP 2: Perform delete only on Yes button click
            else if (e.CommandName == "confirmDelete")
            {
                deleteNestedGridRow(mid);

                // Refresh parent grid
                ParenGridView_RowCommand();
            }
        }

        protected void deleteNestedGridRow(int mid)
        {
            string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(" delete from TblMarks where MID=@MID", conn);
                cmd.Parameters.AddWithValue("@MID", mid);

                conn.Open();
                cmd.ExecuteNonQuery();

            }
        
        }





     


    }
}