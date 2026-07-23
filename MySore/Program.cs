
using DeviceId;
using myClinic;
using MyStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySore
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]


        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            /* if (sqliteHelper.loadReq("subTrigger") == "0")
             {
              Application.Run(new reg());

             }else if (sqliteHelper.loadReq("subTrigger") == "1")
             {

                 string selectString = "select RegDeviceID from Regstration where RegDeviceID ='" + new DeviceIdBuilder().AddMachineName().ToString()+ "' and RegType ='subscraption'";
                 if(sqliteHelper.isFound(selectString))
                 {*/
            Application.Run(new Form1());
            /*  }else
              {
                  MessageBox.Show("معرف الجهاز هذا غير مسجل");
              }*/



        }
            
        }
}


