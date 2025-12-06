using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using WebApplication1.Models;

namespace WebApplication1
{
    public partial class WebForm1 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;

            if (!IsPostBack)
            {

                LoadCities();  // Bind dropdown from DB
                BindGrid();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string email=txtEmail.Text;
            string mobile = txtMobile.Text;
          
            string gender = txtGender.SelectedValue;

            string city = ddlCity.Text;
            string filePath = null;

            if (txtimg.HasFile)
            {
                string fileName = Path.GetFileName(txtimg.PostedFile.FileName);
                 filePath = "~/Images/" + fileName;

                // Ensure folder exists
                string folderPath = Server.MapPath("~/Images/");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                // Save image
                txtimg.SaveAs(Server.MapPath(filePath));

                // Bind image to Image control
                imgPreview.ImageUrl = filePath;
            }

            string adhaarNo = txtAdhaar.Text;



            string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("RegisterCrud", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID", 22);

                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Mobile", mobile);
                cmd.Parameters.AddWithValue("@Gender", gender);
                cmd.Parameters.AddWithValue("@CityID", city);
                cmd.Parameters.AddWithValue("@Image", filePath ?? "NULL");
                cmd.Parameters.AddWithValue("@AdhaarNo", adhaarNo);
                cmd.Parameters.AddWithValue("@Transaction", 'A');

                con.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    lblMessage.Text = "Data added successfully!";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    BindGrid();

                }
                else
                {
                    lblMessage.Text = "Something went Wrong!";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }

        }




        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName== "getRowforUpdation")
            {
              
                getRowforUpdation(id);
              
            }
            if(e.CommandName=="deleteRow")
            {
                DeleteRecord(id);      // Delete selected row
                BindGrid();

            }

        }
        
 


        protected void getRowforUpdation(int id)
        {
            string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("RegisterCrud", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", "");
                cmd.Parameters.AddWithValue("@Email", "");
                cmd.Parameters.AddWithValue("@Mobile", "");
                cmd.Parameters.AddWithValue("@Gender", "");
                cmd.Parameters.AddWithValue("@CityID", "");
                cmd.Parameters.AddWithValue("@Image", "");
                cmd.Parameters.AddWithValue("@AdhaarNo", "");
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.Parameters.AddWithValue("@Transaction", 'R');
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    LoadCities();
                    txtName.Text = dr["Name"].ToString();
                    txtEmail.Text= dr["Email"].ToString();
                    txtMobile.Text = dr["Mobile"].ToString();
                    txtGender.SelectedValue = dr["Gender"].ToString();
                    //ddlCity.SelectedValue = dr["CityID"].ToString();

                    string cityId = dr["CityID"].ToString();
                    if (ddlCity.Items.FindByValue(cityId) != null)
                    {
                        ddlCity.SelectedValue = cityId;
                    }
                    imgPreview.ImageUrl = "~/uploads/" + dr["Image"].ToString();
                    txtAdhaar.Text = dr["AdhaarNo"].ToString();
                    hfID.Value = id.ToString();

                }




            }



            }


        protected void DeleteRecord(int id)
        {
            string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection con = new SqlConnection(cs))
            {

                SqlCommand cmd = new SqlCommand("RegisterCrud", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", "");
                cmd.Parameters.AddWithValue("@Email", "");
                cmd.Parameters.AddWithValue("@Mobile", "");
                cmd.Parameters.AddWithValue("@Gender", "");
                cmd.Parameters.AddWithValue("@CityID", "");
                cmd.Parameters.AddWithValue("@Image", "");
                cmd.Parameters.AddWithValue("@AdhaarNo", "");
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.Parameters.AddWithValue("@Transaction", 'D');
                con.Open();
                 cmd.ExecuteNonQuery();

                



            }





        }



        protected void btnUpdate_Click(object sender, EventArgs e)
            {
            string name = txtName.Text;
            string email = txtEmail.Text;
            string mobile = txtMobile.Text;
            string gender = txtGender.SelectedValue;

            string city = ddlCity.Text;
            string filePath = null;
           
                  if (txtimg.HasFile)
            {
                string fileName = Path.GetFileName(txtimg.PostedFile.FileName);
                filePath = "~/Images/" + fileName;

                // Ensure folder exists
                string folderPath = Server.MapPath("~/Images/");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                // Save image
                txtimg.SaveAs(Server.MapPath(filePath));

                // Bind image to Image control
                imgPreview.ImageUrl = filePath;
            }

            string adhaarNo = txtAdhaar.Text;



            string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
          using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("RegisterCrud", con); 
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(hfID.Value));

                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Mobile", mobile);
                cmd.Parameters.AddWithValue("@Gender", gender);
                cmd.Parameters.AddWithValue("@CityID", city);
                cmd.Parameters.AddWithValue("@Image", filePath ?? "NULL");
                cmd.Parameters.AddWithValue("@AdhaarNo", adhaarNo);
                cmd.Parameters.AddWithValue("@Transaction", 'U');

                con.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    lblMessage.Text = "Data updated successfully!";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    BindGrid();

                }
                else
                {
                    lblMessage.Text = "Something went Wrong!";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }

        }


        private void BindGrid()
        {   
            string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            //using (SqlConnection con = new SqlConnection(cs))
            //{
            //    SqlCommand cmd = new SqlCommand("SELECT Name, Email, Mobile, Gender, City, AdhaarNo, Image FROM TblRegistration", con);

            //    con.Open();
            //    SqlDataAdapter da = new SqlDataAdapter(cmd);

            //    DataTable dt = new DataTable();
            //    da.Fill(dt);

            //    GridView1.DataSource = dt;
            //    GridView1.DataBind();
            //}

            using (SqlConnection con = new SqlConnection(cs))
            {

                SqlCommand cmd = new SqlCommand("RegisterCrud", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", "");
                cmd.Parameters.AddWithValue("@Email", "");
                cmd.Parameters.AddWithValue("@Mobile", "");
                cmd.Parameters.AddWithValue("@Gender", "");
                cmd.Parameters.AddWithValue("@CityID", "");
                cmd.Parameters.AddWithValue("@Image", "");
                cmd.Parameters.AddWithValue("@AdhaarNo", "");
                cmd.Parameters.AddWithValue("@ID", "");
                cmd.Parameters.AddWithValue("@Transaction", 'G');

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                da.Fill(dt);

                GridView1.DataSource = dt;
                GridView1.DataBind();

            }

        }

        private void LoadCities()
        {
            string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("SELECT CID, CityName FROM TblCity", con);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                ddlCity.DataSource = dr;
                ddlCity.DataTextField = "CityName";   // What user sees
                ddlCity.DataValueField = "CID";    // ID stored internally
                ddlCity.DataBind();
                dr.Close();
            }

            // Insert first empty item
            ddlCity.Items.Insert(0, new ListItem("-- Select City --", ""));
        }




    }
}
    