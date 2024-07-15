using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wizard_Of_Set_Up
{
	public partial class FakeAlarm : Form
	{
		public static List<FakeAlarm> alarms = new List<FakeAlarm>();

		public FakeAlarm()
		{
			InitializeComponent();
			alarms.Add(this);
		}

		private void FakeAlarm_FormClosing(object sender, FormClosingEventArgs e)
		{
			alarms = null;
		}

		private void FakeAlarm_Load(object sender, EventArgs e)
		{
			Random random = new Random();
			// Get the working area of the primary screen
			Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;

			// Calculate the maximum X and Y coordinates
			int maxX = workingArea.Width - this.Width;
			int maxY = workingArea.Height - this.Height;

			// Generate random X and Y coordinates
			int randomX = random.Next(workingArea.X, workingArea.X + maxX);
			int randomY = random.Next(workingArea.Y, workingArea.Y + maxY);

			// Move the form to the random coordinates
			this.Location = new Point(randomX, randomY);

			int textId = random.Next(0, 100);
			if (textId < 50)
			{
				ErrorLabel.Text = "Error";
			} else if (textId < 60)
			{
				ErrorLabel.Text = "Big Error";
			} else if (textId < 70)
			{
				ErrorLabel.Text = "Oh No";
			} else if (textId < 80)
			{
				ErrorLabel.Text = "Uh Oh";
			} else if (textId < 85)
			{
				ErrorLabel.Text = "Bad Thing Hit";
			} else if (textId < 90)
			{
				ErrorLabel.Text = "Too Much Whimsy";
			} else if (textId < 95)
			{
				ErrorLabel.Text = "Crisis";
			} else if (textId == 95)
			{
				ErrorLabel.Text = "Womp Womp";
			} else if (textId == 96)
			{
				ErrorLabel.Text = "LOL";
			} else if (textId == 97)
			{
				ErrorLabel.Text = "Get Rekt";
			} else if (textId == 98)
			{
				ErrorLabel.Text = "Pwned";
			} else
			{
				ErrorLabel.Text = "%*$# You";
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
