using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using EmployeeData.Models;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using System.Net;

namespace EmployeeData.Controllers
{
    public class EmployeeController : ApiController
    {
        // GET: Employee
        public HttpResponseMessage Get()
        {
            string query = @"
                select EmployeeId,EmployeeName,Department,
                convert(varchar(10),DateOfJoining,120) as DateOfJoining,
                PhotoFileName
                from
                dbo.Employee
                ";
            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.
                     ConnectionStrings["EmployeeAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, table);
            }
        }
        public string Post(Employee emp)
        {
            try
            {
                string query = @"
                        insert into dbo.Employee values
                        (
                        '" + emp.EmployeeName + @"'
                       , '" + emp.Department + @"'
                       , '" + emp.DateOfJoining + @"'
                       , '" + emp.PhotoFileName + @"'
                        )
                        ";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.
                         ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Added Succesfully";
            }
            catch (Exception)
            {
                return "Failed to Add";
            }
        }

        public string Put(Employee emp)
        {
            try
            {
                string query = @"
                        update dbo.Employee set 
                        EmployeeName='" + emp.EmployeeName +@"'
                        ,Department='" + emp.Department + @"'
                        ,DateOfJoining='" + emp.DateOfJoining + @"'
                        ,PhotoFileName='" + emp.PhotoFileName + @"'
                        where EmployeeId=" + emp.EmployeeId + @"
                        ";

                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.
                         ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Updated Succesfully";
            }
            catch (Exception)
            {
                return "Failed to Update";
            }
        }
        public string Delete(int id)
        {
            try
            {
                string query = @"
                        delete dbo.Employee 
                        where EmployeeId=" + id + @"
                        ";

                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.
                         ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Deleted Succesfully";
            }
            catch (Exception)
            {
                return "Failed to Delete";
            }
        }

        [Route("api/Employee/GetAllDepartmentNames")]
        [HttpGet]
        public HttpResponseMessage GetAllDepartmentNames()
        {
            string query = @"
                select DepartmentName from dbo.Department";

            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.
                ConnectionStrings["EmployeeDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK,table);
        }

        [Route("api/Employee/SaveFile")]
        public string SaveFile()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = HttpContext.Current.Server.MapPath("~/Photos/"  + filename);

                postedFile.SaveAs(physicalPath);

                return filename;
            }

            catch (Exception)
            {
                return "annonymous.png";
            }
        }
    }
}
