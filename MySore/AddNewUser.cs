using Bunifu.UI.WinForms;
//using Guna.UI2.WinForms.Suite;
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
    public partial class AddNewUser : Form
    {
        public AddNewUser()
        {
            InitializeComponent();
        }

        private void AddNewUser_Load(object sender, EventArgs e)
        {
            textBox3.Text = DateTime.Now.ToString("yyyy/MM/dd");
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("هل تريد الحفظ بالتأكيد ؟", "مدير المستخدمين", MessageBoxButtons.YesNo) ==
              System.Windows.Forms.DialogResult.Yes)
            {
                try
                {


                    DateTime dateObject;
                    if (DateTime.TryParse(textBox3.Text, out dateObject) )
                    {
                        string insertString = "insert into Users_Login (UID,Username , Password , AddingDate,AddingBy) values ((select coalesce(max(UID),0)+1 from Users_Login),'"
                           + textBox1.Text.Trim() + "','" + textBox2.Text.Trim() + "','" + textBox3.Text.Replace("/", "-") + "','" + Master.uName + "')";
                        sqliteHelper.insert(insertString, 1);
                        string InsString = "insert into inspection (inID,inText,inAddingDate,inAddingTime,inAddingBy) values " +
                                 "((select coalesce(max(inID),0)+1 from inspection),'" + " قام " + Master.uName + " " + "بإنشاء مستخدم جديد ','" + DateTime.Today.ToString("yyyy/MM/dd") + "','" + DateTime.Now.ToString("hh:mm tt") + "','النظام')";
                        sqliteHelper.insert(InsString, 0);
                        Users.x1 = 1;



                        insertString = "insert into permissions (PID,UID,R1,R2,R3,R4,R5,R6,R7,R8,R9,R10,R11,R12,R13,R14,R15,R16) values ((select coalesce(max(PID),0)+1 from permissions), (select coalesce(max(UID), 0) + 1 from permissions)"
                        + "," + 1 + "," + 1 + "," + 1 + "," + 1 + "," + 1 + "," + 1 + "," + 1 + "," + 1 + "," + 1 + "," + 1 + "," + 1 + "," + 1 + "," + 1 + "," + 1 + "," + 1 + "," + 1 + ")";

                        sqliteHelper.insert(insertString, 0);
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

        private void AddNewUser_FormClosing(object sender, FormClosingEventArgs e)
        {
            Users.x2 = 0;
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}
