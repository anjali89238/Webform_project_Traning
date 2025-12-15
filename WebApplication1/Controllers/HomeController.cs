using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
      

        [HttpPost]
        public ActionResult Index(Register reg, HttpPostedFileBase Image)
        {

            if (Image != null)
            {
                string filename = Path.GetFileName(Image.FileName);
                string path = Server.MapPath("~/Uploads/" + filename);
                Image.SaveAs(path);
                reg.Image = filename;
            }

            if (!ModelState.IsValid)
            {
                // return same view and show errors
                return View(reg);
            }

            string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection con = new SqlConnection(cs)) 
            {
                SqlCommand cmd = new SqlCommand("RegisterCrud", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", reg.Name);
                cmd.Parameters.AddWithValue("@Email", reg.Email);
                cmd.Parameters.AddWithValue("@Mobile", reg.Mobile);
                cmd.Parameters.AddWithValue("@Gender", reg.Gender);
                cmd.Parameters.AddWithValue("@City", reg.City);
                cmd.Parameters.AddWithValue("@Image", reg.Image ?? "NULL");
                cmd.Parameters.AddWithValue("@AdhaarNo", reg.AdharNo);
                cmd.Parameters.AddWithValue("@Transaction", 'A');

                con.Open();
               int i= cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    TempData["Success"] = "Data added successfully!";
                    
                }
                else
                {
                    TempData["error"] = "Something went wrong!";
                }                
            }

            ViewBag.Message = "Data inserted successfully!";

            List<Register> list = new List<Register>();
            list = BindingList();
            return RedirectToAction("Index");
        }

        public ActionResult Index()
        {
            List<Register> list = new List<Register>();
            list = BindingList();

            return View(list);
        }

        public List<Register> BindingList()
        {
            List<Register> list = new List<Register>();

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
                cmd.Parameters.AddWithValue("@ID", "");
                cmd.Parameters.AddWithValue("@Transaction", 'G');

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    list.Add(new Register
                    {
                        Name = dr["Name"].ToString(),
                        Email = dr["Email"].ToString(),
                        Mobile = dr["Mobile"].ToString(),
                        Gender = dr["Gender"].ToString(),
                        City = dr["CityID"].ToString(),
                        Image = dr["Image"].ToString(),
                        AdharNo = dr["AdhaarNo"].ToString()
                    });
                } 
            }
            return list;
        }



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}
