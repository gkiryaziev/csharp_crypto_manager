using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GAKiryaziev;

namespace CryptoManager
{
	public partial class frmMain : Form
	{
		public frmMain()
		{
			InitializeComponent();
		}
		
		private void btnCreate_Click(object sender, EventArgs e)
		{
			txtResult.Text = MyCrypt.SHA1(txtPassword.Text);
			txtResult2.Text = MyCrypt.SHA256(txtPassword.Text);
		}
	}
}
