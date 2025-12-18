using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class ReportWebForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindParentReport();
            }


        }
        public void BindParentReport()
        {
            string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            DataTable d1 = new DataTable();
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("Select * from TblStudentRegistration", conn);
                conn.Open();
             
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(d1);


            }
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/TestReport4.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();

            ReportDataSource rds = new ReportDataSource("DataSet1", d1);
            ReportViewer1.LocalReport.SubreportProcessing += BindSubReport;

            ReportViewer1.LocalReport.DataSources.Add(rds);
            ReportViewer1.LocalReport.Refresh();

        }

        public void BindSubReport(object sender, SubreportProcessingEventArgs e) {
            string FormNo = e.Parameters["FormNo"].Values[0];
            string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            DataTable d1 = new DataTable();
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("select m.Subject,m.Marks,m.MaxMarks from TblMarks as m inner join TblStudentRegistration as s on m.FormID= s.FormNo where s.FormNo=@FormNo", conn);
                cmd.Parameters.AddWithValue("@FormNo", FormNo);
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(d1);


            }
            e.DataSources.Add(new ReportDataSource("DataSet2", d1));
        }

    }
}