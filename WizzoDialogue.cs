using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wizard_Of_Set_Up
{
	public partial class WizzoDialogue : Form
	{
		string text;
		public WizzoDialogue(string text)
		{
			InitializeComponent();
			this.text = text;
		}

		private void WizzoDialogue_Load(object sender, EventArgs e)
		{
			WizzoText.Text = text;
		}
	}
}
