using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Carryover_sol.CarryOverclasses;
using ExcelDataReader;
using Z.Dapper.Plus;

namespace Carryover_sol
{
    public partial class Form3_logindisplay : Form
    {
        private string teacher_id;

        teacher_login c = new teacher_login();
        public Form3_logindisplay(string teacher_id)
        {
            InitializeComponent();
            this.teacher_id = teacher_id;
        }
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        private void Form3_logindisplay_Load(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            string text = form3.TextBox1Text;
            DataTable dt = c.Select(this.teacher_id);
            dataGridView1.DataSource = dt;

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        DataTableCollection tableCollection;
        private void button3_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog OpenFileDialog = new OpenFileDialog() { Filter = "Excel Files|*.xls;*.xlsx" })

            {
                if (OpenFileDialog.ShowDialog() == DialogResult.OK)
                {
                    textBox2.Text = OpenFileDialog.FileName;
                    using (var stream = File.Open(OpenFileDialog.FileName, FileMode.Open, FileAccess.Read))
                    {
                        using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                            {
                                ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
                            });
                            tableCollection = result.Tables;
                            comboBox1.Items.Clear();
                            foreach (DataTable table in tableCollection)
                                comboBox1.Items.Add(table.TableName);
                            

                        }
                    }
                }
                
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            string text = form3.TextBox1Text;
            DataTable dr = c.Select(this.teacher_id);
           
            // Ensure a sheet is selected in the ComboBox
            if (comboBox1.SelectedItem != null)
                {
                    // Get the selected sheet name
                    string selectedSheet = comboBox1.SelectedItem.ToString();

                    // Get the DataTable corresponding to the selected sheet
                    DataTable dt = tableCollection[selectedSheet];

                    // Connect to the database
                    using (SqlConnection conn = new SqlConnection(myconnstrng))
                    {
                        conn.Open();

                        // Iterate through the rows of the DataTable
                        foreach (DataRow row in dt.Rows)
                        {
                            
                            // Adjust column name as per your Excel file
                            string studentName = row["student_name"].ToString();
                            int score = Convert.ToInt32(row["score"]); // Assuming score is integer, adjust as needed

                            // Insert into the database
                            string sql = "INSERT INTO Teacher_marks (student_name, score) VALUES (@student_name, @score)";
                            SqlCommand cmd = new SqlCommand(sql, conn);
                     //   cmd.Parameters.AddWithValue("@teacher_id", teacher_id);
                        cmd.Parameters.AddWithValue("@student_name", studentName);
                            cmd.Parameters.AddWithValue("@score", score);
                            cmd.ExecuteNonQuery(); // Execute the insert command
                        }
                    }

                    MessageBox.Show("Data inserted successfully.");
                }
                else
                {
                    MessageBox.Show("Please select a sheet first.");
                }
            }

        

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
         
                DataTable dt = tableCollection[comboBox1.SelectedItem.ToString()];
                dataGridView1.DataSource = dt;
            
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //get the data from data grid view and load it into the textbox
            //identify the row on which the mouse is clicked
            int rowIndex = e.RowIndex;
            textBox1.Text = dataGridView1.Rows[rowIndex].Cells[1].Value.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //get data from textbox
            c.score =textBox1.Text;
            bool success = c.Update(c);
            if(success==true) 
            {
                MessageBox.Show("Mark has been successfully modified");
            }
            else
            {
                MessageBox.Show("Failed to modify mark. Try again");
            }
        }
    }
}

