using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace WebApplication1
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            if (!IsPostBack)
            {
                LoadRoles();
                BindGrid();
                LoadPageMaster();

            }

        }
        private void LoadRoles()
        {
            string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("select * from TBLRoles", conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                ddlRoles.DataSource = dr;
                ddlRoles.DataTextField = "RoleName";
                ddlRoles.DataValueField = "RoleId";
                ddlRoles.DataBind();


            }
            ddlRoles.Items.Insert(0, new ListItem("--Select Role--", ""));
        }

        private void LoadPageMaster()
        {
            string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("Select * from TblPageMaster", conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                cblPageMaster.DataSource = dr;
                cblPageMaster.DataTextField = "PageName";
                cblPageMaster.DataValueField = "PageID";
                cblPageMaster.DataBind();
            }
        }

        //public void btn_Submit(object sender, EventArgs e)
        //{
        //    List<int> PageMaster = new List<int>();
            
        //    int role =Convert.ToInt32( ddlRoles.SelectedValue);

        //    string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            
        //    foreach (ListItem item in cblPageMaster.Items)
        //    {
        //        if (item.Selected)
        //        {

        //            int selectedId = Convert.ToInt32(item.Value);


        //            using (SqlConnection con = new SqlConnection(cs))
        //            using (SqlCommand cmd = new SqlCommand("Test2CRUD", con))
        //            {
        //                cmd.Parameters.AddWithValue("@RoleId", role);
        //                cmd.Parameters.AddWithValue("@PageID", selectedId);
        //                cmd.Parameters.AddWithValue("@JID", 08);
        //                cmd.Parameters.AddWithValue("@Transaction", 'A');
        //                con.Open();
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.ExecuteNonQuery(); // Execute the insert command for each selected item
        //                con.Close();
        //                BindGrid();

        //            }
        //        }
        //    }








        //}






        //protected void btnSave_Click(object sender, EventArgs e)
        //{
        //    List<int> selectedPageIDs = new List<int>();

        //    // Iterate through the CheckBoxList items
        //    foreach (ListItem item in cblPageIDs.Items)
        //    {
        //        if (item.Selected)
        //        {
        //            // Add the selected PageID (Value property) to a list
        //            selectedPageIDs.Add(Convert.ToInt32(item.Value));
        //        }
        //    }

        //    // Now, save these IDs to your database (example using a helper method)
        //    SavePageIDsToDatabase(selectedPageIDs);
        //    lblMessage.Text = "Selected Page IDs saved successfully to the database!";
        //}

        //private void SavePageIDsToDatabase(List<int> pageIDs)
        //{
        //    // Replace with your actual database connection and insertion logic
        //    string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

        //    using (SqlConnection con = new SqlConnection(connectionString))
        //    {
        //        con.Open();
        //        // Assuming a table named UserPages with columns UserID and PageID
        //        string insertQuery = "INSERT INTO UserPages (UserID, PageID) VALUES (@UserID, @PageID)";

        //        // Example UserID (get this from your session or current user context)
        //        int currentUserID = 1;

        //        foreach (int pageID in pageIDs)
        //        {
        //            using (SqlCommand cmd = new SqlCommand(insertQuery, con))
        //            {
        //                cmd.Parameters.AddWithValue("@UserID", currentUserID);
        //                cmd.Parameters.AddWithValue("@PageID", pageID);
        //                cmd.ExecuteNonQuery();
        //            }
        //        }
        //    }
        //}



        protected void BindGrid()
        {
            string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("select R.RoleID, R.RoleName,STRING_AGG(P.PageName,';') PageName from TBLRoles as R inner join TblTest2 as T2 on T2.RoleId = R.RoleId inner join TblPageMaster as P on P.PageID= T2.PageID group by R.RoleName , R.RoleID;\r\n", conn);
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@PageMaster", "");
                //cmd.Parameters.AddWithValue("@ID", 89);
                //cmd.Parameters.AddWithValue("@Role", "");
                //cmd.Parameters.AddWithValue("@Transaction", 'G');
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable d1 = new DataTable();
                da.Fill(d1);
                TestGridView.DataSource = d1;
                TestGridView.DataBind();

            }
        }

        
        public void TestGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int RID = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "btn_update")
            {
                getRowForUpdate(RID);
            }
            
            if (e.CommandName == "btn_delete")
            {
                deleteRow(RID);
            }
        }

        //protected void getRowForUpdate(int RID)
        //{
        //    List<string> selectedIds = new List<string>();
        //    string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        //    using (SqlConnection conn = new SqlConnection(cs))
        //    {
        //        SqlCommand cmd = new SqlCommand("Test2CRUD", conn);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@PageID", "");
        //        cmd.Parameters.AddWithValue("@RoleId", RID);
        //        cmd.Parameters.AddWithValue("@JID", "");
        //        cmd.Parameters.AddWithValue("@Transaction", 'R');
        //        conn.Open();
        //        SqlDataReader dr = cmd.ExecuteReader();
        //        while (dr.Read())
        //        {
        //            LoadRoles();


        //            string RoleId = dr["RoleId"].ToString();
        //            if (ddlRoles.Items.FindByValue(RoleId) != null)
        //            {
        //                ddlRoles.SelectedValue = RoleId;
        //            }


        //            selectedIds.Add(dr["PageID"].ToString());


        //            hfID.Value = RID.ToString();


        //        }


        //    }
        //    foreach (System.Web.UI.WebControls.ListItem item in cblPageMaster.Items)
        //    {
        //        if (selectedIds.Contains(item.Value))
        //        {
        //            item.Selected = true;
        //        }
        //    }
        //}




        protected void ddlItems_SelectedIndexChanged(object sender, EventArgs e)
        {
           string selectedId = ddlRoles.SelectedValue;

            if (!string.IsNullOrEmpty(selectedId))
            {
                getRowForUpdateByDDL(int.Parse(selectedId)); 
               
            }
          
        }

        protected void getRowForUpdateByDDL(int RID)
        {
            string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            List<string> selectedIds = new List<string>();

            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("Test2CRUD", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PageID", DBNull.Value);
                cmd.Parameters.AddWithValue("@RoleId", RID);
                cmd.Parameters.AddWithValue("@JID", DBNull.Value);
                cmd.Parameters.AddWithValue("@Transaction", "R");

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                //LoadRoles();
                LoadPageMaster();

                while (dr.Read())
                {
                    ddlRoles.SelectedValue = dr["RoleId"].ToString();
                    selectedIds.Add(dr["PageID"].ToString());
                }
            }


            foreach (ListItem item in cblPageMaster.Items)
            {
                item.Selected = selectedIds.Contains(item.Value);
            }

            hfID.Value = RID.ToString();
        }







        protected void getRowForUpdate(int RID)
        {
            string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            List<string> selectedIds = new List<string>();

            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("Test2CRUD", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PageID", DBNull.Value);
                cmd.Parameters.AddWithValue("@RoleId", RID);
                cmd.Parameters.AddWithValue("@JID", DBNull.Value);
                cmd.Parameters.AddWithValue("@Transaction", "R");

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                LoadRoles();
                LoadPageMaster();

                while (dr.Read())
                {
                    ddlRoles.SelectedValue = dr["RoleId"].ToString();
                    selectedIds.Add(dr["PageID"].ToString());
                }
            }


            foreach (ListItem item in cblPageMaster.Items)
            {
                item.Selected = selectedIds.Contains(item.Value);
            }

            hfID.Value = RID.ToString();
        }


        //protected void btn_Update(object sender, EventArgs e)
        //{
        //    List<int> PageMaster = new List<int>();

        //    int role = Convert.ToInt32(ddlRoles.SelectedValue);
        //    string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        //    foreach (ListItem item in cblPageMaster.Items)
        //    {
        //        if (item.Selected)
        //        {
        //            int selectedId = Convert.ToInt32(item.Value);
        //            using (SqlConnection con = new SqlConnection(cs))
        //            using (SqlCommand cmd = new SqlCommand("Test2CRUD", con))
        //            {
        //                cmd.Parameters.AddWithValue("@RoleId", role);
        //                cmd.Parameters.AddWithValue("@PageID", selectedId);
        //                cmd.Parameters.AddWithValue("@JID", 08);
        //                cmd.Parameters.AddWithValue("@Transaction", 'U');
        //                con.Open();
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.ExecuteNonQuery(); 
        //                con.Close();
        //            }
        //        }


        //    }


        //}

        protected void btn_Update(object sender, EventArgs e)
        {
            int role = Convert.ToInt32(ddlRoles.SelectedValue);
            string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();

                using (SqlCommand cmdDel = new SqlCommand("Test2CRUD", con))
                {
                    cmdDel.CommandType = CommandType.StoredProcedure;
                    cmdDel.Parameters.AddWithValue("@RoleID", role);
                    cmdDel.Parameters.AddWithValue("@PageID", 0);
                    cmdDel.Parameters.AddWithValue("@JID", 0);
                    cmdDel.Parameters.AddWithValue("@Transaction", 'D');
                    cmdDel.ExecuteNonQuery();
                }



                foreach (ListItem item in cblPageMaster.Items)
                {
                    if (item.Selected)
                    {
                        using (SqlCommand cmd = new SqlCommand("Test2CRUD", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@RoleID", role);
                            cmd.Parameters.AddWithValue("@PageID", Convert.ToInt32(item.Value));
                            cmd.Parameters.AddWithValue("@JID", 8);
                            cmd.Parameters.AddWithValue("@Transaction", 'U');
                            cmd.ExecuteNonQuery();
                            BindGrid();

                        }
                    }
                }
            }
        }


        protected void deleteRow (int id)
        {
            string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection con = new SqlConnection(cs))
            {

                SqlCommand cmd = new SqlCommand("delete from TblTest2 where RoleID=@RoleID;", con);
                
                cmd.Parameters.AddWithValue("@RoleID", id);
     
                con.Open();
                cmd.ExecuteNonQuery();
                BindGrid();


            }
        }


        //public void GetRoleByText(object sender, EventArgs e)
        //{
        //    string RoleText = txtRole.Text;
        //    foreach (ListItem item in ddlRoles.Items)
        //    {
        //        if (item.Text.StartsWith(RoleText, StringComparison.OrdinalIgnoreCase))
        //        {
        //            ddlRoles.SelectedValue = item.Value;
        //            break;
        //        }
        //    }

        //}

        public void btnAddRole(object sender, EventArgs e)
        {
            string RoleText = txtRole.Text;
            string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(cs)) {
                SqlCommand cmd = new SqlCommand("RoleCrud", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RoleName", RoleText);
                cmd.Parameters.AddWithValue("@Transaction", 'A');
               
                conn.Open();
                cmd.ExecuteNonQuery();
                LoadRoles();

            }

            foreach (ListItem item in ddlRoles.Items)
            {
                if (item.Text.StartsWith(RoleText, StringComparison.OrdinalIgnoreCase))
                {
                    ddlRoles.SelectedValue = item.Value;
                    break;
                }
            }
            LoadRoles();
        }

    }
}
