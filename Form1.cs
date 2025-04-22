using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;

namespace urojaiWk
{
    public partial class mainPage : Form
    {
        public mainPage()
        {
            InitializeComponent();
        }
        /*
* Microsoft Windows [Version 10.0.22631.4602]
* (c) TO "Олегохолдинг" (TO "Olegoholding"). All rights secure.
* License to use GNU-4.0
* Use, and have fun
*/
        private void enterBtn_Click(object sender, EventArgs e)
        {
            secondPage.seconPage transfer = new secondPage.seconPage();
            transfer.Show();
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }
    }
}
