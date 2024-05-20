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
    internal class teacherclass
    {
        public string teacher_id { get; set; }
        public string teacher_name { get; set; }
        public string email { get; set; }
        public string course_id { get; set; }
        public string student_id { get; set; }

        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
        // selecting data from database
        public DataTable Select()
        {
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                //writing sql query
                string sql = "SELECT * FROM Teacher";
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
        public bool Insert(teacherclass c)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {

                //   Sql query to insert data
                string sql = "INSERT INTO Teacher(teacher_id,teacher_name,email,course_id,student_id) VALUES (@teacher_id,@teacher_name,@email,@course_id,@student_id)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@teacher_id", c.teacher_id);
                cmd.Parameters.AddWithValue("@teacher_name", c.teacher_name);
                cmd.Parameters.AddWithValue("email", c.email);
                cmd.Parameters.AddWithValue("@course_id", c.course_id);
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
        public bool Update(teacherclass c)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //sql to update data
                string sql = "UPDATE Teacher SET teacher_name=@teacher_name,email=@email,course_id=@course_id,student_id=@student_id WHERE teacher_id=@teacher_id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@teacher_name",c.teacher_name);
                cmd.Parameters.AddWithValue("email",c.email);
                cmd.Parameters.AddWithValue("@course_id", c.course_id);
                cmd.Parameters.AddWithValue("@student_id", c.student_id);
                cmd.Parameters.AddWithValue("@teacher_id", c.teacher_id);
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
        public bool Delete(teacherclass c)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                string sql = "DELETE FROM Teacher WHERE teacher_id=@teacher_id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@teacher_id", c.teacher_id);
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
