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

namespace MySore
{
    public partial class AddNewUnit : Form
    {
        public AddNewUnit()
        {
            InitializeComponent();
        }

        private void AddNewUnit_Load(object sender, EventArgs e)
        {
            textBox1.Text = DateTime.Now.ToString("yyyy/MM/dd");
        }

        private void AddNewUnit_FormClosing(object sender, FormClosingEventArgs e)
        {
            Units.x1 = 0;
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل تريد الحفظ بالتأكيد ؟", "مدير الوحدات", MessageBoxButtons.YesNo) ==
               System.Windows.Forms.DialogResult.Yes)
            {


                try
                {


                    DateTime dateObject;
                    if (DateTime.TryParse(textBox1.Text, out dateObject))
                    {if(textBox2.Text != "")
                        {
                            string insertString = "insert into Units (UID,Uname , UQuantity , UaddingDate,UaddingTime ,UNotes,UAddingBy) values ((select coalesce(max(UID),0)+1 from Units),'"
                         + textBox2.Text.Trim() + "','" + textBox3.Text.Trim() + "','" + textBox1.Text + "','" + DateTime.Now.ToString("hh:mm tt") + "','" + richTextBox1.Text + "','" + Master.uName + "')";
                            sqliteHelper.insert(insertString, 1);
                            string InsString = "insert into inspection (inID,inText,inAddingDate,inAddingTime,inAddingBy) values " +
                                     "((select coalesce(max(inID),0)+1 from inspection),'" + " قام " + Master.uName + " " + "بإضافة وحدة جديدة ','" + DateTime.Today.ToString("yyyy/MM/dd") + "','" + DateTime.Now.ToString("hh:mm tt") + "','النظام')";
                            sqliteHelper.insert(InsString, 0);

                            Units.x4 = 1;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("اسم الوحدة يجب أن لا يكون فارغ");
                        }
                      
                    }
                }
                catch
                {
                    MessageBox.Show("تاريخ الادخال غير صحيح");
                    textBox1.Text = DateTime.Today.ToString("yyyy/MM/dd");

                }
            }

        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                textBox2.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                richTextBox1.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                bunifuButton1.Focus();
                e.Handled = true;
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                textBox3.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                textBox1.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                bunifuButton1.Focus();
                e.Handled = true;
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                richTextBox1.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                textBox2.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                bunifuButton1.Focus();
                e.Handled = true;
            }
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Down)
            {
                textBox1.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                textBox3.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                bunifuButton1.Focus();
                e.Handled = true;
            }
        }
    }
}
