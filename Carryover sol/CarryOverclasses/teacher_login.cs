using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Carryover_sol.CarryOverclasses
{
    internal class teacher_login
    {
        public string teacher_id { get; set; }
        public string score { get; set; }
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
        public DataTable Select(string teacher_id)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                //writing sql query
                string sql = " SELECT Teacher.teacher_id, Student.student_name, Teacher_marks.score FROM Student INNER JOIN Teacher ON Student.student_id = Teacher.student_id INNER JOIN Teacher_marks ON Student.student_name = Teacher_marks.student_name WHERE Teacher.teacher_id = @teacher_id;" ;
                   SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@teacher_id", teacher_id);
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

        public bool Update(teacher_login c)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //sql to update data
                string sql = "update Teacher_marks set teacher_id=@teacher_id,student_name=@student_name,score=@score where teacher_id=@teacher_id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("score", c.score);
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
