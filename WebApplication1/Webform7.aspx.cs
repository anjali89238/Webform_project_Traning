using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Webform7 : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            this.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            if (!IsPostBack)
            {
                txtSchoolId.Text = GenerateRandomSchoolID();
                LoadState();
              
            }
        }

        private void LoadState()
        {
            string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("SELECT StateID, StateName FROM TblState", con);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                ddlState.DataSource = dr;
                ddlState.DataTextField = "StateName";
                ddlState.DataValueField = "StateID";
                ddlState.DataBind();
                dr.Close();
            }

            ddlState.Items.Insert(0, new ListItem("-- Select State --", ""));
        }


        private void LoadCities(int StateId)
        {
            string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection con = new SqlConnection(cs))
            {

                SqlCommand cmd = new SqlCommand("SELECT C.CityID, C.CityName , C.StateID FROM TblCityByState as C Inner Join TblState as S on C.StateID = S.StateID where C.StateID=@StateID", con);
                cmd.Parameters.AddWithValue("@StateID", StateId);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                ddlCityState.DataSource = dr;
                ddlCityState.DataTextField = "CityName";
                ddlCityState.DataValueField = "CityID";
                ddlCityState.DataBind();
                dr.Close();
            }


            ddlCityState.Items.Insert(0, new ListItem("-- Select City --", ""));
        }

        public void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlState.SelectedValue))
            {
                int stateId = Convert.ToInt32(ddlState.SelectedValue);
                LoadCities(stateId);
            }
            else
            {
          
                ddlCityState.Items.Clear();
                ddlCityState.Items.Insert(0, new ListItem("-- Select City --", ""));
            }
        }

        private string GenerateRandomSchoolID()
        {
            Random random = new Random();
            const string chars = "0123456789";
            var idBuilder = new StringBuilder();

         
            idBuilder.Append("SCH");

           
            for (int i = 0; i < 3; i++)
            {
                idBuilder.Append(chars[random.Next(chars.Length)]);
            }

            return idBuilder.ToString();
        }

        public void btn_submit(object sender, EventArgs e)
        {
            string SchoolId = txtSchoolId.Text;
            string SchoolName= txtSchoolName.Text;
            string Address= txtAddess.Text;
            string PhoneNo= txtPhone.Text;
            string Mobile= txtMobile.Text;
            string Email= txtEmail.Text;
            string LogoPath = null;

            if (txtLogo.HasFile)
            {
                string FileName=Path.GetFileName(txtLogo.PostedFile.FileName);
                LogoPath = "~/Images/" + FileName;
                string folderPath = Server.MapPath("~/Images/");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

             
                txtLogo.SaveAs(Server.MapPath(LogoPath));

              
                imgLogoPreview.ImageUrl = LogoPath;
            }

            string WebAddress=txtWebAddress.Text;

            string PrincipalSignPath = null;

            if (txtPrincipalSign.HasFile)
            {
                string FileName = Path.GetFileName(txtPrincipalSign.PostedFile.FileName);
                PrincipalSignPath = "~/Images/" + FileName;
                string folderPath = Server.MapPath("~/Images/");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }


                txtPrincipalSign.SaveAs(Server.MapPath(PrincipalSignPath));


                imgPrincipalSignPreview.ImageUrl = PrincipalSignPath;
            }

            string BankName= txtBankName.Text;
            string BankAccountNo= txtBankAccountNo.Text;
            string BranchName= txtBranchName.Text;
            string AndriodAppLink= txtAndriodAppLink.Text;
            string PinCode= txtPinCode.Text;
            int StateId = Convert.ToInt32(ddlState.SelectedValue);
            int CityId = Convert.ToInt32(ddlCityState.SelectedValue);
            //int AppStatusId= Convert.ToInt32(ddlAppStatus.SelectedValue);
            string AppStatusId = ddlAppStatus.SelectedItem.Text;
            


                        string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("SchoolCRUD", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolID", SchoolId);
                cmd.Parameters.AddWithValue("@SchoolName", SchoolName);
                cmd.Parameters.AddWithValue("@Address", Address);
                cmd.Parameters.AddWithValue("@PhoneNo", PhoneNo);
                cmd.Parameters.AddWithValue("@MobileNo", Mobile);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@Logo", LogoPath);
                cmd.Parameters.AddWithValue("@WebAddress", WebAddress);
                cmd.Parameters.AddWithValue("@PrincipalSign", PrincipalSignPath);
                cmd.Parameters.AddWithValue("@BankName", BankName);
                cmd.Parameters.AddWithValue("@BankAccountNo", BankAccountNo);
                cmd.Parameters.AddWithValue("@BranchName", BranchName);
                cmd.Parameters.AddWithValue("@AndriodAppLink", AndriodAppLink);
                cmd.Parameters.AddWithValue("@PinCode", PinCode);
                cmd.Parameters.AddWithValue("@State", StateId);
                cmd.Parameters.AddWithValue("@City", CityId);
                cmd.Parameters.AddWithValue("@AppStatus", AppStatusId);
                cmd.Parameters.AddWithValue("@Transaction",'A');
             
                conn.Open();

               int i= cmd.ExecuteNonQuery();

                if (i > 0)
                {
                    lblMessage.Text = "Data added successfully!";
                    lblMessage.ForeColor = System.Drawing.Color.Green;


                }
                else
                {
                    lblMessage.Text = "Something went Wrong!";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }

        }



    }
}

