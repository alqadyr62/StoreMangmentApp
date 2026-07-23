using Bunifu.UI.WinForms;
using myClinic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySore
{
    public partial class Units : Form
    {
        public Units()
        {
            InitializeComponent();
        }

        public static int x1 = 0;
        public static int x2 = 0;

        private Thread th1 = null;
        private Thread th2 = null;

        public static int x3 = 0;
        public static int x4 = 0;


        private string unitId = "";

        string selectString = "";

        int editUnit = 0;

        private void Units_Load(object sender, EventArgs e)
        {

            sqliteHelper.EnableStyle2(this.dataGridView1);
            toolStripTextBox1.Text = DateTime.Today.ToString("yyyy/MM/dd");
            toolStripTextBox2.Text = DateTime.Today.ToString("yyyy/MM/dd");

            

            x3 = 1;
            th1 = new Thread(start);
            th1.Start();

            selectString = "select R9 from permissions where UID =(select UID from Users_Login where Username ='" + Master.uName + "')";
            if (sqliteHelper.selectWithReturn(selectString) == "1")
            {
                toolStripButton2.Enabled = true;
            }
            else
            {
                toolStripButton2.Enabled = false;


            }

            selectString = "select R10 from permissions where UID =(select UID from Users_Login where Username ='" + Master.uName + "')";
            if (sqliteHelper.selectWithReturn(selectString) == "1")
            {
                editUnit= 1;
            }
            else
            {
                editUnit = 0;



            }

            selectString = "select R11 from permissions where UID =(select UID from Users_Login where Username ='" + Master.uName + "')";
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
                th2 = new Thread(refreshUnit);
                th2.Start();
            }
        }

        private void refreshUnit()
        {
            if(x3 == 1)
            {
                x3 = 0;
               
                    toolStripComboBox1.Items.Clear();
                    toolStripComboBox1.Items.Add("الكل");
                    string selectString = "select DISTINCT trim(Uname) from Units where UaddingBy ='" + Master.uName + "'";
                    sqliteHelper.select(selectString, toolStripComboBox1);
                

            }

            if(x4 == 1)
            {
                       x4 = 0;
                       selectString = "select UID as 'رقم الوحدة' ,trim(Uname) as 'اسم الوحدة',trim(UQuantity) as 'الكمية في الوحدة',trim(UaddingDate) as 'تاريخ الإضافة',trim(UNotes) as 'الملاحظات',UaddingBy as 'بواسطة'  from Units  where (UaddingDate = '" + DateTime.Today.ToString("yyyy/MM/dd") + "' ) and UaddingBy ='" + Master.uName + "'";
                        sqliteHelper.select(selectString, this.dataGridView1);
                   
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            DateTime d;
            if (!DateTime.TryParseExact(toolStripTextBox1.Text, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out d) || !DateTime.TryParseExact(toolStripTextBox2.Text, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out d))
            {
                MessageBox.Show("yyyy/mm/dd الشكل المسموح به هو");
            }
            else if (DateTime.Parse(toolStripTextBox1.Text) > DateTime.Parse(toolStripTextBox2.Text))
            {
                MessageBox.Show("تاريخ بداية البحث اكبر من تاريخ نهاية البحث");

            }
            else
            {
                    string selectString = "select UID as 'رقم الوحدة' ,trim(Uname) as 'اسم الوحدة',trim(UQuantity) as 'الكمية في الوحدة',trim(UaddingDate) as 'تاريخ الإضافة',trim(UNotes) as 'الملاحظات',UaddingBy as 'بواسطة'  from Units  where (UaddingDate between '" + toolStripTextBox1.Text + "' and '" + toolStripTextBox2.Text + "') and UaddingBy ='" + Master.uName + "'";
                    sqliteHelper.select(selectString, this.dataGridView1);
                
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("هل تريد الخروج بالتأكيد ؟", "مدير الوحدات", MessageBoxButtons.YesNo) ==
              System.Windows.Forms.DialogResult.Yes)
            {
                this.Close();
            }
            }

            private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (x1 == 0)
            {
                x1 = 1;
                AddNewUnit f1 = new AddNewUnit();
                f1.Show();
            }
        }

        private void Units_FormClosing(object sender, FormClosingEventArgs e)
        {
            Master.x3 = 0;
            th1.Abort();
            th2.Abort();

        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(toolStripComboBox1.Text == "الكل")

            {
                
                    string selectString = "select UID as 'رقم الوحدة' ,trim(Uname) as 'اسم الوحدة',trim(UQuantity) as 'الكمية في الوحدة',trim(UaddingDate) as 'تاريخ الإضافة',UaddingTime as 'وقت الاضافة',UaddingBy as 'بواسطة',trim(UNotes) as 'الملاحظات'  from Units where UaddingBy ='" + Master.uName + "'";
                    sqliteHelper.select(selectString, this.dataGridView1);

            }
            else
            {
                    string selectString = "select UID as 'رقم الوحدة' ,trim(Uname) as 'اسم الوحدة',trim(UQuantity) as 'الكمية في الوحدة',trim(UaddingDate) as 'تاريخ الإضافة',UaddingTime as 'وقت الاضافة',UaddingBy as 'بواسطة',trim(UNotes) as 'الملاحظات'  from Units  where (Uname = '" + toolStripComboBox1.Text + "') and (UaddingBy ='" + Master.uName + "')";
                    sqliteHelper.select(selectString, this.dataGridView1);
            }
            
        }

       

     /*   private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (editUnit == 1)
                {
                    if (x2 == 0)
                    {
                        x2 = 1;
                        UnitEdit f1 = new UnitEdit();
                        f1.itemId = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                        f1.textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                        f1.textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                        f1.textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                        f1.richTextBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();

                        f1.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }*/

        private void تعديلToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {

            if (unitId != "")
            {
                if (MessageBox.Show("هل تريد  حذف هذه الوحدة بالتأكيد ؟", "مدير الوحدات", MessageBoxButtons.YesNo) ==
               System.Windows.Forms.DialogResult.Yes)
                {
                    string deleteString = "";
                    string resetString = "";

                    deleteString = "delete from Units where UID ='" + unitId + "'";
                    sqliteHelper.delete(deleteString, 1);

                    resetString = "DBCC CHECKIDENT ('Units', reseed, (select max(UID) from Units))";
                    sqliteHelper.resetPK(resetString, 0);
                    string InsString = "insert into inspection (inID,inText,inAddingDate,inAddingTime,inAddingBy) values " +
                          "((select coalesce(max(inID),0)+1 from inspection),'" + " قام " + Master.uName + " " + "بحذف وحدة ','" + DateTime.Today.ToString("yyyy/MM/dd")+ "','" + DateTime.Now.ToString("hh:mm tt") + "','النظام')";
                    sqliteHelper.insert(InsString, 0);
                    x3 = 1;
                    x4 = 1;
                }
                else
                {
                    MessageBox.Show("لا يوجد شيئ لحذفه");
                }
            }
        } 

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {

             try
             {
                unitId = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
             }
            catch
             {

             }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (MessageBox.Show("هل تريد  الحفظ بالتأكيد ؟", "مدير الوحدات", MessageBoxButtons.YesNo) ==
                  System.Windows.Forms.DialogResult.Yes)
                {
                    string updateString = "update Units set Uname = '" + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + "' where UID =" + unitId + " and UaddingBy ='" + Master.uName + "'";
                    sqliteHelper.upDate(updateString, 1);
                    string InsString = "insert into inspection (inID,inText,inAddingDate,inAddingBy) values " +
                              "((select coalesce(max(inID),0)+1 from inspection),'" + " قام " + Master.uName + " " + "بتعديل بيانات  وحدة ','" + DateTime.Today.ToString("yyyy-MM-dd hh:mm") + "','النظام')";
                    sqliteHelper.insert(InsString, 0);
                    Items.x3 = 1;
                }
            }
            catch
            {

            }
        }

        private void toolStripTextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                toolStripButton1.PerformClick();

            }
        }

        private void toolStripTextBox1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                toolStripButton1.PerformClick();

            }
        }
    }
}
