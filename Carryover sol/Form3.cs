using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Carryover_sol.CarryOverclasses;

namespace Carryover_sol
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        public string TextBox1Text
        {
            get { return textBox1.Text; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string teacher_id = textBox1.Text;
            Form3_logindisplay form3_Logindisplay = new Form3_logindisplay(teacher_id);
            form3_Logindisplay.ShowDialog();
            
               
            

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
