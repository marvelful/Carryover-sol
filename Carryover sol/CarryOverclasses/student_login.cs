using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carryover_sol.CarryOverclasses
{
    internal class student_login
    {
        public string student_id { get; set; }
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
        public DataTable Select(string student_id)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                //writing sql query
                string sql = "SELECT Student.student_name, Course.course_name, Course.credit, Marks.score\r\nFROM Student\r\nJOIN Marks  ON Student.student_id = Marks.student_id\r\nJOIN Course ON Marks.course_id = Course.course_id\r\nwhere Student.student_id = @student_id;";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@student_id", student_id);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    Console.WriteLine("No data found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }
        public DataTable Display(string student_id)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                //writing sql query
                string sql = "SELECT DISTINCT Student.student_name, Course.course_id, Price.amount_credit, Course.course_name, Marks.score, Price.amount_credit * Course.credit as total_price\r\nFROM Price, Student\r\nJOIN Carryover ON Student.student_id = Carryover.student_id\r\nJOIN Course ON Carryover.course_id = Course.course_id\r\nJOIN Marks ON Marks.student_id = Student.student_id \r\nWHERE Student.student_id = @student_id AND Marks.score < 10;\r\n";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@student_id", student_id);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    Console.WriteLine("No data found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }
        public void UpdateReceipt(string student_id, byte[] imageData)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);
            // SQL query to update the receipt column for the specified student
       
            try
                 {
                        // Open the connection
                        string sql = "UPDATE Carryover SET receipt = @ImageData WHERE student_id = @student_id";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@ImageData", imageData);
                        cmd.Parameters.AddWithValue("@student_id", student_id);
                        conn.Open();

                        // Execute the query
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        // Handle SQL exceptions
                        Console.WriteLine("SQL Error: " + ex.Message);
                    }
                    finally
                    {
                        // Close the connection
                        conn.Close();
                    }
                }
            
        }
}
