using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class WebForm8 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            if (!IsPostBack)
            {
                LoadClass();
                BindCircularGrid();
             

            }
        }

        private void LoadClass()
        {
            string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("Select * from TblClass", conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                cblClass.DataSource = dr;
                cblClass.DataTextField = "ClassName";
                cblClass.DataValueField = "ClassID";
                cblClass.DataBind();
            }
        }

      
        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    string Title = txtTitle.Text;
        //    string Discription = txtDiscription.Text;
        //    string Date = txtDate.Text;

           
        //    //string[] cblArray = new string[12];
        //    string[] cArry = new string[12];

        //    string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        //    using (SqlConnection con = new SqlConnection(cs))
        //    {
        //        con.Open();

             

        //        List<string> selectedItems = new List<string>();

        //        for (int i = 0; i < cblClass.Items.Count; i++)
        //        {
        //            if (cblClass.Items[i].Selected)
        //            {
                    
        //                selectedItems.Add(cArry[i] + cblClass.Items[i].Value);
        //            }
        //        }

        //        string[] cblArray = selectedItems.ToArray();

        //        string resultString = String.Join(", ", cblArray);




        //        using (SqlCommand cmd = new SqlCommand("CircularCRUD", con))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@Title", Title);
        //            cmd.Parameters.AddWithValue("@Discription", Discription);
        //            cmd.Parameters.AddWithValue("@Date", Date);
        //            cmd.Parameters.AddWithValue("@ClassID", resultString);
        //            cmd.Parameters.AddWithValue("@Transaction", 'A');

        //            cmd.ExecuteNonQuery();

        //        }
        //    }
        //}


        private void BindCircularGrid()
        {
            string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("CircularClassGrid", conn);
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt= new DataTable();
                da.Fill(dt);

               GridView1.DataSource= dt;
                GridView1.DataBind();
            }
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            string myValue = hfCircularID.Value;

            int hfID;
            bool isUpdate = int.TryParse(hfCircularID.Value, out hfID);
            int CircularID = 0;


        
            //bool isUpdate = int.TryParse(hfCircularID.Value, out hfID) && hfID > 0;

            string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            DateTime dt;
            if (!DateTime.TryParse(txtDate.Text, out dt))
            {
                dt = DateTime.Now; 
            }


            if (isUpdate)
            {
               

                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("CircularCRUD", con);
                  
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
                    cmd.Parameters.AddWithValue("@Discription", txtDiscription.Text);
                    cmd.Parameters.AddWithValue("@Date", dt);
                    cmd.Parameters.AddWithValue("@Transaction",  'U' );

                    SqlParameter idParam = new SqlParameter("@ID", SqlDbType.Int);
                    idParam.Direction = ParameterDirection.InputOutput;
                    idParam.Value = isUpdate ? hfID : 0;
                    cmd.Parameters.Add(idParam);

                    cmd.ExecuteNonQuery();

                    CircularID = Convert.ToInt32(idParam.Value);

                    foreach (ListItem item in cblClass.Items)
                    {
                        if (item.Selected)
                        {
                            using (SqlCommand cmdClass = new SqlCommand(
                                "INSERT INTO TblCircularClass (CircularID, ClassID) VALUES (@CircularID, @ClassID)",
                                con))
                            {
                                cmdClass.Parameters.AddWithValue("@CircularID", CircularID);
                                cmdClass.Parameters.AddWithValue("@ClassID", item.Value);
                                cmdClass.ExecuteNonQuery();
                            }
                        }
                    }

                }



            }
            else
            {
                using (SqlConnection con = new SqlConnection(cs))
                {


                    SqlCommand cmd = new SqlCommand("CircularCRUD", con);
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
                    cmd.Parameters.AddWithValue("@Discription", txtDiscription.Text);
                    cmd.Parameters.AddWithValue("@Date", txtDate.Text);
                    cmd.Parameters.AddWithValue("@Transaction", 'A');
                    SqlParameter idParam = new SqlParameter("@ID", SqlDbType.Int);
                    idParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(idParam);

                    cmd.ExecuteNonQuery();

                     CircularID = Convert.ToInt32(idParam.Value);



                    foreach (ListItem item in cblClass.Items)
                    {
                        if (item.Selected)
                        {
                            SqlCommand cmdClass = new SqlCommand(
                                "INSERT INTO TblCircularClass (CircularID, ClassID) VALUES (@CircularID, @ClassID)", con);

                            cmdClass.Parameters.AddWithValue("@CircularID", CircularID);
                            cmdClass.Parameters.AddWithValue("@ClassID", item.Value);
                            cmdClass.ExecuteNonQuery();
                        }
                    }


                    con.Close();

                    BindCircularGrid();
                }
            }



            //using (SqlConnection con = new SqlConnection(
            //       ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            //{
              

            //    using (SqlTransaction tran = con.BeginTransaction())
            //    {
            //        try
            //        {
                        
            //            using (SqlCommand cmd = new SqlCommand("CircularCRUD", con, tran))
            //            {
            //                cmd.CommandType = CommandType.StoredProcedure;

            //                cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
            //                cmd.Parameters.AddWithValue("@Discription", txtDiscription.Text);
            //                cmd.Parameters.AddWithValue("@Date", dt);
            //                cmd.Parameters.AddWithValue("@Transaction", isUpdate ? 'U' : 'A');

            //                SqlParameter idParam = new SqlParameter("@ID", SqlDbType.Int);
            //                idParam.Direction = ParameterDirection.InputOutput;
            //                idParam.Value = isUpdate ? hfID : 0; 
            //                cmd.Parameters.Add(idParam);

            //                cmd.ExecuteNonQuery();

            //                CircularID = Convert.ToInt32(idParam.Value); 
            //            }

                    
            //            foreach (ListItem item in cblClass.Items)
            //            {
            //                if (item.Selected)
            //                {
            //                    using (SqlCommand cmdClass = new SqlCommand(
            //                        "INSERT INTO TblCircularClass (CircularID, ClassID) VALUES (@CircularID, @ClassID)",
            //                        con, tran))
            //                    {
            //                        cmdClass.Parameters.AddWithValue("@CircularID", CircularID);
            //                        cmdClass.Parameters.AddWithValue("@ClassID", item.Value);
            //                        cmdClass.ExecuteNonQuery();
            //                    }
            //                }
            //            }

            //            tran.Commit();
            //        }
            //        catch (Exception ex)
            //        {
            //            tran.Rollback();
            //            // Optionally show or log error
            //            throw new Exception("Error saving circular: " + ex.Message);
            //        }
            //    }
            //}

            //// Refresh the grid
            //BindCircularGrid();
        }


        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    int CircularID = 0;

        //    // Safely parse HiddenField value
        //    int hfID = 0;
        //    bool isUpdate = int.TryParse(hfCircularID.Value, out hfID) && hfID > 0;

        //    // Safely parse date
        //    DateTime dt;
        //    if (!DateTime.TryParse(txtDate.Text, out dt))
        //    {
        //        dt = DateTime.Now; // or show error message
        //    }

        //    using (SqlConnection con = new SqlConnection(
        //           ConfigurationManager.ConnectionStrings["con"].ConnectionString))
        //    {
        //        con.Open();

        //        using (SqlTransaction tran = con.BeginTransaction())
        //        {
        //            try
        //            {
        //                // 1️⃣ Insert / Update TblCircular
        //                using (SqlCommand cmd = new SqlCommand("CircularCRUD", con, tran))
        //                {
        //                    cmd.CommandType = CommandType.StoredProcedure;

        //                    cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
        //                    cmd.Parameters.AddWithValue("@Discription", txtDiscription.Text);
        //                    cmd.Parameters.AddWithValue("@Date", dt);
        //                    cmd.Parameters.AddWithValue("@Transaction", isUpdate ? 'U' : 'A');

        //                    SqlParameter idParam = new SqlParameter("@ID", SqlDbType.Int);
        //                    idParam.Direction = ParameterDirection.InputOutput;
        //                    idParam.Value = isUpdate ? hfID : 0;
        //                    cmd.Parameters.Add(idParam);

        //                    cmd.ExecuteNonQuery();

        //                    CircularID = Convert.ToInt32(idParam.Value);
        //                }

        //                // 2️⃣ Delete old classes if UPDATE
        //                if (isUpdate)
        //                {
        //                    using (SqlCommand deleteCmd = new SqlCommand(
        //                        "DELETE FROM TblCircularClass WHERE CircularID = @CircularID",
        //                        con, tran))
        //                    {
        //                        deleteCmd.Parameters.AddWithValue("@CircularID", CircularID);
        //                        deleteCmd.ExecuteNonQuery();
        //                    }
        //                }

        //                // 3️⃣ Re-insert selected classes
        //                foreach (ListItem item in cblClass.Items)
        //                {
        //                    if (item.Selected)
        //                    {
        //                        using (SqlCommand cmdClass = new SqlCommand(
        //                            "INSERT INTO TblCircularClass (CircularID, ClassID) VALUES (@CircularID, @ClassID)",
        //                            con, tran))
        //                        {
        //                            cmdClass.Parameters.AddWithValue("@CircularID", CircularID);
        //                            cmdClass.Parameters.AddWithValue("@ClassID", item.Value);
        //                            cmdClass.ExecuteNonQuery();
        //                        }
        //                    }
        //                }

        //                tran.Commit();
        //            }
        //            catch (Exception ex)
        //            {
        //                tran.Rollback();
        //                // Optionally log the error
        //                throw new Exception("Error saving circular: " + ex.Message);
        //            }
        //        }
        //    }

        //    BindCircularGrid();
        //}




        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    int CircularID = 0;
        //    bool isUpdate = hfCircularID.Value != "0";

        //    using (SqlConnection con = new SqlConnection(
        //           ConfigurationManager.ConnectionStrings["con"].ConnectionString))
        //    {
        //        con.Open();

        //        SqlTransaction tran = con.BeginTransaction();

        //        try
        //        {
        //            // 1️⃣ Insert / Update TblCircular
        //            SqlCommand cmd = new SqlCommand("CircularCRUD", con, tran);
        //            cmd.CommandType = CommandType.StoredProcedure;

        //            cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
        //            cmd.Parameters.AddWithValue("@Discription", txtDiscription.Text);
        //            cmd.Parameters.AddWithValue("@Date", txtDate.Text);
        //            cmd.Parameters.AddWithValue("@Transaction", isUpdate ? 'U' : 'A');

        //            SqlParameter idParam = new SqlParameter("@ID", SqlDbType.Int);
        //            idParam.Direction = ParameterDirection.InputOutput;

        //            idParam.Value = isUpdate ? Convert.ToInt32(hfCircularID.Value) : 0;
        //            cmd.Parameters.Add(idParam);

        //            cmd.ExecuteNonQuery();

        //            CircularID = Convert.ToInt32(idParam.Value);

        //            // 2️⃣ Delete old classes if UPDATE
        //            if (isUpdate)
        //            {
        //                SqlCommand deleteCmd = new SqlCommand(
        //                    "DELETE FROM TblCircularClass WHERE CircularID = @CircularID",
        //                    con, tran);

        //                deleteCmd.Parameters.AddWithValue("@CircularID", CircularID);
        //                deleteCmd.ExecuteNonQuery();
        //            }

        //            // 3️⃣ Re-insert selected classes
        //            foreach (ListItem item in cblClass.Items)
        //            {
        //                if (item.Selected)
        //                {
        //                    SqlCommand cmdClass = new SqlCommand(
        //                        "INSERT INTO TblCircularClass (CircularID, ClassID) VALUES (@CircularID, @ClassID)",
        //                        con, tran);

        //                    cmdClass.Parameters.AddWithValue("@CircularID", CircularID);
        //                    cmdClass.Parameters.AddWithValue("@ClassID", item.Value);
        //                    cmdClass.ExecuteNonQuery();
        //                }
        //            }

        //            tran.Commit();
        //        }
        //        catch
        //        {
        //            tran.Rollback();
        //            throw;
        //        }
        //    }

        //    BindCircularGrid();
        //}




        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);

        //    con.Open();




        //    SqlCommand cmd = new SqlCommand("CircularCRUD", con);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
        //    cmd.Parameters.AddWithValue("@Discription", txtDiscription.Text);
        //    cmd.Parameters.AddWithValue("@Date", txtDate.Text);
        //    cmd.Parameters.AddWithValue("@Transaction", 'A');
        //    SqlParameter idParam = new SqlParameter("@ID", SqlDbType.Int);
        //    idParam.Direction = ParameterDirection.Output;
        //    cmd.Parameters.Add(idParam);

        //    cmd.ExecuteNonQuery();

        //    int CircularID = Convert.ToInt32(idParam.Value);



        //    foreach (ListItem item in cblClass.Items)
        //    {
        //        if (item.Selected)
        //        {
        //            SqlCommand cmdClass = new SqlCommand(
        //                "INSERT INTO TblCircularClass (CircularID, ClassID) VALUES (@CircularID, @ClassID)", con);

        //            cmdClass.Parameters.AddWithValue("@CircularID", CircularID);
        //            cmdClass.Parameters.AddWithValue("@ClassID", item.Value);
        //            cmdClass.ExecuteNonQuery();
        //        }
        //    }


        //    con.Close();

        //    BindCircularGrid();



        //}

        public void TestGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "btn_update")
            {
                getRowForUpdate(ID);
            }

        }



        //protected void getRowForUpdate(int ID)
        //{
        //    string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        //    List<string> selectedIds = new List<string>();

        //    using (SqlConnection conn = new SqlConnection(cs))
        //    {

        //        SqlCommand cmd = new SqlCommand("CircularCRUD", conn);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@Title", "");
        //        cmd.Parameters.AddWithValue("@Discription", "");
        //        cmd.Parameters.AddWithValue("@Date", "");
        //        cmd.Parameters.AddWithValue("@ID", ID);
        //        cmd.Parameters.AddWithValue("@Transaction", 'R');

        //        conn.Open();
        //        SqlDataReader dr = cmd.ExecuteReader();


        //        if (dr.Read())
        //        {
        //            txtTitle.Text = dr["Title"].ToString();
        //            txtDiscription.Text = dr["Discription"].ToString();
        //            txtDate.Text = dr["Date"].ToString();

        //        }


        //        foreach (ListItem item in cblClass.Items)
        //        {
        //            if (item.Selected)
        //            {
        //                SqlCommand cmdClass = new SqlCommand(
        //                    "select ClassID from TblCircularClass where CircularID=@ID ", conn);

        //                cmd.Parameters.AddWithValue("CircularID", ID);


        //                SqlDataReader dr1= cmdClass.ExecuteReader();
        //                selectedIds.Add(dr["PageID"].ToString());

        //            }
        //        }

        //    }





        //}

        protected void getRowForUpdate(int ID)
        {
            
            string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();

             
                SqlCommand cmd = new SqlCommand("CircularCRUD", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Title", "");
                cmd.Parameters.AddWithValue("@Discription", "");
                cmd.Parameters.AddWithValue("@Date", "");
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@Transaction", "R");

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtTitle.Text = dr["Title"].ToString();
                    txtDiscription.Text = dr["Discription"].ToString();
                    txtDate.Text = dr["Date"].ToString();
                }
                dr.Close();

        
                List<string> selectedClassIds = new List<string>();

                SqlCommand cmdClass = new SqlCommand(
                    "SELECT ClassID FROM TblCircularClass WHERE CircularID = @CircularID", conn);

                cmdClass.Parameters.AddWithValue("@CircularID", ID);

                SqlDataReader drClass = cmdClass.ExecuteReader();
                while (drClass.Read())
                {
                    selectedClassIds.Add(drClass["ClassID"].ToString());
                }
                drClass.Close();

              
                foreach (ListItem item in cblClass.Items)
                {
                    if (selectedClassIds.Contains(item.Value))
                    {
                        item.Selected = true;
                    }
                }


            }

            hfCircularID.Value = ID.ToString();
        }





        //protected void btnEdit_Click(object sender, EventArgs e)
        //{
        //    int circularID = Convert.ToInt32(((LinkButton)sender).CommandArgument);
        //    hfCircularID.Value = circularID.ToString();

        //    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
        //    using (SqlCommand cmd = new SqlCommand("CircularCRUD", con))
        //    {
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@ID", circularID);
        //        cmd.Parameters.AddWithValue("@Title", "");
        //        cmd.Parameters.AddWithValue("@Discription", "");
        //        cmd.Parameters.AddWithValue("@Date", DateTime.Now);
        //        cmd.Parameters.AddWithValue("@Transaction", "R");

        //        con.Open();
        //        SqlDataReader dr = cmd.ExecuteReader();

        //        if (dr.Read())
        //        {
        //            txtTitle.Text = dr["Title"].ToString();
        //            txtDiscription.Text = dr["Discription"].ToString();
        //            txtDate.Text = Convert.ToDateTime(dr["Date"]).ToString("yyyy-MM-dd");
        //        }
        //    }

        //    LoadCircularClasses(circularID);
        //}


        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    int circularID = 0;

        //    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
        //    using (SqlCommand cmd = new SqlCommand("CircularCRUD", con))
        //    {
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cmd.Parameters.Add("@Title", SqlDbType.NVarChar, 50).Value = txtTitle.Text;
        //        cmd.Parameters.Add("@Discription", SqlDbType.NVarChar, 100).Value = txtDiscription.Text;
        //        cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = Convert.ToDateTime(txtDate.Text);
        //        cmd.Parameters.Add("@Transaction", SqlDbType.Char, 1);
        //        cmd.Parameters.Add("@ID", SqlDbType.Int);

        //        con.Open();

        //        if (!string.IsNullOrEmpty(hfCircularID.Value))
        //        {
        //            circularID = Convert.ToInt32(hfCircularID.Value);

        //            cmd.Parameters["@ID"].Value = circularID;
        //            cmd.Parameters["@Transaction"].Value = "U";

        //            cmd.ExecuteNonQuery();

        //            SqlCommand deleteCmd = new SqlCommand(
        //                "DELETE FROM TblCircularClass WHERE CircularID=@CircularID", con);
        //            deleteCmd.Parameters.AddWithValue("@CircularID", circularID);
        //            SqlCommand deleteCmd1 = new SqlCommand(
        //                "DELETE FROM TblCircular WHERE ID=@ID", con);
        //            deleteCmd.Parameters.AddWithValue("@ID", ID);
        //            deleteCmd.ExecuteNonQuery();
        //        }
        //        else
        //        {
        //            cmd.Parameters["@ID"].Value = 0; // or DBNull.Value if ID is not used
        //            cmd.Parameters["@Transaction"].Value = "A";

        //            circularID = Convert.ToInt32(cmd.ExecuteScalar());
        //        }

        //        foreach (ListItem item in cblClass.Items)
        //        {
        //            if (item.Selected)
        //            {
        //                SqlCommand cmdClass = new SqlCommand(
        //                    "INSERT INTO TblCircularClass (CircularID, ClassID) VALUES (@CircularID, @ClassID)", con);

        //                cmdClass.Parameters.AddWithValue("@CircularID", circularID);
        //                cmdClass.Parameters.AddWithValue("@ClassID", item.Value);
        //                cmdClass.ExecuteNonQuery();
        //            }
        //        }
        //    }

        //    BindCircularGrid();
        //}




        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    int circularID = 0;

        //    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
        //    using (SqlCommand cmd = new SqlCommand("CircularCRUD", con))
        //    {
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
        //        cmd.Parameters.AddWithValue("@Discription", txtDiscription.Text);
        //        cmd.Parameters.AddWithValue("@Date", Convert.ToDateTime(txtDate.Text));
        //        cmd.Parameters.AddWithValue("@Transaction", "U");

        //        con.Open();


        //        if (!string.IsNullOrEmpty(hfCircularID.Value))
        //        {
        //            circularID = Convert.ToInt32(hfCircularID.Value);
        //            cmd.Parameters.AddWithValue("@ID", circularID);
        //            cmd.Parameters.AddWithValue("@Transaction", "U");
        //            cmd.ExecuteNonQuery();

        //            SqlCommand deleteCmd = new SqlCommand(
        //                "DELETE FROM TblCircularClass WHERE CircularID=@CircularID", con);
        //            deleteCmd.Parameters.AddWithValue("@CircularID", circularID);
        //            deleteCmd.ExecuteNonQuery();
        //        }

        //        else
        //        {
        //            cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
        //            cmd.Parameters.AddWithValue("@Discription", txtDiscription.Text);
        //            cmd.Parameters.AddWithValue("@Date", Convert.ToDateTime(txtDate.Text));
        //            cmd.Parameters.AddWithValue("@Transaction", "A");

        //            //int circularID = Convert.ToInt32(cmd.ExecuteScalar());

        //            circularID = Convert.ToInt32(cmd.ExecuteScalar());
        //        }


        //        foreach (ListItem item in cblClass.Items)
        //        {
        //            if (item.Selected)
        //            {
        //                SqlCommand cmdClass = new SqlCommand(
        //                    "INSERT INTO TblCircularClass (CircularID, ClassID) VALUES (@CircularID, @ClassID)", con);

        //                cmdClass.Parameters.AddWithValue("@CircularID", circularID);
        //                cmdClass.Parameters.AddWithValue("@ClassID", item.Value);
        //                cmdClass.ExecuteNonQuery();
        //            }
        //        }
        //    }

        //    BindCircularGrid();
        //}


        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        //    con.Open();

        //    SqlCommand cmd = new SqlCommand("CircularCRUD", con);
        //    cmd.CommandType = CommandType.StoredProcedure;

        //    cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
        //    cmd.Parameters.AddWithValue("@Discription", txtDiscription.Text);
        //    cmd.Parameters.AddWithValue("@Date", txtDate.Text);

        //    int CircularID;


        //    if (!string.IsNullOrEmpty(hfCircularID.Value))
        //    {
        //        CircularID = Convert.ToInt32(hfCircularID.Value);
        //        cmd.Parameters.AddWithValue("@CircularID", CircularID);
        //        cmd.Parameters.AddWithValue("@Transaction", 'U');
        //        cmd.ExecuteNonQuery();


        //        SqlCommand deleteCmd = new SqlCommand(
        //            "DELETE FROM TblCircularClass WHERE CircularID=@CircularID", con);
        //        deleteCmd.Parameters.AddWithValue("@CircularID", CircularID);
        //        deleteCmd.ExecuteNonQuery();
        //    }

        //    else
        //    {
        //        cmd.Parameters.AddWithValue("@Transaction", 'A');
        //        cmd.Parameters.AddWithValue("@ID", 8);

        //        CircularID = Convert.ToInt32(cmd.ExecuteScalar());
        //    }


        //    foreach (ListItem item in cblClass.Items)
        //    {
        //        if (item.Selected)
        //        {
        //            SqlCommand cmdClass = new SqlCommand(
        //                "INSERT INTO TblCircularClass (CircularID, ClassID) VALUES (@CircularID, @ClassID)", con);

        //            cmdClass.Parameters.AddWithValue("@CircularID", CircularID);
        //            cmdClass.Parameters.AddWithValue("@ClassID", item.Value);
        //            cmdClass.ExecuteNonQuery();
        //        }
        //    }

        //    con.Close();
        //    BindCircularGrid();

        //}


        //private void LoadCircularClasses(int circularID)
        //{   
        //    foreach (ListItem item in cblClass.Items)
        //        item.Selected = false;

        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        //    SqlCommand cmd = new SqlCommand(
        //        "SELECT ClassID FROM TblCircularClass WHERE CircularID=@CircularID", con);
        //    cmd.Parameters.AddWithValue("@CircularID", circularID);

        //    con.Open();
        //    SqlDataReader dr = cmd.ExecuteReader();
        //    while (dr.Read())
        //    {
        //        ListItem item = cblClass.Items.FindByValue(dr["ClassID"].ToString());
        //        if (item != null) item.Selected = true;
        //    }
        //    con.Close();
        //}




    }
}


