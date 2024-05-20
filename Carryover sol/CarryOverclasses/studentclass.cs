using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carryover_sol.CarryOverclasses;
namespace Carryover_sol.CarryOverclasses
{
    internal class studentclass
    {
        //getter setter properties to carry data
        public string student_id { get; set; }
        public string student_name { get; set; }
        public string department_id { get; set; }
        public string class_id { get; set; }

        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
        // selecting data from database
        public DataTable Select() 
        {
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                //writing sql query
                string sql = "SELECT * FROM Student";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return dt;
        }
        //insert data into database
        public bool Insert(studentclass c)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
               
                //   Sql query to insert data
                string sql = "INSERT INTO Student(student_id,student_name,department_id,class_id) VALUES (@student_id,@student_name,@department_id,@class_id)";
                SqlCommand cmd = new SqlCommand(sql,conn);
                cmd.Parameters.AddWithValue("@student_id", c.student_id);
                cmd.Parameters.AddWithValue("@student_name", c.student_name);
                cmd.Parameters.AddWithValue("@department_id", c.department_id);
                cmd.Parameters.AddWithValue("@class_id", c.class_id);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {

            }
            finally { conn.Close(); }
            return isSuccess;
        }
        public bool Update(studentclass c)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //sql to update data
                string sql = "UPDATE Student SET student_name=@student_name,department_id=@department_id,class_id=@class_id WHERE student_id=@student_id";
                SqlCommand cmd = new SqlCommand(sql,conn);
             
               cmd.Parameters.AddWithValue("@student_name", c.student_name);
               cmd.Parameters.AddWithValue("@department_id", c.department_id);
                cmd.Parameters.AddWithValue("@class_id", c.class_id);
                cmd.Parameters.AddWithValue("@student_id", c.student_id);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else {
                    isSuccess = false; }
            }
            catch (Exception ex)
            {

            }
            finally { conn.Close(); }
            return isSuccess;
        }
        public bool Delete(studentclass c)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                string sql = "DELETE FROM Student WHERE student_id=@student_id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@student_id", c.student_id);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {

            }
            finally { conn.Close(); }
            return isSuccess;
        }

       
    }
    
}
