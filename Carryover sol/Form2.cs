using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.SqlServer.Server;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace Carryover_sol
{
    public partial class Form2 : Form
    {
       
        public Form2()
        {

            InitializeComponent();
           
        }
        public string TextBox1Text
        {
            get { return textBox1.Text; }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string student_id = textBox1.Text;
            Form2_logindisplay form2_Logindisplay=new Form2_logindisplay(student_id);
            form2_Logindisplay.ShowDialog();
            
        }


        public void textuser_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
