using myClinic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
//using static DevExpress.XtraEditors.Drawing.SplitContainerViewInfo;


namespace MySore
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public bool key1 = false;     //DESKTOP-FUGQNF4 DESKTOP-N4JQ6C6 DESKTOP-MD8VGAU
        public static bool key2 = false;

        public static int x1 = 0;

        public int x2 = 0;

        private void bunifuDropdown1_SelectedIndexChanged(object sender, EventArgs e)
        {
            }

        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel4.Text = DateTime.Today.ToString("yyyy/MM/dd");
            textBox1.Enabled = false;
            textBox1.UseSystemPasswordChar = true;


            try
            {
                string users = "select Trim(Username) from Users_Login";
                sqliteHelper.select(users,this.guna2ComboBox1);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox1.Checked == false)
            {
                textBox1.UseSystemPasswordChar = true;
            }
            else
            {
                textBox1.UseSystemPasswordChar = false;

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string selelctString = "select Username , Password from Users_Login where Username = '" + guna2ComboBox1.Text + "'" + "AND passWord ='" + textBox1.Text + "'";
            
            if (sqliteHelper.isFound(selelctString))
            {
                
                string InsString = "insert into inspection (inID,inText,inAddingDate,inAddingTime,inAddingBy) values " +
                        "((select coalesce(max(inID),0)+1 from inspection),'"+" قام "+guna2ComboBox1.Text+ " "+"بتسجيل الدخول للبرنامج ','" + DateTime.Today.ToString("yyyy/MM/dd")+ "','" + DateTime.Now.ToString("hh:mm tt") + "','النظام')";
                sqliteHelper.insert(InsString, 0);
                this.key1 = false;
                this.Hide();
                if (x2 == 0)
                {
                    x2 = 1;
                    Master f1 = new Master();
                    f1.MasterDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    f1.MasterUsername.Text = guna2ComboBox1.Text;
                    f1.key2 = true;
                    f1.Show();
                    
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(x1 == 0)
            {
                x1 = 1;
                DataBase f2 = new DataBase();
                f2.Show();
            }
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            textBox1.Focus();

        }
    }
}
