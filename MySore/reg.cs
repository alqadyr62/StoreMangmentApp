using Bunifu.UI.WinForms;
using DeviceId;
using myClinic;
using MySore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyStore
{
    public partial class reg : Form
    {
        public reg()
        {
            InitializeComponent();
        }

        string trail = "";
        string subscraption= "";
        string deviceID = "";


        private void reg_Load(object sender, EventArgs e)
        {
            guna2ComboBox1.Items.Add("تجربة");
            guna2ComboBox1.Items.Add("شراء");
            textBox1.Enabled = false;
            textBox3.Enabled = false;
            guna2Button1.Enabled = false;
            deviceID = new DeviceIdBuilder().AddMachineName().ToString();


        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = sqliteHelper.loadReq("trail");
        }

        private void bunifuDropdown1_SelectedIndexChanged(object sender, EventArgs e)
        {
                  }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
           
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            if (guna2ComboBox1.Text == "تجربة")
            {
                string insertString = "insert into Regstration (RegID,RegType,RegTrailNumber,RegActiveNumber,RegDeviceID,RegAddingDate) values ((select coalesce(max(RegID),0)+1 from Regstration),'" +
                       "trail','" + textBox3.Text + "','" + textBox1.Text + "','" + deviceID + "','" + DateTime.Today.ToString("yyyy/MM/dd") + "')";
                sqliteHelper.insert(insertString, 0);

                trail = (int.Parse(trail) - 1).ToString();
                sqliteHelper.saveReg("trail", trail);
                this.Hide();
                f1.Show();
            }
            else if (guna2ComboBox1.Text == "شراء")
            {

                string selectString = "select RegActiveNumber from Regstration where RegActiveNumber ='" + textBox1.Text + "'";
                if (sqliteHelper.isFound(selectString))
                {
                    MessageBox.Show("رقم الرخصة مستخدم من قبل");
                }
                else
                {
                    if (sqliteHelper.loadReq("subscraption1") == textBox1.Text ||
                        sqliteHelper.loadReq("subscraption2") == textBox1.Text ||
                        sqliteHelper.loadReq("subscraption3") == textBox1.Text ||
                        sqliteHelper.loadReq("subscraption4") == textBox1.Text ||
                        sqliteHelper.loadReq("subscraption5") == textBox1.Text ||
                        sqliteHelper.loadReq("subscraption6") == textBox1.Text ||
                        sqliteHelper.loadReq("subscraption7") == textBox1.Text ||
                        sqliteHelper.loadReq("subscraption8") == textBox1.Text ||
                        sqliteHelper.loadReq("subscraption9") == textBox1.Text ||
                        sqliteHelper.loadReq("subscraption10") == textBox1.Text ||
                        sqliteHelper.loadReq("subscraption11") == textBox1.Text ||
                        sqliteHelper.loadReq("subscraption12") == textBox1.Text ||
                        sqliteHelper.loadReq("subscraption13") == textBox1.Text ||
                        sqliteHelper.loadReq("subscraption14") == textBox1.Text)
                    {
                        string insertString = "insert into Regstration (RegID,RegType,RegTrailNumber,RegActiveNumber,RegDeviceID,RegAddingDate) values ((select coalesce(max(RegID),0)+1 from Regstration),'" +
                       "subscraption','" + textBox3.Text + "','" + textBox1.Text + "','" + deviceID + "','" + DateTime.Today.ToString("yyyy/MM/dd") + "')";
                        sqliteHelper.insert(insertString, 0);
                        sqliteHelper.saveReg("subTrigger", "1");
                        MessageBox.Show("شكرا للشراء");
                        this.Hide();
                        f1.Show();

                    }
                    else
                    {
                        MessageBox.Show("رقم الترخيص هذا غير صالح");
                    }
                }

            }
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (guna2ComboBox1.Text == "تجربة")
            {
                textBox3.Enabled = true;
                textBox1.Enabled = false;
                Random rnd1 = new Random();
                textBox3.Text = rnd1.Next().ToString();
                trail = sqliteHelper.loadReq("trail");
                if (int.Parse(trail) <= 10 && int.Parse(trail) > 0)
                {

                    guna2Button1.Enabled = true;
                }
                else
                {
                    MessageBox.Show("انتهت التجربة");
                }
            }
            else if (guna2ComboBox1.Text == "شراء")
            {
                textBox3.Text = "";
                textBox3.Enabled = false;
                textBox1.Enabled = true;
                guna2Button1.Enabled = true;

            }

        }
    }
}
