using Bunifu.UI.WinForms;
using Bunifu.UI.WinForms.BunifuButton;
using myClinic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MySore
{
    public partial class inputOutput : Form
    {
        public inputOutput()
        {
            InitializeComponent();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل تريد الخروج بالتأكيد ؟", "مدير الصندوق", MessageBoxButtons.YesNo) ==
             System.Windows.Forms.DialogResult.Yes)
            {
                this.Close();
            }
        }

        public static int x1 = 0;
        public static int x2 = 0;
        public static int x3 = 0;
        public static int x4 = 0;
        public static int x5 = 0;




        private Thread th3 ;
        private Thread th4 ;

      

        string selectString = "";


        private void inputOutput_FormClosing(object sender, FormClosingEventArgs e)
        {
            Master.x1 = 0;
          
            th3.Abort();
            th4.Abort();
        }

        private void inputOutput_Load(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = DateTime.Today.ToString("yyyy/MM/dd");
            toolStripTextBox2.Text = DateTime.Today.ToString("yyyy/MM/dd");

            sqliteHelper.EnableStyle(this.dataGridView1);
            sqliteHelper.EnableStyle(this.dataGridView2);

            toolStripComboBox2.Items.Add("الكل");
            toolStripComboBox2.Items.Add("ادخال");
            toolStripComboBox2.Items.Add("اخراج");


          
           
            toolStripComboBox1.Items.Add("الكل");
            string selectString = "select IName from Items where IAddingBy ='" + Master.uName + "'";
            sqliteHelper.select(selectString, toolStripComboBox1);
      



            // x4 = 1;
            th3 = new Thread(start1);
            th3.Start();


         

            selectString = "select R2 from permissions where UID =(select UID from Users_Login where Username ='" + Master.uName+ "')";
            if (sqliteHelper.selectWithReturn(selectString) == "1")
            {
                ادخالToolStripMenuItem.Enabled = true;
            }
            else
            {
                ادخالToolStripMenuItem.Enabled = false;

            }

            selectString = "select R2 from permissions where UID =(select UID from Users_Login where Username ='" + Master.uName + "')";
            if (sqliteHelper.selectWithReturn(selectString) == "1")
            {
                اخراجToolStripMenuItem.Enabled = true;
            }
            else
            {
                اخراجToolStripMenuItem.Enabled = false;

            }
        }

       

        private void start1()
        {
            while (true)
            {
                th4 = new Thread(find1);
                th4.Start();
            //    Thread.Sleep(10000);
            }
        }

       


     


        public void find1()
        {
            if (x4 == 1)
            {
                try
                {
                   // Thread.Sleep(5000);

                    x4 = 0;
                    string selectString = "";
                    if (toolStripComboBox1.Text == "الكل" && toolStripComboBox2.Text == "الكل")
                    {

                        selectString = "select coalesce(sum(BTotal),0)  from Box where " +
                                             "(BAddingDate between '" + toolStripTextBox1.Text + "' and '" + toolStripTextBox2.Text + "'" +
                                             ") and (BType ='ادخال') and (BAddingBy = '" + Master.uName + "')";
                        toolStripTextBox7.Text = string.Format("{0:n}", decimal.Parse(sqliteHelper.selectWithReturn(selectString)));

                        selectString = "select coalesce(sum(BTotal),0)  from Box where " +
                            "(BAddingDate between '" + toolStripTextBox1.Text + "' and '" + toolStripTextBox2.Text + "') " +
                            "and (BType ='اخراج') and (BAddingBy = '" + Master.uName + "')";
                        toolStripTextBox8.Text = string.Format("{0:n}", decimal.Parse(sqliteHelper.selectWithReturn(selectString)));


                              selectString = "select coalesce(sum(BProfite),0)  from Box where  (BType ='اخراج') and (BAddingBy = '" + Master.uName + "')";
                        toolStripTextBox4.Text = string.Format("{0:n}", decimal.Parse(sqliteHelper.selectWithReturn(selectString)));

                              /*    selectString = "select coalesce(sum(BTotal),0)  from Box where   (BType ='اخراج') and (BAddingBy = '" + Master.uName + "')";
                                  toolStripTextBox8.Text = string.Format("{0:n}", decimal.Parse(sqliteHelper.selectWithReturn(selectString)));*/
                    }
                    else if (toolStripComboBox1.Text == "الكل" && toolStripComboBox2.Text == "ادخال")
                    {
                        selectString = "select coalesce(sum(BTotal),0)  from Box where " +
                                           "(BAddingDate between '" + toolStripTextBox1.Text + "' and '" + toolStripTextBox2.Text + "'" +
                                           ") and (BType ='ادخال') and (BAddingBy = '" + Master.uName + "')";
                        toolStripTextBox7.Text = string.Format("{0:n}", decimal.Parse(sqliteHelper.selectWithReturn(selectString)));
                        toolStripTextBox8.Text = "0";
                        toolStripTextBox4.Text = "0";
                    }
                    else if (toolStripComboBox1.Text == "الكل" && toolStripComboBox2.Text == "اخراج")
                    {
                        selectString = "select coalesce(sum(BTotal),0)  from Box where " +
                                                  "(BAddingDate between '" + toolStripTextBox1.Text + "' and '" + toolStripTextBox2.Text + "' " +
                                                  ") and (BType ='اخراج') and (BAddingBy = '" + Master.uName + "')";
                        toolStripTextBox8.Text = string.Format("{0:n}", decimal.Parse(sqliteHelper.selectWithReturn(selectString)));
                        toolStripTextBox7.Text = "0";

                        selectString = "select coalesce(sum(BProfite),0)  from Box where  (BType ='اخراج') and (BAddingBy = '" + Master.uName + "')";
                        toolStripTextBox4.Text = string.Format("{0:n}", decimal.Parse(sqliteHelper.selectWithReturn(selectString)));


                    }
                    else if (toolStripComboBox1.Text != "الكل" && toolStripComboBox2.Text == "ادخال")
                    {

                        selectString = "select coalesce(sum(BTotal),0)  from Box where " +
                                               "((BAddingDate between '" + toolStripTextBox1.Text + "' and '" + toolStripTextBox2.Text + "') " +
                                               ") and (BItem ='" + toolStripComboBox1.Text + "') and (BType ='ادخال') and (BAddingBy = '" + Master.uName + "')";
                        toolStripTextBox7.Text = string.Format("{0:n}", decimal.Parse(sqliteHelper.selectWithReturn(selectString)));

                        /*selectString = "select coalesce(sum(BTotal),0)  from Box where " +
                            "((BAddingDate between '" + toolStripTextBox1.Text + "' and '" + toolStripTextBox2.Text + "') " +
                            " and (BItem ='" + toolStripComboBox1.Text + "') and (BType ='اخراج') and (BAddingBy = '" + Master.uName + "')";
                        toolStripTextBox8.Text = string.Format("{0:n}", decimal.Parse(sqliteHelper.selectWithReturn(selectString)));*/
                        toolStripTextBox8.Text = "0";

                        toolStripTextBox4.Text = "0";

                    }
                    else if(toolStripComboBox1.Text != "الكل" && toolStripComboBox2.Text == "اخراج")
                    {
                        /*   selectString = "select coalesce(sum(BTotal),0)  from Box where " +
                                                 "(BAddingDate between '" + toolStripTextBox1.Text + "' and '" + toolStripTextBox2.Text + "') " +
                                                 ") and (BItem ='" + toolStripComboBox1.Text + "')  and (BType ='ادخال') and (BAddingBy = '" + Master.uName + "')";
                           toolStripTextBox7.Text = string.Format("{0:n}", decimal.Parse(sqliteHelper.selectWithReturn(selectString)));*/
                        toolStripTextBox7.Text = "0";
                           selectString = "select coalesce(sum(BTotal),0)  from Box where " +
                            "(BAddingDate between '" + toolStripTextBox1.Text + "' and '" + toolStripTextBox2.Text + "') " +
                            "and (BItem ='" + toolStripComboBox1.Text + "') and (BType ='اخراج') and (BAddingBy = '" + Master.uName + "')";
                        toolStripTextBox8.Text = string.Format("{0:n}", decimal.Parse(sqliteHelper.selectWithReturn(selectString)));

                        selectString = "select coalesce(sum(BProfite),0)  from Box where  (BType ='اخراج') and (BItem ='" + toolStripComboBox1.Text + "') and (BAddingBy = '" + Master.uName + "')";
                        toolStripTextBox4.Text = string.Format("{0:n}", decimal.Parse(sqliteHelper.selectWithReturn(selectString)));

                    }
                    else if(toolStripComboBox2.Text == "الكل" && toolStripComboBox1.Text != "الكل")
                    {
                        selectString = "select coalesce(sum(BTotal),0)  from Box where " +
                                               "(BAddingDate between '" + toolStripTextBox1.Text + "' and '" + toolStripTextBox2.Text + "') " +
                                               " and (BItem ='" + toolStripComboBox1.Text + "')  and (BType ='ادخال') and (BAddingBy = '" + Master.uName + "')";
                        toolStripTextBox7.Text = string.Format("{0:n}", decimal.Parse(sqliteHelper.selectWithReturn(selectString)));

                        selectString = "select coalesce(sum(BTotal),0)  from Box where " +
                          "(BAddingDate between '" + toolStripTextBox1.Text + "' and '" + toolStripTextBox2.Text + "') " +
                          " and (BItem ='" + toolStripComboBox1.Text + "') and (BType ='اخراج') and (BAddingBy = '" + Master.uName + "')";
                      toolStripTextBox8.Text = string.Format("{0:n}", decimal.Parse(sqliteHelper.selectWithReturn(selectString)));

                        selectString = "select coalesce(sum(BProfite),0)  from Box where  (BType ='اخراج') and (BItem ='" + toolStripComboBox1.Text +"') and (BAddingBy = '" + Master.uName + "')";
                        toolStripTextBox4.Text = string.Format("{0:n}", decimal.Parse(sqliteHelper.selectWithReturn(selectString)));

                    }
                }
                catch { }
               // Thread.Sleep(3000);
            }

                
                
            
        }


    


        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            
        }

        private void ادخالToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (x1 == 0)
            {
                x1 = 1;
                input f1 = new input();
                f1.Show();
            }

        }

       

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           
            
        }

        private void اخراجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (x2 == 0)
            {
                x2 = 1;
                output f2 = new output();
                f2.Show();
            }
        }

    

        private void dataGridView1_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if(Master.uName=="Home")
                {
                    string selectString = "select IID as 'رقم المادة',IName as 'اسم المادة',printf('%,d',IPrice) as 'السعر',IQuantity as 'الكمية الموجودة',printf('%d',IPrice*IQuantity) as 'السعر الكلي', IAddingDate as 'تاريخ الاضافة',INots as 'الملاحظات' from Items where IID =  " + dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    sqliteHelper.select(selectString, dataGridView2);
                }
                else
                {
                    string selectString = "select IID as 'رقم المادة',IName as 'اسم المادة',printf('%,d',IPrice) as 'السعر',IQuantity as 'الكمية الموجودة',printf('%d',IPrice*IQuantity) as 'السعر الكلي', IAddingDate as 'تاريخ الاضافة',INots as 'الملاحظات' from Items where (IID =  " + dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString()+ ") and (IAddingBy ='"+Master.uName+"')";
                    sqliteHelper.select(selectString, dataGridView2);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل تريد بالتأكيد التصدير الى اكسل?", "مدير الصندوق", MessageBoxButtons.YesNo) ==
            System.Windows.Forms.DialogResult.Yes)
            {
                if (dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("لا يوجد شيئ لتصديره");
                }
                else
                {
                    try
                    {
                       
                        dataGridView1.MultiSelect = true;
                        dataGridView1.SelectAll();
                        DataObject copyData = dataGridView1.GetClipboardContent();
                        if (copyData != null) { Clipboard.SetDataObject(copyData); }
                        Microsoft.Office.Interop.Excel.Application exportSheet = new Microsoft.Office.Interop.Excel.Application();
                        exportSheet.Visible = true;
                      
                        Microsoft.Office.Interop.Excel.Workbook workbook;
                        Microsoft.Office.Interop.Excel.Worksheet worksheet;
                        Object obj = System.Reflection.Missing.Value;
                        workbook = exportSheet.Workbooks.Add(obj);
                        worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets.get_Item(1);
                        Microsoft.Office.Interop.Excel.Range xlr = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[1, 1];
                        
                        xlr.Select();
                       
                        worksheet.PasteSpecial(xlr);
                     
                        MessageBox.Show("انتهى التصدير");
                     

                        string InsString = "insert into inspection (inID,inText,inAddingDate,inAddingBy) values " +
                          "((select coalesce(max(inID),0)+1 from inspection),'" + " قام " + Master.uName + " " + "بالتصدير إلى اكسل ','" + DateTime.Today.ToString("yyyy-MM-dd") + "','النظام')";
                        sqliteHelper.insert(InsString, 0);

                       

                    }
                    catch
                    {
                        MessageBox.Show("لا يوجد نسخة اوفيس مفعلة على هذا الحاسب");
                    }
                }
            }
        }

       

        private void toolStripButton4_Click_1(object sender, EventArgs e)
        {
            System.Diagnostics.Process p = System.Diagnostics.Process.Start("calc.exe");
            p.WaitForInputIdle();
           
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            string selectString = "select coalesce(sum(IPrice*IQuantity),0) from Items where IAddingBy ='" + Master.uName + "'";
            toolStripTextBox3.Text = string.Format("{0:n}", decimal.Parse(sqliteHelper.selectWithReturn(selectString)));

        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {   
            
            toolStripComboBox1.Items.Clear();
            toolStripComboBox1.Items.Add("الكل");
            string selectString = "select  IName from items";
            sqliteHelper.select(selectString, toolStripComboBox1);


        }

        private void toolStripTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                toolStripButton5.PerformClick();

            }
        }

        private void toolStripTextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                toolStripButton5.PerformClick();

            }
        }

        private void toolStripComboBox2_SelectedIndexChanged(object sender, EventArgs e)
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

                if (toolStripComboBox1.Text == "الكل" && toolStripComboBox2.Text == "الكل")
                {


                    string selectString = "select BID as 'رقم العملية' ,trim(BType) as 'نوع العملية',IID as 'رقم المادة',trim(BItem) as 'اسم المادة',printf('%,d',BPrice) as 'السعر للوحدة',BQuantity as 'العدد',printf('%,d',BTotal) as 'السعر الكلي',printf('%,d',BProfite) as 'الربح',trim(BAddingDate) as 'تاريخ الإضافة',BAddingTime as 'وقت الاضافة',BAddingBy as 'بواسطة'   from Box  where (BAddingDate between '" + toolStripTextBox1.Text + "' and '" + toolStripTextBox2.Text + "')  and (BAddingBy ='" + Master.uName + "')";
                    sqliteHelper.select(selectString, this.dataGridView1);
                    x4 = 1;

                }
                else if (toolStripComboBox2.Text == "الكل" && toolStripComboBox1.Text != "الكل")
                {

                    string selectString = "select BID as 'رقم العملية' ,trim(BType) as 'نوع العملية',IID as 'رقم المنتج',trim(BItem) as 'اسم المادة',printf('%,d',BPrice) as 'السعر للوحدة',BQuantity as 'العدد',printf('%,d',BTotal) as 'السعر الكلي',printf('%,d',BProfite) as 'الربح',trim(BAddingDate) as 'تاريخ الإضافة',BAddingTime as 'وقت الاضافة' ,BAddingBy as 'بواسطة'   from Box  where (BAddingDate between '" + toolStripTextBox1.Text + "' and '" + toolStripTextBox2.Text + "')  and (BItem ='" + toolStripComboBox1.Text + "') and (BAddingBy ='" + Master.uName + "')";
                    sqliteHelper.select(selectString, this.dataGridView1);
                    x4 = 1;


                }
                else if (toolStripComboBox1.Text == "الكل" && toolStripComboBox2.Text != "الكل")
                {

                    string selectString = "select BID as 'رقم العملية' ,trim(BType) as 'نوع العملية',IID as 'رقم المنتج',trim(BItem) as 'اسم المادة',printf('%,d',BPrice) as 'السعر للوحدة',BQuantity as 'العدد',printf('%,d',BTotal) as 'السعر الكلي',printf('%,d',BProfite) as 'الربح',trim(BAddingDate) as 'تاريخ الإضافة',BAddingTime as 'وقت الاضافة' ,BAddingBy as 'بواسطة'   from Box  where (BAddingDate between '" + toolStripTextBox1.Text + "' and '" + toolStripTextBox2.Text + "') and (BType ='" + toolStripComboBox2.Text + "')  and (BAddingBy ='" + Master.uName + "')";
                    sqliteHelper.select(selectString, this.dataGridView1);

                    x4 = 1;
                }
                else if (toolStripComboBox1.Text != "الكل" && toolStripComboBox2.Text != "الكل")
                {

                    string selectString = "select BID as 'رقم العملية' ,trim(BType) as 'نوع العملية',IID as 'رقم المنتج',trim(BItem) as 'اسم المادة',printf('%,d',BPrice) as 'السعر للوحدة',BQuantity as 'العدد',printf('%,d',BTotal) as 'السعر الكلي',printf('%,d',BProfite) as 'الربح',trim(BAddingDate) as 'تاريخ الإضافة',BAddingTime as 'وقت الاضافة',BAddingBy as 'بواسطة'   from Box  where (BAddingDate between '" + toolStripTextBox1.Text + "' and '" + toolStripTextBox2.Text + "') and (BType ='" + toolStripComboBox2.Text + "') and (BItem ='" + toolStripComboBox1.Text + "') and (BAddingBy ='" + Master.uName + "')";
                    sqliteHelper.select(selectString, this.dataGridView1);
                    x4 = 1;

                }


            }
        }

      
    }
}
