using Bunifu.UI.WinForms;
using myClinic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySore
{
    public partial class addNewItem : Form
    {
        public addNewItem()
        {
            InitializeComponent();
        }

        private string UID = "";
        private string unitQuantity = "";
        private string price = "";


        private void addNewItem_Load(object sender, EventArgs e)
        {
            textBox1.Text = DateTime.Now.ToString("yyyy/MM/dd");
       
            
                string selectString = "select DISTINCT trim(Uname) from Units where UaddingBy ='" + Master.uName + "'";
                sqliteHelper.select(selectString, bunifuDropdown1);
            
            

        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل تريد الحفظ بالتأكيد ؟", "مدير المستودع", MessageBoxButtons.YesNo) ==
              System.Windows.Forms.DialogResult.Yes)
            {
                try
                {

                    float payment = float.Parse(textBox3.Text);
                    float cost = float.Parse(textBox4.Text);
                    float addingCost = float.Parse(textBox5.Text);
                    DateTime dateObject;
                    if (DateTime.TryParse(textBox1.Text, out dateObject) && payment > 0)
                    {
                        if (textBox2.Text !="" || textBox4.Text !="" || textBox5.Text !=""  || textBox3.Text !="")
                        {
                            string insertString = "insert into Items (IID,UID , IName , IUnit ,INumberInUnits,ICost,IAddingCost,IPrice,IQuantity,IAddingDate,IAddingTime,INots,IAddingBy) values ((select coalesce(max(IID),0)+1 from Items),'"
                             + UID + "','" + textBox2.Text.Trim() + "','" + bunifuDropdown1.Text + "','" + unitQuantity + "','"+ cost + "','"+ addingCost + "','" + payment + "','0','" + textBox1.Text + "','"+ DateTime.Now.ToString("hh:mm tt") + "','" + richTextBox1.Text.Trim() + "','" + Master.uName + "')";
                            sqliteHelper.insert(insertString, 1);
                            string InsString = "insert into inspection (inID,inText,inAddingDate,inAddingTime,inAddingBy) values " +
                                     "((select coalesce(max(inID),0)+1 from inspection),'" + " قام " + Master.uName + " " + " بإضافة مادة جديدة في المستودع ','" + DateTime.Today.ToString("yyyy/MM/dd") + "','" + DateTime.Now.ToString("hh:mm tt") + "','النظام')";
                            sqliteHelper.insert(InsString, 0);

                            Items.x3 = 1;
                            Items.x4 = 1;

                           // this.Close();
                        }
                        else
                        {
                            MessageBox.Show("يجب تعبأة جميع الخانات المعلمة بالنجمة");
                        }
                      
                    }

                }
                catch 
                {
                    MessageBox.Show("ادخل التاريخ بالشكل الصيحيح أو السعر المدخل يجب ان يكون ارقام فقط");
                    textBox1.Text = DateTime.Today.ToString("yyyy/MM/dd");

                }
            }
        }

        private void bunifuDropdown1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(Master.uName == "Home")
            {
                string selectString = "select UID from Units where Uname ='" + bunifuDropdown1.Text + "'";
                UID = sqliteHelper.selectWithReturn(selectString);
                selectString = "select UQuantity from Units where Uname ='" + bunifuDropdown1.Text + "'";
                unitQuantity = sqliteHelper.selectWithReturn(selectString);

            }
            else
            {
                string selectString = "select UID from Units where Uname ='" + bunifuDropdown1.Text + "' and UaddingBy ='" + Master.uName + "'";
                UID = sqliteHelper.selectWithReturn(selectString);
                selectString = "select UQuantity from Units where Uname ='" + bunifuDropdown1.Text + "' and UaddingBy ='" + Master.uName + "'";
                unitQuantity = sqliteHelper.selectWithReturn(selectString);

            }

        }

        private void addNewItem_FormClosing(object sender, FormClosingEventArgs e)
        {
            Items.x1 = 0;
        }

      

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox5.Text == "")
                {
                    textBox5.Text = "0";
                    textBox3.Text = (Double.Parse(textBox5.Text) + Double.Parse(textBox4.Text)).ToString();

                }
                else if (textBox4.Text == "")
                {
                    textBox4.Text = "0";
                    textBox3.Text = (Double.Parse(textBox5.Text) + Double.Parse(textBox4.Text)).ToString();

                }
                else
                {
                    textBox3.Text = (Double.Parse(textBox5.Text) + Double.Parse(textBox4.Text)).ToString();
                }
            }
            catch (Exception ex) { }

        }
    }
}
