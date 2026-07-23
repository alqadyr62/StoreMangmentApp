using myClinic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySore
{
    public partial class Inspection : Form
    {
        public Inspection()
        {
            InitializeComponent();
        }

        private void Inspection_Load(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = DateTime.Today.ToString("yyyy/MM/dd");
            toolStripTextBox2.Text = DateTime.Today.ToString("yyyy/MM/dd");

            sqliteHelper.EnableStyle(this.dataGridView1);

            
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Inspection_FormClosing(object sender, FormClosingEventArgs e)
        {
            Master.x6 = 0;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            string selectString = "select inID as 'رقم العملية',inText as 'الحدث',inAddingDate as 'التاريخ',inAddingTime as 'الوقت' ,inAddingBy as 'بواسطة' from inspection where inAddingDate between '"+ toolStripTextBox1.Text+"' and '"+ toolStripTextBox2.Text + "'";
            sqliteHelper.select(selectString,dataGridView1);
        }
    }
}
