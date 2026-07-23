using Bunifu.UI.WinForms;
using Guna.UI2.WinForms;
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
    public partial class input : Form
    {
        public input()
        {
            InitializeComponent();
        }

        private decimal total = 0;
        private string Bid = "";
        private string price = "";
        string users = "";

        private void input_Load(object sender, EventArgs e)
        {
            textBox1.Text = DateTime.Now.ToString("yyyy/MM/dd");
            textBox1.ReadOnly = false;
            textBox2.ReadOnly = true;
            textBox4.ReadOnly = true;
            textBox5.ReadOnly = true;
                users = "select Trim(IName) from Items where IAddingBy ='" + Master.uName + "'";
                sqliteHelper.select(users, this.comboBox1);
           
            

        }

        private void bunifuDropdown1_SelectedIndexChanged(object sender, EventArgs e)
        {
         

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            try   
            {
                total = decimal.Parse(price) * decimal.Parse(numericUpDown1.Value.ToString());

                textBox3.Text = string.Format("{0:n}", total);
            }
            catch
            {
                MessageBox.Show("السعر لا يجب ان يكون فارغ");
            }
            
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل تريد الحفظ بالتأكيد ؟", "مدير الصندوق", MessageBoxButtons.YesNo) ==
              System.Windows.Forms.DialogResult.Yes)
            {
                try
                {


                    DateTime dateObject;
                    if (DateTime.TryParse(textBox1.Text, out dateObject))
                    {
                        string updateString = "update Items set IQuantity = IQuantity + " + numericUpDown1.Value.ToString() + " where IID =" + Bid + " and IAddingBy ='" + Master.uName + "'";
                        sqliteHelper.upDate(updateString, 0);

                        // string profit = sqliteHelper.selectWithReturn("select IAddingCost from Items where IID ="+ Bid);
                        string profit = "0";

                        string insertString = "insert into Box (BID,BType , IID , BItem ,BPrice,BQuantity,BTotal,BProfite,BAddingDate,BAddingTime,BNote,BAddingBy) values ((select coalesce(max(BID),0)+1 from Box),'ادخال',"
                                     + Bid + ",'" + comboBox1.Text + "','" + price + "','" + numericUpDown1.Value.ToString().Trim() + "','" + total + "','"+ profit + "','" + textBox1.Text + "','"+ DateTime.Now.ToString("hh:mm tt") + "','" + richTextBox1.Text.Trim() + "','" + Master.uName + "')";
                        sqliteHelper.insert(insertString, 1);
                        string InsString = "insert into inspection (inID,inText,inAddingDate,inAddingTime,inAddingBy) values " +
                                 "((select coalesce(max(inID),0)+1 from inspection),'" + " قام " + Master.uName + " " + "يإدخال مادة الى المستودع ','" + DateTime.Today.ToString("yyyy/MM/dd") + "','" + DateTime.Now.ToString("hh:mm tt")+ "','النظام')";
                        sqliteHelper.insert(InsString, 0);
                     //   inputOutput.x3 = 1;
                     // inputOutput.x5 = 1;
                      //  this.Close();

                            
                    }
                }
                catch
                {
                    MessageBox.Show("ادخل تاريخ صحيح");
                    textBox1.Text = DateTime.Today.ToString("yyyy/MM/dd");

                }

            }
        }

        private void input_FormClosing(object sender, FormClosingEventArgs e)
        {
            inputOutput.x1 = 0;
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        //    price = textBox2.Text;
            textBox2.Text = string.Format("{0:n}", decimal.Parse(textBox2.Text));
        }

        private void bunifuDropdown2_SelectedIndexChanged(object sender, EventArgs e)
        {
           



        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string selectString = "";

                selectString = "select IPrice from Items where IName = '" + comboBox1.Text + "' and IAddingBy ='" + Master.uName + "'";
                sqliteHelper.select(selectString, textBox4);

                price = sqliteHelper.selectWithReturn(selectString);
                selectString = "select IID from Items where IName ='" + comboBox1.Text + "' and IAddingBy ='" + Master.uName + "'";
                Bid = sqliteHelper.selectWithReturn(selectString);

                selectString = "select IQuantity from Items where IName = '" + comboBox1.Text + "'and IAddingBy ='" + Master.uName + "'";
                textBox5.Text = sqliteHelper.selectWithReturn(selectString);

                selectString = "select IUnit from Items where IName = '" + comboBox1.Text + "'and IAddingBy = '" + Master.uName + "'";
                textBox2.Text = sqliteHelper.selectWithReturn(selectString);
            }
            catch
            {

            }
            
        }
    }
}
