using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class WebForm11Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Btn_Login(object sender, EventArgs e)
        {
            string userId = txtUserId.Text;
            string password = txtPassword.Text;
            string uId = "";
            string pass = "";
            string name = "";
            string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection conn= new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("select Name, UserId, Password from TblReg ", conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    uId = dr["UserId"].ToString();
                    pass = dr["Password"].ToString();
                         name= dr["Name"].ToString();

                }

                
            }
            if(uId==userId && pass== password)
            {
                labelLogin.Text = "Login successful";
                labelLogin.ForeColor = System.Drawing.Color.Green;
                Session["Name"]= name;
                Response.Redirect("WebForm11Dashboard.aspx");
            }
        }
    }
}