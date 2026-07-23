using DevExpress.Utils;
using myClinic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace MySore
{
    public partial class Master : Form
    {
        public Master()
        {
            InitializeComponent();
        }


        public static int x1 = 0;
        public static int x2 = 0;
        public static int x3 = 0;
        public static int x4 = 0;
       
        public static int x6 = 0;
        public  bool key2 = false;

        public static string uName = "";

        string selectString = "";

        private void Master_Load(object sender, EventArgs e)
        {
            uName = this.MasterUsername.Text;

            selectString = "select R1 from permissions where UID =(select UID from Users_Login where Username ='"+MasterUsername.Text+"')";
            if (sqliteHelper.selectWithReturn(selectString) == "1")
            {
                bunifuButton1.Enabled = true;
            }
            else
            {
                bunifuButton1.Enabled = false;

            }

            selectString = "select R4 from permissions where UID =(select UID from Users_Login where Username ='" + MasterUsername.Text + "')";
            if (sqliteHelper.selectWithReturn(selectString) == "1")
            {
                bunifuButton2.Enabled = true;
            }
            else
            {
                bunifuButton2.Enabled = false;
            }
            selectString = "select R8 from permissions where UID =(select UID from Users_Login where Username ='" + MasterUsername.Text + "')";
            if (sqliteHelper.selectWithReturn(selectString) == "1")
            {
                bunifuButton3.Enabled = true;
            }
            else
            {
                bunifuButton3.Enabled = false;
            }
            selectString = "select R12 from permissions where UID =(select UID from Users_Login where Username ='" + MasterUsername.Text + "')";
            if (sqliteHelper.selectWithReturn(selectString) == "1")
            {
                toolStripButton1.Enabled = true;
            }
            else
            {
                toolStripButton1.Enabled = false;
            }
            selectString = "select R17 from permissions where UID =(select UID from Users_Login where Username ='" + MasterUsername.Text + "')";
            if (sqliteHelper.selectWithReturn(selectString) == "1")
            {
                toolStripButton3.Enabled = true;
            }
            else
            {
                toolStripButton3.Enabled = false;
            }

        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
    }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            if (x2 == 0)
            {
                x2 = 1;
                Items f2 = new Items();
                f2.Dock = DockStyle.Fill;
                f2.MdiParent = this;
                xtraTabbedMdiManager1.Pages[xtraTabbedMdiManager1.Pages.Count - 1].Text = "المستودع";
                xtraTabbedMdiManager1.Pages[xtraTabbedMdiManager1.Pages.Count - 1].ShowCloseButton = DefaultBoolean.False;

                f2.Show();
                //f3.username = uName;

                xtraTabbedMdiManager1.Pages[xtraTabbedMdiManager1.Pages.Count - 1].Appearance.Header.Font = new Font("Times New Roman", 14, FontStyle.Bold);
                xtraTabbedMdiManager1.Pages[xtraTabbedMdiManager1.Pages.Count - 1].Appearance.HeaderActive.Font = new Font("Times New Roman", 14, FontStyle.Bold);
            }
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            if (x3 == 0)
            {
                x3 = 1;
                Units f3 = new Units();
                f3.Dock = DockStyle.Fill;
                f3.MdiParent = this;
                xtraTabbedMdiManager1.Pages[xtraTabbedMdiManager1.Pages.Count - 1].Text = "الوحدات";
                xtraTabbedMdiManager1.Pages[xtraTabbedMdiManager1.Pages.Count - 1].ShowCloseButton =  DefaultBoolean.False;

                f3.Show();
                //f3.username = uName;

                xtraTabbedMdiManager1.Pages[xtraTabbedMdiManager1.Pages.Count - 1].Appearance.Header.Font = new Font("Times New Roman", 14, FontStyle.Bold);
                xtraTabbedMdiManager1.Pages[xtraTabbedMdiManager1.Pages.Count - 1].Appearance.HeaderActive.Font = new Font("Times New Roman", 14, FontStyle.Bold);
            }
        }

        private void Master_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (key2 == true)
            {
                this.key2 = false;
                Form1.key2 = false;

                Application.Exit();
            }
        }

        private void bunifuButton1_Click_1(object sender, EventArgs e)
        {

            if (x1 == 0)
            {
                x1 = 1;
                inputOutput f1 = new inputOutput();
                f1.Dock = DockStyle.Fill;
                f1.MdiParent = this;
                xtraTabbedMdiManager1.Pages[xtraTabbedMdiManager1.Pages.Count - 1].Text = "الصندوق";
                xtraTabbedMdiManager1.Pages[xtraTabbedMdiManager1.Pages.Count - 1].ShowCloseButton = DefaultBoolean.False;

                f1.Show();
                //f3.username = uName;

                xtraTabbedMdiManager1.Pages[xtraTabbedMdiManager1.Pages.Count - 1].Appearance.Header.Font = new Font("Times New Roman", 14, FontStyle.Bold);
                xtraTabbedMdiManager1.Pages[xtraTabbedMdiManager1.Pages.Count - 1].Appearance.HeaderActive.Font = new Font("Times New Roman", 14, FontStyle.Bold);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if(x4 == 0)
            {
                Users f4 = new Users();
                f4.Show();
            }
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل تريد الرجوع الى واجهة الدخول بالتأكيد ؟", "لوحة التحكم", MessageBoxButtons.YesNo) ==
             System.Windows.Forms.DialogResult.Yes)
            {
                Form1 f1 = new Form1();
                f1.key1 = true;
                this.key2 = false;
                /*x1 = 0;
                x2 = 0;
                inputOutput.x3 = 0; 
                x3 = 0;
                x4 = 0;
                
                x6 = 0;*/
                this.Close();
                f1.Show();
            }
            }

            private void bunifuButton5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل تريد الخروج بالتأكيد؟", "مدير الوحدات", MessageBoxButtons.YesNo) ==
              System.Windows.Forms.DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if(x6 == 0)
            {
                Inspection f1 = new Inspection();
                f1.Show();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            pictureBox2.Visible = true;
            panel2.Visible = false;
            panel2.Width = 222;
            gunaTransition1.ShowSync(panel2);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;
            pictureBox2.Visible = false;
            panel2.Visible = false;
            panel2.Width = 67;
            gunaTransition1.ShowSync(panel2);
        }
    }
}
