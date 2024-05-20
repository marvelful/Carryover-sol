using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Carryover_sol.CarryOverclasses;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
namespace Carryover_sol
{
    public partial class Form2_logindisplay : Form
    {
        private string student_id;
        private System.Windows.Forms.Button button3;
        student_login c = new student_login();
        public Form2_logindisplay(string student_id)
        {
            InitializeComponent();
            
            this.student_id = student_id;
            Initializebutton3();
        }
        private void Initializebutton3()
        {
            // Create the second button
            button3 = new System.Windows.Forms.Button();
            button3.Text = "Upload";
            button3.Location = new System.Drawing.Point(100, 200); // Set the location of the button
            button3.Visible = false; // Initially set visibility to false
            this.Controls.Add(button3); // Add the button to the form's controls
            button3.Click += button3_Click;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            // Create an instance of OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Set the filter to allow only image files
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png, *.gif, *.bmp)|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All files (*.*)|*.*";

            // Show the dialog and check if the user clicked OK
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Load the selected image file into the PictureBox control
                    pictureBox1.Image = Image.FromFile(openFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    // Handle any errors that may occur while loading the image
                    MessageBox.Show("Error loading image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void Form2_logindisplay_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'cARRYOVERDataSet.Carryover' table. You can move, or remove it, as needed.
            this.carryoverTableAdapter.Fill(this.cARRYOVERDataSet.Carryover);
            Form2 form2 = new Form2();
            string text = form2.TextBox1Text;
            DataTable dt = c.Select(this.student_id);
            dataGridView1.DataSource = dt;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            string text = form2.TextBox1Text;
            DataTable dt = c.Display(this.student_id);
            dataGridView1.DataSource = dt;
            button3.Visible = true;
        }

        DataTable previousData = new DataTable();
        
        private void button2_Click(object sender, EventArgs e)
        {
            DataTable dt = c.Select(this.student_id);
            dataGridView1.DataSource = dt;
            
        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (pictureBox1.Image != null)
            {
                // Convert the image to a byte array
                byte[] imageData;
                using (MemoryStream ms = new MemoryStream())
                {
                    pictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    imageData = ms.ToArray();
                }

                // Call the UpdateReceipt method from student_login class
                c.UpdateReceipt(this.student_id, imageData);

                MessageBox.Show("Image uploaded successfully.");
            }
            else
            {
                MessageBox.Show("Please load an image first.");
            }
           
        }
    }
}
