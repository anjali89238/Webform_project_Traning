using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace WebApplication1
{
    public partial class StudentRegistration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            if (!IsPostBack)
            {
                LoadState();
              
                LoadClass();
                LoadSession();
                LoadCategory();
                LoadBloodGroup();
                LoadReligion();
                BindStudentReport();

                BindMultipleReport();
             
              

            }
        }


        private void LoadClass()
        {
            string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("SELECT ClassID, ClassName FROM TblClass", con);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                ddlClass.DataSource = dr;
                ddlClass.DataTextField = "ClassName";   
                ddlClass.DataValueField = "ClassID";    
                ddlClass.DataBind();
                dr.Close();
            }

            ddlClass.Items.Insert(0, new ListItem("-- Select Class --", ""));
        }

        private void LoadSession()
        {
            string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("SELECT SessionID, SessionYear FROM TblSession", con);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                ddlSession.DataSource = dr;
                ddlSession.DataTextField = "SessionYear";   
                ddlSession.DataValueField = "SessionID";  
                ddlSession.DataBind();
                dr.Close();
            }

            ddlSession.Items.Insert(0, new ListItem("-- Select Session --", ""));
        }

        private void LoadReligion()
        {
            string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("SELECT ReligionID, ReligionName FROM TblReligion", con);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                ddlReligion.DataSource = dr;
                ddlReligion.DataTextField = "ReligionName";   
                ddlReligion.DataValueField = "ReligionID";    
                ddlReligion.DataBind();
                dr.Close();
            }

            ddlReligion.Items.Insert(0, new ListItem("-- Select Religion --", ""));
        }


        private void LoadCategory()
        {
            string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("SELECT CategoryID, CategoryName FROM TblCategory", con);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                ddlCategory.DataSource = dr;
                ddlCategory.DataTextField = "CategoryName";   
                ddlCategory.DataValueField = "CategoryID";   
                ddlCategory.DataBind();
                dr.Close();
            }


            ddlCategory.Items.Insert(0, new ListItem("-- Select Category --", ""));
        }



        private void LoadBloodGroup()
        {
            string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("SELECT BloodGroupID, BloodGroupName FROM TblBloodGroup", con);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                ddlBG.DataSource = dr;
                ddlBG.DataTextField = "BloodGroupName";   
                ddlBG.DataValueField = "BloodGroupID";    
                ddlBG.DataBind();
                dr.Close();
            }

            ddlBG.Items.Insert(0, new ListItem("-- Select BloodGroup --", ""));
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

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlState.SelectedValue))
            {
                int stateId = Convert.ToInt32(ddlState.SelectedValue);
                LoadCities(stateId);
            }
            else
            {
                // Clear city dropdown when no state selected
                ddlCityState.Items.Clear();
                ddlCityState.Items.Insert(0, new ListItem("-- Select City --", ""));
            }
        }


        protected void btn_submitData(object sender, EventArgs e)
        {
            int FormNo =  int.Parse(txtFormNo.Text);
            int classId = Convert.ToInt32(ddlClass.SelectedValue);
            int SessionId= Convert.ToInt32(ddlSession.SelectedValue);
            string name = txtName.Text;
            string DOB=txtDOB.Text;
            string gender = txtGender.SelectedValue;
            int BloodGroup= Convert.ToInt32(ddlBG.SelectedValue);
            int Religion = Convert.ToInt32(ddlReligion.SelectedValue);
            int Category = Convert.ToInt32(ddlCategory.SelectedValue);
            int State = Convert.ToInt32(ddlState.SelectedValue);
            int City = Convert.ToInt32(ddlCityState.SelectedValue);
            string Pin = txtPin.Text;
            string Address=txtAddress.Text;
            string AdhaarNo=txtAdhaar.Text;
            string FatherName=txtFatherName.Text;
            string MobileNo=txtMobile.Text;
            
            string email = txtEmail.Text;
            string MotherName = txtMotherName.Text;

            string mobile = txtMobile.Text;

            string subject = ddlSubject.SelectedValue;


            string filePath = null;

            if (txtImg.HasFile)
            {
                string fileName = Path.GetFileName(txtImg.PostedFile.FileName);
                filePath = "~/Images/" + fileName;

                // Ensure folder exists
                string folderPath = Server.MapPath("~/Images/");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                // Save image
                txtImg.SaveAs(Server.MapPath(filePath));

                // Bind image to Image control
                imgPreview.ImageUrl = filePath;
            }


            string filePath1 = null;

            if (txtFatherImg.HasFile)
            {
                string fileName1 = Path.GetFileName(txtFatherImg.PostedFile.FileName);
                filePath1 = "~/Images/" + fileName1;

                // Ensure folder exists
                string folderPath = Server.MapPath("~/Images/");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                // Save image
                txtFatherImg.SaveAs(Server.MapPath(filePath1));

                // Bind image to Image control
                FatherImgPreview.ImageUrl = filePath1;
            }

            string filePath2 = null;

            if (txtMotherImg.HasFile)
            {
                string fileName2 = Path.GetFileName(txtMotherImg.PostedFile.FileName);
                filePath2 = "~/Images/" + fileName2;

                // Ensure folder exists
                string folderPath = Server.MapPath("~/Images/");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                // Save image
                txtMotherImg.SaveAs(Server.MapPath(filePath2));

                // Bind image to Image control
                MotherImgPreview.ImageUrl = filePath2;
            }


            string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("StudentRegistrationCRUD", con);
               
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FormNo", 22);
                cmd.Parameters.AddWithValue("@Class", classId);
                cmd.Parameters.AddWithValue("@Session", SessionId);
                cmd.Parameters.AddWithValue("@Name", name); 
                cmd.Parameters.AddWithValue("@DOB", DOB);
                cmd.Parameters.AddWithValue("@Gender", gender);
                cmd.Parameters.AddWithValue("@BloodGroup", BloodGroup);
                cmd.Parameters.AddWithValue("@Religion", Religion);
                cmd.Parameters.AddWithValue("@Category", Category);
                cmd.Parameters.AddWithValue("@State", State);
                cmd.Parameters.AddWithValue("@City", City);
                cmd.Parameters.AddWithValue("@Pin", Pin);
                cmd.Parameters.AddWithValue("@Address", Address);
                cmd.Parameters.AddWithValue("@AdhaarNo", AdhaarNo);
                cmd.Parameters.AddWithValue("@FatherName", FatherName);
                cmd.Parameters.AddWithValue("@MobileNo", MobileNo);

                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@MotherName", MotherName);
                cmd.Parameters.AddWithValue("@StudentImage", filePath ?? "NULL");
                cmd.Parameters.AddWithValue("@FatherImage", filePath ?? "NULL");
                cmd.Parameters.AddWithValue("@MotherImage", filePath ?? "NULL");
                cmd.Parameters.AddWithValue("@Subject", subject);
                
                cmd.Parameters.AddWithValue("@Transaction", 'A');

                con.Open();
               
                    cmd.ExecuteNonQuery();
                //if (i > 0)
                //{
                //    lblMessage.Text = "Data added successfully!";
                //    lblMessage.ForeColor = System.Drawing.Color.Green;
                 

                //}
                //else
                //{
                //    lblMessage.Text = "Something went Wrong!";
                //    lblMessage.ForeColor = System.Drawing.Color.Red;
                //}
            }

        }


        private void BindStudentReport()
        {
            String cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            DataTable d1 = new DataTable();
            using (SqlConnection conn=new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("Select * from TblStudentRegistration", conn);

                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(d1);

            }


            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/StudentRegistrationReport.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();

            ReportDataSource rds = new ReportDataSource("DataSet1", d1);
            ReportViewer1.LocalReport.DataSources.Add(rds);
            ReportViewer1.LocalReport.Refresh();
        }

        private void BindMultipleReport()
        {
            String cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            DataTable d1 = new DataTable();
            using (SqlConnection conn=new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("Select * from TblStudentRegistration", conn);

                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(d1);

            }

            ReportViewer2.ProcessingMode = ProcessingMode.Local;
            ReportViewer2.LocalReport.ReportPath = Server.MapPath("~/Reports/Report2.rdlc");
            ReportViewer2.LocalReport.DataSources.Clear();

            ReportDataSource rds = new ReportDataSource("DataSet1", d1);
            
            ReportViewer2.LocalReport.SubreportProcessing += BindStudentSubreport;
            ReportViewer2.LocalReport.DataSources.Add(rds);
            ReportViewer2.LocalReport.Refresh();
        }
         

        public  void BindStudentSubreport(object sender, SubreportProcessingEventArgs e)
        {
            string formNo = e.Parameters["FormNo"].Values[0];
            string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            DataTable d1 = new DataTable();
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("select m.Subject,m.Marks,m.MaxMarks from TblMarks as m inner join TblStudentRegistration as s on m.FormID= s.FormNo where s.FormNo=@FormNo", conn);

                cmd.Parameters.AddWithValue("@FormNo", formNo);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(d1);
;           }

            //ReportViewer2.ProcessingMode = ProcessingMode.Local;
            //ReportViewer2.LocalReport.ReportPath = Server.MapPath("~/Reports/StudentRegistrationSubReport.rdlc");
            //ReportViewer2.LocalReport.DataSources.Clear();
           
            //ReportViewer2.LocalReport.DataSources.Add(rds);
            //ReportViewer2.LocalReport.Refresh();

            e.DataSources.Add(new ReportDataSource("DataSet1", d1));


        }

    }
}




