namespace Wizard_Of_Set_Up
{
	partial class WizzoDialogue
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WizzoDialogue));
			this.WizzoGroupBox = new System.Windows.Forms.GroupBox();
			this.WizzoText = new System.Windows.Forms.Label();
			this.WizzoGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// WizzoGroupBox
			// 
			this.WizzoGroupBox.Controls.Add(this.WizzoText);
			this.WizzoGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.WizzoGroupBox.Location = new System.Drawing.Point(0, 0);
			this.WizzoGroupBox.Name = "WizzoGroupBox";
			this.WizzoGroupBox.Size = new System.Drawing.Size(344, 171);
			this.WizzoGroupBox.TabIndex = 0;
			this.WizzoGroupBox.TabStop = false;
			this.WizzoGroupBox.Text = "Wizzo";
			// 
			// WizzoText
			// 
			this.WizzoText.Dock = System.Windows.Forms.DockStyle.Fill;
			this.WizzoText.Location = new System.Drawing.Point(3, 16);
			this.WizzoText.Name = "WizzoText";
			this.WizzoText.Size = new System.Drawing.Size(338, 152);
			this.WizzoText.TabIndex = 0;
			this.WizzoText.Text = "text";
			// 
			// WizzoDialogue
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(344, 171);
			this.Controls.Add(this.WizzoGroupBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "WizzoDialogue";
			this.Text = "Wizzo";
			this.Load += new System.EventHandler(this.WizzoDialogue_Load);
			this.WizzoGroupBox.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox WizzoGroupBox;
		private System.Windows.Forms.Label WizzoText;
	}
}