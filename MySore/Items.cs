using Bunifu.UI.WinForms;
//using Guna.UI2.WinForms.Suite;
using Microsoft.Office.Interop.Word;
using myClinic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.Design.Directives;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MySore
{
    public partial class Items : Form
    {
        public Items()
        {
            InitializeComponent();
        }


        public static int x2 = 0;
        public static int x1 = 0;
        public static int x3 = 0;
        public static int x4 = 0;

        static Thread th1 = null;
        static Thread th2 = null;

        private string id = "";

        string selectString = "";

        int editItem = 0;

        private int index = 0;


        private void Items_Load(object sender, EventArgs e)
        {
            sqliteHelper.EnableStyle2(this.dataGridView1);

            toolStripTextBox1.Text = DateTime.Today.ToString("yyyy/MM/dd");
            toolStripTextBox2.Text = DateTime.Today.ToString("yyyy/MM/dd");

         
            x3 = 1;
            th1 = new Thread(start);
            th1.Start();

            selectString = "select R5 from permissions where UID =(select UID from Users_Login where Username ='" + Master.uName + "')";
            if (sqliteHelper.selectWithReturn(selectString) == "1")
            {
                toolStripButton2.Enabled = true;
            }
            else
            {
                toolStripButton2.Enabled = false;

            }

            selectString = "select R6 from permissions where UID =(select UID from Users_Login where Username ='" + Master.uName + "')";
            if (sqliteHelper.selectWithReturn(selectString) == "1")
            {
                editItem = 1;
            }
            else
            {
                editItem = 0;

            }

            selectString = "select R7 from permissions where UID =(select UID from Users_Login where Username ='" + Master.uName + "')";
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

        private void  refresh()
        {
            if (x3 == 1)
            {
                x3 = 0;
                
                    toolStripComboBox1.Items.Clear();
                    toolStripComboBox1.Items.Add("الكل");
                    string selectString = "select IName from Items where IAddingBy ='" + Master.uName + "'";
                    sqliteHelper.select(selectString, toolStripComboBox1);

            }

            if(x4 == 1)
            {
                x4 = 0;

            

                    selectString = "select IID as 'رقم المادة' ,trim(IName) as 'اسم المادة',trim(IUnit) as 'وحدة  المادة',INumberInUnits as 'العدد بالوحدة',printf('%,d', ICost) as 'التكلفة' ,printf('%,d', IAddingCost) as 'السعر المضاف' ,printf('%,d', IPrice) as 'السعر',IQuantity as 'العدد',printf('%,d', IPrice)*IQuantity as 'اجمالي المادة'  ,trim(IAddingDate) as 'تاريخ الإضافة',trim(IAddingTime) as 'وقت الاضافة',IAddingBy  as 'بواسطة', trim(INots) as 'الملاحظات'  from Items  where (IAddingDate = '" + DateTime.Today.ToString("yyyy/MM/dd") + "') and IAddingBy ='" + Master.uName + "'";
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
              
                
                    string selectString = "select IID as 'رقم المادة' ,trim(IName) as 'اسم المادة',trim(IUnit) as 'وحدة  المادة',INumberInUnits as 'العدد بالوحدة',printf('%,d', ICost) as 'التكلفة' ,printf('%,d', IAddingCost) as 'السعر المضاف', printf('%,d', IPrice) as 'السعر',IQuantity as 'العدد',printf('%,d', IPrice)*IQuantity as 'اجمالي المادة'  ,trim(IAddingDate) as 'تاريخ الإضافة',trim(IAddingTime) as 'وقت الاضافة',IAddingBy  as 'بواسطة', trim(INots) as 'الملاحظات'  from Items  where (IAddingDate between '" + toolStripTextBox1.Text + "' and '" + toolStripTextBox2.Text + "') and IAddingBy ='"+Master.uName+"'";
                    sqliteHelper.select(selectString, this.dataGridView1);
                
                
            }
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(toolStripComboBox1.Text == "الكل")
            {
               
                    string selectString = "select IID as 'رقم المادة' ,trim(IName) as 'اسم المادة',trim(IUnit) as 'وحدة المادة',INumberInUnits as 'العدد بالوحدة',printf('%,d', ICost) as 'التكلفة' ,printf('%,d', IAddingCost) as 'السعر المضاف', printf('%,d',IPrice) as 'السعر', IQuantity as 'العدد',printf('%,d',IPrice*IQuantity) as 'اجمالي المادة'  ,trim(IAddingDate) as 'تاريخ الإضافة',IAddingTime as 'وقت الاضافة',IAddingBy as 'بواسطة',trim(INots) as 'الملاحظات'  from Items where IAddingBy ='" + Master.uName + "'";
                    sqliteHelper.select(selectString, this.dataGridView1);
                
            }
            else
            {
                    string selectString = "select IID as 'رقم المادة' ,trim(IName) as 'اسم المادة',trim(IUnit) as 'وحدة المادة',INumberInUnits as 'العدد بالوحدة',printf('%,d', ICost) as 'التكلفة' ,printf('%,d', IAddingCost) as 'السعر المضاف', printf('%,d',IPrice) as 'السعر', IQuantity as 'العدد',printf('%,d',IPrice*IQuantity) as 'اجمالي المادة',trim(IAddingDate) as 'تاريخ الإضافة' , IAddingTime as 'وقت الاضافة',IAddingBy as 'بواسطة',trim(INots) as 'الملاحظات'  from Items  where (IName = '" + toolStripComboBox1.Text + "') and (IAddingBy ='" + Master.uName + "')";
                    sqliteHelper.select(selectString, this.dataGridView1);
             
                
            }

            
        }

      

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if(x1 == 0)
            {
                x1 = 1;
                addNewItem f1 = new addNewItem();
                f1.Show();
            }
        }

        private void Items_FormClosing(object sender, FormClosingEventArgs e)
        {
            Master.x2 = 0;
            th1.Abort();
            th2.Abort();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل تريد الخروج بالتأكيد ؟", "مدير المستودع", MessageBoxButtons.YesNo) ==
              System.Windows.Forms.DialogResult.Yes)
            {
                this.Close();
                
            }
        }

       

        private void toolStripButton4_Click_1(object sender, EventArgs e)
        {
           
                if (id != "")
                {
                string quantity = "select IQuantity from Items where IID ='" + id +"'";
                if (int.Parse(sqliteHelper.selectWithReturn(quantity)) == 0)
                {
                    if (MessageBox.Show("هل تريد  حذف هذه الوحدة بالتأكيد ؟", "مدير الوحدات", MessageBoxButtons.YesNo) ==
                   System.Windows.Forms.DialogResult.Yes)
                    {
                        string deleteString = "";
                        string resetString = "";

                        deleteString = "delete from Items where IID ='" + id + "'";
                        sqliteHelper.delete(deleteString, 1);

                        resetString = "DBCC CHECKIDENT ('Items', reseed, (select max(IID) from Items))";
                        sqliteHelper.resetPK(resetString, 0);

                        string InsString = "insert into inspection (inID,inText,inAddingDate,inAddingTime,inAddingBy) values " +
                          "((select coalesce(max(inID),0)+1 from inspection),'" + "قام  " + Master.uName + " " + "بحذف مادة من المستودع  ','" + DateTime.Today.ToString("yyyy/MM/dd")+ "','"+DateTime.Now.ToString("hh:mm tt") + "','النظام')";
                        sqliteHelper.insert(InsString, 0);
                        x3 = 1;
                        x4 = 1;
                    }
                    

                }
                else
                {
                    MessageBox.Show("المادة لا يمكن حذفها ... يوجد رصيد لها في المستودع");
                }
                }
                else
                {
                MessageBox.Show("لا يوجد شيئ لحذفه");
                }
        }



        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                id = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
            catch 
            { 
            }
            
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (MessageBox.Show("هل تريد  الحفظ بالتأكيد ؟", "مدير  المستودع", MessageBoxButtons.YesNo) ==
                  System.Windows.Forms.DialogResult.Yes)
                {
                    string updateString = "update Items set ICost = " + double.Parse(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString()) + " ,IName ='"+ dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + "' ,IAddingCost =" + double.Parse(dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString())+ " ,IPrice = " + (double.Parse(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString()) +double.Parse(dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString())) + " where IID =" + id + " and IAddingBy ='" + Master.uName + "'";

                    sqliteHelper.upDate(updateString, 1);
                    string InsString = "insert into inspection (inID,inText,inAddingDate,inAddingBy) values " +
                              "((select coalesce(max(inID),0)+1 from inspection),'" + " قام " + Master.uName + " " + "بتعديل بيانات مادة في المستودع ','" + DateTime.Today.ToString("yyyy-MM-dd hh:mm") + "','النظام')";
                    sqliteHelper.insert(InsString, 0);
                    Items.x3 = 1;
                }
            }
            catch
            {

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

        private void toolStripTextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                toolStripButton1.PerformClick();

            }
        }


    }
}

