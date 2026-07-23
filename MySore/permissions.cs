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
    public partial class permissions : Form
    {
        public permissions()
        {
            InitializeComponent();
        }

        public string UID = "";
        string selectString = "";

        string R1 = "";
        string R2 = "";
        string R3 = "";
        string R4 = "";
        string R5 = "";
        string R6 = "";
        string R7 = "";
        string R8 = "";
        string R9 = "";
        string R10 = "";
        string R11 = "";
        string R12 = "";
        string R13 = "";
        string R14 = "";
        string R15 = "";
        string R16 = "";
        string R17 = "";





        private void permissions_Load(object sender, EventArgs e)
        {
            selectString = "select R1 from permissions where UID =" + UID;
            if (sqliteHelper.selectWithReturn(selectString) == "1")
            {
                checkBox1.Checked = true;
            }
            selectString = "select R2 from permissions where UID =" + UID;
            if (sqliteHelper.selectWithReturn(selectString) == "1")
            {
                checkBox2.Checked = true;
            }
            selectString = "select R3 from permissions where UID =" + UID;
            if (sqliteHelper.selectWithReturn(selectString) == "1")
            {
                checkBox3.Checked = true;
            }
            selectString = "select R4 from permissions where UID =" + UID;
            if (sqliteHelper.selectWithReturn(selectString) == "1")
            {
                checkBox4.Checked = true;
            }
            selectString = "select R5 from permissions where UID =" + UID;
            if (sqliteHelper.selectWithReturn(selectString) == "1")
            {
                checkBox5.Checked = true;
            }
            selectString = "select R6 from permissions where UID =" + UID;
            if (sqliteHelper.selectWithReturn(selectString) == "1")
            {
                checkBox6.Checked = true;
            }
            selectString = "select R7 from permissions where UID =" + UID;
            if (sqliteHelper.selectWithReturn(selectString) == "1")
            {
                checkBox7.Checked = true;
            }
            selectString = "select R8 from permissions where UID =" + UID;
            if (sqliteHelper.selectWithReturn(selectString) == "1")
            {
                checkBox8.Checked = true;
            }
            selectString = "select R9 from permissions where UID =" + UID;
            if (sqliteHelper.selectWithReturn(selectString) == "1")
            {
                checkBox9.Checked = true;
            }
            selectString = "select R10 from permissions where UID =" + UID;
            if (sqliteHelper.selectWithReturn(selectString) == "1")
            {
                checkBox10.Checked = true;
            }
            selectString = "select R11 from permissions where UID =" + UID;
            if (sqliteHelper.selectWithReturn(selectString) == "1")
            {
                checkBox11.Checked = true;
            }
            selectString = "select R12 from permissions where UID =" + UID;
            if (sqliteHelper.selectWithReturn(selectString) == "1")
            {
                checkBox12.Checked = true;
            }
            selectString = "select R13 from permissions where UID =" + UID;
            if (sqliteHelper.selectWithReturn(selectString) == "1")
            {
                checkBox13.Checked = true;
            }
            selectString = "select R14 from permissions where UID =" + UID;
            if (sqliteHelper.selectWithReturn(selectString) == "1")
            {
                checkBox14.Checked = true;
            }
            selectString = "select R15 from permissions where UID =" + UID;
            if (sqliteHelper.selectWithReturn(selectString) == "1")
            {
                checkBox15.Checked = true;
            }
            selectString = "select R16 from permissions where UID =" + UID;
            if (sqliteHelper.selectWithReturn(selectString) == "1")
            {
                checkBox16.Checked = true;
            }
            selectString = "select R17 from permissions where UID =" + UID;
            if (sqliteHelper.selectWithReturn(selectString) == "1")
            {
                checkBox17.Checked = true;
            }
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل تريد الحفظ بالتأكيد ؟", "مدير المستخدمين", MessageBoxButtons.YesNo) ==
             System.Windows.Forms.DialogResult.Yes)
            {


                if (checkBox1.Checked) R1 = "1"; else R1 = "0";
                if (checkBox2.Checked) R2 = "1"; else R2 = "0";
                if (checkBox3.Checked) R3 = "1"; else R3 = "0";
                if (checkBox4.Checked) R4 = "1"; else R4 = "0";
                if (checkBox5.Checked) R5 = "1"; else R5 = "0";
                if (checkBox6.Checked) R6 = "1"; else R6 = "0";
                if (checkBox7.Checked) R7 = "1"; else R7 = "0";
                if (checkBox8.Checked) R8 = "1"; else R8 = "0";
                if (checkBox9.Checked) R9 = "1"; else R9 = "0";
                if (checkBox10.Checked) R10 = "1"; else R10 = "0";
                if (checkBox11.Checked) R11 = "1"; else R11 = "0";
                if (checkBox12.Checked) R12 = "1"; else R12 = "0";
                if (checkBox13.Checked) R13 = "1"; else R13 = "0";
                if (checkBox14.Checked) R14 = "1"; else R14 = "0";
                if (checkBox15.Checked) R15 = "1"; else R15 = "0";
                if (checkBox16.Checked) R16 = "1"; else R16 = "0";
                if (checkBox17.Checked) R17 = "1"; else R17 = "0";



                string updateString = "update permissions set R1 =" + R1 + "," + "R2 =" + R2 + "," + "R3 =" + R3 + "," + "R4 =" + R4
                    + "," + "R5 =" + R5 + "," + "R6 =" + R6 + "," + "R7 =" + R7 + "," + "R8 =" + R8 + "," + "R9 =" + R9
                    + "," + "R9 =" + R9 + "," + "R10 =" + R10 + "," + "R11 =" + R11 + "," + "R12 =" + R12 + "," + "R13 =" + R13
                    + "," + "R14 =" + R14 + "," + "R15 =" + R15 + "," + "R16 =" + R16 + "," + "R17 =" + R17 + " where UID =" + UID;
                sqliteHelper.upDate(updateString, 1);

                string InsString = "insert into inspection (inID,inText,inAddingDate,inAddingBy) values " +
                              "((select coalesce(max(inID),0)+1 from inspection),'" + " قام " + Master.uName + " " + "بتعديل أذونات مستخدم ','" + DateTime.Today.ToString("yyyy-MM-dd") + "','النظام')";
                sqliteHelper.insert(InsString, 0);
                /*string insertString = "insert into permissions (PID,UID,R1,R2,R3,R4,R5,R6,R7,R8,R9,R10,R11,R12,R13,R14,R15,R16) values ((select coalesce(max(PID),0)+1 from permissions)," +
                    UID + "," + R1 + "," + R2 + "," + R3 + "," + R4 + "," + R5 + "," + R6 + "," + R7 + "," + R8 + "," + R9 + "," + R10 + "," + R11 + "," + R12 + "," + R13 + "," + R14 + "," + R15 + "," + R16 + ")";

                sqliteHelper.insert(insertString, 1);*/

            }










        }
    }
}
