using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class WebForm9 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                LoadGrid();

            }

        }

        //private int SelectedSessionID
        //{
        //    get
        //    {
        //        if (Session["SessionID"] != null)
        //            return (int)Session["SessionID"];
        //        return 0; 
        //    }
        //    set { Session["SessionID"] = value; }
        //}

        //public DataRow GetCheckedData(int sessionId)
        //{

        //    DataTable dt = new DataTable();
        //    string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        //    using (SqlConnection con = new SqlConnection(cs))
        //    {
        //        SqlCommand cmd = new SqlCommand("SELECT Status, SessionID FROM TblSessionStatus  WHERE SessionID = @SessionID", con);
        //        cmd.Parameters.AddWithValue("@LectureID", sessionId);
        //        con.Open();
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        da.Fill(dt);


        //    }

        //    return dt.Rows.Count > 0 ? dt.Rows[0] : null;

        //}


        public void Btn_Submit(object sender, EventArgs e)
        {
            string SessionName = txtSubject.Text;
         
            //int Status = Convert.ToInt32(Convert.ToBoolean(SessionName));

            string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("SessionStatusCRUD", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
        
                cmd.Parameters.AddWithValue("@SessionName", SessionName);
                cmd.Parameters.AddWithValue("@SessionID", "");
                cmd.Parameters.AddWithValue("@Status", "");
                cmd.Parameters.AddWithValue("@Transaction", 'A');
                cmd.ExecuteNonQuery();

               
            }
            LoadGrid();
        }

        public void LoadGrid()
        {
            //int sessionId = Convert.ToInt32(GridView1.DataKeys[e.Row.RowIndex].Value);
            string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("Select * from TblSessionStatus", conn);
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GridView1.DataSource = dt;
                GridView1.DataBind();



            }
           


        }





        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
           

              
                if (e.Row.RowType == DataControlRowType.DataRow)
              {

                    CheckBox chkSelect = (CheckBox)e.Row.FindControl("cbStatus");

                    int sessionId = Convert.ToInt32(GridView1.DataKeys[e.Row.RowIndex].Value);
                using (SqlConnection conn = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand("Select  count(1) from TblSessionStatus where Status=1 and SessionID=@SessionID", conn);
                    cmd.Parameters.AddWithValue("@SessionID", sessionId);
                    conn.Open();
                   int i= (int) cmd.ExecuteScalar();

             
                    if (i > 0)
                    {
                        chkSelect.Checked = true;
                    }
                    else
                    {
                        chkSelect.Checked = false;
                    }
                
              }



            }


        }






        //    public void LoadGrid() {
        //    string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        //    using (SqlConnection conn = new SqlConnection(cs))
        //    {
        //        SqlCommand cmd = new SqlCommand("Select * from TblSessionStatus", conn);
        //        conn.Open();
        //        SqlDataAdapter da= new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);
        //        GridView1.DataSource = dt;
        //        GridView1.DataBind();



        //    }





        //}



        //protected void chkRow_CheckedChanged(object sender, EventArgs e)
        //{
        //    RadioButton activeRadioButton = sender as RadioButton;
        //    if (activeRadioButton != null && activeRadioButton.Checked)
        //    {
        //        GridViewRow row = (GridViewRow)activeRadioButton.NamingContainer;
        //        int sessionId = Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Value);

        //        SelectedSessionID = sessionId;
        //    }

        //    if (SelectedSessionID > 0)
        //    {
        //        // Call your method to update the database
        //        UpdateStudentStatusInDb(SelectedSessionID);
        //        // Optionally, clear the selection or provide feedback
        //        // SelectedStudentID = 0; 
        //    }
        //}




        //private void UpdateStudentStatusInDb(int sessionID)
        //{


        //    int status = Convert.ToInt32(Convert.ToBoolean(sessionID));

        //    string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        //    using (SqlConnection conn = new SqlConnection(cs))
        //    {
        //        SqlCommand cmd = new SqlCommand("delete from ", conn);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        conn.Open();

        //        cmd.Parameters.AddWithValue("@SessionName", "");
        //        cmd.Parameters.AddWithValue("@SessionID", sessionID);

        //        cmd.Parameters.AddWithValue("@Status", status);
        //        cmd.Parameters.AddWithValue("@Transaction", 'U');
        //        cmd.ExecuteNonQuery();


        //    }

        //    using (SqlConnection conn = new SqlConnection(cs))
        //    {
        //        SqlCommand cmd = new SqlCommand("SessionStatusCRUD", conn);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        conn.Open();

        //        cmd.Parameters.AddWithValue("@SessionName", "");
        //        cmd.Parameters.AddWithValue("@SessionID", sessionID);

        //        cmd.Parameters.AddWithValue("@Status", status);
        //        cmd.Parameters.AddWithValue("@Transaction", 'U');
        //        cmd.ExecuteNonQuery();


        //    }
        //}





        protected void chkRow_CheckedChanged(object sender, EventArgs e)
        {

            int status = 0;

            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.NamingContainer;

            int rowId = (int)GridView1.DataKeys[row.RowIndex].Value;

            status = Convert.ToInt32(Convert.ToBoolean(rowId));

            string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(cs))
            {
                //SqlCommand cmd1 = new SqlCommand("update TblSessionStatus set Status=0", conn);
                //conn.Open();
                //cmd1.ExecuteNonQuery();
            
                SqlCommand cmd = new SqlCommand("SessionStatusCRUD", conn);
                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
        

                cmd.Parameters.AddWithValue("@SessionName", "");
                cmd.Parameters.AddWithValue("@SessionID", rowId);

                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@Transaction", 'U');
                cmd.ExecuteNonQuery();


            }


            






        }

    

    }

}


   

