using Bunifu.UI.WinForms;
using myClinic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySore
{
    public partial class Users : Form
    {
        public Users()
        {
            InitializeComponent();
        }

        private Thread th1 = null;
        private Thread th2 = null;

        public static int x1 = 0;
        public static int x2 = 0;
        public static int x3 = 0;
        public static int x4 = 0;



        private string UID = "";
        private string Username = "";

        string selectString = "";

        int userEdit = 0;


        private void Users_Load(object sender, EventArgs e)
        {
           
            x1 = 1;
            th1 = new Thread(start);
            th1.Start();

            selectString = "select R13 from permissions where UID =(select UID from Users_Login where Username ='" + Master.uName + "')";
            if (sqliteHelper.selectWithReturn(selectString) == "1")
            {
                toolStripButton1.Enabled = true;
            }
            else
            {
                toolStripButton1.Enabled = false;
            }

            selectString = "select R14 from permissions where UID =(select UID from Users_Login where Username ='" + Master.uName + "')";
            if (sqliteHelper.selectWithReturn(selectString) == "1")
            {
                userEdit = 1;
            }
            else
            {
                userEdit = 0;

            }

            selectString = "select R15 from permissions where UID =(select UID from Users_Login where Username ='" + Master.uName + "')";
            if (sqliteHelper.selectWithReturn(selectString) == "1")
            {
                toolStripButton3.Enabled = true;
            }
            else
            {
                toolStripButton3.Enabled = false;


            }

            selectString = "select R16 from permissions where UID =(select UID from Users_Login where Username ='" + Master.uName + "')";
            if (sqliteHelper.selectWithReturn(selectString) == "1")
            {
                toolStripButton4.Enabled = true;
            }
            else
            {
                toolStripButton4.Enabled = false;


            }


        }

        private void start()
        {
            while (true)
            {
                th2 = new Thread(refresh);
                th2.Start();
            }
        }

        private void refresh()
        {
            if(x1 == 1)
            {
                x1 = 0;
                toolStripComboBox1.Items.Clear();
                toolStripComboBox1.Items.Add("الكل");
                string selectString = "select Username from Users_Login";
                sqliteHelper.select(selectString, toolStripComboBox1);
                sqliteHelper.EnableStyle(this.dataGridView1);
            }
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectString = "";

            if (toolStripComboBox1.Text == "الكل")
            {
                selectString = "select UID as 'رقم المستخدم', Username as 'اسم المستخدم',AddingDate as 'تاريخ الاضافة',AddingBy as 'بواسطة' from Users_Login";
                sqliteHelper.select(selectString, dataGridView1);
            }
            else
            {
                selectString = "select UID as 'رقم المستخدم', Username as 'اسم المستخدم',AddingDate as 'تاريخ الاضافة',AddingBy as 'بواسطة' from Users_Login where Username ='"+toolStripComboBox1.Text+"'";
                sqliteHelper.select(selectString, dataGridView1);
            }
            
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (x2 == 0)
            {
                AddNewUser f1 = new AddNewUser();
                f1.Show();
            }
        }

        private void Users_FormClosing(object sender, FormClosingEventArgs e)
        {
            Master.x4 = 0;
            th1.Abort();
            th2.Abort();

        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            UID = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            Username = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (UID != "" && Username != Master.uName)
            {
                if (MessageBox.Show("هل تريد  حذف هذا المستخدم بالتأكيد ؟", "مدير الوحدات", MessageBoxButtons.YesNo) ==
               System.Windows.Forms.DialogResult.Yes)
                {
                    string deleteString = "";
                    string resetString = "";

                    deleteString = "delete from Users_Login where UID ='" + UID + "'";
                    sqliteHelper.delete(deleteString, 1);

                    deleteString = "delete from permissions where UID ='" + UID + "'";
                    sqliteHelper.delete(deleteString, 0);

                    resetString = "DBCC CHECKIDENT ('Users_Login', reseed, (select max(UID) from Users_Login))";
                    sqliteHelper.resetPK(resetString, 0);

                    resetString = "DBCC CHECKIDENT ('permissions', reseed, (select max(PID) from permissions))";
                    sqliteHelper.resetPK(resetString, 0);

                    string InsString = "insert into inspection (inID,inText,inAddingDate,inAddingBy) values " +
                         "((select coalesce(max(inID),0)+1 from inspection),'" + " قام " + Master.uName + " " + "بحذف مستخدم ','" + DateTime.Today.ToString("yyyy-MM-dd") + "','النظام')";
                    sqliteHelper.insert(InsString, 0);

                    x1 = 1;

                }
                else
                {
                    MessageBox.Show("لم يتم تحديد يوزر لحذفه أو انك تحاول حذف اليوزر الذي يتم استخدامه حالياً");
                }
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (userEdit == 1)
            {
                if (x3 == 0)
                {
                    EditUser f1 = new EditUser();
                    f1.textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    string selectString = "select Password from Users_Login where UID =" + dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    f1.textBox2.Text = sqliteHelper.selectWithReturn(selectString);
                    f1.Uid = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    f1.Show();
                }
            }
            
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if(x4 == 0)
            {
                permissions f1 = new permissions();
                f1.UID = this.UID;
                f1.Show();
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل تريد  حذف هذه الوحدة بالتأكيد ؟", "مدير الوحدات", MessageBoxButtons.YesNo) ==
                   System.Windows.Forms.DialogResult.Yes)
            {
                string deleteString = "delete from Box where BAddingBy ='" + Username + "'";
                sqliteHelper.delete(deleteString, 1);

                string resetString = "DBCC CHECKIDENT ('Box', reseed, (select max(BID) from Box))";
                sqliteHelper.resetPK(resetString, 0);

                deleteString = "delete from Items where IAddingBy ='" + Username + "'";
                sqliteHelper.delete(deleteString, 0);


                resetString = "DBCC CHECKIDENT ('Items', reseed, (select max(IID) from Items))";
                sqliteHelper.resetPK(resetString, 0);

                deleteString = "delete from Units where UAddingBy ='" + Username + "'";
                sqliteHelper.delete(resetString, 0);

                resetString = "DBCC CHECKIDENT ('Units', reseed, (select max(UID) from Units))";
                sqliteHelper.resetPK(resetString, 0);

            }
        }
    }
}
