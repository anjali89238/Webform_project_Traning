using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace WebApplication1
{
    public partial class WebForm6 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ParenGridView();
                //bindSelectedItem();
                //bindData();

            }
        }


        //protected void bindData()
        //{
        //    string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        //    using (SqlConnection con = new SqlConnection(cs))
        //    {
        //        SqlCommand cmd = new SqlCommand("SELECT LectureID,FacultyID,SubjectID from TblLecture", con);
        //        {
        //            SqlDataAdapter da = new SqlDataAdapter(cmd);
        //            DataTable dt = new DataTable();
        //            da.Fill(dt);   
                    

        //        }
        //    }
        //}



        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int lecId = Convert.ToInt32(GridView1.DataKeys[e.Row.RowIndex].Value);
                DropDownList ddlFaculty = (DropDownList)e.Row.FindControl("ddlFaculty");
                DropDownList ddlSubject = (DropDownList)e.Row.FindControl("ddlSubject");

                DataRowView drv = (DataRowView)e.Row.DataItem;
                string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

          
                if (ddlFaculty != null)
                {
                    using (SqlConnection con = new SqlConnection(cs))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(
                            "SELECT FacultyID, FacultyName FROM TblFacultyName", con);

                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        ddlFaculty.DataSource = dt;
                        ddlFaculty.DataTextField = "FacultyName";
                        ddlFaculty.DataValueField = "FacultyID";
                        ddlFaculty.DataBind();
                    }

                    ddlFaculty.Items.Insert(0, new ListItem("--Select Faculty--", "0"));


                }
    



                if (ddlSubject != null)
                {
                    using (SqlConnection con = new SqlConnection(cs))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(
                            "SELECT SubjectID, SubjectName FROM TblSubject", con);

                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        ddlSubject.DataSource = dt;
                        ddlSubject.DataTextField = "SubjectName";
                        ddlSubject.DataValueField = "SubjectID";
                        ddlSubject.DataBind();
                    }

                    ddlSubject.Items.Insert(0, new ListItem("--Select Subject--", "0"));

              
                    //if (drv["SubjectID"] != DBNull.Value)
                    //{
                    //    ddlSubject.SelectedValue = drv["SubjectID"].ToString();
                    //}
                }

                DataRow dr = GetFacultyData(lecId);
                if (dr != null)
                {
                    ddlFaculty.SelectedValue = dr["FacultyID"].ToString();
                    ddlSubject.SelectedValue = dr["SubjectID"].ToString();
                }
            }
        }

        //public DataRow GetFacultyData(int lecId)
        //{

        //    DataTable dt = new DataTable();
        //    string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        //    using (SqlConnection con = new SqlConnection(cs))
        //    {
                
        //        string sql = @"SELECT FacultyID, SubjectID 
        //           FROM TblFaculty 
        //           WHERE LectureID = @lecId";

        //        using (SqlDataAdapter da = new SqlDataAdapter(sql, con))
        //        {
        //            da.SelectCommand.Parameters.Add(
        //                "@lecId", SqlDbType.Int).Value = lecId;

        //            da.Fill(dt);
        //        }
        //    }

        //    return dt.Rows.Count>0 ? dt.Rows[0] : null;

        //}
        public DataRow GetFacultyData(int lecId)
        {

            DataTable dt = new DataTable();
            string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("SELECT FacultyID, SubjectID  FROM TblFaculty  WHERE LectureID = @LectureID", con);
                cmd.Parameters.AddWithValue("@LectureID", lecId);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

              
            }

            return dt.Rows.Count>0 ? dt.Rows[0] : null;

        }

   


        protected void ParenGridView()
        {
            string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(cs))
            {

                SqlCommand cmd = new SqlCommand("SELECT LectureID ,FacultyID, SubjectID from TblLecture ", conn);

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                da.Fill(dt);

                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }

        public void submitData(object sender, EventArgs e)
        {

            string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            
            foreach (GridViewRow row in GridView1.Rows)
            {
                int LId = Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Value);
                int FacultyID = 0;
                int SubjectID = 0;
                DropDownList ddlFaculty = (DropDownList)row.FindControl("ddlFaculty");
                     string  Faculty= ddlFaculty.SelectedItem.Text;
             int.TryParse(ddlFaculty.SelectedValue,out FacultyID);

                DropDownList ddlSubject = (DropDownList)row.FindControl("ddlSubject");
                     string Subject = ddlSubject.SelectedItem.Text;
                int.TryParse(ddlSubject.SelectedValue, out SubjectID);

                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand("FacultySubjectCRUD", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                
                    cmd.Parameters.AddWithValue("@FacultyName", Faculty);

                    cmd.Parameters.AddWithValue("@Subject", Subject);
                    cmd.Parameters.AddWithValue("@FacultyID", FacultyID);

                    cmd.Parameters.AddWithValue("@SubjectID", SubjectID);

                    cmd.Parameters.AddWithValue("@LectureID", LId);
                    cmd.Parameters.AddWithValue("@Transaction", "A");
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }












    }
}


