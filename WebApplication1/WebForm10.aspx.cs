using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace WebApplication1
{
    public partial class WebForm10 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


        }


        public string GenerateRandomPassword(int length)
        {
            string varcharPass = "abcdefijklmnopqrstuvwxyzABCDEFIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()_+-?/.,<>;:|{}[]";
            Random rand = new Random();
            string arr = "";
            for (int i = 0; i < length; i++)
            {
                arr = arr + varcharPass[rand.Next(0, varcharPass.Length)];
            }
            return arr;

        }

        public string GenerateUserId(string name)
        {
            string prefix = name.Length > 3 ? name.Substring(0, 3).ToUpper() : name.ToUpper();
            Random rand= new Random();
            string username= prefix+rand.Next(1000,9999).ToString();
            return username;
        }

            protected void Rg_submit(object sender, EventArgs e)
            {
                string studentName = txtName.Text;
                string mobile = txtMobile.Text;
                string email = txtEmail.Text;

                if (string.IsNullOrEmpty(studentName) || string.IsNullOrEmpty(mobile) || string.IsNullOrEmpty(email))
                {
                    lblMessage.Text = "Please fill in all fields.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }

              
                string generatedUserId = GenerateUserId(studentName);
                string generatedPassword = GenerateRandomPassword(6); 

                
                try
                {
                    SaveStudentDetails(studentName, mobile, email, generatedUserId, generatedPassword);
                    lblMessage.Text = $"Registration successful! User ID: {generatedUserId}, Password: {generatedPassword}";
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "An error occurred during registration: " + ex.Message;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }


            //private string GenerateUserId(string name)
            //{
            //    string prefix = name.Length >= 3 ? name.Substring(0, 3).ToUpper() : name.ToUpper();
            //    Random rand = new Random();
            //    return prefix + rand.Next(1000, 9999).ToString();
            //}

        //private string GenerateRandomPassword(int length)
        //{
        //     string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()"; 
        //    Random rand = new Random();

        //    char[] chars = new char[length];
        //    for (int i = 0; i < length; i++)
        //    {
        //        chars[i] = validChars[rand.Next(0, validChars.Length)];
        //    }
        //    string password = new string(chars);
        //    return password;

           
        //}


        //private string GenerateRandomPassword(int length)
        //{
        //    const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()";
        //    StringBuilder password = new StringBuilder();



        //    using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
        //    {
        //        byte[] uintBuffer = new byte[sizeof(uint)];
        //        while (length-- > 0)
        //        {
        //            rng.GetBytes(uintBuffer);
        //            uint num = BitConverter.ToUInt32(uintBuffer, 0);
        //            password.Append(validChars[(int)(num % (uint)validChars.Length)]);
        //        }
        //    }
        //    return password.ToString();
        //}

        private void SaveStudentDetails(string name, string mobile, string email, string userId, string password)
            {

            string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(cs))
                {
                    using (SqlCommand command = new SqlCommand("RegCRUD", conn))

                    {
                    command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@Mobile", mobile);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@UserId", userId);
                        command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@Transaction", 'A');

                    conn.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
        
    

}
}