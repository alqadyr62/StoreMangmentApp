using Bunifu.UI.WinForms;
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
//using static DevExpress.Data.Utils.AsyncDownloader<TValue>.LifeTime;

namespace MySore
{
    public partial class output : Form
    {
        public output()
        {
            InitializeComponent();
        }

        string Bid = "";
        decimal total = 0;
        string price = "";
        string users = "";

        private void output_Load(object sender, EventArgs e)
        {
          
                textBox1.Text = DateTime.Now.ToString("yyyy/MM/dd");


                users = "select Trim(IName) from Items where (IAddingBy ='" + Master.uName + "') ";
                sqliteHelper.select(users, this.comboBox1);

        }

        private void bunifuDropdown1_SelectedIndexChanged(object sender, EventArgs e)
        {
          
                string selectString = "";
                selectString = "select IID from Items where (IName ='" + comboBox1.Text + "') and (IAddingBy ='"+Master.uName+"')";
                Bid = sqliteHelper.selectWithReturn(selectString);

             //   selectString = "select IQuantity from Items where IID =" + Bid + " and IAddingBy ='"+Master.uName+"'";

                    selectString = "select IPrice from Items where IName = '" + comboBox1.Text + "' and IAddingBy ='"+Master.uName+"'";

                    

                    textBox2.Text = string.Format("{0:n}", decimal.Parse(sqliteHelper.selectWithReturn(selectString)));

                    selectString = "select IQuantity from Items where IID = '" + Bid + "' and IAddingBy ='" + Master.uName + "'";
                     textBox4.Text = sqliteHelper.selectWithReturn(selectString);

                    selectString = "select IUnit from Items where IName = '" + comboBox1.Text + "'and IAddingBy = '" + Master.uName + "'";
                     textBox5.Text = sqliteHelper.selectWithReturn(selectString);


          
            
           
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
                MessageBox.Show("السعر يجب ان لا يكون فارغ");
            }
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            

            string selectString = "select iif(IQuantity -"+numericUpDown1.Value.ToString()+" < 0,'1','0') from Items where IID ="+Bid + " and IAddingBy ='"+Master.uName+"'";
            if (sqliteHelper.selectWithReturn(selectString) == "1")
            {
                MessageBox.Show("لا يوجد رصيد لهذه المادة");
            }
            else
            {
                if (MessageBox.Show("هل تريد الحفظ بالتأكيد ؟", "مدير الصندوق", MessageBoxButtons.YesNo) ==
                System.Windows.Forms.DialogResult.Yes)
                {
                    try
                    {


                        DateTime dateObject;
                        if (DateTime.TryParse(textBox1.Text, out dateObject))
                        {
                             string updateString = "update Items set IQuantity = IQuantity - " + numericUpDown1.Value.ToString() + " where IID =" + Bid + " and IAddingBy ='"+Master.uName+"'";
                             sqliteHelper.upDate(updateString, 0);


                             selectString = "select IAddingCost from Items where IID =" + Bid;
                            string addingCost = sqliteHelper.selectWithReturn(selectString);

                             string insertString = "insert into Box (BID,BType , IID , BItem ,BPrice,BQuantity,BTotal,BAddingDate,BProfite,BAddingTime,BNote,BAddingBy) values ((select coalesce(max(BID),0)+1 from Box),'اخراج'," + Bid + ",'" + comboBox1.Text + "'," + price + "," + numericUpDown1.Value.ToString().Trim() + "," + total + ",'" + textBox1.Text +"','"+ double.Parse(addingCost) * double.Parse(numericUpDown1.Value.ToString()) + "','" + DateTime.Now.ToString("hh:mm tt") + "','" + richTextBox1.Text.Trim() + "','" + Master.uName + "')";
                             sqliteHelper.insert(insertString, 1);

                             string InsString = "insert into inspection(inID,inText,inAddingDate,inAddingTime,inAddingBy) values " + "((select coalesce(max(inID),0)+1 from inspection),'" + " قام " + Master.uName + " " + "بإخراج مادة من المستودع ','" + DateTime.Today.ToString("yyyy/MM/dd") + "','"+ DateTime.Now.ToString("hh:mm tt")+ "','النظام')";
                             sqliteHelper.insert(InsString, 0);

                            //   inputOutput.x3 = 1;
                            //    inputOutput.x5 = 1;
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
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void output_FormClosing(object sender, FormClosingEventArgs e)
        {
            inputOutput.x2 = 0;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectString = "";
            selectString = "select IID from Items where (IName ='" + comboBox1.Text + "') and (IAddingBy ='" + Master.uName + "')";
            Bid = sqliteHelper.selectWithReturn(selectString);

          //  selectString = "select IQuantity from Items where IID =" + Bid + " and IAddingBy ='" + Master.uName + "'";

                selectString = "select IPrice from Items where IName = '" + comboBox1.Text + "' and IAddingBy ='" + Master.uName + "'";

                price = sqliteHelper.selectWithReturn(selectString);

                textBox2.Text = string.Format("{0:n}", decimal.Parse(price));

                selectString = "select IQuantity from Items where IName = '" + comboBox1.Text + "' and IAddingBy ='" + Master.uName + "'";
                textBox4.Text = sqliteHelper.selectWithReturn(selectString);

                selectString = "select IUnit from Items where IName = '" + comboBox1.Text + "'and IAddingBy = '" + Master.uName + "'";
                textBox5.Text = sqliteHelper.selectWithReturn(selectString);


            
        }
    }
}
