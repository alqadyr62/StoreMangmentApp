using Bunifu.UI.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;


namespace MySore
{
    public partial class DataBase : Form
    {
        public DataBase()
        {
            InitializeComponent();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "DataBase Files (*.db)|*.db";
            openFileDialog1.FileName = "";
            openFileDialog1.ShowDialog();
            textBox1.Text = openFileDialog1.FileName;
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = @"Data Source = " + textBox1.Text.Trim() + ";Version=3;";
                MyCon con = new MyCon();
                con.setDataBasePath(connectionString, "MyStore");
            }
            catch
            {

            }
           
        }

        private void DataBase_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1.x1 = 0;
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
