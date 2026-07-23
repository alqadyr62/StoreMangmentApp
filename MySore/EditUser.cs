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
    public partial class EditUser : Form
    {
        public EditUser()
        {
            InitializeComponent();
        }

        public string Uid = "";

        private void EditUser_Load(object sender, EventArgs e)
        {
            textBox3.Text = DateTime.Today.ToString("yyyy/MM/dd");

        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل تريد الحفظ بالتأكيد ؟", "مدير الوحدات", MessageBoxButtons.YesNo) ==
              System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    DateTime dateObject;
                    if (DateTime.TryParse(textBox3.Text, out dateObject))
                    {
                        string updateString = "update Users_Login set Username = '" + textBox1.Text + "', Password = '" + textBox2.Text + "', AddingDate  = '" + textBox3.Text + "' where UID ='" + Uid + "'";
                        sqliteHelper.upDate(updateString, 1);

                        string InsString = "insert into inspection (inID,inText,inAddingDate,inAddingBy) values " +
                                 "((select coalesce(max(inID),0)+1 from inspection),'" + " قام " + Master.uName + " " + "بتعديل بيانات مستخدم ','" + DateTime.Today.ToString("yyyy/MM/dd") + "','النظام')";
                        sqliteHelper.insert(InsString, 0);
                        this.Close();
                    }
                }
                catch
                {
                    MessageBox.Show("ادخل تاريخ صحيح");
                    textBox3.Text = DateTime.Today.ToString("yyyy/MM/dd");
                }
            }
        }
    }
}
