using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Carryover_sol.CarryOverclasses;

namespace Carryover_sol
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }
        teacherclass c =new teacherclass();

        private void button1_Click(object sender, EventArgs e)
        {

            //get value from input fields
            c.teacher_id = textBox1.Text;
            c.teacher_name = textBox2.Text;
            c.email = textBox3.Text;
            c.course_id = textBox4.Text;
            c.student_id = textBox6.Text;
            //insert value
            bool success = c.Insert(c);
            if (success == true)
            {
                MessageBox.Show("New teacher inserted successfully");
                Clear();
            }
            else
            {
                MessageBox.Show("failed to add teacher. Try again");
            }


            DataTable dt = c.Select();
            dataGridView1.DataSource = dt;
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            DataTable dt = c.Select();
            dataGridView1.DataSource = dt;
        }
        public void Clear()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox6.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            c.teacher_id = textBox1.Text;
            c.teacher_name = textBox2.Text;
            c.email= textBox3.Text;
            c.course_id = textBox4.Text;
            c.student_id= textBox6.Text;
            bool success = c.Update(c);
            if (success == true)
            {
                MessageBox.Show("teacher Updated successfully");
                DataTable dt = c.Select();
                dataGridView1.DataSource = dt;
                Clear();
            }
            else
            {
                MessageBox.Show("teacher failed to update.try again");
            }
        }
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            textBox1.Text = dataGridView1.Rows[rowIndex].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[rowIndex].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[rowIndex].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.Rows[rowIndex].Cells[3].Value.ToString();
            textBox6.Text = dataGridView1.Rows[rowIndex].Cells[4].Value.ToString();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            c.teacher_id = textBox1.Text;
            bool success = c.Delete(c);
            if (success == true)
            {
                MessageBox.Show("Deleted successfully");
                DataTable dt = c.Select();
                dataGridView1.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Failed to delete. try again");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Clear();
        }
        static string connectionString = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            string keyword = textBox5.Text;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Teacher WHERE teacher_name LIKE '%" + keyword + "%'", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        
    }
}
