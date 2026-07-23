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
    public partial class EdItem : Form
    {
        public EdItem()
        {
            InitializeComponent();
        }

        public string itemId = "";
        public string UnitName = "";
        private string  unitQuantity = "";


        private void EdItem_Load(object sender, EventArgs e)
        {
            textBox1.Text = DateTime.Now.ToString("yyyy/MM/dd hh:mm");
            if(Master.uName == "Home")
            {
                string selectString = "select DISTINCT trim(Uname) from Units";
                sqliteHelper.select(selectString, bunifuDropdown1);

            }
            else
            {
                string selectString = "select DISTINCT trim(Uname) from Units where UAddingBy ='" + Master.uName + "'";
                sqliteHelper.select(selectString, bunifuDropdown1);
            }
            bunifuDropdown1.SelectedIndex = bunifuDropdown1.Items.IndexOf(UnitName);
        }


        private void EdItem_FormClosing(object sender, FormClosingEventArgs e)
        {
            Items.x2 = 0;
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            string selectString = "select UQuantity from Units where Uname ='" + bunifuDropdown1.Text + "'";
            unitQuantity = sqliteHelper.selectWithReturn(selectString);

            if (MessageBox.Show("هل تريد الحفظ بالتأكيد ؟", "مدير الوحدات", MessageBoxButtons.YesNo) ==
              System.Windows.Forms.DialogResult.Yes)
            {
                try
                {

                    float payment = float.Parse(textBox3.Text);
                    DateTime dateObject;
                    if (DateTime.TryParse(textBox1.Text, out dateObject) && payment > 0)
                    {
                        string updateString = "update Items set IName = '" + textBox2.Text + "', IUnit = '" + bunifuDropdown1.Text + "', INumberInUnits  = '" + unitQuantity + "', IPrice  = '" + textBox3.Text + "', IAddingDate  = '" + textBox1.Text.Replace("/", "-") + "', INots  = '" + richTextBox1.Text + "', IAddingBy  = '" + Master.uName + "' where IID ='" + itemId + "'";
                        sqliteHelper.upDate(updateString, 1);
                        string InsString = "insert into inspection (inID,inText,inAddingDate,inAddingBy) values " +
                                  "((select coalesce(max(inID),0)+1 from inspection),'" + " قام " + Master.uName + " " + "بتعديل بيانات مادة في المستودع ','" + DateTime.Today.ToString("yyyy-MM-dd hh:mm") + "','النظام')";
                        sqliteHelper.insert(InsString, 0);
                        Items.x3 = 1;
                        this.Close();
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
           /* string selectString = "select UQuantity from Units where Uname ='" + bunifuDropdown1.Text + "'";
            unitQuantity = sqliteHelper.selectWithReturn(selectString);*/
        }
    }
}
