using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using EmployeeData.Models;

namespace EmployeeData.Controllers
{
    public class DepartmentController : ApiController
    {
        // GET: Department
        public HttpResponseMessage Get()
        {
            string query = @"
                select DepartmentId,DepartmentName from
                dbo.Department
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
        public string Post(Department dep)
        {
            try
            {
                string query = @"
                        insert into dbo.Department values
                        ('" + dep.DepartmentName + @"')
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

        public string Put(Department dep)   
        {
            try
            {
                string query = @"
                        update dbo.Department set DepartmentName=
                        '" + dep.DepartmentName + @"'
                        where DepartmentId="+dep.DepartmentId+@"
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
                        update dbo.Department 
                        where DepartmentId=" + id + @"
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
    }
}
