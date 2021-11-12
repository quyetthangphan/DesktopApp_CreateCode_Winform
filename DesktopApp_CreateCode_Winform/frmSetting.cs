using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopApp_CreateCode_Winform
{
    public partial class frmSetting : Form
    {
        ItemCustomFile.AllFile itemCustomFile = new ItemCustomFile.AllFile();
        public frmSetting()
        {
            InitializeComponent();
        }
        
        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            if (txtNhapURL.Text != "")
            {

                string str = checkSpace(txtNhapURL.Text);
                itemCustomFile.writeFile(txtNhapURL.Text);
                MessageBox.Show("Cập nhật link thành công");
                this.Close();
            }
            

        }
         // dasdas dasdas 
        private string checkSpace(string text)
        {
            string str = string.Join("", text.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));

            return str;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmUpdateTeamcs frmUpdate = new frmUpdateTeamcs();
            frmUpdate.Show();
            this.Hide();
        }
    }
}
